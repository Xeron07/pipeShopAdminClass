using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
namespace DAL
{
    public class AddToStock
    {
        DbConn conn;
        List<string>data;
        public AddToStock()
        {
            this.conn = new DbConn();
        }
        public bool AddinTransection(List<string>data)
        {
            this.data = data;
            string sql1, sql2;
            //            long no = Int64.Parse(billNo);

            sql1 = "INSERT INTO `customer`(`name`, `phone_no`, `email`, `due_amount`, `c_billno`) VALUES (\'" + data[5] + "\',\'" + data[6] + "\',\'" + data[7] + "\',\'" + data[9] + "\',\'" + data[13] + "\')";
            sql2 = string.Format("INSERT INTO `transection`(`billNo`,`item_date`, `pipe_name`, `pipe_size`, `quantity`, `amount` ,`paid_amount`,`due_amount`,`done_by`, `status`) VALUES ('{0}','{1}',\'" + data[0] + "\',\'" + data[1] + "\'," + data[2] + "," + data[4] + ",{2},{3},'{4}',\'sale\')", data[13], DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),data[8],data[9],data[11]);

            if (conn.insertData(sql2))
            {

                if (conn.insertData(sql1))
                {

                    if (!UpdateQuantity())
                        return false;

                    // pdfViewer1.openFile();
                    // pdfViewer1.Show();
                    // pdfViewer1.BringToFront();
                    return true;

                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

        }
        public bool UpdateQuantity()
        {
            string sql2 = "select quantity from stock where pipe_name=\"" + data[0] + "\" and pipe_size=\"" + data[1] + "\";";
            string usql;
            int pQuantity = 0;
            if (conn.openConnection())
            {
                pQuantity = conn.getQuantity(sql2);
                if (conn.closeConnection())
                {

                    usql = "update stock set quantity=" + (pQuantity - Int32.Parse(data[2])) + " where pipe_name=\"" + data[0] + "\" and pipe_size=\"" + data[1] + "\"; ";
                    if (conn.Update(usql))
                    {
                        return true;

                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
