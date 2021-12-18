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

namespace Леарн.Pages
{
    /// <summary>
    /// Логика взаимодействия для Vhod.xaml
    /// </summary>
    public partial class Vhod : Page
    {
        public Vhod()
        {
            InitializeComponent();
        }

        private void BtnVhod_Click(object sender, RoutedEventArgs e)
        {
            if(TBoxPassword.Text == "0000")
            {
                NavigationService.Navigate(new Pages.ListOfServices(1));
                App.CurrentClient = null;

            }
            else
            {
                NavigationService.Navigate(new Pages.ListOfServices(0));
                App.CurrentClient = App.Context.Session_Client.FirstOrDefault();
            }
        }
    }
}
