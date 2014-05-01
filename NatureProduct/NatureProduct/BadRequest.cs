using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NatureProduct.NetworkConnect;
using Microsoft.Phone.Net.NetworkInformation;

namespace NatureProduct
{
    class BadRequest
    {
        public static string response(string name) {
            string rez = "";
            if (name.Equals("0"))
            {
                rez = "Отсутствует подключение к интернету";
            }
            if (name.Equals("1")) {
                rez = "Сервер недоступен";
            }
            else if (name.Equals("2"))
            {
                rez = "Штрихкоды с 2ки предназначенны для внутреннего использования. (например: весовые продукты)";
            }
            else if (name.Equals("3"))
            {
                rez = "Товар не найден";
            }
            return rez;
        }

        public static Boolean checkNetworkConnection()
        {
            var ni = NetworkInterface.NetworkInterfaceType;
            return NetworkInterface.GetIsNetworkAvailable();            
        }
    }
}
