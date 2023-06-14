using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using skeleton_market.Classes;
using skeleton_market.EF;
using skeleton_market.Windows;

namespace skeleton_market.Pages
{
    /// <summary>
    /// Логика взаимодействия для MyBagPage.xaml
    /// </summary>
    public partial class MyBagPage : Page
    {
        List<Merchandise> merchandises = new List<Merchandise>();


      

        public MyBagPage()
        {
            InitializeComponent();
            var order = AppData.user_bag;



            var new_list = from o in order
                           join m in AppData.Context.Merchandise on o.idMerchandise equals m.idMerchandise
                           join s in AppData.Context.SizeGrid on o.idSizeGrid equals s.idSizeGrid
                           select new MyBagItem { Name = m.Name, TotalPrice = m.Price * o.Qty, Photo = m.Photo, Qty = o.Qty,
                               idMerch = o.idMerchandise,
                               idSizeGrid = o.idSizeGrid,
                               PriceEach = m.Price,
                               QtyInStock = AppData.Context.MerchSize.Where(x => x.idMerchandise == o.idMerchandise && x.idSizeGrid == o.idSizeGrid).Select(x=> x.Qty).FirstOrDefault(),
                               SizeGrid = AppData.Context.MerchSize.Where(x => x.idMerchandise == o.idMerchandise).Select(x => x.SizeGrid.Value).ToList().ToString(),
                               SizeSelect = AppData.Context.MerchSize.Where(x => x.idMerchandise == o.idMerchandise && x.idSizeGrid == o.idSizeGrid).Select(x => x.SizeGrid.Value).FirstOrDefault()
                           };
       
            
            lvItems.ItemsSource = new_list;

            decimal total_price = 0;
            foreach(var i in new_list)
            {
               total_price+= i.TotalPrice;
            }
            txtTotalCost.Text = total_price.ToString()+ " ₽";

            if (AppData.current_user != null)
            {
                login.Header = "Выйти";
            };
        }

        public class MyBagItem
        {
            public string Name { get; set; }
            public decimal TotalPrice { get; set; }
            public decimal PriceEach { get; set; }
            public byte[] Photo { get; set; }
            public int Qty { get; set; }
            public int idMerch { get; set; }
            public int QtyInStock {get; set; }
            public string SizeGrid { get; set; }
            public int idSizeGrid { get; set; }
            public string SizeSelect { get; set; }
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button == null)
            {
                return;
            }
            var item = (MyBagItem)button.DataContext;

            var order = AppData.user_bag;

            var new_list = from o in order
                           join m in AppData.Context.Merchandise on o.idMerchandise equals m.idMerchandise
                           join s in AppData.Context.SizeGrid on o.idSizeGrid equals s.idSizeGrid
                           select new MyBagItem
                           {
                               Name = m.Name,
                               TotalPrice = m.Price * o.Qty,
                               PriceEach = m.Price,
                               Photo = m.Photo,
                               Qty = o.Qty,
                               idMerch = o.idMerchandise,
                               idSizeGrid = o.idSizeGrid,
                               QtyInStock = AppData.Context.MerchSize.Where(x => x.idMerchandise == o.idMerchandise && x.idSizeGrid == o.idSizeGrid).Select(x => x.Qty).FirstOrDefault(),
                               SizeGrid = AppData.Context.MerchSize.Where(x => x.idMerchandise == o.idMerchandise).Select(x => x.SizeGrid.Value).ToList().ToString(),
                               SizeSelect = AppData.Context.MerchSize.Where(x => x.idMerchandise == o.idMerchandise && x.idSizeGrid == o.idSizeGrid).Select(x => x.SizeGrid.Value).FirstOrDefault()
                           };


