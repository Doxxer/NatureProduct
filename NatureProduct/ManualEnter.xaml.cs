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
        public ManualEnter()
        {
            InitializeComponent();
        }        

       /* private void PhoneApplicationPage_Loaded_1(object sender, RoutedEventArgs e)
        {
            BarCodeNumbers.Text = NavigationContext.QueryString["BarString"].ToString();
        
        }*/    

        private void HandleNumbers_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(BarCodeNumbers.Text);
            
            Good ecompObj = GoodJson.deserializeGood(NetworkConnector.getRequest(BarCodeNumbers.Text));

            MessageBox.Show(ecompObj.name);
            //handle sorry page
            //var obj = App.Current as App;
            //obj.sharEObj = ecompObj;
            //NavigationService.Navigate(new Uri("/ElistProduct.xaml", UriKind.Relative));
        }

        private void checkNetworkConnection() {
            var ni = NetworkInterface.NetworkInterfaceType;

            MessageBox.Show("Network Available: " + NetworkInterface.GetIsNetworkAvailable());

            if (ni == NetworkInterfaceType.Wireless80211) MessageBox.Show("Wireless");
            else if (ni == NetworkInterfaceType.None)
                MessageBox.Show("None");
        }
    }
}