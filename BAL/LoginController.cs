using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
namespace BAL
{
    public class LoginController
    {
        string email;
        string password;
        loginData log;
        public LoginController()
        {
            this.email = null;
            this.password = null;
            this.log = new loginData();
        }

        public string Email
        {
            set { this.email = value; }
            get { return this.email; }
        }
        public string Password
        {
            set { this.password = value; }
            get { return this.password; }
        }

        public bool validation()
        {
            if (!Email.Equals("") && !Password.Equals(""))
            {
                this.log.Email = this.Email;
                this.log.Password = this.Password;
                return true;
            }
            return false;
        }

        public bool logUserIn(ref string uname,ref string uid,ref string status)
        {
            if (validation())
            {

                if (this.log.logCheck())
                {
                    uname = this.log.UserName;
                    uid = this.log.UserId;
                    status = this.log.Status;
                    return true;
                }
                
            }
            return false;
        }
    }
}
