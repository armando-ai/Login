using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Net;
using System.Net.Http;
using System;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Threading;
using System.Windows.Threading;

namespace Login
{
    /// <summary>
    /// Interaction logic for LoggedIn.xaml
    /// </summary>
    public partial class LoggedIn : Window
    {
        public LoggedIn(DataRow dt)
        {
            InitializeComponent();
            Welcome.Content = "Welcome " + dt["username"].ToString();
            timer_Tick1();

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(3);
            timer.Tick += timer_Tick;
            timer.Start();


        }
        void timer_Tick(object sender, EventArgs e)
        {
            //begin plotting stock information fetch off textblock input either class binding or click fetch
            dynamic spy = JObject.Parse(Get("SPY"));
            dynamic nas = JObject.Parse(Get("NDAQ"));
            dynamic dow = JObject.Parse(Get("DIA"));
            List<dynamic> jsonData = new List<dynamic>();
            jsonData.Add(spy);
            jsonData.Add(nas);
            jsonData.Add(dow);

            List<Rectangle> fills = new List<Rectangle>();
            fills.Add(spyBox);
            fills.Add(nasBox);
            fills.Add(dowBox);

            List<Label> info = new List<Label>();
            List<Label> info1 = new List<Label>();
            List<Label> info2 = new List<Label>();
            info.Add(SPY1);
            info1.Add(SPY2);
            info2.Add(SPY3);
            info.Add(NAS1);
            info1.Add(NAS2);
            info2.Add(NAS3);
            info.Add(DOW1);
            info1.Add(DOW2);
            info2.Add(DOW3);
            

            //current stock price 
            for (int i = 0; i < info.Count; i++)
            {
                info[i].Content = "Current: $" + jsonData[i].c;
                info1[i].Content = "% Change: %" + jsonData[i].dp;
                info2[i].Content = "Price Change:$" + jsonData[i].d;
                info[i].Foreground = new SolidColorBrush(Colors.White);
                info1[i].Foreground = new SolidColorBrush(Colors.White);
                info2[i].Foreground = new SolidColorBrush(Colors.White);

            }
            for (int i = 0; i < jsonData.Count; i++)
            {
                if (jsonData[i].d < 0)
                {
                    fills[i].Fill = new SolidColorBrush(System.Windows.Media.Colors.Red);
                }
                else if (jsonData[i].d > 0)
                {
                    fills[i].Fill = new SolidColorBrush(System.Windows.Media.Colors.Green);
                }
                else
                {
                    fills[i].Fill = new SolidColorBrush(System.Windows.Media.Colors.Gray);
                }
            }

        }
        void timer_Tick1()
        {
            //begin plotting stock information fetch off textblock input either class binding or click fetch
            dynamic spy = JObject.Parse(Get("SPY"));
            dynamic nas = JObject.Parse(Get("NDAQ"));
            dynamic dow = JObject.Parse(Get("DIA"));
            List<dynamic> jsonData = new List<dynamic>();
            jsonData.Add(spy);
            jsonData.Add(nas);
            jsonData.Add(dow);

            List<Rectangle> fills = new List<Rectangle>();
            fills.Add(spyBox);
            fills.Add(nasBox);
            fills.Add(dowBox);

            List<Label> info = new List<Label>();
            List<Label> info1 = new List<Label>();
            List<Label> info2 = new List<Label>();
            info.Add(SPY1);
            info1.Add(SPY2);
            info2.Add(SPY3);
            info.Add(NAS1);
            info1.Add(NAS2);
            info2.Add(NAS3);
            info.Add(DOW1);
            info1.Add(DOW2);
            info2.Add(DOW3);


            //current stock price 
            for (int i = 0; i < info.Count; i++)
            {
                info[i].Content = "Current: $" + jsonData[i].c;
                info1[i].Content = "% Change: %" + jsonData[i].dp;
                info2[i].Content = "Price Change:$" + jsonData[i].d;
                info[i].Foreground = new SolidColorBrush(Colors.White);
                info1[i].Foreground = new SolidColorBrush(Colors.White);
                info2[i].Foreground = new SolidColorBrush(Colors.White);

            }
            for (int i = 0; i < jsonData.Count; i++)
            {
                if (jsonData[i].d < 0)
                {
                    fills[i].Fill = new SolidColorBrush(System.Windows.Media.Colors.Red);
                }
                else if (jsonData[i].d > 0)
                {
                    fills[i].Fill = new SolidColorBrush(System.Windows.Media.Colors.Green);
                }
                else
                {
                    fills[i].Fill = new SolidColorBrush(System.Windows.Media.Colors.Gray);
                }
            }

        }

        private void btnDoIt_Click(object sender, RoutedEventArgs e)
        {
            string stock = Ticker1.Text.ToUpper();
            if (stock.Equals(""))
            {
                MessageBox.Show("provide a valid ticker");
            }
            else {

                dynamic ticker = JObject.Parse(Get(stock));

                if (ticker.d == null)
                {
                    MessageBox.Show("provide a valid ticker");
                }
                else {
                   
                    Tick1.Content = "Current: $" + ticker.c;
                    Tick2.Content = "% Change: %" + ticker.dp;
                    TIck3.Content = "Price Change:$" + ticker.d;
                    Tick2.FontSize = 13;
                    if (ticker.d < 0)
                    {
                        Stocks.Fill = new SolidColorBrush(System.Windows.Media.Colors.Red);
                    }
                    else if (ticker.d > 0)
                    {
                        Stocks.Fill = new SolidColorBrush(System.Windows.Media.Colors.Green);
                    }
                    else
                    {
                        Stocks.Fill = new SolidColorBrush(System.Windows.Media.Colors.Gray);
                    }
                }
            }
        }
        public string Get(string ticker)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://finnhub.io/api/v1/quote?symbol=" + ticker + "&token=c8cj07iad3i9nv0co3i0");
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }

        private void btnLogout(object sender, RoutedEventArgs e)
        {
            this.Hide();
            new MainWindow().Show();
        }
    }    
}
