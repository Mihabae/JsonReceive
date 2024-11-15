using System.Net;
using System.Text.Json;
namespace JsonReceive
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string api;
                api = textBox1.Text;
                WebRequest web = WebRequest.Create(api);
                web.Method = "POST";
                web.ContentType = "application/x-www-form-urlencoded";

                string answer = string.Empty;

                // ��������� ������ �� API
                using (WebResponse response = web.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream, System.Text.Encoding.UTF8))
                {
                    answer = reader.ReadToEnd(); // ������ ������ � ������
                }

                // �������������� JSON-������ � ������ ��� ������������� ��������
                var options = new JsonSerializerOptions
                {
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping, // ���������� �������
                    WriteIndented = true // ��� �������������� �����
                };

                // �������������� ������ � ������ �����
                string[] decodedData = JsonSerializer.Deserialize<string[]>(answer, options);

                // ���������� ������ � JSON-���� � UTF-8 ����������
                int i = 0;
                string filePath = $"GGson.json";
                while (File.Exists(filePath))
                {
                    i++;
                    filePath = $"GGson{i}.json";
                }
                File.WriteAllText(filePath, JsonSerializer.Serialize(decodedData, options), System.Text.Encoding.UTF8);

                label1.Text = $"������ ������� ��������� � ����: {filePath}";
            }
            catch (Exception ex) { label1.Text = ex.Message; }
        }
    }
}
