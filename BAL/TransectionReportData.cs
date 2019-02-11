using System.Data;
using DAL;
namespace BAL
{
    public class TransectionReportData
    {
        DataTable dt;
        TransectionFromDb tData;
        DataSet ds = null;
        public TransectionReportData()
        {
            this.ds = new DataSet();
            this.dt = new DataTable();
            tData = new TransectionFromDb();
        }
        public DataSet DataS
        {
            set { this.ds = value; }
            get { return this.ds; }
        }
        public DataTable Datatables
        {
            set { this.dt = value; }
            get { return this.dt; }
        }
        public bool listMaker(string sql)
        {
            if (this.tData.result(sql))
            {
                this.Datatables = this.tData.Data;
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


            if (!hasInt)
                sql = string.Format("select * from transection where {0} like '{1}'", dbColumnName, (searchString + "%"));
            else
                sql = string.Format("select * from transection where  {0}={1}", dbColumnName, searchString);



            if (this.tData.result(sql))
            {
                this.Datatables = new DataTable();
                // this.Datatables.Clear();
                this.Datatables = this.tData.Data;
            
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
