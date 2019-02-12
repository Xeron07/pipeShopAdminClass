using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BAL;
using System.Net.Mail;

namespace view
{
    public partial class purchaseGUI : UserControl
    {
        StockInfo st = null;
        purcahseDataAdder pda=null;
        AddExpense ae;
        Alert alert = null;
         sellGEtData sgd = null;
        bool psize=false;
        bool pname=false;
        bool shouldReport = false;
        priceTager pc;
        public purchaseGUI()
        {
            InitializeComponent();
            this.sgd=new sellGEtData();
            this.st = new StockInfo();
            this.pda = new purcahseDataAdder();
            alert = new Alert();
            this.ae = new AddExpense();
            pc = new priceTager();
            this.label5.Hide();
            this.label6.Hide();
            this.textBox1.Hide();
            this.textBox2.Hide();
           
            this.expensePanel.Hide();
           
            DataAdd();
        }
        //properties
        public string UserName { get; set; }

        public void DataAdd()
        {
            if (!this.st.dataAdd())
                alert.Show(this.alert.Error,"No Internet Connection.");
            else
            {
               
                    foreach(string name in this.st.PipeName)
                    {
                        this.pipename.Items.Add(name);
                    }
                    this.pipename.Items.Add("--new--");

                foreach (string size in this.st.PipeSize)
                {
                    this.pipesize.Items.Add(size);
                }
                this.pipesize.Items.Add("--new--");


            }
        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void yesChange(object sender, EventArgs e)
        {
            if(this.checkBox1.Checked)
            {
                shouldReport = true;
                this.checkBox2.Checked = false;
                this.expensePanel.Show();
                this.expense_label.Text = "";
            }
            
        }

        private void noChanged(object sender, EventArgs e)
        {
            if (this.checkBox2.Checked)
            {
                shouldReport = false;
                this.checkBox1.Checked = false;
                this.expensePanel.Hide();
                this.expense_label.Text = "No Expense";
            }
        }

        private void nameChange(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            if(pipename.Text.Equals("--new--"))
            {
                pname = true;
                this.pipename.Enabled = false;
                this.label5.Show();
               
                this.textBox1.Show();
            }
            else
            {
                pName.Text = pipename.Text;
                this.sgd.getSizes(pipename.Text);
                pipesize.Items.Clear();
                int x = this.sgd.PipeSizes.Count;
                for (int i = 0; i < x; i++)
                {
                    pipesize.Items.Add(this.sgd.PipeSizes[i]);
                }

                pipesize.Items.Add("--new--");
            }

            this.Cursor = Cursors.Default;
        }

        private void sizeChanged(object sender, EventArgs e)
        {
            if(pipesize.Text.Equals("--new--"))
            {
                psize = true;
                this.pipesize.Enabled = false;
                this.label6.Show();
                this.textBox2.Show();
             
            }
            else
            {
                label21.Text = pipesize.Text;
            }
        }

        private void newPipeName(object sender, EventArgs e)
        {
            pName.Text = textBox1.Text;
        }

        private void newPipeSize(object sender, EventArgs e)
        {
            label21.Text = textBox2.Text;
        }

        private void soldBy(object sender, EventArgs e)
        {
            label26.Text = textBox3.Text;
        }

        private void quantityField(object sender, EventArgs e)
        {
            double x;
            if(double.TryParse(textBox4.Text,out x))
            label23.Text = textBox4.Text;
            else
            {
                this.alert.Show(alert.Warning,"Enter a valid number");
                textBox4.Text = "";
                label23.Text = "None";
            }
        }

        private void unitPrice(object sender, EventArgs e)
        {
            double x;
            if (Double.TryParse(textBox5.Text, out x))
                label24.Text = textBox5.Text;
            else
            {
                this.alert.Show(alert.Warning, "Enter a valid number");
                textBox5.Text = "";
                label24.Text = "None";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(textBox4.Text)&&String.IsNullOrEmpty(textBox5.Text))
            {
                MessageBox.Show("Fill up the fields");
            }
            else
            {
                label25.Text = ((double.Parse(label23.Text)) * (double.Parse(label24.Text))).ToString();
                label19.Visible = true;
                label25.Visible = true;
                submit.Visible = true;
            }
        }

        private void submit_Click(object sender, EventArgs e)
        {
            if(this.checkBox1.Checked)
            {
                if(shouldReport)
                {
                    this.alert.Show(alert.Warning,"Report the expense report first");
                }
                else
                {
                    stockUpdate();
                }
            }
            else
                stockUpdate();
        }

        private void report_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(detailsInfo.Text) && String.IsNullOrEmpty(exAmount.Text))
            {
                this.alert.Show(alert.Error, "Fill up all the expense fields");
            }
            else
            {
                int x;
                if (int.TryParse(exAmount.Text, out x))
                {
                    if (this.ae.values(detailsInfo.Text, exAmount.Text, this.UserName))
                    {
                       MessageBox.Show("Expense report done") ;
                    }
                    else
                    {
                        MessageBox.Show("Plesase check internet connection");
                    }
                }
                else
                {
                    MessageBox.Show("Enter a valid expense amount");
                }
            }
                shouldReport = false;
        }

