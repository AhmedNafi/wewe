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
    /// Interaction logic for ucExamsForMentor.xaml
    /// </summary>
    public partial class ucExamsForMentor : UserControl
    {
        public ucExamsForMentor()
        {
            InitializeComponent();

            clearAllTextBoxes();
            chnageBtnBackgroundImageAndHideAllPNL();
            var brushHomeOrange = new ImageBrush();
            brushHomeOrange.ImageSource = new BitmapImage(new Uri("../../Home-Orange.png", UriKind.Relative));
            btnHomeInMentorPNL.Background = brushHomeOrange;

            stkPanelHomeExamsForMentor.Visibility = Visibility.Visible;

            DataTable dtForExams = Database.returnTable("select * from Exams");
            dtForExams.Columns["examid"].ColumnName = "Exam ID";
            dtForExams.Columns["examname"].ColumnName = "Exam Name";
            dtForExams.Columns["dataofexam"].ColumnName = "Data Of Exam";
            dtForExams.Columns["questionnum"].ColumnName = "Num Of Questions";
            dtForExams.Columns["acdid"].ColumnName = "Academic Year ID";
            dtForExams.Columns["acdname"].ColumnName = "Academic Year Name";
            dtForExams.Columns["deptid"].ColumnName = "Department ID";
            dtForExams.Columns["deptname"].ColumnName = "Department Name";
            dtForExams.Columns["totalpoint"].ColumnName = "Total Points";

            DGForExams.ItemsSource = dtForExams.DefaultView;

            clickOnHomeBtn = true;
            currentPnl = "HomeDept";
        }


        public void chnageBtnBackgroundImageAndHideAllPNL()
        {

            var brushHomeWhite = new ImageBrush();
            brushHomeWhite.ImageSource = new BitmapImage(new Uri("../../Home-White.png", UriKind.Relative));
            btnHomeInMentorPNL.Background = brushHomeWhite;

            var brushAddWhite = new ImageBrush();
            brushAddWhite.ImageSource = new BitmapImage(new Uri("../../Add-White.png", UriKind.Relative));
            btnAddNewExamInMentorPNL.Background = brushAddWhite;

            
            var brushEditWhite = new ImageBrush();
            brushEditWhite.ImageSource = new BitmapImage(new Uri("../../Edit-White.png", UriKind.Relative));
            btnEditExamInMentorPNL.Background = brushEditWhite;

            var brushRemoveWhite = new ImageBrush();
            brushRemoveWhite.ImageSource = new BitmapImage(new Uri("../../Remove-White.png", UriKind.Relative));
            btnRemoveExamInMentorPNL.Background = brushRemoveWhite;

            
            stkPanelHomeExamsForMentor.Visibility = Visibility.Hidden;
            stkPanelAddExamsForMentor.Visibility = Visibility.Hidden;
            stkPanelViewSelectedExam.Visibility = Visibility.Hidden;
            stkPanelTakeSynonumsInAddExamsForMentor.Visibility = Visibility.Hidden;
            stkPanelViewQustionsInViewSelectedExam.Visibility = Visibility.Hidden;

            /*
            stkPanelEditStudentsForStuAff.Visibility = Visibility.Hidden;
            stkPanelRemoveStudentsForStuAff.Visibility = Visibility.Hidden;
            */
        }

        public void clearAllTextBoxes()
        {
            lblExamIdInTakeQues.Content = "Exam ID. " + examId;
            lblQuesNumInTakeQues.Content = "Question Number: " + currentQues;
            lblCurrentQuesNumForArrow.Content = currentQues.ToString();
            txtBoxSearchInDatagridInExamsPNL.Text = "";
            txtBoxExamIDInExamsPnl.Text = "";
            txtBoxExamNameInExamPnl.Text = "";
            txtBoxDataOfExamInExamPnl.Text = "";
            txtBoxNumOfQuesInExamPnl.Text = "";
            cmbBoxAcdYearIDInExamPnl.SelectedIndex = -1;
            cmbBoxAcdYearNameInExamPnl.SelectedIndex = -1;
            cmbBoxDeptIDInExamPnl.SelectedIndex = -1;
            cmbBoxDeptNameInExamPnl.SelectedIndex = -1;
            txtBoxQuesInTakeQusInExamPnl.Text = "";
            txtBoxKeywordInTakeQusInExamPnl.Text = "";
            txtBoxModelAnswerInTakeQusInExamPnl.Text = "";
            txtBoxPointsInTakeQusInExamPnl.Text = "";
            txtBoxExamIDInViewSelectedExam.Text = "";
            txtBoxExamNameInViewSelectedExam.Text = "";
            txtBoxDataOfExamInViewSelectedExam.Text = "";
            txtBoxNumOfQuesInViewSelectedExam.Text = "";
            txtBoxAcdYearIDInViewSelectedExam.Text = "";
            txtBoxAcdYearNameInViewSelectedExam.Text = "";
            txtBoxDeptIDInViewSelectedExam.Text = "";
            txtBoxDeptNameInViewSelectedExam.Text = "";
            txtBoxQuesInViewQustionsInViewSelectedExam.Text = "";
            txtBoxKeywordInViewQustionsInViewSelectedExam.Text = "";
            txtBoxModelAnswerInViewQustionsInViewSelectedExam.Text = "";
            txtBoxPointsInViewQustionsInViewSelectedExam.Text = "";
            txtBoxKeywordSynonumsInTakeQusInExamPnl.Text = "";
            txtBoxSynonumsForKeywordInTakeQusInExamPnl.Text = "";

            DataTable dtForAcdYears = Database.returnTable("select * from AcademicYears");
            cmbBoxAcdYearIDInExamPnl.ItemsSource = dtForAcdYears.DefaultView;
            cmbBoxAcdYearIDInExamPnl.DisplayMemberPath = "acdid";

            cmbBoxAcdYearNameInExamPnl.ItemsSource = dtForAcdYears.DefaultView;
            cmbBoxAcdYearNameInExamPnl.DisplayMemberPath = "acdname";


            DataTable dtForDepartment = Database.returnTable("select * from Department");
            cmbBoxDeptIDInExamPnl.ItemsSource = dtForDepartment.DefaultView;
            cmbBoxDeptIDInExamPnl.DisplayMemberPath = "deptid";

            cmbBoxDeptNameInExamPnl.ItemsSource = dtForDepartment.DefaultView;
            cmbBoxDeptNameInExamPnl.DisplayMemberPath = "deptname";
        }

        bool clickOnHomeBtn = false;
        bool clickOnAddBtn = false;
        bool clickOnEditBtn = false;
        bool clickOnRemoveBtn = false;
        string currentPnl = "HomeExams";



        private void btnHomeInMentorPNL_Click(object sender, RoutedEventArgs e)
        {
            clearAllTextBoxes();
            chnageBtnBackgroundImageAndHideAllPNL();
            var brushHomeOrange = new ImageBrush();
            brushHomeOrange.ImageSource = new BitmapImage(new Uri("../../Home-Orange.png", UriKind.Relative));
            btnHomeInMentorPNL.Background = brushHomeOrange;

            stkPanelHomeExamsForMentor.Visibility = Visibility.Visible;

            DataTable dtForExams = Database.returnTable("select * from Exams");
            dtForExams.Columns["examid"].ColumnName = "Exam ID";
            dtForExams.Columns["examname"].ColumnName = "Exam Name";
            dtForExams.Columns["dataofexam"].ColumnName = "Data Of Exam";
            dtForExams.Columns["questionnum"].ColumnName = "Num Of Questions";
            dtForExams.Columns["acdid"].ColumnName = "Academic Year ID";
            dtForExams.Columns["acdname"].ColumnName = "Academic Year Name";
            dtForExams.Columns["deptid"].ColumnName = "Department ID";
            dtForExams.Columns["deptname"].ColumnName = "Department Name";
            dtForExams.Columns["totalpoint"].ColumnName = "Total Points";

            DGForExams.ItemsSource = dtForExams.DefaultView;

            clickOnHomeBtn = true;
            currentPnl = "HomeExams";
        }

        private void btnHomeInMentorPNL_MouseEnter(object sender, MouseEventArgs e)
        {
            var brushHomeOrange = new ImageBrush();
            brushHomeOrange.ImageSource = new BitmapImage(new Uri("../../Home-Orange.png", UriKind.Relative));
            btnHomeInMentorPNL.Background = brushHomeOrange;
        }

        private void btnHomeInMentorPNL_MouseLeave(object sender, MouseEventArgs e)
        {
            if (clickOnHomeBtn == false)
            {
                if (currentPnl != "HomeExams")
                {
                    var brushHomeWhite = new ImageBrush();
                    brushHomeWhite.ImageSource = new BitmapImage(new Uri("../../Home-White.png", UriKind.Relative));
                    btnHomeInMentorPNL.Background = brushHomeWhite;
                }
            }
            else
            { clickOnHomeBtn = false; }
        }


        private void btnAddNewExamInMentorPNL_Click(object sender, RoutedEventArgs e)
        {
            clearAllTextBoxes();
            chnageBtnBackgroundImageAndHideAllPNL();
            var brushAddOrange = new ImageBrush();
            brushAddOrange.ImageSource = new BitmapImage(new Uri("../../Add-Orange.png", UriKind.Relative));
            btnAddNewExamInMentorPNL.Background = brushAddOrange;

            stkPanelTakeQusInAddExamsForMentor.Visibility = Visibility.Hidden;
            stkPanelAddExamsForMentor.Visibility = Visibility.Visible;

            clickOnAddBtn = true;
            currentPnl = "AddExam";
        }

        private void btnAddNewExamInMentorPNL_MouseEnter(object sender, MouseEventArgs e)
        {
            var brushAddNewOrange = new ImageBrush();
            brushAddNewOrange.ImageSource = new BitmapImage(new Uri("../../Add-Orange.png", UriKind.Relative));
            btnAddNewExamInMentorPNL.Background = brushAddNewOrange;
        }

        private void btnAddNewExamInMentorPNL_MouseLeave(object sender, MouseEventArgs e)
        {
            if (clickOnAddBtn == false)
            {
                if (currentPnl != "AddExam")
                {
                    var brushAddNewWhite = new ImageBrush();
                    brushAddNewWhite.ImageSource = new BitmapImage(new Uri("../../Add-White.png", UriKind.Relative));
                    btnAddNewExamInMentorPNL.Background = brushAddNewWhite;
                }
            }
            else
            { clickOnAddBtn = false; }
        }


        private void btnEditExamInMentorPNL_Click(object sender, RoutedEventArgs e)
        {
            clearAllTextBoxes();
            chnageBtnBackgroundImageAndHideAllPNL();
            var brushEditOrange = new ImageBrush();
            brushEditOrange.ImageSource = new BitmapImage(new Uri("../../Edit-Orange.png", UriKind.Relative));
            btnEditExamInMentorPNL.Background = brushEditOrange;

            //stkPanelHomeStudentsForStuAff.Visibility = Visibility.Visible;

            DataTable dtForStudents = Database.returnTable("select * from Students");
            dtForStudents.Columns["stuid"].ColumnName = "Student ID";
            dtForStudents.Columns["stuname"].ColumnName = "Student Name";
            dtForStudents.Columns["acdid"].ColumnName = "Academic Year ID";
            dtForStudents.Columns["acdname"].ColumnName = "Academic Year Name";
            dtForStudents.Columns["deptid"].ColumnName = "Department ID";
            dtForStudents.Columns["deptname"].ColumnName = "Department Name";

            //DGForStudents.ItemsSource = dtForStudents.DefaultView;

            clickOnEditBtn = true;
            currentPnl = "EditExams";
        }

        private void btnEditExamInMentorPNL_MouseEnter(object sender, MouseEventArgs e)
        {
            var brushEditOrange = new ImageBrush();
            brushEditOrange.ImageSource = new BitmapImage(new Uri("../../Edit-Orange.png", UriKind.Relative));
            btnEditExamInMentorPNL.Background = brushEditOrange;
        }

        private void btnEditExamInMentorPNL_MouseLeave(object sender, MouseEventArgs e)
        {
            if (clickOnEditBtn == false)
            {
                if (currentPnl != "EditExams")
                {
                    var brushEditWhite = new ImageBrush();
                    brushEditWhite.ImageSource = new BitmapImage(new Uri("../../Edit-White.png", UriKind.Relative));
                    btnEditExamInMentorPNL.Background = brushEditWhite;
                }
            }
            else
            { clickOnEditBtn = false; }
        }


        private void btnRemoveExamInMentorPNL_Click(object sender, RoutedEventArgs e)
        {
            clearAllTextBoxes();
            chnageBtnBackgroundImageAndHideAllPNL();
            var brushRemoveOrange = new ImageBrush();
            brushRemoveOrange.ImageSource = new BitmapImage(new Uri("../../Remove-Orange.png", UriKind.Relative));
            btnRemoveExamInMentorPNL.Background = brushRemoveOrange;

            //stkPanelHomeStudentsForStuAff.Visibility = Visibility.Visible;

            DataTable dtForStudents = Database.returnTable("select * from Students");
            dtForStudents.Columns["stuid"].ColumnName = "Student ID";
            dtForStudents.Columns["stuname"].ColumnName = "Student Name";
            dtForStudents.Columns["acdid"].ColumnName = "Academic Year ID";
            dtForStudents.Columns["acdname"].ColumnName = "Academic Year Name";
            dtForStudents.Columns["deptid"].ColumnName = "Department ID";
            dtForStudents.Columns["deptname"].ColumnName = "Department Name";

            //DGForStudents.ItemsSource = dtForStudents.DefaultView;

            clickOnRemoveBtn = true;
            currentPnl = "RemoveExams";
        }

        private void btnRemoveExamInMentorPNL_MouseEnter(object sender, MouseEventArgs e)
        {
            var brushRemoveOrange = new ImageBrush();
            brushRemoveOrange.ImageSource = new BitmapImage(new Uri("../../Remove-Orange.png", UriKind.Relative));
            btnRemoveExamInMentorPNL.Background = brushRemoveOrange;
        }

        private void btnRemoveExamInMentorPNL_MouseLeave(object sender, MouseEventArgs e)
        {
            if (clickOnRemoveBtn == false)
            {
                if (currentPnl != "RemoveExams")
                {
                    var brushRemoveWhite = new ImageBrush();
                    brushRemoveWhite.ImageSource = new BitmapImage(new Uri("../../Remove-White.png", UriKind.Relative));
                    btnRemoveExamInMentorPNL.Background = brushRemoveWhite;
                }
            }
            else
            { clickOnRemoveBtn = false; }
        }


        private void cmbBoxAcdYearIDInExamPnl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        { cmbBoxAcdYearNameInExamPnl.SelectedIndex = cmbBoxAcdYearIDInExamPnl.SelectedIndex; }

        private void cmbBoxAcdYearNameInExamPnl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        { cmbBoxAcdYearIDInExamPnl.SelectedIndex = cmbBoxAcdYearNameInExamPnl.SelectedIndex; }

        private void cmbBoxDeptIDInExamPnl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        { cmbBoxDeptNameInExamPnl.SelectedIndex = cmbBoxDeptIDInExamPnl.SelectedIndex; }

        private void cmbBoxDeptNameInExamPnl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        { cmbBoxDeptIDInExamPnl.SelectedIndex = cmbBoxDeptNameInExamPnl.SelectedIndex; }


        DataTable newDTForExamInfo = new DataTable();
        DataTable newDTForExamData = new DataTable();
        DataTable dtForSynonums = new DataTable();

        int numOfQues;
        int currentQues = 1;
        string examId;

        private void btnSubmitAddNewExam_Click(object sender, RoutedEventArgs e)
        {
            if (txtBoxExamIDInExamsPnl.Text.Trim() == "")
            { MessageBox.Show("Please Enter Exam ID", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
            else if (txtBoxExamNameInExamPnl.Text.Trim() == "")
            { MessageBox.Show("Please Enter Exam Name", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
            else if (txtBoxDataOfExamInExamPnl.Text.Trim() == "")
            { MessageBox.Show("Please Enter Date Of Exam", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
            else if (txtBoxNumOfQuesInExamPnl.Text.Trim() == "")
            { MessageBox.Show("Please Enter Number Of Questions", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
            else if (cmbBoxAcdYearIDInExamPnl.SelectedIndex == -1 || cmbBoxAcdYearNameInExamPnl.SelectedIndex == -1)
            { MessageBox.Show("Please Choose Academic Year ID Or Academic Year Name", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
            else if (cmbBoxDeptIDInExamPnl.SelectedIndex == -1 || cmbBoxDeptNameInExamPnl.SelectedIndex == -1)
            { MessageBox.Show("Please Choose Department ID Or Department Name", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
            else
            {
                numOfQues = int.Parse(txtBoxNumOfQuesInExamPnl.Text);
                examId = txtBoxExamIDInExamsPnl.Text;

                newDTForExamInfo.Columns.Add("Exam ID", typeof(System.String));
                newDTForExamInfo.Columns.Add("Exam Name", typeof(System.String));
                newDTForExamInfo.Columns.Add("Data Of Exam", typeof(System.String));
                newDTForExamInfo.Columns.Add("Number Of Questions", typeof(System.Int32));
                newDTForExamInfo.Columns.Add("Academic Year ID", typeof(System.String));
                newDTForExamInfo.Columns.Add("Academic Year Name", typeof(System.String));
                newDTForExamInfo.Columns.Add("Department ID", typeof(System.String));
                newDTForExamInfo.Columns.Add("Department Name", typeof(System.String));

                DataRow dr = newDTForExamInfo.NewRow();
                dr[0] = txtBoxExamIDInExamsPnl.Text;
                dr[1] = txtBoxExamNameInExamPnl.Text;
                dr[2] = txtBoxDataOfExamInExamPnl.Text;
                dr[3] = txtBoxNumOfQuesInExamPnl.Text;
                dr[4] = cmbBoxAcdYearIDInExamPnl.Text;
                dr[5] = cmbBoxAcdYearNameInExamPnl.Text;
                dr[6] = cmbBoxDeptIDInExamPnl.Text;
                dr[7] = cmbBoxDeptNameInExamPnl.Text;
                newDTForExamInfo.Rows.Add(dr);

                stkPanelTakeQusInAddExamsForMentor.Visibility = Visibility.Visible;
                stkPanelAddExamsForMentor.Visibility = Visibility.Hidden;

                newDTForExamData.Columns.Add("Exam ID", typeof(System.String));
                newDTForExamData.Columns.Add("Questions", typeof(System.String));
                newDTForExamData.Columns.Add("Keywords", typeof(System.String));
                newDTForExamData.Columns.Add("Model Answer", typeof(System.String));
                newDTForExamData.Columns.Add("Points", typeof(System.Int32));

                dtForSynonums.Columns.Add("Questions Number", typeof(System.String));
                dtForSynonums.Columns.Add("Keyword", typeof(System.String));
                dtForSynonums.Columns.Add("Synonums", typeof(System.String));
            }
        }

        int totalPoints = 0;

        private void btnSubmitTakeQusInAddNewExam_Click(object sender, RoutedEventArgs e)
        {
            if (txtBoxQuesInTakeQusInExamPnl.Text.Trim() == "")
            { MessageBox.Show("Please Enter The Question", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
            else if (txtBoxKeywordInTakeQusInExamPnl.Text.Trim() == "")
            { MessageBox.Show("Please Enter The Keyword", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
            else if (txtBoxModelAnswerInTakeQusInExamPnl.Text.Trim() == "")
            { MessageBox.Show("Please Enter The Model Answer", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
            else if (txtBoxPointsInTakeQusInExamPnl.Text.Trim() == "")
            { MessageBox.Show("Please Enter The Points", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
            else
            {
                int numOfRowsOfdtForExamData = newDTForExamData.Rows.Count;

                if (currentQues != numOfQues && currentQues < numOfRowsOfdtForExamData)
                {
                    currentQues++;
                    clearAllTextBoxes();
                    txtBoxQuesInTakeQusInExamPnl.Text = Convert.ToString(newDTForExamData.Rows[currentQues - 1][1]);
                    txtBoxKeywordInTakeQusInExamPnl.Text = Convert.ToString(newDTForExamData.Rows[currentQues - 1][2]);
                    txtBoxModelAnswerInTakeQusInExamPnl.Text = Convert.ToString(newDTForExamData.Rows[currentQues - 1][3]);
                    txtBoxPointsInTakeQusInExamPnl.Text = Convert.ToString(newDTForExamData.Rows[currentQues - 1][4]);
                }
                else
                {
                    if (currentQues < numOfQues)
                    {
                        DataRow dr = newDTForExamData.NewRow();
                        dr[0] = examId;
                        dr[1] = txtBoxQuesInTakeQusInExamPnl.Text;
                        dr[2] = txtBoxKeywordInTakeQusInExamPnl.Text;
                        dr[3] = txtBoxModelAnswerInTakeQusInExamPnl.Text;
                        dr[4] = txtBoxPointsInTakeQusInExamPnl.Text;
                        newDTForExamData.Rows.Add(dr);

                        totalPoints += int.Parse(txtBoxPointsInTakeQusInExamPnl.Text);

                        if (currentQues == (numOfQues - 1))
                        { btnSubmitTakeQusInAddNewExam.Content = "Submit"; }
                        currentQues++;

                        lblCurrentQuesNumForArrow.Content = currentQues.ToString();
                        lblQuesNumInTakeQues.Content = "Question Number: " + currentQues;
                        txtBoxQuesInTakeQusInExamPnl.Text = "";
                        txtBoxKeywordInTakeQusInExamPnl.Text = "";
                        txtBoxModelAnswerInTakeQusInExamPnl.Text = "";
                        txtBoxPointsInTakeQusInExamPnl.Text = "";
                    }

                    else if (currentQues == numOfQues)
                    {
                        MessageBoxResult resMakeNewExam = MessageBox.Show("Are You Sure To Add New Exam?", "New Exam", MessageBoxButton.YesNo, MessageBoxImage.Question);

                        if (resMakeNewExam == MessageBoxResult.Yes)
                        {
                            try
                            {
                                string tableName = "exam" + examId;
                                string tableNameForSynonums = "SynonumsForExam" + examId;

                                int numOfRowsOfdtForSynonums = dtForSynonums.Rows.Count;

                                DataRow dr = newDTForExamData.NewRow();
                                dr[0] = examId;
                                dr[1] = txtBoxQuesInTakeQusInExamPnl.Text;
                                dr[2] = txtBoxKeywordInTakeQusInExamPnl.Text;
                                dr[3] = txtBoxModelAnswerInTakeQusInExamPnl.Text;
                                dr[4] = txtBoxPointsInTakeQusInExamPnl.Text;
                                newDTForExamData.Rows.Add(dr);

                                totalPoints += int.Parse(txtBoxPointsInTakeQusInExamPnl.Text);

                                string examID = Convert.ToString(newDTForExamInfo.Rows[0][0]);
                                string examName = Convert.ToString(newDTForExamInfo.Rows[0][1]);
                                string dateOfExam = Convert.ToString(newDTForExamInfo.Rows[0][2]);
                                int numOfQuestions = Convert.ToInt32(newDTForExamInfo.Rows[0][3]);
                                string acdId = Convert.ToString(newDTForExamInfo.Rows[0][4]);
                                string acdName = Convert.ToString(newDTForExamInfo.Rows[0][5]);
                                string deptId = Convert.ToString(newDTForExamInfo.Rows[0][6]);
                                string deptName = Convert.ToString(newDTForExamInfo.Rows[0][7]);

                                Database.Run("insert into Exams values('" + examID + "', '" + examName + "', '" + dateOfExam + "', " + numOfQuestions + ", '" + acdId + "', '" + acdName + "', '" + deptId + "', '" + deptName + "', " + totalPoints + ");");

                                Database.Run("CREATE TABLE '"+ tableName + "' ( 'examid' TEXT, 'questions' TEXT, 'keywords' TEXT, 'modelanswer' TEXT, 'points' INTEGER, PRIMARY KEY('questions','examid'));");
                               
                                Database.Run("CREATE TABLE '" + tableNameForSynonums + "' ( 'questionsnumber' TEXT, 'keyword' TEXT, 'synonums' TEXT, PRIMARY KEY('questionsnumber','keyword','synonums'));");

                                for (int i = 0; i < (numOfRowsOfdtForExamData + 1); i++)
                                {
                                    string questions = Convert.ToString(newDTForExamData.Rows[i][1]);
                                    string keywords = Convert.ToString(newDTForExamData.Rows[i][2]);
                                    string modelanswer = Convert.ToString(newDTForExamData.Rows[i][3]);
                                    string points = Convert.ToString(newDTForExamData.Rows[i][4]);
                                    Database.Run("insert into " + tableName + " values('" + examID + "', '" + questions + "', '" + keywords + "', '" + modelanswer + "', '" + points + "');");
                                }

                                for (int i = 0; i < numOfRowsOfdtForSynonums; i++)
                                {
                                    string questionsNumber = Convert.ToString(dtForSynonums.Rows[i][0]);
                                    string Keyword = Convert.ToString(dtForSynonums.Rows[i][1]);
                                    string Synonums = Convert.ToString(dtForSynonums.Rows[i][2]);
                                    Database.Run("insert into " + tableNameForSynonums + " values('" + questionsNumber + "', '" + Keyword + "', '" + Synonums + "');");/*
                                    if (questionsNumber == "" && Keyword == "" && Synonums == "")
                                    { }
                                    else 
                                    { Database.Run("insert into " + tableNameForSynonums + " values('" + questionsNumber + "', '" + Keyword + "', '" + Synonums + "');"); }*/
                                }
                                
                                //for (int i = 0; i < dtForSynonums.Rows.Count; i++)
                                //{
                                //    string questionsNumber = Convert.ToString(dtForSynonums.Rows[i][0]);
                                //    string Keyword = Convert.ToString(dtForSynonums.Rows[i][1]);
                                //    string Synonums = Convert.ToString(dtForSynonums.Rows[i][2]);
                                //    Database.Run("insert into " + tableNameForSynonums + " values('" + questionsNumber + "', '" + Keyword + "', '" + Synonums + "');");
                                //    /*
                                //    if (questionsNumber == "" && Keyword == "" && Synonums == "")
                                //    { }
                                //    else 
                                //    { Database.Run("insert into " + tableNameForSynonums + " values('" + questionsNumber + "', '" + Keyword + "', '" + Synonums + "');"); }*/
                                //}
                                 
                                newDTForExamInfo.Columns.Remove("Exam ID");
                                newDTForExamInfo.Columns.Remove("Exam Name");
                                newDTForExamInfo.Columns.Remove("Data Of Exam");
                                newDTForExamInfo.Columns.Remove("Number Of Questions");
                                newDTForExamInfo.Columns.Remove("Academic Year ID");
                                newDTForExamInfo.Columns.Remove("Academic Year Name");
                                newDTForExamInfo.Columns.Remove("Department ID");
                                newDTForExamInfo.Columns.Remove("Department Name");

                                newDTForExamData.Columns.Remove("Exam ID");
                                newDTForExamData.Columns.Remove("Questions");
                                newDTForExamData.Columns.Remove("Keywords");
                                newDTForExamData.Columns.Remove("Model Answer");
                                newDTForExamData.Columns.Remove("Points");

                                dtForSynonums.Columns.Remove("Questions Number");
                                dtForSynonums.Columns.Remove("Keyword");
                                dtForSynonums.Columns.Remove("Synonums");

                                currentSynonums = 1;
                                currentQues = 1;
                                totalPoints = 0;
                                btnSubmitTakeQusInAddNewExam.Content = "Next";

                                stkPanelTakeQusInAddExamsForMentor.Visibility = Visibility.Hidden;
                                stkPanelAddExamsForMentor.Visibility = Visibility.Visible;

                                MessageBox.Show("The New Exam Has Been Successfully Added", "New Exam", MessageBoxButton.OK, MessageBoxImage.Information);

                            }
                            catch (Exception ex)
                            { MessageBox.Show("Error here " + ex.Message, "New Exam", MessageBoxButton.OK, MessageBoxImage.Error); }
                        }
                        else if (resMakeNewExam == MessageBoxResult.No)
                        { }
                    }
                }
            }
        }

        private void stkPanelTakeQusInAddExamsForMentor_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        { 
            clearAllTextBoxes();
            if (numOfQues == 1)
            { btnSubmitTakeQusInAddNewExam.Content = "Submit"; }
        }

        private void stkPanelAddExamsForMentor_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        { clearAllTextBoxes(); }


        private void btnGetPreviousQues_Click(object sender, RoutedEventArgs e)
        {
            if (currentQues != 1)
            {
                btnSubmitTakeQusInAddNewExam.Content = "Next";

                currentQues--;
                clearAllTextBoxes();
                txtBoxQuesInTakeQusInExamPnl.Text = Convert.ToString(newDTForExamData.Rows[currentQues - 1][1]);
                txtBoxKeywordInTakeQusInExamPnl.Text = Convert.ToString(newDTForExamData.Rows[currentQues - 1][2]);
                txtBoxModelAnswerInTakeQusInExamPnl.Text = Convert.ToString(newDTForExamData.Rows[currentQues - 1][3]);
                txtBoxPointsInTakeQusInExamPnl.Text = Convert.ToString(newDTForExamData.Rows[currentQues - 1][4]);
            }
        }

        private void btnGetPreviousQues_MouseEnter(object sender, MouseEventArgs e)
        {
            var brushLeftOrange = new ImageBrush();
            brushLeftOrange.ImageSource = new BitmapImage(new Uri("../../Arrow-Left-Orange.png", UriKind.Relative));
            btnGetPreviousQues.Background = brushLeftOrange;
        }

        private void btnGetPreviousQues_MouseLeave(object sender, MouseEventArgs e)
        {
            var brushLeftWhite = new ImageBrush();
            brushLeftWhite.ImageSource = new BitmapImage(new Uri("../../Arrow-Left-White.png", UriKind.Relative));
            btnGetPreviousQues.Background = brushLeftWhite;
        }

        private void btnGetNextQues_Click(object sender, RoutedEventArgs e)
        {
            int numOfRowsOfdtForExamData = newDTForExamData.Rows.Count;

            if (currentQues != numOfQues && currentQues < numOfRowsOfdtForExamData)
            {
                currentQues++;
                clearAllTextBoxes();
                txtBoxQuesInTakeQusInExamPnl.Text = Convert.ToString(newDTForExamData.Rows[currentQues - 1][1]);
                txtBoxKeywordInTakeQusInExamPnl.Text = Convert.ToString(newDTForExamData.Rows[currentQues - 1][2]);
                txtBoxModelAnswerInTakeQusInExamPnl.Text = Convert.ToString(newDTForExamData.Rows[currentQues - 1][3]);
                txtBoxPointsInTakeQusInExamPnl.Text = Convert.ToString(newDTForExamData.Rows[currentQues - 1][4]);
            }
        }

        private void btnGetNextQues_MouseEnter(object sender, MouseEventArgs e)
        {
            var brushRightOrange = new ImageBrush();
            brushRightOrange.ImageSource = new BitmapImage(new Uri("../../Arrow-Right-Orange.png", UriKind.Relative));
            btnGetNextQues.Background = brushRightOrange;
        }

        private void btnGetNextQues_MouseLeave(object sender, MouseEventArgs e)
        {
            var brushRightWhite = new ImageBrush();
            brushRightWhite.ImageSource = new BitmapImage(new Uri("../../Arrow-Right-White.png", UriKind.Relative));
            btnGetNextQues.Background = brushRightWhite;
        }

        private void txtBoxSearchInDatagridInExamsPNL_KeyUp(object sender, KeyEventArgs e)
        {

            DataTable dtForExams = Database.returnTable("select * from Exams");
            DataTable newDTForExamsBySearch = new DataTable();
            newDTForExamsBySearch.Columns.Add("Exam ID", typeof(System.String));
            newDTForExamsBySearch.Columns.Add("Exam Name", typeof(System.String));
            newDTForExamsBySearch.Columns.Add("Date Of Exam", typeof(System.String));
            newDTForExamsBySearch.Columns.Add("Number Of Questions", typeof(System.Int32));
            newDTForExamsBySearch.Columns.Add("Academic Year ID", typeof(System.String));
            newDTForExamsBySearch.Columns.Add("Academic Year Name", typeof(System.String));
            newDTForExamsBySearch.Columns.Add("Department ID", typeof(System.String));
            newDTForExamsBySearch.Columns.Add("Department Name", typeof(System.String));
            newDTForExamsBySearch.Columns.Add("Total Points", typeof(System.Int32));

            for (int i = 0; i < dtForExams.Rows.Count; i++)
            {
                bool found = false;
                for (int j = 0; j < dtForExams.Columns.Count; j++)
                {
                    if (found == false)
                    { 
                        try
                        {
                            string searchText = txtBoxSearchInDatagridInExamsPNL.Text;
                            int numOfCharOfSearchValue = searchText.Length;
                            string valueOfCurrentCell = Convert.ToString(dtForExams.Rows[i][j]);
                            string lastValueOfCellForCompare = valueOfCurrentCell.Substring(0, numOfCharOfSearchValue);

                            if (searchText == lastValueOfCellForCompare)
                            {
                                DataRow dr = newDTForExamsBySearch.NewRow();
                                dr[0] = dtForExams.Rows[i][0];
                                dr[1] = dtForExams.Rows[i][1];
                                dr[2] = dtForExams.Rows[i][2];
                                dr[3] = dtForExams.Rows[i][3];
                                dr[4] = dtForExams.Rows[i][4];
                                dr[5] = dtForExams.Rows[i][5];
                                dr[6] = dtForExams.Rows[i][6];
                                dr[7] = dtForExams.Rows[i][7];
                                dr[8] = dtForExams.Rows[i][8];
                                newDTForExamsBySearch.Rows.Add(dr);
                                found = true;
                            }
                        }
                        catch (Exception)
                        { }
                    }
                }
            }

            if (txtBoxSearchInDatagridInExamsPNL.Text.Trim() == "")
            {
                dtForExams.Columns["examid"].ColumnName = "Exam ID";
                dtForExams.Columns["examname"].ColumnName = "Exam Name";
                dtForExams.Columns["dataofexam"].ColumnName = "Data Of Exam";
                dtForExams.Columns["questionnum"].ColumnName = "Num Of Questions";
                dtForExams.Columns["acdid"].ColumnName = "Academic Year ID";
                dtForExams.Columns["acdname"].ColumnName = "Academic Year Name";
                dtForExams.Columns["deptid"].ColumnName = "Department ID";
                dtForExams.Columns["deptname"].ColumnName = "Department Name";
                dtForExams.Columns["totalpoint"].ColumnName = "Total Points";

                DGForExams.ItemsSource = dtForExams.DefaultView; 
            }
            else
            { DGForExams.ItemsSource = newDTForExamsBySearch.DefaultView; }
        }

        private void DGForExams_LoadingRow(object sender, DataGridRowEventArgs e)
        { e.Row.Header = (e.Row.GetIndex() + 1).ToString(); }

        private void Grid_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            clearAllTextBoxes();
            chnageBtnBackgroundImageAndHideAllPNL();
            var brushHomeOrange = new ImageBrush();
            brushHomeOrange.ImageSource = new BitmapImage(new Uri("../../Home-Orange.png", UriKind.Relative));
            btnHomeInMentorPNL.Background = brushHomeOrange;

            stkPanelHomeExamsForMentor.Visibility = Visibility.Visible;

            DataTable dtForExams = Database.returnTable("select * from Exams");
            dtForExams.Columns["examid"].ColumnName = "Exam ID";
            dtForExams.Columns["examname"].ColumnName = "Exam Name";
            dtForExams.Columns["dataofexam"].ColumnName = "Data Of Exam";
            dtForExams.Columns["questionnum"].ColumnName = "Num Of Questions";
            dtForExams.Columns["acdid"].ColumnName = "Academic Year ID";
            dtForExams.Columns["acdname"].ColumnName = "Academic Year Name";
            dtForExams.Columns["deptid"].ColumnName = "Department ID";
            dtForExams.Columns["deptname"].ColumnName = "Department Name";
            dtForExams.Columns["totalpoint"].ColumnName = "Total Points";

            DGForExams.ItemsSource = dtForExams.DefaultView;

            clickOnHomeBtn = true;
            currentPnl = "HomeExams";
        }

        string examIDForViewExamInPNLViewExam;
        string examNameForViewExamInPNLViewExam;
        string dateOfExamForViewExamInPNLViewExam;
        int numOfQuestionsForViewExamInPNLViewExam;
        string acdIdForViewExamInPNLViewExam;
        string acdNameForViewExamInPNLViewExam;
        string deptIdForViewExamInPNLViewExam;
        string deptNameForViewExamInPNLViewExam;

        private void btnViewCurrentExam_Click(object sender, RoutedEventArgs e)
        {
            DataRowView dataRowView = (DataRowView)((Button)e.Source).DataContext;
            examIDForViewExamInPNLViewExam = dataRowView[0].ToString();
            examNameForViewExamInPNLViewExam = dataRowView[1].ToString();
            dateOfExamForViewExamInPNLViewExam = dataRowView[2].ToString();
            numOfQuestionsForViewExamInPNLViewExam = Convert.ToInt32(dataRowView[3]);
            acdIdForViewExamInPNLViewExam = dataRowView[4].ToString();
            acdNameForViewExamInPNLViewExam = dataRowView[5].ToString();
            deptIdForViewExamInPNLViewExam = dataRowView[6].ToString();
            deptNameForViewExamInPNLViewExam = dataRowView[7].ToString();

            selectedExamID = dataRowView[0].ToString();

            clearAllTextBoxes();
            chnageBtnBackgroundImageAndHideAllPNL();
            stkPanelViewSelectedExam.Visibility = Visibility.Visible;

            var brushHomeOrange = new ImageBrush();
            brushHomeOrange.ImageSource = new BitmapImage(new Uri("../../Home-Orange.png", UriKind.Relative));
            btnHomeInMentorPNL.Background = brushHomeOrange;
        }

        private void stkPanelViewSelectedExam_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            txtBoxExamIDInViewSelectedExam.Text = examIDForViewExamInPNLViewExam;
            txtBoxExamNameInViewSelectedExam.Text = examNameForViewExamInPNLViewExam;
            txtBoxDataOfExamInViewSelectedExam.Text = dateOfExamForViewExamInPNLViewExam;
            txtBoxNumOfQuesInViewSelectedExam.Text = numOfQuestionsForViewExamInPNLViewExam.ToString();
            txtBoxAcdYearIDInViewSelectedExam.Text = acdIdForViewExamInPNLViewExam;
            txtBoxAcdYearNameInViewSelectedExam.Text = acdNameForViewExamInPNLViewExam;
            txtBoxDeptIDInViewSelectedExam.Text = deptIdForViewExamInPNLViewExam;
            txtBoxDeptNameInViewSelectedExam.Text = deptNameForViewExamInPNLViewExam;
        }

        private void btnNextPNLInViewSelectedExam_Click(object sender, RoutedEventArgs e)
        {
            clearAllTextBoxes();
            chnageBtnBackgroundImageAndHideAllPNL();

            string tableName = "exam" + selectedExamID;

            dtForExamsInView = Database.returnTable("select * from " + tableName);
            numOfRowsInTableInSelected = dtForExamsInView.Rows.Count;

            stkPanelViewQustionsInViewSelectedExam.Visibility = Visibility.Visible;
            var brushHomeOrange = new ImageBrush();
            brushHomeOrange.ImageSource = new BitmapImage(new Uri("../../Home-Orange.png", UriKind.Relative));
            btnHomeInMentorPNL.Background = brushHomeOrange;
        }

        private void btnGetPreviousQuesInViewQustionsInViewSelectedExam_MouseEnter(object sender, MouseEventArgs e)
        {
            var brushLeftOrange = new ImageBrush();
            brushLeftOrange.ImageSource = new BitmapImage(new Uri("../../Arrow-Left-Orange.png", UriKind.Relative));
            btnGetPreviousQuesInViewQustionsInViewSelectedExam.Background = brushLeftOrange;
        }

        private void btnGetPreviousQuesInViewQustionsInViewSelectedExam_MouseLeave(object sender, MouseEventArgs e)
        {
            var brushLeftWhite = new ImageBrush();
            brushLeftWhite.ImageSource = new BitmapImage(new Uri("../../Arrow-Left-White.png", UriKind.Relative));
            btnGetPreviousQuesInViewQustionsInViewSelectedExam.Background = brushLeftWhite;
        }

        private void btnGetNextQuesInViewQustionsInViewSelectedExam_MouseEnter(object sender, MouseEventArgs e)
        {
            var brushRightOrange = new ImageBrush();
            brushRightOrange.ImageSource = new BitmapImage(new Uri("../../Arrow-Right-Orange.png", UriKind.Relative));
            btnGetNextQuesInViewQustionsInViewSelectedExam.Background = brushRightOrange;
        }

        private void btnGetNextQuesInViewQustionsInViewSelectedExam_MouseLeave(object sender, MouseEventArgs e)
        {
            var brushRightWhite = new ImageBrush();
            brushRightWhite.ImageSource = new BitmapImage(new Uri("../../Arrow-Right-White.png", UriKind.Relative));
            btnGetNextQuesInViewQustionsInViewSelectedExam.Background = brushRightWhite;
        }

        static string selectedExamID;
        DataTable dtForExamsInView = new DataTable();
        int currentQuesInSelected = 1;
        int numOfRowsInTableInSelected;

        private void stkPanelViewQustionsInViewSelectedExam_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            
            currentQuesInSelected = 1;
            txtBoxQuesInViewQustionsInViewSelectedExam.Text = dtForExamsInView.Rows[currentQuesInSelected - 1][1].ToString();
            txtBoxKeywordInViewQustionsInViewSelectedExam.Text = dtForExamsInView.Rows[currentQuesInSelected - 1][2].ToString();
            txtBoxModelAnswerInViewQustionsInViewSelectedExam.Text = dtForExamsInView.Rows[currentQuesInSelected - 1][3].ToString();
            txtBoxPointsInViewQustionsInViewSelectedExam.Text = dtForExamsInView.Rows[currentQuesInSelected - 1][4].ToString();
            lblExamIdInViewQustionsInViewSelectedExam.Content = "Exam ID. " + selectedExamID;
            lblQuesNumInViewQustionsInViewSelectedExam.Content = "Question Number: " + currentQuesInSelected;
            lblCurrentQuesNumForArrowInViewQustionsInViewSelectedExam.Content = currentQuesInSelected.ToString();
            
        }

        private void btnGetPreviousQuesInViewQustionsInViewSelectedExam_Click(object sender, RoutedEventArgs e)
        {
            if (currentQuesInSelected > 1)
            {
                currentQuesInSelected--;

                txtBoxQuesInViewQustionsInViewSelectedExam.Text = dtForExamsInView.Rows[currentQuesInSelected - 1][1].ToString();
                txtBoxKeywordInViewQustionsInViewSelectedExam.Text = dtForExamsInView.Rows[currentQuesInSelected - 1][2].ToString();
                txtBoxModelAnswerInViewQustionsInViewSelectedExam.Text = dtForExamsInView.Rows[currentQuesInSelected - 1][3].ToString();
                txtBoxPointsInViewQustionsInViewSelectedExam.Text = dtForExamsInView.Rows[currentQuesInSelected - 1][4].ToString();
                lblExamIdInViewQustionsInViewSelectedExam.Content = "Exam ID. " + selectedExamID;
                lblQuesNumInViewQustionsInViewSelectedExam.Content = "Question Number: " + currentQuesInSelected;
                lblCurrentQuesNumForArrowInViewQustionsInViewSelectedExam.Content = currentQuesInSelected.ToString();
            }
        }

        private void btnGetNextQuesInViewQustionsInViewSelectedExam_Click(object sender, RoutedEventArgs e)
        {
            if (currentQuesInSelected < numOfRowsInTableInSelected)
            {
                currentQuesInSelected++;
                txtBoxQuesInViewQustionsInViewSelectedExam.Text = dtForExamsInView.Rows[currentQuesInSelected - 1][1].ToString();
                txtBoxKeywordInViewQustionsInViewSelectedExam.Text = dtForExamsInView.Rows[currentQuesInSelected - 1][2].ToString();
                txtBoxModelAnswerInViewQustionsInViewSelectedExam.Text = dtForExamsInView.Rows[currentQuesInSelected - 1][3].ToString();
                txtBoxPointsInViewQustionsInViewSelectedExam.Text = dtForExamsInView.Rows[currentQuesInSelected - 1][4].ToString();
                lblExamIdInViewQustionsInViewSelectedExam.Content = "Exam ID. " + selectedExamID;
                lblQuesNumInViewQustionsInViewSelectedExam.Content = "Question Number: " + currentQuesInSelected;
                lblCurrentQuesNumForArrowInViewQustionsInViewSelectedExam.Content = currentQuesInSelected.ToString();
            }
        }

        private void stkPanelTakeSynonumsInAddExamsForMentor_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        { 
            clearAllTextBoxes();
            currentKeywordNum = 1;
            lblCurrentSynonumsNumForArrow.Content = currentKeywordNum.ToString();
        }

        private void btnTakeNextSynonumsInAddNewExam_Click(object sender, RoutedEventArgs e)
        {
            if (txtBoxKeywordSynonumsInTakeQusInExamPnl.Text.Trim() == "")
            { MessageBox.Show("Please Enter The Keyword", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
            else if (txtBoxSynonumsForKeywordInTakeQusInExamPnl.Text.Trim() == "")
            { MessageBox.Show("Please Enter The Synonums Of Keyword Splitted By ,", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
            else
            {
                DataRow dr = dtForSynonums.NewRow();
                dr[0] = currentQues;
                dr[1] = txtBoxKeywordSynonumsInTakeQusInExamPnl.Text;
                dr[2] = txtBoxSynonumsForKeywordInTakeQusInExamPnl.Text;
                dtForSynonums.Rows.Add(dr);

                txtBoxKeywordSynonumsInTakeQusInExamPnl.Text = "";
                txtBoxSynonumsForKeywordInTakeQusInExamPnl.Text = "";

                currentSynonums++;

                currentKeywordNum++;
                lblCurrentSynonumsNumForArrow.Content = currentKeywordNum.ToString();
            }
        }

        int currentSynonums = 1;

        private void btnGetPreviousSynonums_Click(object sender, RoutedEventArgs e)
        {
            if (currentSynonums != 1)
            {
                currentSynonums--;
                clearAllTextBoxes();
                txtBoxKeywordSynonumsInTakeQusInExamPnl.Text = Convert.ToString(dtForSynonums.Rows[currentSynonums - 1][1]);
                txtBoxSynonumsForKeywordInTakeQusInExamPnl.Text = Convert.ToString(dtForSynonums.Rows[currentSynonums - 1][2]);
            }
        }

        private void btnGetPreviousSynonums_MouseEnter(object sender, MouseEventArgs e)
        {
            var brushLeftOrange = new ImageBrush();
            brushLeftOrange.ImageSource = new BitmapImage(new Uri("../../Arrow-Left-Orange.png", UriKind.Relative));
            btnGetPreviousSynonums.Background = brushLeftOrange;
        }

        private void btnGetPreviousSynonums_MouseLeave(object sender, MouseEventArgs e)
        {
            var brushLeftWhite = new ImageBrush();
            brushLeftWhite.ImageSource = new BitmapImage(new Uri("../../Arrow-Left-White.png", UriKind.Relative));
            btnGetPreviousSynonums.Background = brushLeftWhite;
        }

        private void btnGetNextSynonums_Click(object sender, RoutedEventArgs e)
        {
            int numOfRowsOfdtForSynonums = dtForSynonums.Rows.Count;

            if (currentSynonums < numOfRowsOfdtForSynonums)
            {
                currentSynonums++;
                clearAllTextBoxes();
                txtBoxKeywordSynonumsInTakeQusInExamPnl.Text = Convert.ToString(dtForSynonums.Rows[currentSynonums - 1][1]);
                txtBoxSynonumsForKeywordInTakeQusInExamPnl.Text = Convert.ToString(dtForSynonums.Rows[currentSynonums - 1][2]);
            }
        }

        private void btnGetNextSynonums_MouseEnter(object sender, MouseEventArgs e)
        {
            var brushRightOrange = new ImageBrush();
            brushRightOrange.ImageSource = new BitmapImage(new Uri("../../Arrow-Right-Orange.png", UriKind.Relative));
            btnGetNextSynonums.Background = brushRightOrange;
        }

        private void btnGetNextSynonums_MouseLeave(object sender, MouseEventArgs e)
        {
            var brushRightWhite = new ImageBrush();
            brushRightWhite.ImageSource = new BitmapImage(new Uri("../../Arrow-Right-White.png", UriKind.Relative));
            btnGetNextSynonums.Background = brushRightWhite;
        }

        string realQues = "";
        string realKeyword = ""; 
        string realModelAnswer = ""; 
        string realPoints = "";
        int currentKeywordNum = 1;

        private void btnTakeSynonumsInAddNewExam_Click(object sender, RoutedEventArgs e)
        {
            realQues = txtBoxQuesInTakeQusInExamPnl.Text;
            realKeyword = txtBoxKeywordInTakeQusInExamPnl.Text;
            realModelAnswer = txtBoxModelAnswerInTakeQusInExamPnl.Text;
            realPoints = txtBoxPointsInTakeQusInExamPnl.Text;

            lblQuesInTakeQusInExamPnl.Visibility = Visibility.Hidden;
            txtBoxQuesInTakeQusInExamPnl.Visibility = Visibility.Hidden;
            lblKeywordInTakeQusInExamPnl.Visibility = Visibility.Hidden;
            txtBoxKeywordInTakeQusInExamPnl.Visibility = Visibility.Hidden;
            lblModelAnswerInTakeQusInExamPnl.Visibility = Visibility.Hidden;
            txtBoxModelAnswerInTakeQusInExamPnl.Visibility = Visibility.Hidden;
            lblPointsInTakeQusInExamPnl.Visibility = Visibility.Hidden;
            txtBoxPointsInTakeQusInExamPnl.Visibility = Visibility.Hidden;
            btnGetPreviousQues.Visibility = Visibility.Hidden;
            lblCurrentQuesNumForArrow.Visibility = Visibility.Hidden;
            btnGetNextQues.Visibility = Visibility.Hidden;
            btnTakeSynonumsInAddNewExam.Visibility = Visibility.Hidden;
            btnSubmitTakeQusInAddNewExam.Visibility = Visibility.Hidden;

            stkPanelTakeSynonumsInAddExamsForMentor.Visibility = Visibility.Visible;
        }

        private void btnFinishTakeSynonumsInAddNewExam_Click(object sender, RoutedEventArgs e)
        {
            if (txtBoxKeywordSynonumsInTakeQusInExamPnl.Text.Trim() == "")
            { MessageBox.Show("Please Enter The Keyword", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
            else if (txtBoxSynonumsForKeywordInTakeQusInExamPnl.Text.Trim() == "")
            { MessageBox.Show("Please Enter The Synonums Of Keyword Splitted By ,", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
            else
            {
                DataRow dr = dtForSynonums.NewRow();
                dr[0] = currentQues;
                dr[1] = txtBoxKeywordSynonumsInTakeQusInExamPnl.Text;
                dr[2] = txtBoxSynonumsForKeywordInTakeQusInExamPnl.Text;
                dtForSynonums.Rows.Add(dr);


                DataTable dtForSwapingSynonums = new DataTable();
                dtForSwapingSynonums.Columns.Add("Questions Number", typeof(System.String));
                dtForSwapingSynonums.Columns.Add("Keyword", typeof(System.String));
                dtForSwapingSynonums.Columns.Add("Synonums", typeof(System.String));
                /*
                DataRow drForSwaping = dtForSwapingSynonums.NewRow();
                drForSwaping[0] = dtForSynonums.Rows[0][0];
                drForSwaping[1] = dtForSynonums.Rows[0][1];
                drForSwaping[2] = dtForSynonums.Rows[0][2];
                dtForSwapingSynonums.Rows.Add(dtForSwapingSynonums);
                */
                bool foundInSwapingTable = false;

                for (int i = 0; i < dtForSynonums.Rows.Count; i++)
                {
                    for (int j = 1; j < dtForSwapingSynonums.Rows.Count; j++)
                    {
                        if (Convert.ToString(dtForSynonums.Rows[i][0]) == Convert.ToString(dtForSwapingSynonums.Rows[j][0]) && Convert.ToString(dtForSynonums.Rows[i][1]) == Convert.ToString(dtForSwapingSynonums.Rows[j][1]) && Convert.ToString(dtForSynonums.Rows[i][2]) == Convert.ToString(dtForSwapingSynonums.Rows[j][2]))
                        { foundInSwapingTable = true; }
                    }
                    if (foundInSwapingTable == false)
                    {
                        DataRow dRowForSwaping = dtForSwapingSynonums.NewRow();
                        dRowForSwaping[0] = dtForSynonums.Rows[i][0];
                        dRowForSwaping[1] = dtForSynonums.Rows[i][1];
                        dRowForSwaping[2] = dtForSynonums.Rows[i][2];
                        dtForSwapingSynonums.Rows.Add(dRowForSwaping);
                    }
                    else
                    { foundInSwapingTable = false; }
                    
                }

                dtForSynonums.Clear();
                
                foreach (DataRow dRow in dtForSwapingSynonums.Rows)
                { dtForSynonums.Rows.Add(dRow.ItemArray); }
                dtForSwapingSynonums.Columns.Remove("Questions Number");
                dtForSwapingSynonums.Columns.Remove("Keyword");
                dtForSwapingSynonums.Columns.Remove("Synonums");

                currentKeywordNum = 1;

                txtBoxKeywordSynonumsInTakeQusInExamPnl.Text = "";
                txtBoxSynonumsForKeywordInTakeQusInExamPnl.Text = "";

                stkPanelTakeSynonumsInAddExamsForMentor.Visibility = Visibility.Hidden;

                lblQuesInTakeQusInExamPnl.Visibility = Visibility.Visible;
                txtBoxQuesInTakeQusInExamPnl.Visibility = Visibility.Visible;
                lblKeywordInTakeQusInExamPnl.Visibility = Visibility.Visible;
                txtBoxKeywordInTakeQusInExamPnl.Visibility = Visibility.Visible;
                lblModelAnswerInTakeQusInExamPnl.Visibility = Visibility.Visible;
                txtBoxModelAnswerInTakeQusInExamPnl.Visibility = Visibility.Visible;
                lblPointsInTakeQusInExamPnl.Visibility = Visibility.Visible;
                txtBoxPointsInTakeQusInExamPnl.Visibility = Visibility.Visible;
                btnGetPreviousQues.Visibility = Visibility.Visible;
                lblCurrentQuesNumForArrow.Visibility = Visibility.Visible;
                btnGetNextQues.Visibility = Visibility.Visible;
                btnTakeSynonumsInAddNewExam.Visibility = Visibility.Visible;
                btnSubmitTakeQusInAddNewExam.Visibility = Visibility.Visible;

                txtBoxQuesInTakeQusInExamPnl.Text = realQues;
                txtBoxKeywordInTakeQusInExamPnl.Text = realModelAnswer;
                txtBoxModelAnswerInTakeQusInExamPnl.Text = realModelAnswer;
                txtBoxPointsInTakeQusInExamPnl.Text = realPoints;

                realQues = "";
                realKeyword = "";
                realModelAnswer = "";
                realPoints = "";
            }
        }


    }
}
