using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
namespace DAL
{
   public class priceCollector
    {
        DbConn conn;
        MySqlDataReader reader;
        public priceCollector()
        {
            this.conn = new DbConn();

        }
        //properties
        public string PricePerUnit { get; set; }
        public string Quantity { get; set; }
        public void unitPrice(string pipename,string pipesize)
        {
            string sql =String.Format("select * from stock where pipe_name='{0}' and pipe_size='{1}'",pipename,pipesize);
            if(this.conn!=null)
            {
                if(this.conn.openConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(sql, this.conn.Connection);
                    if (this.reader != null && !this.reader.IsClosed)
                        this.reader.Close();
                    this.reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            this.PricePerUnit = reader["price_per_unit"].ToString();
                            this.Quantity = reader["quantity"].ToString();
                        }
                        //closing reader...
                        reader.Close();
                        this.conn.closeConnection();
                    }
                    }
            }
        }
    }
}
