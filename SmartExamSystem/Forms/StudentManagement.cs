using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using MongoDB.Driver;
using SmartExamSystem.Database;

namespace SmartExamSystem.Forms
{

    public partial class dgvStudent : Form
    {
        private IMongoCollection<StudentModel> studentCollection;
        // FIX: DataGridView ko code-behind mein manually declare kiya taake designer file error na de
     

        public dgvStudent()
        {
            InitializeComponent();
            var db = MongoDBConnection.GetDatabase();
            studentCollection = db.GetCollection<StudentModel>("Students");

            DesignStudentUI();
            LoadStudentsData();
        }

        private void DesignStudentUI()
        {
            this.Text = "Student Management - Admin Panel";
            this.Size = new Size(850, 550);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.None; // Dashboard panel mein fit hone ke liye border none kiya
            this.BackColor = Color.FromArgb(245, 247, 250);

            // Title Label
            Label title = new Label();
            title.Text = "REGISTERED STUDENTS LIST";
            title.Font = new Font("Segoe UI", 18, FontStyle.Bold);
            title.ForeColor = Color.FromArgb(18, 95, 170);
            title.Location = new Point(40, 25);
            title.AutoSize = true;
            this.Controls.Add(title);

            // DataGridView Grid Styling & Setup
            dgvStudents.Location = new Point(40, 80);
            dgvStudents.Size = new Size(750, 400);
            dgvStudents.BackgroundColor = Color.White;
            dgvStudents.BorderStyle = BorderStyle.None;
            dgvStudents.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvStudents.AllowUserToAddRows = false;
            dgvStudents.ReadOnly = true;
            dgvStudents.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvStudents.RowHeadersVisible = false; // Layout clean rakhne ke liye

            // Grid Styling for modern look
            dgvStudents.EnableHeadersVisualStyles = false;
            dgvStudents.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(18, 95, 170);
            dgvStudents.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvStudents.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvStudents.ColumnHeadersHeight = 35;
            dgvStudents.DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            dgvStudents.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 243, 249);

            // Grid ko main controls panel mein add karna
            this.Controls.Add(dgvStudents);
        }

        public void LoadStudentsData()
        {
            try
            {
                // Cloud MongoDB se saare students fetch karna
                var list = studentCollection.Find(x => true).ToList();

                // Binding direct to Grid
                dgvStudents.DataSource = list;

                // MongoDB standard metadata column/Id ko interface se hide karna
                if (dgvStudents.Columns["Id"] != null)
                    dgvStudents.Columns["Id"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error fetching students from cloud database: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    // Student Model Data Structure
    public class StudentModel
    {
        [MongoDB.Bson.Serialization.Attributes.BsonId]
        [MongoDB.Bson.Serialization.Attributes.BsonIgnoreIfDefault]
        public object Id { get; set; }

        public string RollNumber { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        // 🔥 FIX 1: Naye students ke liye DOB property
        [MongoDB.Bson.Serialization.Attributes.BsonIgnoreIfNull]
        public string DateOfBirth { get; set; }

        // 🔥 FIX 2: Purane database records ke liye Semester property (BsonIgnoreIfNull lazmi lagayein)
        [MongoDB.Bson.Serialization.Attributes.BsonIgnoreIfNull]
        public string Semester { get; set; }
    }
}