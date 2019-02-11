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
    public partial class RegistrationForm : Form
    {
        AddExpense ae;
        public RegistrationForm()
        {
            InitializeComponent();
            this.ae = new AddExpense();
        }

        //propertes
        public UserField UserGui { get; set; }

        private void changeColor(object sender, EventArgs e)
        {
            this.button3.BackColor = Color.Red;
        }

        private void minimize(object sender, EventArgs e)
        {
            this.button2.BackColor = Color.Purple;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(validation())
            {
                string sql =String.Format("INSERT INTO `users`( `name`, `email`, `phone_no`, `password`, `status`, `bday`,`nid`) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}')",textBox1.Text,textBox2.Text,textBox5.Text,textBox3.Text,comboBox1.Text,dateTimePicker1.Text,textBox6.Text);
                if(this.ae.registerUser(sql))
                {
                    DialogResult dialogResult = MessageBox.Show("Registration SuccessFul", "Add new user?", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        textBox1.Text = "";
                        textBox2.Text = "";
                        textBox3.Text = "";
                        textBox4.Text = "";
                        textBox5.Text = "";
                        textBox6.Text = "";
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        this.Hide();
                        this.UserGui.Show();
                    }
                }
                else
                {
                    MessageBox.Show("Unable to register");
                }
            }
            else
            {
                MessageBox.Show("Please give us all the information");
            }
        }

        public bool validation()
        {
            if (check(textBox1.Text) && check(textBox2.Text) && check(textBox3.Text) && check(textBox4.Text) && check(textBox5.Text) && check(textBox6.Text) && check(comboBox1.Text))
                return true;
            return false;
        }

        public bool check(string s)
        {
            if (String.IsNullOrEmpty(s))
                return false;
            return true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.UserGui.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
