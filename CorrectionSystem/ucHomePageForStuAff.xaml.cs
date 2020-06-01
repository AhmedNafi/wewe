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
    /// Interaction logic for ucHomePageForStuAff.xaml
    /// </summary>
    public partial class ucHomePageForStuAff : UserControl
    {
        public ucHomePageForStuAff()
        {
            InitializeComponent();
        }

        public void changeColorOfMainButtonsAndHideAllPnl()
        {
            btnDepartmentForStuAff.Visibility = Visibility.Visible;
            btnAcdYearsForStuAff.Visibility = Visibility.Visible;
            btnCoursesForStuAff.Visibility = Visibility.Visible;
            btnStudentsForStuAff.Visibility = Visibility.Visible;

            btnDepartmentForStuAff.Background = Brushes.Black;
            btnDepartmentForStuAff.Foreground = Brushes.White;
            btnDepartmentForStuAff.Opacity = 0.8;

            btnAcdYearsForStuAff.Background = Brushes.Black;
            btnAcdYearsForStuAff.Foreground = Brushes.White;
            btnAcdYearsForStuAff.Opacity = 0.8;

            btnCoursesForStuAff.Background = Brushes.Black;
            btnCoursesForStuAff.Foreground = Brushes.White;
            btnCoursesForStuAff.Opacity = 0.8;

            btnStudentsForStuAff.Background = Brushes.Black;
            btnStudentsForStuAff.Foreground = Brushes.White;
            btnStudentsForStuAff.Opacity = 0.8;

            stkPanelHomePageForStuAff.Visibility = Visibility.Hidden;
            stkPanelDepartmentPageForStuAff.Visibility = Visibility.Hidden;
            stkPanelAcdYearsPageForStuAff.Visibility = Visibility.Hidden;
            stkPanelCoursesPageForStuAff.Visibility = Visibility.Hidden;
            stkPanelStudentsPageForStuAff.Visibility = Visibility.Hidden;
        }


        private void btnDepartmentinHPforStuAff_Click(object sender, RoutedEventArgs e)
        {
            // To Hide User Control
            //((Panel)this.Parent).Children.Remove(this);

            changeColorOfMainButtonsAndHideAllPnl();
            btnDepartmentForStuAff.Background = Brushes.White;
            btnDepartmentForStuAff.Foreground = Brushes.Black;
            btnDepartmentForStuAff.Opacity = 1;
            stkPanelDepartmentPageForStuAff.Visibility = Visibility.Visible;

            currentPnl = "Department Panel";

            ucDepartmentForStuAff ucdepartmentforstuaff = new ucDepartmentForStuAff();
            stkPanelDepartmentPageForStuAff.Children.Add(ucdepartmentforstuaff);
        }

        private void btnAcdYearsinHPforStuAff_Click(object sender, RoutedEventArgs e)
        {
            changeColorOfMainButtonsAndHideAllPnl();
            btnAcdYearsForStuAff.Background = Brushes.White;
            btnAcdYearsForStuAff.Foreground = Brushes.Black;
            btnAcdYearsForStuAff.Opacity = 1;
            stkPanelAcdYearsPageForStuAff.Visibility = Visibility.Visible;

            currentPnl = "Academic Years Panel";

            ucAcademicYearsForStuAff ucacademicyearsforstuaff = new ucAcademicYearsForStuAff();
            stkPanelAcdYearsPageForStuAff.Children.Add(ucacademicyearsforstuaff);
        }

        private void btnCourserinHPforStuAff_Click(object sender, RoutedEventArgs e)
        {
            changeColorOfMainButtonsAndHideAllPnl();
            btnCoursesForStuAff.Background = Brushes.White;
            btnCoursesForStuAff.Foreground = Brushes.Black;
            btnCoursesForStuAff.Opacity = 1;
            stkPanelCoursesPageForStuAff.Visibility = Visibility.Visible;

            currentPnl = "Courses Panel";

            ucCoursesForStuAff ucacademicyearsforstuaff = new ucCoursesForStuAff();
            stkPanelCoursesPageForStuAff.Children.Add(ucacademicyearsforstuaff);
        }

        private void btnStudentsinHPforStuAff_Click(object sender, RoutedEventArgs e)
        {
            changeColorOfMainButtonsAndHideAllPnl();
            btnStudentsForStuAff.Background = Brushes.White;
            btnStudentsForStuAff.Foreground = Brushes.Black;
            btnStudentsForStuAff.Opacity = 1;
            stkPanelStudentsPageForStuAff.Visibility = Visibility.Visible;

            currentPnl = "Students Panel";

            ucStudentsForStuAff ucacademicyearsforstuaff = new ucStudentsForStuAff();
            stkPanelStudentsPageForStuAff.Children.Add(ucacademicyearsforstuaff);
        }

        bool mainBtnDeptClick = false;
        bool mainBtnAcdYearsClick = false;
        bool mainBtnCoursesClick = false;
        bool mainBtnStudentsClick = false;
        string currentPnl = "";

        private void btnDepartmentForStuAff_Click(object sender, RoutedEventArgs e)
        {
            changeColorOfMainButtonsAndHideAllPnl();
            btnDepartmentForStuAff.Background = Brushes.White;
            btnDepartmentForStuAff.Foreground = Brushes.Black;
            btnDepartmentForStuAff.Opacity = 1;
            stkPanelDepartmentPageForStuAff.Visibility = Visibility.Visible;

            ucDepartmentForStuAff ucdepartmentforstuaff = new ucDepartmentForStuAff();
            stkPanelDepartmentPageForStuAff.Children.Add(ucdepartmentforstuaff);

            mainBtnDeptClick = true;
            currentPnl = "Department Panel";
        }

        private void btnDepartmentForStuAff_MouseEnter(object sender, MouseEventArgs e)
        {
            btnDepartmentForStuAff.Background = new SolidColorBrush(Color.FromRgb(255, 196, 89));
            btnDepartmentForStuAff.Foreground = Brushes.Black;
            btnDepartmentForStuAff.Opacity = 1;
        }

        private void btnDepartmentForStuAff_MouseLeave(object sender, MouseEventArgs e)
        {
            if (mainBtnDeptClick == false)
            {
                if (currentPnl != "Department Panel")
                {
                    btnDepartmentForStuAff.Background = Brushes.Black;
                    btnDepartmentForStuAff.Foreground = Brushes.White;
                    btnDepartmentForStuAff.Opacity = 0.8;
                }
                else
                {
                    btnDepartmentForStuAff.Background = Brushes.White;
                    btnDepartmentForStuAff.Foreground = Brushes.Black;
                    btnDepartmentForStuAff.Opacity = 1;
                }
            }
            else
            {
                mainBtnDeptClick = false;
            }
        }

        private void btnAcdYearsForStuAff_Click(object sender, RoutedEventArgs e)
        {
            changeColorOfMainButtonsAndHideAllPnl();
            btnAcdYearsForStuAff.Background = Brushes.White;
            btnAcdYearsForStuAff.Foreground = Brushes.Black;
            btnAcdYearsForStuAff.Opacity = 1;
            stkPanelAcdYearsPageForStuAff.Visibility = Visibility.Visible;

            ucAcademicYearsForStuAff ucacademicyearsforstuaff = new ucAcademicYearsForStuAff();
            stkPanelAcdYearsPageForStuAff.Children.Add(ucacademicyearsforstuaff);

            mainBtnAcdYearsClick = true;
            currentPnl = "Academic Years Panel";
        }

        private void btnAcdYearsForStuAff_MouseEnter(object sender, MouseEventArgs e)
        {
            btnAcdYearsForStuAff.Background = new SolidColorBrush(Color.FromRgb(255, 196, 89));
            btnAcdYearsForStuAff.Foreground = Brushes.Black;
            btnAcdYearsForStuAff.Opacity = 1;
        }

        private void btnAcdYearsForStuAff_MouseLeave(object sender, MouseEventArgs e)
        {
            if (mainBtnAcdYearsClick == false)
            {
                if (currentPnl != "Academic Years Panel")
                {
                    btnAcdYearsForStuAff.Background = Brushes.Black;
                    btnAcdYearsForStuAff.Foreground = Brushes.White;
                    btnAcdYearsForStuAff.Opacity = 0.8;
                }
                else
                {
                    btnAcdYearsForStuAff.Background = Brushes.White;
                    btnAcdYearsForStuAff.Foreground = Brushes.Black;
                    btnAcdYearsForStuAff.Opacity = 1;
                }
            }
            else
            {
                mainBtnAcdYearsClick = false;
            }
        }

        private void btnCoursesForStuAff_Click(object sender, RoutedEventArgs e)
        {
            changeColorOfMainButtonsAndHideAllPnl();
            btnCoursesForStuAff.Background = Brushes.White;
            btnCoursesForStuAff.Foreground = Brushes.Black;
            btnCoursesForStuAff.Opacity = 1;
            stkPanelCoursesPageForStuAff.Visibility = Visibility.Visible;

            mainBtnCoursesClick = true;
            currentPnl = "Courses Panel";

            ucCoursesForStuAff ucacademicyearsforstuaff = new ucCoursesForStuAff();
            stkPanelCoursesPageForStuAff.Children.Add(ucacademicyearsforstuaff);
        }

        private void btnCoursesForStuAff_MouseEnter(object sender, MouseEventArgs e)
        {
            btnCoursesForStuAff.Background = new SolidColorBrush(Color.FromRgb(255, 196, 89));
            btnCoursesForStuAff.Foreground = Brushes.Black;
            btnCoursesForStuAff.Opacity = 1;
        }

        private void btnCoursesForStuAff_MouseLeave(object sender, MouseEventArgs e)
        {
            if (mainBtnCoursesClick == false)
            {
                if (currentPnl != "Courses Panel")
                {
                    btnCoursesForStuAff.Background = Brushes.Black;
                    btnCoursesForStuAff.Foreground = Brushes.White;
                    btnCoursesForStuAff.Opacity = 0.8;
                }
                else
                {
                    btnCoursesForStuAff.Background = Brushes.White;
                    btnCoursesForStuAff.Foreground = Brushes.Black;
                    btnCoursesForStuAff.Opacity = 1;
                }
            }
            else
            {
                mainBtnCoursesClick = false;
            }
        }

        private void btnStudentsForStuAff_Click(object sender, RoutedEventArgs e)
        {
            changeColorOfMainButtonsAndHideAllPnl();
            btnStudentsForStuAff.Background = Brushes.White;
            btnStudentsForStuAff.Foreground = Brushes.Black;
            btnStudentsForStuAff.Opacity = 1;
            stkPanelStudentsPageForStuAff.Visibility = Visibility.Visible;

            mainBtnStudentsClick = true;
            currentPnl = "Students Panel";

            ucStudentsForStuAff ucacademicyearsforstuaff = new ucStudentsForStuAff();
            stkPanelStudentsPageForStuAff.Children.Add(ucacademicyearsforstuaff);
        }

        private void btnStudentsForStuAff_MouseEnter(object sender, MouseEventArgs e)
        {
            btnStudentsForStuAff.Background = new SolidColorBrush(Color.FromRgb(255, 196, 89));
            btnStudentsForStuAff.Foreground = Brushes.Black;
            btnStudentsForStuAff.Opacity = 1;
        }

        private void btnStudentsForStuAff_MouseLeave(object sender, MouseEventArgs e)
        {
            if (mainBtnStudentsClick == false)
            {
                if (currentPnl != "Students Panel")
                {
                    btnStudentsForStuAff.Background = Brushes.Black;
                    btnStudentsForStuAff.Foreground = Brushes.White;
                    btnStudentsForStuAff.Opacity = 0.8;
                }
                else
                {
                    btnStudentsForStuAff.Background = Brushes.White;
                    btnStudentsForStuAff.Foreground = Brushes.Black;
                    btnStudentsForStuAff.Opacity = 1;
                }
            }
            else
            {
                mainBtnStudentsClick = false;
            }
        }


    }
}
