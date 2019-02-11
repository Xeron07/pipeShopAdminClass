using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
namespace DAL
{
    public class PipeInfo
    {
        List<string> pipeName;
        List<string> pipeSize;
        List<string> quantity;
        List<string> pricePerunit;
        List<string> totalPrice;
        DbConn conn = null;
        string sql;
        public PipeInfo()
        {
            this.conn = new DbConn();
            this.sql = "select * from stock";
            this.pipeName = new List<string>();
            this.pipeSize = new List<string>();
            this.quantity = new List<string>();
            this.totalPrice = new List<string>();
            this.pricePerunit = new List<string>();

        }
        public List<string> PipeName
        {
            set { this.pipeName = value; }
            get { return this.pipeName; }
        }
                                     
        public List<string>PipeSize {
            set {this.pipeSize=value; }
            get {return this.pipeSize; }
        }
        public List<string>Quantity {
            set {this.quantity=value; }
            get {return this.quantity; }
        }
        public List<string>PricePerUnit {
            set {this.pricePerunit=value; }
            get {return this.pricePerunit; }
        }
        public List<string>TotalPrice {
            set { this.totalPrice = value ; }
            get {return this.totalPrice; }
        }
        public bool GetData()
        {
            try
            {
                if (!this.conn.openConnection())
                    return false;

                MySqlCommand cmd = new MySqlCommand("SELECT DISTINCT pipe_name FROM `stock`", this.conn.Connection);
                MySqlDataReader reader = cmd.ExecuteReader();
            
                while (reader.Read())
                {
                    this.PipeName.Add(reader["pipe_name"].ToString());  
                    
                }
                cmd = new MySqlCommand("SELECT DISTINCT pipe_size FROM `stock`", this.conn.Connection);
                MySqlDataReader xreader = cmd.ExecuteReader();
                while (xreader.Read())
                {
                    this.PipeSize.Add(xreader["pipe_size"].ToString());
                   
                }
                cmd = new MySqlCommand(this.sql, this.conn.Connection);
                MySqlDataReader yreader = cmd.ExecuteReader();

                while (yreader.Read())
                {
                    this.PricePerUnit.Add(yreader["price_per_unit"].ToString());
                    this.Quantity.Add(yreader["quantity"].ToString());
                }
                this.conn.closeConnection();
                return true;
            }
            catch
            {
                return false;
            }
            return false;
        }
    }
}
