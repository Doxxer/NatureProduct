using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NatProductClientNetwork
{
    class NetworkConnector
    {
        private static readonly string address = "http://nature-product-422.appspot.com/gobo";

        public static string getResult(string request)
        {
            try
            {
                WebClient wc = new WebClient();
                return wc.UploadString(address, request);
            }
            catch (WebException e)
            {
                throw e;
            }
        }
    }
}
