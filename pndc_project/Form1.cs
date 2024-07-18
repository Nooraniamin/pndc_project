using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
namespace pndc_project
{
    public partial class Form1 : Form
    {
        private Dictionary<string, double> _currencies = new Dictionary<string, double>();
        public Form1()
        {
            InitializeComponent();
            var resp = GetlatestCurrencies();
            dynamic respObject = JsonConvert.DeserializeObject(resp);
            foreach(var item in respObject.data)
            {
                comboBox1.Items.Add(item.First.code.ToString());
                comboBox2.Items.Add(item.First.code.ToString());
                _currencies.Add(item.First.code.ToString(), item.First.value.Value);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private string GetlatestCurrencies()
        {
            var client = new RestClient("https://api.currencyapi.com/v3/latest");

            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("apikey", "cur_live_JKdmEQGqjcvs6JKHfJxUzzRwzCOERkY99oEVSfnW");
            IRestResponse response = client.Execute(request);
            return response.Content;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var selectedcu = comboBox1.SelectedItem;
            var selectedde = comboBox2.SelectedItem;
            var amount = double.Parse(textBox1.Text);
            var sourceCurrenctExchangeRate = _currencies.First(c=>c.Key == selectedcu).Value;
            var destinationCurrenctExchangeRate = _currencies.First(c => c.Key == selectedde).Value;
            var calculatedamount = (amount * sourceCurrenctExchangeRate) * destinationCurrenctExchangeRate;
            textBox2.Text = calculatedamount.ToString();
        }
    }
}
