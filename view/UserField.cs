using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace view
{
    public partial class UserField : Form
    {
        public bool isMax = false;
        public UserField()
        {
            InitializeComponent();
            //sellReport1.BringToFront();
        }

        //properties
        public string UserName{
            get { return this.uname.Text; }
            set { this.uname.Text=value; }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void mouseOnMin(object sender, EventArgs e)
        {
            button3.BackColor = Color.GhostWhite;
        }

        private void mouseOnMax(object sender, EventArgs e)
        {
            button1.BackColor = Color.GhostWhite;
        }

        private void mouseOnExit(object sender, EventArgs e)
        {
            button2.BackColor = Color.Red;
        }

        private void mouseLeaveExit(object sender, EventArgs e)
        {
            button2.BackColor = Color.Transparent;
        }

        private void mouseLeaveMax(object sender, EventArgs e)
        {
            button1.BackColor = Color.Transparent;
        }

        private void mouseLeaveMin(object sender, EventArgs e)
        {
            button3.BackColor = Color.Transparent;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //this.transectionReport1 = new transectionReport();
            this.transectionReport1.BringToFront();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            sellPipeButton.Width = panel2.Width;
            stockReportButton.Width = panel2.Width;
            transectionButton.Width = panel2.Width;
            purchaseButton.Width = panel2.Width;
            regButton.Width = panel2.Width;
            expenseButton.Width = panel2.Width;
            exReportButton.Width = panel2.Width;
            usersButton.Width = panel2.Width;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(this.isMax)
            {
                this.WindowState = FormWindowState.Normal;
                this.isMax = false;
            }
            else
            {
                this.WindowState = FormWindowState.Maximized;
                this.isMax = true;
            }
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            panel3.Width = this.Width;
        }

        private void stockReportButton_Click(object sender, EventArgs e)
        {
            sellReport1.BringToFront();
        }

        private void purchaseButton_Click(object sender, EventArgs e)
        {
            this.purchaseGUI2.UserName = uname.Text;
            this.purchaseGUI2.Refresh();
            this.purchaseGUI2.BringToFront();
        }

        private void sellPipeButton_Click(object sender, EventArgs e)
        {
            sellPipe1.UserName = this.uname.Text;
            sellPipe1.BringToFront();
        }

        private void expenseButton_Click(object sender, EventArgs e)
        {
            expense1.UserName = uname.Text;
            expense1.BringToFront();
        }

        private void form_load(object sender, EventArgs e)
        {
            timer1.Start();
            label3.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy hh:mm:ss tt");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Start();
            label3.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy hh:mm:ss tt");
        }

        private void regButton_Click(object sender, EventArgs e)
        {

            this.Hide();
            new RegistrationForm(){UserGui=this}.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void logoutButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Login().Show();
        }
    }
}
