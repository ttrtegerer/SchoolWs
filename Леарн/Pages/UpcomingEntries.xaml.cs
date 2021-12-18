using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Логика взаимодействия для UpcomingEntries.xaml
    /// </summary>
    public partial class UpcomingEntries : Page
    {
       
        public UpcomingEntries()
        {
            InitializeComponent();
            UpdateUpcomingEntries();
        }
           
        private void BtnService_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pages.ListOfServices(1));
        }
        private void UpdateUpcomingEntries()
        {
            var EndDyas = DateTime.Now;
            EndDyas = EndDyas.AddDays(2);
            LVUpcomingEntries.ItemsSource = App.Context.Session_ClientService.Where(p => p.StartTime > DateTime.Now && p.StartTime <= EndDyas).ToList();

        }
    }
}
