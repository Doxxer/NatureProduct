using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using NatureProduct.Resources;

namespace NatureProduct
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Конструктор
        public MainPage()
        {
            InitializeComponent();

            // Пример кода для локализации ApplicationBar
            //BuildLocalizedApplicationBar();
        }

       
        private void EnterManually_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/ManualEnter.xaml", UriKind.Relative));
        }

        private void MakePhoto_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Scan.xaml", UriKind.Relative));
        }

       
    }
}