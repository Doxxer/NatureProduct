using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace NatureProduct.NetworkConnect
{

    public static class NetworkHelper
    {
        private static readonly string address = "http://nature-product-422.appspot.com/gobolive";

        public static void getRequest(string request)
        { 
            WebClient wc = new WebClient();
            wc.UploadStringCompleted += new UploadStringCompletedEventHandler(loadResponse);
            wc.UploadStringAsync(new Uri(address), "POST", "text=" + request);
        }

        public static void loadResponse(Object sender, UploadStringCompletedEventArgs e)
        {
            string response = e.Result;
            MessageBox.Show(GoodJson.deserializeGood(response).name);
        }
    }
}
