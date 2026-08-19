using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using SmartExamSystem.Models;
using SmartExamSystem.Services;
using ResultDataModel = SmartExamSystem.Models.ResultModel;

namespace SmartExamSystem.Forms
{  
    public partial class ExamForm : Form
    {

        private QuestionService questionService = new QuestionService();
        private ResultService resultService = new ResultService();
        private ExamService examService = new ExamService();

        private List<QuestionModel> examQuestions = new List<QuestionModel>();
        private int currentQuestionIndex = 0;
        private int totalSecondsLeft;
        private Dictionary<int, string> studentAnswers = new Dictionary<int, string>();

        private string currentExamName;
        private string studentName;
        private string studentRoll;
        private string studentEmail;

        // DYNAMIC CONTROLS (Unique names taake designer se clash na ho)
        private Label customQuestionLabel;
        private RadioButton customRbA;
        private RadioButton customRbB;
        private RadioButton customRbC;
        private RadioButton customRbD;
        private Button customBtnNext;
        private Button customBtnBack;
        private Label customTimerLabel;
        private Panel customContentPanel;

        // Timer Object Control Tracker
        private Timer liveExamTimer;

        public ExamForm(
    ExamModel exam,
    string name,
    string email,
    string roll)
        {
            InitializeComponent();

            currentExamName = exam.ExamName;

            studentName = name;
            studentEmail = email;
            studentRoll = roll;


            questionService = new QuestionService();
            resultService = new ResultService();
            examService = new ExamService();


            examQuestions = new List<QuestionModel>();

            studentAnswers = new Dictionary<int, string>();


            this.Load += ExamForm_Load;
        }

