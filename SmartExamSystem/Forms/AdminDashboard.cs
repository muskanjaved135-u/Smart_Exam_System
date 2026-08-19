using SmartExamSystem.Forms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using MongoDB.Driver;
using SmartExamSystem.Database;

namespace SmartExamSystem
{
    public partial class AdminDashboard : Form
    {
        Button btnQuestions;
        Button btnExams;
        Button btnResults;
        Button btnStudents;
        private Panel pnlMain;
        private Panel pnlStudentList;
        private DataGridView dgvStudents;
        Button btnLogout;

        // MongoDB collection instance targeted to Users collection
        private IMongoCollection<StudentModel> userCollection;

        public AdminDashboard()
        {
            // 1. Pehle pure interface controls aur buttons ko build karein
            BuildDashboard();

            // 2. MongoDB Database se connect karke "Users" collection target karein
            try
            {
                var db = MongoDBConnection.GetDatabase();
                // 🔥 FIX 1: Chunki aapka data 'Users' collection me save hai, isliye hum isi ko read karenge!
                userCollection = db.GetCollection<StudentModel>("Users");
            }
            catch (Exception ex)
            {
                MessageBox.Show("MongoDB Connection Error: " + ex.Message);
            }
        }

        private void BuildDashboard()
        {
            this.Text = "Admin Dashboard - Smart Exam System";
            this.Size = new Size(1200, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(245, 247, 250);

            // SIDEBAR
            Panel sidebar = new Panel();
            sidebar.Dock = DockStyle.Left;
            sidebar.Width = 260;
            sidebar.BackColor = Color.FromArgb(18, 95, 170);
            this.Controls.Add(sidebar);

            Label logo = new Label();
            logo.Text = "SMART\nEXAM";
            logo.Font = new Font("Segoe UI", 32, FontStyle.Bold);
            logo.ForeColor = Color.White;
            logo.AutoSize = true;
            logo.Location = new Point(55, 50);
            sidebar.Controls.Add(logo);

            btnQuestions = CreateMenuButton("📚  Questions", 190);
            sidebar.Controls.Add(btnQuestions);

            btnExams = CreateMenuButton("📝  Exams", 260);
            sidebar.Controls.Add(btnExams);

            btnStudents = CreateMenuButton("👨‍🎓  Students", 330);
            sidebar.Controls.Add(btnStudents);

            btnResults = CreateMenuButton("📊  Results", 400);
            sidebar.Controls.Add(btnResults);

            btnLogout = CreateMenuButton("Logout", 550);
            sidebar.Controls.Add(btnLogout);

            // Click Event Binding
            btnQuestions.Click += btnQuestions_Click;
            btnExams.Click += btnExams_Click;
            btnStudents.Click += btnStudents_Click;
            btnResults.Click += btnResults_Click;
            btnLogout.Click += btnLogout_Click;

            // HEADER
            Label title = new Label();
            title.Text = "Admin Dashboard";
            title.Font = new Font("Segoe UI", 34, FontStyle.Bold);
            title.ForeColor = Color.FromArgb(30, 30, 30);
            title.Location = new Point(330, 60);
            title.AutoSize = true;
            this.Controls.Add(title);

            Label welcome = new Label();
            welcome.Text = "Welcome back, Administrator";
            welcome.Font = new Font("Segoe UI", 14);
            welcome.ForeColor = Color.Gray;
            welcome.Location = new Point(335, 120);
            welcome.AutoSize = true;
            this.Controls.Add(welcome);

            // Student Panel aur DataGridView Setup
            pnlStudentList = new Panel();
            pnlStudentList.Location = new Point(330, 190);
            pnlStudentList.Size = new Size(820, 450);
            pnlStudentList.BackColor = Color.White;
            pnlStudentList.Visible = false;

            dgvStudents = new DataGridView();
            dgvStudents.Dock = DockStyle.Fill;
            dgvStudents.BackgroundColor = Color.White;
            dgvStudents.BorderStyle = BorderStyle.None;
            dgvStudents.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvStudents.AllowUserToAddRows = false;
            dgvStudents.ReadOnly = true;

            pnlStudentList.Controls.Add(dgvStudents);
            this.Controls.Add(pnlStudentList);

            // CARDS
            CreateCard("📚 Questions", "Manage exam questions", 340, 200);
            CreateCard("📝 Exams", "Create and manage exams", 650, 200);
            CreateCard("📊 Results", "View student performance", 340, 370);
            CreateCard("👨‍🎓 Students", "Manage registered users", 650, 370);
        }

        private Button CreateMenuButton(string text, int y)
        {
            Button btn = new Button();
            btn.Text = text;
            btn.Size = new Size(210, 45);
            btn.Location = new Point(25, y);
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = Color.FromArgb(35, 120, 200);
            btn.ForeColor = Color.White;
            btn.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            btn.Cursor = Cursors.Hand;
            return btn;
        }

        private void CreateCard(string title, string detail, int x, int y)
        {
            Panel card = new Panel();
            card.Size = new Size(260, 130);
            card.Location = new Point(x, y);
            card.BackColor = Color.White;

            Label heading = new Label();
            heading.Text = title;
            heading.Font = new Font("Segoe UI", 18, FontStyle.Bold);
            heading.Location = new Point(20, 20);
            heading.AutoSize = true;
            card.Controls.Add(heading);

            Label desc = new Label();
            desc.Text = detail;
            desc.ForeColor = Color.Gray;
            desc.Location = new Point(20, 65);
            desc.AutoSize = true;
            card.Controls.Add(desc);

            this.Controls.Add(card);
        }

        private void btnQuestions_Click(object sender, EventArgs e)
        {
            QuestionManagement q = new QuestionManagement();
            q.Show();
        }

        private void btnExams_Click(object sender, EventArgs e)
        {
            ExamManagement ex = new ExamManagement();
            ex.Show();
        }

        private void btnResults_Click(object sender, EventArgs e)
        {
            AdminResultsForm resultsForm = new AdminResultsForm();
            resultsForm.Show();
        }

        private void btnStudents_Click(object sender, EventArgs e)
        {
            LoadRegisteredStudents();
        }

        private void LoadRegisteredStudents()
        {
            try
            {
                if (userCollection == null)
                {
                    var db = SmartExamSystem.Database.MongoDBConnection.GetDatabase();
                    userCollection = db.GetCollection<StudentModel>("Users");
                }

                // Users collection se sirf unko layen jinka Role == "Student" ho
                var studentsList = userCollection.Find(x => x.Role == "Student").ToList();

                if (dgvStudents != null)
                {
                    dgvStudents.DataSource = null;
                    dgvStudents.DataSource = studentsList;

                    // Extra/Internal database fields ko hide karein
                    if (dgvStudents.Columns["Id"] != null) dgvStudents.Columns["Id"].Visible = false;
                    if (dgvStudents.Columns["Password"] != null) dgvStudents.Columns["Password"].Visible = false;
                    if (dgvStudents.Columns["Semester"] != null) dgvStudents.Columns["Semester"].Visible = false; // Agar semester nahi chahiye to hide kar diya

                    // 1. Headers text mapping layout settings
                    if (dgvStudents.Columns["RollNumber"] != null) dgvStudents.Columns["RollNumber"].HeaderText = "Roll Number";
                    if (dgvStudents.Columns["Name"] != null) dgvStudents.Columns["Name"].HeaderText = "Full Name";
                    if (dgvStudents.Columns["Email"] != null) dgvStudents.Columns["Email"].HeaderText = "Email Address";
                    if (dgvStudents.Columns["DateOfBirth"] != null) dgvStudents.Columns["DateOfBirth"].HeaderText = "Date of Birth";
                    if (dgvStudents.Columns["Role"] != null) dgvStudents.Columns["Role"].HeaderText = "Role";

                    // 2. 🔥 FIX: Columns ki sequencing (Order) explicit set karein taake Roll Number sab se pehle aaye
                    if (dgvStudents.Columns["RollNumber"] != null) dgvStudents.Columns["RollNumber"].DisplayIndex = 0;
                    if (dgvStudents.Columns["Name"] != null) dgvStudents.Columns["Name"].DisplayIndex = 1;
                    if (dgvStudents.Columns["Email"] != null) dgvStudents.Columns["Email"].DisplayIndex = 2;
                    if (dgvStudents.Columns["DateOfBirth"] != null) dgvStudents.Columns["DateOfBirth"].DisplayIndex = 3;
                    if (dgvStudents.Columns["Role"] != null) dgvStudents.Columns["Role"].DisplayIndex = 4;

                    dgvStudents.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dgvStudents.Refresh();

                    // Panel visibility on karein
                    pnlStudentList.Visible = true;
                    pnlStudentList.BringToFront();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading students list: " + ex.Message);
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            LoginForm login = new LoginForm();
            login.Show();
            this.Close();
        }

        private void AdminDashboard_Load(object sender, EventArgs e)
        {
            LoadRegisteredStudents();
        }

        private void InitializeComponent()
        {
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnlStudentList = new System.Windows.Forms.Panel();
            this.dgvStudents = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStudents)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Location = new System.Drawing.Point(206, 46);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(200, 100);
            this.pnlMain.TabIndex = 1;
            // 
            // pnlStudentList
            // 
            this.pnlStudentList.Location = new System.Drawing.Point(133, 46);
            this.pnlStudentList.Name = "pnlStudentList";
            this.pnlStudentList.Size = new System.Drawing.Size(200, 100);
            this.pnlStudentList.TabIndex = 2;
            // 
            // dgvStudents
            // 
            this.dgvStudents.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStudents.Location = new System.Drawing.Point(91, 76);
            this.dgvStudents.Name = "dgvStudents";
            this.dgvStudents.RowHeadersWidth = 51;
            this.dgvStudents.RowTemplate.Height = 24;
            this.dgvStudents.Size = new System.Drawing.Size(240, 150);
            this.dgvStudents.TabIndex = 3;
            // 
            // AdminDashboard
            // 
            this.ClientSize = new System.Drawing.Size(509, 219);
            this.Controls.Add(this.dgvStudents);
            this.Controls.Add(this.pnlStudentList);
            this.Controls.Add(this.pnlMain);
            this.Name = "AdminDashboard";
            this.Load += new System.EventHandler(this.AdminDashboard_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvStudents)).EndInit();
            this.ResumeLayout(false);
        }
    }

    // MongoDB Data mapping class matching exactly with 'Users' collection document scheme
    public class StudentModel
    {
        [MongoDB.Bson.Serialization.Attributes.BsonId]
        [MongoDB.Bson.Serialization.Attributes.BsonIgnoreIfDefault]
        public object Id { get; set; }

        // MongoDB database fields key mapping structure
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

        [MongoDB.Bson.Serialization.Attributes.BsonIgnoreIfNull]
        public string RollNumber { get; set; }

        [MongoDB.Bson.Serialization.Attributes.BsonIgnoreIfNull]
        public string DateOfBirth { get; set; }

        [MongoDB.Bson.Serialization.Attributes.BsonIgnoreIfNull]
        public string Semester { get; set; }
    }
}