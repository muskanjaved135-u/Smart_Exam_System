using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using MongoDB.Driver;
using SmartExamSystem.Database;

namespace SmartExamSystem.Forms
{
    public partial class AdminResultsForm : Form
    {
        private IMongoCollection<ResultModel> resultCollection;

        public AdminResultsForm()
        {
            InitializeComponent();
            var db = MongoDBConnection.GetDatabase();
            resultCollection = db.GetCollection<ResultModel>("Results"); // Cloud Results collection

            DesignResultsUI();
            LoadAllResults();
        }

        private void DesignResultsUI()
        {
            this.Text = "Exam Results Center - Admin Panel";
            this.Size = new Size(900, 580);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.BackColor = Color.FromArgb(245, 247, 250);

            // Title
            Label title = new Label();
            title.Text = "STUDENT EXAM PERFORMANCE MATRIX";
            title.Font = new Font("Segoe UI", 18, FontStyle.Bold);
            title.ForeColor = Color.FromArgb(18, 95, 170);
            title.Location = new Point(220, 25);
            title.AutoSize = true;
            this.Controls.Add(title);

            // Grid Layout Styling
            dgvAdminResults.Location = new Point(50, 100);
            dgvAdminResults.Size = new Size(780, 390);
            dgvAdminResults.BackgroundColor = Color.White;
            dgvAdminResults.BorderStyle = BorderStyle.None;
            dgvAdminResults.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvAdminResults.AllowUserToAddRows = false;
            dgvAdminResults.ReadOnly = true;
            dgvAdminResults.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void LoadAllResults()
        {
            try
            {
                var resultsList = resultCollection.Find(x => true).ToList();
                dgvAdminResults.DataSource = resultsList;

                // Hide MongoDB ID Column
                if (dgvAdminResults.Columns["Id"] != null) dgvAdminResults.Columns["Id"].Visible = false;

                // Better Headers Formatting
                if (dgvAdminResults.Columns["StudentRoll"] != null) dgvAdminResults.Columns["StudentRoll"].HeaderText = "Roll Number";
                if (dgvAdminResults.Columns["StudentName"] != null) dgvAdminResults.Columns["StudentName"].HeaderText = "Student Name";
                if (dgvAdminResults.Columns["StudentEmail"] != null) dgvAdminResults.Columns["StudentEmail"].HeaderText = "Email Address";
                if (dgvAdminResults.Columns["ExamName"] != null) dgvAdminResults.Columns["ExamName"].HeaderText = "Exam Name";
                if (dgvAdminResults.Columns["TotalQuestions"] != null) dgvAdminResults.Columns["TotalQuestions"].HeaderText = "Total Qs";
                if (dgvAdminResults.Columns["CorrectAnswers"] != null) dgvAdminResults.Columns["CorrectAnswers"].HeaderText = "Correct";

                // 🔥 FIX: Runtime Status Evaluation & Coloring Logic
                foreach (DataGridViewRow row in dgvAdminResults.Rows)
                {
                    if (row.Cells["Percentage"] != null && row.Cells["Percentage"].Value != null)
                    {
                        if (double.TryParse(row.Cells["Percentage"].Value.ToString(), out double percentage))
                        {
                            // Agar student 40 ya usse zyada marks leta hai toh Pass, warna Fail
                            if (percentage >= 40)
                            {
                                row.Cells["Status"].Value = "Pass";
                                row.Cells["Status"].Style.ForeColor = Color.Green;
                                row.Cells["Status"].Style.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                            }
                            else
                            {
                                row.Cells["Status"].Value = "Fail";
                                row.Cells["Status"].Style.ForeColor = Color.Red;
                                row.Cells["Status"].Style.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading results track: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    // Database documents mapping model
    // 🔥 FIXED: Data type handling dynamic kar di taake BsonType Int32 aur String dono accept hon
    [MongoDB.Bson.Serialization.Attributes.BsonIgnoreExtraElements]
    public class ResultModel
    {
        public object Id { get; set; }

        // 🔥 FIX: 'string' ki jaga 'object' kiya taake database ka Int32 number ya String text dono match ho sakein
        public object ResultID { get; set; }

        public string StudentRoll { get; set; }
        public string StudentName { get; set; }
        public string StudentEmail { get; set; }
        public string ExamName { get; set; }
        public int TotalQuestions { get; set; }
        public int CorrectAnswers { get; set; }
        public int Marks { get; set; }
        public double Percentage { get; set; }
        public string Status { get; set; }
        public DateTime Date { get; set; }
    }
}