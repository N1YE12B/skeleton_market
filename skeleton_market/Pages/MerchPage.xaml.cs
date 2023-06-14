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

using skeleton_market.EF;
using skeleton_market.Pages;
using System.IO;
using skeleton_market.Windows;
using System.Data;
using System.Text.RegularExpressions;
using HandyControl.Tools.Extension;

namespace skeleton_market.Pages
{
    /// <summary>
    /// Логика взаимодействия для MerchPage.xaml
    /// </summary>
    public partial class MerchPage : Page
    {
        private List<SizeGrid> size_grid = new List<SizeGrid>();
        public Merchandise current_merch;
        
        public MerchPage(Merchandise merch)
        {
            InitializeComponent();
            current_merch = merch;
            txtTitle.Text = merch.Name;
            txtPrice.Text = merch.Price.ToString()+ " ₽";
            txtDescription.Text = "Обо мне: \n" + merch.Description;
            imgMerch.Source = ByteArrayToImage(merch.Photo);

            var size = AppData.Context.MerchSize.Where(x=> x.idMerchandise == merch.idMerchandise).Select(x=> x.SizeGrid.Value).ToList();
            size.Insert(0, "--Выбрать размер--");
            cmbSize.ItemsSource = size;
            cmbSize.SelectedIndex = 0;

            if(AppData.current_user != null)
            {
                var check_like = AppData.Context.Wishlist.Where(x => x.idClient == AppData.current_user.idClient && x.idMerchandise == current_merch.idMerchandise).FirstOrDefault();
                if (check_like == null)
                {
                    btnLike.Content = "♡";

                }
                else
                {
                    btnLike.Content = "♥";
                }
            }

            if (AppData.current_user != null)
            {
                login.Header = "Выйти";
            };

        }

        public MerchPage()
        {
            InitializeComponent();
            current_merch = AppData.last_merch;
            txtTitle.Text = current_merch.Name;
            txtPrice.Text = current_merch.Price.ToString() + " ₽";
            txtDescription.Text = "Обо мне: \n" + current_merch.Description;
            imgMerch.Source = ByteArrayToImage(current_merch.Photo);

            var size = AppData.Context.MerchSize.Where(x => x.idMerchandise == current_merch.idMerchandise).Select(x => x.SizeGrid.Value).ToList();
            size.Insert(0, "--Выбрать размер--");
            cmbSize.ItemsSource = size;
            cmbSize.SelectedIndex = 0;

            if (AppData.current_user != null)
            {
                var check_like = AppData.Context.Wishlist.Where(x => x.idClient == AppData.current_user.idClient && x.idMerchandise == current_merch.idMerchandise).FirstOrDefault();
                if (check_like == null)
                {
                    btnLike.Content = "♡";

                }
                else
                {
                    btnLike.Content = "♥";
                }
            }

            if (AppData.current_user != null)
            {
                login.Header = "Выйти";
            };
        }
        //метод для конвертации из byte[] в imagesource
        public BitmapSource ByteArrayToImage(byte[] buffer)
        {
            using (var stream = new MemoryStream(buffer))
            {
                return BitmapFrame.Create(stream,
                    BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
            }
        }

        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            FrameNav.current_frame.Navigate(new Pages.MainPage());
            AppData.isLastPageWasAuth = false;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if(AppData.isLastPageWasAuth == true)
            {
                FrameNav.current_frame.Navigate(new Pages.SearchPage());
            }
            else
            {
                FrameNav.current_frame.GoBack();
                AppData.isLastPageWasAuth = false;
            }
            
        }

        private void login_Click(object sender, RoutedEventArgs e)
        {
            if (AppData.current_user != null)
            {
                MessageWindow msg = new MessageWindow("Вы вышли из аккаунта");
                msg.ShowDialog();
                AppData.current_user = null;
                AppData.last_merch = null;
                login.Header = "Войти/Зарегистрироваться";
                AppData.isLastPageWasAuth = false;
                return;
            }
            else
            {
                AppData.last_merch = current_merch;
                FrameNav.current_frame.Navigate(new Pages.AuthPage(this));
                AppData.isLastPageWasAuth = true;
            }
        }

        private void profile_settings_Click(object sender, RoutedEventArgs e)
        {
            if (AppData.current_user == null)
            {
                FrameNav.current_frame.Navigate(new Pages.AuthPage(this));
            }
            else
            {
                FrameNav.current_frame.Navigate(new Pages.ClientProfilePage(1));
            }
            AppData.isLastPageWasAuth = false;
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
            AppData.isLastPageWasAuth = false;
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
            AppData.isLastPageWasAuth = false;
        }

