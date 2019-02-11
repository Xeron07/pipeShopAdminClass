using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
namespace DAL
{
    public class loginData
    {
        DbConn conn;
        string email;
        string pass;
        string uname;
        string uid;
        string status;
        MySqlDataReader reader;
        public loginData()
        {
            this.conn = new DbConn();
            this.email = null;
            this.pass = null;
        }
        public loginData( string email, string pass)
        {
            this.conn = new DbConn();
            this.email = email;
            this.pass = pass;
        }

        //properties
        public string Email
        {
            set { this.email = value; }
            get { return this.email; }
        }
        public string Password
        {
            set { this.pass = value; }
            get { return this.pass; }
        }
        public string UserName
        {
            set { this.uname = value; }
            get { return this.uname; }
        }
        public string UserId
        {
            set { this.uid = value; }
            get { return this.uid; }
        }
        public string Status
        {
            set { this.status = value; }
            get { return this.status; }
        }

        public bool logCheck()
        {
            string sql = string.Format("select * from users where email='{0}' and password='{1}'",this.Email,this.Password);
            if (this.conn == null)
                return false;
            if(this.conn.openConnection())
            {
                MySqlCommand cmd = new MySqlCommand(sql, this.conn.Connection);
                 if(this.reader != null && !this.reader.IsClosed)
                    this.reader.Close();
                 this.reader = cmd.ExecuteReader();
                if(reader.HasRows)
                {
                    while (reader.Read())
                    {
                        this.UserName = reader["name"].ToString();
                        this.UserId = reader["uid"].ToString();
                        this.Status = reader["status"].ToString();
                    }
                    //closing reader...
                    reader.Close();
                    this.conn.closeConnection();
                    return true;
                }
            }
            return false;
        }
    }
}
