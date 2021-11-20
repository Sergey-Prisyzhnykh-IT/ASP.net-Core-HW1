using System.Globalization;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

/*���������� ��������, ������� ����� ���������� ������� ���� � ����� � ������ ������� 
 (������� �������� ��� ������ � ������), �� ����� ���������� � ��������� language. �
 ������� language ���������� � ������� ISO 639-1 (ru, en, fr, cn � �. �.).*/

var fullData = FullData("fr-FR");

string FullData(string language)
{
    return DateTime.Now.ToString("dddd , MMM dd yyyy,hh:mm:ss", CultureInfo.GetCultureInfo(language));
}

app.MapGet("/", () => fullData);





/*���������� ������� ��� ���������� �� ��������� (/customs_duty), 
 ������� ����� ��������� ������ ���������� �������: � ���������
 price ���������� ��������� �������. ������� ����������� ��� ���������� 200�, 
 � �� ������ ����� 15% �� ����� ����������.
�������� ����������� ������� �������� ���������� �������.*/

double _price = 1000;

var _customsDuty = CustomsDuty(_price);

var _path = CreateTempDirectoryAndWrite(@"C:\TempDuty");

var _lastRequests = ReadFile(_path);

/// <summary>
/// ������� ���� � ���������� � ���� ��� ���� � ������� (��������� ���� ������ ��� ���� ����� ��������������( )
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
            sw.WriteLine($"���� = {_price} {_customsDuty}");
        }
        return writePath.ToString();
    }
    catch (Exception exp)
    {
        return string.Empty;
    }

}
/// <summary>
/// ������ ���� 
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

        return sb.Append("��� �� ����� �� ���" + exp.ToString());
    } 
}


string CustomsDuty(double price)
{
    if (price <= 200)
        return "������� ��� :)";
    else 
    {
        var excess = price - 200;
        var customsDuty = excess * 0.15;
        return "������� = " + customsDuty.ToString();
    }
        
}

app.MapGet
    (
        "/customs_duty", () => _customsDuty + "\n\n" + "������� �������" +"\n" + _lastRequests.ToString()
    );

app.Run();
