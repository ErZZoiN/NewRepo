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
using System.IO;
using Xceed.Wpf.Toolkit;
using Microsoft.Win32;

namespace Activity_Manager
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Pour sauvegarder le dossier de sauvegarde d'une session à l'autre,
        // on enregistre dans un fichier le chemin du dossier de sauvegarde.
        // Celui-ci sera chargé au démarrage du programme, et enregistré à sa fermeture.
        static string saving = Directory.GetCurrentDirectory() + "pref.dat";

        #region VARIABLE
        private ObservableCollection<Activity> _liste_activite = null;
        private Activity b;
        private Activity _current_activity = null;
        private bool _modify_flag = false;
        private string _save_location = "";

        #endregion

        #region PROPRIETE

        public String SaveLocation
        {
            get { return _save_location; }
            set { _save_location = value; }
        }

        #endregion

        public MainWindow()
        {
            SaveLocation = System.IO.Directory.GetCurrentDirectory();
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

            try
            {
                byte[] bytes = File.ReadAllBytes(saving);
                SaveLocation = Encoding.ASCII.GetString(bytes);
            }
            catch(FileNotFoundException)
            {
                SaveLocation = Directory.GetCurrentDirectory();
            }

            _liste_activite = new ObservableCollection<Activity>();
            _liste_activite.Add(b);
            main_panel.DataContext=_liste_activite;
            name_list.DataContext=_liste_activite;

            searchbar.Foreground = Brushes.Gray;
            searchbar.Text = "Recherche par lieu";
            searchbar.GotKeyboardFocus += new KeyboardFocusChangedEventHandler(searchbar_GotKeyboardFocus);
            searchbar.LostKeyboardFocus += new KeyboardFocusChangedEventHandler(searchbar_LostKeyboardFocus);
        }

        #region LISTENER

        #region MENU
        private void Menu_about_Click(object sender, RoutedEventArgs e)
        {
            DateTime ajd = new DateTime();
            ajd = DateTime.Now;
            System.Windows.MessageBox.Show("Clément Parmentier\nTOUS DROITS RESERVES !\n Date : " + ajd.ToShortDateString());
        }

        private void Menu_option_Click(object sender, RoutedEventArgs e)
        {
            Option win2 = new Option(this);
            win2.Show();
        }

        private void Menu_exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Menu_save_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = SaveLocation;
            saveFileDialog.ShowDialog();
        }

        #endregion

        #region MAIN GRID
        private void Main_panel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
         /*   if (main_panel.SelectedItem!=null)
            {
                modify_event.Visibility = Visibility.Visible;
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
            else
                modify_event.Visibility = Visibility.Collapsed;*/
        }
        #endregion

        #region TOOLBAR

        private void Bouton_create_Click(object sender, RoutedEventArgs e)
        {
            _modify_flag = false;
            modify_event_title.Content = "Création d'un événement";
            modify_event.Visibility = Visibility.Visible;
        }


        private void Bouton_modify_Click(object sender, RoutedEventArgs e)
        {
            if (main_panel.SelectedItem != null)
            {
                _modify_flag = true;
                modify_event_title.Content = "Modification d'un événement";
                modify_event.Visibility = Visibility.Visible;
                _current_activity = (Activity)main_panel.SelectedItem;
                modify_event_description.Text = _current_activity.Description;
                modify_event_lieu.Text = _current_activity.Lieu;
                modify_event_name.Text = _current_activity.Intitule;
                modify_event_occurence.Value = _current_activity.Nboccurence;
                modify_event_periodicite.Text = _current_activity.Periodicite1.ToString();

                modify_event_periodicite.Text = Activity.PeriodiciteToString(_current_activity.Periodicite1);

                modify_event_debut.Value = _current_activity.Debut;
                modify_event_fin.Value = _current_activity.Fin;

                modify_event_debut.Value = _current_activity.Debut;
                modify_event_fin.Value = _current_activity.Fin;
            }
            else
                modify_event.Visibility = Visibility.Collapsed;
        }

        private void Bouton_delete_Click(object sender, RoutedEventArgs e)
        {
            if(main_panel.SelectedIndex>=0)
                _liste_activite.RemoveAt(main_panel.SelectedIndex);
        }


        #endregion

        #region MODIFY

        private void Modify_event_cancel_Click(object sender, RoutedEventArgs e)
        {
            modify_event.Visibility = Visibility.Collapsed;
            modify_event_description.Text = "";
            modify_event_lieu.Text = "";
            modify_event_name.Text = "";
            modify_event_occurence.Value = 1;
            modify_event_periodicite.SelectedIndex = 0;
            modify_event_debut.Value = DateTime.Now;
            modify_event_fin.Value = DateTime.Now;
        }

        private void Modify_event_valider_Click(object sender, RoutedEventArgs e)
        {

            _current_activity = (Activity)main_panel.SelectedItem;
            if ((main_panel.SelectedItem != null) && _modify_flag)
            {
                int index = main_panel.SelectedIndex;
                _liste_activite[index].Description = modify_event_description.Text;
                _liste_activite[index].Lieu = modify_event_lieu.Text;
                _liste_activite[index].Intitule = modify_event_name.Text;
                _liste_activite[index].Nboccurence = (int)modify_event_occurence.Value;

                _liste_activite[index].Periodicite1 = Activity.StringToPeriodicite(modify_event_periodicite.Text);

                _liste_activite[index].Debut = (DateTime)modify_event_debut.Value;
                _liste_activite[index].Fin = (DateTime)modify_event_fin.Value;

                main_panel.Items.Refresh();
            }
            else if(!_modify_flag)
            {
                _liste_activite.Add(new Activity
                {
                    Intitule = modify_event_name.Text,
                    Lieu = modify_event_lieu.Text,
                    Description = modify_event_description.Text,
                    Nboccurence = (int)modify_event_occurence.Value,
                    Periodicite1 = Activity.StringToPeriodicite(modify_event_periodicite.Text),
                    Debut = (DateTime)modify_event_debut.Value,
                    Fin = (DateTime)modify_event_fin.Value,
                });
            }

            modify_event_description.Text = "";
            modify_event_lieu.Text = "";
            modify_event_name.Text = "";
            modify_event_occurence.Value = 1;
            modify_event_periodicite.SelectedIndex = 0;
            modify_event_debut.Value = DateTime.Now;
            modify_event_fin.Value = DateTime.Now;

            modify_event.Visibility = Visibility.Collapsed;
        }

        private void Modify_event_apply_Click(object sender, RoutedEventArgs e)
        {

            _current_activity = (Activity)main_panel.SelectedItem;
            if ((main_panel.SelectedItem != null) && _modify_flag)
            {
                int index = main_panel.SelectedIndex;
                _liste_activite[index].Description = modify_event_description.Text;
                _liste_activite[index].Lieu = modify_event_lieu.Text;
                _liste_activite[index].Intitule = modify_event_name.Text;
                _liste_activite[index].Nboccurence = (int)modify_event_occurence.Value;

                _liste_activite[index].Periodicite1 = Activity.StringToPeriodicite(modify_event_periodicite.Text);

                _liste_activite[index].Debut = (DateTime)modify_event_debut.Value;
                _liste_activite[index].Fin = (DateTime)modify_event_fin.Value;

                main_panel.Items.Refresh();
            }
            else if (!_modify_flag)
            {
                _liste_activite.Add(new Activity
                {
                    Intitule = modify_event_name.Text,
                    Lieu = modify_event_lieu.Text,
                    Description = modify_event_description.Text,
                    Nboccurence = (int)modify_event_occurence.Value,
                    Periodicite1 = Activity.StringToPeriodicite(modify_event_periodicite.Text),
                    Debut = (DateTime)modify_event_debut.Value,
                    Fin = (DateTime)modify_event_fin.Value,
                });
            }
        }

        #endregion

        #region SEARCHBAR
        private void searchbar_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (sender is TextBox)
            {
                //If nothing has been entered yet.
                if (((TextBox)sender).Foreground == Brushes.Gray)
                {
                    ((TextBox)sender).Text = "";
                    ((TextBox)sender).Foreground = Brushes.Black;
                }
            }
        }


        private void searchbar_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            //Make sure sender is the correct Control.
            if (sender is TextBox)
            {
                //If nothing was entered, reset default text.
                if (((TextBox)sender).Text.Trim().Equals(""))
                {
                    ((TextBox)sender).Foreground = Brushes.Gray;
                    ((TextBox)sender).Text = "Recherche par lieu";
                }
            }
        }
        #endregion

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(SaveLocation);

            FileStream fd = File.Open(saving, FileMode.Create, FileAccess.ReadWrite);
            fd.Write(bytes, 0, SaveLocation.Length);
            fd.Close();
        }

        #endregion
    }
}
