namespace SmartExamSystem.Forms
{
    partial class AdminResultsForm
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
            this.dgvAdminResults = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAdminResults)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvAdminResults
            // 
            this.dgvAdminResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAdminResults.Location = new System.Drawing.Point(239, 199);
            this.dgvAdminResults.Name = "dgvAdminResults";
            this.dgvAdminResults.RowHeadersWidth = 51;
            this.dgvAdminResults.Size = new System.Drawing.Size(240, 150);
            this.dgvAdminResults.TabIndex = 0;
            // 
            // AdminResultsForm
            // 
            this.ClientSize = new System.Drawing.Size(726, 366);
            this.Controls.Add(this.dgvAdminResults);
            this.Name = "AdminResultsForm";
            ((System.ComponentModel.ISupportInitialize)(this.dgvAdminResults)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvAdminResults;
    }
}