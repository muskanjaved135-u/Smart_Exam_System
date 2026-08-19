using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SmartExamSystem.Services;

namespace SmartExamSystem.Forms
{
    public partial class ResultManagement : Form
    {
        ResultService service;
        public ResultManagement()
        {
            InitializeComponent();
            service = new ResultService();


            LoadResults();
        }
        private void LoadResults()
        {

            dgvResults.DataSource =
            service.GetResults();

        }
        private void ResultManagement_Load(object sender, EventArgs e)
        {

        }
    }
}
