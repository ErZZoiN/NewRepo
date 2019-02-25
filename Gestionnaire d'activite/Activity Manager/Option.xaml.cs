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
using System.Windows.Shapes;
using System.IO;
using Xceed.Wpf.Toolkit;

namespace Activity_Manager
{
    /// <summary>
    /// Logique d'interaction pour Option.xaml
    /// </summary>
    public partial class Option : Window
    {
        public Option(MainWindow mainWindow)
        {
            DateTimePicker t = new DateTimePicker();
            t.Height = 150;
            t.Width = 200;
            InitializeComponent();
            guk
        }

        private void Option_dossier_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void Valider_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Annuler_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Appliquer_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
