using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace DAL
{
   public class sellDb
    {
        DbConn conn = null;
        MySqlDataReader reader;
        List<string> pipeNames;
        List<string> pipeSizes;
        public sellDb()
        {
            this.conn = new DbConn();
            this.pipeNames = null;
            this.pipeSizes = null;
            this.reader = null;
            AddPipeNames();
            AddPipeSizes();

        }
        public List<string> PipeNames
        {
            set { this.pipeNames = value; }
            get { return this.pipeNames; }
        }
        public List<string> PipeSizes
        {
            set { this.pipeSizes = value; }
            get { return this.pipeSizes; }
        }

        public void AddPipeNames()
        {
            string sql = "select distinct pipe_name from stock";
            List<string> pn = new List<string>();
            if (this.conn.openConnection())
            {
                MySqlCommand cmd = new MySqlCommand(sql, this.conn.Connection);

                if (this.reader != null && !reader.IsClosed)
                    reader.Close();
                this.reader = cmd.ExecuteReader();
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        pn.Add(reader["pipe_name"].ToString());
                    }
                    this.conn.closeConnection();
                   

                }
                else
                {
                    pn.Add("no data found");
                }
            }
            else
            {
                pn.Add("no data found");
            }
            this.PipeNames = pn;
        }
        public void  AddPipeSizes()
        {
            string sql = "select distinct pipe_size from stock";
            List<string> pn = new List<string>();
            if (this.conn.openConnection())
            {
                MySqlCommand cmd = new MySqlCommand(sql, this.conn.Connection);

                if (this.reader != null && !reader.IsClosed)
                    reader.Close();
                this.reader = cmd.ExecuteReader();
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        pn.Add(reader["pipe_size"].ToString());
                    }
                    this.conn.closeConnection();
                  
                }
                else
                {
                    pn.Add("no data found");
                }
            }
            else
            {
                pn.Add("no data found");
            }
            this.PipeSizes = pn;

        }

        public void AddPipeSizes(string pname)
        {
            string sql = string.Format("select distinct pipe_size from stock where pipe_name='{0}'",pname);
            List<string> pn = new List<string>();
            if (this.conn.openConnection())
            {
                MySqlCommand cmd = new MySqlCommand(sql, this.conn.Connection);

                if (this.reader != null && !reader.IsClosed)
                    reader.Close();
                this.reader = cmd.ExecuteReader();
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        pn.Add(reader["pipe_size"].ToString());
                    }
                    this.conn.closeConnection();
                   
                }
                else
                {
                    pn.Add("no data found");
                }
            }
            else
            {
                pn.Add("no data found");
            }
            this.PipeSizes = pn;
        }
    }
}
