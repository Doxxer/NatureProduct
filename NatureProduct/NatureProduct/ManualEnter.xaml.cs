﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace NatureProduct
{
    public partial class ManualEnter : PhoneApplicationPage
    {
        public ManualEnter()
        {
            InitializeComponent();
        }

        private void HandleNumbers_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("The file will be saved here.", "File Save", MessageBoxButton.OKCancel);
        }
    }
}