using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
namespace BAL
{
    public class StockInfo
    {
        PipeInfo pinfo = null;
        List<string> pipeName;
        List<string> pipeSize;
        List<string> quantity;
        List<string> pricePerunit;
        List<string> totalPrice;
        public StockInfo()
        {
            this.pinfo = new PipeInfo();
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

        public List<string> PipeSize
        {
            set { this.pipeSize = value; }
            get { return this.pipeSize; }
        }
        public List<string> Quantity
        {
            set { this.quantity = value; }
            get { return this.quantity; }
        }
        public List<string> PricePerUnit
        {
            set { this.pricePerunit = value; }
            get { return this.pricePerunit; }
        }
        public List<string> TotalPrice
        {
            set { this.totalPrice = value; }
            get { return this.totalPrice; }
        }


        public bool CheckData()
        {
            if (this.pinfo.GetData())
                return true;
            else
                return false;
        }

        public bool dataAdd()
        {
            if (CheckData())
            {
                this.PipeName = this.pinfo.PipeName;
                this.PipeSize = this.pinfo.PipeSize;
                this.Quantity = this.pinfo.Quantity;
                this.PricePerUnit = this.pinfo.PricePerUnit;
                return true;
            }
            else
                return false;
            
        }

    }
}
