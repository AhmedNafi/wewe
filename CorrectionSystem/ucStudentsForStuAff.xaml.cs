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
    /// Interaction logic for ucStudentsForStuAff.xaml
    /// </summary>
    public partial class ucStudentsForStuAff : UserControl
    {
        public ucStudentsForStuAff()
        {
            InitializeComponent();

            clearAllTextBoxes();
            chnageBtnBackgroundImageAndHideAllPNL();
            var brushHomeOrange = new ImageBrush();
            brushHomeOrange.ImageSource = new BitmapImage(new Uri("../../Home-Orange.png", UriKind.Relative));
            btnHomeInStudentsPNL.Background = brushHomeOrange;

            stkPanelHomeStudentsForStuAff.Visibility = Visibility.Visible;

            DataTable dtForStudents = Database.returnTable("select * from Students");
            dtForStudents.Columns["stuid"].ColumnName = "Student ID";
            dtForStudents.Columns["stuname"].ColumnName = "Student Name";
            dtForStudents.Columns["acdid"].ColumnName = "Academic Year ID";
            dtForStudents.Columns["acdname"].ColumnName = "Academic Year Name";
            dtForStudents.Columns["deptid"].ColumnName = "Department ID";
            dtForStudents.Columns["deptname"].ColumnName = "Department Name";

            DGForStudents.ItemsSource = dtForStudents.DefaultView;

            clickOnHomeBtn = true;
            currentPnl = "HomeStudents";
        }

        private void btnSubmitAddNewStudents_Click(object sender, RoutedEventArgs e)
        {
            //Get Number Of Rows In Table To Check If Student ID Is Exist
            bool idIsExist = false;
            DataTable dtForStudents = Database.returnTable("select * from Students");
            int numOfRowsOfdtForStudents = dtForStudents.Rows.Count;

            if (txtBoxStudentsIDInStudentsPnl.Text.Trim() == "")
            { MessageBox.Show("Please Enter Students ID", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
            else if (txtBoxStudentsNameInStudentsPnl.Text.Trim() == "")
            { MessageBox.Show("Please Enter Student Name", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
            else if (cmbBoxAcdYearIDInStudentsPnl.SelectedIndex == -1 || cmbBoxAcdYearNameInStudentsPnl.SelectedIndex == -1)
            { MessageBox.Show("Please Choose Academic Year ID Or Academic Year Name", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
            else if (cmbBoxDeptIDInStudentsPnl.SelectedIndex == -1 || cmbBoxDeptNameInStudentsPnl.SelectedIndex == -1)
            { MessageBox.Show("Please Choose Department ID Or Department Name", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
            else
            {
                string newStudentsID = txtBoxStudentsIDInStudentsPnl.Text;

                for (int i = 0; i < numOfRowsOfdtForStudents; i++)
                { if (newStudentsID == Convert.ToString(dtForStudents.Rows[i][0])) { idIsExist = true; } }

                if (idIsExist == true)
                { MessageBox.Show("Your Students ID already Exist.\nPlease Enter Another Students Number", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }

                else
                {
                    MessageBoxResult resMakeNewStudents = MessageBox.Show("Are You Sure To Add New Student?", "New Student", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    if (resMakeNewStudents == MessageBoxResult.Yes)
                    {
                        try
                        {
                            Database.Run("insert into Students values('" + txtBoxStudentsIDInStudentsPnl.Text + "', '"
                                                                        + txtBoxStudentsNameInStudentsPnl.Text + "', '"
                                                                        + cmbBoxAcdYearIDInStudentsPnl.Text + "', '"
                                                                        + cmbBoxAcdYearNameInStudentsPnl.Text + "', '"
                                                                        + cmbBoxDeptIDInStudentsPnl.Text + "', '"
                                                                        + cmbBoxDeptNameInStudentsPnl.Text + "')");

                            txtBoxStudentsIDInStudentsPnl.Text = "";
                            txtBoxStudentsNameInStudentsPnl.Text = "";
                            cmbBoxAcdYearIDInStudentsPnl.SelectedIndex = -1;
                            cmbBoxAcdYearNameInStudentsPnl.SelectedIndex = -1;
                            cmbBoxDeptIDInStudentsPnl.SelectedIndex = -1;
                            cmbBoxDeptNameInStudentsPnl.SelectedIndex = -1;
                            MessageBox.Show("The New Student Has Been Successfully Added", "New Student", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        catch (Exception ex)
                        { MessageBox.Show("Error here" + ex.Message, "New Student", MessageBoxButton.OK, MessageBoxImage.Error); }
                    }
                    else if (resMakeNewStudents == MessageBoxResult.No)
                    { }
                }
            }
        }

        private void DGForStudents_LoadingRow(object sender, DataGridRowEventArgs e)
        { e.Row.Header = (e.Row.GetIndex() + 1).ToString(); }

        private void txtBoxSearchInDatagridInStudentsPNL_KeyUp(object sender, KeyEventArgs e)
        {
            DataTable dtForStudents = Database.returnTable("select * from Students");
            DataTable newDTForStudentsBySearch = new DataTable();
            newDTForStudentsBySearch.Columns.Add("Student ID", typeof(System.String));
            newDTForStudentsBySearch.Columns.Add("Student Name", typeof(System.String));
            newDTForStudentsBySearch.Columns.Add("Academic Year ID", typeof(System.String));
            newDTForStudentsBySearch.Columns.Add("Academic Year Name", typeof(System.String));
            newDTForStudentsBySearch.Columns.Add("Department ID", typeof(System.String));
            newDTForStudentsBySearch.Columns.Add("Department Name", typeof(System.String));

            for (int i = 0; i < dtForStudents.Rows.Count; i++)
            {
                bool found = false;
                for (int j = 0; j < dtForStudents.Columns.Count; j++)
                {
                    if (found == false)
                    {
                        try
                        {
                            string searchText = txtBoxSearchInDatagridInStudentsPNL.Text;
                            int numOfCharOfSearchValue = searchText.Length;
                            string valueOfCurrentCell = Convert.ToString(dtForStudents.Rows[i][j]);
                            string lastValueOfCellForCompare = valueOfCurrentCell.Substring(0, numOfCharOfSearchValue);

                            if (searchText == lastValueOfCellForCompare)
                            {
                                DataRow dr = newDTForStudentsBySearch.NewRow();
                                dr[0] = dtForStudents.Rows[i][0];
                                dr[1] = dtForStudents.Rows[i][1];
                                dr[2] = dtForStudents.Rows[i][2];
                                dr[3] = dtForStudents.Rows[i][3];
                                dr[4] = dtForStudents.Rows[i][4];
                                dr[5] = dtForStudents.Rows[i][5];
                                newDTForStudentsBySearch.Rows.Add(dr);
                                found = true;
                            }
                        }
                        catch (Exception)
                        { }
                    }
                }
            }

            if (txtBoxSearchInDatagridInStudentsPNL.Text.Trim() == "")
            {
                dtForStudents.Columns["stuid"].ColumnName = "Student ID";
                dtForStudents.Columns["stuname"].ColumnName = "Student Name";
                dtForStudents.Columns["acdid"].ColumnName = "Academic Year ID";
                dtForStudents.Columns["acdname"].ColumnName = "Academic Year Name";
                dtForStudents.Columns["deptid"].ColumnName = "Department ID";
                dtForStudents.Columns["deptname"].ColumnName = "Department Name";

                DGForStudents.ItemsSource = dtForStudents.DefaultView;
            }
            else
            { DGForStudents.ItemsSource = newDTForStudentsBySearch.DefaultView; }
        }

        public void chnageBtnBackgroundImageAndHideAllPNL()
        {
            var brushHomeWhite = new ImageBrush();
            brushHomeWhite.ImageSource = new BitmapImage(new Uri("../../Home-White.png", UriKind.Relative));
            btnHomeInStudentsPNL.Background = brushHomeWhite;

            var brushAddWhite = new ImageBrush();
            brushAddWhite.ImageSource = new BitmapImage(new Uri("../../Add-White.png", UriKind.Relative));
            btnAddNewStudentsInStudentsPNL.Background = brushAddWhite;

            var brushEditWhite = new ImageBrush();
            brushEditWhite.ImageSource = new BitmapImage(new Uri("../../Edit-White.png", UriKind.Relative));
            btnEditStudentsInStudentsPNL.Background = brushEditWhite;

            var brushRemoveWhite = new ImageBrush();
            brushRemoveWhite.ImageSource = new BitmapImage(new Uri("../../Remove-White.png", UriKind.Relative));
            btnRemoveStudentsInStudentsPNL.Background = brushRemoveWhite;

            stkPanelHomeStudentsForStuAff.Visibility = Visibility.Hidden;
            stkPanelAddStudentsForStuAff.Visibility = Visibility.Hidden;
            stkPanelEditStudentsForStuAff.Visibility = Visibility.Hidden;
            stkPanelRemoveStudentsForStuAff.Visibility = Visibility.Hidden;

            txtBoxSearchInDatagridInStudentsPNL.Text = "";
            txtBoxSearchInDatagridInEditStudentsPNL.Text = "";
            txtBoxSearchInDatagridInRemoveStudentsPNL.Text = "";
            txtBoxStudentsIDInStudentsPnl.Text = "";
            txtBoxStudentsNameInStudentsPnl.Text = "";
        }

        bool clickOnHomeBtn = false;
        bool clickOnAddBtn = false;
        bool clickOnEditBtn = false;
        bool clickOnRemoveBtn = false;
        string currentPnl = "HomeStudents";

        private void btnHomeInStudentsPNL_Click(object sender, RoutedEventArgs e)
        {
            clearAllTextBoxes();
            chnageBtnBackgroundImageAndHideAllPNL();
            var brushHomeOrange = new ImageBrush();
            brushHomeOrange.ImageSource = new BitmapImage(new Uri("../../Home-Orange.png", UriKind.Relative));
            btnHomeInStudentsPNL.Background = brushHomeOrange;

            stkPanelHomeStudentsForStuAff.Visibility = Visibility.Visible;

            DataTable dtForStudents = Database.returnTable("select * from Students");
            dtForStudents.Columns["stuid"].ColumnName = "Student ID";
            dtForStudents.Columns["stuname"].ColumnName = "Student Name";
            dtForStudents.Columns["acdid"].ColumnName = "Academic Year ID";
            dtForStudents.Columns["acdname"].ColumnName = "Academic Year Name";
            dtForStudents.Columns["deptid"].ColumnName = "Department ID";
            dtForStudents.Columns["deptname"].ColumnName = "Department Name";

            DGForStudents.ItemsSource = dtForStudents.DefaultView;

            clickOnHomeBtn = true;
            currentPnl = "HomeStudents";
        }

        private void btnHomeInStudentsPNL_MouseEnter(object sender, MouseEventArgs e)
        {
            var brushHomeOrange = new ImageBrush();
            brushHomeOrange.ImageSource = new BitmapImage(new Uri("../../Home-Orange.png", UriKind.Relative));
            btnHomeInStudentsPNL.Background = brushHomeOrange;
        }

        private void btnHomeInStudentsPNL_MouseLeave(object sender, MouseEventArgs e)
        {
            if (clickOnHomeBtn == false)
            {
                if (currentPnl != "HomeStudents")
                {
                    var brushHomeWhite = new ImageBrush();
                    brushHomeWhite.ImageSource = new BitmapImage(new Uri("../../Home-White.png", UriKind.Relative));
                    btnHomeInStudentsPNL.Background = brushHomeWhite;
                }
            }
            else
            { clickOnHomeBtn = false; }
        }

        private void btnAddNewStudentsInStudentsPNL_Click(object sender, RoutedEventArgs e)
        {
            clearAllTextBoxes();
            chnageBtnBackgroundImageAndHideAllPNL();
            var brushAddOrange = new ImageBrush();
            brushAddOrange.ImageSource = new BitmapImage(new Uri("../../Add-Orange.png", UriKind.Relative));
            btnAddNewStudentsInStudentsPNL.Background = brushAddOrange;

            stkPanelAddStudentsForStuAff.Visibility = Visibility.Visible;

            clickOnAddBtn = true;
            currentPnl = "AddStudents";
        }

        private void btnAddNewStudentsInStudentsPNL_MouseEnter(object sender, MouseEventArgs e)
        {
            var brushAddNewOrange = new ImageBrush();
            brushAddNewOrange.ImageSource = new BitmapImage(new Uri("../../Add-Orange.png", UriKind.Relative));
            btnAddNewStudentsInStudentsPNL.Background = brushAddNewOrange;
        }

        private void btnAddNewStudentsInStudentsPNL_MouseLeave(object sender, MouseEventArgs e)
        {
            if (clickOnAddBtn == false)
            {
                if (currentPnl != "AddStudents")
                {
                    var brushAddNewWhite = new ImageBrush();
                    brushAddNewWhite.ImageSource = new BitmapImage(new Uri("../../Add-White.png", UriKind.Relative));
                    btnAddNewStudentsInStudentsPNL.Background = brushAddNewWhite;
                }
            }
            else
            { clickOnAddBtn = false; }
        }

        private void btnEditStudentsInStudentsPNL_Click(object sender, RoutedEventArgs e)
        {
            clearAllTextBoxes();
            chnageBtnBackgroundImageAndHideAllPNL();
            var brushEditOrange = new ImageBrush();
            brushEditOrange.ImageSource = new BitmapImage(new Uri("../../Edit-Orange.png", UriKind.Relative));
            btnEditStudentsInStudentsPNL.Background = brushEditOrange;

            stkPanelEditStudentsForStuAff.Visibility = Visibility.Visible;

            DataTable dtForStudentsInEditPNL = Database.returnTable("select * from Students");
            dtForStudentsInEditPNL.Columns["stuid"].ColumnName = "Student ID";
            dtForStudentsInEditPNL.Columns["stuname"].ColumnName = "Student Name";
            dtForStudentsInEditPNL.Columns["acdid"].ColumnName = "Academic Year ID";
            dtForStudentsInEditPNL.Columns["acdname"].ColumnName = "Academic Year Name";
            dtForStudentsInEditPNL.Columns["deptid"].ColumnName = "Department ID";
            dtForStudentsInEditPNL.Columns["deptname"].ColumnName = "Department Name";

            DGForStudentsInEdit.ItemsSource = dtForStudentsInEditPNL.DefaultView;

            clickOnEditBtn = true;
            currentPnl = "EditStudents";
        }

        private void btnEditStudentsInStudentsPNL_MouseEnter(object sender, MouseEventArgs e)
        {
            var brushEditOrange = new ImageBrush();
            brushEditOrange.ImageSource = new BitmapImage(new Uri("../../Edit-Orange.png", UriKind.Relative));
            btnEditStudentsInStudentsPNL.Background = brushEditOrange;
        }

        private void btnEditStudentsInStudentsPNL_MouseLeave(object sender, MouseEventArgs e)
        {
            if (clickOnEditBtn == false)
            {
                if (currentPnl != "EditStudents")
                {
                    var brushEditWhite = new ImageBrush();
                    brushEditWhite.ImageSource = new BitmapImage(new Uri("../../Edit-White.png", UriKind.Relative));
                    btnEditStudentsInStudentsPNL.Background = brushEditWhite;
                }
            }
            else
            { clickOnEditBtn = false; }
        }

        private void btnRemoveStudentsInStudentsPNL_Click(object sender, RoutedEventArgs e)
        {
            clearAllTextBoxes();
            chnageBtnBackgroundImageAndHideAllPNL();
            var brushRemoveOrange = new ImageBrush();
            brushRemoveOrange.ImageSource = new BitmapImage(new Uri("../../Remove-Orange.png", UriKind.Relative));
            btnRemoveStudentsInStudentsPNL.Background = brushRemoveOrange;

            stkPanelRemoveStudentsForStuAff.Visibility = Visibility.Visible;

            DataTable dtForStudentsInRemovePNL = Database.returnTable("select * from Students");
            dtForStudentsInRemovePNL.Columns["stuid"].ColumnName = "Student ID";
            dtForStudentsInRemovePNL.Columns["stuname"].ColumnName = "Student Name";
            dtForStudentsInRemovePNL.Columns["acdid"].ColumnName = "Academic Year ID";
            dtForStudentsInRemovePNL.Columns["acdname"].ColumnName = "Academic Year Name";
            dtForStudentsInRemovePNL.Columns["deptid"].ColumnName = "Department ID";
            dtForStudentsInRemovePNL.Columns["deptname"].ColumnName = "Department Name";

            DGForStudentsInRemove.ItemsSource = dtForStudentsInRemovePNL.DefaultView;

            clickOnRemoveBtn = true;
            currentPnl = "RemoveStudents";
        }

        private void btnRemoveStudentsInStudentsPNL_MouseEnter(object sender, MouseEventArgs e)
        {
            var brushRemoveOrange = new ImageBrush();
            brushRemoveOrange.ImageSource = new BitmapImage(new Uri("../../Remove-Orange.png", UriKind.Relative));
            btnRemoveStudentsInStudentsPNL.Background = brushRemoveOrange;
        }

        private void btnRemoveStudentsInStudentsPNL_MouseLeave(object sender, MouseEventArgs e)
        {
            if (clickOnRemoveBtn == false)
            {
                if (currentPnl != "RemoveStudents")
                {
                    var brushRemoveWhite = new ImageBrush();
                    brushRemoveWhite.ImageSource = new BitmapImage(new Uri("../../Remove-White.png", UriKind.Relative));
                    btnRemoveStudentsInStudentsPNL.Background = brushRemoveWhite;
                }
            }
            else
            { clickOnRemoveBtn = false; }
        }

        private void txtBoxSearchInDatagridInEditStudentsPNL_KeyUp(object sender, KeyEventArgs e)
        {
            DataTable dtForStudentsEdit = Database.returnTable("select * from Students");
            DataTable newDTForStudentsEditBySearch = new DataTable();
            newDTForStudentsEditBySearch.Columns.Add("Student ID", typeof(System.String));
            newDTForStudentsEditBySearch.Columns.Add("Student Name", typeof(System.String));
            newDTForStudentsEditBySearch.Columns.Add("Academic Year ID", typeof(System.String));
            newDTForStudentsEditBySearch.Columns.Add("Academic Year Name", typeof(System.String));
            newDTForStudentsEditBySearch.Columns.Add("Department ID", typeof(System.String));
            newDTForStudentsEditBySearch.Columns.Add("Department Name", typeof(System.String));

            for (int i = 0; i < dtForStudentsEdit.Rows.Count; i++)
            {
                bool found = false;
                for (int j = 0; j < dtForStudentsEdit.Columns.Count; j++)
                {
                    if (found == false)
                    { 
                        try
                        {
                            string searchText = txtBoxSearchInDatagridInEditStudentsPNL.Text;
                            int numOfCharOfSearchValue = searchText.Length;
                            string valueOfCurrentCell = Convert.ToString(dtForStudentsEdit.Rows[i][j]);
                            string lastValueOfCellForCompare = valueOfCurrentCell.Substring(0, numOfCharOfSearchValue);

                            if (searchText == lastValueOfCellForCompare)
                            {
                                DataRow dr = newDTForStudentsEditBySearch.NewRow();
                                dr[0] = dtForStudentsEdit.Rows[i][0];
                                dr[1] = dtForStudentsEdit.Rows[i][1];
                                dr[2] = dtForStudentsEdit.Rows[i][2];
                                dr[3] = dtForStudentsEdit.Rows[i][3];
                                dr[4] = dtForStudentsEdit.Rows[i][4];
                                dr[5] = dtForStudentsEdit.Rows[i][5];
                                newDTForStudentsEditBySearch.Rows.Add(dr);
                            }
                        }
                        catch (Exception)
                        { }
                    }
                }
            }

            if (txtBoxSearchInDatagridInEditStudentsPNL.Text.Trim() == "")
            {
                DataTable dtForStudentsInEditPNL = Database.returnTable("select * from Students");
                dtForStudentsInEditPNL.Columns["stuid"].ColumnName = "Student ID";
                dtForStudentsInEditPNL.Columns["stuname"].ColumnName = "Student Name";
                dtForStudentsInEditPNL.Columns["acdid"].ColumnName = "Academic Year ID";
                dtForStudentsInEditPNL.Columns["acdname"].ColumnName = "Academic Year Name";
                dtForStudentsInEditPNL.Columns["deptid"].ColumnName = "Department ID";
                dtForStudentsInEditPNL.Columns["deptname"].ColumnName = "Department Name";

                DGForStudentsInEdit.ItemsSource = dtForStudentsInEditPNL.DefaultView;
            }
            else
            { DGForStudentsInEdit.ItemsSource = newDTForStudentsEditBySearch.DefaultView; }
        }

        private void txtBoxSearchInDatagridInRemoveStudentsPNL_KeyUp(object sender, KeyEventArgs e)
        {
            DataTable dtForStudentsRemove = Database.returnTable("select * from Students");
            DataTable newDTForStudentsRemoveBySearch = new DataTable();
            newDTForStudentsRemoveBySearch.Columns.Add("Student ID", typeof(System.String));
            newDTForStudentsRemoveBySearch.Columns.Add("Student Name", typeof(System.String));
            newDTForStudentsRemoveBySearch.Columns.Add("Academic Year ID", typeof(System.String));
            newDTForStudentsRemoveBySearch.Columns.Add("Academic Year Name", typeof(System.String));
            newDTForStudentsRemoveBySearch.Columns.Add("Department ID", typeof(System.String));
            newDTForStudentsRemoveBySearch.Columns.Add("Department Name", typeof(System.String));

            for (int i = 0; i < dtForStudentsRemove.Rows.Count; i++)
            {
                bool found = false;
                for (int j = 0; j < dtForStudentsRemove.Columns.Count; j++)
                {
                    if (found == false) 
                    {
                        try
                        {
                            string searchText = txtBoxSearchInDatagridInRemoveStudentsPNL.Text;
                            int numOfCharOfSearchValue = searchText.Length;
                            string valueOfCurrentCell = Convert.ToString(dtForStudentsRemove.Rows[i][j]);
                            string lastValueOfCellForCompare = valueOfCurrentCell.Substring(0, numOfCharOfSearchValue);

                            if (searchText == lastValueOfCellForCompare)
                            {
                                DataRow dr = newDTForStudentsRemoveBySearch.NewRow();
                                dr[0] = dtForStudentsRemove.Rows[i][0];
                                dr[1] = dtForStudentsRemove.Rows[i][1];
                                dr[2] = dtForStudentsRemove.Rows[i][2];
                                dr[3] = dtForStudentsRemove.Rows[i][3];
                                dr[4] = dtForStudentsRemove.Rows[i][4];
                                dr[5] = dtForStudentsRemove.Rows[i][5];
                                newDTForStudentsRemoveBySearch.Rows.Add(dr);
                            }
                        }
                        catch (Exception)
                        { }
                    }
                }
            }

            if (txtBoxSearchInDatagridInRemoveStudentsPNL.Text.Trim() == "")
            {
                stkPanelRemoveStudentsForStuAff.Visibility = Visibility.Visible;

                DataTable dtForStudentsInRemovePNL = Database.returnTable("select * from Students");
                dtForStudentsInRemovePNL.Columns["stuid"].ColumnName = "Student ID";
                dtForStudentsInRemovePNL.Columns["stuname"].ColumnName = "Student Name";
                dtForStudentsInRemovePNL.Columns["acdid"].ColumnName = "Academic Year ID";
                dtForStudentsInRemovePNL.Columns["acdname"].ColumnName = "Academic Year Name";
                dtForStudentsInRemovePNL.Columns["deptid"].ColumnName = "Department ID";
                dtForStudentsInRemovePNL.Columns["deptname"].ColumnName = "Department Name";

                DGForStudentsInRemove.ItemsSource = dtForStudentsInRemovePNL.DefaultView;
            }
            else
            { DGForStudentsInRemove.ItemsSource = newDTForStudentsRemoveBySearch.DefaultView; }
        }

        public void clearAllTextBoxes()
        {
            txtBoxStudentsNameInStudentsPnl.Text = "";
            txtBoxStudentsIDInStudentsPnl.Text = "";
            txtBoxSearchInDatagridInStudentsPNL.Text = "";
            txtBoxSearchInDatagridInEditStudentsPNL.Text = "";
            txtBoxSearchInDatagridInRemoveStudentsPNL.Text = "";

            DataTable dtForAcdYears = Database.returnTable("select * from AcademicYears");
            cmbBoxAcdYearIDInStudentsPnl.ItemsSource = dtForAcdYears.DefaultView;
            cmbBoxAcdYearIDInStudentsPnl.DisplayMemberPath = "acdid";

            cmbBoxAcdYearNameInStudentsPnl.ItemsSource = dtForAcdYears.DefaultView;
            cmbBoxAcdYearNameInStudentsPnl.DisplayMemberPath = "acdname";


            DataTable dtForDepartment = Database.returnTable("select * from Department");
            cmbBoxDeptIDInStudentsPnl.ItemsSource = dtForDepartment.DefaultView;
            cmbBoxDeptIDInStudentsPnl.DisplayMemberPath = "deptid";

            cmbBoxDeptNameInStudentsPnl.ItemsSource = dtForDepartment.DefaultView;
            cmbBoxDeptNameInStudentsPnl.DisplayMemberPath = "deptname";
        }

        private void Grid_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            clearAllTextBoxes();
            chnageBtnBackgroundImageAndHideAllPNL();
            var brushHomeOrange = new ImageBrush();
            brushHomeOrange.ImageSource = new BitmapImage(new Uri("../../Home-Orange.png", UriKind.Relative));
            btnHomeInStudentsPNL.Background = brushHomeOrange;

            stkPanelHomeStudentsForStuAff.Visibility = Visibility.Visible;

            DataTable dtForStudents = Database.returnTable("select * from Students");
            dtForStudents.Columns["stuid"].ColumnName = "Student ID";
            dtForStudents.Columns["stuname"].ColumnName = "Student Name";
            dtForStudents.Columns["acdid"].ColumnName = "Academic Year ID";
            dtForStudents.Columns["acdname"].ColumnName = "Academic Year Name";
            dtForStudents.Columns["deptid"].ColumnName = "Department ID";
            dtForStudents.Columns["deptname"].ColumnName = "Department Name";

            DGForStudents.ItemsSource = dtForStudents.DefaultView;

            clickOnHomeBtn = true;
            currentPnl = "HomeStudents";
        }

        private void cmbBoxAcdYearIDInStudentsPnl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        { cmbBoxAcdYearNameInStudentsPnl.SelectedIndex = cmbBoxAcdYearIDInStudentsPnl.SelectedIndex; }

        private void cmbBoxAcdYearNameInStudentsPnl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        { cmbBoxAcdYearIDInStudentsPnl.SelectedIndex = cmbBoxAcdYearNameInStudentsPnl.SelectedIndex; }

        private void cmbBoxDeptIDInStudentsPnl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        { cmbBoxDeptNameInStudentsPnl.SelectedIndex = cmbBoxDeptIDInStudentsPnl.SelectedIndex; }

        private void cmbBoxDeptNameInStudentsPnl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        { cmbBoxDeptIDInStudentsPnl.SelectedIndex = cmbBoxDeptNameInStudentsPnl.SelectedIndex; }


    }
}
