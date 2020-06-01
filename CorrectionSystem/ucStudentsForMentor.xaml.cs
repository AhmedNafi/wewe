using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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
using System.Diagnostics;

namespace CorrectionSystem
{
    /// <summary>
    /// Interaction logic for ucStudentsForMentor.xaml
    /// </summary>
    public partial class ucStudentsForMentor : UserControl
    {
        public ucStudentsForMentor()
        {
            InitializeComponent();

            clearAllTextBoxes();
            chnageBtnBackgroundImageAndHideAllPNL();
            var brushHomeOrange = new ImageBrush();
            brushHomeOrange.ImageSource = new BitmapImage(new Uri("../../Home-Orange.png", UriKind.Relative));
            btnHomeInMentorPNL.Background = brushHomeOrange;

            stkPanelHomeStudentsForMentor.Visibility = Visibility.Visible;

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

        public void chnageBtnBackgroundImageAndHideAllPNL()
        {

            var brushHomeWhite = new ImageBrush();
            brushHomeWhite.ImageSource = new BitmapImage(new Uri("../../Home-White.png", UriKind.Relative));
            btnHomeInMentorPNL.Background = brushHomeWhite;

            var brushAddWhite = new ImageBrush();
            brushAddWhite.ImageSource = new BitmapImage(new Uri("../../Add-White.png", UriKind.Relative));
            btnAddStudentsAnswersInMentorPNL.Background = brushAddWhite;

            var brushEditWhite = new ImageBrush();
            brushEditWhite.ImageSource = new BitmapImage(new Uri("../../Edit-White.png", UriKind.Relative));
            btnEditStudentInMentorPNL.Background = brushEditWhite;

            var brushRemoveWhite = new ImageBrush();
            brushRemoveWhite.ImageSource = new BitmapImage(new Uri("../../Remove-White.png", UriKind.Relative));
            btnRemoveStudentInMentorPNL.Background = brushRemoveWhite;

            
            stkPanelHomeStudentsForMentor.Visibility = Visibility.Hidden;
            stkPanelAddStudentsAnswersForMentor.Visibility = Visibility.Hidden;
            /*
            stkPanelViewSelectedExam.Visibility = Visibility.Hidden;
            stkPanelViewQustionsInViewSelectedExam.Visibility = Visibility.Hidden;
            */
        }

        public void clearAllTextBoxes()
        {
            /*
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
            txtBoxSynonymsInTakeQusInExamPnl.Text = "";
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
            txtBoxSynonymsInViewQustionsInViewSelectedExam.Text = "";

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
            */
        }



        bool clickOnHomeBtn = false;
        bool clickOnAddBtn = false;
        bool clickOnEditBtn = false;
        bool clickOnRemoveBtn = false;
        string currentPnl = "HomeStudents";

        private void btnHomeInMentorPNL_Click(object sender, RoutedEventArgs e)
        {
            clearAllTextBoxes();
            chnageBtnBackgroundImageAndHideAllPNL();
            var brushHomeOrange = new ImageBrush();
            brushHomeOrange.ImageSource = new BitmapImage(new Uri("../../Home-Orange.png", UriKind.Relative));
            btnHomeInMentorPNL.Background = brushHomeOrange;

            stkPanelHomeStudentsForMentor.Visibility = Visibility.Visible;

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
                if (currentPnl != "HomeStudents")
                {
                    var brushHomeWhite = new ImageBrush();
                    brushHomeWhite.ImageSource = new BitmapImage(new Uri("../../Home-White.png", UriKind.Relative));
                    btnHomeInMentorPNL.Background = brushHomeWhite;
                }
            }
            else
            { clickOnHomeBtn = false; }
        }

        private void btnAddStudentsAnswersInMentorPNL_Click(object sender, RoutedEventArgs e)
        {
            clearAllTextBoxes();
            chnageBtnBackgroundImageAndHideAllPNL();
            var brushAddOrange = new ImageBrush();
            brushAddOrange.ImageSource = new BitmapImage(new Uri("../../Add-Orange.png", UriKind.Relative));
            btnAddStudentsAnswersInMentorPNL.Background = brushAddOrange;

            stkPanelAddStudentsAnswersForMentor.Visibility = Visibility.Visible;

            clickOnAddBtn = true;
            currentPnl = "AddStudents";
        }

