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
    /// Логика взаимодействия для ListOfServices.xaml
    /// </summary>
    public partial class ListOfServices : Page
    {
       
        public ListOfServices( int TypePolzovatel)
        {
            InitializeComponent();
            CBSortByCost.SelectedIndex = 0;
            CBSortByDiscount.SelectedIndex = 0;
            TBoxMaxItems.Text = App.Context.Session_Service.Count().ToString();
            UpdateServices();
            if (TypePolzovatel == 0)
            {
                BtnAddService.Visibility = Visibility.Hidden;
                BtnUpcomingEntries.Visibility = Visibility.Hidden;
               
            }
            else
            {
                
                BtnAddService.Visibility = Visibility.Visible;
                BtnUpcomingEntries.Visibility = Visibility.Visible;
              
            }


        }

        private void LVServices_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
        }

        private void BtnDelService_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Entities.Service Services = (Entities.Service)LVServices.SelectedItem;
                if (Services != null)
                {
                    if (MessageBox.Show($"Вы уверены, что хотите удалить услугу: " +
                  $"{Services.Title}?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    {
                        try
                        {
                            App.Context.Session_Service.Remove(Services);
                            App.Context.SaveChanges();
                            MessageBox.Show("Удаление успешно");
                            UpdateServices();
                        }
                        catch
                        {
                            MessageBox.Show("Удалить услугу невозможно");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Выберите услугу которую вы хотите удалить");
                }
            }
            catch
            {
                MessageBox.Show("Ошибка");
            }
        }
        private void BtnEditService_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Entities.Service Services = (Entities.Service)LVServices.SelectedItem;
                if (Services != null)
                {
                    NavigationService.Navigate(new AddEditServicePage(Services));
                }
                else
                {
                    MessageBox.Show("Выберите услугу которую вы хотите редактировать");
                }
            }
            catch
            {
                MessageBox.Show("Ошибка");
            }
        }

        private void CBSortByCost_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateServices();
        }
       
        private void CBSortByDiscount_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateServices();
        }
        private void TBSerch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateServices();
        }
        private void UpdateServices()
        {
            IEnumerable<Entities.Service> Services = App.Context.Session_Service;

            if (CBSortByCost.SelectedIndex == 0) 
            {
                Services = Services.OrderBy(p => p.Cost);
            } 
            else Services = Services.OrderByDescending(p => p.Cost);

            if (CBSortByDiscount.SelectedIndex == 1)
            {
                Services = Services.Where(p => p.Discount >= 0 && p.Discount < 0.05);
            }
            if (CBSortByDiscount.SelectedIndex == 2)
            {
                Services = Services.Where(p => p.Discount >= 0.05 && p.Discount < 0.15);
            }
            if (CBSortByDiscount.SelectedIndex == 3)
            {
                Services = Services.Where(p => p.Discount >= 0.15 && p.Discount < 0.30);
            }
            if (CBSortByDiscount.SelectedIndex == 4)
            {
                Services = Services.Where(p => p.Discount >= 0.30 && p.Discount < 0.70);
            }
            if (CBSortByDiscount.SelectedIndex == 5)
            {
                Services = Services.Where(p => p.Discount >= 0.70 && p.Discount < 1);
            }
            Services = Services.Where(p => p.Title.ToLower().Contains(TBSerch.Text.ToLower()));

            LVServices.ItemsSource = Services.ToList();
            TBoxKolvoItems.Text = Services.Count().ToString();
        }

        private void BtnAddService_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddEditServicePage(null));
        }

        private void BtnRecordClients_Click(object sender, RoutedEventArgs e)
        {
            Entities.Service Services = (Entities.Service)LVServices.SelectedItem;
            if (Services != null)
            {
                NavigationService.Navigate(new RecordClients(Services));
            }
            else
            {
                MessageBox.Show("Выберите услугу для записи клиента");
            }
        }


        private void BtnUpcomingEntries_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new UpcomingEntries());
        }
    }
}
