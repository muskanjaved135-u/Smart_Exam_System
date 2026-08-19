namespace SmartExamSystem.Forms
{
    partial class QuestionManagement
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
            this.txtOptionC = new System.Windows.Forms.TextBox();
            this.txtOptionD = new System.Windows.Forms.TextBox();
            this.btnSaveQuestion = new System.Windows.Forms.Button();
            this.btnDone = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.cmbExams = new System.Windows.Forms.ComboBox();
            this.cmbCorrectOption = new System.Windows.Forms.ComboBox();
            this.txtOptionB = new System.Windows.Forms.TextBox();
            this.txtOptionA = new System.Windows.Forms.TextBox();
            this.txtQuestion = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtOptionC
            // 
            this.txtOptionC.Location = new System.Drawing.Point(401, 173);
            this.txtOptionC.Name = "txtOptionC";
            this.txtOptionC.Size = new System.Drawing.Size(100, 22);
            this.txtOptionC.TabIndex = 5;
            // 
            // txtOptionD
            // 
            this.txtOptionD.Location = new System.Drawing.Point(531, 176);
            this.txtOptionD.Name = "txtOptionD";
            this.txtOptionD.Size = new System.Drawing.Size(100, 22);
            this.txtOptionD.TabIndex = 6;
            // 
            // btnSaveQuestion
            // 
            this.btnSaveQuestion.Location = new System.Drawing.Point(28, 336);
            this.btnSaveQuestion.Name = "btnSaveQuestion";
            this.btnSaveQuestion.Size = new System.Drawing.Size(136, 23);
            this.btnSaveQuestion.TabIndex = 9;
            this.btnSaveQuestion.Text = "Save";
            this.btnSaveQuestion.UseVisualStyleBackColor = true;
            this.btnSaveQuestion.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnDone
            // 
            this.btnDone.Location = new System.Drawing.Point(217, 336);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(75, 23);
            this.btnDone.TabIndex = 10;
            this.btnDone.Text = "Done";
            this.btnDone.UseVisualStyleBackColor = true;
            this.btnDone.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(394, 327);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(107, 23);
            this.btnDelete.TabIndex = 11;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // cmbExams
            // 
            this.cmbExams.FormattingEnabled = true;
            this.cmbExams.Location = new System.Drawing.Point(394, 98);
            this.cmbExams.Name = "cmbExams";
            this.cmbExams.Size = new System.Drawing.Size(121, 24);
            this.cmbExams.TabIndex = 13;
            // 
            // cmbCorrectOption
            // 
            this.cmbCorrectOption.FormattingEnabled = true;
            this.cmbCorrectOption.Location = new System.Drawing.Point(401, 238);
            this.cmbCorrectOption.Name = "cmbCorrectOption";
            this.cmbCorrectOption.Size = new System.Drawing.Size(121, 24);
            this.cmbCorrectOption.TabIndex = 14;
            // 
            // txtOptionB
            // 
            this.txtOptionB.Location = new System.Drawing.Point(272, 173);
            this.txtOptionB.Name = "txtOptionB";
            this.txtOptionB.Size = new System.Drawing.Size(100, 22);
            this.txtOptionB.TabIndex = 4;
            // 
            // txtOptionA
            // 
            this.txtOptionA.Location = new System.Drawing.Point(119, 176);
            this.txtOptionA.Name = "txtOptionA";
            this.txtOptionA.Size = new System.Drawing.Size(100, 22);
            this.txtOptionA.TabIndex = 3;
            // 
            // txtQuestion
            // 
            this.txtQuestion.Location = new System.Drawing.Point(241, 98);
            this.txtQuestion.Name = "txtQuestion";
            this.txtQuestion.Size = new System.Drawing.Size(100, 22);
            this.txtQuestion.TabIndex = 15;
            // 
            // QuestionManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 572);
            this.Controls.Add(this.txtQuestion);
            this.Controls.Add(this.cmbCorrectOption);
            this.Controls.Add(this.cmbExams);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnDone);
            this.Controls.Add(this.btnSaveQuestion);
            this.Controls.Add(this.txtOptionD);
            this.Controls.Add(this.txtOptionC);
            this.Controls.Add(this.txtOptionB);
            this.Controls.Add(this.txtOptionA);
            this.Name = "QuestionManagement";
            this.Text = "QuestionManagement";
            this.Load += new System.EventHandler(this.QuestionManagement_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtOptionC;
        private System.Windows.Forms.TextBox txtOptionD;
        private System.Windows.Forms.Button btnSaveQuestion;
        private System.Windows.Forms.Button btnDone;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.ComboBox cmbExams;
        private System.Windows.Forms.ComboBox cmbCorrectOption;
        private System.Windows.Forms.TextBox txtOptionB;
        private System.Windows.Forms.TextBox txtOptionA;
        private System.Windows.Forms.TextBox txtQuestion;
    }
}