using MnbCurrencyReader.Entities;
using MnbCurrencyReader.MnbServiceReference;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml;

namespace MnbCurrencyReader
{
    
    public partial class Form1 : Form
    {
        BindingList<RateData> Rates = new BindingList<RateData>();


        public Form1()
        {
            InitializeComponent();
            RefreshData();

        }

        private void RefreshData()
        {
            Rates.Clear();
            string xmlstring = GetExchangeRates();
            LoadXml(xmlstring);
            dataGridView1.DataSource = Rates;
            Charting();
            RefreshData();
        }



        private void Charting()
        {
            chartRateData.DataSource = Rates;
            Series series = chartRateData.Series[0];
            series.ChartType = SeriesChartType.Line;
            series.XValueMember = "Date";
            series.YValueMembers = "Value";
            series.BorderWidth = 2;
            var chartArea = chartRateData.ChartAreas[0];
            chartArea.AxisX.MajorGrid.Enabled = false;
            chartArea.AxisY.MajorGrid.Enabled = false;
            chartArea.AxisY.IsStartedFromZero = false;
            var legend = chartRateData.Legends[0];
            legend.Enabled = false;

        }

        private void LoadXml(string input)
        {
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(input);
            foreach (XmlElement item in xml.DocumentElement)
            {
                RateData r = new RateData();
                r.Date = DateTime.Parse(item.GetAttribute("date"));
                XmlElement child = (XmlElement)item.FirstChild;
                r.Currency = child.GetAttribute("curr");
                r.Value = decimal.Parse(child.InnerText);
                int unit = int.Parse(child.GetAttribute("unit"));
                if (unit != 0)
                    r.Value = r.Value / unit;
                
                Rates.Add(r);
            }
        }

        string GetExchangeRates()
        {
            MNBArfolyamServiceSoapClient mnBService = new MNBArfolyamServiceSoapClient();
            GetExchangeRatesRequestBody request = new GetExchangeRatesRequestBody();
            request.currencyNames = (string)comboBox1.SelectedItem; //"EUR";
            request.startDate = dateTimePicker1.Value.ToString("yyyy-MM-dd");//"2020 - 01 - 01";
            request.endDate = dateTimePicker2.Value.ToString("yyyy-MM-dd"); //"2020-06-30";
            var response = mnBService.GetExchangeRates(request);
            string result = response.GetExchangeRatesResult;
            return result; //File.WriteAllText("export.xml", result);
        }


        private void filterChanged(object sender, EventArgs e)
        {
            RefreshData();
        }
    }
}
