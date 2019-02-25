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
        private MainWindow fenetre_principale;

        public Option(MainWindow mainWindow)
        {
            fenetre_principale = mainWindow;
            InitializeComponent();
        }

        private void Option_dossier_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            dialog.ShowDialog();
            option_dossier.Text = dialog.SelectedPath;
        }

        private void Valider_Click(object sender, RoutedEventArgs e)
        {
            Brush brush_background = new SolidColorBrush(background_color.SelectedColor.Value);
            Brush brush_foreground = new SolidColorBrush(foreground_color.SelectedColor.Value);
            fenetre_principale.name_list.Background = brush_background;
            fenetre_principale.name_list.Foreground = brush_foreground;
            this.Close();

        }

        private void Annuler_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Appliquer_Click(object sender, RoutedEventArgs e)
        {
            Brush brush_background = new SolidColorBrush(background_color.SelectedColor.Value);
            Brush brush_foreground = new SolidColorBrush(foreground_color.SelectedColor.Value);
            fenetre_principale.name_list.Background = brush_background;
            fenetre_principale.name_list.Foreground = brush_foreground;
        }
    }
}
