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
namespace pipeshop
{
    public partial class Expense : UserControl
    {
        
        AddExpense ae;
        public Expense()
        {
            this.ae = new AddExpense();
            InitializeComponent();
        }
        //properties
        public string UserName { get; set; }
        private void button1_Click(object sender, EventArgs e)
        {
            if (this.ae.values(textBox1.Text, textBox2.Text,this.UserName))
            {
                MessageBox.Show("Reporting Success");
            }
            else
            {
                MessageBox.Show("Connection Problem.");
            }
        }
    }
}
