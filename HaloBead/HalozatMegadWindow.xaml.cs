using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HaloBead
{
    /// <summary>
    /// Interaction logic for HalozatMegadWindow.xaml
    /// </summary>
    public partial class HalozatMegadWindow : Window
    {
        private Halozat alhalozat;

        public Halozat Alhalozat { get => alhalozat; private set => alhalozat = value; }

        private const int GWL_STYLE = -16;
        private const int WS_SYSMENU = 0x80000;
        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        
        void ToolWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Code to remove close box from window
            var hwnd = new System.Windows.Interop.WindowInteropHelper(this).Handle;
            SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);
        }


        public HalozatMegadWindow(int sorszam)
        {
            InitializeComponent();
            Loaded += ToolWindow_Loaded;
            this.Title = String.Format($"{sorszam + 1}. alhálózat hozzááadása");

        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            if(!String.IsNullOrWhiteSpace(alhalNevTxb.Text))
            {
                if(int.TryParse(cimekDBTxb.Text, out int szam) && szam > 0){
                    alhalozat = new Halozat(alhalNevTxb.Text, int.Parse(cimekDBTxb.Text));
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Csak 0-nál nagyobb egész szám adható meg!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Alhálózat nevét kötelező megadni!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true; // this will prevent to close

        }
    }
}