        private void ExamForm_Load(object sender, EventArgs e)
        {
            LoadExamQuestions();

            if (examQuestions == null || examQuestions.Count == 0)
            {
                MessageBox.Show($"This exam ('{currentExamName}') does not contain any questions yet!\nReturning to Dashboard.",
                                "Configuration Missing", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                this.BeginInvoke(new Action(() => {
                    this.Close();
                    if (Application.OpenForms["StudentDashboard"] != null)
                    {
                        Application.OpenForms["StudentDashboard"].Show();
                    }
                }));
                return;
            }

            BuildCustomLayout();
            StartExamCountdown();
            DisplayCurrentQuestion();
        }

        private void LoadExamQuestions()
        {
            try
            {
                if (questionService == null) questionService = new QuestionService();
                var allQuestions = questionService.GetQuestions();

                if (allQuestions != null)
                {
                    examQuestions = allQuestions.FindAll(q => q.ExamName != null && q.ExamName.Equals(currentExamName, StringComparison.OrdinalIgnoreCase));
                }
                else
                {
                    examQuestions = new List<QuestionModel>();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Sync Error: " + ex.Message, "Fetch Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                examQuestions = new List<QuestionModel>();
            }
        }

        private void BuildCustomLayout()
        {
            this.Text = $"Live Examination Portal - {currentExamName}";
            this.BackColor = Color.FromArgb(248, 250, 252);
            this.Size = new Size(900, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Controls.Clear(); // Purane saare faulty designer controls screen se ghayab

            // Header Top Bar
            Panel headerPanel = new Panel();
            headerPanel.Size = new Size(900, 70);
            headerPanel.Dock = DockStyle.Top;
            headerPanel.BackColor = Color.FromArgb(15, 23, 42);
            this.Controls.Add(headerPanel);

            Label lblTitle = new Label();
            lblTitle.Text = $"Subject: {currentExamName.ToUpper()}";
            lblTitle.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(25, 20);
            lblTitle.AutoSize = true;
            headerPanel.Controls.Add(lblTitle);

            customTimerLabel = new Label();
            customTimerLabel.Text = "Time Left: 00:00";
            customTimerLabel.Font = new Font("Segoe UI", 13, FontStyle.Bold);
            customTimerLabel.ForeColor = Color.FromArgb(244, 63, 94);
            customTimerLabel.Location = new Point(700, 20);
            customTimerLabel.AutoSize = true;
            headerPanel.Controls.Add(customTimerLabel);

            // Center Board Panel
            customContentPanel = new Panel();
            customContentPanel.Location = new Point(40, 90);
            customContentPanel.Size = new Size(800, 440);
            customContentPanel.BackColor = Color.White;
            customContentPanel.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(customContentPanel);

            customQuestionLabel = new Label();
            customQuestionLabel.Location = new Point(30, 30);
            customQuestionLabel.Size = new Size(740, 80);
            customQuestionLabel.Font = new Font("Segoe UI", 13, FontStyle.Bold);
            customQuestionLabel.ForeColor = Color.FromArgb(30, 41, 59);
            customContentPanel.Controls.Add(customQuestionLabel);

            // Radio Options Layout Setup
            customRbA = new RadioButton() { Location = new Point(40, 130), Size = new Size(700, 40), Font = new Font("Segoe UI", 11, FontStyle.Regular) };
            customRbB = new RadioButton() { Location = new Point(40, 180), Size = new Size(700, 40), Font = new Font("Segoe UI", 11, FontStyle.Regular) };
            customRbC = new RadioButton() { Location = new Point(40, 230), Size = new Size(700, 40), Font = new Font("Segoe UI", 11, FontStyle.Regular) };
            customRbD = new RadioButton() { Location = new Point(40, 280), Size = new Size(700, 40), Font = new Font("Segoe UI", 11, FontStyle.Regular) };

            customContentPanel.Controls.Add(customRbA);
            customContentPanel.Controls.Add(customRbB);
            customContentPanel.Controls.Add(customRbC);
            customContentPanel.Controls.Add(customRbD);

            Label dividerLine = new Label();
            dividerLine.BorderStyle = BorderStyle.Fixed3D;
            dividerLine.Location = new Point(30, 350);
            dividerLine.Size = new Size(740, 2);
            customContentPanel.Controls.Add(dividerLine);

            // Navigation Back Button Action mapping
            customBtnBack = new Button();
            customBtnBack.Text = "🡨 BACK";
            customBtnBack.Location = new Point(30, 370);
            customBtnBack.Size = new Size(130, 45);
            customBtnBack.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            customBtnBack.ForeColor = Color.White;
            customBtnBack.BackColor = Color.FromArgb(100, 116, 139);
            customBtnBack.FlatStyle = FlatStyle.Flat;
            customBtnBack.Click += CustomBtnBack_Click;
            customContentPanel.Controls.Add(customBtnBack);

            // Navigation Next / Final Submit Button Action mapping
            customBtnNext = new Button();
            customBtnNext.Text = "NEXT QUESTION ➔";
            customBtnNext.Location = new Point(640, 370);
            customBtnNext.Size = new Size(130, 45);
            customBtnNext.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            customBtnNext.ForeColor = Color.White;
            customBtnNext.BackColor = Color.FromArgb(37, 99, 235);
            customBtnNext.FlatStyle = FlatStyle.Flat;
            customBtnNext.Click += CustomBtnNext_Click;
            customContentPanel.Controls.Add(customBtnNext);
        }

        private void StartExamCountdown()
        {
            int durationMins = 15;
            try
            {
                if (examService != null)
                {
                    var examConfig = examService.GetExams().Find(e => e.ExamName.Equals(currentExamName, StringComparison.OrdinalIgnoreCase));
                    if (examConfig != null && examConfig.TimeDuration > 0)
                    {
                        durationMins = examConfig.TimeDuration;
                    }
                }
            }
            catch { }

            totalSecondsLeft = durationMins * 60;

            liveExamTimer = new Timer();
            liveExamTimer.Interval = 1000;
            liveExamTimer.Tick += LiveExamTimer_Tick;
            liveExamTimer.Start();
        }

        private void LiveExamTimer_Tick(object sender, EventArgs e)
        {
            if (totalSecondsLeft > 0)
            {
                totalSecondsLeft--;
                TimeSpan span = TimeSpan.FromSeconds(totalSecondsLeft);
                if (customTimerLabel != null)
                {
                    customTimerLabel.Text = $"Time Left: {span.Minutes:D2}:{span.Seconds:D2}";
                }
            }
            else
            {
                if (liveExamTimer != null) liveExamTimer.Stop();
                MessageBox.Show("Time is finished! Automatically saving your current progress.", "Timeout Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                SaveExamResultsToMongoDB();
            }
        }

        private void DisplayCurrentQuestion()
        {
            if (examQuestions != null && currentQuestionIndex < examQuestions.Count)
            {
                var q = examQuestions[currentQuestionIndex];

                customQuestionLabel.Text = $"Question {currentQuestionIndex + 1} of {examQuestions.Count}:\n\n{q.QuestionText}";
                customRbA.Text = "A) " + q.OptionA;
                customRbB.Text = "B) " + q.OptionB;
                customRbC.Text = "C) " + q.OptionC;
                customRbD.Text = "D) " + q.OptionD;

                ClearRadioMarkers();
                if (studentAnswers.ContainsKey(currentQuestionIndex))
                {
                    string state = studentAnswers[currentQuestionIndex];
                    if (state == "A") customRbA.Checked = true;
                    if (state == "B") customRbB.Checked = true;
                    if (state == "C") customRbC.Checked = true;
                    if (state == "D") customRbD.Checked = true;
                }

                customBtnBack.Enabled = (currentQuestionIndex > 0);

                if (currentQuestionIndex == examQuestions.Count - 1)
                {
                    customBtnNext.Text = "SUBMIT EXAM 🎉";
                    customBtnNext.BackColor = Color.FromArgb(22, 163, 74); // Vibrant Success Green
                }
                else
                {
                    customBtnNext.Text = "NEXT QUESTION ➔";
                    customBtnNext.BackColor = Color.FromArgb(37, 99, 235);
                }
            }
        }

        private void CacheUserSelection()
        {
            if (customRbA.Checked) studentAnswers[currentQuestionIndex] = "A";
            else if (customRbB.Checked) studentAnswers[currentQuestionIndex] = "B";
            else if (customRbC.Checked) studentAnswers[currentQuestionIndex] = "C";
            else if (customRbD.Checked) studentAnswers[currentQuestionIndex] = "D";
        }

        private void CustomBtnNext_Click(object sender, EventArgs e)
        {
            CacheUserSelection();

            if (currentQuestionIndex == examQuestions.Count - 1)
            {
                // CRITICAL VALIDATION CHECK: Agar koi question skip kiya ho
                if (studentAnswers.Count < examQuestions.Count)
                {
                    MessageBox.Show("Please solve all questions before submitting! You have missed some questions.",
                                    "Incomplete Evaluation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // REQUIREMENT FIX 1: Student ne submit dabba diya -> TIMER FORAN ROKO!
                if (liveExamTimer != null)
                {
                    liveExamTimer.Stop();
                }

                // REQUIREMENT FIX 2: Data save karwao aur result screen pe jao
                SaveExamResultsToMongoDB();
            }
            else
            {
                currentQuestionIndex++;
                DisplayCurrentQuestion();
            }
        }
       
        private void CustomBtnBack_Click(object sender, EventArgs e)
        {
            CacheUserSelection();
            if (currentQuestionIndex > 0)
            {
                currentQuestionIndex--;
                DisplayCurrentQuestion();
            }
        }

        private void SaveExamResultsToMongoDB()
        {
            try
            {
                int correctCount = 0;
                for (int i = 0; i < examQuestions.Count; i++)
                {
                    if (studentAnswers.ContainsKey(i))
                    {
                        if (studentAnswers[i].Equals(examQuestions[i].CorrectAnswer, StringComparison.OrdinalIgnoreCase))
                        {
                            correctCount++;
                        }
                    }
                }

                double calculatedPercentage = examQuestions.Count > 0 ? ((double)correctCount / examQuestions.Count) * 100 : 0;
                int marksSecured = correctCount * 2;

                // Creating the full safe model wrapper package mapping parameters precisely
        


                // DATA INSERTION CALL TO MONGODB
                SmartExamSystem.Models.ResultModel reportInstance =
 new SmartExamSystem.Models.ResultModel
 {
     ExamName = currentExamName ?? "General Exam",

     StudentName = studentName ?? "Unknown Student",

     StudentEmail = studentEmail ?? "unknown@gmail.com",

     StudentRoll = studentRoll ?? "N/A",

     TotalQuestions = examQuestions.Count,

     CorrectAnswers = correctCount,

     Marks = marksSecured,

     Percentage = Math.Round(calculatedPercentage, 2),

     Date = DateTime.Now
 };


                if (resultService == null)
                {
                    resultService = new ResultService();
                }


                resultService.AddResult(reportInstance);


                // Elegant professional popup result visualization report layout block
                string popupTitle = calculatedPercentage >= 50 ? "🎉 Examination Passed!" : "⚠️ Assessment Failed";
                string summaryReport = $"====================================\n" +
                                       $"          SMART EXAM SYSTEM REPORT         \n" +
                                       $"====================================\n\n" +
                                       $"Student Name : {this.studentName}\n" +
                                       $"Roll Number  : {this.studentRoll}\n" +
                                       $"Exam Subject : {this.currentExamName}\n\n" +
                                       $"------------------------------------\n" +
                                       $"Total Questions : {examQuestions.Count}\n" +
                                       $"Correct Answers : {correctCount}\n" +
                                       $"Marks Obtained  : {marksSecured}\n" +
                                       $"Final Percentage: {Math.Round(calculatedPercentage, 2)}%\n" +
                                       $"------------------------------------\n\n";

                if (calculatedPercentage >= 50)
                {
                    summaryReport += "CONGRATULATIONS! Excellent work! You have successfully cleared the criteria benchmark. Keep shining! 👍✨";
                    MessageBox.Show(summaryReport, popupTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    summaryReport += "Hard Luck! You couldn't secure passing criteria this time. Revise your weak chapters and try again! 📚💪";
                    MessageBox.Show(summaryReport, popupTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("MongoDB synchronization runtime error log sequence: " + ex.Message, "Write Fail Trace", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Close();
                if (Application.OpenForms["StudentDashboard"] != null)
                {
                    Application.OpenForms["StudentDashboard"].Show();
                }
            }
        }

        private void ClearRadioMarkers()
        {
            customRbA.Checked = false;
            customRbB.Checked = false;
            customRbC.Checked = false;
            customRbD.Checked = false;
        }
    }
}