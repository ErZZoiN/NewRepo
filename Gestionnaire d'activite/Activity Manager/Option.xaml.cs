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
        private MainWindow _fenetre_principale;

        public Option(MainWindow mainWindow)
        {
            _fenetre_principale = mainWindow;
            InitializeComponent();
            option_dossier.Text = mainWindow.SaveLocation;
            background_color.SelectedColor = (mainWindow.name_list.Background as SolidColorBrush).Color;
            foreground_color.SelectedColor = (mainWindow.name_list.Foreground as SolidColorBrush).Color;
        }

        #region LISTENER

        private void Option_dossier_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            dialog.SelectedPath = option_dossier.Text;
            dialog.ShowDialog();
            option_dossier.Text = dialog.SelectedPath;
        }

        private void Valider_Click(object sender, RoutedEventArgs e)
        {
            Appliquer_Click(sender, e);

            this.Close();

        }

        private void Annuler_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Appliquer_Click(object sender, RoutedEventArgs e)
        {

            /*try
            {
                Brush brush_background = new SolidColorBrush(background_color.SelectedColor.Value);
                _fenetre_principale.name_list.Background = brush_background;
            }
            catch (InvalidOperationException) { }

            try
            {
                Brush brush_foreground = new SolidColorBrush(foreground_color.SelectedColor.Value);
                _fenetre_principale.name_list.Foreground = brush_foreground;
            }
            catch (InvalidOperationException) { }

            _fenetre_principale.SaveLocation = option_dossier.Text;*/

            var brush_background = new SolidColorBrush(background_color.SelectedColor.Value);
            var brush_foreground = new SolidColorBrush(foreground_color.SelectedColor.Value);

            Changement(brush_background, brush_foreground, option_dossier.Text);

        }
        #endregion

        public delegate void OptionChanged(Brush back, Brush front, string save);

        public event OptionChanged Changement;
    }
}
