using skeleton_market.Classes;
using skeleton_market.EF;
using skeleton_market.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        
        public MainPage()
        {
            InitializeComponent();

            if(AppData.current_user != null) 
            {
                login.Header = "Выйти";
            };

            
        }
       

     

        private void login_Click(object sender, RoutedEventArgs e)
        {
            
            if(AppData.current_user != null)
            {
                MessageWindow msg = new MessageWindow("Вы вышли из аккаунта");
                msg.ShowDialog();
                AppData.current_user = null;
                login.Header = "Войти/Зарегистрироваться";
                return;
            }
            else
            {
               FrameNav.current_frame.Navigate(new Pages.AuthPage(this));
            }
           
        }

        private void tbSearch_GotFocus(object sender, RoutedEventArgs e)
        {
            tbSearch.Text = string.Empty;
            tbSearch.FontStyle = FontStyles.Normal;
        }

        private void tbSearch_LostFocus(object sender, RoutedEventArgs e)
        {
            tbSearch.Text = "Поиск вещей";
            tbSearch.FontStyle = FontStyles.Oblique;
        }

        private void btnViewAll_Click(object sender, RoutedEventArgs e)
        {
            FrameNav.current_frame.Navigate(new Pages.SearchPage(false));
        }

        private void profile_settings_Click(object sender, RoutedEventArgs e)
        {
            if(AppData.current_user == null)
            {
                FrameNav.current_frame.Navigate(new Pages.AuthPage(this));
            }
            else
            {
                FrameNav.current_frame.Navigate(new Pages.ClientProfilePage(1));
            }
           
        }

        private void my_orders_Click(object sender, RoutedEventArgs e)
        {
            if (AppData.current_user == null)
            {
                FrameNav.current_frame.Navigate(new Pages.AuthPage(this));
            }
            else
            {
                FrameNav.current_frame.Navigate(new Pages.ClientProfilePage(3));
            }
        }

        private void wishlist_Click(object sender, RoutedEventArgs e)
        {
            if (AppData.current_user == null)
            {
                FrameNav.current_frame.Navigate(new Pages.AuthPage(this));
            }
            else
            {
                FrameNav.current_frame.Navigate(new Pages.ClientProfilePage(4));
            }
        }

        private void btnAllCategories_Click(object sender, RoutedEventArgs e)
        {
            FrameNav.current_frame.Navigate(new Pages.SearchPage(true));
        }

        private void btnHoodie_Click(object sender, RoutedEventArgs e)
        {
            FrameNav.current_frame.Navigate(new Pages.SearchPage(false, 1));
        }

        private void btnHats_Click(object sender, RoutedEventArgs e)
        {
            FrameNav.current_frame.Navigate(new Pages.SearchPage(false, 2));
        }

        private void btnShoes_Click(object sender, RoutedEventArgs e)
        {
            FrameNav.current_frame.Navigate(new Pages.SearchPage(false, 3));
        }

        private void btnGoToBag_Click(object sender, RoutedEventArgs e)
        {
            FrameNav.current_frame.Navigate(new Pages.MyBagPage());
        }






        private void tbSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var res = AppData.Context.Merchandise.Where(x => x.Name.Contains(tbSearch.Text)).OrderBy(x => x.Price).ToList();
                FrameNav.current_frame.Navigate(new Pages.SearchPage(res));
            }
        }

        private void tbSearch_PreviewKeyDown(object sender, KeyEventArgs e)
        {
         
            
        }

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tbSearch.Text == "" || tbSearch.Text == "Поиск вещей")
            {
                tbSearch.IsDropDownOpen = false;
                return;
            }
            else
            {
                tbSearch.IsDropDownOpen = true;
            }


            tbSearch.ItemsSource = AppData.Context.Merchandise.Where(x => x.Name.Contains(tbSearch.Text)).ToList();
            tbSearch.DisplayMemberPath = "Name";


            // убрать selection, если dropdown только открылся
            var tb = (TextBox)e.OriginalSource;
            tb.Select(tb.SelectionStart + tb.SelectionLength, 0);
            CollectionView cv = (CollectionView)CollectionViewSource.GetDefaultView(tbSearch.ItemsSource);
            cv.Filter = s =>
            {
                var res = (Merchandise)s;
                return res.Name.IndexOf(tbSearch.Text, StringComparison.CurrentCultureIgnoreCase) >= 0;
            };
        }

      

        private void tbSearch_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                //берем текст из выюранного элемента
                var typeItem = (Merchandise)tbSearch.SelectedItem;
                if (typeItem != null)
                {
                    string value = typeItem.Name;
                    var res = AppData.Context.Merchandise.Where(x => x.Name == value).ToList();
                    FrameNav.current_frame.Navigate(new Pages.SearchPage(res));
                }

            }
            catch
            {
                return;
            }
        }
    }
}
