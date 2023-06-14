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
using skeleton_market.EF;
using skeleton_market.Classes;

namespace skeleton_market.Pages
{
    /// <summary>
    /// Логика взаимодействия для WishListPage.xaml
    /// </summary>
    public partial class WishListPage : Page
    {
        List<Wishlist> wishList = new List<Wishlist>();
        List<Merchandise> merchList = new List<Merchandise>();
        public WishListPage()
        {
            InitializeComponent();

            wishList = AppData.Context.Wishlist.Where(x => x.idClient == AppData.current_user.idClient).ToList();

            if(wishList.Count > 0)
            {
                foreach (var item in wishList)
                {
                    var merch = AppData.Context.Merchandise.Where(x => x.idMerchandise == item.idMerchandise).ToList().FirstOrDefault();
                    merchList.Add(merch);
                }

                lvItems.ItemsSource = merchList;
            }
            else
            {
                txtNoResults.Opacity = 1;
                imgNoResults.Opacity = 1;
            }
           
          


        }

        private void btnMoveToProductPage_temp_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button == null)
            {
                return;
            }
            var item = button.DataContext as Merchandise;
            FrameNav.current_frame.Navigate(new Pages.MerchPage(item));
        }

        

        private void btnLike_temp_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button == null)
            {
                return;
            }
            var item = button.DataContext as Merchandise;
            var check_like = AppData.Context.Wishlist.Where(x => x.idClient == AppData.current_user.idClient && x.idMerchandise == item.idMerchandise).FirstOrDefault();
            if (check_like != null)
            {
                AppData.Context.Wishlist.Remove(check_like);
                AppData.Context.SaveChanges();
              
            }

            FrameNav.inner_profile_frame.Navigate(new Pages.WishListPage());

        }
    }
}
