using skeleton_market.Classes;
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
using skeleton_market.Windows;
using HandyControl.Controls;

namespace skeleton_market.Pages
{
    /// <summary>
    /// Логика взаимодействия для AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        private Page prev_page = new Page();

        public AuthPage()
        {
            InitializeComponent();

            prev_page = null;
            dpBirth_reg.DisplayDateEnd = DateTime.Now;
            cmbGender_reg.ItemsSource = AppData.Context.Gender.ToList();
            cmbGender_reg.DisplayMemberPath = "Name";
        }
        public AuthPage(Page prev_page_s)
        {
            InitializeComponent();
            
            prev_page = prev_page_s;
            dpBirth_reg.DisplayDateEnd = DateTime.Now;
            cmbGender_reg.ItemsSource = AppData.Context.Gender.ToList();
            cmbGender_reg.DisplayMemberPath = "Name";
           
        }

        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            FrameNav.current_frame.Navigate(new Pages.MainPage());
        }

        private void btnReg_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrWhiteSpace(tbName_reg.Text))
            {
                MessageWindow msg = new MessageWindow("Введите имя");
                msg.ShowDialog();
                return;
            }
            if (string.IsNullOrWhiteSpace(tbLastName_reg.Text))
            {
                MessageWindow msg = new MessageWindow("Введите фамилию");
                msg.ShowDialog();
                return;
            }
            if (string.IsNullOrWhiteSpace(tbEmail_reg.Text))
            {
                MessageWindow msg = new MessageWindow("Введите email");
                msg.ShowDialog();
                return;
            }

            string email = tbEmail_reg.Text;
            string emailPattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
            bool emailCheck = Regex.IsMatch(email, emailPattern);

            if (emailCheck == false)
            {
                MessageWindow msg = new MessageWindow("Неверный формат Email");
                msg.ShowDialog();
                return;
            }

            var email_exist_check = AppData.Context.Client.Where(x => x.Email == email).ToList().FirstOrDefault();
            if (email_exist_check != null)
            {
                MessageWindow msg = new MessageWindow("Такой пользователь уже есть");
                msg.ShowDialog();
                return;
            }

            if (string.IsNullOrWhiteSpace(tbPass_reg.Password))
            {
                MessageWindow msg = new MessageWindow("Введите пароль");
                msg.ShowDialog();
                return;
            }
            if (dpBirth_reg.SelectedDate == null)
            {
                MessageWindow msg = new MessageWindow("Выберите дату рождения");
                msg.ShowDialog();
                return;
            }
            DateTime birth_date = dpBirth_reg.SelectedDate.Value;
            double age =Math.Round((DateTime.Now - birth_date).TotalDays / 365.25);
            if(age < 16)
            {
                MessageWindow msgwx = new MessageWindow("Вам должно быть больше 16 лет");
                msgwx.ShowDialog();
                return;
            }

            if (cmbGender_reg.SelectedIndex == -1)
            {
                MessageWindow msg = new MessageWindow("Выберите пол");
                msg.ShowDialog();
                return;
            }
            string patronymic = "";
            if(string.IsNullOrEmpty(tbPatr_reg.Text))
            {
                patronymic = null;
            }
            else
            {
                patronymic= tbPatr_reg.Text;
            }

           

            AppData.Context.Client.Add(new EF.Client 
            {
                FirstName = tbName_reg.Text,
                LastName = tbLastName_reg.Text,
                Email = tbEmail_reg.Text,
                Patronymic = patronymic,
                idGender = cmbGender_reg.SelectedIndex + 1,
                Password = tbPass_reg.Password,
                BirthDay = dpBirth_reg.SelectedDate.Value,
            });
            AppData.Context.SaveChanges();
            MessageWindow msgw = new MessageWindow("Регистрация завершена!");
            msgw.ShowDialog();

            FrameNav.current_frame.Navigate(new Pages.AuthPage());

        }

        private void tbName_reg_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (e.Handled != "йцукенгшщзхъфывапролджэячсмитьбюЙЦУК-ЕНГШЩЗХЪФЫВАПРОЛДЖЭЯЧСМИТЬБЮ".IndexOf(e.Text) < 0)
            {
                e.Handled = true;
            }
        }

        private void tbName_reg_PreviewExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (
      e.Command == ApplicationCommands.Cut ||
      e.Command == ApplicationCommands.Paste)
            {
                e.Handled = true;
            }
        }

        private void tbLastName_reg_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (e.Handled != "йцукенгшщзхъфывапролджэячсмитьбюЙЦУКЕНГШЩЗХЪФЫВАПРОЛД- ЖЭЯЧСМИТЬБЮ".IndexOf(e.Text) < 0)
            {
                e.Handled = true;
            }
        }

        private void tbLastName_reg_PreviewExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (
     e.Command == ApplicationCommands.Cut ||
     e.Command == ApplicationCommands.Paste)
            {
                e.Handled = true;
            }
        }

        private void tbPatr_reg_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (e.Handled != "йцукенгшщзхъфывапролджэячсмитьбюЙЦУКЕНГШЩЗХЪФЫВАПРОЛД-ЖЭЯЧСМИТЬБЮ".IndexOf(e.Text) < 0)
            {
                e.Handled = true;
            }
        }

        private void tbPatr_reg_PreviewExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (
     e.Command == ApplicationCommands.Cut ||
     e.Command == ApplicationCommands.Paste)
            {
                e.Handled = true;
            }
        }

        private void tbEmail_reg_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (e.Handled != "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM1234567890-_@.".IndexOf(e.Text) < 0)
            {
                e.Handled = true;
            }
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if(prev_page != null) 
            {
                string prev_p_title = prev_page.Title;

                if (string.IsNullOrWhiteSpace(tbEmail.Text))
                {
                    MessageWindow msg = new MessageWindow("Введите email");
                    msg.ShowDialog();
                    return;
                }
                if (string.IsNullOrWhiteSpace(tbPassword.Password))
                {
                    MessageWindow msg = new MessageWindow("Введите пароль");
                    msg.ShowDialog();
                    return;
                }

                var user_check = AppData.Context.Client.Where(x => x.Email == tbEmail.Text && x.Password == tbPassword.Password).ToList().FirstOrDefault();
                if (user_check != null)
                {
                    MessageWindow msg = new MessageWindow("Вход выполнен");
                    msg.ShowDialog();

                    AppData.current_user = user_check;
                    FrameNav.current_frame.Navigate(new Uri($"/Pages/{prev_p_title}.xaml", UriKind.RelativeOrAbsolute));

                }
                else
                {
                    MessageWindow msg = new MessageWindow("Неверные данные");
                    msg.ShowDialog();
                    return;
                }
            }
            else
            {
                

                if (string.IsNullOrWhiteSpace(tbEmail.Text))
                {
                    MessageWindow msg = new MessageWindow("Введите email");
                    msg.ShowDialog();
                    return;
                }
                if (string.IsNullOrWhiteSpace(tbPassword.Password))
                {
                    MessageWindow msg = new MessageWindow("Введите пароль");
                    msg.ShowDialog();
                    return;
                }

                var user_check = AppData.Context.Client.Where(x => x.Email == tbEmail.Text && x.Password == tbPassword.Password).ToList().FirstOrDefault();
                if (user_check != null)
                {
                    MessageWindow msg = new MessageWindow("Вход выполнен");
                    msg.ShowDialog();

                    AppData.current_user = user_check;
                    FrameNav.current_frame.Navigate(new Pages.MainPage());

                }
                else
                {
                    MessageWindow msg = new MessageWindow("Неверные данные");
                    msg.ShowDialog();
                    return;
                }
            }
           


        }
    }
}
