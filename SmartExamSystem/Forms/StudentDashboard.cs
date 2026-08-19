using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using SmartExamSystem.Models;
using SmartExamSystem.Services;

namespace SmartExamSystem.Forms
{
    public partial class StudentDashboard : Form
    {
        private ExamService examService = new ExamService();

        private string loggedInStudentName;
        private string loggedInStudentRoll;
        private string loggedInStudentEmail;

        private Panel sidebarPanel = new Panel();
        private Panel mainContentPanel = new Panel();
        private FlowLayoutPanel examsFlowPanel = new FlowLayoutPanel();

        public StudentDashboard(string name, string rollNum, string email)
        {
            InitializeComponent();

            // Clean values synchronization
            this.loggedInStudentName = !string.IsNullOrEmpty(name) ? name : "Student";
            this.loggedInStudentRoll = !string.IsNullOrEmpty(rollNum) ? rollNum : "N/A";
            this.loggedInStudentEmail = !string.IsNullOrEmpty(email) ? email : "N/A";

            CreateModernStudentDashboard();
            LoadLiveExamsIntoGrid();
        }

        private void CreateModernStudentDashboard()
        {
            this.Text = "Student Console - Smart Exam System";
            this.Size = new Size(1100, 680);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.BackColor = Color.FromArgb(244, 246, 249);

            // ==========================================
            // SIDEBAR NAVIGATION PANEL
            // ==========================================
            sidebarPanel.Size = new Size(260, 680);
            sidebarPanel.Location = new Point(0, 0);
            sidebarPanel.BackColor = Color.FromArgb(28, 35, 43);
            this.Controls.Add(sidebarPanel);

            Panel profileBox = new Panel();
            profileBox.Size = new Size(220, 110);
            profileBox.Location = new Point(20, 30);
            profileBox.BackColor = Color.FromArgb(43, 53, 65);
            sidebarPanel.Controls.Add(profileBox);

            // Profile Label Styling
            if (lblWelcome != null)
            {
                lblWelcome.Text = loggedInStudentName.ToUpper();
                lblWelcome.Font = new Font("Segoe UI", 12, FontStyle.Bold);
                lblWelcome.ForeColor = Color.White;
                lblWelcome.Location = new Point(10, 15);
                lblWelcome.Size = new Size(200, 25);
                lblWelcome.TextAlign = ContentAlignment.MiddleCenter;
                profileBox.Controls.Add(lblWelcome);
                lblWelcome.BringToFront();
            }

            // Label displays actual Roll Number now instead of Email
            Label lblMetaSub = new Label();
            lblMetaSub.Text = $"Roll No: {loggedInStudentRoll}";
            lblMetaSub.Font = new Font("Segoe UI", 9, FontStyle.Regular);
            lblMetaSub.ForeColor = Color.FromArgb(170, 180, 195);
            lblMetaSub.Location = new Point(10, 50);
            lblMetaSub.Size = new Size(200, 20);
            lblMetaSub.TextAlign = ContentAlignment.MiddleCenter;
            profileBox.Controls.Add(lblMetaSub);

            Label lblEmailDisplay = new Label();
            lblEmailDisplay.Text = loggedInStudentEmail;
            lblEmailDisplay.Font = new Font("Segoe UI", 8, FontStyle.Italic);
            lblEmailDisplay.ForeColor = Color.FromArgb(130, 140, 155);
            lblEmailDisplay.Location = new Point(10, 75);
            lblEmailDisplay.Size = new Size(200, 20);
            lblEmailDisplay.TextAlign = ContentAlignment.MiddleCenter;
            profileBox.Controls.Add(lblEmailDisplay);

            Button btnMenuExams = CreateSidebarButton("📝   Available Exams", 170);
            btnMenuExams.BackColor = Color.FromArgb(18, 95, 170);
            sidebarPanel.Controls.Add(btnMenuExams);

            Button btnMenuPerformance = CreateSidebarButton("📊   My Performance", 230);
            btnMenuPerformance.Click += (s, e) => {
                StudentResultForm resultForm = new StudentResultForm(loggedInStudentRoll);
                resultForm.Show();
            };
            sidebarPanel.Controls.Add(btnMenuPerformance);

            Button btnMenuLogout = CreateSidebarButton("🚪   Log Out", 550);
            btnMenuLogout.Click += (s, e) => {
                DialogResult res = MessageBox.Show("Are you sure you want to log out?", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    this.Close();
                    if (Application.OpenForms["LoginForm"] != null) Application.OpenForms["LoginForm"].Show();
                }
            };
            sidebarPanel.Controls.Add(btnMenuLogout);

            // ==========================================
            // MAIN HUB CONTENT DISPLAY PANEL
            // ==========================================
            mainContentPanel.Size = new Size(820, 680);
            mainContentPanel.Location = new Point(260, 0);
            mainContentPanel.BackColor = Color.FromArgb(244, 246, 249);
            this.Controls.Add(mainContentPanel);

            Label lblMainTitle = new Label();
            lblMainTitle.Text = "Your Examination Portal Dashboard";
            lblMainTitle.Font = new Font("Segoe UI", 20, FontStyle.Bold);
            lblMainTitle.ForeColor = Color.FromArgb(30, 41, 59);
            lblMainTitle.Location = new Point(35, 30);
            lblMainTitle.AutoSize = true;
            mainContentPanel.Controls.Add(lblMainTitle);

            Label lblSubtitle = new Label();
            lblSubtitle.Text = "Select any assigned active assessment card below to initiate your online automated exam session.";
            lblSubtitle.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            lblSubtitle.ForeColor = Color.FromArgb(100, 116, 139);
            lblSubtitle.Location = new Point(37, 70);
            lblSubtitle.AutoSize = true;
            mainContentPanel.Controls.Add(lblSubtitle);

            examsFlowPanel.Size = new Size(750, 480);
            examsFlowPanel.Location = new Point(35, 120);
            examsFlowPanel.AutoScroll = true;
            examsFlowPanel.BackColor = Color.Transparent;
            mainContentPanel.Controls.Add(examsFlowPanel);
        }

