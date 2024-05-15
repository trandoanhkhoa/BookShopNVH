using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookShopNVH
{
    public partial class LoadingForm : Form
    {
        public LoadingForm()
        {
            InitializeComponent();
        }
        int startpos = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            startpos += 1;
            prgLoading.Value = startpos;
            lblLoadingpercent.Text = startpos + "%";
            if(prgLoading.Value == 100)
            {
                prgLoading.Value = 0;
                timer1.Stop();
                LoginForm frm = new LoginForm();
                frm.Show();
                this.Hide();
            }
        }

        private void LoadingForm_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }
    }
}
