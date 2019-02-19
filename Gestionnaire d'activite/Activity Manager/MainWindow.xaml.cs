using LibrairieActivity;
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
using System.Data;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Activity_Manager
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Activity> liste_activite = null;

        public Activity b;

        public MainWindow()
        {
            b = new Activity
            {
                Intitule = "yoyo",
                Lieu = "Ailleurs",
                Description="La plus grosse teuf de Yoyo de ta life.",
                Nboccurence = 3,
                Periodicite1 = Activity.periodicite.mensuel,
                Debut = DateTime.Now,
                Fin = DateTime.Now
            };

            InitializeComponent();

            liste_activite = new ObservableCollection<Activity>();
            liste_activite.Add(b);
            main_panel.DataContext=liste_activite;
        }

        private void Bouton_create_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Menu_about_Click(object sender, RoutedEventArgs e)
        {
            DateTime ajd = new DateTime();
            ajd = DateTime.Now;
            MessageBox.Show("Clément Parmentier\nTOUS DROITS RESERVES !\n Date : " + ajd.ToShortDateString());
        }

        private void Main_panel_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void Main_panel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
