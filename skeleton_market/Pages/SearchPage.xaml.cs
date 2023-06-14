using skeleton_market.Classes;
using skeleton_market.EF;
using skeleton_market.Windows;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.ConstrainedExecution;
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
    /// Логика взаимодействия для SearchPage.xaml
    /// </summary>
    public partial class SearchPage : Page
    {
        private List<Category> merch_categories = new List<Category>();
        private List<SizeGrid> merch_size = new List<SizeGrid>();
        private List<Brand> merch_brands = new List<Brand>();

        private List<Merchandise> merchList = new List<Merchandise>();

        public int merchSort = 0;
        public int merchCategory = 0;
        public int merchSize = 0;
        public int merchBrand = 0;
        public double priceStart = 0;
        public double priceEnd = 0;
        public int merchGender = 0;

        public SearchPage(bool cat_expand, int cat_id = 0)
        {
            InitializeComponent();
            merchGender = 0;
            merchCategory = 0;
            merchList = AppData.Context.Merchandise.OrderByDescending(x => x.idMerchandise).ToList();

            cmbSort.SelectedIndex = 0;

            sliderPrice.Maximum = Convert.ToDouble(AppData.Context.Merchandise.Max(x => x.Price));
            sliderPrice.Minimum = Convert.ToDouble(AppData.Context.Merchandise.Min(x => x.Price));
            sliderPrice.ValueEnd = Convert.ToDouble(AppData.Context.Merchandise.Max(x => x.Price));
            sliderPrice.ValueStart = Convert.ToDouble(0);

            if (cat_expand == false)
            {
                merch_categories = AppData.Context.Category.ToList();
                merch_categories.Insert(0, new Category { Name = "--Не выбрано--" });
                cmbCategory.ItemsSource = merch_categories;
                cmbCategory.DisplayMemberPath = "Name";

                if (cat_id != 0)
                {
                    cmbCategory.SelectedIndex = cat_id;
                    lvItems.ItemsSource = AppData.Context.Merchandise.Where(x => x.idCategory == cat_id).OrderByDescending(x => x.idMerchandise).ToList();
                }
                else
                {
                    lvItems.ItemsSource = AppData.Context.Merchandise.OrderByDescending(x => x.idMerchandise).ToList();
                }

                cmbPrice.Items.Insert(0, "Диапазон");



                merch_size = AppData.Context.SizeGrid.ToList();
                merch_size.Insert(0, new SizeGrid { Value = "--Не выбрано--" });
                cmbSize.ItemsSource = merch_size;
                cmbSize.DisplayMemberPath = "Value";

                merch_brands = AppData.Context.Brand.OrderBy(x => x.Name).ToList();
                merch_brands.Insert(0, new Brand { Name = "--Не выбрано--" });
                cmbBrand.ItemsSource = merch_brands;
                cmbBrand.DisplayMemberPath = "Name";




                //tbSearch.ItemsSource = AppData.Context.Merchandise.ToList();
                //tbSearch.DisplayMemberPath = "Name";
            }
            else
            {
                cmbPrice.Items.Insert(0, "Диапазон");
                lvItems.ItemsSource = AppData.Context.Merchandise.ToList();

                merch_categories = AppData.Context.Category.OrderBy(x => x.Name).ToList();
                merch_categories.Insert(0, new Category { Name = "--Не выбрано--" });
                cmbCategory.ItemsSource = merch_categories;
                cmbCategory.DisplayMemberPath = "Name";

                merch_size = AppData.Context.SizeGrid.ToList();
                merch_size.Insert(0, new SizeGrid { Value = "--Не выбрано--" });
                cmbSize.ItemsSource = merch_size;
                cmbSize.DisplayMemberPath = "Value";

                merch_brands = AppData.Context.Brand.OrderBy(x => x.Name).ToList();
                merch_brands.Insert(0, new Brand { Name = "--Не выбрано--" });
                cmbBrand.ItemsSource = merch_brands;
                cmbBrand.DisplayMemberPath = "Name";


                cmbCategory.IsDropDownOpen = true;

                //tbSearch.ItemsSource = AppData.Context.Merchandise.ToList();
                //tbSearch.DisplayMemberPath = "Name";
            }
            if (AppData.current_user != null)
            {
                login.Header = "Выйти";
            };

        }

        public SearchPage(List<Merchandise> queryMerch)
        {
            InitializeComponent();
            merchGender = 0;
            merchCategory = 0;
            merchList = AppData.Context.Merchandise.OrderByDescending(x => x.idMerchandise).ToList();

            sliderPrice.Maximum = Convert.ToDouble(AppData.Context.Merchandise.Max(x => x.Price));
            sliderPrice.Minimum = Convert.ToDouble(AppData.Context.Merchandise.Min(x => x.Price));
            sliderPrice.ValueEnd = Convert.ToDouble(AppData.Context.Merchandise.Max(x => x.Price));
            sliderPrice.ValueStart = Convert.ToDouble(0);

            cmbSort.SelectedIndex = 0;

            merch_categories = AppData.Context.Category.ToList();
            merch_categories.Insert(0, new Category { Name = "--Не выбрано--" });
            cmbCategory.ItemsSource = merch_categories;
            cmbCategory.DisplayMemberPath = "Name";


            if (queryMerch == null)
            {
                txtNoResults.Opacity = 1;
                imgNoResults.Opacity = 1;

            }
            else
            {
                txtNoResults.Opacity = 0;
                imgNoResults.Opacity = 0;
                lvItems.ItemsSource = queryMerch;
            }
           


            cmbPrice.Items.Insert(0, "Диапазон");



            merch_size = AppData.Context.SizeGrid.ToList();
            merch_size.Insert(0, new SizeGrid { Value = "--Не выбрано--" });
            cmbSize.ItemsSource = merch_size;
            cmbSize.DisplayMemberPath = "Value";

            merch_brands = AppData.Context.Brand.OrderBy(x => x.Name).ToList();
            merch_brands.Insert(0, new Brand { Name = "--Не выбрано--" });
            cmbBrand.ItemsSource = merch_brands;
            cmbBrand.DisplayMemberPath = "Name";


            if (AppData.current_user != null)
            {
                login.Header = "Выйти";
            };
        }


        public SearchPage()
        {
            InitializeComponent();
            merchGender = 0;
            merchCategory = 0;
            merchList = AppData.Context.Merchandise.OrderByDescending(x => x.idMerchandise).ToList();

            sliderPrice.Maximum = Convert.ToDouble(AppData.Context.Merchandise.Max(x => x.Price));
            sliderPrice.Minimum = Convert.ToDouble(AppData.Context.Merchandise.Min(x => x.Price));
            sliderPrice.ValueEnd = Convert.ToDouble(AppData.Context.Merchandise.Max(x => x.Price));
            sliderPrice.ValueStart = Convert.ToDouble(0);

            cmbSort.SelectedIndex = 0;

            merch_categories = AppData.Context.Category.ToList();
            merch_categories.Insert(0, new Category { Name = "--Не выбрано--" });
            cmbCategory.ItemsSource = merch_categories;
            cmbCategory.DisplayMemberPath = "Name";

                
            lvItems.ItemsSource = AppData.Context.Merchandise.OrderByDescending(x => x.idMerchandise).ToList();


            cmbPrice.Items.Insert(0, "Диапазон");



            merch_size = AppData.Context.SizeGrid.ToList();
            merch_size.Insert(0, new SizeGrid { Value = "--Не выбрано--" });
            cmbSize.ItemsSource = merch_size;
            cmbSize.DisplayMemberPath = "Value";

            merch_brands = AppData.Context.Brand.OrderBy(x => x.Name).ToList();
            merch_brands.Insert(0, new Brand { Name = "--Не выбрано--" });
            cmbBrand.ItemsSource = merch_brands;
            cmbBrand.DisplayMemberPath = "Name";

            
            if (AppData.current_user != null)
            {
                login.Header = "Выйти";
            };
        }

        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            FrameNav.current_frame.Navigate(new Pages.MainPage());
        }

        //private void btnPriceApply_Click(object sender, RoutedEventArgs e)
        //{
        //    decimal low_price = Convert.ToDecimal(sliderPrice.ValueStart);
        //    decimal high_price = Convert.ToDecimal(sliderPrice.ValueEnd);

        //    var res = AppData.Context.Merchandise.Where(x => x.Price >= low_price && x.Price <= high_price).OrderBy(x => x.Price).ToList();
        //    if (res.Count == 0)
        //    {
        //        txtNoResults.Opacity = 1;
        //        imgNoResults.Opacity = 1;

        //    }
        //    lvItems.ItemsSource = res;

        //}

        private void cmbPrice_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cmbPrice.Text = "Цена";

        }

        private void tbSearch_LostFocus(object sender, RoutedEventArgs e)
        {

            if (tbSearch.Text == "Поиск вещей" || string.IsNullOrWhiteSpace(tbSearch.Text))
            {
                tbSearch.FontStyle = FontStyles.Oblique;
                tbSearch.Text = "Поиск вещей";
                lvItems.ItemsSource = AppData.Context.Merchandise.ToList();
            }

        }



        private void tbSearch_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tbSearch.Text == "" || tbSearch.Text == "Поиск вещей")
            {
                tbSearch.IsDropDownOpen = false;
               
            }

            tbSearch.Text = null;

            tbSearch.FontStyle = FontStyles.Normal;

        }









        private void cmbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txtNoResults.Opacity = 0;
            imgNoResults.Opacity = 0;
            if (cmbSort.SelectedIndex == 0)
            {
                cmbSort.Text = "z";
                txtSort.Opacity = 1;
               
                merchSort = cmbSort.SelectedIndex;
                //if (merchGender != 0)
                //{
                //    if (cmbSort.SelectedIndex == 1)
                //    {
                //        lvItems.ItemsSource = merchList = merchList.OrderByDescending(x => x.ReleaseDate).Where(x => x.idGender == merchGender).ToList();
                //    }
                //    if (cmbSort.SelectedIndex == 2)
                //    {
                //        lvItems.ItemsSource = merchList = merchList.OrderByDescending(x => x.Price).Where(x => x.idGender == merchGender).ToList();
                //    }
                //    if (cmbSort.SelectedIndex == 3)
                //    {
                //        lvItems.ItemsSource = merchList = merchList.OrderBy(x => x.Price).Where(x => x.idGender == merchGender).ToList();
                //    }
                //}
                //else
                //{
                //    if (cmbSort.SelectedIndex == 1)
                //    {
                //        lvItems.ItemsSource = merchList = merchList.OrderByDescending(x => x.ReleaseDate).ToList();
                //    }
                //    if (cmbSort.SelectedIndex == 2)
                //    {
                //        lvItems.ItemsSource = merchList = merchList.OrderByDescending(x => x.Price).ToList();
                //    }
                //    if (cmbSort.SelectedIndex == 3)
                //    {
                //        lvItems.ItemsSource = merchList = merchList.OrderBy(x => x.Price).ToList();
                //    }
                //}

            }
            else
            { 
                merchSort = cmbSort.SelectedIndex;
               
                txtSort.Opacity = 0;
               // lvItems.ItemsSource = merchList.OrderBy(x => x.idMerchandise).ToList();
            }

        }

       
        private void cmbCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txtNoResults.Opacity = 0;
            imgNoResults.Opacity = 0;
            if (cmbCategory.SelectedIndex != 0)
            {
                txtCat.Opacity = 0;
                merchCategory= cmbCategory.SelectedIndex;
                ////берем текст из выюранного элемента
                //var typeItem = (Category)cmbCategory.SelectedItem;
                //if (typeItem != null)
                //{
                //    string value = typeItem.Name;
                //    lvItems.ItemsSource = merchList = merchList.Where(x => x.Category.Name == value).ToList();
                //    merchCategory = cmbCategory.SelectedIndex;
                //}


            }
            else
            {
                cmbCategory.Text = "z";
                txtCat.Opacity = 1;
                //lvItems.ItemsSource = merchList.ToList();
                merchCategory = 0;
            }




        }

        private void cmbSize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txtNoResults.Opacity = 0;
            imgNoResults.Opacity = 0;
            if (cmbSize.SelectedIndex != 0)
            {
                txtSize.Opacity = 0;
                merchSize = cmbSize.SelectedIndex;
                //if (merchCategory != 0)
                //{
                //    if (merchGender != 0)
                //    {
                //        txtSize.Opacity = 0;
                //        //берем текст из выюранного элемента
                //        var typeItem = (SizeGrid)cmbSize.SelectedItem;
                //        if (typeItem != null)
                //        {
                //            string value = typeItem.Value;
                //            var size_value = AppData.Context.SizeGrid.Where(x => x.Value == value).FirstOrDefault();


                //            var merchsize = AppData.Context.MerchSize.Where(x => x.idSizeGrid == size_value.idSizeGrid).ToList();

                //            List<Merchandise> merch = new List<Merchandise>();
                //            foreach (var m in merchsize)
                //            {
                //                merch.Add(m.Merchandise);
                //            }

                //            lvItems.ItemsSource = merch.Where(x => x.idCategory == merchCategory && x.idGender == merchGender).ToList();


                //        }
                //    }
                //    else
                //    {
                //        txtSize.Opacity = 0;
                //        //берем текст из выюранного элемента
                //        var typeItem2 = (SizeGrid)cmbSize.SelectedItem;
                //        if (typeItem2 != null)
                //        {
                //            string value = typeItem2.Value;
                //            var size_value = AppData.Context.SizeGrid.Where(x => x.Value == value).FirstOrDefault();


                //            var merchsize = AppData.Context.MerchSize.Where(x => x.idSizeGrid == size_value.idSizeGrid).ToList();

                //            List<Merchandise> merch = new List<Merchandise>();
                //            foreach (var m in merchsize)
                //            {
                //                merch.Add(m.Merchandise);
                //            }

                //            lvItems.ItemsSource = merchList = merch.Where(x => x.idCategory == merchCategory).ToList();


                //        }

                //    }
                //}
                //else
                //{

                //    if (merchGender != 0)
                //    {
                //        txtSize.Opacity = 0;
                //        //берем текст из выюранного элемента
                //        var typeItem2 = (SizeGrid)cmbSize.SelectedItem;
                //        if (typeItem2 != null)
                //        {
                //            string value = typeItem2.Value;
                //            var size_value = AppData.Context.SizeGrid.Where(x => x.Value == value).FirstOrDefault();
                //            var merchsize = AppData.Context.MerchSize.Where(x => x.idSizeGrid == size_value.idSizeGrid).ToList();

                //            List<Merchandise> merch = new List<Merchandise>();
                //            foreach (var m in merchsize)
                //            {
                //                merch.Add(m.Merchandise);
                //            }

                //            lvItems.ItemsSource = merch.Where(x => x.idGender == merchGender).ToList();

                //            merchList = merch;
                //        }
                //    }
                //    else
                //    {
                //        txtSize.Opacity = 0;
                //        //берем текст из выюранного элемента
                //        var typeItem2 = (SizeGrid)cmbSize.SelectedItem;
                //        if (typeItem2 != null)
                //        {
                //            string value = typeItem2.Value;
                //            var size_value = AppData.Context.SizeGrid.Where(x => x.Value == value).FirstOrDefault();
                //            var merchsize = AppData.Context.MerchSize.Where(x => x.idSizeGrid == size_value.idSizeGrid).ToList();

                //            List<Merchandise> merch = new List<Merchandise>();
                //            foreach (var m in merchsize)
                //            {
                //                merch.Add(m.Merchandise);
                //            }

                //            lvItems.ItemsSource = merchList = merch;


                //        }
                //    }

                //}
            }
            else
            {
                cmbSize.Text = "z";
                txtSize.Opacity = 1;
                merchSize = 0;
                //lvItems.ItemsSource = merchList.ToList();
            }


        }
        
        private void cmbBrand_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txtNoResults.Opacity = 0;
            imgNoResults.Opacity = 0;
            if (cmbBrand.SelectedIndex != 0)
            {
                txtBrand.Opacity = 0;
                merchBrand = cmbBrand.SelectedIndex;
                ////берем текст из выюранного элемента
                //var typeItem = (Brand)cmbBrand.SelectedItem;
                //if (typeItem != null)
                //{
                //    string value = typeItem.Name;
                //    lvItems.ItemsSource = merchList.Where(x => x.Brand.Name == value).ToList();
                //}
            }
            else
            {
                cmbBrand.Text = "z";
                txtBrand.Opacity = 1;
                merchBrand = 0;
                //lvItems.ItemsSource = merchList.ToList();
            }


        }
        #region Search

        
        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(tbSearch.Text == "" || tbSearch.Text == "Поиск вещей")
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
            //try
            //{
            //    //берем текст из выюранного элемента
            //    var typeItem = (Merchandise)tbSearch.SelectedItem;
            //    if (typeItem != null)
            //    {
            //        string value = typeItem.Name;
            //        lvItems.ItemsSource = AppData.Context.Merchandise.Where(x => x.Name == value).ToList();
            //    }

            //}
            //catch
            //{
            //    return;
            //}
        }
        #endregion
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
           
            merchGender = 0;
            merchCategory = 0;
            merchBrand= 0;
            merchSize= 0;

            lvItems.ItemsSource = AppData.Context.Merchandise.OrderByDescending(x => x.idMerchandise).ToList();
            tbSearch.Text = "Поиск вещей";
            tbSearch.FontStyle = FontStyles.Oblique;
            cmbSort.SelectedIndex = 0;
            cmbPrice.SelectedIndex = 0;
            cmbBrand.SelectedIndex = 0;
            cmbCategory.SelectedIndex = 0;
            cmbSize.SelectedIndex = 0;
            cmbGender.SelectedIndex = 0;
        }

        #region menu

      

        private void login_Click(object sender, RoutedEventArgs e)
        {
            if (AppData.current_user != null)
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
        #endregion
        private void cmbGender_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txtNoResults.Opacity = 0;
            imgNoResults.Opacity = 0;
            merchGender = cmbGender.SelectedIndex;
            if (cmbGender.SelectedIndex != 0)
            {
                txtGender.Opacity = 0;
                merchGender = cmbGender.SelectedIndex;
                //if (merchCategory != 0)
                //{
                //    lvItems.ItemsSource = merchList.Where(x => x.idGender == cmbGender.SelectedIndex && x.idCategory == merchCategory).ToList();

                //}
                //else
                //{
                //    lvItems.ItemsSource = merchList.Where(x => x.idGender == cmbGender.SelectedIndex).ToList();

                //}

            }
            else
            {
                cmbGender.Text = "z";
                txtGender.Opacity = 1;
                //lvItems.ItemsSource = merchList.ToList();
                merchGender = 0;
            }



        }









        private void tbSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var res = AppData.Context.Merchandise.Where(x => x.Name.Contains(tbSearch.Text)).OrderBy(x => x.Price).ToList();
                if (res.Count == 0)
                {
                    txtNoResults.Opacity = 1;
                    imgNoResults.Opacity = 1;

                }
                else
                {
                    txtNoResults.Opacity = 0;
                    imgNoResults.Opacity = 0;
                }
                lvItems.ItemsSource = res;
            }
          

        }

        private void tbSearch_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var res = AppData.Context.Merchandise.Where(x => x.Name.Contains(tbSearch.Text)).OrderBy(x => x.Price).ToList();
                if (res.Count == 0)
                {
                    txtNoResults.Opacity = 1;
                    imgNoResults.Opacity = 1;
                }
                else
                {
                    txtNoResults.Opacity = 0;
                    imgNoResults.Opacity = 0;
                }
                lvItems.ItemsSource = res;
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
                button.Content = "♡";
            }
            else
            {
                AppData.Context.Wishlist.Add(new Wishlist
                {
                    idClient = AppData.current_user.idClient,
                    idMerchandise = item.idMerchandise
                });
                AppData.Context.SaveChanges();
                button.Content = "♥";
            }
        }

        private void btnLike_temp_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
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


        //public int merchSort = 0;
        //public int merchCategory = 0;
        //public int merchSize = 0;
        //public int merchBrand = 0;
        //public double priceStart = 0;
        //public double priceEnd = 0;
        //public int merchGender = 0;

        public List<Merchandise> MerchFilter()
        {
           
                try
                {
                    if (merchSort == -1)
                    {

                    //совпадений нет
                    if (merchCategory == 0 && merchSize == 0 && merchBrand == 0 && merchGender == 0)
                    {

                      
                        decimal low_price = Convert.ToDecimal(sliderPrice.ValueStart);
                        decimal high_price = Convert.ToDecimal(sliderPrice.ValueEnd);

                        return AppData.Context.Merchandise.Where(x => x.Price >= low_price && x.Price <= high_price).OrderByDescending(x => x.idMerchandise).ToList();
                    }
                    //совпадение всех полей
                    if (merchCategory != 0 && merchSize != 0 && merchBrand != 0  && merchGender != 0)
                       {

                            var sizeItem = (SizeGrid)cmbSize.SelectedItem;
                            string sizeValue = sizeItem.Value;
                            var size_value = AppData.Context.SizeGrid.Where(x => x.Value == sizeValue).FirstOrDefault();
                            var merchsize = AppData.Context.MerchSize.Where(x => x.idSizeGrid == size_value.idSizeGrid).ToList();

                            List<Merchandise> merch = new List<Merchandise>();
                            foreach (var m in merchsize)
                            {
                                merch.Add(m.Merchandise);
                            }

                            string brandName = "";
                            var typeItem = (Brand)cmbBrand.SelectedItem;
                            if (typeItem != null)
                            {
                                brandName = typeItem.Name;

                            }
                            decimal low_price = Convert.ToDecimal(sliderPrice.ValueStart);
                            decimal high_price = Convert.ToDecimal(sliderPrice.ValueEnd);

                            return merch.Where(x=> x.idCategory== merchCategory && x.Brand.Name == brandName && x.Price >= low_price && x.Price <= high_price && x.idGender == merchGender).OrderByDescending(x=>x.idMerchandise).ToList();
                       }
                       //кроме категории
                        if (merchCategory == 0 && merchSize != 0 && merchBrand != 0 && merchGender != 0)
                        {

                            var sizeItem = (SizeGrid)cmbSize.SelectedItem;
                            string sizeValue = sizeItem.Value;
                            var size_value = AppData.Context.SizeGrid.Where(x => x.Value == sizeValue).FirstOrDefault();
                            var merchsize = AppData.Context.MerchSize.Where(x => x.idSizeGrid == size_value.idSizeGrid).ToList();

                            List<Merchandise> merch = new List<Merchandise>();
                            foreach (var m in merchsize)
                            {
                                merch.Add(m.Merchandise);
                            }

                            string brandName = "";
                            var typeItem = (Brand)cmbBrand.SelectedItem;
                            if (typeItem != null)
                            {
                                brandName = typeItem.Name;

                            }
                            decimal low_price = Convert.ToDecimal(sliderPrice.ValueStart);
                            decimal high_price = Convert.ToDecimal(sliderPrice.ValueEnd);
                            return merch.Where(x => x.Brand.Name == brandName && x.Price >= low_price && x.Price <= high_price && x.idGender == merchGender).OrderByDescending(x => x.idMerchandise).ToList();
                        }
                        //кроме размера
                        if (merchCategory != 0 && merchSize == 0 && merchBrand != 0 && merchGender != 0)
                        {
                            string brandName = "";
                            var typeItem = (Brand)cmbBrand.SelectedItem;
                            if (typeItem != null)
                            {
                                brandName = typeItem.Name;

                            }
                            decimal low_price = Convert.ToDecimal(sliderPrice.ValueStart);
                            decimal high_price = Convert.ToDecimal(sliderPrice.ValueEnd);
                            return AppData.Context.Merchandise.Where(x => x.idCategory == merchCategory && x.Brand.Name == brandName && x.Price >= low_price && x.Price <= high_price && x.idGender == merchGender).OrderByDescending(x => x.idMerchandise).ToList();
                        }
                        //кроме бренда
                        if (merchCategory != 0 && merchSize != 0 && merchBrand == 0 && merchGender != 0)
                        {
                            var sizeItem = (SizeGrid)cmbSize.SelectedItem;
                            string sizeValue = sizeItem.Value;
                            var size_value = AppData.Context.SizeGrid.Where(x => x.Value == sizeValue).FirstOrDefault();
                            var merchsize = AppData.Context.MerchSize.Where(x => x.idSizeGrid == size_value.idSizeGrid).ToList();

                            List<Merchandise> merch = new List<Merchandise>();
                            foreach (var m in merchsize)
                            {
                                merch.Add(m.Merchandise);
                            }

                            decimal low_price = Convert.ToDecimal(sliderPrice.ValueStart);
                            decimal high_price = Convert.ToDecimal(sliderPrice.ValueEnd);
                            return merch.Where(x => x.idCategory == merchCategory  && x.Price >= low_price && x.Price <= high_price && x.idGender == merchGender).OrderByDescending(x => x.idMerchandise).ToList();
                        }
                        //кроме пола
                        if (merchCategory != 0 && merchSize != 0 && merchBrand != 0 && merchGender == 0)
                        {
                            var sizeItem = (SizeGrid)cmbSize.SelectedItem;
                            string sizeValue = sizeItem.Value;
                            var size_value = AppData.Context.SizeGrid.Where(x => x.Value == sizeValue).FirstOrDefault();
                            var merchsize = AppData.Context.MerchSize.Where(x => x.idSizeGrid == size_value.idSizeGrid).ToList();

                            List<Merchandise> merch = new List<Merchandise>();
                            foreach (var m in merchsize)
                            {
                                merch.Add(m.Merchandise);
                            }

                            string brandName = "";
                            var typeItem = (Brand)cmbBrand.SelectedItem;
                            if (typeItem != null)
                            {
                                brandName = typeItem.Name;

                            }
                            decimal low_price = Convert.ToDecimal(sliderPrice.ValueStart);
                            decimal high_price = Convert.ToDecimal(sliderPrice.ValueEnd);
                            return merch.Where(x => x.idCategory == merchCategory && x.Brand.Name == brandName && x.Price >= low_price && x.Price <= high_price).OrderByDescending(x => x.idMerchandise).ToList();
                        }
                        //кроме категории и размера
                        if (merchCategory == 0 && merchSize == 0 && merchBrand != 0 && merchGender != 0)
                        {
                            string brandName = "";
                            var typeItem = (Brand)cmbBrand.SelectedItem;
                            if (typeItem != null)
                            {
                                brandName = typeItem.Name;

                            }
                            decimal low_price = Convert.ToDecimal(sliderPrice.ValueStart);
                            decimal high_price = Convert.ToDecimal(sliderPrice.ValueEnd);
                            return AppData.Context.Merchandise.Where(x =>  x.Brand.Name == brandName && x.Price >= low_price && x.Price <= high_price && x.idGender == merchGender).OrderByDescending(x => x.idMerchandise).ToList();
                        }
                        //кроме категории и бренда
                        if (merchCategory == 0 && merchSize != 0 && merchBrand == 0 && merchGender != 0)
                        {
                            var sizeItem = (SizeGrid)cmbSize.SelectedItem;
                            string sizeValue = sizeItem.Value;
                            var size_value = AppData.Context.SizeGrid.Where(x => x.Value == sizeValue).FirstOrDefault();
                            var merchsize = AppData.Context.MerchSize.Where(x => x.idSizeGrid == size_value.idSizeGrid).ToList();

                            List<Merchandise> merch = new List<Merchandise>();
                            foreach (var m in merchsize)
                            {
                                merch.Add(m.Merchandise);
                            }

                            decimal low_price = Convert.ToDecimal(sliderPrice.ValueStart);
                            decimal high_price = Convert.ToDecimal(sliderPrice.ValueEnd);

                            return merch.Where(x => x.Price >= low_price && x.Price <= high_price && x.idGender == merchGender).OrderByDescending(x => x.idMerchandise).ToList();
                        }
                        //кроме категории и пола
                        if (merchCategory == 0 && merchSize != 0 && merchBrand != 0 && merchGender == 0)
                        {

                            var sizeItem = (SizeGrid)cmbSize.SelectedItem;
                            string sizeValue = sizeItem.Value;
                            var size_value = AppData.Context.SizeGrid.Where(x => x.Value == sizeValue).FirstOrDefault();
                            var merchsize = AppData.Context.MerchSize.Where(x => x.idSizeGrid == size_value.idSizeGrid).ToList();

                            List<Merchandise> merch = new List<Merchandise>();
                            foreach (var m in merchsize)
                            {
                                merch.Add(m.Merchandise);
                            }

                            string brandName = "";
                            var typeItem = (Brand)cmbBrand.SelectedItem;
                            if (typeItem != null)
                            {
                                brandName = typeItem.Name;

                            }
                            decimal low_price = Convert.ToDecimal(sliderPrice.ValueStart);
                            decimal high_price = Convert.ToDecimal(sliderPrice.ValueEnd);

                            return merch.Where(x => x.Brand.Name == brandName && x.Price >= low_price && x.Price <= high_price).OrderByDescending(x => x.idMerchandise).ToList();
                        }
                        //кроме размера и бренда
                        if (merchCategory != 0 && merchSize == 0 && merchBrand == 0 && merchGender != 0)
                        {

                          
                            decimal low_price = Convert.ToDecimal(sliderPrice.ValueStart);
                            decimal high_price = Convert.ToDecimal(sliderPrice.ValueEnd);

                            return AppData.Context.Merchandise.Where(x => x.idCategory == merchCategory && x.Price >= low_price && x.Price <= high_price && x.idGender == merchGender).OrderByDescending(x => x.idMerchandise).ToList();
                        }
                        //кроме размера и пола
                        if (merchCategory != 0 && merchSize == 0 && merchBrand != 0 && merchGender == 0)
                        {

                          
                            string brandName = "";
                            var typeItem = (Brand)cmbBrand.SelectedItem;
                            if (typeItem != null)
                            {
                                brandName = typeItem.Name;

                            }
                            decimal low_price = Convert.ToDecimal(sliderPrice.ValueStart);
                            decimal high_price = Convert.ToDecimal(sliderPrice.ValueEnd);

                            return AppData.Context.Merchandise.Where(x => x.idCategory == merchCategory && x.Brand.Name == brandName && x.Price >= low_price && x.Price <= high_price).OrderByDescending(x => x.idMerchandise).ToList();
                        }
                        //кроме бренда и пола
                        if (merchCategory != 0 && merchSize != 0 && merchBrand == 0 && merchGender == 0)
                        {

                            var sizeItem = (SizeGrid)cmbSize.SelectedItem;
                            string sizeValue = sizeItem.Value;
                            var size_value = AppData.Context.SizeGrid.Where(x => x.Value == sizeValue).FirstOrDefault();
                            var merchsize = AppData.Context.MerchSize.Where(x => x.idSizeGrid == size_value.idSizeGrid).ToList();

                            List<Merchandise> merch = new List<Merchandise>();
                            foreach (var m in merchsize)
                            {
                                merch.Add(m.Merchandise);
                            }

                           
                            decimal low_price = Convert.ToDecimal(sliderPrice.ValueStart);
                            decimal high_price = Convert.ToDecimal(sliderPrice.ValueEnd);

                            return merch.Where(x => x.idCategory == merchCategory  && x.Price >= low_price && x.Price <= high_price ).OrderByDescending(x => x.idMerchandise).ToList();
                        }
                        //только категория
                        if (merchCategory != 0 && merchSize == 0 && merchBrand == 0 && merchGender == 0)
                        {
                            decimal low_price = Convert.ToDecimal(sliderPrice.ValueStart);
                            decimal high_price = Convert.ToDecimal(sliderPrice.ValueEnd);

                            return AppData.Context.Merchandise.Where(x => x.idCategory == merchCategory && x.Price >= low_price && x.Price <= high_price).OrderByDescending(x => x.idMerchandise).ToList();
                        }
                        //только размер
                        if (merchCategory == 0 && merchSize != 0 && merchBrand == 0 && merchGender == 0)
                        {
                            var sizeItem = (SizeGrid)cmbSize.SelectedItem;
                            string sizeValue = sizeItem.Value;
                            var size_value = AppData.Context.SizeGrid.Where(x => x.Value == sizeValue).FirstOrDefault();
                            var merchsize = AppData.Context.MerchSize.Where(x => x.idSizeGrid == size_value.idSizeGrid).ToList();

                            List<Merchandise> merch = new List<Merchandise>();
                            foreach (var m in merchsize)
                            {
                                merch.Add(m.Merchandise);
                            }

                            decimal low_price = Convert.ToDecimal(sliderPrice.ValueStart);
                            decimal high_price = Convert.ToDecimal(sliderPrice.ValueEnd);

                            return merch.Where(x => x.Price >= low_price && x.Price <= high_price).OrderByDescending(x => x.idMerchandise).ToList();
                        }
                        //только бренд
                        if (merchCategory == 0 && merchSize == 0 && merchBrand != 0 && merchGender == 0)
                        {

                          
                            string brandName = "";
                            var typeItem = (Brand)cmbBrand.SelectedItem;
                            if (typeItem != null)
                            {
                                brandName = typeItem.Name;

                            }
                            decimal low_price = Convert.ToDecimal(sliderPrice.ValueStart);
                            decimal high_price = Convert.ToDecimal(sliderPrice.ValueEnd);

                            return AppData.Context.Merchandise.Where(x =>  x.Brand.Name == brandName && x.Price >= low_price && x.Price <= high_price ).OrderByDescending(x => x.idMerchandise).ToList();
                        }
                        //только пол
                        if (merchCategory == 0 && merchSize == 0 && merchBrand == 0 && merchGender != 0)
                        {
                            decimal low_price = Convert.ToDecimal(sliderPrice.ValueStart);
                            decimal high_price = Convert.ToDecimal(sliderPrice.ValueEnd);

                            return AppData.Context.Merchandise.Where(x => x.idGender == merchGender && x.Price >= low_price && x.Price <= high_price).OrderByDescending(x => x.idMerchandise).ToList();
                        }

                    }
                if (merchSort == 1)
                {
                    //совпадений нет
                    if (merchCategory == 0 && merchSize == 0 && merchBrand == 0 && merchGender == 0)
                    {


                        decimal low_price = Convert.ToDecimal(sliderPrice.ValueStart);
                        decimal high_price = Convert.ToDecimal(sliderPrice.ValueEnd);

                        return AppData.Context.Merchandise.Where(x => x.Price >= low_price && x.Price <= high_price).OrderByDescending(x => x.Price).ToList();
                    }
                    //совпадение всех полей
                    if (merchCategory != 0 && merchSize != 0 && merchBrand != 0 && merchGender != 0)
                    {

                        var sizeItem = (SizeGrid)cmbSize.SelectedItem;
                        string sizeValue = sizeItem.Value;
                        var size_value = AppData.Context.SizeGrid.Where(x => x.Value == sizeValue).FirstOrDefault();
                        var merchsize = AppData.Context.MerchSize.Where(x => x.idSizeGrid == size_value.idSizeGrid).ToList();

                        List<Merchandise> merch = new List<Merchandise>();
                        foreach (var m in merchsize)
                        {
                            merch.Add(m.Merchandise);
                        }

                        string brandName = "";
                        var typeItem = (Brand)cmbBrand.SelectedItem;
                        if (typeItem != null)
                        {
                            brandName = typeItem.Name;

                        }
                        decimal low_price = Convert.ToDecimal(sliderPrice.ValueStart);
                        decimal high_price = Convert.ToDecimal(sliderPrice.ValueEnd);

                        return merch.Where(x => x.idCategory == merchCategory && x.Brand.Name == brandName && x.Price >= low_price && x.Price <= high_price && x.idGender == merchGender).OrderByDescending(x => x.Price).ToList();
                    }
                    //кроме категории
                    if (merchCategory == 0 && merchSize != 0 && merchBrand != 0 && merchGender != 0)
                    {

                        var sizeItem = (SizeGrid)cmbSize.SelectedItem;
                        string sizeValue = sizeItem.Value;
                        var size_value = AppData.Context.SizeGrid.Where(x => x.Value == sizeValue).FirstOrDefault();
                        var merchsize = AppData.Context.MerchSize.Where(x => x.idSizeGrid == size_value.idSizeGrid).ToList();

                        List<Merchandise> merch = new List<Merchandise>();
                        foreach (var m in merchsize)
                        {
                            merch.Add(m.Merchandise);
                        }

                        string brandName = "";
                        var typeItem = (Brand)cmbBrand.SelectedItem;
                        if (typeItem != null)
                        {
                            brandName = typeItem.Name;

                        }
                        decimal low_price = Convert.ToDecimal(sliderPrice.ValueStart);
                        decimal high_price = Convert.ToDecimal(sliderPrice.ValueEnd);
                        return merch.Where(x => x.Brand.Name == brandName && x.Price >= low_price && x.Price <= high_price && x.idGender == merchGender).OrderByDescending(x => x.Price).ToList();
                    }
                    //кроме размера
                    if (merchCategory != 0 && merchSize == 0 && merchBrand != 0 && merchGender != 0)
                    {
                        string brandName = "";
                        var typeItem = (Brand)cmbBrand.SelectedItem;
                        if (typeItem != null)
                        {
                            brandName = typeItem.Name;

                        }
                        decimal low_price = Convert.ToDecimal(sliderPrice.ValueStart);
                        decimal high_price = Convert.ToDecimal(sliderPrice.ValueEnd);
                        return AppData.Context.Merchandise.Where(x => x.idCategory == merchCategory && x.Brand.Name == brandName && x.Price >= low_price && x.Price <= high_price && x.idGender == merchGender).OrderByDescending(x => x.Price).ToList();
                    }
                    //кроме бренда
                    if (merchCategory != 0 && merchSize != 0 && merchBrand == 0 && merchGender != 0)
                    {
                        var sizeItem = (SizeGrid)cmbSize.SelectedItem;
                        string sizeValue = sizeItem.Value;
                        var size_value = AppData.Context.SizeGrid.Where(x => x.Value == sizeValue).FirstOrDefault();
                        var merchsize = AppData.Context.MerchSize.Where(x => x.idSizeGrid == size_value.idSizeGrid).ToList();

                        List<Merchandise> merch = new List<Merchandise>();
                        foreach (var m in merchsize)
                        {
                            merch.Add(m.Merchandise);
                        }

                        decimal low_price = Convert.ToDecimal(sliderPrice.ValueStart);
                        decimal high_price = Convert.ToDecimal(sliderPrice.ValueEnd);
                        return merch.Where(x => x.idCategory == merchCategory && x.Price >= low_price && x.Price <= high_price && x.idGender == merchGender).OrderByDescending(x => x.Price).ToList();
                    }
                    //кроме пола
                    if (merchCategory != 0 && merchSize != 0 && merchBrand != 0 && merchGender == 0)
                    {
                        var sizeItem = (SizeGrid)cmbSize.SelectedItem;
                        string sizeValue = sizeItem.Value;
                        var size_value = AppData.Context.SizeGrid.Where(x => x.Value == sizeValue).FirstOrDefault();
                        var merchsize = AppData.Context.MerchSize.Where(x => x.idSizeGrid == size_value.idSizeGrid).ToList();

                        List<Merchandise> merch = new List<Merchandise>();
                        foreach (var m in merchsize)
                        {
                            merch.Add(m.Merchandise);
                        }

                        string brandName = "";
                        var typeItem = (Brand)cmbBrand.SelectedItem;
                        if (typeItem != null)
                        {
                            brandName = typeItem.Name;

                        }
                        decimal low_price = Convert.ToDecimal(sliderPrice.ValueStart);
                        decimal high_price = Convert.ToDecimal(sliderPrice.ValueEnd);
                        return merch.Where(x => x.idCategory == merchCategory && x.Brand.Name == brandName && x.Price >= low_price && x.Price <= high_price).OrderByDescending(x => x.Price).ToList();
                    }
                    //кроме категории и размера
                    if (merchCategory == 0 && merchSize == 0 && merchBrand != 0 && merchGender != 0)
                    {
                        string brandName = "";
                        var typeItem = (Brand)cmbBrand.SelectedItem;
                        if (typeItem != null)
                        {
                            brandName = typeItem.Name;

                        }
                        decimal low_price = Convert.ToDecimal(sliderPrice.ValueStart);
                        decimal high_price = Convert.ToDecimal(sliderPrice.ValueEnd);
                        return AppData.Context.Merchandise.Where(x => x.Brand.Name == brandName && x.Price >= low_price && x.Price <= high_price && x.idGender == merchGender).OrderByDescending(x => x.Price).ToList();
                    }
                    //кроме категории и бренда
                    if (merchCategory == 0 && merchSize != 0 && merchBrand == 0 && merchGender != 0)
                    {
                        var sizeItem = (SizeGrid)cmbSize.SelectedItem;
                        string sizeValue = sizeItem.Value;
                        var size_value = AppData.Context.SizeGrid.Where(x => x.Value == sizeValue).FirstOrDefault();
                        var merchsize = AppData.Context.MerchSize.Where(x => x.idSizeGrid == size_value.idSizeGrid).ToList();

                        List<Merchandise> merch = new List<Merchandise>();
                        foreach (var m in merchsize)
                        {
                            merch.Add(m.Merchandise);
                        }

                        decimal low_price = Convert.ToDecimal(sliderPrice.ValueStart);
                        decimal high_price = Convert.ToDecimal(sliderPrice.ValueEnd);

                        return merch.Where(x => x.Price >= low_price && x.Price <= high_price && x.idGender == merchGender).OrderByDescending(x => x.Price).ToList();
                    }
                    //кроме категории и пола
                    if (merchCategory == 0 && merchSize != 0 && merchBrand != 0 && merchGender == 0)
                    {

                        var sizeItem = (SizeGrid)cmbSize.SelectedItem;
                        string sizeValue = sizeItem.Value;
                        var size_value = AppData.Context.SizeGrid.Where(x => x.Value == sizeValue).FirstOrDefault();
                        var merchsize = AppData.Context.MerchSize.Where(x => x.idSizeGrid == size_value.idSizeGrid).ToList();

                        List<Merchandise> merch = new List<Merchandise>();
                        foreach (var m in merchsize)
                        {
                            merch.Add(m.Merchandise);
                        }

                        string brandName = "";
                        var typeItem = (Brand)cmbBrand.SelectedItem;
                        if (typeItem != null)
                        {
                            brandName = typeItem.Name;

                        }
                        decimal low_price = Convert.ToDecimal(sliderPrice.ValueStart);
                        decimal high_price = Convert.ToDecimal(sliderPrice.ValueEnd);

                        return merch.Where(x => x.Brand.Name == brandName && x.Price >= low_price && x.Price <= high_price).OrderByDescending(x => x.Price).ToList();
                    }
                    //кроме размера и бренда
                    if (merchCategory != 0 && merchSize == 0 && merchBrand == 0 && merchGender != 0)
                    {


                        decimal low_price = Convert.ToDecimal(sliderPrice.ValueStart);
                        decimal high_price = Convert.ToDecimal(sliderPrice.ValueEnd);

                        return AppData.Context.Merchandise.Where(x => x.idCategory == merchCategory && x.Price >= low_price && x.Price <= high_price && x.idGender == merchGender).OrderByDescending(x => x.Price).ToList();
                    }
                    //кроме размера и пола
                    if (merchCategory != 0 && merchSize == 0 && merchBrand != 0 && merchGender == 0)
                    {


                        string brandName = "";
                        var typeItem = (Brand)cmbBrand.SelectedItem;
                        if (typeItem != null)
                        {
                            brandName = typeItem.Name;

                        }
                        decimal low_price = Convert.ToDecimal(sliderPrice.ValueStart);
                        decimal high_price = Convert.ToDecimal(sliderPrice.ValueEnd);

                        return AppData.Context.Merchandise.Where(x => x.idCategory == merchCategory && x.Brand.Name == brandName && x.Price >= low_price && x.Price <= high_price).OrderByDescending(x => x.Price).ToList();
                    }
                    //кроме бренда и пола
                    if (merchCategory != 0 && merchSize != 0 && merchBrand == 0 && merchGender == 0)
                    {

                        var sizeItem = (SizeGrid)cmbSize.SelectedItem;
                        string sizeValue = sizeItem.Value;
                        var size_value = AppData.Context.SizeGrid.Where(x => x.Value == sizeValue).FirstOrDefault();
                        var merchsize = AppData.Context.MerchSize.Where(x => x.idSizeGrid == size_value.idSizeGrid).ToList();

                        List<Merchandise> merch = new List<Merchandise>();
                        foreach (var m in merchsize)
                        {
                            merch.Add(m.Merchandise);
                        }


                        decimal low_price = Convert.ToDecimal(sliderPrice.ValueStart);
                        decimal high_price = Convert.ToDecimal(sliderPrice.ValueEnd);

                        return merch.Where(x => x.idCategory == merchCategory && x.Price >= low_price && x.Price <= high_price).OrderByDescending(x => x.Price).ToList();
                    }
                    //только категория
                    if (merchCategory != 0 && merchSize == 0 && merchBrand == 0 && merchGender == 0)
                    {
                        decimal low_price = Convert.ToDecimal(sliderPrice.ValueStart);
                        decimal high_price = Convert.ToDecimal(sliderPrice.ValueEnd);

                        return AppData.Context.Merchandise.Where(x => x.idCategory == merchCategory && x.Price >= low_price && x.Price <= high_price).OrderByDescending(x => x.Price).ToList();
                    }
                    //только размер
                    if (merchCategory == 0 && merchSize != 0 && merchBrand == 0 && merchGender == 0)
                    {
                        var sizeItem = (SizeGrid)cmbSize.SelectedItem;
                        string sizeValue = sizeItem.Value;
                        var size_value = AppData.Context.SizeGrid.Where(x => x.Value == sizeValue).FirstOrDefault();
                        var merchsize = AppData.Context.MerchSize.Where(x => x.idSizeGrid == size_value.idSizeGrid).ToList();

                        List<Merchandise> merch = new List<Merchandise>();
                        foreach (var m in merchsize)
                        {
                            merch.Add(m.Merchandise);
                        }

                        decimal low_price = Convert.ToDecimal(sliderPrice.ValueStart);
                        decimal high_price = Convert.ToDecimal(sliderPrice.ValueEnd);

                        return merch.Where(x => x.Price >= low_price && x.Price <= high_price).OrderByDescending(x => x.idMerchandise).ToList();
                    }
                    //только бренд
                    if (merchCategory == 0 && merchSize == 0 && merchBrand != 0 && merchGender == 0)
                    {


                        string brandName = "";
                        var typeItem = (Brand)cmbBrand.SelectedItem;
                        if (typeItem != null)
                        {
                            brandName = typeItem.Name;

                        }
                        decimal low_price = Convert.ToDecimal(sliderPrice.ValueStart);
                        decimal high_price = Convert.ToDecimal(sliderPrice.ValueEnd);

                        return AppData.Context.Merchandise.Where(x => x.Brand.Name == brandName && x.Price >= low_price && x.Price <= high_price).OrderByDescending(x => x.Price).ToList();
                    }
                    //только пол
                    if (merchCategory == 0 && merchSize == 0 && merchBrand == 0 && merchGender != 0)
                    {
                        decimal low_price = Convert.ToDecimal(sliderPrice.ValueStart);
                        decimal high_price = Convert.ToDecimal(sliderPrice.ValueEnd);

                        return AppData.Context.Merchandise.Where(x => x.idGender == merchGender && x.Price >= low_price && x.Price <= high_price).OrderByDescending(x => x.Price).ToList();
                    }

                }

                if (merchSort == 2)
                {
                    //совпадений нет
                    if (merchCategory == 0 && merchSize == 0 && merchBrand == 0 && merchGender == 0)
                    {


                        decimal low_price = Convert.ToDecimal(sliderPrice.ValueStart);
                        decimal high_price = Convert.ToDecimal(sliderPrice.ValueEnd);

                        return AppData.Context.Merchandise.Where(x => x.Price >= low_price && x.Price <= high_price).OrderBy(x => x.Price).ToList();
                    }
                    //совпадение всех полей
                    if (merchCategory != 0 && merchSize != 0 && merchBrand != 0 && merchGender != 0)
                    {

                        var sizeItem = (SizeGrid)cmbSize.SelectedItem;
                        string sizeValue = sizeItem.Value;
                        var size_value = AppData.Context.SizeGrid.Where(x => x.Value == sizeValue).FirstOrDefault();
                        var merchsize = AppData.Context.MerchSize.Where(x => x.idSizeGrid == size_value.idSizeGrid).ToList();

                        List<Merchandise> merch = new List<Merchandise>();
                        foreach (var m in merchsize)
                        {
                            merch.Add(m.Merchandise);
                        }

                        string brandName = "";
                        var typeItem = (Brand)cmbBrand.SelectedItem;
                        if (typeItem != null)
                        {
                            brandName = typeItem.Name;

                        }
                        decimal low_price = Convert.ToDecimal(sliderPrice.ValueStart);
                        decimal high_price = Convert.ToDecimal(sliderPrice.ValueEnd);

                        return merch.Where(x => x.idCategory == merchCategory && x.Brand.Name == brandName && x.Price >= low_price && x.Price <= high_price && x.idGender == merchGender).OrderBy(x => x.Price).ToList();
                    }
                    //кроме категории
                    if (merchCategory == 0 && merchSize != 0 && merchBrand != 0 && merchGender != 0)
                    {

                        var sizeItem = (SizeGrid)cmbSize.SelectedItem;
                        string sizeValue = sizeItem.Value;
                        var size_value = AppData.Context.SizeGrid.Where(x => x.Value == sizeValue).FirstOrDefault();
                        var merchsize = AppData.Context.MerchSize.Where(x => x.idSizeGrid == size_value.idSizeGrid).ToList();

                        List<Merchandise> merch = new List<Merchandise>();
                        foreach (var m in merchsize)
                        {
                            merch.Add(m.Merchandise);
                        }

                        string brandName = "";
                        var typeItem = (Brand)cmbBrand.SelectedItem;
                        if (typeItem != null)
                        {
                            brandName = typeItem.Name;

                        }
                        decimal low_price = Convert.ToDecimal(sliderPrice.ValueStart);
                        decimal high_price = Convert.ToDecimal(sliderPrice.ValueEnd);
                        return merch.Where(x => x.Brand.Name == brandName && x.Price >= low_price && x.Price <= high_price && x.idGender == merchGender).OrderBy(x => x.Price).ToList();
                    }
                    //кроме размера
                    if (merchCategory != 0 && merchSize == 0 && merchBrand != 0 && merchGender != 0)
                    {
                        string brandName = "";
                        var typeItem = (Brand)cmbBrand.SelectedItem;
                        if (typeItem != null)
                        {
                            brandName = typeItem.Name;

                        }
                        decimal low_price = Convert.ToDecimal(sliderPrice.ValueStart);
                        decimal high_price = Convert.ToDecimal(sliderPrice.ValueEnd);
                        return AppData.Context.Merchandise.Where(x => x.idCategory == merchCategory && x.Brand.Name == brandName && x.Price >= low_price && x.Price <= high_price && x.idGender == merchGender).OrderBy(x => x.Price).ToList();
                    }
                    //кроме бренда
                    if (merchCategory != 0 && merchSize != 0 && merchBrand == 0 && merchGender != 0)
                    {
                        var sizeItem = (SizeGrid)cmbSize.SelectedItem;
                        string sizeValue = sizeItem.Value;
                        var size_value = AppData.Context.SizeGrid.Where(x => x.Value == sizeValue).FirstOrDefault();
                        var merchsize = AppData.Context.MerchSize.Where(x => x.idSizeGrid == size_value.idSizeGrid).ToList();

                        List<Merchandise> merch = new List<Merchandise>();
                        foreach (var m in merchsize)
                        {
                            merch.Add(m.Merchandise);
                        }

                        decimal low_price = Convert.ToDecimal(sliderPrice.ValueStart);
                        decimal high_price = Convert.ToDecimal(sliderPrice.ValueEnd);
                        return merch.Where(x => x.idCategory == merchCategory && x.Price >= low_price && x.Price <= high_price && x.idGender == merchGender).OrderBy(x => x.Price).ToList();
                    }
                    //кроме пола
                    if (merchCategory != 0 && merchSize != 0 && merchBrand != 0 && merchGender == 0)
                    {
                        var sizeItem = (SizeGrid)cmbSize.SelectedItem;
                        string sizeValue = sizeItem.Value;
                        var size_value = AppData.Context.SizeGrid.Where(x => x.Value == sizeValue).FirstOrDefault();
                        var merchsize = AppData.Context.MerchSize.Where(x => x.idSizeGrid == size_value.idSizeGrid).ToList();

                        List<Merchandise> merch = new List<Merchandise>();
                        foreach (var m in merchsize)
                        {
                            merch.Add(m.Merchandise);
                        }

                        string brandName = "";
                        var typeItem = (Brand)cmbBrand.SelectedItem;
                        if (typeItem != null)
                        {
                            brandName = typeItem.Name;

                        }
                        decimal low_price = Convert.ToDecimal(sliderPrice.ValueStart);
                        decimal high_price = Convert.ToDecimal(sliderPrice.ValueEnd);
                        return merch.Where(x => x.idCategory == merchCategory && x.Brand.Name == brandName && x.Price >= low_price && x.Price <= high_price).OrderBy(x => x.Price).ToList();
                    }
                    //кроме категории и размера
                    if (merchCategory == 0 && merchSize == 0 && merchBrand != 0 && merchGender != 0)
                    {
                        string brandName = "";
                        var typeItem = (Brand)cmbBrand.SelectedItem;
                        if (typeItem != null)
                        {
                            brandName = typeItem.Name;

                        }
                        decimal low_price = Convert.ToDecimal(sliderPrice.ValueStart);
                        decimal high_price = Convert.ToDecimal(sliderPrice.ValueEnd);
                        return AppData.Context.Merchandise.Where(x => x.Brand.Name == brandName && x.Price >= low_price && x.Price <= high_price && x.idGender == merchGender).OrderBy(x => x.Price).ToList();
                    }
                    //кроме категории и бренда
                    if (merchCategory == 0 && merchSize != 0 && merchBrand == 0 && merchGender != 0)
                    {
                        var sizeItem = (SizeGrid)cmbSize.SelectedItem;
                        string sizeValue = sizeItem.Value;
                        var size_value = AppData.Context.SizeGrid.Where(x => x.Value == sizeValue).FirstOrDefault();
                        var merchsize = AppData.Context.MerchSize.Where(x => x.idSizeGrid == size_value.idSizeGrid).ToList();

                        List<Merchandise> merch = new List<Merchandise>();
                        foreach (var m in merchsize)
                        {
                            merch.Add(m.Merchandise);
                        }

                        decimal low_price = Convert.ToDecimal(sliderPrice.ValueStart);
                        decimal high_price = Convert.ToDecimal(sliderPrice.ValueEnd);

                        return merch.Where(x => x.Price >= low_price && x.Price <= high_price && x.idGender == merchGender).OrderBy(x => x.Price).ToList();
                    }
                    //кроме категории и пола
                    if (merchCategory == 0 && merchSize != 0 && merchBrand != 0 && merchGender == 0)
                    {

                        var sizeItem = (SizeGrid)cmbSize.SelectedItem;
                        string sizeValue = sizeItem.Value;
                        var size_value = AppData.Context.SizeGrid.Where(x => x.Value == sizeValue).FirstOrDefault();
                        var merchsize = AppData.Context.MerchSize.Where(x => x.idSizeGrid == size_value.idSizeGrid).ToList();

                        List<Merchandise> merch = new List<Merchandise>();
                        foreach (var m in merchsize)
                        {
                            merch.Add(m.Merchandise);
                        }

                        string brandName = "";
                        var typeItem = (Brand)cmbBrand.SelectedItem;
                        if (typeItem != null)
                        {
                            brandName = typeItem.Name;

                        }
                        decimal low_price = Convert.ToDecimal(sliderPrice.ValueStart);
                        decimal high_price = Convert.ToDecimal(sliderPrice.ValueEnd);

                        return merch.Where(x => x.Brand.Name == brandName && x.Price >= low_price && x.Price <= high_price).OrderBy(x => x.Price).ToList();
                    }
                    //кроме размера и бренда
                    if (merchCategory != 0 && merchSize == 0 && merchBrand == 0 && merchGender != 0)
                    {


                        decimal low_price = Convert.ToDecimal(sliderPrice.ValueStart);
                        decimal high_price = Convert.ToDecimal(sliderPrice.ValueEnd);

                        return AppData.Context.Merchandise.Where(x => x.idCategory == merchCategory && x.Price >= low_price && x.Price <= high_price && x.idGender == merchGender).OrderBy(x => x.Price).ToList();
                    }
                    //кроме размера и пола
                    if (merchCategory != 0 && merchSize == 0 && merchBrand != 0 && merchGender == 0)
                    {


                        string brandName = "";
                        var typeItem = (Brand)cmbBrand.SelectedItem;
                        if (typeItem != null)
                        {
                            brandName = typeItem.Name;

                        }
                        decimal low_price = Convert.ToDecimal(sliderPrice.ValueStart);
                        decimal high_price = Convert.ToDecimal(sliderPrice.ValueEnd);

                        return AppData.Context.Merchandise.Where(x => x.idCategory == merchCategory && x.Brand.Name == brandName && x.Price >= low_price && x.Price <= high_price).OrderBy(x => x.Price).ToList();
                    }
                    //кроме бренда и пола
                    if (merchCategory != 0 && merchSize != 0 && merchBrand == 0 && merchGender == 0)
                    {

                        var sizeItem = (SizeGrid)cmbSize.SelectedItem;
                        string sizeValue = sizeItem.Value;
                        var size_value = AppData.Context.SizeGrid.Where(x => x.Value == sizeValue).FirstOrDefault();
                        var merchsize = AppData.Context.MerchSize.Where(x => x.idSizeGrid == size_value.idSizeGrid).ToList();

                        List<Merchandise> merch = new List<Merchandise>();
                        foreach (var m in merchsize)
                        {
                            merch.Add(m.Merchandise);
                        }


                        decimal low_price = Convert.ToDecimal(sliderPrice.ValueStart);
                        decimal high_price = Convert.ToDecimal(sliderPrice.ValueEnd);

                        return merch.Where(x => x.idCategory == merchCategory && x.Price >= low_price && x.Price <= high_price).OrderBy(x => x.Price).ToList();
                    }
                    //только категория
                    if (merchCategory != 0 && merchSize == 0 && merchBrand == 0 && merchGender == 0)
                    {
                        decimal low_price = Convert.ToDecimal(sliderPrice.ValueStart);
                        decimal high_price = Convert.ToDecimal(sliderPrice.ValueEnd);

                        return AppData.Context.Merchandise.Where(x => x.idCategory == merchCategory && x.Price >= low_price && x.Price <= high_price).OrderBy(x => x.Price).ToList();
                    }
                    //только размер
                    if (merchCategory == 0 && merchSize != 0 && merchBrand == 0 && merchGender == 0)
                    {
                        var sizeItem = (SizeGrid)cmbSize.SelectedItem;
                        string sizeValue = sizeItem.Value;
                        var size_value = AppData.Context.SizeGrid.Where(x => x.Value == sizeValue).FirstOrDefault();
                        var merchsize = AppData.Context.MerchSize.Where(x => x.idSizeGrid == size_value.idSizeGrid).ToList();

                        List<Merchandise> merch = new List<Merchandise>();
                        foreach (var m in merchsize)
                        {
                            merch.Add(m.Merchandise);
                        }

                        decimal low_price = Convert.ToDecimal(sliderPrice.ValueStart);
                        decimal high_price = Convert.ToDecimal(sliderPrice.ValueEnd);

                        return merch.Where(x => x.Price >= low_price && x.Price <= high_price).OrderBy(x => x.idMerchandise).ToList();
                    }
                    //только бренд
                    if (merchCategory == 0 && merchSize == 0 && merchBrand != 0 && merchGender == 0)
                    {


                        string brandName = "";
                        var typeItem = (Brand)cmbBrand.SelectedItem;
                        if (typeItem != null)
                        {
                            brandName = typeItem.Name;

                        }
                        decimal low_price = Convert.ToDecimal(sliderPrice.ValueStart);
                        decimal high_price = Convert.ToDecimal(sliderPrice.ValueEnd);

                        return AppData.Context.Merchandise.Where(x => x.Brand.Name == brandName && x.Price >= low_price && x.Price <= high_price).OrderBy(x => x.Price).ToList();
                    }
                    //только пол
                    if (merchCategory == 0 && merchSize == 0 && merchBrand == 0 && merchGender != 0)
                    {
                        decimal low_price = Convert.ToDecimal(sliderPrice.ValueStart);
                        decimal high_price = Convert.ToDecimal(sliderPrice.ValueEnd);

                        return AppData.Context.Merchandise.Where(x => x.idGender == merchGender && x.Price >= low_price && x.Price <= high_price).OrderBy(x => x.Price).ToList();
                    }

                }



            }
            catch { }
                 
            
           

            return merchList;
        }


        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
          
            lvItems.ItemsSource = MerchFilter();
        }

        //private void tbSearch_PreviewKeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.Key == Key.Back || e.Key == Key.Delete)
        //    {
        //        if(tbSearch.Text == "")
        //        {
        //            return;
        //        }

        //        var res = AppData.Context.Merchandise.Where(x => x.Name.Contains(tbSearch.Text)).OrderBy(x => x.Price).ToList();
        //        if (res.Count == 0)
        //        {
        //            txtNoResults.Opacity = 1;
        //            imgNoResults.Opacity = 1;
        //            lvItems.ItemsSource = res;
        //            MessageBox.Show("0");

        //        }
        //        else
        //        {
        //            txtNoResults.Opacity = 0;
        //            imgNoResults.Opacity = 0;
        //            lvItems.ItemsSource = res;
        //            MessageBox.Show("1");
        //            e.Handled = true;
        //        }

        //    }
        //}











    }
}
