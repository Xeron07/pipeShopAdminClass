using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace BAL
{
   public class StockDataTable
    {
        DataTable stockData = null;
        DataTable stockData2 = null;
        public StockDataTable()
        {
            this.stockData = new DataTable();
            this.stockData2 = new DataTable();
            addColums();
            addColums2();
        }
        //properties
        public DataTable StockData
        {
            set { this.stockData = value; }
            get { return this.stockData; }
        }

        public DataTable StockData2
        {
            set { this.stockData2 = value; }
            get { return this.stockData2; }
        }



        //Methods
        public void addColums()
        {
            StockData.Columns.Add("Date");
            StockData.Columns.Add("Bill_No");
            StockData.Columns.Add("Pipe_Name");
            StockData.Columns.Add("Pipe_Size");
            StockData.Columns.Add("Quantity");
            StockData.Columns.Add("Total_Amount");
            StockData.Columns.Add("Due_Amount");
            StockData.Columns.Add("Paid_Amount");
            StockData.Columns.Add("Done_By");
            StockData.Columns.Add("Status");
        }

        public void addColums2()
        {
            StockData2.Columns.Add("Pipe_Name");
            StockData2.Columns.Add("Pipe_Size");
            StockData2.Columns.Add("Quantity");
            StockData2.Columns.Add("Unit_Price");
        }


    }
}
