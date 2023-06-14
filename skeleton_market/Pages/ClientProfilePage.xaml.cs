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
using skeleton_market.Pages;

namespace skeleton_market.Pages
{
    /// <summary>
    /// Логика взаимодействия для ClientProfilePage.xaml
    /// </summary>
    public partial class ClientProfilePage : Page
    {
        public ClientProfilePage(int pagenum)
        {
            InitializeComponent();
            FrameNav.inner_profile_frame = AdditionalFrame;
            if(pagenum == 1)
            {
                FrameNav.inner_profile_frame.Navigate(new Pages.AccountInfoPage());
            }
            if (pagenum == 3)
            {
                FrameNav.inner_profile_frame.Navigate(new Pages.MyOrdersPage());
            }
            if (pagenum == 4)
            {
                FrameNav.inner_profile_frame.Navigate(new Pages.WishListPage());
            }
        }

        private void btnMyProfile_Click(object sender, RoutedEventArgs e)
        {
            FrameNav.inner_profile_frame.Navigate(new Pages.AccountInfoPage());
        }

        private void btnMyBag_Click(object sender, RoutedEventArgs e)
        {
            FrameNav.current_frame.Navigate(new Pages.MyBagPage());
        }

        private void btnMyOrders_Click(object sender, RoutedEventArgs e)
        {
            FrameNav.inner_profile_frame.Navigate(new Pages.MyOrdersPage());
        }

        private void btnMyWishList_Click(object sender, RoutedEventArgs e)
        {
            FrameNav.inner_profile_frame.Navigate(new Pages.WishListPage());
        }

        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            FrameNav.current_frame.Navigate(new Pages.MainPage());
        }
    }
}