        private void btnAddStudentsAnswersInMentorPNL_MouseEnter(object sender, MouseEventArgs e)
        {
            var brushAddNewOrange = new ImageBrush();
            brushAddNewOrange.ImageSource = new BitmapImage(new Uri("../../Add-Orange.png", UriKind.Relative));
            btnAddStudentsAnswersInMentorPNL.Background = brushAddNewOrange;
        }

        private void btnAddStudentsAnswersInMentorPNL_MouseLeave(object sender, MouseEventArgs e)
        {
            if (clickOnAddBtn == false)
            {
                if (currentPnl != "AddStudents")
                {
                    var brushAddNewWhite = new ImageBrush();
                    brushAddNewWhite.ImageSource = new BitmapImage(new Uri("../../Add-White.png", UriKind.Relative));
                    btnAddStudentsAnswersInMentorPNL.Background = brushAddNewWhite;
                }
            }
            else
            { clickOnAddBtn = false; }
        }

        private void btnEditStudentInMentorPNL_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnEditStudentInMentorPNL_MouseEnter(object sender, MouseEventArgs e)
        {
            var brushEditOrange = new ImageBrush();
            brushEditOrange.ImageSource = new BitmapImage(new Uri("../../Edit-Orange.png", UriKind.Relative));
            btnEditStudentInMentorPNL.Background = brushEditOrange;
        }

        private void btnEditStudentInMentorPNL_MouseLeave(object sender, MouseEventArgs e)
        {
            if (clickOnEditBtn == false)
            {
                if (currentPnl != "EditStudent")
                {
                    var brushEditWhite = new ImageBrush();
                    brushEditWhite.ImageSource = new BitmapImage(new Uri("../../Edit-White.png", UriKind.Relative));
                    btnEditStudentInMentorPNL.Background = brushEditWhite;
                }
            }
            else
            { clickOnEditBtn = false; }
        }

        private void btnRemoveStudentInMentorPNL_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnRemoveStudentInMentorPNL_MouseEnter(object sender, MouseEventArgs e)
        {
            var brushRemoveOrange = new ImageBrush();
            brushRemoveOrange.ImageSource = new BitmapImage(new Uri("../../Remove-Orange.png", UriKind.Relative));
            btnRemoveStudentInMentorPNL.Background = brushRemoveOrange;
        }

