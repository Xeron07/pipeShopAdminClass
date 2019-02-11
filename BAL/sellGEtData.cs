using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
namespace BAL
{
   public class sellGEtData
    {
        sellDb sdb = null;
        List<string> pipeNames;
        List<string> pipeSizes;
        string userName;
        public sellGEtData()
        {
            this.sdb = new sellDb();
            this.pipeNames = null;
            this.pipeSizes = new List<string>();
            this.userName = null;
            concateValues();
        }
        public void concateValues()
        {
            this.PipeNames = this.sdb.PipeNames;
            this.PipeSizes = this.sdb.PipeSizes;
        }
        public string UserName
        {
            set { this.userName = value; }
            get { return this.userName; }
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

        public void getSizes(string name)
        {
            this.sdb.AddPipeSizes(name);
            this.PipeSizes = this.sdb.PipeSizes;
        }
    }
}
