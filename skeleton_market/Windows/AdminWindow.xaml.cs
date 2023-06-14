using Microsoft.Win32;
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
using System.Windows.Shapes;
using System.IO;

namespace skeleton_market.Windows
{
    /// <summary>
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        
       
        public AdminWindow()
        {
            InitializeComponent();
            cmbIdsItems.ItemsSource = AppData.Context.Merchandise.ToList();
            cmbIdsItems.DisplayMemberPath = "idMerchandise";
        }

        private void btnSelectImage_Click(object sender, RoutedEventArgs e)
        {
            if(cmbIdsItems.SelectedIndex == -1)
            {
                MessageBox.Show("Не выбран id");
                return;
            }

            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == true)
            {
                //fileDialog.Filter = "Image files (*.BMP, *.JPG, *.GIF, *.TIF, *.PNG, *.ICO, *.EMF, *.WMF)|*.bmp;*.jpg;*.gif; *.tif; *.png; *.ico; *.emf; *.wmf";
                ofd.Filter = "PNG Photos (*.png, *.jpg)|*.png;*.jpg";
                
                img_preview.Source = new BitmapImage(new Uri(ofd.FileName));
            }

            EF.Merchandise merch = new EF.Merchandise();
            merch = AppData.Context.Merchandise.Where(x => x.idMerchandise == cmbIdsItems.SelectedIndex + 1).FirstOrDefault();
            merch.Photo = File.ReadAllBytes(ofd.FileName);
            MessageBox.Show("ok");
            AppData.Context.SaveChanges();

        }
    }
}
