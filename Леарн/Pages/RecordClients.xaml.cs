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
    /// Логика взаимодействия для RecordClients.xaml
    /// </summary>
    public partial class RecordClients : Page
    {
        string DatesStart = null;
        private Entities.Service CurrentService = null;
        DateTime DateTimeRecording = DateTime.Now;
        public RecordClients(Entities.Service Service)
        {
            InitializeComponent();
            CBoxClient.ItemsSource = App.Context.Session_Client.ToList();
            DatesStart = DateTime.Now.ToShortDateString();
            var DateStart = DateTime.Parse(DatesStart);
            Date.DisplayDateStart = DateStart;
            CurrentService = Service;
            TBoxName.IsEnabled = false;
            TBoxDuration.IsEnabled = false;
            if (CurrentService != null)
            {
                TBoxName.Text = CurrentService.Title;
                TBoxDuration.Text = Convert.ToString(CurrentService.DurationInSeconds / 60);
            }
            
        }

        private void BtnRecord_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Date.SelectedDate != null)
                {
                    DateTimeRecording = Date.SelectedDate.GetValueOrDefault();
                    List<string> Time = TBoxDateTime.Text.Split(':').ToList();
                    if (Time.Count != 2)
                    {
                        MessageBox.Show("Неверное время");
                        return;
                    }
                    if (Int32.TryParse(Time[0], out int Hour) && !(Hour > 24) && !(Hour < 0) && Int32.TryParse(Time[1], out int Minute) && !(Minute > 60) && !(Minute < 00))
                    {
                        DateTimeRecording -= new TimeSpan(DateTimeRecording.Hour, DateTimeRecording.Minute, DateTimeRecording.Second);
                        DateTimeRecording += new TimeSpan(Hour, Minute, 0);
                    }
                    else
                    {
                        MessageBox.Show("Неверное время");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Выберите дату");
                    return;
                }
                if (CBoxClient.SelectedItem == null)
                {
                    MessageBox.Show("Выберите клиента");
                    return;
                }
                else
                {
                    Entities.ClientService ClientService = new Entities.ClientService
                    {
                        Session_Client = (Entities.Client)CBoxClient.SelectedItem,
                        Session_Service = CurrentService,
                        StartTime = DateTimeRecording
                    };
                    App.Context.Session_ClientService.Add(ClientService);
                    App.Context.SaveChanges();
                    MessageBox.Show("Клиент записан");
                }
            }
            catch
            {
                MessageBox.Show("Ошибка");
            }
        }

        private void Date_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void BtnUpcomingEntries_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pages.UpcomingEntries());
        }

        private void BtnService_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pages.ListOfServices(1));
        }
    }
}
