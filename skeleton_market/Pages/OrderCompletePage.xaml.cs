using skeleton_market.Classes;
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

namespace skeleton_market.Pages
{
    /// <summary>
    /// Логика взаимодействия для OrderCompletePage.xaml
    /// </summary>
    public partial class OrderCompletePage : Page
    {
        public OrderCompletePage()
        {
            InitializeComponent();
            AppData.user_bag = null;
        }

        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            FrameNav.current_frame.Navigate(new Pages.MainPage());
        }
    }
}
