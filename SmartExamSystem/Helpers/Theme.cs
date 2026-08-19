using System.Drawing;
using System.Windows.Forms;

namespace SmartExamSystem.Helpers
{
    public static class Theme
    {

        public static Color PrimaryColor =
            Color.FromArgb(25, 118, 210);


        public static Color DarkColor =
            Color.FromArgb(20, 33, 61);


        public static Color BackgroundColor =
            Color.FromArgb(245, 247, 250);



        public static void StyleButton(Button btn)
        {

            btn.BackColor = PrimaryColor;

            btn.ForeColor = Color.White;

            btn.FlatStyle = FlatStyle.Flat;

            btn.FlatAppearance.BorderSize = 0;

            btn.Font =
            new Font("Segoe UI", 11, FontStyle.Bold);


            btn.Height = 40;

        }



        public static void StyleLabel(Label lbl)
        {

            lbl.Font =
            new Font("Segoe UI", 11);


            lbl.ForeColor =
            DarkColor;

        }



        public static void StyleForm(Form form)
        {

            form.BackColor =
            BackgroundColor;


            form.Font =
            new Font("Segoe UI", 10);


        }

    }
}