        public void stockUpdate()
        {
            if(check(pName.Text))
            {
                this.alert.Show(this.alert.Warning, "Pipe Name Not Given ");
            }
            else if (check(label21.Text))
            {
                this.alert.Show(this.alert.Warning, "Pipe Size Not Given ");
            }
            else if (check(label26.Text))
            {
                this.alert.Show(this.alert.Warning, "Seller Information Not Given ");
            }
            else
            {
                string sql;
                string sql2;
                string billNo = DateTime.Now.ToString("mm") + DateTime.Now.ToString("dd") + DateTime.Now.ToString("yy") + DateTime.Now.ToString("hh") + DateTime.Now.ToString("MM") + DateTime.Now.ToString("ss");
                var data = new sellPipe()
                {
                    PipeName = pName.Text,
                    PipeSize=label21.Text,
                    Quantity=label23.Text,
                    PricePerPipe=label24.Text,
                    TotalPrice=label25.Text,
                    UserName=this.UserName
                };

                if(!pname && !psize)
                {
                    this.pc.getPrice(data.PipeName, data.PipeSize);
                    int quantity = int.Parse(this.pc.Quantity);
                    data.Quantity = (quantity + int.Parse(label23.Text)).ToString();
                    sql = String.Format("update stock set quantity={0} where pipe_name='{1}' and pipe_size='{2}'",data.Quantity,data.PipeName,data.PipeSize);
                    sql2 = sql2 = string.Format("INSERT INTO `transection`(`billNo`,`item_date`, `pipe_name`, `pipe_size`, `quantity`, `amount` ,`paid_amount`,`due_amount`,`done_by`, `status`) VALUES ('{0}','{1}',\'" + data.PipeName + "\',\'" + data.PipeSize + "\'," + data.Quantity + "," + data.TotalPrice + ",{2},{3},'{4}',\'purchase\')", billNo, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), data.TotalPrice, 0, data.UserName);
                    // MessageBox.Show(sql);
                    if(this.pda.addToDb(sql))
                    {

                        if(this.pda.addToDb(sql2))
                        {
                            this.alert.Show(alert.Success, "Purchase report done");
                            mailSender(data,billNo);
                        }
                        else
                        {
                            this.alert.Show(alert.Error, "Transection error\nNet Connection problem");
                        }

                    }
                    else
                    {
                        this.alert.Show(alert.Error, "update error\nNet Connection problem");
                    }
                }
                else
                {
                    if (!String.IsNullOrEmpty(textBox5.Text))
                    {
                        if(!this.pda.CheckStock(data.PipeName,data.PipeSize))
                        sql = String.Format("insert into stock(`pipe_name`,`pipe_size`,`quantity`,`price_per_unit`)values('{0}','{1}',{2},{3})", data.PipeName, data.PipeSize, data.Quantity, data.PricePerPipe);
                        else
                        {
                            this.pc.getPrice(data.PipeName, data.PipeSize);
                            int quantity = int.Parse(this.pc.Quantity);
                            data.Quantity = (quantity + int.Parse(label23.Text)).ToString();
                            sql = String.Format("update stock set quantity={0} where pipe_name='{1}' and pipe_size='{2}'", data.Quantity, data.PipeName, data.PipeSize);
                        }
                        MessageBox.Show(sql);
                        sql2 = sql2 = string.Format("INSERT INTO `transection`(`billNo`,`item_date`, `pipe_name`, `pipe_size`, `quantity`, `amount` ,`paid_amount`,`due_amount`,`done_by`, `status`) VALUES ('{0}','{1}',\'" + data.PipeName + "\',\'" + data.PipeSize + "\'," + data.Quantity + "," + data.TotalPrice + ",{2},{3},'{4}',\'purchase\')", billNo, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), data.TotalPrice, 0, data.UserName);
                        if (this.pda.addToDb(sql))
                        {
                            if (this.pda.addToDb(sql2))
                            {
                                this.alert.Show(alert.Success, "Purchase report done");
                            }
                            else
                            {
                                this.alert.Show(alert.Error, "Transection error\nNet Connection problem");
                            }

                        }
                        else
                        {
                            this.alert.Show(alert.Error, "update error\nNet Connection problem");
                        }
                    }
                    else
                    {
                        this.alert.Show(alert.Error, "Enter price per unit ");
                    }
                 
                }
            }
        }

        public bool check(string value)
        {
            if (value.Equals("None"))
                return true;
            return false;
        }

        public void mailSender(sellPipe data,string billNo)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("anspipeshop@gmail.com");
                mail.To.Add("ans2k19.a@hotmail.com");
                mail.Subject = "Purchase Bill No:" + billNo;
                mail.Body = "\nPurchase By: " + data.UserName + " \n\n\nBought From:   " + textBox3.Text + "\n\nPipe Name:    " + data.PipeName + "\nPipe Size:   " + data.PipeSize + "\nQuantity:   " + data.Quantity + "\n\nTotal Price:  " + data.TotalPrice ;

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("anspipeshop@gmail.com", "tonmoydotzone$$$");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
                //MessageBox.Show("mail Send");
                notifyIcon1.ShowBalloonTip(1000, "Sending Report As Mail","Mail has been send to admin",ToolTipIcon.Info);
            }
            catch (Exception ex)
            {
                //  MessageBox.Show(ex.ToString());
               // notifyIcon1.ShowBalloonTip(1000, "Sending Purchase report Mail failed", ex.ToString(), ToolTipIcon.Info);
            }
        }
    }
}
