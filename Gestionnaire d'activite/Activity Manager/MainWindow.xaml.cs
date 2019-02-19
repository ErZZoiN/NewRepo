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

        Activity current_activity = null;

        int selected_item = -1;

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

        private void Main_panel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (main_panel.SelectedItem!=null)
            {
                current_activity = (Activity)main_panel.SelectedItem;
                modify_event_description.Text = current_activity.Description;
                modify_event_lieu.Text = current_activity.Lieu;
                modify_event_name.Text = current_activity.Intitule;
                modify_event_occurence.Text = current_activity.Nboccurence.ToString();
                modify_event_periodicite.Text = current_activity.Periodicite1.ToString();
            }

            refresh();
        }

        private void Bouton_modify_Click(object sender, RoutedEventArgs e)
        {
            current_activity = (Activity)main_panel.SelectedItem;
            if (main_panel.SelectedItem!=null)
            {
                int index = main_panel.SelectedIndex;
                liste_activite[index].Description = modify_event_description.Text;
                liste_activite[index].Lieu = modify_event_lieu.Text;
                liste_activite[index].Intitule = modify_event_name.Text;
                liste_activite[index].Nboccurence = int.Parse(modify_event_occurence.Text);

                switch (modify_event_periodicite.Text)
                {
                    case "Annuel":liste_activite[index].Periodicite1 = Activity.periodicite.annuel;
                        break;
                    case "Mensuel":
                        liste_activite[index].Periodicite1 = Activity.periodicite.mensuel;
                        break;
                    case "Hebdomadaire":
                        liste_activite[index].Periodicite1 = Activity.periodicite.hebdomadaire;
                        break;
                    case "Quotidien":
                        liste_activite[index].Periodicite1 = Activity.periodicite.quotidien;
                        break;
                }

                main_panel.Items.Refresh();
            }
        }
    }
}
