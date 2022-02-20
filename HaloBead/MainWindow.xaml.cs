using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HaloBead
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static List<Halozat> alhalozatok = new List<Halozat>();
        public string HalozatCim { get;private set; }

        public MainWindow()
        {
            InitializeComponent();
            prefixCmb.ItemsSource = PrefixFeltolt();
        }

        private List<int> PrefixFeltolt()
        {
            List<int> prefixek = new List<int>();
            for (int i = 32; i > 0; i--)
            {
                prefixek.Add(i);
            }
            return prefixek;
        }

        private void ResultCalc()
        {
            var ok = alhalozatok.Where(x => x.HaloMeret == 0).ToList();
            alhalozatok = alhalozatok.OrderByDescending(x => x.HaloMeret).ToList();

            for (int i = 0; i < alhalozatok.Count; i++)
            {
                alhalozatok[i].Calc(i == 0 ? HalozatCim : alhalozatok[i - 1].SzorasCim, i == 0 ? true : false);
            }
             dgAdatok.ItemsSource = alhalozatok;
           
        }

        private void calBtn_Click(object sender, RoutedEventArgs e)
        {
            string ip = ipTxb.Text;
            if(Converter.CheckIfIP(ip))
            {
                if (prefixCmb.SelectedItem != null)
                {
                    if (!String.IsNullOrWhiteSpace(szamTxb.Text) && int.TryParse(szamTxb.Text, out int haloDB))
                    {
                        var maszk = Converter.PrefixToMask(int.Parse(prefixCmb.SelectedItem.ToString()));
                        var korrigaltIP = Converter.BinaryToDec(Converter.CheckIfNetwork(ip, maszk));
                        HalozatCim = korrigaltIP;
                        kiindulLabel.Content = HalozatCim;
                        int db = 0;
                        while (db != haloDB)
                        {
                            var Ablak = new HalozatMegadWindow(db);
                            Ablak.ShowDialog();
                            alhalozatok.Add(Ablak.Alhalozat);                   
                            db++;
                        }
                        ResultCalc();
                    }
                    else
                    {
                        MessageBox.Show("Alhálózat darabszámnak csak pozitív egész szám adható meg!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Prefix megadása kötelező!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Az IP cím formátuma nem megfelelő!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

       
    }
}
