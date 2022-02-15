using Microsoft.Win32;
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
    /// Логика взаимодействия для ServiceNewxaml.xaml
    /// </summary>
    public partial class ServiceNewxaml : Page
    {
        string img = "\\null\\null";
        Service bufferservices;
        Otobrazhenie viewmodel = new Otobrazhenie();
        public ServiceNewxaml()
        {
            InitializeComponent();
        }

        private void Last_Click(object sender, RoutedEventArgs e)
        {
            viewmodel.service = viewmodel.newserviceslist();
            PagesChange.switchPage.Navigate(new ServiceAdmin());
        }

        private void Photo_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.DefaultExt = ".jpg"; 
            openFileDialog.Filter = "Изображения |*.jpg;*.png";
            var result = openFileDialog.ShowDialog();

            if (result == true)
            {

                img = openFileDialog.FileName;
                ImageBox.Source = BitmapFrame.Create(new Uri(img));
            }
        }

        private void NewService_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string c, s;
                c = Cost.Text;
                s = Sale.Text;
                c = c.Replace(".", ",");
                s = s.Replace(".", ",");
                if (Sale.Text == "")
                {
                    Sale.Text = "0";
                }
                if (Convert.ToDouble(s) > 1)
                {
                    double a = Convert.ToDouble(s);
                    s = Convert.ToString(a / 100);
                }
                if (Sale.Text == "")
                {

                }
                if ((Convert.ToInt32(Lasting.Text) < 0) || Convert.ToInt32(Lasting.Text) >= 240)
                {
                    throw new Exception("Лицам до 18 регистрация запрещена");
                }
                Service newService = new Service() { naimenovanie = txtName.Text, dlitelnost = Convert.ToInt32(Lasting.Text) * 60, skidka = Convert.ToDouble(s), stoimost = Convert.ToDecimal(Cost.Text), izobrazhenie = img };
                DataBase.BaseModel.Service.Add(newService);
                DataBase.BaseModel.SaveChanges();
                viewmodel.service = viewmodel.newserviceslist();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка", "Error");
            }
        }
    }
}
