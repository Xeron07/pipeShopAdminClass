using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
namespace BAL
{
   public class addData
    {
        AddToStock aTs;
        public addData()
        {
            aTs = new AddToStock();
        }
        public bool dataAddToDb(List<string> data)
        {
            if(this.aTs.AddinTransection(data))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


    }
}
