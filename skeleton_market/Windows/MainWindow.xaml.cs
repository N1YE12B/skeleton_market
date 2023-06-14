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
using System.Windows.Navigation;
using System.Windows.Shapes;


using skeleton_market;
using skeleton_market.Classes;

namespace skeleton_market
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {

        [DllImport("user32.dll", EntryPoint = "ReleaseCapture")]
        public static extern void ReleaseCapture();

        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        public static extern void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        public int count_window_state;
      

        public MainWindow()
        {
            InitializeComponent();
            count_window_state = 0;
            FrameNav.current_frame = mainFrame;
            FrameNav.current_frame.Navigate(new Pages.MainPage());
          
        }

        private void titlebar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            HwndSource hwndSource;
            hwndSource = PresentationSource.FromVisual(this) as HwndSource;
            IntPtr hwnd = hwndSource.Handle;
           
            ReleaseCapture();
            SendMessage(hwnd, 0x112, 0xf012, 0);
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
           Application.Current.Shutdown();
        }

        private void btnMaximize_Click(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow.WindowState == WindowState.Maximized)
            {
                Application.Current.MainWindow.WindowState = WindowState.Normal;
                count_window_state = 0;
                return;
            }

            if (count_window_state == 0)
            { 
                Application.Current.MainWindow.WindowState = WindowState.Maximized;
                count_window_state = 1;
                return;
            }
           
            if(count_window_state == 1) 
            { 
                Application.Current.MainWindow.WindowState = WindowState.Normal;
                count_window_state = 0;
                return;
            }
            
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }

        private void btnMessage_Click(object sender, RoutedEventArgs e)
        {
            Windows.MessageWindow mesww = new Windows.MessageWindow("223");
           
            mesww.ShowDialog();
        }

        private void btntemp_Click(object sender, RoutedEventArgs e)
        {
            Windows.merch_template mtp = new Windows.merch_template();
            mtp.ShowDialog();
        }

        private async void btnAdmin_Click(object sender, RoutedEventArgs e)
        {
            Windows.AdminWindow admin = new Windows.AdminWindow();
            admin.ShowDialog();
        }

        private void btnMerchPage_Click(object sender, RoutedEventArgs e)
        {
            Windows.MerchPageTemplate mvp = new Windows.MerchPageTemplate();
            mvp.ShowDialog();
        }
    }
}
