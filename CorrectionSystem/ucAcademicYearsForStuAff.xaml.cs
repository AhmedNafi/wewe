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
    /// Interaction logic for ucAcademicYearsForStuAff.xaml
    /// </summary>
    public partial class ucAcademicYearsForStuAff : UserControl
    {
        public ucAcademicYearsForStuAff()
        {
            InitializeComponent();

            clearAllTextBoxes();
            chnageBtnBackgroundImageAndHideAllPNL();
            var brushHomeOrange = new ImageBrush();
            brushHomeOrange.ImageSource = new BitmapImage(new Uri("../../Home-Orange.png", UriKind.Relative));
            btnHomeInAcdYearsPNL.Background = brushHomeOrange;

            stkPanelHomeAcdYearsForStuAff.Visibility = Visibility.Visible;
            
            DataTable dtForAcdYears = Database.returnTable("select * from AcademicYears");
            dtForAcdYears.Columns["acdid"].ColumnName = "Academic Year ID";
            dtForAcdYears.Columns["acdname"].ColumnName = "Academic Year Name";

            DGForAcdYears.ItemsSource = dtForAcdYears.DefaultView;

            clickOnHomeBtn = true;
            currentPnl = "HomeAcdYears";
        }




        private void btnSubmitAddNewAcdYears_Click(object sender, RoutedEventArgs e)
        {
            //Get Number Of Rows In Table To Check If AcdYears Number Is Exist
            bool idIsExist = false;
            DataTable dtForAcdYears = Database.returnTable("select * from AcademicYears");
            int numOfRowsOfdtForAcdYears = dtForAcdYears.Rows.Count;

            if (txtBoxAcdYearsIDInAcdYearsPnl.Text.Trim() == "")
            { MessageBox.Show("Please Enter Academic Year ID", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
            else if (txtBoxAcdYearsNameInAcdYearsPnl.Text.Trim() == "")
            { MessageBox.Show("Please Enter Academic Year Name", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
            else
            {
                string newAcdYearsID = txtBoxAcdYearsIDInAcdYearsPnl.Text;

                for (int i = 0; i < numOfRowsOfdtForAcdYears; i++)
                { if (newAcdYearsID == Convert.ToString(dtForAcdYears.Rows[i][0])) { idIsExist = true; } }

                if (idIsExist == true)
                { MessageBox.Show("Your Academic Year ID already Exist.\nPlease Enter Another Academic Year ID", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
                else
                {
                    MessageBoxResult resMakeNewAcdYears = MessageBox.Show("Are You Sure To Add New Academic Year?", "New Academic Year", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    if (resMakeNewAcdYears == MessageBoxResult.Yes)
                    {
                        try
                        {
                            Database.Run("insert into AcademicYears values('" + txtBoxAcdYearsIDInAcdYearsPnl.Text + "', '" + txtBoxAcdYearsNameInAcdYearsPnl.Text + "')");
                            txtBoxAcdYearsIDInAcdYearsPnl.Text = "";
                            txtBoxAcdYearsNameInAcdYearsPnl.Text = "";
                            MessageBox.Show("The New Academic Year Has Been Successfully Added", "New Academic Year", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        catch (Exception ex)
                        { MessageBox.Show("Error here" + ex.Message, "New Academic Year", MessageBoxButton.OK, MessageBoxImage.Error); }
                    }
                    else if (resMakeNewAcdYears == MessageBoxResult.No)
                    { }
                }
            }
        }

        private void DGForAcdYears_LoadingRow(object sender, DataGridRowEventArgs e)
        { e.Row.Header = (e.Row.GetIndex() + 1).ToString(); }

        private void txtBoxSearchInDatagridInAcdYearsPNL_KeyUp(object sender, KeyEventArgs e)
        {
            DataTable dtForAcdYears = Database.returnTable("select * from AcademicYears");
            DataTable newDTForAcdYearsBySearch = new DataTable();
            newDTForAcdYearsBySearch.Columns.Add("Academic Year ID", typeof(System.String));
            newDTForAcdYearsBySearch.Columns.Add("Academic Year Name", typeof(System.String));

            for (int i = 0; i < dtForAcdYears.Rows.Count; i++)
            {
                bool found = false;
                for (int j = 0; j < dtForAcdYears.Columns.Count; j++)
                {
                    if (found == false)
                    { 
                        try
                        {
                            string searchText = txtBoxSearchInDatagridInAcdYearsPNL.Text;
                            int numOfCharOfSearchValue = searchText.Length;
                            string valueOfCurrentCell = Convert.ToString(dtForAcdYears.Rows[i][j]);
                            string lastValueOfCellForCompare = valueOfCurrentCell.Substring(0, numOfCharOfSearchValue);

                            if (searchText == lastValueOfCellForCompare)
                            {
                                DataRow dr = newDTForAcdYearsBySearch.NewRow();
                                dr[0] = dtForAcdYears.Rows[i][0];
                                dr[1] = dtForAcdYears.Rows[i][1];
                                newDTForAcdYearsBySearch.Rows.Add(dr);
                                found = true;
                            }
                        }
                        catch (Exception)
                        { }
                    }
                  
                }
            }

            int numOfRowsOfdtForAcdYearsNew = newDTForAcdYearsBySearch.Rows.Count;

            if (txtBoxSearchInDatagridInAcdYearsPNL.Text.Trim() == "")
            {
                dtForAcdYears.Columns["acdid"].ColumnName = "Academic Year ID";
                dtForAcdYears.Columns["acdname"].ColumnName = "Academic Year Name";

                DGForAcdYears.ItemsSource = dtForAcdYears.DefaultView;
            }
            else
            { DGForAcdYears.ItemsSource = newDTForAcdYearsBySearch.DefaultView; }
        }

        public void chnageBtnBackgroundImageAndHideAllPNL()
        {
            var brushHomeWhite = new ImageBrush();
            brushHomeWhite.ImageSource = new BitmapImage(new Uri("../../Home-White.png", UriKind.Relative));
            btnHomeInAcdYearsPNL.Background = brushHomeWhite;

            var brushAddWhite = new ImageBrush();
            brushAddWhite.ImageSource = new BitmapImage(new Uri("../../Add-White.png", UriKind.Relative));
            btnAddNewAcdYearInAcdYearsPNL.Background = brushAddWhite;

            var brushEditWhite = new ImageBrush();
            brushEditWhite.ImageSource = new BitmapImage(new Uri("../../Edit-White.png", UriKind.Relative));
            btnEditAcdYearInAcdYearsPNL.Background = brushEditWhite;

            var brushRemoveWhite = new ImageBrush();
            brushRemoveWhite.ImageSource = new BitmapImage(new Uri("../../Remove-White.png", UriKind.Relative));
            btnRemoveAcdYearInAcdYearsPNL.Background = brushRemoveWhite;

            stkPanelHomeAcdYearsForStuAff.Visibility = Visibility.Hidden;
            stkPanelAddAcdYearsForStuAff.Visibility = Visibility.Hidden;
            stkPanelEditAcdYearsForStuAff.Visibility = Visibility.Hidden;
            stkPanelRemoveAcdYearsForStuAff.Visibility = Visibility.Hidden;

            txtBoxSearchInDatagridInAcdYearsPNL.Text = "";
            txtBoxSearchInDatagridInEditAcdYearsPNL.Text = "";
            txtBoxSearchInDatagridInRemoveAcdYearsPNL.Text = "";
        }

        bool clickOnHomeBtn = false;
        bool clickOnAddBtn = false;
        bool clickOnEditBtn = false;
        bool clickOnRemoveBtn = false;
        string currentPnl = "HomeAcdYears";


        private void btnHomeInAcdYearsPNL_Click(object sender, RoutedEventArgs e)
        {
            clearAllTextBoxes();
            chnageBtnBackgroundImageAndHideAllPNL();
            var brushHomeOrange = new ImageBrush();
            brushHomeOrange.ImageSource = new BitmapImage(new Uri("../../Home-Orange.png", UriKind.Relative));
            btnHomeInAcdYearsPNL.Background = brushHomeOrange;

            stkPanelHomeAcdYearsForStuAff.Visibility = Visibility.Visible;

            DataTable dtForAcdYears = Database.returnTable("select * from AcademicYears");
            dtForAcdYears.Columns["acdid"].ColumnName = "Academic Year ID";
            dtForAcdYears.Columns["acdname"].ColumnName = "Academic Year Name";

            DGForAcdYears.ItemsSource = dtForAcdYears.DefaultView;

            clickOnHomeBtn = true;
            currentPnl = "HomeAcdYears";
        }

        private void btnHomeInAcdYearsPNL_MouseEnter(object sender, MouseEventArgs e)
        {
            var brushHomeOrange = new ImageBrush();
            brushHomeOrange.ImageSource = new BitmapImage(new Uri("../../Home-Orange.png", UriKind.Relative));
            btnHomeInAcdYearsPNL.Background = brushHomeOrange;
        }

        private void btnHomeInAcdYearsPNL_MouseLeave(object sender, MouseEventArgs e)
        {
            if (clickOnHomeBtn == false)
            {
                if (currentPnl != "HomeAcdYears")
                {
                    var brushHomeWhite = new ImageBrush();
                    brushHomeWhite.ImageSource = new BitmapImage(new Uri("../../Home-White.png", UriKind.Relative));
                    btnHomeInAcdYearsPNL.Background = brushHomeWhite;
                }
            }
            else
            { clickOnHomeBtn = false; }
        }

        private void btnAddNewAcdYearInAcdYearsPNL_Click(object sender, RoutedEventArgs e)
        {
            clearAllTextBoxes();
            chnageBtnBackgroundImageAndHideAllPNL();
            var brushAddOrange = new ImageBrush();
            brushAddOrange.ImageSource = new BitmapImage(new Uri("../../Add-Orange.png", UriKind.Relative));
            btnAddNewAcdYearInAcdYearsPNL.Background = brushAddOrange;

            stkPanelAddAcdYearsForStuAff.Visibility = Visibility.Visible;

            clickOnAddBtn = true;
            currentPnl = "AddAcdYears";
        }

        private void btnAddNewAcdYearInAcdYearsPNL_MouseEnter(object sender, MouseEventArgs e)
        {
            var brushAddNewOrange = new ImageBrush();
            brushAddNewOrange.ImageSource = new BitmapImage(new Uri("../../Add-Orange.png", UriKind.Relative));
            btnAddNewAcdYearInAcdYearsPNL.Background = brushAddNewOrange;
        }

        private void btnAddNewAcdYearInAcdYearsPNL_MouseLeave(object sender, MouseEventArgs e)
        {
            if (clickOnAddBtn == false)
            {
                if (currentPnl != "AddAcdYears")
                {
                    var brushAddNewWhite = new ImageBrush();
                    brushAddNewWhite.ImageSource = new BitmapImage(new Uri("../../Add-White.png", UriKind.Relative));
                    btnAddNewAcdYearInAcdYearsPNL.Background = brushAddNewWhite;
                }
            }
            else
            { clickOnAddBtn = false; }
        }

        private void btnEditAcdYearInAcdYearsPNL_Click(object sender, RoutedEventArgs e)
        {
            clearAllTextBoxes();
            chnageBtnBackgroundImageAndHideAllPNL();
            var brushEditOrange = new ImageBrush();
            brushEditOrange.ImageSource = new BitmapImage(new Uri("../../Edit-Orange.png", UriKind.Relative));
            btnEditAcdYearInAcdYearsPNL.Background = brushEditOrange;

            stkPanelEditAcdYearsForStuAff.Visibility = Visibility.Visible;

            DataTable dtForAcdYearsInEditPNL = Database.returnTable("select * from AcademicYears");
            dtForAcdYearsInEditPNL.Columns["acdid"].ColumnName = "Academic Year ID";
            dtForAcdYearsInEditPNL.Columns["acdname"].ColumnName = "Academic Year Name";

            DGForAcdYearsInEdit.ItemsSource = dtForAcdYearsInEditPNL.DefaultView;

            clickOnEditBtn = true;
            currentPnl = "EditAcdYears";
        }

        private void btnEditAcdYearInAcdYearsPNL_MouseEnter(object sender, MouseEventArgs e)
        {
            var brushEditOrange = new ImageBrush();
            brushEditOrange.ImageSource = new BitmapImage(new Uri("../../Edit-Orange.png", UriKind.Relative));
            btnEditAcdYearInAcdYearsPNL.Background = brushEditOrange;
        }

        private void btnEditAcdYearInAcdYearsPNL_MouseLeave(object sender, MouseEventArgs e)
        {
            if (clickOnEditBtn == false)
            {
                if (currentPnl != "EditAcdYears")
                {
                    var brushEditWhite = new ImageBrush();
                    brushEditWhite.ImageSource = new BitmapImage(new Uri("../../Edit-White.png", UriKind.Relative));
                    btnEditAcdYearInAcdYearsPNL.Background = brushEditWhite;
                }
            }
            else
            { clickOnEditBtn = false; }
        }

        private void btnRemoveAcdYearInAcdYearsPNL_Click(object sender, RoutedEventArgs e)
        {
            clearAllTextBoxes();
            chnageBtnBackgroundImageAndHideAllPNL();
            var brushRemoveOrange = new ImageBrush();
            brushRemoveOrange.ImageSource = new BitmapImage(new Uri("../../Remove-Orange.png", UriKind.Relative));
            btnRemoveAcdYearInAcdYearsPNL.Background = brushRemoveOrange;

            stkPanelRemoveAcdYearsForStuAff.Visibility = Visibility.Visible;

            DataTable dtForAcdYearsInRemovePNL = Database.returnTable("select * from AcademicYears");
            dtForAcdYearsInRemovePNL.Columns["acdid"].ColumnName = "Academic Year ID";
            dtForAcdYearsInRemovePNL.Columns["acdname"].ColumnName = "Academic Year Name";

            DGForAcdYearsInRemove.ItemsSource = dtForAcdYearsInRemovePNL.DefaultView;

            clickOnRemoveBtn = true;
            currentPnl = "RemoveAcdYears";
        }

        private void btnRemoveAcdYearInAcdYearsPNL_MouseEnter(object sender, MouseEventArgs e)
        {
            var brushRemoveOrange = new ImageBrush();
            brushRemoveOrange.ImageSource = new BitmapImage(new Uri("../../Remove-Orange.png", UriKind.Relative));
            btnRemoveAcdYearInAcdYearsPNL.Background = brushRemoveOrange;
        }

        private void btnRemoveAcdYearInAcdYearsPNL_MouseLeave(object sender, MouseEventArgs e)
        {
            if (clickOnRemoveBtn == false)
            {
                if (currentPnl != "RemoveAcdYears")
                {
                    var brushRemoveWhite = new ImageBrush();
                    brushRemoveWhite.ImageSource = new BitmapImage(new Uri("../../Remove-White.png", UriKind.Relative));
                    btnRemoveAcdYearInAcdYearsPNL.Background = brushRemoveWhite;
                }
            }
            else
            { clickOnRemoveBtn = false; }
        }

        private void txtBoxSearchInDatagridInEditAcdYearsPNL_KeyUp(object sender, KeyEventArgs e)
        {
            DataTable dtForAcdYearsEdit = Database.returnTable("select * from AcademicYears");
            DataTable newDTForAcdYearsEditBySearch = new DataTable();
            newDTForAcdYearsEditBySearch.Columns.Add("Academic Year ID", typeof(System.String));
            newDTForAcdYearsEditBySearch.Columns.Add("Academic Year Name", typeof(System.String));

            for (int i = 0; i < dtForAcdYearsEdit.Rows.Count; i++)
            {
                bool found = false;
                for (int j = 0; j < dtForAcdYearsEdit.Columns.Count; j++)
                {
                    if (found == false)
                    { 
                        try
                        {
                            string searchText = txtBoxSearchInDatagridInEditAcdYearsPNL.Text;
                            int numOfCharOfSearchValue = searchText.Length;
                            string valueOfCurrentCell = Convert.ToString(dtForAcdYearsEdit.Rows[i][j]);
                            string lastValueOfCellForCompare = valueOfCurrentCell.Substring(0, numOfCharOfSearchValue);

                            if (searchText == lastValueOfCellForCompare)
                            {
                                DataRow dr = newDTForAcdYearsEditBySearch.NewRow();
                                dr[0] = dtForAcdYearsEdit.Rows[i][0];
                                dr[1] = dtForAcdYearsEdit.Rows[i][1];
                                newDTForAcdYearsEditBySearch.Rows.Add(dr);
                                found = true;
                            }
                        }
                        catch (Exception)
                        { }
                    }
                }
            }

            int numOfRowsOfdtForAcdYearsNew = newDTForAcdYearsEditBySearch.Rows.Count;

            if (txtBoxSearchInDatagridInEditAcdYearsPNL.Text.Trim() == "")
            {
                DataTable dtForAcdYearsInEditPNL = Database.returnTable("select * from AcademicYears");
                dtForAcdYearsInEditPNL.Columns["acdid"].ColumnName = "Academic Year ID";
                dtForAcdYearsInEditPNL.Columns["acdname"].ColumnName = "Academic Year Name";

                DGForAcdYearsInEdit.ItemsSource = dtForAcdYearsInEditPNL.DefaultView;
            }
            else
            { DGForAcdYearsInEdit.ItemsSource = newDTForAcdYearsEditBySearch.DefaultView; }
        }

        private void txtBoxSearchInDatagridInRemoveAcdYearsPNL_KeyUp(object sender, KeyEventArgs e)
        {
            DataTable dtForAcdYearsRemove = Database.returnTable("select * from AcademicYears");
            DataTable newDTForAcdYearsRemoveBySearch = new DataTable();
            newDTForAcdYearsRemoveBySearch.Columns.Add("Academic Year ID", typeof(System.String));
            newDTForAcdYearsRemoveBySearch.Columns.Add("Academic Year Name", typeof(System.String));

            for (int i = 0; i < dtForAcdYearsRemove.Rows.Count; i++)
            {
                bool found = false;
                for (int j = 0; j < dtForAcdYearsRemove.Columns.Count; j++)
                {
                    if (found == false)
                    { 
                        try
                        {
                            string searchText = txtBoxSearchInDatagridInRemoveAcdYearsPNL.Text;
                            int numOfCharOfSearchValue = searchText.Length;
                            string valueOfCurrentCell = Convert.ToString(dtForAcdYearsRemove.Rows[i][j]);
                            string lastValueOfCellForCompare = valueOfCurrentCell.Substring(0, numOfCharOfSearchValue);

                            if (searchText == lastValueOfCellForCompare)
                            {
                                DataRow dr = newDTForAcdYearsRemoveBySearch.NewRow();
                                dr[0] = dtForAcdYearsRemove.Rows[i][0];
                                dr[1] = dtForAcdYearsRemove.Rows[i][1];
                                newDTForAcdYearsRemoveBySearch.Rows.Add(dr);
                                found = true;
                            }
                        }
                        catch (Exception)
                        { }
                    }
                }
            }

            int numOfRowsOfdtForAcdYearsNew = newDTForAcdYearsRemoveBySearch.Rows.Count;

            if (txtBoxSearchInDatagridInRemoveAcdYearsPNL.Text.Trim() == "")
            {
                DataTable dtForAcdYearsInRemovePNL = Database.returnTable("select * from AcademicYears");
                dtForAcdYearsInRemovePNL.Columns["acdid"].ColumnName = "Academic Year ID";
                dtForAcdYearsInRemovePNL.Columns["acdname"].ColumnName = "Academic Year Name";

                DGForAcdYearsInRemove.ItemsSource = dtForAcdYearsInRemovePNL.DefaultView;
            }
            else
            { DGForAcdYearsInRemove.ItemsSource = newDTForAcdYearsRemoveBySearch.DefaultView; }
        }

        public void clearAllTextBoxes()
        {
            txtBoxAcdYearsNameInAcdYearsPnl.Text = "";
            txtBoxAcdYearsIDInAcdYearsPnl.Text = "";
            txtBoxSearchInDatagridInAcdYearsPNL.Text = "";
            txtBoxSearchInDatagridInEditAcdYearsPNL.Text = "";
            txtBoxSearchInDatagridInRemoveAcdYearsPNL.Text = "";
        }

        private void Grid_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            clearAllTextBoxes();
            chnageBtnBackgroundImageAndHideAllPNL();
            var brushHomeOrange = new ImageBrush();
            brushHomeOrange.ImageSource = new BitmapImage(new Uri("../../Home-Orange.png", UriKind.Relative));
            btnHomeInAcdYearsPNL.Background = brushHomeOrange;

            stkPanelHomeAcdYearsForStuAff.Visibility = Visibility.Visible;

            DataTable dtForAcdYears = Database.returnTable("select * from AcademicYears");
            dtForAcdYears.Columns["acdid"].ColumnName = "Academic Year ID";
            dtForAcdYears.Columns["acdname"].ColumnName = "Academic Year Name";

            DGForAcdYears.ItemsSource = dtForAcdYears.DefaultView;

            clickOnHomeBtn = true;
            currentPnl = "HomeAcdYears";
        }
    }
}
