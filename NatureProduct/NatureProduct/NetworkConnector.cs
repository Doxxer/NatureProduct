using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NatProductClientNetwork
{
    class NetworkConnector
    {
        private static readonly string address = "http://nature-product-422.appspot.com/gobo";

        public static void sendRequest(string request)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(new Uri(address));
            httpWebRequest.Method = "POST";
            httpWebRequest.ContentLength = Encoding.UTF8.GetBytes(request).Length;
            httpWebRequest.ContentType = "text/plain";
            Stream dataStream = httpWebRequest.GetRequestStream();
            dataStream.Write(Encoding.UTF8.GetBytes(request), 0, Encoding.UTF8.GetBytes(request).Length);
            dataStream.Close();
            httpWebRequest.BeginGetResponse(new AsyncCallback(ReadCallback), httpWebRequest);
        }

        private static void ReadCallback(IAsyncResult asynchronousResult)
        {
            HttpWebRequest request = (HttpWebRequest)asynchronousResult.AsyncState;
            HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(asynchronousResult);
            using (StreamReader streamReader1 = new StreamReader(response.GetResponseStream()))
            {
                string resultString = streamReader1.ReadToEnd();
                //resultBlock.Text = "Using HttpWebRequest: " + resultString;
                System.IO.File.WriteAllText(@"C:\Users\Kirill Luchikhin\Documents\test.txt", resultString);
            }

        }
    }
}
