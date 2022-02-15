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

namespace Demo_Ekz.Pages
{
    /// <summary>
    /// Логика взаимодействия для Autorizasia.xaml
    /// </summary>
    public partial class Autorizasia : Page
    {
        public Autorizasia()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Parol.Password == "0000")
            {
                MessageBox.Show("Вы вошли как администратор");
                PagesChange.switchPage.Navigate(new ServiceAdmin());
            }
            else
            {
                MessageBox.Show("Неверный пароль");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            PagesChange.switchPage.Navigate(new ServicesUsers());
        }
    }
}