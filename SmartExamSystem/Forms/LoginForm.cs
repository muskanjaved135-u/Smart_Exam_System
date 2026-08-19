using MongoDB.Driver;
using SmartExamSystem.Database;
using SmartExamSystem.Forms;
using SmartExamSystem.Helpers;
using SmartExamSystem.Models;
using SmartExamSystem.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace SmartExamSystem
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            CreateProfessionalUI();
            BindEventsExplicitly();
        }

        private void BindEventsExplicitly()
        {
            btnLogin.Click -= btnLogin_Click;
            btnLogin.Click += new EventHandler(btnLogin_Click);

            btnRegister.Click -= btnRegister_Click;
            btnRegister.Click += new EventHandler(btnRegister_Click);

            if (btnTestConnection != null)
            {
                btnTestConnection.Click -= btnTestConnection_Click;
                btnTestConnection.Click += new EventHandler(btnTestConnection_Click);
            }
        }

        private void btnTestConnection_Click(object sender, EventArgs e)
        {
            try
            {
                var database = MongoDBConnection.GetDatabase();
                MessageBox.Show("MongoDB Connected Successfully", "Connection Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Database Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CreateProfessionalUI()
        {
            this.Text = "Smart Exam System";
            this.Size = new Size(950, 550);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // ============================
            // LEFT BRAND PANEL
            // ============================
            Panel leftPanel = new Panel();
            leftPanel.Size = new Size(400, 550);
            leftPanel.Location = new Point(0, 0);
            leftPanel.BackColor = Color.FromArgb(18, 95, 170);
            this.Controls.Add(leftPanel);

            // LOGO
            Label logo = new Label();
            logo.Text = "☁";
            logo.Font = new Font("Segoe UI", 75, FontStyle.Bold);
            logo.ForeColor = Color.White;
            logo.AutoSize = true;
            logo.Location = new Point(80, 50);
            leftPanel.Controls.Add(logo);

            // SYSTEM NAME
            Label appName = new Label();
            appName.Text = "SMART\nEXAM\nSYSTEM";
            appName.Font = new Font("Segoe UI", 30, FontStyle.Bold);
            appName.ForeColor = Color.White;
            appName.AutoSize = true;
            appName.Location = new Point(70, 180);
            leftPanel.Controls.Add(appName);

            // DESCRIPTION
            Label detail = new Label();
            detail.Text = "Cloud Based\nOnline Examination\nPlatform";
            detail.Font = new Font("Segoe UI", 14);
            detail.ForeColor = Color.White;
            detail.AutoSize = true;
            detail.Location = new Point(90, 360);
            leftPanel.Controls.Add(detail);

            Label footer = new Label();
            footer.Text = "Secure  •  Fast  •  Reliable";
            footer.Font = new Font("Segoe UI", 11);
            footer.ForeColor = Color.White;
            footer.AutoSize = true;
            footer.Location = new Point(100, 480);
            leftPanel.Controls.Add(footer);

            // ============================
            // RIGHT LOGIN AREA
            // ============================
            Label welcome = new Label();
            welcome.Text = "Welcome Back";
            welcome.Font = new Font("Segoe UI", 26, FontStyle.Bold);
            welcome.ForeColor = Color.FromArgb(30, 30, 30);
            welcome.AutoSize = true;
            welcome.Location = new Point(560, 70);
            this.Controls.Add(welcome);

            Label emailLabel = new Label();
            emailLabel.Text = "Email Address";
            emailLabel.Font = new Font("Segoe UI", 11);
            emailLabel.Location = new Point(560, 160);
            emailLabel.AutoSize = true;
            this.Controls.Add(emailLabel);

            txtEmail.Location = new Point(560, 190);
            txtEmail.Size = new Size(300, 40);
            txtEmail.Font = new Font("Segoe UI", 12);

            Label passwordLabel = new Label();
            passwordLabel.Text = "Password";
            passwordLabel.Font = new Font("Segoe UI", 11);
            passwordLabel.Location = new Point(560, 250);
            passwordLabel.AutoSize = true;
            this.Controls.Add(passwordLabel);

            txtPassword.Location = new Point(560, 280);
            txtPassword.Size = new Size(300, 40);
            txtPassword.Font = new Font("Segoe UI", 12);
            txtPassword.PasswordChar = '●';
            txtPassword.UseSystemPasswordChar = false;

            // 🔥 FIX: Login Password Show/Hide Toggle CheckBox (Explicit reset)
            CheckBox chkLoginShowPwd = new CheckBox();
            chkLoginShowPwd.Text = "Show";
            chkLoginShowPwd.Font = new Font("Segoe UI", 9);
            chkLoginShowPwd.Location = new Point(870, 288);
            chkLoginShowPwd.AutoSize = true;
            chkLoginShowPwd.Cursor = Cursors.Hand;
            chkLoginShowPwd.CheckedChanged += (s, ev) => {
                if (chkLoginShowPwd.Checked)
                {
                    txtPassword.UseSystemPasswordChar = false;
                    txtPassword.PasswordChar = '\0'; // Show text
                }
                else
                {
                    txtPassword.PasswordChar = '●';  // Hide text
                }
            };
            this.Controls.Add(chkLoginShowPwd);

            // LOGIN BUTTON
            btnLogin.Text = "LOGIN";
            btnLogin.Location = new Point(560, 350);
            btnLogin.Size = new Size(300, 45);
            StyleModernButton(btnLogin);

            // REGISTER BUTTON
            btnRegister.Text = "CREATE ACCOUNT";
            btnRegister.Location = new Point(560, 420);
            btnRegister.Size = new Size(300, 45);
            btnRegister.BackColor = Color.White;
            btnRegister.ForeColor = Color.FromArgb(18, 95, 170);
            btnRegister.FlatStyle = FlatStyle.Flat;
            btnRegister.Font = new Font("Segoe UI", 11, FontStyle.Bold);
        }

        private void StyleModernButton(Button btn)
        {
            btn.BackColor = Color.FromArgb(18, 95, 170);
            btn.ForeColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            btn.Cursor = Cursors.Hand;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both Email and Password.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            UserService service = new UserService();
            UserModel user = service.LoginUser(email, password);

            if (user != null)
            {
                MessageBox.Show("Login Successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (user.Role == "Student")
                {
                    string validRollNumber = "NIMS-2026-ST";
                    StudentDashboard dashboard = new StudentDashboard(user.Name, validRollNumber, user.Email);
                    dashboard.Show();
                    this.Hide();
                }
                else if (user.Role == "Admin")
                {
                    AdminDashboard admin = new AdminDashboard();
                    admin.Show();
                    this.Hide();
                }
            }
            else
            {
                MessageBox.Show("Invalid Email or Password", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            RegisterForm register = new RegisterForm();
            register.Show();
            this.Hide();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
        }
    }
}