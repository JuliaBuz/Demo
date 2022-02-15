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
using System.Windows.Threading;

namespace Demo_Ekz.Pages
{
    /// <summary>
    /// Логика взаимодействия для Zapis.xaml
    /// </summary>
    public partial class Zapis : Page
    {
        Otobrazhenie VM = new Otobrazhenie();
        List<ServiceClient> sc;
        public Zapis()
        {
            InitializeComponent();
            sc = VM.createSC();
            lbEntry.ItemsSource = sc;
            tbTime.Text = DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second;
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(3);
            timer.Tick += timer_Tick;
            timer.Start();

        }
        void timer_Tick(object sender, EventArgs e)
        {
            sc = VM.createSC();
            lbEntry.ItemsSource = sc;
            tbTime.Text = DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second;
        }
    }
}
