using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
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

namespace CorrectionSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {

            InitializeComponent();



            /***********************************************************/
            /********************** Open Database **********************/
            try
            { Database.openDB(); }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
            /******************** End Open Database ********************/
            /***********************************************************/
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Database.closeDB();
        }

        private void btnStudentAffairs_Click(object sender, RoutedEventArgs e)
        {
            home.Visibility = Visibility.Hidden;
            locStuAff.Visibility = Visibility.Visible;
        }

        private void Mentor_Click(object sender, RoutedEventArgs e)
        {
            home.Visibility = Visibility.Hidden;
            locMentor.Visibility = Visibility.Visible;
        }
    }
}
