namespace SmartExamSystem.Forms
{
    partial class ExamManagement
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
            this.txtExamName = new System.Windows.Forms.TextBox();
            this.txtDuration = new System.Windows.Forms.TextBox();
            this.btnSaveExam = new System.Windows.Forms.Button();
            this.txtTotalQuestions = new System.Windows.Forms.TextBox();
            this.dgvExams = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExams)).BeginInit();
            this.SuspendLayout();
            // 
            // txtExamName
            // 
            this.txtExamName.Location = new System.Drawing.Point(400, 160);
            this.txtExamName.Name = "txtExamName";
            this.txtExamName.Size = new System.Drawing.Size(100, 22);
            this.txtExamName.TabIndex = 2;
            // 
            // txtDuration
            // 
            this.txtDuration.Location = new System.Drawing.Point(400, 256);
            this.txtDuration.Name = "txtDuration";
            this.txtDuration.Size = new System.Drawing.Size(100, 22);
            this.txtDuration.TabIndex = 3;
            // 
            // btnSaveExam
            // 
            this.btnSaveExam.Location = new System.Drawing.Point(210, 81);
            this.btnSaveExam.Name = "btnSaveExam";
            this.btnSaveExam.Size = new System.Drawing.Size(157, 23);
            this.btnSaveExam.TabIndex = 6;
            this.btnSaveExam.Text = "CREATE EXAM";
            this.btnSaveExam.UseVisualStyleBackColor = true;
            this.btnSaveExam.Click += new System.EventHandler(this.btnSaveExam_Click);
            // 
            // txtTotalQuestions
            // 
            this.txtTotalQuestions.Location = new System.Drawing.Point(572, 255);
            this.txtTotalQuestions.Name = "txtTotalQuestions";
            this.txtTotalQuestions.Size = new System.Drawing.Size(100, 22);
            this.txtTotalQuestions.TabIndex = 7;
            // 
            // dgvExams
            // 
            this.dgvExams.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvExams.Location = new System.Drawing.Point(307, 385);
            this.dgvExams.Name = "dgvExams";
            this.dgvExams.RowHeadersWidth = 51;
            this.dgvExams.RowTemplate.Height = 24;
            this.dgvExams.Size = new System.Drawing.Size(420, 53);
            this.dgvExams.TabIndex = 8;
            // 
            // ExamManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dgvExams);
            this.Controls.Add(this.txtTotalQuestions);
            this.Controls.Add(this.btnSaveExam);
            this.Controls.Add(this.txtDuration);
            this.Controls.Add(this.txtExamName);
            this.Name = "ExamManagement";
            this.Text = "ExamManagement";
            ((System.ComponentModel.ISupportInitialize)(this.dgvExams)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtExamName;
        private System.Windows.Forms.TextBox txtDuration;
        private System.Windows.Forms.Button btnSaveExam;
        private System.Windows.Forms.TextBox txtTotalQuestions;
        private System.Windows.Forms.DataGridView dgvExams;
    }
}