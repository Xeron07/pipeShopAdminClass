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
namespace view
{
    public partial class sellReport : UserControl
    {
        SellReportData sData;
        bool dates = false;
        Alert alert;
        StockDataTable sdt;
        public sellReport()
        {
            InitializeComponent();
            this.alert = new Alert();
            comboBox2.DisplayMember = "Text";
            comboBox2.ValueMember = "Value";
            this.sdt = new StockDataTable();
            dateTimePicker1.Hide();
            sData = new SellReportData();
            getData();
        }
        public void getData()
        {
            string sql = string.Format("select * from stock");
            if(this.sData.listMaker(sql))
            {

                if (!addData(this.sData.Datatables))
                    MessageBox.Show("No Data Found......");
            }
            else
            {
                MessageBox.Show("Check your Internet connection");
            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_Paint(object sender, PaintEventArgs e)
        {
           // dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(206, 214, 224);
         //   dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.FromArgb(22, 160, 133);
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView1.BackgroundColor = Color.White;

            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(204, 174, 98);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        }
        public bool addData(DataTable dt)
        {
            if (dataGridView1.Rows.Count != 0)
            {
                try
                {
                    dataGridView1.Rows.Clear();
                    dataGridView1.Refresh();
                }
                catch
                {
                    //
                    dataGridView1 = new DataGridView();
                }
            }
            else
            {
                dataGridView1.DataSource = null;
            }

            if (dt.Rows.Count>=1)
            {
                foreach(DataRow row in dt.Rows)
                {
                    var n = dataGridView1.Rows.Add();
                    DataRow dr = this.sdt.StockData2.NewRow();
                    dataGridView1.Rows[n].Cells[0].Value =dr["Pipe_Name"]=row["pipe_name"].ToString();
                    dataGridView1.Rows[n].Cells[1].Value =dr["Pipe_Size"]=row["pipe_size"].ToString();
                    dataGridView1.Rows[n].Cells[2].Value =dr["Quantity"]=row["quantity"].ToString();
                    dataGridView1.Rows[n].Cells[3].Value =dr["Unit_Price"]=row["price_per_unit"].ToString();
                    this.sdt.StockData2.Rows.Add(dr); 
                }
                return true;
            }
            return false; 
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {
           
        }


        public bool dataFilter(DataTable dt)
        {
            if (dataGridView1.Rows.Count != 0)
            {
                try
                {
                    dataGridView1.Rows.Clear();
                    dataGridView1.Refresh();
                    
                }
                catch
                {
                    //
                    dataGridView1 = new DataGridView();
                }
            }
            else
            {
                dataGridView1.DataSource = null;
            }
            if (dt.Rows.Count >= 1)
            {
                foreach (DataRow row in dt.Rows)
                {
                    var n = dataGridView1.Rows.Add();
                    DataRow dr = this.sdt.StockData2.NewRow();
                    dataGridView1.Rows[n].Cells[0].Value = row["Pipe_Name"];
                    dataGridView1.Rows[n].Cells[1].Value = row["Pipe_Size"];
                    dataGridView1.Rows[n].Cells[2].Value = row["Quantity"];
                    dataGridView1.Rows[n].Cells[3].Value = row["Unit_Price"];
                    this.sdt.StockData2.Rows.Add(dr);
                }

                return true;
            }
            return false;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox1.Text.Equals("Date"))
            {
                this.seachString.Hide();
                this.dateTimePicker1.Show();
               this.dateTimePicker1.Location= this.seachString.Location;
                dates = true;
            }
            else
            {
                this.seachString.Show();
                this.dateTimePicker1.Hide();
                 this.seachString.Location= this.dateTimePicker1.Location ;
                dates = false;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.Text != null && !comboBox2.Text.Equals("Sort By") && !comboBox2.Text.Equals(""))

                dataGridView1.Sort(dataGridView1.Columns[(comboBox2.SelectedItem as dynamic).Value], ListSortDirection.Ascending);

        }

        private void comboBox1_Click(object sender, EventArgs e)
        {
            this.comboBox1.Items.Clear();
            this.comboBox1.DisplayMember = "Header";
            this.comboBox1.ValueMember = "Value";
            if (this.sData.Datatables != null)
            {
                for (int i = 0; i < this.sData.Datatables.Columns.Count; i++)
                {
                    this.comboBox1.Items.Add(new { Header = this.dataGridView1.Columns[i].HeaderText, Value = this.sdt.StockData2.Columns[i].ColumnName });
                }
            }
            else
            {
                alert.Show(this.alert.Warning, "Data Table is NULL");

            }
        }

        private void dataSetView(object sender, KeyEventArgs e)
        {

            if (comboBox1.Text.Equals("Search By"))
                alert.Show(this.alert.Warning, "Select a valid item to search");

            else if (seachString.Text != null && !seachString.Text.Equals(""))
            {
                DataView dv = new DataView(this.sdt.StockData2);
                //MessageBox.Show("Combo Bix=" + comboBox1.Text);
                //MessageBox.Show("Textbox=" + seachString.Text);
                string s = null;
                s = string.Format("{0} LIKE '{1}%'", (comboBox1.SelectedItem as dynamic).Value, seachString.Text);
                dv.RowFilter = s;
                dataGridView1.DataSource = null;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                if (!dataFilter(dv.ToTable()))
                    alert.Show(this.alert.Warning, "No Data Found......");
                //   dataGridView1.DataSource = dv;
            }

        }

        private void getColumnName(object sender, MouseEventArgs e)
        {
            AutoCompleteStringCollection data = new AutoCompleteStringCollection();

            int index = dataGridView1.ColumnCount;
            comboBox2.Items.Clear();
            for (int i = 0; i < index; i++)
            {

                comboBox2.Items.Add(new { Text = dataGridView1.Columns[i].HeaderText, Value = dataGridView1.Columns[i].Name });
            }


        }

        private void button1_Click(object sender, MouseEventArgs e)
        {
            if (comboBox1.Text.Equals("Search By"))
                alert.Show(this.alert.Warning, "Select a valid option from the combo box");

            else
            {
                string hello = null;

                dateTimePicker1.CustomFormat = "dd/MM/yyy";
                dateTimePicker1.Format = DateTimePickerFormat.Custom;
                hello = dateTimePicker1.Text;
                //checking for dates
                DataView dv = new DataView(this.sdt.StockData2);
                //MessageBox.Show("Combo Bix=" + comboBox1.Text);
                //MessageBox.Show("Textbox=" + seachString.Text);
                string s = null;
                s = string.Format("{0} LIKE '{1}%'", (comboBox1.SelectedItem as dynamic).Value, hello);

                dv.RowFilter = s;
                dataGridView1.DataSource = null;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                if (!dataFilter(dv.ToTable()))
                    alert.Show(this.alert.Warning, "No Data Found......");
                //   dataGridView1.DataSource = dv;
            }
        }

        private void refresh_Click(object sender, EventArgs e)
        {
            this.sData = new SellReportData();
            this.sdt = new StockDataTable();
            Cursor.Current = Cursors.WaitCursor;
            if(this.sData.listMaker("select * from stock"))
            {
                if (!addData(this.sData.Datatables))
                {
                    Cursor.Current = Cursors.Default;
                    alert.Show(this.alert.Warning, "No Data Found........ ");

                }
            }
            else
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show("check your internet connection");
            }
          
            Cursor.Current = Cursors.Default;
        }
    }
}
