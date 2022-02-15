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
    /// Логика взаимодействия для ServiceAdmin.xaml
    /// </summary>
    public partial class ServiceAdmin : Page
    {
        Otobrazhenie otobraz = new Otobrazhenie();
        List<Service> filter;
        public ServiceAdmin()
        {
            InitializeComponent();
            filter = otobraz.service;
            Allservices.ItemsSource = otobraz.service;
        }

        private void delite_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button button = (Button)sender;
                int i = Convert.ToInt32(button.Uid);
                Service delite = DataBase.BaseModel.Service.FirstOrDefault(x => x.id_service == i);
                DataBase.BaseModel.Service.Remove(delite);
                DataBase.BaseModel.SaveChanges();
                Otobrazhenie viewmodel = new Otobrazhenie();
                Allservices.ItemsSource = viewmodel.service;
            }
            catch
            {
                MessageBox.Show("Запись связана. Ее удаление не может быть выполнено");
            }
        }

        private void chande_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            int id = Convert.ToInt32(btn.Uid);
            Service services = DataBase.BaseModel.Service.FirstOrDefault(x => x.id_service == id);
            PagesChange.switchPage.Navigate(new ServiceChange(services));
        }

        private void signup_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            int id = Convert.ToInt32(btn.Uid);
            Service services = DataBase.BaseModel.Service.FirstOrDefault(x => x.id_service == id);
            PagesChange.switchPage.Navigate(new NewZapisxaml(services));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PagesChange.switchPage.Navigate(new ServiceNewxaml());
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            PagesChange.switchPage.Navigate(new Zapis());
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            PagesChange.switchPage.Navigate(new ServicesUsers());
        }
        private void Filter(object sender, RoutedEventArgs e)
        {
            filter = otobraz.service;
            if (scan.Text != "")
            {
                filter = filter.Where(x => x.naimenovanie.Contains(scan.Text)).ToList();
            }
            if (up.IsChecked == true)
            {
                filter = filter.OrderBy(x => x.stoimost).ToList();
                Allservices.ItemsSource = filter.ToList();
            }
            if (down.IsChecked == true)
            {
                filter = filter.OrderBy(x => x.stoimost).ToList();
                filter.Reverse();
                Allservices.ItemsSource = filter.ToList();
            }
            switch (sizesale.SelectedIndex)
            {
                case 0:
                    filter = filter.Where(x => (x.skidka >= 0) && (x.skidka < 0.05)).ToList();
                    break;
                case 1:
                    filter = filter.Where(x => (x.skidka >= 0.05) && (x.skidka < 0.15)).ToList();
                    break;
                case 2:
                    filter = filter.Where(x => (x.skidka >= 15) && (x.skidka < 0.30)).ToList();
                    break;
                case 3:
                    filter = filter.Where(x => (x.skidka >= 30) && (x.skidka < 0.70)).ToList();
                    break;
                case 4:
                    filter = filter.Where(x => (x.skidka >= 70) && (x.skidka < 1)).ToList();
                    break;
                default:
                    filter = filter.Where(x => (x.skidka >= 0) && (x.skidka < 1)).ToList();
                    break;
            }
            Allservices.ItemsSource = filter.ToList();
        }
    }
}