namespace SmartExamSystem.Forms
{
    partial class ExamForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnBackQuestion = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnBackQuestion
            // 
            this.btnBackQuestion.Location = new System.Drawing.Point(134, 120);
            this.btnBackQuestion.Name = "btnBackQuestion";
            this.btnBackQuestion.Size = new System.Drawing.Size(75, 23);
            this.btnBackQuestion.TabIndex = 0;
            this.btnBackQuestion.Text = "button1";
            this.btnBackQuestion.UseVisualStyleBackColor = true;
            // 
            // ExamForm
            // 
            this.ClientSize = new System.Drawing.Size(282, 253);
            this.Controls.Add(this.btnBackQuestion);
            this.Name = "ExamForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblQuestion;
        private System.Windows.Forms.RadioButton rbOptionA;
        private System.Windows.Forms.RadioButton rbOptionB;
        private System.Windows.Forms.RadioButton rbOptionC;
        private System.Windows.Forms.RadioButton rbOptionD;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Timer examTimer;
        private System.Windows.Forms.Label lblTimer;
        private System.Windows.Forms.Button btnNextQuestion;
        private System.Windows.Forms.Label lblQuestionNumber;
        private System.Windows.Forms.Label lblExamTitle;
        private System.Windows.Forms.Label lblQuestionText;
        private System.Windows.Forms.Label lblStudentMeta;
        private System.Windows.Forms.Label lblTimerDisplay;
        private System.Windows.Forms.RadioButton rbtnA;
        private System.Windows.Forms.RadioButton rbtnB;
        private System.Windows.Forms.RadioButton rbtnC;
        private System.Windows.Forms.RadioButton rbtnD;
        private System.Windows.Forms.Button btnBackQuestion;
    }
}