using System.Globalization;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

/*Реализуйте страницу, которая будет показывать текущую дату и время в полном формате 
 (включая название дня недели и месяца), на языке переданном в параметре language. П
 араметр language передается в формате ISO 639-1 (ru, en, fr, cn и т. д.).*/
/*
var fullData = FullData("ru-RU");

string FullData(string language)
{
    return DateTime.Now.ToString("dddd , MMM dd yyyy,hh:mm:ss", CultureInfo.GetCultureInfo(language));
}*/

app.MapGet("/", () => DateTime.Now.ToString("dddd , MMM dd yyyy,hh:mm:ss"));

app.MapGet("/language", (string language) => DateTime.Now.ToString("dddd , MMM dd yyyy,hh:mm:ss", CultureInfo.GetCultureInfo(language)));


/*Необходимо создать веб приложение со страницей (/customs_duty), 
 которая будет вычислять размер таможенной пошлины: в параметре
 price передается стоимость посылки. Пошлина начисляется при превышении 200€, 
 а ее размер равен 15% от суммы превышения.
Сделайте отображение истории расчетов таможенной пошлины.*/

var _path = @"C:\TempDuty";

/// <summary>
/// создает файл и записывает в него все цены и пошлины (возращает путь потому что лень стало заморачиваться( )
/// </summary>
string CreateTempDirectoryAndWrite(string path, double price,string customsDuty) 
{
    try
    {
        DirectoryInfo dirInfo = new DirectoryInfo(path);
        if (!dirInfo.Exists)
            dirInfo.Create();

        string writePath = @$"{path}\customsDuty.txt";

        using (StreamWriter sw = new StreamWriter(writePath, true, System.Text.Encoding.Default))
        {
            sw.WriteLine($"цена = {price} {customsDuty}");
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

        return sb.Append("Что то пошло не так" + exp.ToString());
    } 
}


string CustomsDuty(double price)
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

StringBuilder MegaFunction(double price) 
{
    var customsDuty = CustomsDuty(price);
    var path = CreateTempDirectoryAndWrite(_path, price, customsDuty);
    return ReadFile(path);
}

app.MapGet
    (
        "/customs_duty", (double price) => CustomsDuty(price) + "\n\n" + "Прошлые запросы" + "\n" + MegaFunction(price)
    );

app.Run();
