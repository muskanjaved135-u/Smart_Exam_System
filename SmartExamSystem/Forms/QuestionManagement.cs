using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using SmartExamSystem.Models;
using SmartExamSystem.Services;

namespace SmartExamSystem.Forms
{
    public partial class QuestionManagement : Form
    {
        ExamService examService = new ExamService();
        QuestionService questionService = new QuestionService();

        private string targetExamName = "";
        private int totalQuestionsAllowed = 0;
        private int currentQuestionCount = 0;

        // Constructor 1: Jab Manage Exam se direct khule
        public QuestionManagement(string examName, int totalQuestions)
        {
            InitializeComponent();

            this.targetExamName = examName;
            this.totalQuestionsAllowed = totalQuestions;

            DesignQuestionUI();
            BindEventsExplicitly(); // Force-binding events via code
            LoadExamsInDropdown();
            SetupCorrectOptionsDropdown();

            cmbExams.SelectedItem = targetExamName;
            cmbExams.Enabled = false;

            CheckExistingQuestionCount();
        }

        // Constructor 2: Safety fallback (agar direct khule)
        public QuestionManagement()
        {
            InitializeComponent();
            DesignQuestionUI();
            BindEventsExplicitly(); // Force-binding events via code
            LoadExamsInDropdown();
            SetupCorrectOptionsDropdown();
        }

        // CODES SE EVENTS BIND KARNA (Taake click lazmi kaam kare)
        private void BindEventsExplicitly()
        {
            // Designer ke purane links tor kar direct code se connect kar rahe hain
            btnSaveQuestion.Click -= btnSaveQuestion_Click;
            btnSaveQuestion.Click += new EventHandler(btnSaveQuestion_Click);

            btnDone.Click -= btnDone_Click;
            btnDone.Click += new EventHandler(btnDone_Click);
        }

        private void DesignQuestionUI()
        {
            this.Text = "Question Management - Smart Exam System";
            this.Size = new Size(1000, 680);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.BackColor = Color.FromArgb(245, 247, 250);

            // TITLE HEADER
            Label title = new Label();
            title.Text = string.IsNullOrEmpty(targetExamName) ? "MANAGE EXAM QUESTIONS" : $"ADD QUESTIONS FOR: {targetExamName.ToUpper()}";
            title.Font = new Font("Segoe UI", 20, FontStyle.Bold);
            title.ForeColor = Color.FromArgb(18, 95, 170);
            title.Location = new Point(250, 25);
            title.AutoSize = true;
            this.Controls.Add(title);

            // LEFT COLUMN
            AddLabel("Target Exam", 60, 100);
            cmbExams.Location = new Point(60, 125);
            cmbExams.Size = new Size(400, 35);
            cmbExams.Font = new Font("Segoe UI", 11);
            cmbExams.DropDownStyle = ComboBoxStyle.DropDownList;

            AddLabel("Question Text / Statement", 60, 185);
            txtQuestion.Multiline = true;
            txtQuestion.ScrollBars = ScrollBars.Vertical;
            txtQuestion.Location = new Point(60, 210);
            txtQuestion.Size = new Size(400, 120);
            txtQuestion.Font = new Font("Segoe UI", 11);

            AddLabel("Correct Answer Key", 60, 355);
            cmbCorrectOption.Location = new Point(60, 380);
            cmbCorrectOption.Size = new Size(400, 35);
            cmbCorrectOption.Font = new Font("Segoe UI", 11);
            cmbCorrectOption.DropDownStyle = ComboBoxStyle.DropDownList;

            // RIGHT COLUMN
            AddLabel("Option A", 520, 100);
            txtOptionA.Location = new Point(520, 125);
            txtOptionA.Size = new Size(400, 35);
            txtOptionA.Font = new Font("Segoe UI", 11);

            AddLabel("Option B", 520, 185);
            txtOptionB.Location = new Point(520, 210);
            txtOptionB.Size = new Size(400, 35);
            txtOptionB.Font = new Font("Segoe UI", 11);

            AddLabel("Option C", 520, 270);
            txtOptionC.Location = new Point(520, 295);
            txtOptionC.Size = new Size(400, 35);
            txtOptionC.Font = new Font("Segoe UI", 11);

            AddLabel("Option D", 520, 355);
            txtOptionD.Location = new Point(520, 380);
            txtOptionD.Size = new Size(400, 35);
            txtOptionD.Font = new Font("Segoe UI", 11);

            // SAVE ACTION BUTTON
            btnSaveQuestion.Text = "SAVE QUESTION TO CLOUD";
            btnSaveQuestion.Location = new Point(180, 500);
            btnSaveQuestion.Size = new Size(300, 50);
            StyleButton(btnSaveQuestion, Color.FromArgb(18, 95, 170));

            // DONE BUTTON
            btnDone.Text = "DONE / FINISH";
            btnDone.Location = new Point(520, 500);
            btnDone.Size = new Size(300, 50);
            StyleButton(btnDone, Color.FromArgb(46, 139, 87));
        }

        private void AddLabel(string text, int x, int y)
        {
            Label lbl = new Label();
            lbl.Text = text;
            lbl.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lbl.ForeColor = Color.FromArgb(70, 70, 70);
            lbl.AutoSize = true;
            lbl.Location = new Point(x, y);
            this.Controls.Add(lbl);
        }