            foreach (var m in new_list.ToList())
            {
                if(item.idMerch == m.idMerch && item.idSizeGrid == m.idSizeGrid)
                {
                    
                        string splitPrice = txtTotalCost.Text.Substring(0, txtTotalCost.Text.IndexOf(" "));
                        decimal cost = Convert.ToDecimal(splitPrice);
                        cost = cost - (m.PriceEach * m.Qty);
                        txtTotalCost.Text = cost.ToString() + " ₽";

                        foreach (var bagItem in AppData.user_bag.ToList())
                        {
                            if (bagItem.idMerchandise == item.idMerch && bagItem.idSizeGrid == item.idSizeGrid)
                            {
                                AppData.user_bag.Remove(bagItem);
                            }
                        }


                        var order2 = AppData.user_bag;

                        var new_list2 = from o in order
                                        join m2 in AppData.Context.Merchandise on o.idMerchandise equals m2.idMerchandise
                                        join s in AppData.Context.SizeGrid on o.idSizeGrid equals s.idSizeGrid
                                        select new MyBagItem
                                        {
                                            Name = m2.Name,
                                            TotalPrice = m2.Price * o.Qty,
                                            PriceEach = m2.Price,
                                            Photo = m2.Photo,
                                            Qty = o.Qty,
                                            idMerch = o.idMerchandise,
                                            idSizeGrid = o.idSizeGrid,
                                            QtyInStock = AppData.Context.MerchSize.Where(x => x.idMerchandise == o.idMerchandise && x.idSizeGrid == o.idSizeGrid).Select(x => x.Qty).FirstOrDefault(),
                                            SizeGrid = AppData.Context.MerchSize.Where(x => x.idMerchandise == o.idMerchandise).Select(x => x.SizeGrid.Value).ToList().ToString(),
                                            SizeSelect = AppData.Context.MerchSize.Where(x => x.idMerchandise == o.idMerchandise && x.idSizeGrid == o.idSizeGrid).Select(x => x.SizeGrid.Value).FirstOrDefault()
                                        };
                    lvItems.ItemsSource = new_list2;
                  
                 
                }
            }


