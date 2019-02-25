﻿using LibrairieActivity;
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

namespace Activity_Manager
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region VARIABLE

        public ObservableCollection<Activity> liste_activite = null;
        public Activity b;
        private Activity current_activity = null;
        private bool modify_flag = false;
        public DirectoryInfo save_location=null;

        #endregion

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
            name_list.DataContext=liste_activite;

            for(int i=0;i<24;i++)
            {
                modify_event_debut_heure.Items.Add(i);
                modify_event_fin_heure.Items.Add(i);
            }

            for (int i = 0; i < 60; i++)
            {
                modify_event_debut_minute.Items.Add(i);
                modify_event_fin_minute.Items.Add(i);
            }

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
            modify_flag = false;
            modify_event_title.Content = "Création d'un événement";
            modify_event.Visibility = Visibility.Visible;
        }


        private void Bouton_modify_Click(object sender, RoutedEventArgs e)
        {
            if (main_panel.SelectedItem != null)
            {
                modify_flag = true;
                modify_event_title.Content = "Modification d'un événement";
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

                modify_event_debut_heure.SelectedIndex = current_activity.Debut.Hour;
                modify_event_fin_heure.SelectedIndex = current_activity.Fin.Hour;
                modify_event_debut_minute.SelectedIndex = current_activity.Debut.Minute;
                modify_event_fin_minute.SelectedIndex = current_activity.Fin.Minute;
            }
            else
                modify_event.Visibility = Visibility.Collapsed;
        }

        private void Bouton_delete_Click(object sender, RoutedEventArgs e)
        {
            if(main_panel.SelectedIndex>=0)
                liste_activite.RemoveAt(main_panel.SelectedIndex);
        }


        #endregion

        #region MODIFY

        private void Modify_event_cancel_Click(object sender, RoutedEventArgs e)
        {
            modify_event.Visibility = Visibility.Collapsed;
            modify_event_description.Text = "";
            modify_event_lieu.Text = "";
            modify_event_name.Text = "";
            modify_event_occurence.Text = "";
            modify_event_periodicite.SelectedIndex = 0;
            modify_event_debut.SelectedDate = DateTime.Now;
            modify_event_fin.SelectedDate = DateTime.Now;

            modify_event_fin_heure.SelectedIndex = 0;
            modify_event_debut_heure.SelectedIndex = 0;
            modify_event_fin_minute.SelectedIndex = 0;
            modify_event_debut_minute.SelectedIndex = 0;
        }

        private void Modify_event_valider_Click(object sender, RoutedEventArgs e)
        {
            DateTime tmp_debut = (DateTime)modify_event_debut.SelectedDate;
            TimeSpan s = new TimeSpan(int.Parse(modify_event_debut_heure.SelectedItem.ToString()), int.Parse(modify_event_debut_minute.SelectedItem.ToString()), 0);
            tmp_debut = tmp_debut.Date + s;

            DateTime tmp_fin = (DateTime)modify_event_fin.SelectedDate;
            TimeSpan s2 = new TimeSpan(int.Parse(modify_event_fin_heure.SelectedItem.ToString()), int.Parse(modify_event_fin_minute.SelectedItem.ToString()), 0);
            tmp_fin = tmp_fin.Date + s2;

            current_activity = (Activity)main_panel.SelectedItem;
            if ((main_panel.SelectedItem != null) && modify_flag)
            {
                int index = main_panel.SelectedIndex;
                liste_activite[index].Description = modify_event_description.Text;
                liste_activite[index].Lieu = modify_event_lieu.Text;
                liste_activite[index].Intitule = modify_event_name.Text;
                liste_activite[index].Nboccurence = int.Parse(modify_event_occurence.Text);

                liste_activite[index].Periodicite1 = Activity.StringToPeriodicite(modify_event_periodicite.Text);

                liste_activite[index].Debut = tmp_debut;
                liste_activite[index].Fin = tmp_fin;

                main_panel.Items.Refresh();
            }
            else if(!modify_flag)
            {
                liste_activite.Add(new Activity
                {
                    Intitule = modify_event_name.Text,
                    Lieu = modify_event_lieu.Text,
                    Description = modify_event_description.Text,
                    Nboccurence = int.Parse(modify_event_occurence.Text),
                    Periodicite1 = Activity.StringToPeriodicite(modify_event_periodicite.Text),
                    Debut = tmp_debut,
                    Fin = tmp_fin,
                });
            }

            modify_event_description.Text = "";
            modify_event_lieu.Text = "";
            modify_event_name.Text = "";
            modify_event_occurence.Text = "";
            modify_event_periodicite.SelectedIndex = 0;
            modify_event_debut.SelectedDate = DateTime.Now;
            modify_event_fin.SelectedDate = DateTime.Now;
            modify_event_fin_heure.SelectedIndex = 0;
            modify_event_debut_heure.SelectedIndex = 0;
            modify_event_fin_minute.SelectedIndex = 0;
            modify_event_debut_minute.SelectedIndex = 0;

            modify_event.Visibility = Visibility.Collapsed;
        }

        private void Modify_event_apply_Click(object sender, RoutedEventArgs e)
        {
            DateTime tmp_debut = (DateTime)modify_event_debut.SelectedDate;
            TimeSpan s = new TimeSpan(int.Parse(modify_event_debut_heure.SelectedItem.ToString()), int.Parse(modify_event_debut_minute.SelectedItem.ToString()), 0);
            tmp_debut = tmp_debut.Date + s;

            DateTime tmp_fin = (DateTime)modify_event_fin.SelectedDate;
            TimeSpan s2 = new TimeSpan(int.Parse(modify_event_fin_heure.SelectedItem.ToString()), int.Parse(modify_event_fin_minute.SelectedItem.ToString()), 0);
            tmp_fin = tmp_fin.Date + s2;

            current_activity = (Activity)main_panel.SelectedItem;
            if ((main_panel.SelectedItem != null) && modify_flag)
            {
                int index = main_panel.SelectedIndex;
                liste_activite[index].Description = modify_event_description.Text;
                liste_activite[index].Lieu = modify_event_lieu.Text;
                liste_activite[index].Intitule = modify_event_name.Text;
                liste_activite[index].Nboccurence = int.Parse(modify_event_occurence.Text);

                liste_activite[index].Periodicite1 = Activity.StringToPeriodicite(modify_event_periodicite.Text);

                liste_activite[index].Debut = tmp_debut;
                liste_activite[index].Fin = tmp_fin;

                main_panel.Items.Refresh();
            }
            else if (!modify_flag)
            {
                liste_activite.Add(new Activity
                {
                    Intitule = modify_event_name.Text,
                    Lieu = modify_event_lieu.Text,
                    Description = modify_event_description.Text,
                    Nboccurence = int.Parse(modify_event_occurence.Text),
                    Periodicite1 = Activity.StringToPeriodicite(modify_event_periodicite.Text),
                    Debut = tmp_debut,
                    Fin = tmp_fin,
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

        #endregion
    }
}