        private void StyleButton(Button btn, Color backColor)
        {
            btn.BackColor = backColor;
            btn.ForeColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            btn.Cursor = Cursors.Hand;
        }

        private void SetupCorrectOptionsDropdown()
        {
            cmbCorrectOption.Items.Clear();
            cmbCorrectOption.Items.Add("A");
            cmbCorrectOption.Items.Add("B");
            cmbCorrectOption.Items.Add("C");
            cmbCorrectOption.Items.Add("D");
            if (cmbCorrectOption.Items.Count > 0) cmbCorrectOption.SelectedIndex = 0;
        }

        private void LoadExamsInDropdown()
        {
            try
            {
                List<ExamModel> examsList = examService.GetExams();
                cmbExams.Items.Clear();
                foreach (var exam in examsList)
                {
                    cmbExams.Items.Add(exam.ExamName);
                }
            }
            catch (Exception ex) { MessageBox.Show("Error loading exams: " + ex.Message); }
        }

        private void CheckExistingQuestionCount()
        {
            if (string.IsNullOrEmpty(targetExamName)) return;
            try
            {
                long count = questionService.GetQuestionCountForExam(targetExamName);
                currentQuestionCount = (int)count;

                if (currentQuestionCount >= totalQuestionsAllowed && totalQuestionsAllowed > 0)
                {
                    MessageBox.Show("This exam already has all required questions configuration!", "Complete");
                    btnSaveQuestion.Enabled = false;
                }
            }
            catch { currentQuestionCount = 0; }
        }

        // SAVE BUTTON CLICK
        private void btnSaveQuestion_Click(object sender, EventArgs e)
        {
            // Agar bina exam create kiye direct direct khul jaye handle karne k liye
            if (cmbExams.SelectedIndex == -1 && string.IsNullOrEmpty(targetExamName))
            {
                MessageBox.Show("Please select an Exam from dropdown first!", "Validation Error");
                return;
            }

            string targetExam = string.IsNullOrEmpty(targetExamName) ? cmbExams.SelectedItem.ToString() : targetExamName;

            // Target limits verify krna dynamic dropdown selection se bhi
            if (totalQuestionsAllowed == 0)
            {
                List<ExamModel> examsList = examService.GetExams();
                foreach (var ex in examsList)
                {
                    if (ex.ExamName == targetExam)
                    {
                        totalQuestionsAllowed = ex.TotalQuestions;
                        break;
                    }
                }
            }

            if (currentQuestionCount >= totalQuestionsAllowed && totalQuestionsAllowed > 0)
            {
                MessageBox.Show($"Limit reached! This exam only allows {totalQuestionsAllowed} questions.", "Warning");
                return;
            }

            if (string.IsNullOrEmpty(txtQuestion.Text) || string.IsNullOrEmpty(txtOptionA.Text) ||
                string.IsNullOrEmpty(txtOptionB.Text) || string.IsNullOrEmpty(txtOptionC.Text) || string.IsNullOrEmpty(txtOptionD.Text))
            {
                MessageBox.Show("Please fill all options and the question text box before saving!", "Incomplete Fields");
                return;
            }

            try
            {
                QuestionModel q = new QuestionModel
                {
                    ExamName = targetExam,
                    QuestionText = txtQuestion.Text,
                    OptionA = txtOptionA.Text,
                    OptionB = txtOptionB.Text,
                    OptionC = txtOptionC.Text,
                    OptionD = txtOptionD.Text,
                    CorrectAnswer = cmbCorrectOption.SelectedItem.ToString()
                };

                // MongoDB Cloud insertion
                questionService.AddQuestion(q);
                currentQuestionCount++;

                if (currentQuestionCount == totalQuestionsAllowed)
                {
                    MessageBox.Show("Paper Completed Successfully! All questions saved.", "Exam Configured Successfully");
                    btnSaveQuestion.Enabled = false;
                    this.Close();
                }
                else
                {
                    MessageBox.Show($"Question {currentQuestionCount} of {totalQuestionsAllowed} Saved to MongoDB Cloud!", "Success");

                    // CLEAR TEXTBOXES IMMEDIATELY FOR NEXT QUESTION
                    txtQuestion.Text = "";
                    txtOptionA.Text = "";
                    txtOptionB.Text = "";
                    txtOptionC.Text = "";
                    txtOptionD.Text = "";
                    cmbCorrectOption.SelectedIndex = 0;
                    txtQuestion.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to save data to cloud: " + ex.Message, "Database Error");
            }
        }

        // DONE BUTTON CLICK
        private void btnDone_Click(object sender, EventArgs e)
        {
            // Validation guard logic
            if (totalQuestionsAllowed == 0 || (currentQuestionCount == 0 && totalQuestionsAllowed == 0))
            {
                MessageBox.Show("No active exam context found. Please create or configure an exam properly first!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (currentQuestionCount < totalQuestionsAllowed)
            {
                int remaining = totalQuestionsAllowed - currentQuestionCount;
                MessageBox.Show($"Incomplete Paper! Please complete the remaining {remaining} questions before finishing.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                MessageBox.Show("Paper Completed Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        // DESIGNER STUBS
        private void QuestionManagement_Load(object sender, EventArgs e) { }
        private void dgvQuestions_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void btnAdd_Click(object sender, EventArgs e) { }
        private void btnUpdate_Click(object sender, EventArgs e) { }
        private void btnDelete_Click(object sender, EventArgs e) { }
    }
}