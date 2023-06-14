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
using System.Windows.Shapes;
using skeleton_market.Classes;
using skeleton_market.EF;


namespace skeleton_market.Windows
{
    /// <summary>
    /// Логика взаимодействия для DeliveryWindow.xaml
    /// </summary>
    public partial class DeliveryWindow : Window
    {
        public DeliveryWindow()
        {
            InitializeComponent();
            List<PickUpPoint> points = new List<PickUpPoint>();
            points = AppData.Context.PickUpPoint.ToList();
            points.Insert(0, new PickUpPoint { Address = "--Постамат--" });
            cmbPickUpPoint.ItemsSource = points;
            cmbPickUpPoint.DisplayMemberPath = "Address";

            cmbPickUpPoint.SelectedIndex = 0;
        }

        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {
            if(cmbPickUpPoint.SelectedIndex== 0)
            {
                return;
            }

            foreach(var point in AppData.Context.PickUpPoint.ToList())
            {
                if(point.Address == cmbPickUpPoint.Text)
                {
                    AppData.pick_point = point;
                }
            }

            this.Close();

        }
    }
}
