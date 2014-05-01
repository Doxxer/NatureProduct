using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace NatureProduct.NetworkConnect
{
    class NetworkHelper
    {
        private static readonly string address = "http://nature-product-422.appspot.com/gobolive";

        public static void getRequest(string request, UploadStringCompletedEventHandler callback)
        {
            WebClient wc = new WebClient();
            wc.UploadStringCompleted += callback;
            wc.UploadStringAsync(new Uri(address), "POST", "text=" + request);
        }
    }
}
