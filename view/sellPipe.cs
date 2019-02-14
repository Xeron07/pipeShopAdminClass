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
    public partial class sellPipe : UserControl
    {
        sellGEtData sgd = null;
        priceTager pt = null;
        Alert alert = null;
        bool hasDiscount=false;
        public sellPipe()
        {
            InitializeComponent();
            this.sgd = new sellGEtData();
            this.alert = new Alert();
            this.pt = new priceTager();
            paidAmount.Enabled = false;
            discountField.Hide();
           
            AddCombo1Data();
           // AddCombo2Data();
        }

        //properties
        public string CustomerName{ get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPhoneNumber { get; set; }
        public string PipeSize { set; get; }
        public string TotalPrice { get; set; }
        public string PaidAmount { get; set; }
        public string DueAmount { get; set; }
        public string PricePerPipe { set; get; }
        public string Discount { get; set; }
        public string Quantity { get; set; }
        public string PipeName { get; set; }
        public string UserName { get; set; }
        public string TotalPrize { get; set; }

        public void AddCombo1Data()
        {
            List<string> data = this.sgd.PipeNames;
            if(data.Count>0)
            {
                for (int i = 0; i < data.Count; i++)
                    comboBox1.Items.Add(data[i]);
            }
            else
                alert.Show(this.alert.Warning, "No Data Found or Check your internet connection.");
        }
        public void AddCombo2Data()
        {
            // int x = 1;
            List<string> x = this.sgd.PipeSizes;
            if (x.Count>0)
            {
                for (int i = 0; i < x.Count; i++)
                    comboBox2.Items.Add(x[i]);
            }
            else
                alert.Show(this.alert.Warning, "No Data Found or Check your internet connection.");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            hasDiscount = true;

            discountField.Show();
        }

        private void checkPhoneNumber(object sender, KeyEventArgs e)
        {
            int val;
            if(!String.IsNullOrEmpty(phoneNumber.Text))
            if(phoneNumber.Text.Length>8 || !int.TryParse(phoneNumber.Text,out val))
            {
                alert.Show(alert.Error, "Enter valid number & it must be 8 digit only");
                phoneNumber.Text = "";
            }
        }

        private void proceedButton_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
             if (String.IsNullOrEmpty(textBox1.Text) || String.IsNullOrEmpty(cEmail.Text) || String.IsNullOrEmpty(phoneNumber.Text) || String.IsNullOrEmpty(paidAmount.Text) || quantityField.Text.Equals("0"))
            {


                MessageBox.Show("Enter all information\nEnter \'no-email\' if customer doesn't have any email-address.\nPipe Quanity must be more then 0");
            }
            else if ((int.Parse(paidAmount.Text)) == 0)
            {
                this.alert.Show(alert.Error, "Customer must pay");
            }
            else
            {
                if ((Double.Parse(totalPrice.Text) / 2) > (Double.Parse(paidAmount.Text)))
                    this.alert.Show(alert.Error, "Customer should may more then half");
                else
                {
                    if (discountField.Text.Equals(""))
                    {
                        discountField.Text = "0";
                        ddval.Text = "0";
                    }
                    string billNo = DateTime.Now.ToString("MM") + DateTime.Now.ToString("dd") + DateTime.Now.ToString("yy") + DateTime.Now.ToString("hh") + DateTime.Now.ToString("mm") + DateTime.Now.ToString("ss");
                    var data = new sellPipe()
                    {

                        PipeName = comboBox1.Text,
                        PipeSize = comboBox2.Text,
                        Quantity = quantityField.Text,
                        CustomerName = textBox1.Text,
                        Discount = discountField.Text,
                        CustomerEmail = cEmail.Text,
                        CustomerPhoneNumber = (comboBox3.Text + phoneNumber.Text),
                        TotalPrize = (Double.Parse(totalPrice.Text)-Double.Parse(ddval.Text)).ToString(),
                        PricePerPipe = pricePerUnit.Text,
                        DueAmount = dueAmount.Text,
                        UserName = this.UserName,
                        PaidAmount = this.paidAmount.Text,
                        TotalPrice=totalPrice.Text
                    };

                    new PdfMaker().createPdf(billNo, data);
                    this.Cursor = Cursors.Default;
                    mailSender(data, billNo);
                }
            }

             this.Cursor = Cursors.Default;

        }

        private void checkQuantity(object sender, EventArgs e)
        {
            double val;
            if (!String.IsNullOrEmpty(quantityField.Text))
            {
                if (comboBox1.Text.Equals("-Select-") && comboBox2.Text.Equals("-Select-"))
                {
                    this.alert.Show(alert.Error, "You have to select pipe name and size first");
                }
                else if (!Double.TryParse(quantityField.Text, out val))
                {
                    alert.Show(alert.Error, "Enter a valid quantity number");
                    quantityField.Text = "0";
                }
                else
                {

                    if (this.pt.getPrice(comboBox1.Text, comboBox2.Text))
                    {
                        if (double.Parse(this.pt.Quantity) >= double.Parse(this.quantityField.Text)&&!String.IsNullOrEmpty(quantityField.Text))
                        {
                            double x;
                            if(double.TryParse(this.quantityField.Text,out x)) {

                                this.pricePerUnit.Text = this.pt.UnitPrice;
                                this.totalPrice.Text = (double.Parse(this.pt.UnitPrice) * x).ToString();
                                if (hasDiscount && discountField.Text.Equals("0"))
                                    TotalPrize = totalPrice.Text;
                                paidAmount.Enabled = true;
                            }
                            else
                            {
                                //
                            }
                           
                        }
                        else
                        {
                            paidAmount.Enabled = false;
                            totalPrice.Text = "0";
                            this.alert.Show(alert.Error, "This amount of product is not available right now");
                        }

                    }
                    else
                    {
                        MessageBox.Show("Connection Problem\n\nContact with administrator");
                    }
                }
            }
            else
            {
                //MessageBox.Show("empty");
            }
        }

        private void calculateDueAmount(object sender, KeyEventArgs e)
        {
            int x;
            if (!String.IsNullOrEmpty(paidAmount.Text))
            {
                if (int.TryParse(paidAmount.Text, out x))
                {

                    if (!String.IsNullOrEmpty(ddval.Text))
                    {
                        if ((int.Parse((Double.Parse(totalPrice.Text) - Double.Parse(ddval.Text)).ToString()) < int.Parse(paidAmount.Text)))
                            {
                            this.alert.Show(alert.Error, "You can't pay more than total price");
                            paidAmount.Text = "";
                         }
                        else
                        dueAmount.Text = (int.Parse((Double.Parse(totalPrice.Text) - Double.Parse(ddval.Text)).ToString()) - int.Parse(paidAmount.Text)).ToString();

                    }
                    else
                    {
                        if(int.Parse(paidAmount.Text)> (int.Parse(totalPrice.Text)))
                            {
                            this.alert.Show(alert.Error, "You can't pay more than total price");
                            paidAmount.Text = "";
                        }
                        else
                        dueAmount.Text = (int.Parse(totalPrice.Text) - int.Parse(paidAmount.Text)).ToString();
                    }
                       
                }
                else
                {
                    MessageBox.Show("Enter valid number only");
                }
            }
        }

        private void discountValue(object sender, KeyEventArgs e)
        {
            int check;
            if(int.TryParse(discountField.Text,out check)|| String.IsNullOrEmpty(discountField.Text))
            {
                if (!String.IsNullOrEmpty(discountField.Text))
                {
                    double value = Double.Parse(discountField.Text);
                    double tprice = Double.Parse(totalPrice.Text);
                    if (value > (tprice / 2))
                    {
                        this.alert.Show(alert.Error, "Discount must be less then 30%");
                        discountField.Text = "";
                        ddval.Text = "0";
                    }
                    else
                    {
                        ddval.Text = discountField.Text;
                        TotalPrize = (Double.Parse(totalPrice.Text) - Double.Parse(ddval.Text)).ToString();
                    }
                }
            }
            else
            {
                this.alert.Show(alert.Error, "Enter valid Number");
                discountField.Text = "";
            }
        }


        public void mailSender(sellPipe data, string billNo)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("anspipeshop@gmail.com");
                mail.To.Add("ans2k19.a@hotmail.com");
                mail.Subject = "Sells Bill No:" + billNo;
                mail.Body = "\nSold By: " + data.UserName + " \n\n\nCustomer Name:   " + data.CustomerName +"\nCustomer Email: "+data.CustomerEmail+"\nCustomer PhoneNumber: "+data.CustomerPhoneNumber+ "\n\nPipe Name:    " + data.PipeName +"\nPipe Size:   " + data.PipeSize + "\nQuantity:   " + data.Quantity + "\n\nTotal Price:  " + data.TotalPrice+"\n\nPaid Amount: "+data.PaidAmount+"\n\nDue Amount: "+data.DueAmount+"\n\nDiscount: "+discountField.Text;

                //anspipeshop@gmail.com", "tonmoydotzone$$$"


                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("anspipeshop@gmail.com", "tonmoydotzone$$$");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
                //MessageBox.Show("mail Send");
                notifyIcon1.ShowBalloonTip(1000, "Sending Sell Report As Mail", "Mail has been send to admin", ToolTipIcon.Info);
            }
            catch (Exception ex)
            {
                //  MessageBox.Show(ex.ToString());
               // notifyIcon1.ShowBalloonTip(1000, "Sending Sell Mail failed", ex.ToString(), ToolTipIcon.Info);
              //  MessageBox.Show(""+ex);
            }
        }

        private void changePipeSize(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            this.sgd.getSizes(comboBox1.Text);
            comboBox2.Items.Clear();
            int x = this.sgd.PipeSizes.Count;
            for(int i=0;i<x;i++)
            {
                comboBox2.Items.Add(this.sgd.PipeSizes[i]);
            }

            this.Cursor = Cursors.Default;
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }
    }
}