            //cost = cost - (item.Price * Convert.ToDecimal((AppData.user_bag.Where(x => x.idMerchandise == item.idMerchandise).Select(x => x.Qty).ToString())));

           
            
        }

      
        private void btnIncrease_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button == null)
            {
                return;
            }
            var item = (MyBagItem)button.DataContext;
            
            var inStock = AppData.Context.MerchSize.Where(x=> x.idMerchandise == item.idMerch).Select(x => x.Qty).FirstOrDefault();

            if(inStock - item.Qty <= 0)
            {
                MessageWindow mw = new MessageWindow("Резерв превышен");
                mw.Show();
                return;
            }
            else
            {
                foreach(var i in AppData.user_bag.ToList())
                {
                    if(i.idMerchandise == item.idMerch && i.idSizeGrid == item.idSizeGrid)
                    {
                        i.Qty++;
                    }
                }
               

                var order = AppData.user_bag;
                var new_list2 = from o in order
                                join m2 in AppData.Context.Merchandise on o.idMerchandise equals m2.idMerchandise
                                join s in AppData.Context.SizeGrid on o.idSizeGrid equals s.idSizeGrid
                                select new MyBagItem
                                {
                                    Name = m2.Name,
                                    TotalPrice = m2.Price * o.Qty,
                                    PriceEach = m2.Price,
                                    Photo = m2.Photo,
                                    Qty = o.Qty,
                                    idMerch = o.idMerchandise,
                                    idSizeGrid = o.idSizeGrid,
                                    QtyInStock = AppData.Context.MerchSize.Where(x => x.idMerchandise == o.idMerchandise && x.idSizeGrid == o.idSizeGrid).Select(x => x.Qty).FirstOrDefault(),
                                    SizeGrid = AppData.Context.MerchSize.Where(x => x.idMerchandise == o.idMerchandise).Select(x => x.SizeGrid.Value).ToList().ToString(),
                                    SizeSelect = AppData.Context.MerchSize.Where(x => x.idMerchandise == o.idMerchandise && x.idSizeGrid == o.idSizeGrid).Select(x => x.SizeGrid.Value).FirstOrDefault()
                                };
                lvItems.ItemsSource = new_list2;

                string splitPrice = txtTotalCost.Text.Substring(0, txtTotalCost.Text.IndexOf(" "));
                decimal cost = Convert.ToDecimal(splitPrice);
                cost = cost + (item.PriceEach);
                txtTotalCost.Text = cost.ToString() + " ₽";
            }
            
        }

        private void btnDecrease_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button == null)
            {
                return;
            }
            var item = (MyBagItem)button.DataContext;
     
            if (item.Qty > 1)
            {
                foreach (var i in AppData.user_bag.ToList())
                {
                    if (i.idMerchandise == item.idMerch && i.idSizeGrid == item.idSizeGrid)
                    {
                        i.Qty--;
                    }
                }


                var order = AppData.user_bag;
                var new_list2 = from o in order
                                join m2 in AppData.Context.Merchandise on o.idMerchandise equals m2.idMerchandise
                                join s in AppData.Context.SizeGrid on o.idSizeGrid equals s.idSizeGrid
                                select new MyBagItem
                                {
                                    Name = m2.Name,
                                    TotalPrice = m2.Price * o.Qty,
                                    PriceEach = m2.Price,
                                    Photo = m2.Photo,
                                    Qty = o.Qty,
                                    idMerch = o.idMerchandise,
                                    idSizeGrid = o.idSizeGrid,
                                    QtyInStock = AppData.Context.MerchSize.Where(x => x.idMerchandise == o.idMerchandise && x.idSizeGrid == o.idSizeGrid).Select(x => x.Qty).FirstOrDefault(),
                                    SizeGrid = AppData.Context.MerchSize.Where(x => x.idMerchandise == o.idMerchandise).Select(x => x.SizeGrid.Value).ToList().ToString(),
                                    SizeSelect = AppData.Context.MerchSize.Where(x => x.idMerchandise == o.idMerchandise && x.idSizeGrid == o.idSizeGrid).Select(x => x.SizeGrid.Value).FirstOrDefault()
                                };
                lvItems.ItemsSource = new_list2;

                string splitPrice = txtTotalCost.Text.Substring(0, txtTotalCost.Text.IndexOf(" "));
                decimal cost = Convert.ToDecimal(splitPrice);
                cost = cost - (item.PriceEach);
                txtTotalCost.Text = cost.ToString() + " ₽";
            }
            else
            {
                return;
            }
        }




        private void btnLike_temp_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button == null)
            {
                return;
            }
            var item = (MyBagItem)button.DataContext;

            var order = AppData.user_bag;

            var new_list = from o in order
                           join m in AppData.Context.Merchandise on o.idMerchandise equals m.idMerchandise
                           join s in AppData.Context.SizeGrid on o.idSizeGrid equals s.idSizeGrid
                           select new MyBagItem
                           {
                               Name = m.Name,
                               TotalPrice = m.Price * o.Qty,
                               PriceEach = m.Price,
                               Photo = m.Photo,
                               Qty = o.Qty,
                               idMerch = o.idMerchandise,
                               idSizeGrid = o.idSizeGrid,
                               QtyInStock = AppData.Context.MerchSize.Where(x => x.idMerchandise == o.idMerchandise && x.idSizeGrid == o.idSizeGrid).Select(x => x.Qty).FirstOrDefault(),
                               SizeGrid = AppData.Context.MerchSize.Where(x => x.idMerchandise == o.idMerchandise).Select(x => x.SizeGrid.Value).ToList().ToString(),
                               SizeSelect = AppData.Context.MerchSize.Where(x => x.idMerchandise == o.idMerchandise && x.idSizeGrid == o.idSizeGrid).Select(x => x.SizeGrid.Value).FirstOrDefault()
                           };


            foreach (var m in new_list.ToList())
            {
                if (item.idMerch == m.idMerch && item.idSizeGrid == m.idSizeGrid)
                {

                    string splitPrice = txtTotalCost.Text.Substring(0, txtTotalCost.Text.IndexOf(" "));
                    decimal cost = Convert.ToDecimal(splitPrice);
                    cost = cost - (m.PriceEach * m.Qty);
                    txtTotalCost.Text = cost.ToString() + " ₽";

                    foreach (var bagItem in AppData.user_bag.ToList())
                    {
                        if (bagItem.idMerchandise == item.idMerch && bagItem.idSizeGrid == item.idSizeGrid)
                        {
                            AppData.user_bag.Remove(bagItem);
                        }
                    }


                    var order2 = AppData.user_bag;

                    var new_list2 = from o in order
                                    join m2 in AppData.Context.Merchandise on o.idMerchandise equals m2.idMerchandise
                                    join s in AppData.Context.SizeGrid on o.idSizeGrid equals s.idSizeGrid
                                    select new MyBagItem
                                    {
                                        Name = m2.Name,
                                        TotalPrice = m2.Price * o.Qty,
                                        PriceEach = m2.Price,
                                        Photo = m2.Photo,
                                        Qty = o.Qty,
                                        idMerch = o.idMerchandise,
                                        idSizeGrid = o.idSizeGrid,
                                        QtyInStock = AppData.Context.MerchSize.Where(x => x.idMerchandise == o.idMerchandise && x.idSizeGrid == o.idSizeGrid).Select(x => x.Qty).FirstOrDefault(),
                                        SizeGrid = AppData.Context.MerchSize.Where(x => x.idMerchandise == o.idMerchandise).Select(x => x.SizeGrid.Value).ToList().ToString(),
                                        SizeSelect = AppData.Context.MerchSize.Where(x => x.idMerchandise == o.idMerchandise && x.idSizeGrid == o.idSizeGrid).Select(x => x.SizeGrid.Value).FirstOrDefault()
                                    };
                    lvItems.ItemsSource = new_list2;

                    var likeCheck = AppData.Context.Wishlist.Where(x=> x.idClient == AppData.current_user.idClient && x.idMerchandise == item.idMerch).FirstOrDefault();

                    if(likeCheck == null)
                    {
                        AppData.Context.Wishlist.Add(new Wishlist
                        {
                            idClient = AppData.current_user.idClient,
                            idMerchandise = item.idMerch
                        });
                        AppData.Context.SaveChanges();
                    }
                   

                }
            }

        }

        private void btnCheckout_Click(object sender, RoutedEventArgs e)
        {
            if (AppData.current_user == null)
            {
                FrameNav.current_frame.Navigate(new Pages.AuthPage(this));
                return;
            }

            if(AppData.user_bag.Count < 1)
            {
                return;
            }

            var addressCheck = AppData.Context.ClientAddress.Where(x=> x.idClient == AppData.current_user.idClient).FirstOrDefault();
            if(addressCheck == null)
            {
                MessageWindow ws = new MessageWindow("Чтобы воспользоваться сервисом, нужно указать свой адрес в профиле!");
                ws.ShowDialog();
                return;
            }

            DeliveryWindow de = new DeliveryWindow();
            de.ShowDialog();

          

            decimal totalSum = 0;
            foreach(var zxc in AppData.user_bag.ToList())
            {
                totalSum += AppData.Context.Merchandise.Where(x => x.idMerchandise == zxc.idMerchandise).Select(x => x.Price).FirstOrDefault();
            }

            var currentCheck = AppData.Context.Checkout.Add(new Checkout
            {
                idClient = AppData.current_user.idClient,
                Date = DateTime.Now,
              
                idPickUpPoint = AppData.pick_point.idPickUpPoint,
                TotalSum = totalSum 
            });

            AppData.Context.SaveChanges();

            var currentStatus = AppData.Context.OrderStatus.Add(new OrderStatus
            {
                idEmployee = null,
                Date = DateTime.Now,
                idStatus = 2,
                idCheckout = currentCheck.idCheckout
            });


            AppData.Context.SaveChanges();

            foreach (var item in AppData.user_bag.ToList())
            {
                var item_price = AppData.Context.Merchandise.Where(x=> x.idMerchandise ==item.idMerchandise).Select(x=> x.Price).FirstOrDefault();
                AppData.Context.Bag.Add(new Bag
                {            
                    idMerchSize = item.idMerchSize,
                    idCheckout = currentCheck.idCheckout,
                    Qty = item.Qty,
                    TotalSum = item_price * item.Qty
                });
                AppData.Context.SaveChanges();
            }

            FrameNav.current_frame.Navigate(new Pages.OrderCompletePage());

        }

        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            FrameNav.current_frame.Navigate(new Pages.MainPage());
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

    }
}
