using System.Data;
using MySql.Data.MySqlClient;
namespace DAL
{
    public class TransectionFromDb
    {
        DbConn conn;
        DataTable dt;
        public TransectionFromDb()
        {
            this.conn = new DbConn();
            this.dt = new DataTable();
        }
        public DataTable Data
        {
            set { this.dt = value; }
            get { return this.dt; }
        }
        public bool result(string sql)
        {
            if (this.conn.openConnection())
            {
                MySqlCommand cmd = new MySqlCommand(sql, this.conn.Connection);
                cmd.ExecuteNonQuery();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                this.Data = new DataTable();
                da.Fill(this.Data);
                this.conn.closeConnection();
                return true;

            }
            return false;

        }
    }
}
