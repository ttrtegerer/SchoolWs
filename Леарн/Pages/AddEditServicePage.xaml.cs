using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace Леарн.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddEditServicePage.xaml
    /// </summary>
    public partial class AddEditServicePage : Page
    {
        
        private Entities.Service CurrentService = null;
        public string PhotoSoures = "";
        public AddEditServicePage(Entities.Service Service)
        {
            InitializeComponent();
            CurrentService = Service;
            TBoxId.IsEnabled = false;
            if (CurrentService != null)
            {
                TBoxId.Text = Convert.ToString(CurrentService.ID);
                TBoxTitle.Text = CurrentService.Title;
                TBoxCost.Text = Convert.ToString(CurrentService.Cost);
                TBoxDescription.Text = CurrentService.Description;
                TBoxDiscount.Text = Convert.ToString(CurrentService.Discount * 100);
                TBoxDuration.Text = Convert.ToString(CurrentService.DurationInSeconds / 60);
                ImageService.Source = new BitmapImage(new Uri(CurrentService.MainImagePath, UriKind.Relative));
                PhotoSoures = CurrentService.MainImagePath.Substring(14);
            }
            else
            {
                TBoxId.Visibility = Visibility.Hidden;
                TBlockId.Visibility = Visibility.Hidden;
            }
            
        }

        private void BtnSelectImage_Click(object sender, RoutedEventArgs e)
        {
            try { 
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image |*.png; *.jpg; *.jpeg";
            if (ofd.ShowDialog() == true)
            {
                var _mainImageData = File.ReadAllBytes(ofd.FileName);
                ImageService.Source = (ImageSource)new ImageSourceConverter().ConvertFrom(_mainImageData);
                int IndexOfSubstring = ofd.FileName.IndexOf("Услуги школы") + 13;
                PhotoSoures = ofd.FileName.Substring(IndexOfSubstring);
                
            }
            }
            catch
            {
                MessageBox.Show("Ошибка");
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try { 
            if (CurrentService != null)
            {
                var ErrorMessage = ChekErrors();
                if (ErrorMessage.Length > 0)
                {
                    MessageBox.Show(ErrorMessage, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    if (CurrentService.Title.ToLower() != TBoxTitle.Text.ToLower())
                    {
                        var ProverkaTitle = App.Context.Session_Service.Where(p => p.Title.ToLower() == TBoxTitle.Text.ToLower()).ToList();
                        if (ProverkaTitle.Count > 0)
                        {
                            MessageBox.Show("Услуга с таким названием уже существует");
                        }
                        else
                        {
                            CurrentService.Title = TBoxTitle.Text;
                            CurrentService.Cost = Convert.ToInt32(TBoxCost.Text);
                            CurrentService.DurationInSeconds = Convert.ToInt32(TBoxDuration.Text)*60;
                            CurrentService.Description = TBoxDescription.Text;
                            CurrentService.Discount = Convert.ToInt32(TBoxDiscount.Text) / 100;
                            CurrentService.MainImagePath = "/Услуги школы/" + PhotoSoures;
                            App.Context.SaveChanges();
                            MessageBox.Show("Редактирование услуги успешно");
                            NavigationService.Navigate(new Pages.ListOfServices(1));

                        }
                    }
                    else
                    {
                        CurrentService.Title = TBoxTitle.Text;
                        CurrentService.Cost = Convert.ToInt32(TBoxCost.Text);
                        CurrentService.DurationInSeconds = Convert.ToInt32(TBoxDuration.Text) * 60;
                        CurrentService.Description = TBoxDescription.Text;
                        CurrentService.Discount = Convert.ToInt32(TBoxDiscount.Text) / 100;
                        CurrentService.MainImagePath = "/Услуги школы/" + PhotoSoures;
                        MessageBox.Show(PhotoSoures);
                        App.Context.SaveChanges();
                        MessageBox.Show("Редактирование услуги успешно");
                        NavigationService.Navigate(new Pages.ListOfServices(1));
                    }


                }

            }
            else
            {
                var ErrorMessage = ChekErrors();
                if (ErrorMessage.Length > 0)
                {
                    MessageBox.Show(ErrorMessage, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    var ProverkaTitle = App.Context.Session_Service.Where(p => p.Title.ToLower() == TBoxTitle.Text.ToLower()).ToList();
                    if (ProverkaTitle.Count > 0)
                    {
                        MessageBox.Show("Услуга с таким названием уже существует");
                    }
                    else
                    {
                        var Service = new Entities.Service
                        {
                            Title = TBoxTitle.Text,
                            Cost = Convert.ToInt32(TBoxCost.Text),
                            DurationInSeconds = Convert.ToInt32(TBoxDuration.Text) * 60,
                            Description = TBoxDescription.Text,
                            Discount = Convert.ToInt32(TBoxDiscount.Text) / 100,
                            MainImagePath = "/Услуги школы/" + PhotoSoures
                        };
                        App.Context.Session_Service.Add(Service);
                        App.Context.SaveChanges(); 
                        MessageBox.Show("Услуга успешно добавлена");
                        NavigationService.Navigate(new Pages.ListOfServices(1));
                    }
                }
            }
            }
            catch
            {
                MessageBox.Show("Ошибка");
            }

        }

         private string ChekErrors()
            {
            var errorBuilder = new StringBuilder();
            if(TBoxTitle.Text=="") errorBuilder.AppendLine("Поле название обязателно для заполнения;");
            if (TBoxCost.Text == "") errorBuilder.AppendLine("Поле стоимость обязателно для заполнения;");
            if (TBoxDuration.Text == "") errorBuilder.AppendLine("Поле длительность обязателно для заполнения;");
            if (TBoxDescription.Text == "") errorBuilder.AppendLine("Поле описание обязателно для заполнения;");
            if (TBoxDiscount.Text == "") errorBuilder.AppendLine("Поле скидка обязателно для заполнения;");
            if(!Regex.IsMatch(TBoxCost.Text, @"^\d+$")) errorBuilder.AppendLine("Поле стоимость должно быть числовым;");
            if (!Regex.IsMatch(TBoxDuration.Text, @"^\d+$")) errorBuilder.AppendLine("Поле длительность должно быть числовым;");
            if (!Regex.IsMatch(TBoxDiscount.Text, @"^\d+$")) errorBuilder.AppendLine("Поле скидка должно быть числовым;");
            if (Convert.ToInt32(TBoxCost.Text)<=0) errorBuilder.AppendLine("Cтоимость не может быть отрицательной или равной 0;");
            if (Convert.ToInt32(TBoxDuration.Text) <= 0) errorBuilder.AppendLine("Длительность не может быть отрицательной или равной 0;");
            if (Convert.ToInt32(TBoxDuration.Text) > 240) errorBuilder.AppendLine("Длительность не может быть больше чем 4 часа;");
            if (Convert.ToInt32(TBoxDiscount.Text) < 0) errorBuilder.AppendLine("Скидка не может быть отрицательной;");
            if (Convert.ToInt32(TBoxDiscount.Text) > 100) errorBuilder.AppendLine("Скидка не может быть больше чем 100%;");
            return errorBuilder.ToString();
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
