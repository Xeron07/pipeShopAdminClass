using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
namespace BAL
{
    public class purcahseDataAdder
    {
        DbConn conn;
        public purcahseDataAdder()
        {
            this.conn = new DbConn();
        }

        public bool addToDb(string sql)
        {
            if (this.conn.insertData(sql))
                return true;
            return false;
        }

        public bool CheckStock(string pname,string psize)
        {
            string sql = String.Format("select * from stock where pipe_name='{0}' and pipe_size='{1}'",pname,psize);
            if (this.conn.checkRow(sql))
            {
               
                return true;
            }
            return false;
        }
    }
}
