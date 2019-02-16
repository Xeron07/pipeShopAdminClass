using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
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
            this.button2.BackColor = Color.Red;
        }

        private void minimize(object sender, EventArgs e)
        {
            this.button3.BackColor = Color.Purple;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(validation())
            {
                if (textBox5.Text.Length != 11)
                {
                    MessageBox.Show("Give us a valid mobile number");
                }
                else if (textBox6.Text.Length != 15)
                {
                    MessageBox.Show("NID number is not valid");
                }
                else
                {
                    Register();
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

        public string EmailString { get; set; }
        public bool sendMail(string email, string password)
        {
            makeString(email, password);
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.IsBodyHtml = true;
                mail.From = new MailAddress("anspipeshop@gmail.com");
                mail.To.Add(email);
                mail.Subject = "Registration Confirmation ";
                mail.Body = EmailString;

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("anspipeshop@gmail.com", "tonmoydotzone$$$");
                SmtpServer.EnableSsl = true;
                

                SmtpServer.Send(mail);
                
                return true;
            }
            catch (Exception ex)
            {
               
                return false;

            }
        }


        public void makeString(string m, string p)
        {

            string value = " <p style='color: cadetblue'>Your Email address: "+m+"</p> <p style='color: dimgrey'>Your current password: "+p+"</p><br/><br/><h2>Regards,</h2><br><h2>Admin</h2><br/><h2><i><u>Afsar & Sons Pipe Shop</u></i></h2><br/><hr/><h4 style='color: darkred'> Do not reply to this mail......</h4></div></body></html>";
            EmailString = string.Format("{0} {1}",Properties.Resources.mailSenderText,value);
        }


        public void Register()
        {
            string sql = String.Format("INSERT INTO `users`( `name`, `email`, `phone_no`, `password`, `status`, `bday`,`nid`) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}')", textBox1.Text, textBox2.Text, textBox5.Text, textBox3.Text, comboBox1.Text, dateTimePicker1.Text, textBox6.Text);
            string checkSql = String.Format("select * from users where email='{0}'", textBox2.Text);
            if (!this.ae.checkUser(checkSql))
            {
                if (this.ae.registerUser(sql))
                {
                    if (sendMail(textBox2.Text, textBox3.Text))
                    {
                        DialogResult dialogResult = MessageBox.Show("Registration SuccessFul", "Add new user?",
                            MessageBoxButtons.YesNo);
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
                        MessageBox.Show("Something wrong with this email address.");
                    }
                }
                else
                {
                    MessageBox.Show("Unable to register");
                }
            }
            else
            {
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
                MessageBox.Show("There is a user exist with the same email");
            }


        }
    }
}
