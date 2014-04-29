using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NatureProduct.NetworkConnect
{

    class NetworkConnector
    {
        private static readonly string address = "http://nature-product-422.appspot.com/gobo";

        public static string getRequest(string request)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(new Uri(address));
            httpWebRequest.Method = "POST";
            httpWebRequest.ContentLength = Encoding.UTF8.GetBytes(request).Length;
            httpWebRequest.ContentType = "text/plain";
            Stream dataStream = GetRequestStream(httpWebRequest);
            dataStream.Write(Encoding.UTF8.GetBytes(request), 0, Encoding.UTF8.GetBytes(request).Length);
            dataStream.Close();
            HttpWebResponse httpWebResponse = GetResponse(httpWebRequest);
            using (Stream stream = httpWebResponse.GetResponseStream())
            {
                StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                String responseString = reader.ReadToEnd();
                return responseString;
            }
        }

        public static HttpWebResponse GetResponse(HttpWebRequest request)
        {
            var dataReady = new AutoResetEvent(false);
            HttpWebResponse response = null;
            var callback = new AsyncCallback(delegate(IAsyncResult asynchronousResult)
            {
                response = (HttpWebResponse)request.EndGetResponse(asynchronousResult);
                dataReady.Set();
            });

            request.BeginGetResponse(callback, request);

            if (dataReady.WaitOne(10000))
            {
                return response;
            }

            return null;
        }

        public static Stream GetRequestStream(HttpWebRequest request)
        {
            var dataReady = new AutoResetEvent(false);
            Stream stream = null;
            var callback = new AsyncCallback(delegate(IAsyncResult asynchronousResult)
            {
                stream = (Stream)request.EndGetRequestStream(asynchronousResult);
                dataReady.Set();
            });

            request.BeginGetRequestStream(callback, request);
            if (!dataReady.WaitOne(10000))
            {
                return null;
            }

            return stream;
        }
    }
}
