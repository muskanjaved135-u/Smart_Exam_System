using System;
using System.Drawing;
using System.Windows.Forms;

namespace SmartExamSystem.Forms
{
    public partial class SplashScreen : Form
    {


        Timer splashTimer;


        public SplashScreen()
        {
            InitializeComponent();


            DesignSplash();

            splashTimer = new Timer();

            splashTimer.Interval = 4000;

            splashTimer.Tick += SplashTimer_Tick;

            splashTimer.Start();

        }





        private void DesignSplash()
        {


            this.Size =
            new Size(900, 500);


            this.StartPosition =
            FormStartPosition.CenterScreen;


            this.FormBorderStyle =
            FormBorderStyle.None;


            this.BackColor =
            Color.White;





            // IMAGE


            PictureBox picture =
            new PictureBox();


            picture.Size =
            new Size(900, 500);


            picture.Location =
            new Point(0, 0);



            picture.SizeMode =
            PictureBoxSizeMode.StretchImage;



            picture.Image =
            Image.FromFile(
            Application.StartupPath +
            @"\Images\Splash.png");



            this.Controls.Add(picture);






            // DARK OVERLAY


            Panel overlay =
            new Panel();


            overlay.Size =
            new Size(900, 500);


            overlay.BackColor =
            Color.FromArgb(
            120,
            0,
            0,
            0);


            overlay.Location =
            new Point(0, 0);



            this.Controls.Add(overlay);





            // TITLE


            Label title =
            new Label();


            title.Text =
            "SMART EXAM\nSYSTEM";


            title.Font =
            new Font(
            "Segoe UI",
            35,
            FontStyle.Bold);



            title.ForeColor =
            Color.White;


            title.AutoSize = true;


            title.TextAlign =
            ContentAlignment.MiddleCenter;



            title.Location =
            new Point(260, 120);



            overlay.Controls.Add(title);






            // SUB TITLE


            Label sub =
            new Label();


            sub.Text =
            "Online Examination Management System";


            sub.Font =
            new Font(
            "Segoe UI",
            15);



            sub.ForeColor =
            Color.White;


            sub.AutoSize = true;


            sub.Location =
            new Point(260, 250);



            overlay.Controls.Add(sub);








            // LOADING


            ProgressBar bar =
            new ProgressBar();


            bar.Location =
            new Point(250, 350);



            bar.Size =
            new Size(400, 20);



            bar.Style =
            ProgressBarStyle.Marquee;


            bar.MarqueeAnimationSpeed =
            30;



            overlay.Controls.Add(bar);





            Label loading =
            new Label();


            loading.Text =
            "Loading Application...";


            loading.ForeColor =
            Color.White;


            loading.Font =
            new Font(
            "Segoe UI",
            11);



            loading.AutoSize = true;


            loading.Location =
            new Point(350, 390);



            overlay.Controls.Add(loading);


        }






        private void SplashTimer_Tick(
        object sender,
        EventArgs e)
        {


            splashTimer.Stop();

            LoginForm login = new LoginForm();

            login.Show();

            this.Hide();
        }

        
        private void timer1_Tick(object sender, EventArgs e)
        {

        }


    }
}