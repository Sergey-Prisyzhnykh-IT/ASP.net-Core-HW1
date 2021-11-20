using System.Globalization;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

/*–еализуйте страницу, котора€ будет показывать текущую дату и врем€ в полном формате 
 (включа€ название дн€ недели и мес€ца), на €зыке переданном в параметре language. ѕ
 араметр language передаетс€ в формате ISO 639-1 (ru, en, fr, cn и т. д.).*/

var fullData = FullData("fr-FR");

string FullData(string language)
{
    return DateTime.Now.ToString("dddd , MMM dd yyyy,hh:mm:ss", CultureInfo.GetCultureInfo(language));
}

app.MapGet("/", () => fullData);





/*Ќеобходимо создать веб приложение со страницей (/customs_duty), 
 котора€ будет вычисл€ть размер таможенной пошлины: в параметре
 price передаетс€ стоимость посылки. ѕошлина начисл€етс€ при превышении 200И, 
 а ее размер равен 15% от суммы превышени€.
—делайте отображение истории расчетов таможенной пошлины.*/

double _price = 1000;

var _customsDuty = CustomsDuty(_price);

var _path = CreateTempDirectoryAndWrite(@"C:\TempDuty");

var _lastRequests = ReadFile(_path);

/// <summary>
/// создает файл и записывает в него все цены и пошлины (возращает путь потому что лень стало заморачиватьс€( )
/// </summary>
string CreateTempDirectoryAndWrite(string path) 
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
StringBuilder ReadFile(string path)
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

        return sb.Append("„то то пошло не так" + exp.ToString());
    } 
}


string CustomsDuty(double price)
{
    if (price <= 200)
        return "ѕошлины нет :)";
    else 
    {
        var excess = price - 200;
        var customsDuty = excess * 0.15;
        return "ѕошлина = " + customsDuty.ToString();
    }
        
}

app.MapGet
    (
        "/customs_duty", () => _customsDuty + "\n\n" + "ѕрошлые запросы" +"\n" + _lastRequests.ToString()
    );

app.Run();
