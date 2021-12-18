using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Леарн
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Entities.Client CurrentClient = null;
        public static Entities.DbEntities Context
        { get; } = new Entities.DbEntities();
       
    }
}
