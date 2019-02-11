using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace DAL
{
    public class DbConn
    {
        private string connectionString = null;
        public MySqlConnection connectione;

        //constructors...........
        public DbConn()
        {
            //online db
            //this.connectionString= "server = mysql6001.site4now.net; user id = a43e39_pipe; database = db_a43e39_pipe; password = 1@afsarnsons";

            //local db
            this.connectionString = "server = mysql6001.site4now.net; user id = a43e39_pipe; database = db_a43e39_pipe; password = afsarnsons2019";
            this.connectione = new MySqlConnection(this.connectionString);
        }
        public DbConn(string connectionString)
        {
            this.connectionString = connectionString;
            this.connectione = new MySqlConnection(this.connectionString);
        }

        //properties............
        public MySqlConnection Connection
        {
            get { return this.connectione; }
        }

        //connection open method...
        public bool openConnection()
        {
            if (Connection.State == System.Data.ConnectionState.Closed)
            {
                try
                {
                    Connection.Open();
                }
                catch
                {
                    return false;
                }
            }
           return true;
        }

        //connection close method
        public bool closeConnection()
        {
            if (Connection.State != System.Data.ConnectionState.Closed)
            {
                try
                {
                    Connection.Close();
                }
                catch
                {
                    return false;
                }
            }
            return true;
        }
        public bool insertData(string sql)
        {
            if (openConnection())
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand(sql, Connection);
                    MySqlDataReader rea = cmd.ExecuteReader();
                    if (closeConnection())
                        return true;
                    else
                        return false;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
            return false;
        }

        public int getQuantity(string sql)
        {
            int quantity = 0;
            MySqlCommand cmd = new MySqlCommand(sql, Connection);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                quantity = Int32.Parse(reader["quantity"].ToString());
            }
            return quantity;
        }
        public bool Update(string sql)
        {
            if (insertData(sql))
                return true;
            return false;
        }

        public bool checkRow(string sql)
        {
            try
            {
                if(this.openConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(sql, Connection);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    if(reader.HasRows)
                    {
                        this.closeConnection();
                        return true;
                    }

                    this.closeConnection();
                }
            }
            catch
            {
                return false;
            }
            return false;
        }

    }
}
