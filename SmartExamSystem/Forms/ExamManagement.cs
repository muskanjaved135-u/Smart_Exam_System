using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using SmartExamSystem.Models;
using SmartExamSystem.Services;


namespace SmartExamSystem.Forms
{
    public partial class ExamManagement : Form
    {


        ExamService service;



        public ExamManagement()
        {
            InitializeComponent();


            service =
            new ExamService();


            DesignExamUI();


            LoadExams();

        }




        private void DesignExamUI()
        {

            this.Text =
            "Exam Management";


            this.Size =
            new Size(1000, 600);


            this.StartPosition =
            FormStartPosition.CenterScreen;



            this.BackColor =
            Color.FromArgb(245, 247, 250);





            Label title =
            new Label();


            title.Text =
            "CREATE & MANAGE EXAMS";


            title.Font =
            new Font(
            "Segoe UI",
            24,
            FontStyle.Bold);


            title.ForeColor =
            Color.FromArgb(18, 95, 170);


            title.Location =
            new Point(300, 30);


            title.AutoSize = true;


            this.Controls.Add(title);





            AddLabel(
            "Exam Name",
            80,
            120);



            txtExamName.Location =
            new Point(80, 150);


            txtExamName.Size =
            new Size(300, 35);






            AddLabel(
            "Duration (Minutes)",
            80,
            210);



            txtDuration.Location =
            new Point(80, 240);


            txtDuration.Size =
            new Size(300, 35);






            AddLabel(
            "Total Questions",
            80,
            300);



            txtTotalQuestions.Location =
            new Point(80, 330);


            txtTotalQuestions.Size =
            new Size(300, 35);







            btnSaveExam.Text =
            "CREATE EXAM";


            btnSaveExam.Location =
            new Point(80, 410);


            btnSaveExam.Size =
            new Size(300, 45);


            StyleButton(btnSaveExam);







            dgvExams.Location =
            new Point(450, 120);


            dgvExams.Size =
            new Size(450, 330);


            dgvExams.BackgroundColor =
            Color.White;


            dgvExams.AutoSizeColumnsMode =
            DataGridViewAutoSizeColumnsMode.Fill;


            dgvExams.ReadOnly = true;


        }






        private void AddLabel(string text, int x, int y)
        {

            Label lbl =
            new Label();


            lbl.Text = text;


            lbl.Font =
            new Font(
            "Segoe UI",
            11);


            lbl.AutoSize = true;


            lbl.Location =
            new Point(x, y);


            this.Controls.Add(lbl);

        }





        private void StyleButton(Button btn)
        {

            btn.BackColor =
            Color.FromArgb(18, 95, 170);


            btn.ForeColor =
            Color.White;


            btn.FlatStyle =
            FlatStyle.Flat;


            btn.FlatAppearance.BorderSize = 0;


            btn.Font =
            new Font(
            "Segoe UI",
            11,
            FontStyle.Bold);


            btn.Cursor =
            Cursors.Hand;

        }






        private void LoadExams()
        {

            List<ExamModel> list =
            service.GetExams();


            dgvExams.DataSource =
            list;

        }






        private void btnSaveExam_Click(object sender, EventArgs e)
        {


            if (txtExamName.Text == "" || txtDuration.Text == "" || txtTotalQuestions.Text == "")
            {
                MessageBox.Show("Please fill all fields");
                return;
            }

            ExamModel exam = new ExamModel();
            exam.ExamName = txtExamName.Text;
            exam.TimeDuration = Convert.ToInt32(txtDuration.Text);
            exam.TotalQuestions = Convert.ToInt32(txtTotalQuestions.Text);

            service.AddExam(exam);
            MessageBox.Show("Exam Created Successfully! Now let's add questions.", "Success");

            // Dynamic Flow: Direct Question form open karein aur values pass karein
            QuestionManagement qForm = new QuestionManagement(exam.ExamName, exam.TotalQuestions);
            qForm.Show();

            this.Close(); // Manage Exam form close ho jayega

        }



    }
}