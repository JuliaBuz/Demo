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
    /// Логика взаимодействия для ServiceChange.xaml
    /// </summary>
    public partial class ServiceChange : Page
    {
        public int ind, id = 0;
        BitmapImage BI;
        List<string> imgDirServ = new List<string>();
        Otobrazhenie predstav = new Otobrazhenie();
        Service ser;

        public ServiceChange(Service serv)
        {
            InitializeComponent();
            DataContext = serv;
            ser = serv;

            foreach (Service s in predstav.service)
            {
                imgDirServ.Add(s.izobrazhenie);
            }


            imgDirServ = imgDirServ.Distinct().ToList();
            BI = new BitmapImage(new Uri(imgDirServ[0], UriKind.Relative));
            Img.Source = BI;
        }

        private void ChangeService_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Service servic2 = DataBase.BaseModel.Service.FirstOrDefault(x => x.id_service == ser.id_service);
                string c, s;
                c = Cost.Text;
                s = Sale.Text;
                c = c.Replace(".", ",");
                s = s.Replace(".", ",");

                if (Convert.ToDouble(s) > 1)
                {
                    double a = Convert.ToDouble(s);
                    s = Convert.ToString(a / 100);
                }
                servic2.skidka = Convert.ToDouble(s);
                servic2.stoimost = Convert.ToDecimal(c);
                servic2.naimenovanie = txtName.Text;

                if ((Convert.ToInt32(Lasting.Text) < 0) || Convert.ToInt32(Lasting.Text) >= 240)
                {
                    throw new Exception("Лицам до 18 регистрация запрещена");
                }
                servic2.dlitelnost = Convert.ToInt32(Lasting.Text) * 60;
                servic2.izobrazhenie = imgDirServ[id];
                predstav.service = predstav.newserviceslist();
                DataBase.BaseModel.SaveChanges();
                PagesChange.switchPage.Navigate(new ServiceAdmin());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка", "Error");
            }

        }

        private void Last_Click(object sender, RoutedEventArgs e)
        {
            predstav.service = predstav.newserviceslist();
        }
        private void NewFoto(object sender, RoutedEventArgs e)
        {

            Button btn = (Button)sender;
            switch (btn.Name)
            {
                case "BtnNext":
                    if (id < imgDirServ.Count - 1)
                        id++;
                    else
                        id = 0;
                    if (id < imgDirServ.Count)
                    {
                        BI = new BitmapImage(new Uri(imgDirServ[id], UriKind.Relative));
                        Img.Source = BI;
                    }
                    break;
                case "BtnBack":
                    if (id != 0)
                        id--;
                    else
                        id = imgDirServ.Count - 1;
                    if (id >= 0)
                    {
                        BI = new BitmapImage(new Uri(imgDirServ[id], UriKind.Relative));
                        Img.Source = BI;
                    }
                    break;
            }
        }
    }
}

