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

namespace Demo_Ekz.Pages
{
    /// <summary>
    /// Логика взаимодействия для NewZapisxaml.xaml
    /// </summary>
    public partial class NewZapisxaml : Page
    {
        public Service servic;
        Service SC = new Service();
        Otobrazhenie viewModel = new Otobrazhenie();
        List<Service> ListUslug2;
        public NewZapisxaml(Service sr)
        {
            InitializeComponent();

            DataContext = sr;
            ComboClient.ItemsSource = DataBase.BaseModel.Clienti.ToList();
            ComboClient.SelectedValuePath = "idClients";
            ComboClient.DisplayMemberPath = "fullname";
            servic = sr;
        }
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            string date = DateTimeP.Text + " " + Time.Text;
            DateTime dated = Convert.ToDateTime(date);
            int idservice = servic.id_service;
            int check = 0;
            Regex r = new Regex("([0][8,9]|[1][0-9]|[2][0]):[0-5][0-9]");
            try
            {
                if (r.IsMatch(Time.Text))
                {
                    foreach (ServiceClient s in DataBase.BaseModel.ServiceClient.ToList())
                    {
                        if ((s.nachalo_okazania == dated && s.service == idservice) || (dated <= DateTime.Now) || (s.nachalo_okazania == dated && s.service == (int)ComboClient.SelectedValue))
                        {

                            check = 0;
                            break;
                        }
                        else
                        {
                            check++;

                        }
                    }


                }
                else
                {
                    MessageBox.Show("Введено не верное время.\n Часы работы школы с 8:00 до 20:00\nФормат записи должен быть следующим: 08:00", "Ошибка записи!", MessageBoxButton.OK, MessageBoxImage.Error);

                }

                if (check > 0)
                {
                    ServiceClient newServicec = new ServiceClient() { service = idservice, nachalo_okazania = Convert.ToDateTime(date), client = (int)ComboClient.SelectedValue };
                    DataBase.BaseModel.ServiceClient.Add(newServicec);
                    DataBase.BaseModel.SaveChanges();

                    MessageBox.Show("Запись добавлена");
                }
                else
                {
                    MessageBox.Show("Данное время не подходит для записи.");

                }
            }
            catch
            {
                MessageBox.Show("Произошла ошибка. Повторите попытку позже");
            }
                    }
        void TimeEnding()
        {
            try
            {
                TimeEnd.Text = (Convert.ToDateTime(Time.Text).AddMinutes(Convert.ToDouble(txtDuration.Text))).ToString("HH:mm");
            }
            catch
            {

            }
        }
        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            viewModel.service = viewModel.newserviceslist();
            PagesChange.switchPage.Navigate(new ServiceAdmin());
        }

        private void DateTimeP_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            TimeEnding();
        }

        private void Time_TextChanged(object sender, TextChangedEventArgs e)
        {
            TimeEnding();
        }
    }
}