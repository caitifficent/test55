﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NTB
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }

        private void Form2_Activated(object sender, EventArgs e)
        {
            Application.DoEvents();
        }

        private void Form2_MouseMove(object sender, MouseEventArgs e)
        {
            this.Update();
            Application.DoEvents();
        }
        


    }
}
