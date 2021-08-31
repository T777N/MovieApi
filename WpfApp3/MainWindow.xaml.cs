using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string ImagePath { get; set; }
        public string Minute { get; set; }
        public string Description { get; set; }

        public dynamic Data { get; set; }
        public dynamic SingleData { get; set; }

        public int  i { get; set; }=0;
        public int  i2 { get; set; }=0;

        HttpClient httpClient = new HttpClient(); 

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            i2 = 1;
            Funk();
            


        }

        public void Funk() {
            var name = movieTextbox.Text;
            HttpResponseMessage response = new HttpResponseMessage();
            response = httpClient.GetAsync($@"http://www.omdbapi.com/?apikey=92ba3e9a&s={name}&plot=full").Result;

            var str = response.Content.ReadAsStringAsync().Result;
            Data = JsonConvert.DeserializeObject(str);

            response = httpClient.GetAsync($@"http://www.omdbapi.com/?apikey=92ba3e9a&t={Data.Search[i].Title}&plot=full").Result;

            str = response.Content.ReadAsStringAsync().Result;
            SingleData = JsonConvert.DeserializeObject(str);

            movieImage.Source = SingleData.Poster;
            movieImageB.Source = SingleData.Poster;
            Minute = SingleData.Runtime;
            Description = SingleData.Genre;

            movieLabel.Content = Minute + " " + Description;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (i2 == 1)
            {

            ++i;
            Funk();
            }

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (i>0&&i2==1)
            {

            --i;
            Funk();
            }
            
        }
    }
}
