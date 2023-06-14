using skeleton_market.Classes;
using skeleton_market.EF;
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
    /// Логика взаимодействия для MyOrdersPage.xaml
    /// </summary>
    public partial class MyOrdersPage : Page
    {
    
        List<Checkout> checkoutList = new List<Checkout>();
        List<Bag> bags = new List<Bag>();
       

        public MyOrdersPage()
        {
            InitializeComponent();
            List<MerchSize> merchSizeList = new List<MerchSize>();
            try
            {
                checkoutList = AppData.Context.Checkout.Where(x => x.idClient == AppData.current_user.idClient).ToList();
                foreach (var item in checkoutList)
                {              
                    bags = AppData.Context.Bag.Where(x => x.idCheckout == item.idCheckout).ToList();
                    foreach(var item2 in bags)
                    {
                        var merch = AppData.Context.MerchSize.Where(x => x.idMerchSize == item2.idMerchSize).ToList().FirstOrDefault();
                        merchSizeList.Add(merch);
                    }
                }

               

            var new_list = from o in merchSizeList
                           join m in AppData.Context.Merchandise on o.idMerchandise equals m.idMerchandise
                           join s in AppData.Context.SizeGrid on o.idSizeGrid equals s.idSizeGrid
                           join bag in AppData.Context.Bag on o.idMerchSize equals bag.idMerchSize
                           join check in AppData.Context.Checkout on bag.idCheckout equals check.idCheckout

                           select new
                           {
                               Name = m.Name,
                               Price = m.Price * o.Qty,
                               Photo = m.Photo,
                               Qty = o.Qty,
                               SaleDate = check.Date,
                               QtyInStock = AppData.Context.MerchSize.Where(x => x.idMerchandise == o.idMerchandise && x.idSizeGrid == o.idSizeGrid).Select(x => x.Qty).FirstOrDefault(),
                               SizeGrid = AppData.Context.MerchSize.Where(x => x.idMerchandise == o.idMerchandise).Select(x => x.SizeGrid.Value).ToList(),
                               SizeSelect = AppData.Context.MerchSize.Where(x => x.idMerchandise == o.idMerchandise && x.idSizeGrid == o.idSizeGrid).Select(x => x.SizeGrid.Value).FirstOrDefault()
                           };
            lvItems.ItemsSource = new_list.OrderByDescending(x=> x.SaleDate).ToList();
            }
            catch
            {
                txtNoResults.Opacity = 1;
                imgNoResults.Opacity = 1;
            }

        }
    }
}
