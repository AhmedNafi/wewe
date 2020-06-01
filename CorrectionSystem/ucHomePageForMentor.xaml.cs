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

namespace CorrectionSystem
{
    /// <summary>
    /// Interaction logic for ucHomePageForMentor.xaml
    /// </summary>
    public partial class ucHomePageForMentor : UserControl
    {
        public ucHomePageForMentor()
        {
            InitializeComponent();
        }

        public void changeColorOfMainButtonsAndHideAllPnl()
        {
            btnExamsForMentor.Visibility = Visibility.Visible;
            btnStudentsForMentor.Visibility = Visibility.Visible;

            btnExamsForMentor.Background = Brushes.Black;
            btnExamsForMentor.Foreground = Brushes.White;
            btnExamsForMentor.Opacity = 0.8;

            btnStudentsForMentor.Background = Brushes.Black;
            btnStudentsForMentor.Foreground = Brushes.White;
            btnStudentsForMentor.Opacity = 0.8;

            stkPanelHomePageForMentor.Visibility = Visibility.Hidden;
            stkPanelExamsPageForMentor.Visibility = Visibility.Hidden;
            stkPanelStudentsPageForMentor.Visibility = Visibility.Hidden;

        }

        bool mainBtnExamsClick = false;
        bool mainBtnStudentsClick = false;
        string currentPnl = "";

        private void btnExamsinHPforMentor_Click(object sender, RoutedEventArgs e)
        {
            changeColorOfMainButtonsAndHideAllPnl();
            btnExamsForMentor.Background = Brushes.White;
            btnExamsForMentor.Foreground = Brushes.Black;
            btnExamsForMentor.Opacity = 1;
            stkPanelExamsPageForMentor.Visibility = Visibility.Visible;

            ucExamsForMentor ucexamsformentor = new ucExamsForMentor();
            stkPanelExamsPageForMentor.Children.Add(ucexamsformentor);

            mainBtnExamsClick = false;
            currentPnl = "Exams Panel";

        }

        private void btnStudentsinHPforMentor_Click(object sender, RoutedEventArgs e)
        {
            changeColorOfMainButtonsAndHideAllPnl();
            btnStudentsForMentor.Background = Brushes.White;
            btnStudentsForMentor.Foreground = Brushes.Black;
            btnStudentsForMentor.Opacity = 1;
            stkPanelStudentsPageForMentor.Visibility = Visibility.Visible;

            ucStudentsForMentor ucstudentsformentor = new ucStudentsForMentor();
            stkPanelStudentsPageForMentor.Children.Add(ucstudentsformentor);

            mainBtnStudentsClick = false;
            currentPnl = "Students Panel";
        }

        private void btnExamsForMentor_Click(object sender, RoutedEventArgs e)
        {
            changeColorOfMainButtonsAndHideAllPnl();
            btnExamsForMentor.Background = Brushes.White;
            btnExamsForMentor.Foreground = Brushes.Black;
            btnExamsForMentor.Opacity = 1;
            stkPanelExamsPageForMentor.Visibility = Visibility.Visible;

            ucExamsForMentor ucexamsformentor = new ucExamsForMentor();
            stkPanelExamsPageForMentor.Children.Add(ucexamsformentor);

            mainBtnExamsClick = false;
            currentPnl = "Exams Panel";
        }

        private void btnExamsForMentor_MouseEnter(object sender, MouseEventArgs e)
        {
            btnExamsForMentor.Background = new SolidColorBrush(Color.FromRgb(255, 196, 89));
            btnExamsForMentor.Foreground = Brushes.Black;
            btnExamsForMentor.Opacity = 1;
        }

        private void btnExamsForMentor_MouseLeave(object sender, MouseEventArgs e)
        {
            if (mainBtnExamsClick == false)
            {
                if (currentPnl != "Exams Panel")
                {
                    btnExamsForMentor.Background = Brushes.Black;
                    btnExamsForMentor.Foreground = Brushes.White;
                    btnExamsForMentor.Opacity = 0.8;
                }
                else
                {
                    btnExamsForMentor.Background = Brushes.White;
                    btnExamsForMentor.Foreground = Brushes.Black;
                    btnExamsForMentor.Opacity = 1;
                }
            }
            else
            { mainBtnExamsClick = false; }
        }


        private void btnStudentsForMentor_Click(object sender, RoutedEventArgs e)
        {
            changeColorOfMainButtonsAndHideAllPnl();
            btnStudentsForMentor.Background = Brushes.White;
            btnStudentsForMentor.Foreground = Brushes.Black;
            btnStudentsForMentor.Opacity = 1;
            stkPanelStudentsPageForMentor.Visibility = Visibility.Visible;

            ucStudentsForMentor ucstudentsformentor = new ucStudentsForMentor();
            stkPanelStudentsPageForMentor.Children.Add(ucstudentsformentor);

            mainBtnStudentsClick = false;
            currentPnl = "Students Panel";
        }

        private void btnStudentsForMentor_MouseEnter(object sender, MouseEventArgs e)
        {
            btnStudentsForMentor.Background = new SolidColorBrush(Color.FromRgb(255, 196, 89));
            btnStudentsForMentor.Foreground = Brushes.Black;
            btnStudentsForMentor.Opacity = 1;
        }

        private void btnStudentsForMentor_MouseLeave(object sender, MouseEventArgs e)
        {
            if (mainBtnStudentsClick == false)
            {
                if (currentPnl != "Students Panel")
                {
                    btnStudentsForMentor.Background = Brushes.Black;
                    btnStudentsForMentor.Foreground = Brushes.White;
                    btnStudentsForMentor.Opacity = 0.8;
                }
                else
                {
                    btnStudentsForMentor.Background = Brushes.White;
                    btnStudentsForMentor.Foreground = Brushes.Black;
                    btnStudentsForMentor.Opacity = 1;
                }
            }
            else
            { mainBtnStudentsClick = false; }
        }


    }
}
