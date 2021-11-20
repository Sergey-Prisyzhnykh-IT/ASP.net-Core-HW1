using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace HomeWork1
{
    public class HW1 : Controller
    {
        static double _price = 1000;

        static string _customsDuty = CustomsDuty(_price);

        static string _path = CreateTempDirectoryAndWrite(@"C:\TempDuty");

        StringBuilder _lastRequests = ReadFile(_path);


        /// <summary>
        /// создает файл и записывает в него все цены и пошлины (возращает путь потому что лень стало заморачиваться( )
        /// </summary>
        static string CreateTempDirectoryAndWrite(string path)
        {
            try
            {
                DirectoryInfo dirInfo = new DirectoryInfo(path);
                if (!dirInfo.Exists)
                    dirInfo.Create();

                string writePath = @$"{path}\customsDuty.txt";

                using (StreamWriter sw = new StreamWriter(writePath, true, System.Text.Encoding.Default))
                {
                    sw.WriteLine($"цена = {_price} {_customsDuty}");
                }
                return writePath.ToString();
            }
            catch (Exception exp)
            {
                return string.Empty;
            }

        }
        /// <summary>
        /// читает файл 
        /// </summary>
        static StringBuilder ReadFile(string path)
        {
            try
            {
                using (StreamReader sr = new StreamReader(path, System.Text.Encoding.Default))
                {
                    string line;
                    StringBuilder sb = new StringBuilder(100);

                    while ((line = sr.ReadLine()) != null)
                    {
                        sb.Append(line + "\n");
                    }

                    return sb;
                }
            }
            catch (Exception exp)
            {
                StringBuilder sb = new StringBuilder(100);

                return sb.Append("Что то пошло не так" + exp.ToString());
            }
        }


        static public string CustomsDuty(double price)
        {
            if (price <= 200)
                return "Пошлины нет :)";
            else
            {
                var excess = price - 200;
                var customsDuty = excess * 0.15;
                return "Пошлина = " + customsDuty.ToString();
            }

        }

    }
}
