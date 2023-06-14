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
using skeleton_market.Windows;
using System.Data;

namespace skeleton_market.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddressPage.xaml
    /// </summary>
    public partial class AddressPage : Page
    {
        private List<City> cityList = new List<City>();
        public AddressPage()
        {
            InitializeComponent();
            cityList = AppData.Context.City.OrderBy(x=> x.Name).ToList();
            cityList.Insert(0, new City { Name = "--Выберите город--" });
            cmbCity.ItemsSource = cityList;
            cmbCity.DisplayMemberPath = "Name";         
            cmbCity.SelectedIndex = 0;
            //проверка на существование адреса у клиента
            try
            {
                EF.ClientAddress check_address_exist = AppData.Context.ClientAddress.Where(x => x.idClient == AppData.current_user.idClient).FirstOrDefault();
                EF.Address address = AppData.Context.Address.Where(x => x.idAddress == check_address_exist.idAddress).FirstOrDefault();
                EF.City city = AppData.Context.City.Where(x => x.idCity == address.idCity).FirstOrDefault();

                if (check_address_exist != null)
                { 
                    //заполнение текстовых полей значениями адреса
                    //получение индекса города в combobox по названию
                    var comboBoxItem = cmbCity.Items.OfType<City>().FirstOrDefault(x => x.Name.ToString() == city.Name.ToString());
                    int cityIndex = cmbCity.SelectedIndex = cmbCity.Items.IndexOf(comboBoxItem);
                    cmbCity.SelectedIndex = cityIndex;

                    tbAddress.Text = address.Street;
                    tbPostCode.Text = address.MailIndex;
                }
            }
            catch(Exception ex)
            {

            }


        }

        private void btnSaveChange_Click(object sender, RoutedEventArgs e)
        {
            if(cmbCity.SelectedIndex == 0)
            {
                MessageWindow mw0 = new MessageWindow("Выберите город");
                mw0.ShowDialog();
                return;
            }

            if (string.IsNullOrWhiteSpace(tbAddress.Text))
            {
                MessageWindow mw1 = new MessageWindow("Введите адрес");
                mw1.ShowDialog();
                return;
            }
            if (string.IsNullOrWhiteSpace(tbPostCode.Text))
            {
                MessageWindow mw2 = new MessageWindow("Введите почтовый индекс");
                mw2.ShowDialog();
                return;
            }
            //получение текста из выбранного элемента

            string cityName = cmbCity.Text;

            var city = AppData.Context.City.Where(x=> x.Name == cityName).FirstOrDefault();

            var newAddress = AppData.Context.Address.Add(new Address
            {
                idCity = city.idCity,
                Street = tbAddress.Text,
                MailIndex = tbPostCode.Text
            });

            var newClientAddress = AppData.Context.ClientAddress.Add(new ClientAddress
            {
                idClient = AppData.current_user.idClient,
                idAddress = newAddress.idAddress,
                DateChange = DateTime.Now
            });

            AppData.Context.SaveChanges();

            MessageWindow mw = new MessageWindow("Данные сохранены");
            mw.ShowDialog();
            FrameNav.inner_profile_frame.Navigate(new Pages.AddressPage());
        }

        private void tbPostCode_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (e.Handled != "1234567890".IndexOf(e.Text) < 0)
            {
                e.Handled = true;
            }
        }
    }
}