        private Button CreateSidebarButton(string text, int y)
        {
            Button btn = new Button();
            btn.Text = text;
            btn.Size = new Size(220, 45);
            btn.Location = new Point(20, y);
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.ForeColor = Color.White;
            btn.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            btn.TextAlign = ContentAlignment.MiddleLeft;
            btn.Padding = new Padding(15, 0, 0, 0);
            btn.Cursor = Cursors.Hand;
            return btn;
        }

        private void LoadLiveExamsIntoGrid()
        {
            try
            {
                examsFlowPanel.Controls.Clear();
                List<ExamModel> liveExamsList = examService.GetExams();

                if (liveExamsList == null || liveExamsList.Count == 0)
                {
                    Label lblNoExams = new Label();
                    lblNoExams.Text = "No exams are currently active or configured by management.";
                    lblNoExams.Font = new Font("Segoe UI", 12, FontStyle.Italic);
                    lblNoExams.ForeColor = Color.Gray;
                    lblNoExams.Size = new Size(500, 40);
                    examsFlowPanel.Controls.Add(lblNoExams);
                    return;
                }

                foreach (var exam in liveExamsList)
                {
                    Panel examCard = new Panel();
                    examCard.Size = new Size(710, 95);
                    examCard.BackColor = Color.White;
                    examCard.Margin = new Padding(0, 0, 0, 15);

                    Label lblTitle = new Label();
                    lblTitle.Text = exam.ExamName.ToUpper();
                    lblTitle.Font = new Font("Segoe UI", 13, FontStyle.Bold);
                    lblTitle.ForeColor = Color.FromArgb(18, 95, 170);
                    lblTitle.Location = new Point(20, 20);
                    lblTitle.AutoSize = true;
                    examCard.Controls.Add(lblTitle);

                    Label lblDetails = new Label();
                    lblDetails.Text = $"⏱ Duration: {exam.TimeDuration} Mins    |    ❓ Questions Count: {exam.TotalQuestions}";
                    lblDetails.Font = new Font("Segoe UI", 10, FontStyle.Regular);
                    lblDetails.ForeColor = Color.FromArgb(100, 116, 139);
                    lblDetails.Location = new Point(20, 52);
                    lblDetails.AutoSize = true;
                    examCard.Controls.Add(lblDetails);

                    Button btnLaunch = new Button();
                    btnLaunch.Text = "START EXAM";
                    btnLaunch.Size = new Size(160, 45);
                    btnLaunch.Location = new Point(520, 25);
                    btnLaunch.BackColor = Color.FromArgb(46, 139, 87);
                    btnLaunch.ForeColor = Color.White;
                    btnLaunch.FlatStyle = FlatStyle.Flat;
                    btnLaunch.FlatAppearance.BorderSize = 0;
                    btnLaunch.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                    btnLaunch.Cursor = Cursors.Hand;

                    // Current loop state ko capture karne ke liye variable create kiya
                    ExamModel currentExam = exam;
                    string examScopeName = exam.ExamName;

                    btnLaunch.Click += (sender, e) => {
                        DialogResult confirmation = MessageBox.Show($"Are you sure you want to begin the '{examScopeName}'? Your timer will initiate immediately.", "Confirm Execution", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (confirmation == DialogResult.Yes)
                        {
                            try
                            {
                                // FIX: Pehla argument string (examScopeName) ke bajaye 'currentExam' (ExamModel) pass kiya ha
                                ExamForm liveExamEngine = new ExamForm(currentExam, loggedInStudentName, loggedInStudentRoll, loggedInStudentEmail);

                                if (liveExamEngine != null && !liveExamEngine.IsDisposed)
                                {
                                    liveExamEngine.Show();
                                    this.Hide();
                                }
                            }
                            catch (Exception exInit)
                            {
                                MessageBox.Show("Could not launch this exam. Please ensure questions are added for this subject in Admin Panel.", "Launch Restricted", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                        }
                    };

                    examCard.Controls.Add(btnLaunch);
                    examsFlowPanel.Controls.Add(examCard);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to synchronize active database exams: " + ex.Message, "Fetch Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void StudentDashboard_Load(object sender, EventArgs e) { }

        private void btnMyPerformance_Click(object sender, EventArgs e)
        {
            try
            {
                string currentStudentEmail = this.loggedInStudentEmail;

                if (string.IsNullOrEmpty(currentStudentEmail))
                {
                    currentStudentEmail = "student@gmail.com";
                }

                StudentResultForm resultForm = new StudentResultForm(currentStudentEmail);
                resultForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error opening performance dashboard: " + ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}