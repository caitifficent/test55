using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace NTB
{
               


    public partial class Form3 : Form
    {
           private static Socket socketrcon5;
           NTB3.Form1 dd = new NTB3.Form1();

        public Form3()
        {
          
            InitializeComponent();
          
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (textBox1.Text == "")
            {
                button2_Click(null, null);
                return;
            }



           
            sendrcon(dd.rcon.Text, "say '" + textBox1.Text + "'", dd.serverip.Text, Convert.ToInt32(dd.serverport.Text), "", "");
            textBox1.Clear();

            this.Close();
            this.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            this.Close();
            this.Dispose();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
         
           
        }
        private byte[] StringToByteArray(string output)
        {
            byte[] array = new byte[output.Length];

            int i = 0;

            foreach (char c in output.ToCharArray())
            {
                array[i] = (byte)c;
                i++;
            }

            return array;


        }
        void sendrcon(string psw, string command, string address, int port, string playername, string playerclan)
        {

            try
            {
                socketrcon5.Close();
            }

            catch
            {

            }

            try
            {

                
                command = command.Replace("^;", "^p");

                    IPAddress ip = IPAddress.Parse(address);
                    IPEndPoint iep5 = new IPEndPoint(IPAddress.Any, Convert.ToInt32(dd.textBox55.Text));
                    IPEndPoint endpoint5 = new IPEndPoint(ip, port);
                    socketrcon5 = new Socket(endpoint5.AddressFamily, SocketType.Dgram, ProtocolType.Udp);
                    socketrcon5.SendTimeout = Convert.ToInt32(dd.sendtimeout.Text);
                    socketrcon5.ReceiveTimeout = Convert.ToInt32(dd.responsetimeout.Text);
                    socketrcon5.Bind(iep5);
                    socketrcon5.Connect(endpoint5);



                    //                    byte[] output = StringToByteArray("\xFF\xFFRcon\xFF" + psw + "\xFF" + " " + command + "\x00\x00");
                    byte[] output = StringToByteArray("\xFF\xFFRcon\xFF" + psw + "\xFF" + command + "\x00\x00");


                    socketrcon5.Send(output, output.Length, 0);
                    Application.DoEvents();

                    

                    
                    socketrcon5.Close();
                    Application.DoEvents();


                    output = null;
                    ip = null;
                    endpoint5 = null;

                
                command = null;
                psw = null;
                playername = null;
                address = null;




            }

            catch 
            {


                command = null;
                psw = null;
                playername = null;
                address = null;
   

                try
                {
                    socketrcon5.Close();
                }
                catch
                {
                }
            }


        }
       
      
    }
}
