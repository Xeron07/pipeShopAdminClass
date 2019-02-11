using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DAL;
namespace BAL
{
    public class SellReportData
    {
        public string pipeName ;
        public string pipeSize;
        public string quantity;
        public string date ;
        public string billNo ;
        public string tAmount ;
        public string paidAmount ;
        public string dueAmount ;
        public string doneBy;

        DataTable dt;
        SellDataFromDb sellDataFromDb;

        public SellReportData()
        {
            this.pipeName= null;
            this.pipeSize = null;
            this.quantity = null;
            this.date = null;
            this.billNo = null;
            this.tAmount = null;
            this.paidAmount = null;
            this.dueAmount = null;
            this.doneBy = null;
            this.sellDataFromDb = new SellDataFromDb();
            this.dt = null;
        }

        public  string PipeName
        {
            set { this.pipeName = value; }
            get { return this.pipeName; }
        }
        public string Quantity
        {
            set { this.quantity = value; }
            get { return this.quantity; }
        }
        public string PipeSize
        {
            set { this.pipeSize = value; }
            get { return this.pipeSize; }
        }

        public string Date
        {
            set { this.date = value; }
            get { return this.date; }
        }

        public string BillNo
        {
            set { this.billNo = value; }
            get { return this.billNo; }
        }

        public string TotalAmount
        {
            set { this.tAmount = value; }
            get { return this.tAmount; }
        }

        public string PaidAmount
        {
            set { this.paidAmount = value; }
            get { return this.paidAmount; }
        }
        public string DueAmount
        {
            set { this.dueAmount = value; }
            get { return this.dueAmount; }
        }

        public string DoneBy
        {
            set { this.doneBy = value; }
            get { return this.doneBy; }
        }

        public DataTable Datatables
        {
            set { this.dt = value; }
            get { return this.dt; }
        }
        public bool listMaker( string sql)
        {
           if(this.sellDataFromDb.result(sql))
            {
                this.Datatables = this.sellDataFromDb.Data;
                return true;
            }
            return false;
        }
        public bool searchData(string option, string searchString)
        {
            string dbColumnName = null;
            string sql = null;
            bool hasInt = false;
            if (option.Equals("Pipe Name"))
                dbColumnName = "pipe_name";
            else if (option.Equals("Pipe Size"))
                dbColumnName = "pipe_size";
            else if (option.Equals("Date"))
                dbColumnName = "item_date";
            else if (option.Equals("Total Price"))
            {
                hasInt = true;
                dbColumnName = "amount";
            }
            else if (option.Equals("Bill No"))
                dbColumnName = "billno";


            if(!hasInt)
                sql = string.Format("select * from transection where status='{0}' and {1} like '{2}'","sale",dbColumnName, ( searchString + "%"));
            else
                sql = string.Format("select * from transection where status='{0}' and {1}={2}", "sale", dbColumnName, searchString);

            

            if (this.sellDataFromDb.result(sql))
            {
                this.Datatables = new DataTable();
               // this.Datatables.Clear();
                this.Datatables = this.sellDataFromDb.Data;
                return true;
            }
            return false;
        }

        public int sortingIndex(string s)
        {
            if (s.Equals("Date"))
                return 0;
            else if (s.Equals("Bill No"))
                return 1;
            else if (s.Equals("Pipe Name"))
                return 2;
            else if (s.Equals("Pipe Size"))
                return 3;
            else if (s.Equals("Quantity"))
                return 4;
            else if (s.Equals("Total Amount"))
                return 5;
            else if (s.Equals("Paid Amount"))
                return 6;
            else if (s.Equals("Due Amount"))
                return 7;
            else if (s.Equals("Done by"))
                return 8;
            return 0;
        }
      
    }
}
