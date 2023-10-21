using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace NTB3
{
    partial class AboutBox1 : Form
    {
        public AboutBox1()
        {
            InitializeComponent();
           




        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void AboutBox1_Load(object sender, EventArgs e)
        {
       
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Font fuente = new Font("Verdana", 8);
            SolidBrush brocha = new SolidBrush(Color.Black);
            e.Graphics.DrawString("NTB 2.0 by VampiroX", fuente, brocha, 267, 145);
        }

        private void AboutBox1_Leave(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void AboutBox1_Deactivate(object sender, EventArgs e)
        {
            
            this.Dispose();

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {

                System.Diagnostics.Process.Start(linkLabel1.Text);
            }

            catch
            {

            }

        }

       
      

       

    }
}
