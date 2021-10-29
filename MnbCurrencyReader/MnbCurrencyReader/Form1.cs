﻿using MnbCurrencyReader.MnbServiceReference;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MnbCurrencyReader
{
    
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            GetExchangeRates();

        }

        private void GetExchangeRates()
        {
            MNBArfolyamServiceSoapClient mnBService = new MNBArfolyamServiceSoapClient();
            GetExchangeRatesRequestBody request = new GetExchangeRatesRequestBody();
            request.currencyNames = "EUR";
            request.startDate = "2020 - 01 - 01";
            request.endDate = "2020-06-30";
            var response = mnBService.GetExchangeRates(request);
            string result = response.GetExchangeRatesResult;
        }
    }
}
