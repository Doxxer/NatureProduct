﻿#pragma checksum "C:\Users\KateKate\documents\visual studio 2012\Projects\NatureProduct\NatureProduct\ElistProduct.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "9E30C8C9218C39DACF671346BCBCDC78"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.34014
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace NatureProduct {
    
    
    public partial class ElistProduct : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal Microsoft.Phone.Controls.Panorama ProdName;
        
        internal Microsoft.Phone.Controls.LongListSelector GroupedEList;
        
        internal Microsoft.Phone.Controls.LongListSelector GroupedExtenList;
        
        internal Microsoft.Phone.Controls.LongListSelector GroupedDescribeList;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/NatureProduct;component/ElistProduct.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.ProdName = ((Microsoft.Phone.Controls.Panorama)(this.FindName("ProdName")));
            this.GroupedEList = ((Microsoft.Phone.Controls.LongListSelector)(this.FindName("GroupedEList")));
            this.GroupedExtenList = ((Microsoft.Phone.Controls.LongListSelector)(this.FindName("GroupedExtenList")));
            this.GroupedDescribeList = ((Microsoft.Phone.Controls.LongListSelector)(this.FindName("GroupedDescribeList")));
        }
    }
}

