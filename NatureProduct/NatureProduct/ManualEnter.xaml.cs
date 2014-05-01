using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using NatureProduct.NetworkConnect;
using Microsoft.Phone.Net.NetworkInformation;

namespace NatureProduct
{
    public partial class ManualEnter : PhoneApplicationPage
    {
        private string barCode;

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }
       
        public ManualEnter()
        {
            InitializeComponent();
        }        

        private void HandleNumbers_Click(object sender, RoutedEventArgs e)
        {
            barCode = BarCodeNumbers.Text;
            string response = Cache.getFromCache(barCode);
            if (response.Equals(""))
            {
                if (!BadRequest.checkNetworkConnection())
                {
                    NavigationService.Navigate(new Uri("/SorryPage.xaml?SorryString=" + BadRequest.response("0"), UriKind.Relative));
                }
                else
                {
                    NetworkHelper.getRequest(barCode, new UploadStringCompletedEventHandler(handler));
                }
            }
            else
            {
                addToInterface(response);
                Cache.saveCache(barCode, response);
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

        
    }
}