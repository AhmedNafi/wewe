using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for ucCoursesForStuAff.xaml
    /// </summary>
    public partial class ucCoursesForStuAff : UserControl
    {
        public ucCoursesForStuAff()
        {
            InitializeComponent();

            clearAllTextBoxes();
            chnageBtnBackgroundImageAndHideAllPNL();
            var brushHomeOrange = new ImageBrush();
            brushHomeOrange.ImageSource = new BitmapImage(new Uri("../../Home-Orange.png", UriKind.Relative));
            btnHomeInCoursesPNL.Background = brushHomeOrange;

            stkPanelHomeCoursesForStuAff.Visibility = Visibility.Visible;

            DataTable dtForCourses = Database.returnTable("select * from Courses");
            dtForCourses.Columns["courseid"].ColumnName = "Course ID";
            dtForCourses.Columns["coursename"].ColumnName = "Course Name";
            dtForCourses.Columns["acdid"].ColumnName = "Academic Year ID";
            dtForCourses.Columns["acdname"].ColumnName = "Academic Year Name";
            dtForCourses.Columns["deptid"].ColumnName = "Department ID";
            dtForCourses.Columns["deptname"].ColumnName = "Department Name";

            DGForCourses.ItemsSource = dtForCourses.DefaultView;

            clickOnHomeBtn = true;
            currentPnl = "HomeCourses";
        }

        private void btnSubmitAddNewCourses_Click(object sender, RoutedEventArgs e)
        {
            //Get Number Of Rows In Table To Check If Course ID Is Exist
            bool idIsExist = false;
            DataTable dtForCourses = Database.returnTable("select * from Courses");
            int numOfRowsOfdtForCourses = dtForCourses.Rows.Count;

            if (txtBoxCoursesIDInCoursesPnl.Text.Trim() == "")
            { MessageBox.Show("Please Enter Courses ID", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
            else if (txtBoxCoursesNameInCoursesPnl.Text.Trim() == "")
            { MessageBox.Show("Please Enter Course Name", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
            else if (cmbBoxAcdYearIDInCoursesPnl.SelectedIndex == -1 || cmbBoxAcdYearNameInCoursesPnl.SelectedIndex == -1)
            { MessageBox.Show("Please Choose Academic Year ID Or Academic Year Name", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
            else if (cmbBoxDeptIDInCoursesPnl.SelectedIndex == -1 || cmbBoxDeptNameInCoursesPnl.SelectedIndex == -1)
            { MessageBox.Show("Please Choose Department ID Or Department Name", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
            else
            {
                string newCoursesID = txtBoxCoursesIDInCoursesPnl.Text;

                for (int i = 0; i < numOfRowsOfdtForCourses; i++)
                { if (newCoursesID == Convert.ToString(dtForCourses.Rows[i][0])) { idIsExist = true; } }

                if (idIsExist == true)
                { MessageBox.Show("Your Courses ID already Exist.\nPlease Enter Another Courses Number", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }

                else
                {
                    MessageBoxResult resMakeNewCourses = MessageBox.Show("Are You Sure To Add New Course?", "New Course", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    if (resMakeNewCourses == MessageBoxResult.Yes)
                    {
                        try
                        {
                            Database.Run("insert into Courses values('" + txtBoxCoursesIDInCoursesPnl.Text + "', '" 
                                                                        + txtBoxCoursesNameInCoursesPnl.Text + "', '" 
                                                                        + cmbBoxAcdYearIDInCoursesPnl.Text + "', '"
                                                                        + cmbBoxAcdYearNameInCoursesPnl.Text + "', '"
                                                                        + cmbBoxDeptIDInCoursesPnl.Text + "', '"
                                                                        + cmbBoxDeptNameInCoursesPnl.Text + "')");

                            txtBoxCoursesIDInCoursesPnl.Text = "";
                            txtBoxCoursesNameInCoursesPnl.Text = "";
                            cmbBoxAcdYearIDInCoursesPnl.SelectedIndex = -1;
                            cmbBoxAcdYearNameInCoursesPnl.SelectedIndex = -1;
                            cmbBoxDeptIDInCoursesPnl.SelectedIndex = -1;
                            cmbBoxDeptNameInCoursesPnl.SelectedIndex = -1;
                            MessageBox.Show("The New Course Has Been Successfully Added", "New Course", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        catch (Exception ex)
                        { MessageBox.Show("Error here" + ex.Message, "New Course", MessageBoxButton.OK, MessageBoxImage.Error); }
                    }
                    else if (resMakeNewCourses == MessageBoxResult.No)
                    {}
                }
            }
        }

        private void DGForCourses_LoadingRow(object sender, DataGridRowEventArgs e)
        { e.Row.Header = (e.Row.GetIndex() + 1).ToString(); }

        private void txtBoxSearchInDatagridInCoursesPNL_KeyUp(object sender, KeyEventArgs e)
        {
            DataTable dtForCourses = Database.returnTable("select * from Courses");
            DataTable newDTForCoursesBySearch = new DataTable();
            newDTForCoursesBySearch.Columns.Add("Course ID", typeof(System.String));
            newDTForCoursesBySearch.Columns.Add("Course Name", typeof(System.String));
            newDTForCoursesBySearch.Columns.Add("Academic Year ID", typeof(System.String));
            newDTForCoursesBySearch.Columns.Add("Academic Year Name", typeof(System.String));
            newDTForCoursesBySearch.Columns.Add("Department ID", typeof(System.String));
            newDTForCoursesBySearch.Columns.Add("Department Name", typeof(System.String));

            for (int i = 0; i < dtForCourses.Rows.Count; i++)
            {
                bool found = false;
                for (int j = 0; j < dtForCourses.Columns.Count; j++)
                {
                    if (found == false)
                    { 
                        try
                        {
                            string searchText = txtBoxSearchInDatagridInCoursesPNL.Text;
                            int numOfCharOfSearchValue = searchText.Length;
                            string valueOfCurrentCell = Convert.ToString(dtForCourses.Rows[i][j]);
                            string lastValueOfCellForCompare = valueOfCurrentCell.Substring(0, numOfCharOfSearchValue);

                            if (searchText == lastValueOfCellForCompare)
                            {
                                DataRow dr = newDTForCoursesBySearch.NewRow();
                                dr[0] = dtForCourses.Rows[i][0];
                                dr[1] = dtForCourses.Rows[i][1];
                                dr[2] = dtForCourses.Rows[i][2];
                                dr[3] = dtForCourses.Rows[i][3];
                                dr[4] = dtForCourses.Rows[i][4];
                                dr[5] = dtForCourses.Rows[i][5];
                                newDTForCoursesBySearch.Rows.Add(dr);
                                found = true;
                            }
                        }
                        catch (Exception)
                        { }
                    }
                }
            }

            if (txtBoxSearchInDatagridInCoursesPNL.Text.Trim() == "")
            {
                dtForCourses.Columns["courseid"].ColumnName = "Course ID";
                dtForCourses.Columns["coursename"].ColumnName = "Course Name";
                dtForCourses.Columns["acdid"].ColumnName = "Academic Year ID";
                dtForCourses.Columns["acdname"].ColumnName = "Academic Year Name";
                dtForCourses.Columns["deptid"].ColumnName = "Department ID";
                dtForCourses.Columns["deptname"].ColumnName = "Department Name";

                DGForCourses.ItemsSource = dtForCourses.DefaultView;
            }
            else
            { DGForCourses.ItemsSource = newDTForCoursesBySearch.DefaultView; }
        }

        public void chnageBtnBackgroundImageAndHideAllPNL()
        {
            var brushHomeWhite = new ImageBrush();
            brushHomeWhite.ImageSource = new BitmapImage(new Uri("../../Home-White.png", UriKind.Relative));
            btnHomeInCoursesPNL.Background = brushHomeWhite;

            var brushAddWhite = new ImageBrush();
            brushAddWhite.ImageSource = new BitmapImage(new Uri("../../Add-White.png", UriKind.Relative));
            btnAddNewCoursesInCoursesPNL.Background = brushAddWhite;

            var brushEditWhite = new ImageBrush();
            brushEditWhite.ImageSource = new BitmapImage(new Uri("../../Edit-White.png", UriKind.Relative));
            btnEditCoursesInCoursesPNL.Background = brushEditWhite;

            var brushRemoveWhite = new ImageBrush();
            brushRemoveWhite.ImageSource = new BitmapImage(new Uri("../../Remove-White.png", UriKind.Relative));
            btnRemoveCoursesInCoursesPNL.Background = brushRemoveWhite;

            stkPanelHomeCoursesForStuAff.Visibility = Visibility.Hidden;
            stkPanelAddCoursesForStuAff.Visibility = Visibility.Hidden;
            stkPanelEditCoursesForStuAff.Visibility = Visibility.Hidden;
            stkPanelRemoveCoursesForStuAff.Visibility = Visibility.Hidden;

            txtBoxSearchInDatagridInCoursesPNL.Text = "";
            txtBoxSearchInDatagridInEditCoursesPNL.Text = "";
            txtBoxSearchInDatagridInRemoveCoursesPNL.Text = "";
            txtBoxCoursesIDInCoursesPnl.Text = "";
            txtBoxCoursesNameInCoursesPnl.Text = "";
        }

        bool clickOnHomeBtn = false;
        bool clickOnAddBtn = false;
        bool clickOnEditBtn = false;
        bool clickOnRemoveBtn = false;
        string currentPnl = "HomeCourses";

        private void btnHomeInCoursesPNL_Click(object sender, RoutedEventArgs e)
        {
            clearAllTextBoxes();
            chnageBtnBackgroundImageAndHideAllPNL();
            var brushHomeOrange = new ImageBrush();
            brushHomeOrange.ImageSource = new BitmapImage(new Uri("../../Home-Orange.png", UriKind.Relative));
            btnHomeInCoursesPNL.Background = brushHomeOrange;

            stkPanelHomeCoursesForStuAff.Visibility = Visibility.Visible;

            DataTable dtForCourses = Database.returnTable("select * from Courses");
            dtForCourses.Columns["courseid"].ColumnName = "Course ID";
            dtForCourses.Columns["coursename"].ColumnName = "Course Name";
            dtForCourses.Columns["acdid"].ColumnName = "Academic Year ID";
            dtForCourses.Columns["acdname"].ColumnName = "Academic Year Name";
            dtForCourses.Columns["deptid"].ColumnName = "Department ID";
            dtForCourses.Columns["deptname"].ColumnName = "Department Name";

            DGForCourses.ItemsSource = dtForCourses.DefaultView;

            clickOnHomeBtn = true;
            currentPnl = "HomeCourses";
        }

        private void btnHomeInCoursesPNL_MouseEnter(object sender, MouseEventArgs e)
        {
            var brushHomeOrange = new ImageBrush();
            brushHomeOrange.ImageSource = new BitmapImage(new Uri("../../Home-Orange.png", UriKind.Relative));
            btnHomeInCoursesPNL.Background = brushHomeOrange;
        }

        private void btnHomeInCoursesPNL_MouseLeave(object sender, MouseEventArgs e)
        {
            if (clickOnHomeBtn == false)
            {
                if (currentPnl != "HomeCourses")
                {
                    var brushHomeWhite = new ImageBrush();
                    brushHomeWhite.ImageSource = new BitmapImage(new Uri("../../Home-White.png", UriKind.Relative));
                    btnHomeInCoursesPNL.Background = brushHomeWhite;
                }
            }
            else
            { clickOnHomeBtn = false; }
        }

        private void btnAddNewCoursesInCoursesPNL_Click(object sender, RoutedEventArgs e)
        {
            clearAllTextBoxes();
            chnageBtnBackgroundImageAndHideAllPNL();
            var brushAddOrange = new ImageBrush();
            brushAddOrange.ImageSource = new BitmapImage(new Uri("../../Add-Orange.png", UriKind.Relative));
            btnAddNewCoursesInCoursesPNL.Background = brushAddOrange;

            stkPanelAddCoursesForStuAff.Visibility = Visibility.Visible;

            clickOnAddBtn = true;
            currentPnl = "AddCourses";
        }

        private void btnAddNewCoursesInCoursesPNL_MouseEnter(object sender, MouseEventArgs e)
        {
            var brushAddNewOrange = new ImageBrush();
            brushAddNewOrange.ImageSource = new BitmapImage(new Uri("../../Add-Orange.png", UriKind.Relative));
            btnAddNewCoursesInCoursesPNL.Background = brushAddNewOrange;
        }

        private void btnAddNewCoursesInCoursesPNL_MouseLeave(object sender, MouseEventArgs e)
        {
            if (clickOnAddBtn == false)
            {
                if (currentPnl != "AddCourses")
                {
                    var brushAddNewWhite = new ImageBrush();
                    brushAddNewWhite.ImageSource = new BitmapImage(new Uri("../../Add-White.png", UriKind.Relative));
                    btnAddNewCoursesInCoursesPNL.Background = brushAddNewWhite;
                }
            }
            else
            { clickOnAddBtn = false; }
        }

        private void btnEditCoursesInCoursesPNL_Click(object sender, RoutedEventArgs e)
        {
            clearAllTextBoxes();
            chnageBtnBackgroundImageAndHideAllPNL();
            var brushEditOrange = new ImageBrush();
            brushEditOrange.ImageSource = new BitmapImage(new Uri("../../Edit-Orange.png", UriKind.Relative));
            btnEditCoursesInCoursesPNL.Background = brushEditOrange;

            stkPanelEditCoursesForStuAff.Visibility = Visibility.Visible;

            DataTable dtForCoursesInEditPNL = Database.returnTable("select * from Courses");
            dtForCoursesInEditPNL.Columns["courseid"].ColumnName = "Course ID";
            dtForCoursesInEditPNL.Columns["coursename"].ColumnName = "Course Name";
            dtForCoursesInEditPNL.Columns["acdid"].ColumnName = "Academic Year ID";
            dtForCoursesInEditPNL.Columns["acdname"].ColumnName = "Academic Year Name";
            dtForCoursesInEditPNL.Columns["deptid"].ColumnName = "Department ID";
            dtForCoursesInEditPNL.Columns["deptname"].ColumnName = "Department Name";

            DGForCoursesInEdit.ItemsSource = dtForCoursesInEditPNL.DefaultView;

            clickOnEditBtn = true;
            currentPnl = "EditCourses";
        }

        private void btnEditCoursesInCoursesPNL_MouseEnter(object sender, MouseEventArgs e)
        {
            var brushEditOrange = new ImageBrush();
            brushEditOrange.ImageSource = new BitmapImage(new Uri("../../Edit-Orange.png", UriKind.Relative));
            btnEditCoursesInCoursesPNL.Background = brushEditOrange;
        }

        private void btnEditCoursesInCoursesPNL_MouseLeave(object sender, MouseEventArgs e)
        {
            if (clickOnEditBtn == false)
            {
                if (currentPnl != "EditCourses")
                {
                    var brushEditWhite = new ImageBrush();
                    brushEditWhite.ImageSource = new BitmapImage(new Uri("../../Edit-White.png", UriKind.Relative));
                    btnEditCoursesInCoursesPNL.Background = brushEditWhite;
                }
            }
            else
            { clickOnEditBtn = false; }
        }

        private void btnRemoveCoursesInCoursesPNL_Click(object sender, RoutedEventArgs e)
        {
            clearAllTextBoxes();
            chnageBtnBackgroundImageAndHideAllPNL();
            var brushRemoveOrange = new ImageBrush();
            brushRemoveOrange.ImageSource = new BitmapImage(new Uri("../../Remove-Orange.png", UriKind.Relative));
            btnRemoveCoursesInCoursesPNL.Background = brushRemoveOrange;

            stkPanelRemoveCoursesForStuAff.Visibility = Visibility.Visible;

            DataTable dtForCoursesInRemovePNL = Database.returnTable("select * from Courses");
            dtForCoursesInRemovePNL.Columns["courseid"].ColumnName = "Course ID";
            dtForCoursesInRemovePNL.Columns["coursename"].ColumnName = "Course Name";
            dtForCoursesInRemovePNL.Columns["acdid"].ColumnName = "Academic Year ID";
            dtForCoursesInRemovePNL.Columns["acdname"].ColumnName = "Academic Year Name";
            dtForCoursesInRemovePNL.Columns["deptid"].ColumnName = "Department ID";
            dtForCoursesInRemovePNL.Columns["deptname"].ColumnName = "Department Name";

            DGForCoursesInRemove.ItemsSource = dtForCoursesInRemovePNL.DefaultView;

            clickOnRemoveBtn = true;
            currentPnl = "RemoveCourses";
        }

        private void btnRemoveCoursesInCoursesPNL_MouseEnter(object sender, MouseEventArgs e)
        {
            var brushRemoveOrange = new ImageBrush();
            brushRemoveOrange.ImageSource = new BitmapImage(new Uri("../../Remove-Orange.png", UriKind.Relative));
            btnRemoveCoursesInCoursesPNL.Background = brushRemoveOrange;
        }

        private void btnRemoveCoursesInCoursesPNL_MouseLeave(object sender, MouseEventArgs e)
        {
            if (clickOnRemoveBtn == false)
            {
                if (currentPnl != "RemoveCourses")
                {
                    var brushRemoveWhite = new ImageBrush();
                    brushRemoveWhite.ImageSource = new BitmapImage(new Uri("../../Remove-White.png", UriKind.Relative));
                    btnRemoveCoursesInCoursesPNL.Background = brushRemoveWhite;
                }
            }
            else
            { clickOnRemoveBtn = false; }
        }

        private void txtBoxSearchInDatagridInEditCoursesPNL_KeyUp(object sender, KeyEventArgs e)
        {
            DataTable dtForCoursesEdit = Database.returnTable("select * from Courses");
            DataTable newDTForCoursesEditBySearch = new DataTable();
            newDTForCoursesEditBySearch.Columns.Add("Course ID", typeof(System.String));
            newDTForCoursesEditBySearch.Columns.Add("Course Name", typeof(System.String));
            newDTForCoursesEditBySearch.Columns.Add("Academic Year ID", typeof(System.String));
            newDTForCoursesEditBySearch.Columns.Add("Academic Year Name", typeof(System.String));
            newDTForCoursesEditBySearch.Columns.Add("Department ID", typeof(System.String));
            newDTForCoursesEditBySearch.Columns.Add("Department Name", typeof(System.String));

            for (int i = 0; i < dtForCoursesEdit.Rows.Count; i++)
            {
                bool found = false;
                for (int j = 0; j < dtForCoursesEdit.Columns.Count; j++)
                {
                    if (found == false) 
                    { 
                        try
                        {
                            string searchText = txtBoxSearchInDatagridInEditCoursesPNL.Text;
                            int numOfCharOfSearchValue = searchText.Length;
                            string valueOfCurrentCell = Convert.ToString(dtForCoursesEdit.Rows[i][j]);
                            string lastValueOfCellForCompare = valueOfCurrentCell.Substring(0, numOfCharOfSearchValue);

                            if (searchText == lastValueOfCellForCompare)
                            {
                                DataRow dr = newDTForCoursesEditBySearch.NewRow();
                                dr[0] = dtForCoursesEdit.Rows[i][0];
                                dr[1] = dtForCoursesEdit.Rows[i][1];
                                dr[2] = dtForCoursesEdit.Rows[i][2];
                                dr[3] = dtForCoursesEdit.Rows[i][3];
                                dr[4] = dtForCoursesEdit.Rows[i][4];
                                dr[5] = dtForCoursesEdit.Rows[i][5];
                                newDTForCoursesEditBySearch.Rows.Add(dr);
                                found = true;
                            }
                        }
                        catch (Exception)
                        { }
                    }
                }
            }

            if (txtBoxSearchInDatagridInEditCoursesPNL.Text.Trim() == "")
            {
                DataTable dtForCoursesInEditPNL = Database.returnTable("select * from Courses");
                dtForCoursesInEditPNL.Columns["courseid"].ColumnName = "Course ID";
                dtForCoursesInEditPNL.Columns["coursename"].ColumnName = "Course Name";
                dtForCoursesInEditPNL.Columns["acdid"].ColumnName = "Academic Year ID";
                dtForCoursesInEditPNL.Columns["acdname"].ColumnName = "Academic Year Name";
                dtForCoursesInEditPNL.Columns["deptid"].ColumnName = "Department ID";
                dtForCoursesInEditPNL.Columns["deptname"].ColumnName = "Department Name";

                DGForCoursesInEdit.ItemsSource = dtForCoursesInEditPNL.DefaultView;
            }
            else
            { DGForCoursesInEdit.ItemsSource = newDTForCoursesEditBySearch.DefaultView; }
        }

        private void txtBoxSearchInDatagridInRemoveCoursesPNL_KeyUp(object sender, KeyEventArgs e)
        {
            DataTable dtForCoursesRemove = Database.returnTable("select * from Courses");
            DataTable newDTForCoursesRemoveBySearch = new DataTable();
            newDTForCoursesRemoveBySearch.Columns.Add("Course ID", typeof(System.String));
            newDTForCoursesRemoveBySearch.Columns.Add("Course Name", typeof(System.String));
            newDTForCoursesRemoveBySearch.Columns.Add("Academic Year ID", typeof(System.String));
            newDTForCoursesRemoveBySearch.Columns.Add("Academic Year Name", typeof(System.String));
            newDTForCoursesRemoveBySearch.Columns.Add("Department ID", typeof(System.String));
            newDTForCoursesRemoveBySearch.Columns.Add("Department Name", typeof(System.String));

            for (int i = 0; i < dtForCoursesRemove.Rows.Count; i++)
            {
                bool found = false;
                for (int j = 0; j < dtForCoursesRemove.Columns.Count; j++)
                {
                    if (found == false) 
                    { 
                        try
                        {
                            string searchText = txtBoxSearchInDatagridInRemoveCoursesPNL.Text;
                            int numOfCharOfSearchValue = searchText.Length;
                            string valueOfCurrentCell = Convert.ToString(dtForCoursesRemove.Rows[i][j]);
                            string lastValueOfCellForCompare = valueOfCurrentCell.Substring(0, numOfCharOfSearchValue);

                            if (searchText == lastValueOfCellForCompare)
                            {
                                DataRow dr = newDTForCoursesRemoveBySearch.NewRow();
                                dr[0] = dtForCoursesRemove.Rows[i][0];
                                dr[1] = dtForCoursesRemove.Rows[i][1];
                                dr[2] = dtForCoursesRemove.Rows[i][2];
                                dr[3] = dtForCoursesRemove.Rows[i][3];
                                dr[4] = dtForCoursesRemove.Rows[i][4];
                                dr[5] = dtForCoursesRemove.Rows[i][5];
                                newDTForCoursesRemoveBySearch.Rows.Add(dr);
                                found = true;
                            }
                        }
                        catch (Exception)
                        { }
                    }
                }
            }

            if (txtBoxSearchInDatagridInRemoveCoursesPNL.Text.Trim() == "")
            {
                DataTable dtForCoursesInRemovePNL = Database.returnTable("select * from Courses");
                dtForCoursesInRemovePNL.Columns["courseid"].ColumnName = "Course ID";
                dtForCoursesInRemovePNL.Columns["coursename"].ColumnName = "Course Name";
                dtForCoursesInRemovePNL.Columns["acdid"].ColumnName = "Academic Year ID";
                dtForCoursesInRemovePNL.Columns["acdname"].ColumnName = "Academic Year Name";
                dtForCoursesInRemovePNL.Columns["deptid"].ColumnName = "Department ID";
                dtForCoursesInRemovePNL.Columns["deptname"].ColumnName = "Department Name";

                DGForCoursesInRemove.ItemsSource = dtForCoursesInRemovePNL.DefaultView;
            }
            else
            { DGForCoursesInRemove.ItemsSource = newDTForCoursesRemoveBySearch.DefaultView; }
        }

        public void clearAllTextBoxes()
        {
            txtBoxCoursesNameInCoursesPnl.Text = "";
            txtBoxCoursesIDInCoursesPnl.Text = "";
            txtBoxSearchInDatagridInCoursesPNL.Text = "";
            txtBoxSearchInDatagridInEditCoursesPNL.Text = "";
            txtBoxSearchInDatagridInRemoveCoursesPNL.Text = "";

            DataTable dtForAcdYears = Database.returnTable("select * from AcademicYears");
            cmbBoxAcdYearIDInCoursesPnl.ItemsSource = dtForAcdYears.DefaultView;
            cmbBoxAcdYearIDInCoursesPnl.DisplayMemberPath = "acdid";

            cmbBoxAcdYearNameInCoursesPnl.ItemsSource = dtForAcdYears.DefaultView;
            cmbBoxAcdYearNameInCoursesPnl.DisplayMemberPath = "acdname";


            DataTable dtForDepartment = Database.returnTable("select * from Department");
            cmbBoxDeptIDInCoursesPnl.ItemsSource = dtForDepartment.DefaultView;
            cmbBoxDeptIDInCoursesPnl.DisplayMemberPath = "deptid";

            cmbBoxDeptNameInCoursesPnl.ItemsSource = dtForDepartment.DefaultView;
            cmbBoxDeptNameInCoursesPnl.DisplayMemberPath = "deptname";
        }

        private void Grid_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            clearAllTextBoxes();
            chnageBtnBackgroundImageAndHideAllPNL();
            var brushHomeOrange = new ImageBrush();
            brushHomeOrange.ImageSource = new BitmapImage(new Uri("../../Home-Orange.png", UriKind.Relative));
            btnHomeInCoursesPNL.Background = brushHomeOrange;

            stkPanelHomeCoursesForStuAff.Visibility = Visibility.Visible;

            DataTable dtForCourses = Database.returnTable("select * from Courses");
            dtForCourses.Columns["courseid"].ColumnName = "Course ID";
            dtForCourses.Columns["coursename"].ColumnName = "Course Name";
            dtForCourses.Columns["acdid"].ColumnName = "Academic Year ID";
            dtForCourses.Columns["acdname"].ColumnName = "Academic Year Name";
            dtForCourses.Columns["deptid"].ColumnName = "Department ID";
            dtForCourses.Columns["deptname"].ColumnName = "Department Name";

            DGForCourses.ItemsSource = dtForCourses.DefaultView;

            clickOnHomeBtn = true;
            currentPnl = "HomeCourses";
        }

        private void cmbBoxAcdYearIDInCoursesPnl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        { cmbBoxAcdYearNameInCoursesPnl.SelectedIndex = cmbBoxAcdYearIDInCoursesPnl.SelectedIndex; }

        private void cmbBoxAcdYearNameInCoursesPnl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        { cmbBoxAcdYearIDInCoursesPnl.SelectedIndex = cmbBoxAcdYearNameInCoursesPnl.SelectedIndex; }

        private void cmbBoxDeptIDInCoursesPnl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        { cmbBoxDeptNameInCoursesPnl.SelectedIndex = cmbBoxDeptIDInCoursesPnl.SelectedIndex; }

        private void cmbBoxDeptNameInCoursesPnl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        { cmbBoxDeptIDInCoursesPnl.SelectedIndex = cmbBoxDeptNameInCoursesPnl.SelectedIndex; }

    }
}
