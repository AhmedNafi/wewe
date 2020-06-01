using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for ucDepartmentForStuAff.xaml
    /// </summary>
    public partial class ucDepartmentForStuAff : UserControl
    {
        public ucDepartmentForStuAff()
        {
            InitializeComponent();

            clearAllTextBoxes();
            chnageBtnBackgroundImageAndHideAllPNL();
            var brushHomeOrange = new ImageBrush();
            brushHomeOrange.ImageSource = new BitmapImage(new Uri("../../Home-Orange.png", UriKind.Relative));
            btnHomeInDeptPNL.Background = brushHomeOrange;

            stkPanelHomeDepartmentForStuAff.Visibility = Visibility.Visible;

            DataTable dtForDepartment = Database.returnTable("select * from Department");
            dtForDepartment.Columns["deptid"].ColumnName = "Department ID";
            dtForDepartment.Columns["deptname"].ColumnName = "Department Name";

            DGForDepartment.ItemsSource = dtForDepartment.DefaultView;

            clickOnHomeBtn = true;
            currentPnl = "HomeDept";
        }


        private void btnSubmitAddNewDept_Click(object sender, RoutedEventArgs e)
        {
            //Get Number Of Rows In Table To Check If Department ID Is Exist
            bool idIsExist = false;
            DataTable dtForDepartment = Database.returnTable("select * from Department");
            int numOfRowsOfdtForDept = dtForDepartment.Rows.Count;

            if (txtBoxDeptIDInDeptPnl.Text.Trim() == "")
            { MessageBox.Show("Please Enter Department ID", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
            else if (txtBoxDeptNameInDeptPnl.Text.Trim() == "")
            { MessageBox.Show("Please Enter Department Name", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
            else
            {
                string newDeptID = txtBoxDeptIDInDeptPnl.Text;

                for (int i = 0; i < numOfRowsOfdtForDept; i++)
                { if (newDeptID == Convert.ToString(dtForDepartment.Rows[i][0])) { idIsExist = true; } }

                if (idIsExist == true)
                { MessageBox.Show("Your Department ID already Exist.\nPlease Enter Another Department ID", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }

                else
                {
                    MessageBoxResult resMakeNewDept = MessageBox.Show("Are You Sure To Add New Department?", "New Department", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    if (resMakeNewDept == MessageBoxResult.Yes)
                    {
                        try
                        {
                            Database.Run("insert into Department values('" + txtBoxDeptIDInDeptPnl.Text + "', '" + txtBoxDeptNameInDeptPnl.Text + "')");
                            txtBoxDeptIDInDeptPnl.Text = "";
                            txtBoxDeptNameInDeptPnl.Text = "";
                            MessageBox.Show("The New Department Has Been Successfully Added", "New Department", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        catch (Exception ex)
                        { MessageBox.Show("Error here" + ex.Message, "New Department", MessageBoxButton.OK, MessageBoxImage.Error); }
                    }
                    else if (resMakeNewDept == MessageBoxResult.No)
                    { }
                }
            }
        }

        private void DGForDepartment_LoadingRow(object sender, DataGridRowEventArgs e)
        { e.Row.Header = (e.Row.GetIndex() + 1).ToString(); }

        private void DGForDepartmentInEdit_LoadingRow(object sender, DataGridRowEventArgs e)
        { e.Row.Header = (e.Row.GetIndex() + 1).ToString(); }

        private void txtBoxSearchInDatagridInDeptPNL_KeyUp(object sender, KeyEventArgs e)
        {
            DataTable dtForDepartment = Database.returnTable("select * from Department");
            DataTable newDTForDepartmentBySearch = new DataTable();
            newDTForDepartmentBySearch.Columns.Add("Department ID", typeof(System.String));
            newDTForDepartmentBySearch.Columns.Add("Department Name", typeof(System.String));

            for (int i = 0; i < dtForDepartment.Rows.Count; i++)
            {
                bool found = false;
                for (int j = 0; j < dtForDepartment.Columns.Count; j++)
                {
                    if (found == false)
                    { 
                        try
                        {
                            string searchText = txtBoxSearchInDatagridInDeptPNL.Text;
                            int numOfCharOfSearchValue = searchText.Length;
                            string valueOfCurrentCell = Convert.ToString(dtForDepartment.Rows[i][j]);
                            string lastValueOfCellForCompare = valueOfCurrentCell.Substring(0, numOfCharOfSearchValue);

                            if (searchText == lastValueOfCellForCompare)
                            {
                                DataRow dr = newDTForDepartmentBySearch.NewRow();
                                dr[0] = dtForDepartment.Rows[i][0];
                                dr[1] = dtForDepartment.Rows[i][1];
                                newDTForDepartmentBySearch.Rows.Add(dr);
                                found = true;
                            }
                        }
                        catch (Exception)
                        { }
                    }
                }
            }

            if (txtBoxSearchInDatagridInDeptPNL.Text.Trim() == "")
            {
                dtForDepartment.Columns["deptid"].ColumnName = "Department ID";
                dtForDepartment.Columns["deptname"].ColumnName = "Department Name";

                DGForDepartment.ItemsSource = dtForDepartment.DefaultView;
            }
            else
            { DGForDepartment.ItemsSource = newDTForDepartmentBySearch.DefaultView; }
        }

        public void chnageBtnBackgroundImageAndHideAllPNL()
        {
            var brushHomeWhite = new ImageBrush();
            brushHomeWhite.ImageSource = new BitmapImage(new Uri("../../Home-White.png", UriKind.Relative));
            btnHomeInDeptPNL.Background = brushHomeWhite;

            var brushAddWhite = new ImageBrush();
            brushAddWhite.ImageSource = new BitmapImage(new Uri("../../Add-White.png", UriKind.Relative));
            btnAddNewDeptInDeptPNL.Background = brushAddWhite;

            var brushEditWhite = new ImageBrush();
            brushEditWhite.ImageSource = new BitmapImage(new Uri("../../Edit-White.png", UriKind.Relative));
            btnEditDeptInDeptPNL.Background = brushEditWhite;

            var brushRemoveWhite = new ImageBrush();
            brushRemoveWhite.ImageSource = new BitmapImage(new Uri("../../Remove-White.png", UriKind.Relative));
            btnRemoveDeptInDeptPNL.Background = brushRemoveWhite;

            stkPanelHomeDepartmentForStuAff.Visibility = Visibility.Hidden;
            stkPanelAddDepartmentForStuAff.Visibility = Visibility.Hidden;
            stkPanelEditDepartmentForStuAff.Visibility = Visibility.Hidden;
            stkPanelRemoveDepartmentForStuAff.Visibility = Visibility.Hidden;

            txtBoxSearchInDatagridInDeptPNL.Text = "";
            txtBoxSearchInDatagridInEditDeptPNL.Text = "";
            txtBoxSearchInDatagridInRemoveDeptPNL.Text = "";
        }

        bool clickOnHomeBtn = false;
        bool clickOnAddBtn = false;
        bool clickOnEditBtn = false;
        bool clickOnRemoveBtn = false;
        string currentPnl = "HomeDept";


        private void btnHomeInDeptPNL_Click(object sender, RoutedEventArgs e)
        {
            clearAllTextBoxes();
            chnageBtnBackgroundImageAndHideAllPNL();
            var brushHomeOrange = new ImageBrush();
            brushHomeOrange.ImageSource = new BitmapImage(new Uri("../../Home-Orange.png", UriKind.Relative));
            btnHomeInDeptPNL.Background = brushHomeOrange;

            stkPanelHomeDepartmentForStuAff.Visibility = Visibility.Visible;

            DataTable dtForDepartment = Database.returnTable("select * from Department");
            dtForDepartment.Columns["deptid"].ColumnName = "Department ID";
            dtForDepartment.Columns["deptname"].ColumnName = "Department Name";

            DGForDepartment.ItemsSource = dtForDepartment.DefaultView;

            clickOnHomeBtn = true;
            currentPnl = "HomeDept";
        }

        private void btnHomeInDeptPNL_MouseEnter(object sender, MouseEventArgs e)
        {
            var brushHomeOrange = new ImageBrush();
            brushHomeOrange.ImageSource = new BitmapImage(new Uri("../../Home-Orange.png", UriKind.Relative));
            btnHomeInDeptPNL.Background = brushHomeOrange;
        }

        private void btnHomeInDeptPNL_MouseLeave(object sender, MouseEventArgs e)
        {
            if (clickOnHomeBtn == false)
            {
                if (currentPnl != "HomeDept")
                {
                    var brushHomeWhite = new ImageBrush();
                    brushHomeWhite.ImageSource = new BitmapImage(new Uri("../../Home-White.png", UriKind.Relative));
                    btnHomeInDeptPNL.Background = brushHomeWhite;
                }
            }
            else
            { clickOnHomeBtn = false; }
        }

        private void btnAddNewDeptInDeptPNL_Click(object sender, RoutedEventArgs e)
        {
            clearAllTextBoxes();
            chnageBtnBackgroundImageAndHideAllPNL();
            var brushAddOrange = new ImageBrush();
            brushAddOrange.ImageSource = new BitmapImage(new Uri("../../Add-Orange.png", UriKind.Relative));
            btnAddNewDeptInDeptPNL.Background = brushAddOrange;

            stkPanelAddDepartmentForStuAff.Visibility = Visibility.Visible;

            clickOnAddBtn = true;
            currentPnl = "AddDept";
        }

        private void btnAddNewDeptInDeptPNL_MouseEnter(object sender, MouseEventArgs e)
        {
            var brushAddNewOrange = new ImageBrush();
            brushAddNewOrange.ImageSource = new BitmapImage(new Uri("../../Add-Orange.png", UriKind.Relative));
            btnAddNewDeptInDeptPNL.Background = brushAddNewOrange;
        }

        private void btnAddNewDeptInDeptPNL_MouseLeave(object sender, MouseEventArgs e)
        {
            if (clickOnAddBtn == false)
            {
                if (currentPnl != "AddDept")
                {
                    var brushAddNewWhite = new ImageBrush();
                    brushAddNewWhite.ImageSource = new BitmapImage(new Uri("../../Add-White.png", UriKind.Relative));
                    btnAddNewDeptInDeptPNL.Background = brushAddNewWhite;
                }
            }
            else
            { clickOnAddBtn = false; }
        }

        private void btnEditDeptInDeptPNL_Click(object sender, RoutedEventArgs e)
        {
            clearAllTextBoxes();
            chnageBtnBackgroundImageAndHideAllPNL();
            var brushEditOrange = new ImageBrush();
            brushEditOrange.ImageSource = new BitmapImage(new Uri("../../Edit-Orange.png", UriKind.Relative));
            btnEditDeptInDeptPNL.Background = brushEditOrange;

            stkPanelEditDepartmentForStuAff.Visibility = Visibility.Visible;

            DataTable dtForDepartmentInEditPNL = Database.returnTable("select * from Department");
            dtForDepartmentInEditPNL.Columns["deptid"].ColumnName = "Department ID";
            dtForDepartmentInEditPNL.Columns["deptname"].ColumnName = "Department Name";

            DGForDepartmentInEdit.ItemsSource = dtForDepartmentInEditPNL.DefaultView;

            clickOnEditBtn = true;
            currentPnl = "EditDept";
        }

        private void btnEditDeptInDeptPNL_MouseEnter(object sender, MouseEventArgs e)
        {
            var brushEditOrange = new ImageBrush();
            brushEditOrange.ImageSource = new BitmapImage(new Uri("../../Edit-Orange.png", UriKind.Relative));
            btnEditDeptInDeptPNL.Background = brushEditOrange;
        }

        private void btnEditDeptInDeptPNL_MouseLeave(object sender, MouseEventArgs e)
        {
            if (clickOnEditBtn == false)
            {
                if (currentPnl != "EditDept")
                {
                    var brushEditWhite = new ImageBrush();
                    brushEditWhite.ImageSource = new BitmapImage(new Uri("../../Edit-White.png", UriKind.Relative));
                    btnEditDeptInDeptPNL.Background = brushEditWhite;
                }
            }
            else
            { clickOnEditBtn = false; }
        }

        private void btnRemoveDeptInDeptPNL_Click(object sender, RoutedEventArgs e)
        {
            clearAllTextBoxes();
            chnageBtnBackgroundImageAndHideAllPNL();
            var brushRemoveOrange = new ImageBrush();
            brushRemoveOrange.ImageSource = new BitmapImage(new Uri("../../Remove-Orange.png", UriKind.Relative));
            btnRemoveDeptInDeptPNL.Background = brushRemoveOrange;

            stkPanelRemoveDepartmentForStuAff.Visibility = Visibility.Visible;

            DataTable dtForDepartmentInRemovePNL = Database.returnTable("select * from Department");
            dtForDepartmentInRemovePNL.Columns["deptid"].ColumnName = "Department ID";
            dtForDepartmentInRemovePNL.Columns["deptname"].ColumnName = "Department Name";

            DGForDepartmentInRemove.ItemsSource = dtForDepartmentInRemovePNL.DefaultView;

            clickOnRemoveBtn = true;
            currentPnl = "RemoveDept";
        }

        private void btnRemoveDeptInDeptPNL_MouseEnter(object sender, MouseEventArgs e)
        {
            var brushRemoveOrange = new ImageBrush();
            brushRemoveOrange.ImageSource = new BitmapImage(new Uri("../../Remove-Orange.png", UriKind.Relative));
            btnRemoveDeptInDeptPNL.Background = brushRemoveOrange;
        }

        private void btnRemoveDeptInDeptPNL_MouseLeave(object sender, MouseEventArgs e)
        {
            if (clickOnRemoveBtn == false)
            {
                if (currentPnl != "RemoveDept")
                {
                    var brushRemoveWhite = new ImageBrush();
                    brushRemoveWhite.ImageSource = new BitmapImage(new Uri("../../Remove-White.png", UriKind.Relative));
                    btnRemoveDeptInDeptPNL.Background = brushRemoveWhite;
                }
            }
            else
            { clickOnRemoveBtn = false; }
        }

        private void txtBoxSearchInDatagridInEditDeptPNL_KeyUp(object sender, KeyEventArgs e)
        {
            DataTable dtForDepartmentEdit = Database.returnTable("select * from Department");
            DataTable newDTForDepartmentEditBySearch = new DataTable();
            newDTForDepartmentEditBySearch.Columns.Add("Department ID", typeof(System.String));
            newDTForDepartmentEditBySearch.Columns.Add("Department Name", typeof(System.String));

            for (int i = 0; i < dtForDepartmentEdit.Rows.Count; i++)
            {
                bool found = false;
                for (int j = 0; j < dtForDepartmentEdit.Columns.Count; j++)
                {
                    if (found == false)
                    { 
                        try
                        {
                            string searchText = txtBoxSearchInDatagridInEditDeptPNL.Text;
                            int numOfCharOfSearchValue = searchText.Length;
                            string valueOfCurrentCell = Convert.ToString(dtForDepartmentEdit.Rows[i][j]);
                            string lastValueOfCellForCompare = valueOfCurrentCell.Substring(0, numOfCharOfSearchValue);

                            if (searchText == lastValueOfCellForCompare)
                            {
                                DataRow dr = newDTForDepartmentEditBySearch.NewRow();
                                dr[0] = dtForDepartmentEdit.Rows[i][0];
                                dr[1] = dtForDepartmentEdit.Rows[i][1];
                                newDTForDepartmentEditBySearch.Rows.Add(dr);
                                found = true;
                            }
                        }
                        catch (Exception)
                        { }
                    }
                }
            }

            if (txtBoxSearchInDatagridInEditDeptPNL.Text.Trim() == "")
            {
                DataTable dtForDepartmentInEditPNL = Database.returnTable("select * from Department");
                dtForDepartmentInEditPNL.Columns["deptid"].ColumnName = "Department ID";
                dtForDepartmentInEditPNL.Columns["deptname"].ColumnName = "Department Name";

                DGForDepartmentInEdit.ItemsSource = dtForDepartmentInEditPNL.DefaultView;
            }
            else
            { DGForDepartmentInEdit.ItemsSource = newDTForDepartmentEditBySearch.DefaultView; }
        }

        private void txtBoxSearchInDatagridInRemoveDeptPNL_KeyUp(object sender, KeyEventArgs e)
        {
            DataTable dtForDepartmentRemove = Database.returnTable("select * from Department");
            DataTable newDTForDepartmentRemoveBySearch = new DataTable();
            newDTForDepartmentRemoveBySearch.Columns.Add("Department ID", typeof(System.String));
            newDTForDepartmentRemoveBySearch.Columns.Add("Department Name", typeof(System.String));

            for (int i = 0; i < dtForDepartmentRemove.Rows.Count; i++)
            {
                bool found = false;
                for (int j = 0; j < dtForDepartmentRemove.Columns.Count; j++)
                {
                    if (found == false)
                    { 
                        try
                        {
                            string searchText = txtBoxSearchInDatagridInRemoveDeptPNL.Text;
                            int numOfCharOfSearchValue = searchText.Length;
                            string valueOfCurrentCell = Convert.ToString(dtForDepartmentRemove.Rows[i][j]);
                            string lastValueOfCellForCompare = valueOfCurrentCell.Substring(0, numOfCharOfSearchValue);

                            if (searchText == lastValueOfCellForCompare)
                            {
                                DataRow dr = newDTForDepartmentRemoveBySearch.NewRow();
                                dr[0] = dtForDepartmentRemove.Rows[i][0];
                                dr[1] = dtForDepartmentRemove.Rows[i][1];
                                newDTForDepartmentRemoveBySearch.Rows.Add(dr);
                                found = true;
                            }
                        }
                        catch (Exception)
                        { }
                    }
                }
            }

            if (txtBoxSearchInDatagridInRemoveDeptPNL.Text.Trim() == "")
            {
                DataTable dtForDepartmentInRemovePNL = Database.returnTable("select * from Department");
                dtForDepartmentInRemovePNL.Columns["deptid"].ColumnName = "Department ID";
                dtForDepartmentInRemovePNL.Columns["deptname"].ColumnName = "Department Name";

                DGForDepartmentInRemove.ItemsSource = dtForDepartmentInRemovePNL.DefaultView;
            }
            else
            { DGForDepartmentInRemove.ItemsSource = newDTForDepartmentRemoveBySearch.DefaultView; }
        }

        public void clearAllTextBoxes()
        {
            txtBoxDeptNameInDeptPnl.Text = "";
            txtBoxDeptIDInDeptPnl.Text = "";
            txtBoxSearchInDatagridInDeptPNL.Text = "";
            txtBoxSearchInDatagridInEditDeptPNL.Text = "";
            txtBoxSearchInDatagridInRemoveDeptPNL.Text = "";
        }

        private void Grid_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            clearAllTextBoxes();
            chnageBtnBackgroundImageAndHideAllPNL();
            var brushHomeOrange = new ImageBrush();
            brushHomeOrange.ImageSource = new BitmapImage(new Uri("../../Home-Orange.png", UriKind.Relative));
            btnHomeInDeptPNL.Background = brushHomeOrange;

            stkPanelHomeDepartmentForStuAff.Visibility = Visibility.Visible;

            DataTable dtForDepartment = Database.returnTable("select * from Department");
            dtForDepartment.Columns["deptid"].ColumnName = "Department ID";
            dtForDepartment.Columns["deptname"].ColumnName = "Department Name";

            DGForDepartment.ItemsSource = dtForDepartment.DefaultView;

            clickOnHomeBtn = true;
            currentPnl = "HomeDept";
        }
    }
}
