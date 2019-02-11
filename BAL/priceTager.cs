using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
namespace BAL
{
   public class priceTager
    {
        priceCollector pc;
        public priceTager()
        {
            this.pc = new priceCollector();
        }
        //properties
        public string UnitPrice { get; set; }
        public string Quantity { get; set; }

        public bool getPrice(string pipename,string pipesize)
        {
            this.pc.unitPrice(pipename, pipesize);
            if(!String.IsNullOrEmpty(this.pc.Quantity)&&!String.IsNullOrEmpty(this.pc.PricePerUnit))
            {
                this.UnitPrice = this.pc.PricePerUnit;
                this.Quantity = this.pc.Quantity;
                return true;
            }
            return false;
        }

    }
}
