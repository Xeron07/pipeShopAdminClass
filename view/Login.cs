using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BAL;
namespace view
{
    public partial class Login : Form
    {
        loading ld;
        int i = 0;
        LoginController lc = null;
        Alert alert = null;
        UserField uf = null;
        public Login()
        {
            InitializeComponent();
            this.alert = new Alert();
            this.lc = new LoginController();
            loadingImg.Hide();
            labelCheck.Hide();
        }

        //properties

            public string Uname { set; get; }

        public string Email
        {
            set { this.email.Text = value; }
            get { return this.email.Text; }
        }        

        public string Password
        {
            set { this.password.Text = value; }
            get { return this.password.Text; }
        }

        private void mouseOnExitButton(object sender, EventArgs e)
        {
            button1.BackColor = Color.Red;
        }

        private void mouseLeaveExitButton(object sender, EventArgs e)
        {
            button1.BackColor = Color.Transparent;
        }

        private void mouseOnMinButton(object sender, EventArgs e)
        {
            button2.BackColor = Color.WhiteSmoke;
        }

        private void mouseLeaveMinButton(object sender, EventArgs e)
        {
            button2.BackColor = Color.Transparent;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void loginButton_Click(object sender, EventArgs e)
        {

            //loginButton.Image = Properties.Resource1.loading1;
            //loginButton.Text = "Checking";
            loginButton.Hide();
            loadingImg.Show();
            labelCheck.Show();
            string uname=null;
            string uid=null;
            string status=null;
            lc.Email = this.Email;
            lc.Password = this.Password;
            if (lc.logUserIn(ref uname, ref uid, ref status))
            {
               //if(string.Equals(status, "admin", StringComparison.OrdinalIgnoreCase))
               // {
                    this.Hide();
                    this.Uname = uname;
                ld = new loading(this.Uname);
                    ld.Show(); 
                    timer1.Interval = 1000; // specify interval time as you want
                    timer1.Tick += new EventHandler(timer_Tick);
                    timer1.Start();
                    
                    this.uf = new UserField() { UserName =  Uname };
               //     alert.Show(this.alert.Success, "Login SuccessFul ");
               // }
               //else
               //{
               //     alert.Show(alert.Error, "Only admin can login from this software");
               //     loginButton.Show();
               //     loadingImg.Hide();
               //     labelCheck.Hide();


               // }


            }
            else
            {
                alert.Show(this.alert.Error, "Wrong Email Or Password");
                loginButton.Show();
                loadingImg.Hide();
                labelCheck.Hide();
            }
        }


        void timer_Tick(object sender, EventArgs e)
        {
            
            if (i <= 5)
                i++;
            else
            {
                ld.Hide();
                timer1.Stop();
                this.uf.Show();
            }
        }
    }
}
