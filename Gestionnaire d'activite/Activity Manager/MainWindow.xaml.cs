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

        public MainWindow()
        {
            b = new Activity
            {
                Intitule = "La vie de Clément",
                Lieu = "The world",
                Description = "La plus grosse teuf de Yoyo de ta life. lol.",
                Nboccurence = 3,
                Periodicite1 = Activity.periodicite.mensuel,
                Debut = new DateTime(1995, 8, 3),
                Fin = DateTime.Now,
            };

            InitializeComponent();

            liste_activite = new ObservableCollection<Activity>();
            liste_activite.Add(b);
            main_panel.DataContext=liste_activite;
        }

        #region LISTENER

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

                modify_event_periodicite.Text = Activity.PeriodiciteToString(current_activity.Periodicite1);

                modify_event_debut.SelectedDate = current_activity.Debut;
                modify_event_fin.SelectedDate = current_activity.Fin;

                modify_event_debut.DisplayDate = current_activity.Debut;
                modify_event_fin.DisplayDate = current_activity.Fin;
            }
        }

        #region TOOLBAR

        private void Bouton_create_Click(object sender, RoutedEventArgs e)
        {
            liste_activite.Add(new Activity
            {
                Intitule = modify_event_name.Text,
                Lieu = modify_event_lieu.Text,
                Description = modify_event_description.Text,
                Nboccurence = int.Parse(modify_event_occurence.Text),
                Periodicite1 = Activity.StringToPeriodicite(modify_event_periodicite.Text),
                Debut = (DateTime)modify_event_debut.SelectedDate,
                Fin = (DateTime)modify_event_fin.SelectedDate,
            });
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

                liste_activite[index].Periodicite1 = Activity.StringToPeriodicite(modify_event_periodicite.Text);

                liste_activite[index].Debut = (DateTime)modify_event_debut.SelectedDate;
                liste_activite[index].Fin = (DateTime)modify_event_fin.SelectedDate;

                main_panel.Items.Refresh();
            }
        }

        private void Bouton_delete_Click(object sender, RoutedEventArgs e)
        {
            if(main_panel.SelectedIndex>=0)
                liste_activite.RemoveAt(main_panel.SelectedIndex);
        }


        #endregion

        #endregion
    }
}
