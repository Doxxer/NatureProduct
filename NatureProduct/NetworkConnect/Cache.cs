using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkConnect
{
    class Cache
    {
        private static int MAX_LIMIT = 100;

        private void saveCache(string barCode, string jsonGood)
        {
            IsolatedStorageFile fileStorage = IsolatedStorageFile.GetUserStoreForApplication();
            IsolatedStorageSettings settingsStorage = IsolatedStorageSettings.ApplicationSettings;

            if (!fileStorage.FileExists("cache"))
            {
                IsolatedStorageFileStream cacheFile = fileStorage.CreateFile("cache");
            }

            try
            {
                List<string> stores = new List<string>();
                using (StreamReader sr = new StreamReader(fileStorage.OpenFile("cache", FileMode.Open)))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        stores.Add(line);
                    }
                }

                if (stores.Count() == MAX_LIMIT)
                {
                    if (settingsStorage.Contains(barCode))
                    {
                        settingsStorage.Remove(barCode);
                    }
                    stores.RemoveAt(0);
                }
                stores.Add(barCode);
                using (StreamWriter sw = new StreamWriter(fileStorage.OpenFile("cache", FileMode.Open)))
                {
                    foreach (string line in stores)
                    {
                        sw.WriteLine(line);
                    }
                }
                settingsStorage.Add(barCode, jsonGood);
            }
            catch (IsolatedStorageException ex)
            {
                throw ex;
            }
        }

        private string getFromCache(string barCode)
        {
            IsolatedStorageSettings settingsStorage = IsolatedStorageSettings.ApplicationSettings;

            if (settingsStorage.Contains(barCode))
            {
                return (string)settingsStorage[barCode];
            }

            return "";
        }
    }
}
