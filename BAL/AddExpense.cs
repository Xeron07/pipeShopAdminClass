using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
namespace BAL
{
    public class AddExpense
    {
        AddExpenseToDb ed;
        DbConn conn;
        public AddExpense()
        {
            this.ed = new AddExpenseToDb();
            conn = new DbConn();
        }
        public bool values(string t1,string t2,string uname)
        {
            string sql = "INSERT INTO `expense`(`expense_date`, `expense_details`, `amount`,`reported_by`) VALUES (\'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\',\'" + t1 + "\'," + t2+ ",\'"+uname+"\')";
            if (conn.insertData(sql))
            {
                return true;
            }


            return false;
        }

        public bool registerUser(string sql)
        {
            if (conn.insertData(sql))
                return true;
            return false;
        }
    }
}
