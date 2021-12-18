using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Леарн.Pages;

namespace Леарн.Entities
{
    public partial class Service
    {
        public string DiscountText
        {
            get
            {
                if (Discount == 0 || Discount == null)
                    return "";
                else return $"* скидка {Discount * 100}%";
            }
        }

        public string TotalCost
        {
            get
            {
                if (Discount == 0 || Discount == null)
                {
                    return $"{Cost:N2} рублей за {DurationInSeconds / 60} минут";
                }
                else
                {
                    return $"{CostWithDiscount:N2} рублей за {DurationInSeconds / 60} минут";
                }
            }
        }

        public double CostWithDiscount
        {
            get
            {
                if (Discount == 0 || Discount == null)
                {
                    return (double)Cost;
                }
                else
                {
                    var costWithDiscount = (double)Cost * (1.00 - Discount);
                    return costWithDiscount.Value;
                }
            }
        }

        public Visibility DiscountVisibility
        {
            get
            {
                if (Discount == 0 || Discount == null) return Visibility.Hidden;
                else return Visibility.Visible;
            }
        }

        public string BackColor
        {
            get
            {
                if (Discount == 0 || Discount == null) return "#FFFFFF";
                else return "#e7fabf";
            }
        }

        public string AdminControlsVisibility
        {
            get
            {
                if (App.CurrentClient==null) return "Visible";
                else return "Collapsed";

            }

        }
    }
       
  }