        private void btnRemoveStudentInMentorPNL_MouseLeave(object sender, MouseEventArgs e)
        {
            if (clickOnRemoveBtn == false)
            {
                if (currentPnl != "RemoveStudent")
                {
                    var brushRemoveWhite = new ImageBrush();
                    brushRemoveWhite.ImageSource = new BitmapImage(new Uri("../../Remove-White.png", UriKind.Relative));
                    btnRemoveStudentInMentorPNL.Background = brushRemoveWhite;
                }
            }
            else
            { clickOnRemoveBtn = false; }
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

        private void btnChooseFileForStuAnswer_Click(object sender, RoutedEventArgs e)
        {
            string fileContent1 = "";

            OpenFileDialog opFileDilgSelectImagesFiles = new OpenFileDialog();
            opFileDilgSelectImagesFiles.Multiselect = true;
            opFileDilgSelectImagesFiles.Title = "Choose Images For Students Answers";
            if (opFileDilgSelectImagesFiles.ShowDialog() == true) 
            {
                foreach (String file in opFileDilgSelectImagesFiles.FileNames)
                {
                    DataTable dtForStudentAnswer = new DataTable();
                    dtForStudentAnswer.Columns.Add("Student ID", typeof(System.String));
                    dtForStudentAnswer.Columns.Add("Exam ID", typeof(System.String));
                    dtForStudentAnswer.Columns.Add("Answers", typeof(System.String));
                    dtForStudentAnswer.Columns.Add("Answers After Correct", typeof(System.String));
                    dtForStudentAnswer.Columns.Add("Points For Answers", typeof(System.String));
                   
                    //takePathOfImageReturnPathOfFile(file);
                    string fileContent = File.ReadAllText(file);

                    string[] arrForAnswersAfterSplit = fileContent.Split(new string[] { "|*|" }, StringSplitOptions.None);
                    int numOfElementInArrAnswers = arrForAnswersAfterSplit.Length;
                    for (int i = 2; i < numOfElementInArrAnswers; i++)
                    {
                        DataRow dr = dtForStudentAnswer.NewRow();
                        dr[0] = arrForAnswersAfterSplit[0];
                        dr[1] = arrForAnswersAfterSplit[1];
                        dr[2] = arrForAnswersAfterSplit[i];
                        dr[3] = "";
                        dr[4] = "";
                        dtForStudentAnswer.Rows.Add(dr);
                    }
                    string studentId = arrForAnswersAfterSplit[0];
                    string examId = arrForAnswersAfterSplit[1];
                    string tableName = "stu" + studentId + "answerexam" + examId;
                    try
                    {
                        string tableNameSynonums = "SynonumsForExam" + examId;

                        Database.Run("CREATE TABLE '" + tableName + "' ( 'stuid' TEXT, 'examid' TEXT, 'answers' TEXT, 'answersaftercorrect' TEXT, 'pointsforanswers' INTEGER, PRIMARY KEY('stuid','examid','answers'));");

                        DataTable dtForSynonums = Database.returnTable("select * from " + tableNameSynonums);

                        /*
                        IDictionary<string, string[]> dictHaveSynonumsValue = new Dictionary<string, string[]>();
                        for (int i = 0; i < dtForSynonums.Rows.Count; i++)
                        {
                            string synonumsValues = Convert.ToString(dtForSynonums.Rows[i][2]);
                            string[] synonumsValuesAsArray = synonumsValues.Split(',');
                            dictHaveSynonumsValue.Add(Convert.ToString(dtForSynonums.Rows[i][1]), synonumsValuesAsArray);
                        }
                        */

                        int numberOfRecords = dtForStudentAnswer.Rows.Count;

                        for (int i = 0; i < numberOfRecords; i++)
                        {
                            IDictionary<string, string[]> dictHaveSynonumsValue = new Dictionary<string, string[]>();
                            for (int j = 0; j < dtForSynonums.Rows.Count; j++)
                            {
                                if (Convert.ToString(dtForSynonums.Rows[j][0]) == Convert.ToString(i + 1))
                                {
                                    string synonumsValues = Convert.ToString(dtForSynonums.Rows[j][2]);
                                    string[] synonumsValuesAsArray = synonumsValues.Split(',');
                                    dictHaveSynonumsValue.Add(Convert.ToString(dtForSynonums.Rows[j][1]), synonumsValuesAsArray);
                                }
                            }

                            string answers = Convert.ToString(dtForStudentAnswer.Rows[i][2]);
                            string answersaftercorrect = correctStuAnswers(answers, dictHaveSynonumsValue);
                            int pointsforanswers = calcPointsForQus(answersaftercorrect);
                            Database.Run("insert into " + tableName + " values('" + studentId + "', '" + examId + "', '" + answers + "', '" + answersaftercorrect + "', " + pointsforanswers + ");");
                        }
                    }
                    catch (Exception ex)
                    { MessageBox.Show("Error here " + ex.Message, "Student Answer", MessageBoxButton.OK, MessageBoxImage.Error); }
                }

                MessageBox.Show("Answers Of Students Has Been Successfully Added", "Student Answer", MessageBoxButton.OK, MessageBoxImage.Information);

                /*
                string pathOfImage1 = selectImagesFiles.FileName;
                txtBoxPathOfFileForStuAnswer.Text = pathOfImage1;
                string pathOfFileThatHaveAnswers22 = takePathOfImageReturnPathOfFile(pathOfImage1);
                ahmed.Text = pathOfFileThatHaveAnswers22;
                
                MessageBox.Show(pathOfFileThatHaveAnswers22);
                */
                //fileContent1 = File.ReadAllText(pathOfFileThatHaveAnswers22);
                //txtBoxPathOfFileForStuAnswer.Text = selectImagesFiles.FileName;
                //fileContent = File.ReadAllText(@"C: \Users\Mohamed.S.Nafi\Desktop\stu1.txt");
                //fileContent = File.ReadAllText(txtBoxPathOfFileForStuAnswer.Text);
                /*
                string[] arrForAnswersAfterSplit = fileContent.Split(new string[] { "|*|" }, StringSplitOptions.None);
                int numOfElementInArrAnswers = arrForAnswersAfterSplit.Length;
                for (int i = 2; i < numOfElementInArrAnswers; i++)
                {
                    DataRow dr = dtForStudentAnswer.NewRow();
                    dr[0] = arrForAnswersAfterSplit[0];
                    dr[1] = arrForAnswersAfterSplit[1];
                    dr[2] = arrForAnswersAfterSplit[i];
                    dr[3] = "";
                    dr[4] = "";
                    dtForStudentAnswer.Rows.Add(dr);
                }
                string studentId = arrForAnswersAfterSplit[0];
                string examId = arrForAnswersAfterSplit[1];
                string tableName = "stu" + studentId + "answerexam" + examId;
                try
                {
                    Database.Run("CREATE TABLE '" + tableName + "' ( 'stuid' TEXT, 'examid' TEXT, 'answers' TEXT, 'answersaftercorrect' TEXT, 'pointsforanswers' INTEGER, PRIMARY KEY('stuid','examid','answers'));");

                    int numberOfRecords = dtForStudentAnswer.Rows.Count;

                    for (int i = 0; i < numberOfRecords; i++)
                    {
                        string answers = Convert.ToString(dtForStudentAnswer.Rows[i][2]);
                        string answersaftercorrect = correctStuAnswers(answers);
                        int pointsforanswers = calcPointsForQus(answersaftercorrect);
                        Database.Run("insert into " + tableName + " values('" + studentId + "', '" + examId + "', '" + answers + "', '" + answersaftercorrect + "', " + pointsforanswers + ");");
                    }

                    MessageBox.Show("Answers Of Student Has Been Successfully Added", "Student Answer", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                { MessageBox.Show("Error here " + ex.Message, "Student Answer", MessageBoxButton.OK, MessageBoxImage.Error); }
                */
            }

        }

        public static string takePathOfImageReturnPathOfFile(string pathOfImg)
        {
            /*
             * using System.Diagnostics;

            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();

            cmd.StandardInput.WriteLine("echo Oscar");
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            cmd.WaitForExit();
            Console.WriteLine(cmd.StandardOutput.ReadToEnd());
                         */

            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();
            OpenFileDialog ahmedopenfiledialog = new OpenFileDialog();
            ahmedopenfiledialog.ShowDialog();
            string path1 = "\"F:\\Ahmed Nafi\\Graduation Project v 2.0\\dist\\imtote\\imtote.exe\"";
            string path2 = " -i ";
            string path3 = "\"C:\\Users\\Mohamed.S.Nafi\\Desktop\\img111.jpg\"";
            string last = "'" + ahmedopenfiledialog.FileName + "' -i '" + pathOfImg + "'";
            //string anyCommand = ahmedopenfiledialog.FileName + " -i " + pathOfImg;
            string anyCommand = "C:\\Users\\Mohamed.S.Nafi\\Desktop\\1\\dist\\imtote\\imtote.exe -i C:\\Users\\Mohamed.S.Nafi\\Desktop\\img111.jpg";
            MessageBox.Show(anyCommand);
            cmd.StandardInput.WriteLine(anyCommand);
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            cmd.WaitForExit();
            
            MessageBox.Show(cmd.StandardOutput.ReadToEnd());
            return "";

            //var proc1 = new ProcessStartInfo();
            //OpenFileDialog ahmedopenfiledialog = new OpenFileDialog();
            //ahmedopenfiledialog.ShowDialog();
            //string anyCommand = "\"" + ahmedopenfiledialog.FileName + "\" -i \"" + pathOfImg + "\"";
            //proc1.UseShellExecute = true;

            //proc1.WorkingDirectory = @"C:\Windows\System32";

            //proc1.FileName = @"C:\Windows\System32\cmd.exe";
            //proc1.Verb = "runas";
            //proc1.Arguments = "/c " + anyCommand;
            //proc1.WindowStyle = ProcessWindowStyle.Hidden;
            //Process.Start(proc1);

        }

        public static string correctStuAnswers(string ansOfQues, IDictionary<string, string[]> dictionaryOfSynonums)
        {
            
            foreach (KeyValuePair<string, string[]> record in dictionaryOfSynonums) 
            { MessageBox.Show("Key:" + record.Key); }
            
            return "after";
        }

        public static int calcPointsForQus(string ansOfQues)
        {
            return 0;
        }



    }
}