        private void btnAddToBag_Click(object sender, RoutedEventArgs e)
        {
            if(cmbSize.SelectedIndex == 0) 
            {
                
                MessageWindow msg = new MessageWindow("Выберите размер");
                msg.ShowDialog();
                return;
            }

            var typeItem = (string)cmbSize.SelectedItem;           
            string value = typeItem;
            var size_value = AppData.Context.SizeGrid.Where(x => x.Value == value).FirstOrDefault();
            
            var merch_reserve = AppData.Context.MerchSize.Where(x=> x.idMerchandise == current_merch.idMerchandise && x.idSizeGrid == size_value.idSizeGrid).FirstOrDefault();
            
            MerchSize this_merch = new MerchSize();            
            this_merch.idMerchandise = current_merch.idMerchandise;
            this_merch.idSizeGrid = size_value.idSizeGrid;
            this_merch.Qty = 1;
            this_merch.idMerchSize = AppData.Context.MerchSize.Where(x => x.idMerchandise == this_merch.idMerchandise && x.idSizeGrid == this_merch.idSizeGrid).Select(x=> x.idMerchSize).FirstOrDefault();


            var merch_qty = AppData.user_bag;
            
            if (AppData.user_bag.Where(x=> x.idMerchandise == this_merch.idMerchandise && x.idSizeGrid == size_value.idSizeGrid).Select(x=> x.Qty).FirstOrDefault() == merch_reserve.Qty) 
            {
                MessageWindow msg = new MessageWindow("Резерв превышен");
                msg.ShowDialog();
                return;
            }
            else
            {
                foreach(var i in AppData.user_bag)
                {
                    if(i.idMerchandise == this_merch.idMerchandise && i.idSizeGrid == size_value.idSizeGrid)
                    {
                        i.Qty += 1;
                    }
                }
                var check_on_exist = AppData.user_bag.Where(x => x.idMerchandise == this_merch.idMerchandise && x.idSizeGrid == size_value.idSizeGrid).FirstOrDefault();
                if(check_on_exist == null)
                {
                    AppData.user_bag.Add(this_merch);
                }
               
              
            }
            AppData.isLastPageWasAuth = false;

        }

        private void btnLike_Click(object sender, RoutedEventArgs e)
        {
            var check_like = AppData.Context.Wishlist.Where(x=> x.idClient == AppData.current_user.idClient && x.idMerchandise == current_merch.idMerchandise).FirstOrDefault();
            if(check_like != null)
            {
                AppData.Context.Wishlist.Remove(check_like);
                AppData.Context.SaveChanges();
                btnLike.Content = "♡";
            }
            else
            {
                AppData.Context.Wishlist.Add(new Wishlist
                {
                    idClient = AppData.current_user.idClient,
                    idMerchandise = current_merch.idMerchandise
                });
                AppData.Context.SaveChanges();
                btnLike.Content = "♥";
            }
            AppData.isLastPageWasAuth = false;

        }

        private void btnLike_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (AppData.current_user == null)
            {
                FrameNav.current_frame.Navigate(new Pages.AuthPage(this));
                return;
            }
        }

        private void btnGoToBag_Click(object sender, RoutedEventArgs e)
        {
            FrameNav.current_frame.Navigate(new Pages.MyBagPage());
        }

        private void btnAddToBag_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (cmbSize.SelectedIndex == 0)
                {

                    MessageWindow msg = new MessageWindow("Выберите размер");
                    msg.ShowDialog();
                    return;
                }

                var typeItem = (string)cmbSize.SelectedItem;
                string value = typeItem;
                var size_value = AppData.Context.SizeGrid.Where(x => x.Value == value).FirstOrDefault();

                var merch_reserve = AppData.Context.MerchSize.Where(x => x.idMerchandise == current_merch.idMerchandise && x.idSizeGrid == size_value.idSizeGrid).FirstOrDefault();

                MerchSize this_merch = new MerchSize();
                this_merch.idMerchandise = current_merch.idMerchandise;
                this_merch.idSizeGrid = size_value.idSizeGrid;
                this_merch.Qty = 1;


                var merch_qty = AppData.user_bag;

                if (AppData.user_bag.Where(x => x.idMerchandise == this_merch.idMerchandise && x.idSizeGrid == size_value.idSizeGrid).Select(x => x.Qty).FirstOrDefault() == merch_reserve.Qty)
                {
                    MessageWindow msg = new MessageWindow("Резерв превышен");
                    msg.ShowDialog();
                    return;
                }
            }
          catch
            {

            }
        }
    }
}
