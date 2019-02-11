using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace view
{
    public partial class Opening : Form
    {
        Timer t1 = null;
        public Opening()
        {
            t1 = new Timer();
            InitializeComponent();
        }

        private void Opening_Load(object sender, EventArgs e)
        {
            Opacity = 0;      //first the opacity is 0

            t1.Interval = 20;  //we'll increase the opacity every 20ms
            t1.Tick += new EventHandler(fadeIn);  //this calls the function that changes opacity 
            t1.Start();
        }

        void fadeIn(object sender, EventArgs e)
        {
            if (Opacity >= 1)
            {
                System.Threading.Thread.Sleep(2000);
                fadeOutController(sender, e);
            }
            else
                Opacity += 0.05;
        }

        void fadeOutController(object sender, EventArgs e)
        {
            t1.Tick += new EventHandler(fadeOut);  //this calls the fade out function
            t1.Start();

            if (Opacity == 0)  //if the form is completly transparent
            {
                Login st = new Login();
                st.Show();
                this.Hide();
            }


        }

        void fadeOut(object sender, EventArgs e)
        {
            if (Opacity <= 0)     //check if opacity is 0
            {
                t1.Stop();    //if it is, we stop the timer

                Login st = new Login();
                st.Show();
                this.Hide();

            }
            else
                Opacity -= 0.05;
        }


    }
}
