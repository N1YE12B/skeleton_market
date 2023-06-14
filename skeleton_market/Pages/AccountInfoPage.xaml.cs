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
using skeleton_market.Classes;
using skeleton_market.Windows;
using System.Text.RegularExpressions;

namespace skeleton_market.Pages
{
    /// <summary>
    /// Логика взаимодействия для AccountInfoPage.xaml
    /// </summary>
    public partial class AccountInfoPage : Page
    {
        public AccountInfoPage()
        {
            InitializeComponent();
            EF.Client client = AppData.current_user;
            tbEmail.Text = client.Email.ToString();
            tbFirstName.Text = client.FirstName.ToString();
            tbLastName.Text = client.LastName.ToString();
            try
            {
                tbPatronymic.Text = client.Patronymic;
            }
            catch (Exception)
            {

                tbPatronymic = null;
            }
           
            txtClientName.Text = "Здравствуйте, " + client.LastName + " " + client.FirstName + " " + tbPatronymic.Text + " !";
           
        }

        private void btnAddress_Click(object sender, RoutedEventArgs e)
        {
            FrameNav.inner_profile_frame.Navigate(new Pages.AddressPage());
        }

        private void btnSaveChange_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrWhiteSpace(tbEmail.Text))
            {
                MessageWindow mw3 = new MessageWindow("Неверный формат Email");
                mw3.ShowDialog();
                return;
            }

            string email = tbEmail.Text;
            string emailPattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
            bool emailCheck = Regex.IsMatch(email, emailPattern);

            if (emailCheck == false)
            {
                MessageWindow msg = new MessageWindow("Неверный формат Email");
                msg.ShowDialog();
                return;
            }


            var email_exist_check = AppData.Context.Client.Where(x => x.Email == email && x.idClient != AppData.current_user.idClient).ToList().FirstOrDefault();
            if (email_exist_check != null)
            {
                MessageWindow msg = new MessageWindow("Этот Email уже используется");
                msg.ShowDialog();
                tbEmail.Text = AppData.current_user.Email;
                return;
            }

            if (string.IsNullOrWhiteSpace(tbFirstName.Text))
            {
                MessageWindow mw1 = new MessageWindow("Введите имя");
                mw1.ShowDialog();
                return;
            }
            if (string.IsNullOrWhiteSpace(tbLastName.Text))
            {
                MessageWindow mw2 = new MessageWindow("Введите фамилию");
                mw2.ShowDialog();
                return;
            }

            if (string.IsNullOrWhiteSpace(tbPatronymic.Text))
            {
                tbPatronymic.Text = null;
            }

            AppData.current_user.FirstName = tbFirstName.Text;
            AppData.current_user.LastName = tbLastName.Text;
            AppData.current_user.Patronymic = tbPatronymic.Text;
            AppData.current_user.Email = tbEmail.Text;

            AppData.Context.SaveChanges();

            MessageWindow mw = new MessageWindow("Данные сохранены");
            mw.ShowDialog();
            FrameNav.inner_profile_frame.Navigate(new Pages.AccountInfoPage());
        }

        private void tbFirstName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (e.Handled != "йцукенгшщзхъфывапролджэячсмитьбюЙЦУК-ЕНГШЩЗХЪФЫВАПРОЛДЖЭЯЧСМИТЬБЮ".IndexOf(e.Text) < 0)
            {
                e.Handled = true;
            }
        }

        private void tbLastName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (e.Handled != "йцукенгшщзхъфывапролджэячсмитьбюЙЦУК-ЕНГШЩЗХЪФЫВАПРОЛДЖЭЯЧСМИТЬБЮ".IndexOf(e.Text) < 0)
            {
                e.Handled = true;
            }
        }

        private void tbPatronymic_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (e.Handled != "йцукенгшщзхъфывапролджэячсмитьбюЙЦУК-ЕНГШЩЗХЪФЫВАПРОЛДЖЭЯЧСМИТЬБЮ".IndexOf(e.Text) < 0)
            {
                e.Handled = true;
            }
        }
    }
}
