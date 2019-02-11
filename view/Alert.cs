using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using System.Windows.Forms;


namespace view
{
    public partial class Alert : Form
    {
        Image success = null;
        Image warning = null;
        Image error = null;
        Image info = null;
        public Alert()
        {
            InitializeComponent();
            this.warning = Properties.Resource1.warning;
            this.info = Properties.Resource1.info;
            this.success = Properties.Resource1.success;
            this.error = Properties.Resource1.error;
        }

        protected override CreateParams CreateParams
        {
            get
            {
                const int CS_DROPSHADOW = 0x30000;
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= CS_DROPSHADOW;
                return cp;
            }

        }


        public Image Warning
        {
            get { return this.warning; }
        }

        public Image Success
        {
            get { return this.success; }
        }

        public Image Error
        {
            get { return this.error; }
        }

        public Image Info
        {
            get { return this.info; }
        }

       public void Show(Image name, string message)
        {
            SoundPlayer player = new SoundPlayer();
            this.icon.Image = name;
            this.message.Text = message;
            this.Show();
            if (name.Equals(Success))
                player.Stream = Properties.Resource1.successS;
            else if (name.Equals(Warning))
                player.Stream = Properties.Resource1.warningS;
            else if (name.Equals(Error))
                player.Stream = Properties.Resource1.errorS;
            else if (name.Equals(Info))
                player.Stream = Properties.Resource1.infoS;
            player.Play();

        }

        private void close(object sender, EventArgs e)
        {
            this.Hide();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
