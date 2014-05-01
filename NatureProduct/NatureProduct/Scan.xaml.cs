using System;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Phone.Controls;
using Microsoft.Devices;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using ZXing;
using System.Net;
using NatureProduct.NetworkConnect;
using System.Collections.Generic;


namespace NatureProduct
{
    public partial class Scan : PhoneApplicationPage
    {
        private PhotoCamera _phoneCamera;
        private IBarcodeReader _barcodeReader;
        private DispatcherTimer _scanTimer;
        private WriteableBitmap _previewBuffer;
        private string barCode;

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }

        public Scan()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            _phoneCamera = new PhotoCamera();
            _phoneCamera.Initialized += cam_Initialized;
            _phoneCamera.AutoFocusCompleted += _phoneCamera_AutoFocusCompleted;

            CameraButtons.ShutterKeyHalfPressed += CameraButtons_ShutterKeyHalfPressed;

            viewfinderBrush.SetSource(_phoneCamera);


            _scanTimer = new DispatcherTimer();
            _scanTimer.Interval = TimeSpan.FromMilliseconds(250);
            _scanTimer.Tick += (o, arg) => ScanForBarcode();

            viewfinderCanvas.Tap += new EventHandler<GestureEventArgs>(focus_Tapped);

            base.OnNavigatedTo(e);
        }

        void _phoneCamera_AutoFocusCompleted(object sender, CameraOperationCompletedEventArgs e)
        {
            Deployment.Current.Dispatcher.BeginInvoke(delegate()
            {
                focusBrackets.Visibility = Visibility.Collapsed;
            });
        }

        void focus_Tapped(object sender, GestureEventArgs e)
        {
            if (_phoneCamera != null)
            {
                if (_phoneCamera.IsFocusAtPointSupported == true)
                {
                    Point tapLocation = e.GetPosition(viewfinderCanvas);

                    focusBrackets.SetValue(Canvas.LeftProperty, tapLocation.X - 30);
                    focusBrackets.SetValue(Canvas.TopProperty, tapLocation.Y - 28);

                    double focusXPercentage = tapLocation.X / viewfinderCanvas.ActualWidth;
                    double focusYPercentage = tapLocation.Y / viewfinderCanvas.ActualHeight;

                    focusBrackets.Visibility = Visibility.Visible;
                    try
                    {
                        _phoneCamera.FocusAtPoint(focusXPercentage, focusYPercentage);
                    }
                    catch (Exception xe) 
                    {
                        MessageBox.Show("Can't recognize BarCode");
                        NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
                    }
                }
            }
        }

        void CameraButtons_ShutterKeyHalfPressed(object sender, EventArgs e)
        {
            _phoneCamera.Focus();
        }

        protected override void OnNavigatingFrom(System.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            _scanTimer.Stop();

            if (_phoneCamera != null)
            {
                _phoneCamera.Dispose();
                _phoneCamera.Initialized -= cam_Initialized;
                CameraButtons.ShutterKeyHalfPressed -= CameraButtons_ShutterKeyHalfPressed;
            }
        }

        void cam_Initialized(object sender, Microsoft.Devices.CameraOperationCompletedEventArgs e)
        {
            if (e.Succeeded)
            {
                this.Dispatcher.BeginInvoke(delegate()
                {
                    _phoneCamera.FlashMode = FlashMode.Off;
                    _previewBuffer = new WriteableBitmap((int)_phoneCamera.PreviewResolution.Width, (int)_phoneCamera.PreviewResolution.Height);

                    _barcodeReader = new BarcodeReader();

                    _barcodeReader.Options.TryHarder = true;
                    _barcodeReader.ResultFound += _bcReader_ResultFound;
                    _barcodeReader.Options.PossibleFormats = new List<BarcodeFormat>() { BarcodeFormat.EAN_13 };

                    _scanTimer.Start();
                });
            }
            else
            {
                Dispatcher.BeginInvoke(() =>
                {
                    MessageBox.Show("Unable to initialize the camera");
                });
            }
        }

        void _bcReader_ResultFound(Result obj)
        {
            try
            {
                barCode = obj.Text;
                if (!obj.Text.Equals(tbBarcodeData.Text))
                {
                    tbBarcodeType.Text = obj.BarcodeFormat.ToString();
                    tbBarcodeData.Text = obj.Text;

                    VibrateController.Default.Start(TimeSpan.FromMilliseconds(100));

                    line.Visibility = System.Windows.Visibility.Collapsed;
                    Ok.Visibility = System.Windows.Visibility.Visible;

                    string response = Cache.getFromCache(obj.Text);
                    if (response.Equals(""))
                    {
                        if (!BadRequest.checkNetworkConnection())
                        {
                            NavigationService.Navigate(new Uri("/SorryPage.xaml?SorryString=" + BadRequest.response("0"), UriKind.Relative));
                        }
                        else
                        {
                            NetworkHelper.getRequest(obj.Text, new UploadStringCompletedEventHandler(handler));
                        }
                    }
                    else
                    {
                        addToInterface(response);
                        Cache.saveCache(barCode, response);
                    }

                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Can't recognize BarCode");
                NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
            }
        
        }

        private void handler(object sender, UploadStringCompletedEventArgs e)
        {
            string response = e.Result;
            if (addToInterface(response))
            {
                Cache.saveCache(barCode, response);
            }
        }

        private Boolean addToInterface(string response)
        {
            var obj = App.Current as App;
            obj.sharEObj = GoodJson.deserializeGood(response);
            if (obj.sharEObj.name.Equals("1") || obj.sharEObj.name.Equals("2") || obj.sharEObj.name.Equals("3"))
            {
                NavigationService.Navigate(new Uri("/SorryPage.xaml?SorryString=" + BadRequest.response(obj.sharEObj.name), UriKind.Relative));
                return false;
            }

            NavigationService.Navigate(new Uri("/ElistProduct.xaml", UriKind.Relative));
            return true;
        }

        private void ScanForBarcode()
        {
            _phoneCamera.GetPreviewBufferArgb32(_previewBuffer.Pixels);
            _previewBuffer.Invalidate();

            _barcodeReader.Decode(_previewBuffer);
        }        
    }
}