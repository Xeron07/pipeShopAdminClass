﻿using System;
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
    public partial class loading : Form
    {
        public loading(string name)
        {
            
            InitializeComponent();
            label2.Text = name+"!!!";
        }
    }
}
