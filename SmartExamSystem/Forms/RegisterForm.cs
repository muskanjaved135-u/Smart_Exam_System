using System;
using System.Drawing;
using System.Windows.Forms;
using SmartExamSystem.Models;
using SmartExamSystem.Services;
using MongoDB.Driver;

namespace SmartExamSystem.Forms
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();

            DesignRegisterUI();

            cmbRole.Items.Clear();
            cmbRole.Items.Add("Student");
            cmbRole.Items.Add("Teacher");
            cmbRole.Items.Add("Admin");

            cmbRole.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void DesignRegisterUI()
        {
            this.Text = "Smart Exam System - Register";
            this.Size = new Size(650, 780);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.BackColor = Color.FromArgb(245, 247, 250);

            // TITLE
            Label title = new Label();
            title.Text = "CREATE ACCOUNT";
            title.Font = new Font("Segoe UI", 28, FontStyle.Bold);
            title.ForeColor = Color.FromArgb(18, 95, 170);
            title.AutoSize = true;
            title.Location = new Point(150, 40);
            this.Controls.Add(title);

            Label sub = new Label();
            sub.Text = "Register for Online Examination";
            sub.Font = new Font("Segoe UI", 12);
            sub.AutoSize = true;
            sub.Location = new Point(190, 90);
            this.Controls.Add(sub);

            // NAME
            AddLabel("Full Name", 150, 140);
            txtName.Location = new Point(150, 165);
            txtName.Size = new Size(330, 40);

            // EMAIL
            AddLabel("Email Address", 150, 220);
            txtEmail.Location = new Point(150, 245);
            txtEmail.Size = new Size(330, 40);

            // PASSWORD
            AddLabel("Password", 150, 300);
            txtPassword.Location = new Point(150, 325);
            txtPassword.Size = new Size(330, 40);
            txtPassword.PasswordChar = '●';
            txtPassword.UseSystemPasswordChar = false; // System default overrides ko rokne ke liye

            // 🔥 FIX: Register Password Show/Hide Toggle CheckBox
            CheckBox chkShowPwd = new CheckBox();
            chkShowPwd.Text = "Show";
            chkShowPwd.Font = new Font("Segoe UI", 9);
            chkShowPwd.Location = new Point(490, 332);
            chkShowPwd.AutoSize = true;
            chkShowPwd.Cursor = Cursors.Hand;
            chkShowPwd.CheckedChanged += (s, ev) => {
                if (chkShowPwd.Checked)
                {
                    txtPassword.UseSystemPasswordChar = false;
                    txtPassword.PasswordChar = '\0'; // Show text
                }
                else
                {
                    txtPassword.PasswordChar = '●'; // Hide text
                }
            };
            this.Controls.Add(chkShowPwd);

            // CONFIRM PASSWORD
            AddLabel("Confirm Password", 150, 380);
            txtConfirmPassword.Location = new Point(150, 405);
            txtConfirmPassword.Size = new Size(330, 40);
            txtConfirmPassword.PasswordChar = '●';
            txtConfirmPassword.UseSystemPasswordChar = false;

            // 🔥 FIX: Register Confirm Password Show/Hide Toggle CheckBox
            CheckBox chkShowConfirmPwd = new CheckBox();
            chkShowConfirmPwd.Text = "Show";
            chkShowConfirmPwd.Font = new Font("Segoe UI", 9);
            chkShowConfirmPwd.Location = new Point(490, 412);
            chkShowConfirmPwd.AutoSize = true;
            chkShowConfirmPwd.Cursor = Cursors.Hand;
            chkShowConfirmPwd.CheckedChanged += (s, ev) => {
                if (chkShowConfirmPwd.Checked)
                {
                    txtConfirmPassword.UseSystemPasswordChar = false;
                    txtConfirmPassword.PasswordChar = '\0'; // Show text
                }
                else
                {
                    txtConfirmPassword.PasswordChar = '●'; // Hide text
                }
            };
            this.Controls.Add(chkShowConfirmPwd);

            // DATE OF BIRTH
            AddLabel("Date of Birth", 150, 460);
            dtpDOB.Location = new Point(150, 485);
            dtpDOB.Size = new Size(330, 40);
            dtpDOB.Font = new Font("Segoe UI", 12);

            // ROLE
            AddLabel("Account Type", 150, 540);
            cmbRole.Location = new Point(150, 565);
            cmbRole.Size = new Size(330, 40);

            // REGISTER BUTTON
            btnRegister.Text = "CREATE ACCOUNT";
            btnRegister.Location = new Point(150, 635);
            btnRegister.Size = new Size(330, 45);
            StyleButton(btnRegister);

            // BACK BUTTON
            btnBack.Text = "BACK TO LOGIN";
            btnBack.Location = new Point(150, 695);
            btnBack.Size = new Size(330, 40);
            btnBack.FlatStyle = FlatStyle.Flat;
            btnBack.ForeColor = Color.FromArgb(18, 95, 170);

            // Event bindings
            btnRegister.Click -= btnRegister_Click;
            btnRegister.Click += btnRegister_Click;

            btnBack.Click -= btnBack_Click;
            btnBack.Click += btnBack_Click;
        }

        private void AddLabel(string text, int x, int y)
        {
            Label lbl = new Label();
            lbl.Text = text;
            lbl.Font = new Font("Segoe UI", 11);
            lbl.AutoSize = true;
            lbl.Location = new Point(x, y);
            this.Controls.Add(lbl);
        }

        private void StyleButton(Button btn)
        {
            btn.BackColor = Color.FromArgb(18, 95, 170);
            btn.ForeColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            btn.Cursor = Cursors.Hand;
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (txtName.Text == "" || txtEmail.Text == "" || txtPassword.Text == "" || txtConfirmPassword.Text == "" || cmbRole.SelectedIndex == -1)
            {
                MessageBox.Show("Please fill all fields");
                return;
            }

            // 🔥 STRIKT EMAIL FORMAT CHECK
            string emailInput = txtEmail.Text.Trim().ToLower();
            if (!emailInput.Contains("@") || !emailInput.EndsWith(".com") || emailInput.Length < 7)
            {
                MessageBox.Show("Please enter a valid email address format (e.g., user@gmail.com).", "Invalid Email Format", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 🔥 FIX: Password Mismatch Validation Check (Error box popup triggered immediately)
            if (txtPassword.Text != txtConfirmPassword.Text)
            {
                MessageBox.Show("Password does not match! Please check both fields again.", "Password Mismatch", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Age Restriction Verification (15 Years old)
            DateTime dob = dtpDOB.Value;
            DateTime today = DateTime.Today;
            int age = today.Year - dob.Year;
            if (dob.Date > today.AddYears(-age)) age--;

            if (age < 15)
            {
                MessageBox.Show("Registration failed! You must be at least 15 years old to register.", "Age Restriction", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            UserModel user = new UserModel();
            user.Name = txtName.Text;
            user.Email = emailInput;
            user.Password = txtPassword.Text;
            user.Role = cmbRole.SelectedItem.ToString();

            if (user.Role == "Student")
            {
                string generatedRoll = "NIMS-" + DateTime.Now.Year + "-ST" + new Random().Next(100, 999);
                user.RollNumber = generatedRoll;
                user.DateOfBirth = dtpDOB.Value.ToString("yyyy-MM-dd");
                user.Semester = "1st Semester";
            }
            else
            {
                user.RollNumber = null;
                user.DateOfBirth = null;
                user.Semester = null;
            }

            UserService service = new UserService();
            service.RegisterUser(user);

            MessageBox.Show("Registration Successful");
            LoginForm login = new LoginForm();
            login.Show();
            this.Hide();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            LoginForm login = new LoginForm();
            login.Show();
            this.Hide();
        }

        private void RegisterForm_Load(object sender, EventArgs e)
        {
        }
    }
}