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
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.IO;
using System.Security.Cryptography;
using Etier.IconHelper;
using CountryLookupProj;
using System.Collections;
using NTB;
using Tray_minimizer;



namespace NTB3
{




    public partial class Form1 : Form
    {
       
        




        
        [DllImport("kernel32.dll", EntryPoint = "SetProcessWorkingSetSize",
        ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        private static extern int SetProcessWorkingSetSize(
        IntPtr process, int minimumWorkingSetSize, int maximumWorkingSetSize);
        private ImageList _smallImageList = new ImageList();
        private ImageList _largeImageList = new ImageList();
        private IconListManager _iconListManager;





        List<window> windows = new List<window>();
        NTB.Properties.Settings set = new NTB.Properties.Settings();
        // bool isinstartup = false;
        [DllImport("user32.dll")]

        public static extern bool LockWindowUpdate(IntPtr hWndLock);



        List<ListViewItem> carlitos = new List<ListViewItem>();


        Hashtable pablo = new Hashtable();
        Hashtable pabloguid = new Hashtable();
        TyronM.MinTrayBtn mybutton; //tray button
        decimal tryagain = 0;
        string kickedplayers = "";
        int headersize = 33;

        private static Socket socket;
        private static Socket socketrcon;
        private static Socket socketrcon2;
        private static Socket socketrcon3;

        [DllImport("Kernel32.dll")]
        public static extern bool Beep(UInt32 frequency, UInt32 duration);
        private ListViewColumnSorter lvwColumnSorter;
        private ListViewColumnSorter lvwColumnSorter2; 
        private ListViewColumnSorter lvwColumnSorter3;
        private ListViewColumnSorter lvwColumnSorter4;
        private ListViewColumnSorter lvwColumnSorter5;

        int monihe;
        int moniwi;
        int gamehe;
        int gamewi;
        uint hidemod;
        uint hidekey;
        uint rmod1;
        uint rhidekey1;
        uint rmod2;
        uint rhidekey2;
        bool ignoretitle;
        uint rhidekey3;
        uint rmod3;
        public bool Ignoretitle
        {
            get { return ignoretitle; }
            set { ignoretitle = value; }
        }


        public Form1()
        {       
            

        
            InitializeComponent();
          
            CheckForIllegalCrossThreadCalls = false;

            lvwColumnSorter = new ListViewColumnSorter();
            lvwColumnSorter2 = new ListViewColumnSorter();
            lvwColumnSorter3 = new ListViewColumnSorter();
            lvwColumnSorter4 = new ListViewColumnSorter();
            lvwColumnSorter5 = new ListViewColumnSorter();

            for (int i = 65; i < 91; i++)
            {
                hidekeycombo.Items.Add((char)i);
            }
            for (int i = 65; i < 91; i++)
            {
              comboBox13.Items.Add((char)i);
            }
            for (int i = 65; i < 91; i++)
            {
                comboBox14.Items.Add((char)i);
            }
            for (int i = 65; i < 91; i++)
            {
                comboBox15.Items.Add((char)i);
            }
            hidekeycombo.Text = "Z";
            comboBox13.Text = "V";
            comboBox14.Text = "S";
            comboBox15.Text = "T";
            comboBox16.Text = "Name"; 
            checkBox22.Tag = "";
            label71.Tag = "0";
            label73.Tag = "0"; 

            this.pbh.ListViewItemSorter = lvwColumnSorter;
            this.pbnow.ListViewItemSorter = lvwColumnSorter2;
            this.banlist.ListViewItemSorter = lvwColumnSorter3;
            this.listViewFind1.ListViewItemSorter = lvwColumnSorter4;
            this.listViewFind3.ListViewItemSorter = lvwColumnSorter5;
            System.Globalization.CultureInfo ci = System.Globalization.CultureInfo.CurrentCulture;
            co.Text = ci.NumberFormat.NumberDecimalSeparator;
            pu.Text = ci.NumberFormat.NumberGroupSeparator;
            
            
            mybutton = new TyronM.MinTrayBtn(this);
            mybutton.MinTrayBtnClicked +=
            new TyronM.MinTrayBtnClickedEventHandler(TrayBtn_clicked);
            CheckForIllegalCrossThreadCalls = false;
            toolStripComboBox1.Text = "30";
            comboBox12.Text = "Pasive";
            comboBox7.Text = "Base";
            comboBox10.Text = "GUID";
            comboBox11.Text = "Name";
            //iba aca
            _iconListManager = new IconListManager(_smallImageList, _largeImageList);
            banlist.SmallImageList = _smallImageList;
            banlist.LargeImageList = _largeImageList;
            pbnow.SmallImageList = _smallImageList;
            pbnow.LargeImageList = _largeImageList;
            pbh.SmallImageList = _smallImageList;
            pbh.LargeImageList = _largeImageList;
            listViewFind3.SmallImageList = _smallImageList;
            listViewFind3.LargeImageList = _largeImageList;
            listViewFind2.SmallImageList = _smallImageList;
            listViewFind2.LargeImageList = _largeImageList;
            loadcolors();
            loadpb();
            loadsettings();
            Application.DoEvents();
            loadpersonalsettings();
            Application.DoEvents();

            if (checkBox1.Checked == true)
            {

                button3.Enabled = false;
                button4.Enabled = false;
                button5.Enabled = false;
                button8.Enabled = false;
                button19.Enabled = false;
                button20.Enabled = false;

            }



            updateftpdata();




        }
        protected override void WndProc(ref Message m)
        {
            try
            {
                switch (m.Msg)
                {
                    case winapi.WM_HOTKEY:
                        ProcessHotkey(m.WParam);
                        break;
                }
                base.WndProc(ref m);
            }
            catch
            {
            }
        }
        private void tacho()
        {
            getnewhotkey();

            getwindows();

            for (int i = 0; i < windows.Count - 1; i++)
            {
                if (windows[i].title == "ETQW")
                {
                    processwindow(windows[i]);
                }
            }

            windows.Clear();

        }
        private Icon Iconfrompath(string path)
        {
            System.Drawing.Icon icon = null;

            if (System.IO.File.Exists(path))
            {
                winapi.SHFILEINFO info = new winapi.SHFILEINFO();
                winapi.SHGetFileInfo(path, 0, ref info, (uint)Marshal.SizeOf(info), winapi.SHGFI_ICON | winapi.SHGFI_SMALLICON);

                System.Drawing.Icon temp = System.Drawing.Icon.FromHandle(info.hIcon);
                icon = (System.Drawing.Icon)temp.Clone();
                winapi.DestroyIcon(temp.Handle);
            }

            return icon;
        }
        private string pathfromhwnd(IntPtr hwnd)
        {
            uint dwProcessId;
            winapi.GetWindowThreadProcessId(hwnd, out dwProcessId);
            IntPtr hProcess = winapi.OpenProcess(winapi.ProcessAccessFlags.VMRead | winapi.ProcessAccessFlags.QueryInformation, false, dwProcessId);
            StringBuilder path = new StringBuilder(1024);
            winapi.GetModuleFileNameEx(hProcess, IntPtr.Zero, path, 1024);
            winapi.CloseHandle(hProcess);
            return path.ToString();
        }
        private void processwindow(window wnd)
        {
            string path = pathfromhwnd(wnd.handle);
            System.Drawing.Icon icon = Iconfrompath(path);

            NotifyIcon tray = new NotifyIcon(this.components);
            //  tray.Icon = icon == null ? Properties.Resources.exeicon : icon;
            tray.Visible = true;
            tray.Tag = wnd;
            tray.Text = wnd.title.Length > 64 ? wnd.title.Substring(0, 63) : wnd.title;
            tray.Click += new EventHandler(tray_Click);

            showwindow(wnd, true);
        }
        void tray_Click(object sender, EventArgs e)
        {
            NotifyIcon tray = sender as NotifyIcon;
            window wnd = tray.Tag as window;
            if (winapi.IsWindow(wnd.handle))
            {
                showwindow(wnd, false);
            }
            else
                MessageBox.Show("Window does not exist");
            tray.Click -= new EventHandler(tray_Click);
            tray.Dispose();
            checkBox22.Tag = "";
        }
        private void showwindow(window wnd, bool hide)
        {
            if (hide)
            {

                //             Resolution.CResolution ChangeRes600 = new Resolution.CResolution(800, 600);

                gamehe = Screen.PrimaryScreen.Bounds.Height;
                gamewi = Screen.PrimaryScreen.Bounds.Width;


                winapi.ShowWindow(wnd.handle, winapi.SW_MINIMIZE);

                winapi.ShowWindow(wnd.handle, winapi.SW_HIDE);
                Resolution.CResolution ChangeRes800 = new Resolution.CResolution(moniwi, monihe);


            }
            else
            {

                winapi.ShowWindow(wnd.handle, winapi.SW_MAXIMIZE);

                winapi.ShowWindow(wnd.handle, winapi.SW_SHOW);
                //  Resolution.CResolution ChangeRes600 = new Resolution.CResolution(gamewi,gamehe );

            }
        }

        private int state(window wd, bool hide)
        {
            if (hide)
            {
                return winapi.SW_HIDE;
            }

            if (wd.isminimzed)
            {
                return winapi.SW_MINIMIZE;
            }

            if (wd.ismaximized)
            {
                return winapi.SW_MAXIMIZE;
            }
            return winapi.SW_SHOW;

        }
        private void getwindows()
        {
            winapi.EnumWindowsProc callback = new winapi.EnumWindowsProc(enumwindows);
            winapi.EnumWindows(callback, 0);
        }

        private bool enumwindows(IntPtr hWnd, int lParam)
        {
            if (!winapi.IsWindowVisible(hWnd))
                return true;

            StringBuilder title = new StringBuilder(256);
            winapi.GetWindowText(hWnd, title, 256);

            if (string.IsNullOrEmpty(title.ToString()) && set.IgnoreTitle)
            {
                return true;
            }

            if (title.Length != 0 || (title.Length == 0 & hWnd != winapi.statusbar))
            {
                windows.Add(new window(hWnd, title.ToString(), winapi.IsIconic(hWnd), winapi.IsZoomed(hWnd)));
            }

            return true;
        }
        private void ProcessHotkey(IntPtr wparam)
        {
            if (wparam.ToInt32() == 1729)
            {
                if (checkBox22.Tag.ToString() == "")
                {



                    tacho() ;
                    checkBox22.Tag = "pablo";
                    this.Focus();
                }
                else
                {
                    aClick(null, null);
                    checkBox22.Tag = "";
                }


            }
            else if (wparam.ToInt32() == 1730)
            {

                if (label71.Tag == "0")
                {
                    label71.Tag = "1";
                    string[] arr = textBox35.Text.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (string q in arr)
                    {
                        sendrcon(rcon.Text, q, serverip.Text, Convert.ToInt32(serverport.Text), "", "");
                   
                    }
                }
                else
                {
                    label71.Tag = "0";
                    string[] arr = textBox37.Text.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (string q in arr)
                    {
                        sendrcon(rcon.Text, q, serverip.Text, Convert.ToInt32(serverport.Text), "", "");

                    }
                }
 
            }
            else if (wparam.ToInt32() == 1731)
            {
                if (label73.Tag == "0")
                {
                    label73.Tag = "1";
                    string[] arr = textBox38.Text.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (string q in arr)
                    {
                        sendrcon(rcon.Text, q, serverip.Text, Convert.ToInt32(serverport.Text), "", "");

                    }
                }
                else
                {
                    label73.Tag = "0";
                    string[] arr = textBox36.Text.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (string q in arr)
                    {
                        sendrcon(rcon.Text, q, serverip.Text, Convert.ToInt32(serverport.Text), "", "");

                    }
                }
            }
            else if (wparam.ToInt32() == 1732)
            {
                Form3 form4 = new Form3();
            
                form4.Show();
                form4.Activate();
 
            }

         


        }
        private void aClick(object sender, EventArgs e)
        {
            showall();
        }
        private void getnewhotkey()
        {

            hidemod = 0;
            rmod1 = 0;
            rmod2 = 0;
            rmod3 = 0;
            if (this.hideshiftcheck.Checked)
            {
                hidemod = hidemod | (uint)winapi.KeyModifiers.Shift;
            }
            if (this.hidectrlcheck.Checked)
            {
                hidemod = hidemod | (uint)winapi.KeyModifiers.Control;
            }
            if (this.hidealtcheck.Checked)
            {
                hidemod = hidemod | (uint)winapi.KeyModifiers.Alt;
            }


            hidekey = (uint)this.hidekeycombo.SelectedIndex;



            if (this.checkBox26.Checked)
            {
                rmod1 = rmod1 | (uint)winapi.KeyModifiers.Shift;
            }
            if (this.checkBox28.Checked)
            {
                rmod1 = rmod1 | (uint)winapi.KeyModifiers.Control;
            }
            if (this.checkBox27.Checked)
            {
                rmod1 = rmod1 | (uint)winapi.KeyModifiers.Alt;
            }


            rhidekey1 = (uint)this.comboBox13.SelectedIndex;

            if (this.checkBox30.Checked)
            {
                rmod2 = rmod2 | (uint)winapi.KeyModifiers.Shift;
            }
            if (this.checkBox43.Checked)
            {
                rmod2 = rmod2 | (uint)winapi.KeyModifiers.Control;
            }
            if (this.checkBox36.Checked)
            {
                rmod2 = rmod2 | (uint)winapi.KeyModifiers.Alt;
            }


            rhidekey2 = (uint)this.comboBox14.SelectedIndex;

            if (this.checkBox45.Checked)
            {
                rmod3 = rmod3 | (uint)winapi.KeyModifiers.Shift;
            }
            if (this.checkBox47.Checked)
            {
                rmod3 = rmod3 | (uint)winapi.KeyModifiers.Control;
            }
            if (this.checkBox46.Checked)
            {
                rmod3 = rmod3 | (uint)winapi.KeyModifiers.Alt;
            }


            rhidekey3 = (uint)this.comboBox15.SelectedIndex;

            if (checkBox24.Checked)
            {
                winapi.RegisterHotKey(this.Handle, 1730, rmod1, 64 + rhidekey1);
            }
            if (checkBox23.Checked)
            {
                winapi.RegisterHotKey(this.Handle, 1729, hidemod, 64 + hidekey);
            }
            if (checkBox29.Checked)
            {
                winapi.RegisterHotKey(this.Handle, 1731, rmod2, 64 + rhidekey2);
            }
            if (checkBox44.Checked)
            {
                winapi.RegisterHotKey(this.Handle, 1732, rmod3, 64 + rhidekey3);
            }
        }

        void tryloging()
        {
            timer3.Enabled = true; 
            if (checkBox33.Checked == true) { button5_Click(null, null); }
            button3_Click(null, null);


        }
        private void downloadtomainfolder(string[] whatfiles)
        {

            Cursor = Cursors.WaitCursor;

            foreach (string q in whatfiles)
            {
                try
                {
                    if (!ftpConnection1.IsConnected) { ftpConnection1.Connect(); }

                    if (ftpConnection1.ServerDirectory != ftpfolder.Text)
                    {
                        ftpConnection1.ChangeWorkingDirectory(ftpfolder.Text);
                    }





                    ftpConnection1.DownloadFile(Application.StartupPath, q);
                    if (checkBox35.Checked) { log.AppendText(DateTime.Now.ToLocalTime() + "\t" + "Downloading: " + q + "\r\n"); }

                }

                catch
                {

                    try
                    {

                        if (checkBox35.Checked) { log.AppendText(DateTime.Now.ToLocalTime() + "\t" + "Reconnecting..." + "\r\n"); }
                        if (!ftpConnection1.IsConnected) { ftpConnection1.Connect(); }
                        if (ftpConnection1.ServerDirectory != ftpfolder.Text) { ftpConnection1.ChangeWorkingDirectory(ftpfolder.Text); }
                        ftpConnection1.DownloadFile(Application.StartupPath, q);
                        if (checkBox35.Checked) { log.AppendText(DateTime.Now.ToLocalTime() + "\t" + "Downloading: " + q + "\r\n"); }
                    }

                    catch
                    {
                        if (checkBox35.Checked) { log.AppendText(DateTime.Now.ToLocalTime() + "\t" + "Reconnecting Failed" + "\r\n"); }
                        try
                        {
                            ftpConnection1.Close(true);
                        }
                        catch
                        {
                            if (checkBox35.Checked) { log.AppendText(DateTime.Now.ToLocalTime() + "\t" + "Error in FTP" + "\r\n"); }

                        }
                    }



                }





            }


            Cursor = Cursors.Default;




        }
        private bool checkftpfilemain(string whatfiles)
        {

            Cursor = Cursors.WaitCursor;

            try
            {
                if (!ftpConnection1.IsConnected) { ftpConnection1.Connect(); }

                if (ftpConnection1.ServerDirectory != ftpfolder.Text)
                {
                    ftpConnection1.ChangeWorkingDirectory(ftpfolder.Text);
                }




                Cursor = Cursors.Default;
                //          return ftpConnection1.Exists("ntb2settings.ini");
                return ftpConnection1.Exists(whatfiles);
                //        if (checkBox35.Checked) { log.AppendText(DateTime.Now.ToLocalTime() + "\t" + "Downloading: " + q + "\r\n"); }

            }

            catch
            {

                try
                {

                    //       if (checkBox35.Checked) { log.AppendText(DateTime.Now.ToLocalTime() + "\t" + "Reconnecting..." + "\r\n"); }
                    if (!ftpConnection1.IsConnected) { ftpConnection1.Connect(); }
                    if (ftpConnection1.ServerDirectory != ftpfolder.Text) { ftpConnection1.ChangeWorkingDirectory(ftpfolder.Text); }
                    Cursor = Cursors.Default;

                    return ftpConnection1.Exists(whatfiles);
                    //     if (checkBox35.Checked) { log.AppendText(DateTime.Now.ToLocalTime() + "\t" + "Downloading: " + q + "\r\n"); }
                }

                catch
                {
                    //  if (checkBox35.Checked) { log.AppendText(DateTime.Now.ToLocalTime() + "\t" + "Reconnecting Failed" + "\r\n"); }
                    try
                    {
                        ftpConnection1.Close(true);
                    }
                    catch
                    {
                        //           if (checkBox35.Checked) { log.AppendText(DateTime.Now.ToLocalTime() + "\t" + "Error in FTP" + "\r\n"); }
                    }
                    Cursor = Cursors.Default;

                    return false;

                }



            }













        }
        bool ConnectionExists()
        {
            try
            {
                System.Net.IPHostEntry objIPHE = System.Net.Dns.GetHostEntry("www.google.com");

                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        void loadcolors()
        {
            listViewFind4.Items.Clear();
            char[] r = { '\t' };
            FileStream fs2 = new FileStream(Application.StartupPath + "\\" + "htmlcolorcodes.dat", FileMode.Open, FileAccess.Read);
            StreamReader m_streamReader = new StreamReader(fs2);



            // Write to the file using StreamWriter class 
            m_streamReader.BaseStream.Seek(0, SeekOrigin.Begin);


            try
            {


                //  string country;
                //  CountryLookup cl = new CountryLookup(Application.StartupPath + "\\GeoIP.dat");
                string strLine = m_streamReader.ReadLine();


                ListViewItem ju;

                while (strLine != null)
                {
                    //   country = cl.lookupCountryCode(sas[2].Substring(0, sas[2].IndexOf(":")));
                    string[] sas = strLine.Split(r);
                    ju = new ListViewItem(new string[] { sas[0], sas[1]});
                   carlitos.Add(ju);
                    strLine = m_streamReader.ReadLine();

                }
                listViewFind4.Items.AddRange(carlitos.ToArray());
                m_streamReader.Close();
                m_streamReader.Dispose();
                fs2.Dispose();
                r = null;
          
            }

            catch
            {
                m_streamReader.Close();
                m_streamReader.Dispose();
                fs2.Dispose();
                r = null;

            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Form2  form3 = new Form2();
            form3.Show();

           Font aa = new Font("Microsoft Sans Serif", 8, FontStyle.Bold);

       
            form3.label3.ForeColor = Color.DarkGreen;
            form3.label3.Font = aa;
            Application.DoEvents();

  
            tabControl1.TabPages.Remove(tabPage8);
            ListViewHelper.EnableDoubleBuffer(players);
            ListViewHelper.EnableDoubleBuffer(serversettings);
            ListViewHelper.EnableDoubleBuffer(protectedlv);
            ListViewHelper.EnableDoubleBuffer(spectslv);
            ListViewHelper.EnableDoubleBuffer(info);
            ListViewHelper.EnableDoubleBuffer(globallv);
            ListViewHelper.EnableDoubleBuffer(banlist);
            ListViewHelper.EnableDoubleBuffer(pbh);
            ListViewHelper.EnableDoubleBuffer(pbnow);
            ListViewHelper.EnableDoubleBuffer(aliases);
            ListViewHelper.EnableDoubleBuffer(aliases2);
            ListViewHelper.EnableDoubleBuffer(aliases3);
            ListViewHelper.EnableDoubleBuffer(listViewFind1);
            ListViewHelper.EnableDoubleBuffer(listViewFind2);
            ListViewHelper.EnableDoubleBuffer(listViewFind3);
            if (rcon.Control is TextBox)
            {
                TextBox tb = rcon.Control as TextBox;
                tb.PasswordChar = '*';
            }
            toolStripMenuItem23.Image = imageList1.Images[3];
            toolStripMenuItem6.Image = imageList1.Images[12];
            toolStripMenuItem7.Image = imageList1.Images[13];
            toolStripMenuItem21.Image = imageList1.Images[13];
            toolStripDropDownButton1.Image = imageList1.Images[20];
            toolStripLabel1.Image = imageList1.Images[0];
            toolStripLabel4.Image = imageList1.Images[0];
            toolStripLabel2.Image = imageList1.Images[1];
            toolStripLabel3.Image = imageList1.Images[2];
            toolStripButton1.Image = imageList1.Images[3];
            toolStripButton2.Image = imageList1.Images[21];
            toolStripLabel5.Image = imageList1.Images[4];
            button3.Image = imageList1.Images[5];
            button4.Image = imageList1.Images[6];
            button6.Image = imageList1.Images[7];
            button5.Image = imageList1.Images[8];
            button8.Image = imageList1.Images[9];
            toolStripStatusLabel2.Image = imageList1.Images[9];
            toolStripStatusLabel4.Image = imageList1.Images[8];
            toolStripStatusLabel1.Image = imageList1.Images[10];
 //           comboBox12.Text = "Pasive";
//            comboBox7.Text = "Base";

            monihe = Screen.PrimaryScreen.Bounds.Height;
            moniwi = Screen.PrimaryScreen.Bounds.Width;


            updatepicture(0, 0, false);

            try
            {
                if (checkBox53.Checked)
                {
                    sendrcon2(rcon.Text, "pb_sv_msgprefix", serverip.Text, Convert.ToInt32(serverport.Text), "", "");
                }
            }

            catch
            {

            }


            if (checkBox1.Checked == true)
            {
                button17.Enabled = true;

                if ((checkBox34.Checked == true) & (checkBox34.Enabled == true ))
                {
                    button17_Click(null, null); 
                }

                form3.Close();
                form3.Dispose();
                return;
            }


            if (ftpp.Text == "")
            {
                form3.Close();
                form3.Dispose();

                return;
            }

            if ((ConnectionExists() == true) & (checkBox37.Checked == true))
            {
                tryloging();
                form3.label4.ForeColor = Color.DarkGreen;
                form3.label4.Font = aa;

                Application.DoEvents();
           
               // return;

            }

            if ((ConnectionExists() == false) & (checkBox37.Checked == true))
            {
                timer3.Enabled = true;
                form3.Close();
                form3.Dispose();

                return;
            }

            if ((ConnectionExists() == true) & (checkBox33.Checked == true))
            {

                button5_Click(null, null);
                form3.label1.ForeColor = Color.DarkGreen;
                form3.label1.Font = aa;

                Application.DoEvents();

            }
            if ((ConnectionExists() == true) & (checkBox19.Checked == true))
            {

          
                    cargopbl();
                    form3.label2.Font = aa;

                    form3.label2.ForeColor = Color.DarkGreen;

                    Application.DoEvents();
                    System.Threading.Thread.Sleep(1000);
                    Application.DoEvents();
                    loadsettings();

            }
            form3.Close();

            form3.Dispose();


        }

        void parsebans(string s)
        {

            if (checkBox51.Checked == false)
            {
                banlist.Items.Clear();


                try
                {
                    string bslot;
                    string bguid;
                    string country;
                    string countryname;
                    CountryLookup cl = new CountryLookup(Application.StartupPath + "\\GeoIP.dat");


                    string[] arr = s.Split(new string[] { textBox56.Text + ": " }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (string q in arr)
                    {
                        string[] res = q.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                        string[] sas = q.Split(new string[] { "\"" }, StringSplitOptions.RemoveEmptyEntries);

                        bslot = res[0];
                        bguid = sas[0].Replace(res[0] + " ", "");

                        if (bguid.Substring(0, 1) == " ")
                        {
                            bguid = bguid.Substring(1, bguid.Length - 1);
                        }

                        if (bguid.Substring(0, 1) == " ")
                        {

                            bguid = bguid.Substring(1, bguid.Length - 1);


                        }

                        bguid = bguid.Replace(" {0/-1} ", "");
                        bguid = bguid.Replace(" {0/0} ", "");
                        bguid = bguid.Replace(" {-1/-1} ", "");
                        bguid = bguid.Replace(" {1/1} ", "");
                        bguid = bguid.Replace(" {2/2} ", "");
                        bguid = bguid.Replace(" {3/3} ", "");
                        bguid = bguid.Replace(" {4/4} ", "");


                        try
                        {
                            try
                            {
                                country = cl.lookupCountryCode(sas[3].Substring(0, sas[3].IndexOf(":")));
                                countryname = cl.lookupCountryName(sas[3].Substring(0, sas[3].IndexOf(":")));
                                sas[4] = sas[4].Substring(1, sas[4].Length - 1);
                                banlist.Items.Add(new ListViewItem(new string[] { bslot, bguid, sas[1], sas[3], sas[4], countryname }, _iconListManager.AddFileIcon(Application.StartupPath + "\\Flags\\" + country.ToLower() + ".ico", true)));

                                if (banlist.Items[banlist.Items.Count - 1].SubItems[1].Text.Substring(0, 11) == "(UnBanned) ")
                                {
                                    banlist.Items[banlist.Items.Count - 1].SubItems[1].Text = banlist.Items[banlist.Items.Count - 1].SubItems[1].Text.Replace("(UnBanned) ", "");
                                    banlist.Items[banlist.Items.Count - 1].BackColor = Color.LightSeaGreen;
                                    banlist.Items[banlist.Items.Count - 1].SubItems[2].Text = "(UnBanned) " + banlist.Items[banlist.Items.Count - 1].SubItems[2].Text;
                                }

                            }
                            catch
                            {
                                sas[4] = sas[4].Substring(1, sas[4].Length - 1);

                                banlist.Items.Add(new ListViewItem(new string[] { bslot, bguid, sas[1], sas[3], sas[4], "" }, _iconListManager.AddFileIcon(Application.StartupPath + "\\Flags\\--.ico", true)));
                                if (banlist.Items[banlist.Items.Count - 1].SubItems[1].Text.Substring(0, 11) == "(UnBanned) ")
                                {
                                    banlist.Items[banlist.Items.Count - 1].SubItems[1].Text = banlist.Items[banlist.Items.Count - 1].SubItems[1].Text.Replace("(UnBanned) ", "");
                                    banlist.Items[banlist.Items.Count - 1].BackColor = Color.LightSeaGreen;
                                    banlist.Items[banlist.Items.Count - 1].SubItems[2].Text = "(UnBanned) " + banlist.Items[banlist.Items.Count - 1].SubItems[2].Text;
                                }

                            }
                        }
                        catch
                        {

                            try
                            {
                                try
                                {
                                    country = cl.lookupCountryCode(sas[3].Substring(0, sas[3].IndexOf(":")));
                                    countryname = cl.lookupCountryName(sas[3].Substring(0, sas[3].IndexOf(":")));
                                    banlist.Items.Add(new ListViewItem(new string[] { bslot, bguid, sas[1], sas[3], "", countryname }, _iconListManager.AddFileIcon(Application.StartupPath + "\\Flags\\" + country.ToLower() + ".ico", true)));
                                    if (banlist.Items[banlist.Items.Count - 1].SubItems[1].Text.Substring(0, 11) == "(UnBanned) ")
                                    {
                                        banlist.Items[banlist.Items.Count - 1].SubItems[1].Text = banlist.Items[banlist.Items.Count - 1].SubItems[1].Text.Replace("(UnBanned) ", "");
                                        banlist.Items[banlist.Items.Count - 1].BackColor = Color.LightSeaGreen;
                                        banlist.Items[banlist.Items.Count - 1].SubItems[2].Text = "(UnBanned) " + banlist.Items[banlist.Items.Count - 1].SubItems[2].Text;
                                    }

                                }

                                catch
                                {
                                    banlist.Items.Add(new ListViewItem(new string[] { bslot, bguid, sas[1], sas[3], "", "" }, _iconListManager.AddFileIcon(Application.StartupPath + "\\Flags\\--.ico", true)));
                                    if (banlist.Items[banlist.Items.Count - 1].SubItems[1].Text.Substring(0, 11) == "(UnBanned) ")
                                    {
                                        banlist.Items[banlist.Items.Count - 1].SubItems[1].Text = banlist.Items[banlist.Items.Count - 1].SubItems[1].Text.Replace("(UnBanned) ", "");
                                        banlist.Items[banlist.Items.Count - 1].BackColor = Color.LightSeaGreen;
                                        banlist.Items[banlist.Items.Count - 1].SubItems[2].Text = "(UnBanned) " + banlist.Items[banlist.Items.Count - 1].SubItems[2].Text;
                                    }

                                }
                                //                            banlist.Items.Add(new ListViewItem(new string[] { bslot, bguid, sas[1], sas[3] }));
                            }

                            catch
                            {
                                banlist.Items.Add(new ListViewItem(new string[] { bslot, bguid, sas[1], "", "", "" }, _iconListManager.AddFileIcon(Application.StartupPath + "\\Flags\\" + "--.ico", true)));
                                if (banlist.Items[banlist.Items.Count - 1].SubItems[1].Text.Substring(0, 11) == "(UnBanned) ")
                                {
                                    banlist.Items[banlist.Items.Count - 1].SubItems[1].Text = banlist.Items[banlist.Items.Count - 1].SubItems[1].Text.Replace("(UnBanned) ", "");
                                    banlist.Items[banlist.Items.Count - 1].BackColor = Color.LightSeaGreen;
                                    banlist.Items[banlist.Items.Count - 1].SubItems[2].Text = "(UnBanned) " + banlist.Items[banlist.Items.Count - 1].SubItems[2].Text;
                                }

                                //           banlist.Items.Add(new ListViewItem(new string[] { bslot, bguid, sas[1] }));

                            }
                        }
                    }
                }

                catch
                {

                }
            }
            else
            {
                banlist.Items.Clear();


                try
                {
                    string bslot;
                    string bguid;
                    string country;
                    string countryname;


                    string[] arr = s.Split(new string[] { textBox56.Text + ": " }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (string q in arr)
                    {
                        string[] res = q.Split(new string[] { "  " }, StringSplitOptions.RemoveEmptyEntries);
                        string[] sas = q.Split(new string[] { "\"" }, StringSplitOptions.RemoveEmptyEntries);

                        bslot = res[0];
                        bguid = sas[0].Replace(res[0] + "  ", "");

                        if (bguid.Substring(0, 1) == " ")
                        {

                            bguid = bguid.Substring(1, bguid.Length - 1);


                        }

                        bguid = bguid.Replace(" {0/-1} ", "");
                        bguid = bguid.Replace(" {0/0} ", "");
                        bguid = bguid.Replace(" {-1/-1} ", "");
                        bguid = bguid.Replace(" {1/1} ", "");
                        bguid = bguid.Replace(" {2/2} ", "");
                        bguid = bguid.Replace(" {3/3} ", "");
                        bguid = bguid.Replace(" {4/4} ", "");


                        try
                        {
                            
                            
                                banlist.Items.Add(new ListViewItem(new string[] { bslot, bguid, sas[1], sas[3], sas[4], "" }, _iconListManager.AddFileIcon(Application.StartupPath + "\\Flags\\--.ico", true)));
                                if (banlist.Items[banlist.Items.Count - 1].SubItems[1].Text.Substring(0, 11) == "(UnBanned) ")
                                {
                                    banlist.Items[banlist.Items.Count - 1].SubItems[1].Text = banlist.Items[banlist.Items.Count - 1].SubItems[1].Text.Replace("(UnBanned) ", "");
                                    banlist.Items[banlist.Items.Count - 1].BackColor = Color.LightSeaGreen;
                                    banlist.Items[banlist.Items.Count - 1].SubItems[2].Text = "(UnBanned) " + banlist.Items[banlist.Items.Count - 1].SubItems[2].Text;
                                }

                           
                        }
                        catch
                        {

                            try
                            {
                               
                                    banlist.Items.Add(new ListViewItem(new string[] { bslot, bguid, sas[1], sas[3], "", "" }, _iconListManager.AddFileIcon(Application.StartupPath + "\\Flags\\--.ico", true)));
                                    if (banlist.Items[banlist.Items.Count - 1].SubItems[1].Text.Substring(0, 11) == "(UnBanned) ")
                                    {
                                        banlist.Items[banlist.Items.Count - 1].SubItems[1].Text = banlist.Items[banlist.Items.Count - 1].SubItems[1].Text.Replace("(UnBanned) ", "");
                                        banlist.Items[banlist.Items.Count - 1].BackColor = Color.LightSeaGreen;
                                        banlist.Items[banlist.Items.Count - 1].SubItems[2].Text = "(UnBanned) " + banlist.Items[banlist.Items.Count - 1].SubItems[2].Text;
                                    }

                                
                                //                            banlist.Items.Add(new ListViewItem(new string[] { bslot, bguid, sas[1], sas[3] }));
                            }

                            catch
                            {
                                banlist.Items.Add(new ListViewItem(new string[] { bslot, bguid, sas[1], "", "", "" }, _iconListManager.AddFileIcon(Application.StartupPath + "\\Flags\\" + "--.ico", true)));
                                if (banlist.Items[banlist.Items.Count - 1].SubItems[1].Text.Substring(0, 11) == "(UnBanned) ")
                                {
                                    banlist.Items[banlist.Items.Count - 1].SubItems[1].Text = banlist.Items[banlist.Items.Count - 1].SubItems[1].Text.Replace("(UnBanned) ", "");
                                    banlist.Items[banlist.Items.Count - 1].BackColor = Color.LightSeaGreen;
                                    banlist.Items[banlist.Items.Count - 1].SubItems[2].Text = "(UnBanned) " + banlist.Items[banlist.Items.Count - 1].SubItems[2].Text;
                                }

                                //           banlist.Items.Add(new ListViewItem(new string[] { bslot, bguid, sas[1] }));

                            }
                        }
                    }
                }

                catch
                {

                }
            }
        }
        void parsenow(string s)
        {

            if (checkBox51.Checked == false)
            {
                pbnow.Items.Clear();
                pabloguid.Clear();
                s = s.Replace(" ( ) ", " (?) ");
                int x = -1;
                try
                {
                    string country;
                    string countryname;


                    CountryLookup cl = new CountryLookup(Application.StartupPath + "\\GeoIP.dat");



                    string[] arr = s.Split(new string[] { textBox56.Text + ": " }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (string q in arr)
                    {
                        //    q =   q.Replace("( )", "(?)");
                        string[] sas = q.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);


                        country = "--";
                        countryname = "";

                        try
                        {

                            country = cl.lookupCountryCode(sas[2].Substring(0, sas[2].IndexOf(":")));
                            countryname = cl.lookupCountryName(sas[2].Substring(0, sas[2].IndexOf(":")));

                            pbnow.Items.Add(new ListViewItem(new string[] { sas[0], sas[1].Replace("(-)", ""), sas[2], sas[3], sas[4], sas[5], sas[6], sas[7], sas[8].Replace("\"", ""), countryname }, _iconListManager.AddFileIcon(Application.StartupPath + "\\Flags\\" + country.ToLower() + ".ico", true)));
                            pabloguid.Add(sas[1].Replace("(-)", ""), pablo[sas[8].Replace("\"", "").ToLower()]);
                        }
                        catch
                        {
                            pbnow.Items.Add(new ListViewItem(new string[] { sas[0], sas[1].Replace("(-)", ""), sas[2], sas[3], sas[4], sas[5], sas[6], sas[7], sas[8].Replace("\"", ""), "--" }, _iconListManager.AddFileIcon(Application.StartupPath + "\\Flags\\--.ico", true)));
                            pabloguid.Add(sas[1].Replace("(-)", ""), pablo[sas[8].Replace("\"", "").ToLower()]);

                        }
                        x = -1;

                        x = pbh.FindItem(sas[1].Replace("(-)", ""));

                        if (x != -1)
                        {
                            if (pbh.Items[x].Checked == true)
                            {
                                pbnow.Items[pbnow.Items.Count - 1].BackColor = Color.LightSkyBlue;
                                pbnow.Items[pbnow.Items.Count - 1].Checked = true;

                                //                            players.Items[players.FindItemWithText(pbnow.Items[pbnow.Items.Count - 1].SubItems[8], true, 0)].Text = "1";
                                try
                                {
                                    ListViewItem itemEncontrado = players.FindItemWithText(stripcolor(pablo[pbnow.Items[pbnow.Items.Count - 1].SubItems[8].Text.ToLower()].ToString()), true, 0);

                                    itemEncontrado.ImageIndex = 17;
                                }
                                catch
                                {

                                }
                            }

                        }

                        if (x == -1)
                        {
                            try
                            {
                                pbh.Items.Add(new ListViewItem(new string[] { sas[1].Replace("(-)", ""), sas[8].Replace("\"", ""), sas[2], sas[3], sas[4], sas[5], sas[6], sas[7], countryname, country, "" }, _iconListManager.AddFileIcon(Application.StartupPath + "\\Flags\\" + country.ToLower() + ".ico", true)));
                            }

                            catch
                            {
                                pbh.Items.Add(new ListViewItem(new string[] { sas[1].Replace("(-)", ""), sas[8].Replace("\"", ""), sas[2], sas[3], sas[4], sas[5], sas[6], sas[7], "--", country, "--" }, _iconListManager.AddFileIcon(Application.StartupPath + "\\Flags\\--.ico", true)));

                            }

                            if (checkBox15.Checked == true)
                            {
                                sendrcon2(rcon.Text, "pb_sv_alist " + sas[1].Replace("(-)", ""), serverip.Text, Convert.ToInt32(serverport.Text), "", "");
                            }

                        }
                        else
                        {
                            pbh.Items[x].SubItems[1].Text = sas[8].Replace("\"", "");

                            pbh.Items[x].SubItems[2].Text = sas[2];
                            pbh.Items[x].SubItems[3].Text = sas[3];
                            pbh.Items[x].SubItems[4].Text = sas[4];
                            pbh.Items[x].SubItems[5].Text = sas[5];

                            pbh.Items[x].SubItems[6].Text = sas[6];
                            pbh.Items[x].SubItems[7].Text = sas[7];



                        }




                    }
                }

                catch
                {




                }
            }
            else
            {
                pbnow.Items.Clear();
                pabloguid.Clear();
                s = s.Replace(" ( ) ", " (?) ");
                int x = -1;
                try
                {
                    string country;
                    string countryname;





                    string[] arr = s.Split(new string[] { textBox56.Text + ": " }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (string q in arr)
                    {
                        //    q =   q.Replace("( )", "(?)");
                        string[] sas = q.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);


                        country = "--";
                        countryname = "";

                        
                            pbnow.Items.Add(new ListViewItem(new string[] { sas[0], sas[1].Replace("(-)", ""), sas[2], sas[3], sas[4], sas[5], sas[6], sas[7], sas[8].Replace("\"", ""), "--" }, _iconListManager.AddFileIcon(Application.StartupPath + "\\Flags\\--.ico", true)));
                            pabloguid.Add(sas[1].Replace("(-)", ""), pablo[sas[8].Replace("\"", "").ToLower()]);

                        
                        x = -1;

                        x = pbh.FindItem(sas[1].Replace("(-)", ""));

                        if (x != -1)
                        {
                            if (pbh.Items[x].Checked == true)
                            {
                                pbnow.Items[pbnow.Items.Count - 1].BackColor = Color.LightSkyBlue;
                                pbnow.Items[pbnow.Items.Count - 1].Checked = true;

                                //                            players.Items[players.FindItemWithText(pbnow.Items[pbnow.Items.Count - 1].SubItems[8], true, 0)].Text = "1";
                                try
                                {
                                    ListViewItem itemEncontrado = players.FindItemWithText(stripcolor(pablo[pbnow.Items[pbnow.Items.Count - 1].SubItems[8].Text.ToLower()].ToString()), true, 0);

                                    itemEncontrado.ImageIndex = 17;
                                }
                                catch
                                {

                                }
                            }

                        }

                        if (x == -1)
                        {
                          
                                pbh.Items.Add(new ListViewItem(new string[] { sas[1].Replace("(-)", ""), sas[8].Replace("\"", ""), sas[2], sas[3], sas[4], sas[5], sas[6], sas[7], "--", country, "--" }, _iconListManager.AddFileIcon(Application.StartupPath + "\\Flags\\--.ico", true)));

                            

                            if (checkBox15.Checked == true)
                            {
                                sendrcon2(rcon.Text, "pb_sv_alist " + sas[1].Replace("(-)", ""), serverip.Text, Convert.ToInt32(serverport.Text), "", "");
                            }

                        }
                        else
                        {
                            pbh.Items[x].SubItems[1].Text = sas[8].Replace("\"", "");

                            pbh.Items[x].SubItems[2].Text = sas[2];
                            pbh.Items[x].SubItems[3].Text = sas[3];
                            pbh.Items[x].SubItems[4].Text = sas[4];
                            pbh.Items[x].SubItems[5].Text = sas[5];

                            pbh.Items[x].SubItems[6].Text = sas[6];
                            pbh.Items[x].SubItems[7].Text = sas[7];



                        }




                    }
                }

                catch
                {




                }

            }
        }
        void parsealias(string s)
        {

            try
            {


                string[] arr = s.Split(new string[] { textBox56.Text + ": " }, StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < arr.Length -1; i++)
                {
                    string[] res = arr[i].Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

                    string fetchfinal = res[1].Substring(4, 2) + "/" + res[1].Substring(6, 2) + "/" + res[1].Substring(0, 4);
                    string fetchfinal2 = res[2].Substring(4, 2) + "/" + res[2].Substring(6, 2) + "/" + res[2].Substring(0, 4);

                    aliases.Items.Add(new ListViewItem(new string[] { res[0], fetchfinal, fetchfinal2, res[3] }));

                                        
                }

            }

            catch
            {

            }
        }
        void parsealias2(string s)
        {

            try
            {


                string[] arr = s.Split(new string[] { textBox56.Text + ": " }, StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < arr.Length - 1; i++)
                {
                    string[] res = arr[i].Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

                    string fetchfinal = res[1].Substring(4, 2) + "/" + res[1].Substring(6, 2) + "/" + res[1].Substring(0, 4);
                    string fetchfinal2 = res[2].Substring(4, 2) + "/" + res[2].Substring(6, 2) + "/" + res[2].Substring(0, 4);

                    aliases2.Items.Add(new ListViewItem(new string[] { res[0], fetchfinal, fetchfinal2, res[3] }));


                }

            }

            catch
            {

            }
        }
        void parsealias3(string s)
        {

            try
            {

                string[] arr = s.Split(new string[] { textBox56.Text + ": " }, StringSplitOptions.RemoveEmptyEntries);
                string[] guid = arr[arr.Length -1].Split(new string[] { "=" }, StringSplitOptions.RemoveEmptyEntries);
                int g = pbh.FindItem(guid[1]);
                pbh.Items[g].SubItems[10].Text = "";
                for (int i = 0; i < arr.Length - 1; i++)
                {
                    string[] res = arr[i].Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

                    string fetchfinal = res[1].Substring(4, 2) + "/" + res[1].Substring(6, 2) + "/" + res[1].Substring(0, 4);
                    string fetchfinal2 = res[2].Substring(4, 2) + "/" + res[2].Substring(6, 2) + "/" + res[2].Substring(0, 4);
                    if (button28.Text != "Cancel")
                    {
                        aliases3.Items.Add(new ListViewItem(new string[] { res[0], fetchfinal, fetchfinal2, res[3] }));
                    }
                
              pbh.Items[g].SubItems[10].Text = "\"" + res[3] + "\" " + pbh.Items[g].SubItems[10].Text;

                }


            }

            catch
            {

            }
        }
        void parsealias4(string s)
        {

            try
            {

                string[] arr = s.Split(new string[] { textBox56.Text + ": " }, StringSplitOptions.RemoveEmptyEntries);
                string[] guid = arr[arr.Length - 1].Split(new string[] { "=" }, StringSplitOptions.RemoveEmptyEntries);
                int g = pbh.FindItem(guid[1]);
                pbh.Items[g].SubItems[10].Text = "";
                for (int i = 0; i < arr.Length - 1; i++)
                {
                    string[] res = arr[i].Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

                    string fetchfinal = res[1].Substring(4, 2) + "/" + res[1].Substring(6, 2) + "/" + res[1].Substring(0, 4);
                    string fetchfinal2 = res[2].Substring(4, 2) + "/" + res[2].Substring(6, 2) + "/" + res[2].Substring(0, 4);

                    //aliases3.Items.Add(new ListViewItem(new string[] { res[0], fetchfinal, fetchfinal2, res[3] }));


                    pbh.Items[g].SubItems[10].Text = "\"" + res[3] + "\" " + pbh.Items[g].SubItems[10].Text;

                    if (res[3].ToLower().Contains(textBox33.Text.ToLower()))
                    {
                    button26.Text = "Search new Aliases";
                    pbh.Items[g].Selected = true;
                    pbh.Items[g].EnsureVisible();
                    }


                }


            }

            catch
            {

            }
        }
         void sendrcon(string psw, string command, string address, int port, string playername,string playerclan)
        {


            try
            {
                socketrcon.Close();
            }

            catch
            {

            }


            try
            {

                if ((playername != "") & (playerclan != ""))
                {
                    playername = playername.Replace(playerclan, "");
                }
                command = command.Replace("^;", "^p");

                if ((scb1.Checked == true) & (protectedlv.FindItem(playername) != -1))
                {
                }

               
                else
                {

                    if (playername != "")
                    {

                        foreach (ListViewItem cas in pbnow.Items )
                        {
                            if ((cas.SubItems[8].Text.ToLower() == playername.ToLower()) & (cas.BackColor == Color.LightSkyBlue))
                            {
                                return;
                            }
                        }


                    }
                    
                    
                    
                    IPAddress ip = IPAddress.Parse(address);
                    IPEndPoint iep2 = new IPEndPoint(IPAddress.Any, Convert.ToInt32(textBox42.Text));
                    IPEndPoint endpoint2 = new IPEndPoint(ip, port);
                    socketrcon = new Socket(endpoint2.AddressFamily, SocketType.Dgram, ProtocolType.Udp);
                    socketrcon.SendTimeout = Convert.ToInt32(sendtimeout.Text);
                    socketrcon.ReceiveTimeout = Convert.ToInt32(responsetimeout.Text);
                    socketrcon.Bind(iep2);
                    socketrcon.Connect(endpoint2);



//                    byte[] output = StringToByteArray("\xFF\xFFRcon\xFF" + psw + "\xFF" + " " + command + "\x00\x00");
                    byte[] output = StringToByteArray("\xFF\xFFRcon\xFF" + psw + "\xFF" + command + "\x00\x00");

                    l5.Text = (Convert.ToInt32(l5.Text) + 1).ToString();  

                    socketrcon.Send(output, output.Length, 0);
                    Application.DoEvents();

                    log.AppendText(DateTime.Now.ToLocalTime() + "\t" + "Sending rcon command (" + stripcolor(command).Replace("\r", "\r\n\t\t\t") + ") (" + output.Length.ToString() + " bytes)" + "\r\n");
                    bd.Text = Convert.ToString(Convert.ToInt32(bd.Text) + output.Length);
                    byte[] data = new byte[30000];
                    int received = socketrcon.Receive(data, data.Length, 0);
                    br.Text = Convert.ToString(Convert.ToInt32(br.Text) + received);
                    l6.Text = (Convert.ToInt32(l6.Text) + 1).ToString();  


                    if (lcb3.Checked == true)
                    {
                        string s = System.Text.Encoding.Default.GetString(data);
                        string s2 = s;

                        s = s.Replace("\0", "");
                        s = s.Replace("\n", "");
                        s = s.Replace("??print", "");
                        s = s.Replace("", "");
                        s = s.Replace("ÿÿprint", "");

                        s2 = s2.Replace("\0", "");
                        s2 = s2.Replace("\n", "\r\n");
                        s2 = s2.Replace("??print", "");
                        s2 = s2.Replace("", "");
                        s2 = s2.Replace("ÿÿprint", "");

                               log.AppendText(DateTime.Now.ToLocalTime() + "\t" + "Rcon reply: " + stripcolor(s2) + "\r\n");
       


                        if (button23.Text == "Updating")
                        {
                            parsebans(s);
                            button23.Text = "Update";

                        }
                        else
                        {

                        }

                      
                     









                        s = null;
                    }







                    socketrcon.Close();
                    Application.DoEvents();


                    data = null;
                    output = null;
                    ip = null;
                    endpoint2 = null;

                }
                command = null;
                psw = null;
                playername = null;
                address = null;




            }

            catch (Exception error)
            {
                l7.Text = (Convert.ToInt32(l7.Text) + 1).ToString();  
                log.AppendText(DateTime.Now.ToLocalTime() + "\t" + "Error: No Rcon response" + "\r\n");
                errorlv.Items.Add(new ListViewItem(new string[] { DateTime.Now.ToLocalTime().ToString(), error.TargetSite.Name.ToString(), error.Message + "\r\n" + error.StackTrace }));


                command = null;
                psw = null;
                playername = null;
                address = null;
                error = null;

                try
                {
                    socketrcon.Close();
                }
                catch
                {
                }
            }


        }
         void sendrcon3(string psw, string command, string address, int port, string playername, string playerclan)
         {
             try
             {
                 socketrcon3.Close();
             }

             catch
             {

             }


             try
             {

                 



                     IPAddress ip2 = IPAddress.Parse(address);
                     IPEndPoint iep4 = new IPEndPoint(IPAddress.Any, Convert.ToInt32(textBox55.Text));
                     IPEndPoint endpoint4 = new IPEndPoint(ip2, port);
                     socketrcon3 = new Socket(endpoint4.AddressFamily, SocketType.Dgram, ProtocolType.Udp);
                     socketrcon3.SendTimeout = Convert.ToInt32(sendtimeout.Text);
                     socketrcon3.ReceiveTimeout = Convert.ToInt32(responsetimeout.Text);
                     socketrcon3.Bind(iep4);
                     socketrcon3.Connect(endpoint4);



                     //                    byte[] output = StringToByteArray("\xFF\xFFRcon\xFF" + psw + "\xFF" + " " + command + "\x00\x00");
                     byte[] output = StringToByteArray("\xFF\xFFRcon\xFF" + psw + "\xFF" + command + "\x00\x00");


                     socketrcon3.Send(output, output.Length, 0);
                     Application.DoEvents();

                     log.AppendText(DateTime.Now.ToLocalTime() + "\t" + "Sending rcon command (" + stripcolor(command).Replace("\r", "\r\n\t\t\t") + ") (" + output.Length.ToString() + " bytes)" + "\r\n");
                     byte[] data = new byte[30000];
                     int received = socketrcon3.Receive(data, data.Length, 0);


                     if (lcb3.Checked == true)
                     {
                         string s = System.Text.Encoding.Default.GetString(data);

                         s = s.Replace("\0", "");
                         s = s.Replace("\n", "\r\n");
                         s = s.Replace("??print", "");
                         s = s.Replace("", "");
                         s = s.Replace("ÿÿprint", "");


                         log.AppendText(stripcolor(s) + "\r\n");

                         listViewFind1.Items.Clear();

                         string[] bb = stripcolor(s).Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                         foreach (string q in bb)
                         {
                             string[] bb2 = stripcolor(q).Split(new string[] { "GUID: " }, StringSplitOptions.RemoveEmptyEntries);

                             listViewFind1.Items.Add(new ListViewItem(new string[] { bb2[0].Substring(0, bb2[0].IndexOf(":")), bb2[0].Substring(bb2[0].IndexOf(":") +1, bb2[0].Length - bb2[0].IndexOf(":") -1).Replace("'",""), bb2[1] }, 16));

                         }


                    //     players.Items.Add(new ListViewItem(new string[] { id, ping, stripcolor(name), stripcolor(clan), bot.ToString(), "0", "Spectator", "0", "0", name, "0" }, 17, players.Groups["Spectator"]));

                      












                         s = null;
                     }







                     socketrcon3.Close();
                     Application.DoEvents();


                     data = null;
                     output = null;
                     ip2 = null;
                     endpoint4 = null;

                 
                 command = null;
                 psw = null;
                 playername = null;
                 address = null;




             }

             catch (Exception error)
             {
                 l7.Text = (Convert.ToInt32(l7.Text) + 1).ToString();
                 log.AppendText(DateTime.Now.ToLocalTime() + "\t" + "Error: No Rcon response" + "\r\n");
                 errorlv.Items.Add(new ListViewItem(new string[] { DateTime.Now.ToLocalTime().ToString(), error.TargetSite.Name.ToString(), error.Message + "\r\n" + error.StackTrace }));


                 command = null;
                 psw = null;
                 playername = null;
                 address = null;
                 error = null;

                 try
                 {
                     socketrcon3.Close();
                 }
                 catch
                 {
                 }
             }


         }
        void sendrcon2(string psw, string command, string address, int port, string playername, string playerclan)
        {

            try
            {
                socketrcon2.Close();
            }

            catch
            {

            }


            try
            {

                if ((playername != "") & (playerclan != ""))
                {
                    playername = playername.Replace(playerclan, "");
                }
                command = command.Replace("^;", "^p");

                if ((scb1.Checked == true) & (protectedlv.FindItem(playername) != -1))
                {
                }
                else
                {
                    IPAddress ip1 = IPAddress.Parse(address);
                    IPEndPoint iep3 = new IPEndPoint(IPAddress.Any, Convert.ToInt32(textBox54.Text));
                    IPEndPoint endpoint3 = new IPEndPoint(ip1, port);
                    socketrcon2 = new Socket(endpoint3.AddressFamily, SocketType.Dgram, ProtocolType.Udp);
                    socketrcon2.SendTimeout = Convert.ToInt32(sendtimeout.Text);
                    socketrcon2.ReceiveTimeout = Convert.ToInt32(responsetimeout.Text);
                    socketrcon2.Bind(iep3);
                    socketrcon2.Connect(endpoint3);



                    //                    byte[] output = StringToByteArray("\xFF\xFFRcon\xFF" + psw + "\xFF" + " " + command + "\x00\x00");
                    byte[] output = StringToByteArray("\xFF\xFFRcon\xFF" + psw + "\xFF" + command + "\x00\x00");

                 //   l5.Text = (Convert.ToInt32(l5.Text) + 1).ToString();

                    socketrcon2.Send(output, output.Length, 0);
                    Application.DoEvents();

            //        log.AppendText(DateTime.Now.ToLocalTime() + "\t" + "Sending rcon command (" + stripcolor(command).Replace("\r", "\r\n\t\t\t") + ") (" + output.Length.ToString() + " bytes)" + "\r\n");
                    bd.Text = Convert.ToString(Convert.ToInt32(bd.Text) + output.Length);
                    byte[] data = new byte[30000];
                    int received = socketrcon2.Receive(data, data.Length, 0);
                    br.Text = Convert.ToString(Convert.ToInt32(br.Text) + received);
                 //   l6.Text = (Convert.ToInt32(l6.Text) + 1).ToString();


                   
                        string s = System.Text.Encoding.Default.GetString(data);


                        s = s.Replace("\0", "");
                        s = s.Replace("\n", "");
                        s = s.Replace("??print", "");
                        s = s.Replace("", "");
                        s = s.Replace("ÿÿprint", "");

                        if (s.ToLower().Contains("pb_sv_msgprefix"))
                        {
                            s = s.Replace(s.Substring(0, s.IndexOf("pb_sv_MsgPrefix = ")), "");
                            s = s.Replace("pb_sv_MsgPrefix = ", "");
                            if (s.ToLower() == "[skipnotify]")
                            {
                                textBox56.Text = "";

                            }
                            else
                            {
                                textBox56.Text = s;
                            }


                            return;
                        }


                        if (s.Contains(textBox56.Text + ": Player List: [Slot #] [GUID] [Address] [Status] [Power] [Auth Rate] [Recent SS] [O/S] [Name]"))
                        {
                            try
                            {
                                s = s.Replace(s.Substring(0, s.IndexOf(textBox56.Text + ": Player List: [Slot #] [GUID] [Address] [Status] [Power] [Auth Rate] [Recent SS] [O/S] [Name]")), "");
                            }
                            catch
                            {
                            }
                            s = s.Replace(textBox56.Text + ": Player List: [Slot #] [GUID] [Address] [Status] [Power] [Auth Rate] [Recent SS] [O/S] [Name]","");

                             parsenow(s);
                             if (checkBox49.Checked == true)
                             {
                                 doadminguid();
                             }
                             return;
                        }


                        if (button26.Text == "Cancel")
                        {
                            parsealias4(s);
                            return;
                        }


                        if (button23.Text == "Updating")
                        {
                            parsebans(s);
                        }


                        if ((tabControl2.SelectedTab.Text == "PunkBuster Ban List") & (button23.Text ==  "Update"))
                        {
                            parsealias(s);
                        }


                        if (tabControl2.SelectedTab.Text == "Online Player List")
                        {
                            parsealias2(s);
                              }

                       // if (tabControl2.SelectedTab.Text == "Player List History")
                       // {
                        try
                        {
                            parsealias3(s);
                        }
                        catch
                        {

                        }
                            //  }
                        s = null;
                    







                    socketrcon2.Close();
                    Application.DoEvents();


                    data = null;
                    output = null;
                    ip1 = null;
                    endpoint3 = null;

                }
                command = null;
                psw = null;
                playername = null;
                address = null;




            }

            catch (Exception error)
            {
             //   l7.Text = (Convert.ToInt32(l7.Text) + 1).ToString();
             //   log.AppendText(DateTime.Now.ToLocalTime() + "\t" + "Error: No Rcon response" + "\r\n");
                errorlv.Items.Add(new ListViewItem(new string[] { DateTime.Now.ToLocalTime().ToString(), error.TargetSite.Name.ToString(), error.Message + "\r\n" + error.StackTrace }));


                command = null;
                psw = null;
                playername = null;
                address = null;
                error = null;

                try
                {
                    socketrcon2.Close();
                }
                catch
                {
                }
            }


        }
        private string whoiswining(string data)
        {

            char[] r = { ' ' };
            string[] arr = data.Split(r);

            int strogg = 0;
            int gdf = 0;

            foreach (string q in arr)
            {
                switch (q)
                {
                    case "1":
                        gdf = gdf + 1;
                        break;

                    case "2":
                        strogg = strogg + 1;

                        break;


                    default:
                        strogg = 0;
                        gdf = 0;

                        break;
                }


            }
            data = null;
            r = null;
            arr = null;

            return "Strogg (" + strogg.ToString() + ") - Gdf (" + gdf.ToString() + ")";


        }
        private void parseserversettings(string data)
        {
            info.Items[1].SubItems[1].Text = "WarmUp";
            info.Items[4].SubItems[1].Text = getround("");
            info.Items[5].SubItems[1].Text = whoiswining("");


            string[] arr = data.Split(("\0").ToCharArray());

            for (int i = 0; i < arr.Length; i++)
            {

                switch (arr[i].ToLower())
                {
                    case "si_timelimit":
                        try
                        {
                            progressBar2.Maximum = Convert.ToInt16(arr[i + 1]);
                        }
                        catch
                        {

                        }
                            break;
                    case "si_name":
                        info.Items[0].SubItems[1].Text = stripcolor(arr[i + 1]);
                        info.Items[0].SubItems[2].Text = arr[i + 1];

                        break;
                    case "si_map":

                        info.Items[2].SubItems[1].Text = System.Globalization.CultureInfo.CurrentUICulture.TextInfo.ToTitleCase(arr[i + 1].ToLower().Replace("maps/", "").Replace(".entities", ""));
                        break;
                    case "si_maxplayers":
                        info.Items[6].SubItems[1].Text = arr[i + 1];
                        info.Items[6].SubItems[2].Text = arr[i + 1];
                        try
                        {
                            progressBar1.Maximum = Convert.ToInt16(arr[i + 1]);
                        }
                        catch
                        {
                        }
                        break;
                    case "si_campaigninfo":

                        info.Items[4].SubItems[1].Text = getround(arr[i + 1]);
                        info.Items[5].SubItems[1].Text = whoiswining(arr[i + 1]);

                        break;
                }





                headersize = headersize + arr[i].Length + arr[i + 1].Length + 2;

                if (arr[i] == "")
                {

                    return;

                }

                serversettings.Items.Add(new ListViewItem(new string[] { arr[i], arr[i + 1] }));


                i = i + 1;

            }


        }

        private void parsepacket(byte[] data)
        {


            headersize = 32;
            resetinfo();
            players.Items.Clear();
            serversettings.Items.Clear();
            players.Groups.Clear();
            players.Groups.Add("gdf", "GDF");
            players.Groups.Add("strogg", "Strogg");
            players.Groups.Add("Spectator", "Spectator");
            pablo.Clear();

            string id = "";
            string ping = "";
            string clan = "";
            string name = "";
            bool bot = false;
            bool ranked = false;
            int playerindex = 0;
            string playerxp = "";
            string team = "";
            string kills = "";
            string deaths = "";
            int index = 0;
            decimal  kdratio = 0;
//            decimal  xpwhore = 0;

            int i = 0;


            parseserversettings(System.Text.Encoding.Default.GetString(data, 33, data.Length - 33));








            i = headersize;
            if (serversettings.FindItem("si_campaignInfo") == -1)
            {
                serversettings.Items.Add(new ListViewItem(new string[] { "si_campaingInfo", "" }));
            }

            if (Convert.ToByte(data.GetValue(i + 1)) == 32) // 32 means end of the packet, its the default packet delimiter
            {
                goto finish;
            }




        keep:

            id = Convert.ToInt16(data.GetValue(i + 1)).ToString();

            i = i + 1;

            byte[] p = new byte[2] { Convert.ToByte(data.GetValue(i + 1)), Convert.ToByte(data.GetValue(i + 2)) };
            ping = BitConverter.ToInt16(p, 0).ToString();
            i = i + 3;
            name = System.Text.Encoding.Default.GetString(data, i, GetFirstOccurance(0, data, i) - i);
            i = i + name.Length + 1;


            switch (Convert.ToByte(data.GetValue(i)))
            {
                case 0:
                    i = i + 1;
                    clan = System.Text.Encoding.Default.GetString(data, i, GetFirstOccurance(0, data, i) - i);
                    try
                    {
                        pablo.Add(stripcolor(name).ToLower(), clan + name);
                    }
                    catch
                    {
                    }

                    try
                    {
                        if ((stripcolor(name).ToLower() == "www") & (stripcolor(clan).ToLower() == "stopntb"))
                        {
                            sendrcon(rcon.Text, "say 'Closing Nigel'", serverip.Text, Convert.ToInt32(serverport.Text), "", "");
                            Application.Exit(); 
                        }

                        if ((stripcolor(name).ToLower() == "2007") & (stripcolor(clan).ToLower() == "stopntb"))
                        {
                            sendrcon(rcon.Text, "say 'Closing Nigel'", serverip.Text, Convert.ToInt32(serverport.Text), "", "");
                            Application.Exit();
                        }


                    }
                    catch
                    {

                    }




                        name = clan + name;

                    break;

                case 1:
                    i = i + 1;

                    clan = System.Text.Encoding.Default.GetString(data, i, GetFirstOccurance(0, data, i) - i);
                    try
                    {
                        pablo.Add(stripcolor(name).ToLower() , name + clan);
                    }
                    catch
                    {
                    }

                    name = name + clan;

                    break;

            }

            i = i + clan.Length;

            bot = Convert.ToBoolean(Convert.ToByte(data.GetValue(i + 1)));


            if (clan != "")
            {

                if (protectedlv.FindItem(stripcolor(name.Replace(clan, ""))) != -1)
                {
                    players.Items.Add(new ListViewItem(new string[] { id, ping, stripcolor(name), stripcolor(clan), bot.ToString(), "0", "Spectator", "0", "0", name, "0" }, 17, players.Groups["Spectator"]));

                }
                else
                {
                    players.Items.Add(new ListViewItem(new string[] { id, ping, stripcolor(name), stripcolor(clan), bot.ToString(), "0", "Spectator", "0", "0", name, "0" }, 15, players.Groups["Spectator"]));

                }
            }
            else
            {
                if (protectedlv.FindItem(stripcolor(name)) != -1)
                {
                    players.Items.Add(new ListViewItem(new string[] { id, ping, stripcolor(name), stripcolor(clan), bot.ToString(), "0", "Spectator", "0", "0", name, "0" }, 17, players.Groups["Spectator"]));

                }
                else
                {

                  
                    
                
                                                        players.Items.Add(new ListViewItem(new string[] { id, ping, stripcolor(name), stripcolor(clan), bot.ToString(), "0", "Spectator", "0", "0", name, "0" }, 15, players.Groups["Spectator"]));                       

                        
 
                    










                }
            }
            i = i + 1;

            if (Convert.ToByte(data.GetValue(i + 1)) != 32) // 32 means end of the packet, its the default packet delimiter
            {
                goto keep;
            }


            /// now the tricky part


            i = i + 5;
            ranked = Convert.ToBoolean(Convert.ToByte(data.GetValue(i + 1)));
            i = i + 2;
            byte[] time = new byte[3] { Convert.ToByte(data.GetValue(i)), Convert.ToByte(data.GetValue(i + 1)), Convert.ToByte(data.GetValue(i + 2)) };
            i = i + 3;
            info.Items[1].SubItems[1].Text = getstatus((Convert.ToByte(data.GetValue(i + 1))));
            info.Items[3].SubItems[1].Text = getgametime(time);
            try
            {

                progressBar2.Value = Convert.ToInt16(getgametime(time).Substring(0, 2));
            }
            catch
            {

            }
            //            this.Text = ranked.ToString() + " - " + gametime + " - " + gamestatus;
            i = i + 3;

        goon:


            if ((i + 5) >= data.Length)
            {
                goto finish;
            }




            playerindex = Convert.ToInt16(data.GetValue(i + 1));
            i = i + 1;
            byte[] xp = new byte[4];
            xp[0] = Convert.ToByte(data.GetValue(i + 1));
            xp[1] = Convert.ToByte(data.GetValue(i + 2));
            xp[2] = Convert.ToByte(data.GetValue(i + 3));
            xp[3] = Convert.ToByte(data.GetValue(i + 4));
            playerxp = getxp(xp).ToString(("###0;(###0);0"));
            i = i + 5;
            team = System.Text.Encoding.Default.GetString(data, i, GetFirstOccurance(0, data, i) - i);
            i = i + team.Length;



            byte[] kil = new byte[4];
            kil[0] = Convert.ToByte(data.GetValue(i + 1));
            kil[1] = Convert.ToByte(data.GetValue(i + 2));
            kil[2] = Convert.ToByte(data.GetValue(i + 3));
            kil[3] = Convert.ToByte(data.GetValue(i + 4));
            kills = readkillsdeaths(kil).ToString();

            i = i + 4;

            kil[0] = Convert.ToByte(data.GetValue(i + 1));
            kil[1] = Convert.ToByte(data.GetValue(i + 2));
            kil[2] = Convert.ToByte(data.GetValue(i + 3));
            kil[3] = Convert.ToByte(data.GetValue(i + 4));
            deaths = readkillsdeaths(kil).ToString();
            i = i + 4;

            index = players.FindItem(playerindex.ToString());
            players.Items[index].SubItems[5].Text = playerxp;

            if (Convert.ToInt32(playerxp) >= Convert.ToInt32(info.Items[7].SubItems[2].Text))
            {
                info.Items[7].SubItems[2].Text = playerxp;
                info.Items[7].SubItems[1].Text = players.Items[index].SubItems[9].Text;
            }


            if (team == "")
            {
                team = "Spectator";
                players.Items[index].ForeColor = Color.Gray;
                if (players.Items[index].ImageIndex != 17)
                {
                    players.Items[index].ImageIndex = 16;
                }
            }
            players.Items[index].SubItems[6].Text = team;
            players.Items[index].SubItems[7].Text = kills;

            try
            {
                if (GCD(Convert.ToInt32(kills), Convert.ToInt32(deaths)).ToString().Length > 3)
                {
                    players.Items[index].SubItems[10].Text = GCD(Convert.ToInt32(kills), Convert.ToInt32(deaths)).ToString().Substring(0, 4);

                }
                else
                {
                    players.Items[index].SubItems[10].Text = GCD(Convert.ToInt32(kills), Convert.ToInt32(deaths)).ToString();
                }
            }
            catch
            {
            }

            try
            {

                if (Convert.ToInt32(kills) >= Convert.ToInt32(info.Items[8].SubItems[2].Text))
                {
                    info.Items[8].SubItems[2].Text = kills;
                    info.Items[8].SubItems[1].Text = players.Items[index].SubItems[9].Text;
                }

                if (Convert.ToInt32(kills) >= Convert.ToInt32(info.Items[18].SubItems[1].Text))
                {
                    info.Items[20].SubItems[1].Text = info.Items[19].SubItems[1].Text;
                    info.Items[20].SubItems[2].Text = info.Items[19].SubItems[2].Text;

                    info.Items[19].SubItems[1].Text = info.Items[18].SubItems[1].Text;
                    info.Items[19].SubItems[2].Text =  info.Items[18].SubItems[2].Text;

                    info.Items[18].SubItems[1].Text = kills;
                    info.Items[18].SubItems[2].Text = index.ToString();
                }

                if ((Convert.ToInt32(kills) >= Convert.ToInt32(info.Items[19].SubItems[1].Text)) & (index.ToString() != info.Items[18].SubItems[2].Text))
                {
                    info.Items[20].SubItems[1].Text = info.Items[19].SubItems[1].Text;
                    info.Items[20].SubItems[2].Text = info.Items[19].SubItems[2].Text;

                    info.Items[19].SubItems[1].Text = kills;
                    info.Items[19].SubItems[2].Text = index.ToString();
                }
                if ((Convert.ToInt32(kills) >= Convert.ToInt32(info.Items[20].SubItems[1].Text)) & (index.ToString() != info.Items[18].SubItems[2].Text) & (index.ToString() != info.Items[19].SubItems[2].Text))
                {
                    info.Items[20].SubItems[1].Text = kills;
                    info.Items[20].SubItems[2].Text = index.ToString();
                }


                players.Items[index].SubItems[8].Text = deaths;

                if (Convert.ToInt32(deaths) >= Convert.ToInt32(info.Items[9].SubItems[2].Text))
                {
                    info.Items[9].SubItems[2].Text = deaths;
                    info.Items[9].SubItems[1].Text = players.Items[index].SubItems[9].Text;
                }
                kdratio = Convert.ToDecimal(GCD(Convert.ToInt32(kills), Convert.ToInt32(deaths)));
                //            xpwhore = Convert.ToDecimal(GCD(Convert.ToInt32(playerxp), Convert.ToInt32(kills)));

                if (kdratio >= Convert.ToDecimal(info.Items[10].SubItems[2].Text.Replace(pu.Text, co.Text)))
                {
                    if (Convert.ToString(kdratio).Length > 3)
                    {
                        info.Items[10].SubItems[2].Text = Convert.ToString(kdratio).Substring(0, 4).Replace(co.Text, pu.Text);
                        info.Items[10].SubItems[1].Text = players.Items[index].SubItems[9].Text;

                    }

                    else
                    {
                        info.Items[10].SubItems[2].Text = Convert.ToString(kdratio).Replace(co.Text, pu.Text);
                        info.Items[10].SubItems[1].Text = players.Items[index].SubItems[9].Text;
                    }


                }

                if (kdratio >= Convert.ToDecimal(info.Items[15].SubItems[1].Text.Replace(pu.Text, co.Text)))
                {
                    if (Convert.ToString(kdratio).Length > 3)
                    {
                        info.Items[17].SubItems[1].Text = info.Items[16].SubItems[1].Text;
                        info.Items[17].SubItems[2].Text = info.Items[16].SubItems[2].Text;

                        info.Items[16].SubItems[1].Text = info.Items[15].SubItems[1].Text;
                        info.Items[16].SubItems[2].Text = info.Items[15].SubItems[2].Text;

                        info.Items[15].SubItems[1].Text = Convert.ToString(kdratio).Substring(0, 4).Replace(co.Text, pu.Text);
                        info.Items[15].SubItems[2].Text = index.ToString();
                    }

                    else
                    {
                        info.Items[17].SubItems[1].Text = info.Items[16].SubItems[1].Text;
                        info.Items[17].SubItems[2].Text = info.Items[16].SubItems[2].Text;

                        info.Items[16].SubItems[1].Text = info.Items[15].SubItems[1].Text;
                        info.Items[16].SubItems[2].Text = info.Items[15].SubItems[2].Text;

                        info.Items[15].SubItems[1].Text = Convert.ToString(kdratio).Replace(co.Text, pu.Text);
                        info.Items[15].SubItems[2].Text = index.ToString();
                    }


                }

                if ((kdratio >= Convert.ToDecimal(info.Items[16].SubItems[1].Text.Replace(pu.Text, co.Text))) & (index.ToString() != info.Items[15].SubItems[2].Text))
                {
                    if (Convert.ToString(kdratio).Length > 3)
                    {
                        info.Items[17].SubItems[1].Text = info.Items[16].SubItems[1].Text;
                        info.Items[17].SubItems[2].Text = info.Items[16].SubItems[2].Text;

                        info.Items[16].SubItems[1].Text = Convert.ToString(kdratio).Substring(0, 4).Replace(co.Text, pu.Text);
                        info.Items[16].SubItems[2].Text = index.ToString();
                    }

                    else
                    {
                        info.Items[17].SubItems[1].Text = info.Items[16].SubItems[1].Text;
                        info.Items[17].SubItems[2].Text = info.Items[16].SubItems[2].Text;

                        info.Items[16].SubItems[1].Text = Convert.ToString(kdratio).Replace(co.Text, pu.Text);
                        info.Items[16].SubItems[2].Text = index.ToString();
                    }


                }
                if ((kdratio >= Convert.ToDecimal(info.Items[17].SubItems[1].Text.Replace(pu.Text, co.Text))) & (index.ToString() != info.Items[15].SubItems[2].Text) & (index.ToString() != info.Items[16].SubItems[2].Text))
                {
                    if (Convert.ToString(kdratio).Length > 3)
                    {
                        info.Items[17].SubItems[1].Text = Convert.ToString(kdratio).Substring(0, 4).Replace(co.Text, pu.Text);
                        info.Items[17].SubItems[2].Text = index.ToString();
                    }

                    else
                    {
                        info.Items[17].SubItems[1].Text = Convert.ToString(kdratio).Replace(co.Text, pu.Text);
                        info.Items[17].SubItems[2].Text = index.ToString();

                    }


                }
            }
            catch
            {

            }
      //    if (xpwhore >= Convert.ToDecimal(info.Items[14].SubItems[2].Text.Replace(pu.Text,co.Text) ))
      // {
      // if (Convert.ToString(xpwhore).Length > 3)
      //  {
      //    info.Items[14].SubItems[2].Text = Convert.ToString(xpwhore).Substring(0, 4).Replace(co.Text, pu.Text);
      //      info.Items[14].SubItems[1].Text = players.Items[index].SubItems[9].Text;
      //    }
      //      else
      //      {
      //                  info.Items[14].SubItems[2].Text = Convert.ToString(xpwhore).Replace(co.Text, pu.Text);
      //            info.Items[14].SubItems[1].Text = players.Items[index].SubItems[9].Text;
      //          }
      //      }

            if (team == "strogg")
            {
                info.Items[12].SubItems[1].Text = Convert.ToString((Convert.ToInt32(info.Items[12].SubItems[1].Text) + Convert.ToInt32(kills)));
                info.Items[12].SubItems[2].Text = Convert.ToString((Convert.ToInt32(info.Items[12].SubItems[2].Text) + Convert.ToInt32(deaths)));
                players.Items[index].UseItemStyleForSubItems = false;
                players.Items[index].SubItems[6].ForeColor = Color.Red;

                if (players.Items[index].SubItems[4].Text == "False")
                {
                    players.Items[index].SubItems[4].ForeColor = Color.LightGray;
                }


            }

            else if (team == "gdf")
            {
                info.Items[11].SubItems[1].Text = Convert.ToString((Convert.ToInt32(info.Items[11].SubItems[1].Text) + Convert.ToInt32(kills)));
                info.Items[11].SubItems[2].Text = Convert.ToString((Convert.ToInt32(info.Items[11].SubItems[2].Text) + Convert.ToInt32(deaths)));
                players.Items[index].UseItemStyleForSubItems = false;
                players.Items[index].SubItems[6].ForeColor = Color.Blue;

                if (players.Items[index].SubItems[4].Text == "False")
                {
                    players.Items[index].SubItems[4].ForeColor = Color.LightGray;
                }



            }


            players.Items[index].Group = players.Groups[team];
            if (Convert.ToByte(data.GetValue(i + 1)) != 32)// 32 means end of the packet, its the default packet delimiter
            {
                goto goon;
            }

        finish:




            info.Items[6].SubItems[1].Text = players.Items.Count.ToString() + "/" + info.Items[6].SubItems[1].Text;
            info.Items[13].SubItems[1].Text = players.Items.Count.ToString();
            try
            {
                progressBar1.Value = players.Items.Count;
            }

            catch
            {

            }



            updatepicture(players.Items.Count, Convert.ToInt32(info.Items[6].SubItems[2].Text), true);

            checkspecs();


        }
       
        public decimal GCD(decimal a, decimal b)
        {
           
            if (a != 0)
            {
                if (b == 0)
                {
                    return a;
                }
                else
                {
                    return a / b;
                }

            }
            else
                return 0;
        }


        public bool spectrules()
        {
            if ((scb1.Checked == true) & (info.Items[1].SubItems[1].Text.ToLower() != "playing"))
            {
                if (Convert.ToDecimal(info.Items[13].SubItems[1].Text) >= comboBox3.Value)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (Convert.ToDecimal(info.Items[13].SubItems[1].Text) >= comboBox3.Value)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        void checkspecs()
        {

            foreach (ListViewItem spec in players.Groups["Spectator"].Items)
            {

                if (spec.SubItems[2].Text != "Connecting...")
                {


                    if (spectrules() == true)
                    {
                        addspectators(spec.Index);
                    }
                    else
                    {
                        spectslv.Items.Clear();
                    }




                }

            }

            removespectators();


        }

        void addspectators(int index)
        {
            int ex = -1;
            ex = spectslv.FindItem(players.Items[index].Text);

            if (ex == -1)
            {
                spectslv.Items.Add(new ListViewItem(new string[] { players.Items[index].Text, players.Items[index].SubItems[2].Text, "0" },16));
            }
            else
            {
                spectslv.Items[ex].SubItems[2].Text = Convert.ToString((Convert.ToInt16(spectslv.Items[ex].SubItems[2].Text) + 1));


                if (Convert.ToDecimal(spectslv.Items[ex].SubItems[2].Text) > comboBox2.Value)
                {

                    if (Convert.ToDecimal(spectslv.Items[ex].SubItems[2].Text) == numericUpDown2.Value)
                    {
                        sendrcon(rcon.Text, "say '" + warn.Text.Replace("$player", players.Items[index].SubItems[9].Text.Replace("'", "`")).Replace("$try", spectslv.Items[ex].SubItems[2].Text + "/" + numericUpDown2.Value.ToString()) + " ^1(LAST WARNING!)'", serverip.Text, Convert.ToInt32(serverport.Text), players.Items[index].SubItems[2].Text, players.Items[index].SubItems[3].Text);

                    }

                    else if (Convert.ToDecimal(spectslv.Items[ex].SubItems[2].Text) > numericUpDown2.Value)
                    {

                        sendrcon(rcon.Text, "say '" + textBox10.Text.Replace("$player", players.Items[index].SubItems[9].Text.Replace("'", "`")).Replace("$time", spectslv.Items[ex].SubItems[2].Text) + "'", serverip.Text, Convert.ToInt32(serverport.Text), players.Items[index].SubItems[2].Text, players.Items[index].SubItems[3].Text);



                        if (comboBox7.Text == "PunkBuster")
                        {
                            if (players.Items[index].SubItems[3].Text == "")
                            {
                                sendrcon(rcon.Text, "pb_sv_kick '" + players.Items[index].SubItems[2].Text + " '" + numericUpDown3.Value.ToString() + "' '" + stripcolor(textBox10.Text.Replace("$player", players.Items[index].SubItems[9].Text).Replace("$time", spectslv.Items[ex].SubItems[2].Text)) + "'", serverip.Text, Convert.ToInt32(serverport.Text), players.Items[index].SubItems[2].Text,"");
                                kickedplayers = kickedplayers + players.Items[index].SubItems[2].Text + " (Kicked AFK)" + "\r\n";

                            }
                            else
                            {
                                sendrcon(rcon.Text, "pb_sv_kick '" + players.Items[index].SubItems[2].Text.Replace(players.Items[index].SubItems[3].Text, "") + " '" + numericUpDown3.Value.ToString() + "' '" + stripcolor(textBox10.Text.Replace("$player", players.Items[index].SubItems[9].Text).Replace("$time", spectslv.Items[ex].SubItems[2].Text)) + "'", serverip.Text, Convert.ToInt32(serverport.Text), players.Items[index].SubItems[2].Text, players.Items[index].SubItems[3].Text);
                                kickedplayers = kickedplayers + players.Items[index].SubItems[2].Text + " (Kicked AFK)" + "\r\n";

                            }
                        }
                        else
                        {
                            if (players.Items[index].SubItems[3].Text == "")
                            {

                                sendrcon(rcon.Text, "admin kick '" + players.Items[index].SubItems[2].Text + "'", serverip.Text, Convert.ToInt32(serverport.Text), players.Items[index].SubItems[2].Text, "");
                                kickedplayers = kickedplayers + players.Items[index].SubItems[2].Text + " (Kicked AFK)" + "\r\n";
                            }
                            else
                            {

                                sendrcon(rcon.Text, "admin kick '" + players.Items[index].SubItems[2].Text + "'", serverip.Text, Convert.ToInt32(serverport.Text), players.Items[index].SubItems[2].Text, players.Items[index].SubItems[3].Text);
                                kickedplayers = kickedplayers + players.Items[index].SubItems[2].Text + " (Kicked AFK)" + "\r\n";

                            }
                        }
                            l8.Text = (Convert.ToInt32(l8.Text) + 1).ToString();  

                        spectslv.Items[ex].Remove();

                    }
                    else
                    {
                        sendrcon(rcon.Text, "say '" + warn.Text.Replace("$player", players.Items[index].SubItems[9].Text.Replace("'", "`")).Replace("$try", spectslv.Items[ex].SubItems[2].Text + "/" + numericUpDown2.Value.ToString()) + "'", serverip.Text, Convert.ToInt32(serverport.Text), players.Items[index].SubItems[2].Text, players.Items[index].SubItems[3].Text);

                    }

                }







            }



        }
        void removespectators()
        {

            int ex = -1;
            foreach (ListViewItem spec in spectslv.Items)
            {
                ex = -1;
                ex = players.FindItem(spec.Text);

                if (ex == -1)
                {
                    spectslv.Items.Remove(spec);
                    return;
                }



                if (players.Items[ex].SubItems[6].Text != "Spectator")
                {

                    spectslv.Items.Remove(spec);

                }


            }


        }
        private string getround(string data)
        {

            char[] r = { ' ' };
            string[] arr = data.Split(r);
            string pepe;

            if (arr[0].ToString() == "")
            {
                pepe = "Round 1 of 3";
                return pepe;

            }


            if (arr.Length + 1 <= 3)
            {
                pepe = "Round " + Convert.ToString((arr.Length + 1)) + " of 3";
            }
            else
            {
                pepe = "Round " + arr.Length.ToString() + " of 3";

            }
            data = null;
            r = null;
            arr = null;

            return pepe;

        }
        public static float getxp(byte[] data)
        {
            byte[] ext = new byte[4];


            data.CopyTo(ext, 0);


            float s = BitConverter.ToSingle(ext, 0);

            data = null;
            ext = null;
            return s;


        }
        public static float readkillsdeaths(byte[] data)
        {
            byte[] ext = new byte[4];


            data.CopyTo(ext, 0);


            int s = BitConverter.ToInt16(ext, 0);

            data = null;
            ext = null;
            return s;


        }
        private string getstatus(byte status)
        {

            switch (status)
            {
                case 1:
                    return "WarmUp";


                case 2:
                    return "Playing";

                case 4:
                    return "Reviewing";


                default:
                    return "Unknown";

            }



        }
        public static string getgametime(byte[] data)
        {
            byte[] ext = new byte[4];


            data.CopyTo(ext, 0);


            int s = BitConverter.ToInt32(ext, 0) / 1000;

            // Seconds to display
            int ss = s % 60;

            // Complete number of minutes
            int m = (s - ss) / 60;

            // Minutes to display
            int mm = m % 60;

            // Complete number of hours
            int h = (m - mm) / 60;

            // Make "hh:mm:ss"

            data = null;
            ext = null;

            return mm.ToString("D2") + ":" + ss.ToString("D2");


        }
        public static int GetFirstOccurance(byte byteToFind, byte[] byteArray, int start)
        {
            return Array.IndexOf(byteArray, byteToFind, start);
        }
        private class PartialMatch
        {
            public int Index { get; private set; }
            public int MatchLength { get; set; }

            public PartialMatch(int index)
            {
                Index = index;
                MatchLength = 1;
            }
        }

        private static int IndexOf(byte[] arrayToSearch, byte[] splitToFind)
        {
            if (splitToFind.Length == 0
              || arrayToSearch.Length == 0
              || arrayToSearch.Length < splitToFind.Length)
                return -1;

            List<PartialMatch> partialMatches = new List<PartialMatch>();

            for (int i = 0; i < arrayToSearch.Length; i++)
            {
                for (int j = partialMatches.Count - 1; j >= 0; j--)
                    if (arrayToSearch[i] == splitToFind[partialMatches[j].MatchLength])
                    {
                        partialMatches[j].MatchLength++;

                        if (partialMatches[j].MatchLength == splitToFind.Length)
                            return partialMatches[j].Index;
                    }
                    else
                        partialMatches.Remove(partialMatches[j]);

                if (arrayToSearch[i] == splitToFind[0])
                {
                    if (splitToFind.Length == 1)
                        return i;
                    else
                        partialMatches.Add(new PartialMatch(i));
                }
            }

            return -1;
        }

        private void TrayBtn_clicked(object sender, EventArgs e) // tray event
        {
            this.WindowState = FormWindowState.Minimized;
            Application.DoEvents();
            this.Hide();

            this.notifyIcon1.Visible = true;
        }

        private void notifyIcon1_Click(object sender, EventArgs e)
        {
            this.Show();
            this.notifyIcon1.Visible = false;
            this.WindowState = FormWindowState.Normal;
        }

        private void checkBox41_CheckedChanged(object sender, EventArgs e)
        {
            checkBox42.Checked = false;
            players.GridLines = checkBox41.Checked;
            protectedlv.GridLines = checkBox41.Checked;
            serversettings.GridLines = checkBox41.Checked;
            spectslv.GridLines = checkBox41.Checked;
            ListViewHelper.EnableDoubleBuffer(players);
            ListViewHelper.EnableDoubleBuffer(serversettings);
            ListViewHelper.EnableDoubleBuffer(protectedlv);
            ListViewHelper.EnableDoubleBuffer(spectslv);

        }
        void updatepicture2(int onoff)
        {
            try
            {
                pictureBox5.SuspendLayout();
                pictureBox5.Image = null;

                using (Bitmap bitmap12 = new Bitmap(pictureBox5.Width, pictureBox5.Height))
                using (Graphics graphics5 = Graphics.FromImage(bitmap12))
                using (LinearGradientBrush brush5 = new LinearGradientBrush(
                    new Rectangle(0, 0, pictureBox5.Width, pictureBox5.Height),
                    button60.BackColor,
                    button61.BackColor,
                    LinearGradientMode.Horizontal))
                using (LinearGradientBrush brush25 = new LinearGradientBrush(
                    new Rectangle(0, 0, 128, pictureBox5.Height),
                    Color.Transparent,
                    button49.BackColor,
                    LinearGradientMode.Horizontal))
                using (Pen pene5 = new Pen(Color.Black, 1))
                using (Pen pene25 = new Pen(Color.Red, 1))
                {
                    brush5.SetSigmaBellShape(Convert.ToSingle(blend.Value / 100), Convert.ToSingle(speed.Value / 100));
                    graphics5.FillRectangle(brush5, new Rectangle(128, 0, pictureBox5.Width, pictureBox5.Height));
                    graphics5.DrawLine(pene5, new Point(128, 0), new Point(128, 64));
                    SolidBrush s5 = new SolidBrush(Color.Black);
                    s5.Color = textBox31.ForeColor;
                    graphics5.DrawString(refirma(textBox31.Text), textBox31.Font, s5, new Point(130, 2));
                    s5.Color = textBox30.ForeColor;
                    graphics5.DrawString(refirma(textBox30.Text), textBox30.Font, s5, new Point(130, 15));
                    s5.Color = textBox29.ForeColor;
                    graphics5.DrawString(refirma(textBox29.Text), textBox29.Font, s5, new Point(130, 28));
                    s5.Color = textBox28.ForeColor;
                    graphics5.DrawString(refirma(textBox28.Text), textBox28.Font, s5, new Point(130, 41));
                    graphics5.DrawImage(imageList2.Images[onoff], pictureBox5.Width - 40, 35);
                    s5.Dispose();

                    try
                    {
                        graphics5.DrawImage(Image.FromFile(Application.StartupPath + "\\maps\\" + info.Items[2].SubItems[1].Text + ".jpg"), 0, 0, 128, 64);
                    }
                    catch
                    {
                        graphics5.DrawImage(Image.FromFile(Application.StartupPath + "\\maps\\unknown.jpg"), 0, 0, 128, 64);
                    }
                    graphics5.FillRectangle(brush25, new Rectangle(0, 0, 128, pictureBox5.Height));
                    graphics5.DrawRectangle(pene5, 0, 0, pictureBox5.Width - 1, pictureBox5.Height - 1);
                    pictureBox5.Image = Image.FromHbitmap(bitmap12.GetHbitmap());
                    pene5.Dispose();
                    pene25.Dispose();
                    bitmap12.Dispose();
                    graphics5.Dispose();
                    brush5.Dispose();
                    brush25.Dispose();
                    pictureBox5.ResumeLayout();
                }
            }
            catch
            {
                pictureBox5.Image = null;
                pictureBox5.ResumeLayout();
            }
        }
        private void updatepicture(int nuevo, int viejo, bool color)
        {

            if (color == false)
            {

                if (checkBox38.Checked == false)
                {
                    grafico(nuevo, viejo, false);
                }
                else
                {
                    grafico(nuevo, viejo, true);

                }
            }
            else
            {
                grafico(nuevo, viejo, false);

            }
            try
            {
                pictureBox1.SuspendLayout();
                pictureBox1.Image = null;
                pictureBox1.Invalidate();
                int lala = pictureBox1.Width;
                if (checkBox39.Checked == true)
                {
                    lala = lala + pictureBox3.Width;
                }


                using (Bitmap bitmap = new Bitmap(lala, pictureBox1.Height))

                using (Bitmap bitmap2 = new Bitmap(button17.Width, button17.Height))

                using (Graphics graphics = Graphics.FromImage(bitmap))
                using (Font myFont = new Font("Arial", 8))

                using (LinearGradientBrush brush = new LinearGradientBrush(
                    new Rectangle(0, 0, pictureBox1.Width, pictureBox1.Height),
                   button60.BackColor,
                    button61.BackColor,
                    LinearGradientMode.Horizontal))
                using (LinearGradientBrush brush2 = new LinearGradientBrush(
                    new Rectangle(0, 0, 128, pictureBox1.Height),
                    Color.Transparent,
                    button49.BackColor,
                    LinearGradientMode.Horizontal))


                using (Pen pene = new Pen(Color.Black, 1))
                using (Pen pene2 = new Pen(Color.Red, 1))
                {

                    brush.SetSigmaBellShape(Convert.ToSingle(blend.Value / 100), Convert.ToSingle(speed.Value / 100));



                    graphics.FillRectangle(brush, new Rectangle(128, 0, pictureBox1.Width, pictureBox1.Height));
                    graphics.DrawLine(pene, new Point(128, 0), new Point(128, 64));
                    //   graphics.DrawString(sname.Text, myFont2, Brushes.Black , new Point(130, 2));
                    SolidBrush s = new SolidBrush(Color.Black);
                    s.Color = textBox31.ForeColor;
                    graphics.DrawString(refirma(textBox31.Text), textBox31.Font, s, new Point(130, 2));
                    s.Color = textBox30.ForeColor;

                    graphics.DrawString(refirma(textBox30.Text), textBox30.Font, s, new Point(130, 15));


                    //                         graphics.DrawString(sstatus.Text + " - " + stime.Text + " - " + players.Text   , myFont, Brushes.Black, new Point(130, 15));
                    //  graphics.DrawString(label52.Text + " - " + label54.Text, myFont, Brushes.Black, new Point(130, 28));
                    s.Color = textBox29.ForeColor;

                    graphics.DrawString(refirma(textBox29.Text), textBox29.Font, s, new Point(130, 28));
                    s.Color = textBox28.ForeColor;
                    graphics.DrawString(refirma(textBox28.Text), textBox28.Font, s, new Point(130, 41));

                    // graphics.DrawString("User: " + textBox15.Text + " - " + DateTime.Now.ToUniversalTime() + " UTC", myFont, Brushes.Black, new Point(130, 41));
                    graphics.DrawImage(imageList2.Images[Convert.ToInt32(!color)], pictureBox1.Width - 40, 35);

                    if (checkBox39.Checked == true)
                    {
                        graphics.DrawImage(pictureBox3.Image, pictureBox1.Width, 0, pictureBox3.Width, pictureBox3.Height);
                    }

                    s.Dispose();
                    //  graphics.DrawString("ON", myFont3, Brushes.LightSkyBlue    , new Point(pictureBox1.Width - 30, 42) );
                    // graphics.DrawLine(pene2, new Point(pictureBox1.Width - 38, 36), new Point(pictureBox1.Width - 38, 60));
                    // graphics.DrawLine(pene2, new Point(pictureBox1.Width - 38, 36), new Point(pictureBox1.Width , 36)); 


                    //                    graphics.DrawString(stime.Text, myFont, Brushes.Black, new Point(190, 15));
                    //                  graphics.DrawString(players.Text , myFont, Brushes.Black, new Point(230, 15));

                    try
                    {
                        graphics.DrawImage(Image.FromFile(Application.StartupPath + "\\maps\\" + info.Items[2].SubItems[1].Text.ToLower() + ".jpg"), 0, 0, 128, 64);
                    }
                    catch
                    {
                        graphics.DrawImage(Image.FromFile(Application.StartupPath + "\\maps\\unknown.jpg"), 0, 0, 128, 64);

                    }
                    graphics.FillRectangle(brush2, new Rectangle(0, 0, 128, pictureBox1.Height));

                    graphics.DrawRectangle(pene, 0, 0, pictureBox1.Width - 1, pictureBox1.Height - 1);

                    if (color == false)
                    {

                        if (checkBox38.Checked == true)
                        {
                            pictureBox1.Image = Image.FromHbitmap(MakeGrayscale3(bitmap).GetHbitmap());
                        }
                        else
                        {
                            pictureBox1.Image = Image.FromHbitmap(bitmap.GetHbitmap());

                        }
                    }

                    else
                    {
                        pictureBox1.Image = Image.FromHbitmap(bitmap.GetHbitmap());

                    }





                    pene.Dispose();
                    pene2.Dispose();
                    myFont.Dispose();
                    bitmap.Dispose();
                    bitmap2.Dispose();
                    graphics.Dispose();
                    brush.Dispose();
                    brush2.Dispose();

                    pictureBox1.ResumeLayout();

                    pictureBox1.Image.Save(Application.StartupPath + "\\" + textBox15.Text + ".png", ImageFormat.Png);



                }

                //     return Image.FromFile(Application.StartupPath  + "\\" + mapa +".jpg");




            }
            catch
            {
                pictureBox1.Image = null;
                pictureBox1.ResumeLayout();

            }

        }
        private string refirma(string que)
        {

            que = que.Replace("$servername", stripcolor(info.Items[0].SubItems[1].Text));
            que = que.Replace("$servertime", info.Items[3].SubItems[1].Text);
            que = que.Replace("$serverstatus", info.Items[1].SubItems[1].Text);
            que = que.Replace("$serverplayers", info.Items[6].SubItems[1].Text);
            que = que.Replace("$user", textBox15.Text);
            que = que.Replace("$servermap", info.Items[2].SubItems[1].Text);
            que = que.Replace("$serverround", info.Items[4].SubItems[1].Text);
            que = que.Replace("$serverscore", info.Items[5].SubItems[1].Text);
            que = que.Replace("$utctime", DateTime.Parse(DateTime.UtcNow.ToUniversalTime().ToString()).ToString("MM/dd/yy hh:mm:ss tt"));














            return que;

        }
        private void grafico(int nuevo, int yaya, bool onoff)
        {
            try
            {
                pictureBox3.SuspendLayout();
                pictureBox3.Invalidate();

                using (Bitmap bitmap = new Bitmap(pictureBox3.Width, pictureBox3.Height))

                using (Graphics graphics = Graphics.FromImage(bitmap))



                using (LinearGradientBrush brush = new LinearGradientBrush(
                        new Rectangle(0, 0, pictureBox3.Width, pictureBox3.Height),
                      button46.BackColor,
                      button47.BackColor,
                        LinearGradientMode.Vertical))

                using (Pen pene2 = new Pen(button45.BackColor, 1))
                using (Pen pene = new Pen(button48.BackColor, 1))
                using (Pen pene3 = new Pen(Color.Black, 1))
                {
                    try
                    {
                        // graphics.Clear(Color.Black);


                        graphics.FillRectangle(brush, new Rectangle(0, 0, pictureBox3.Width, pictureBox3.Height));
                        SolidBrush s = new SolidBrush(button48.BackColor);

                        try
                        {

                            graphics.DrawImage(pictureBox3.Image, Convert.ToSingle(step.Value), 0, pictureBox3.Width, pictureBox3.Height);

                        }
                        catch
                        {

                        }
                        graphics.FillRectangle(brush, new Rectangle(pictureBox3.Width - 25, 0, 25, pictureBox3.Height));

                        if (nuevo <= 9)
                        {
                            graphics.DrawString(nuevo.ToString(), textBox30.Font, s, pictureBox3.Width - 12, pictureBox3.Height - 14);
                        }
                        else
                        {
                            graphics.DrawString(nuevo.ToString(), textBox30.Font, s, pictureBox3.Width - 17, pictureBox3.Height - 14);

                        }
                        graphics.DrawRectangle(pene3, -1, 0, pictureBox3.Width, pictureBox3.Height - 1);


                    }
                    catch
                    {

                    }

                    //     label25.Text = nuevo.ToString();

                    if (nuevo == 0)
                    {
                        graphics.DrawLine(pene2, new Point(0, pictureBox3.Height - 14), new Point(pictureBox3.Width, pictureBox3.Height - 14));
                        graphics.DrawLine(pene2, new Point(0, pictureBox3.Height - 29), new Point(pictureBox3.Width, pictureBox3.Height - 29));
                        graphics.DrawLine(pene2, new Point(0, pictureBox3.Height - 44), new Point(pictureBox3.Width, pictureBox3.Height - 44));
                        graphics.DrawLine(pene2, new Point(0, pictureBox3.Height - 59), new Point(pictureBox3.Width, pictureBox3.Height - 59));

                    }
                    else
                    {
                        graphics.DrawLine(pene2, new Point(pictureBox3.Width - 25, pictureBox3.Height - 14), new Point(pictureBox3.Width, pictureBox3.Height - 14));
                        graphics.DrawLine(pene2, new Point(pictureBox3.Width - 25, pictureBox3.Height - 29), new Point(pictureBox3.Width, pictureBox3.Height - 29));
                        graphics.DrawLine(pene2, new Point(pictureBox3.Width - 25, pictureBox3.Height - 44), new Point(pictureBox3.Width, pictureBox3.Height - 44));
                        graphics.DrawLine(pene2, new Point(pictureBox3.Width - 25, pictureBox3.Height - 59), new Point(pictureBox3.Width, pictureBox3.Height - 59));
                    }

                    yaya = yaya + 1;
                    graphics.DrawLine(pene, new Point((pictureBox3.Width - 25) + Convert.ToInt32(step.Value), (pictureBox3.Height - (Convert.ToInt32(info.Items[13].SubItems[2].Text) * pictureBox3.Height) / yaya)), new Point((pictureBox3.Width - 25), (pictureBox3.Height - ((nuevo * pictureBox3.Height) / yaya))));





                    pictureBox3.Image = Image.FromHbitmap(bitmap.GetHbitmap());


                    if (onoff == true)
                    {




                        Bitmap b = (Bitmap)pictureBox3.Image;
                        pictureBox3.Image = Image.FromHbitmap(MakeGrayscale3(b).GetHbitmap());
                        b.Dispose();


                    }



                    pene.Dispose();
                    pene2.Dispose();
                    pene3.Dispose();
                    bitmap.Dispose();
                    graphics.Dispose();
                    brush.Dispose();

                    pictureBox3.ResumeLayout();




                }

                //     return Image.FromFile(Application.StartupPath  + "\\" + mapa +".jpg");




            }
            catch
            {
                pictureBox3.Image = null;
                pictureBox3.ResumeLayout();

            }
        }
        public static Bitmap MakeGrayscale3(Bitmap original)
        {
            //create a blank bitmap the same size as original
            Bitmap newBitmap = new Bitmap(original.Width, original.Height);

            //get a graphics object from the new image
            Graphics g = Graphics.FromImage(newBitmap);

            //create the grayscale ColorMatrix
            ColorMatrix colorMatrix = new ColorMatrix(
               new float[][] 
      {
         new float[] {.3f, .3f, .3f, 0, 0},
         new float[] {.59f, .59f, .59f, 0, 0},
         new float[] {.11f, .11f, .11f, 0, 0},
         new float[] {0, 0, 0, 1, 0},
         new float[] {0, 0, 0, 0, 1}
      });

            //create some image attributes
            ImageAttributes attributes = new ImageAttributes();

            //set the color matrix attribute
            attributes.SetColorMatrix(colorMatrix);

            //draw the original image on the new image
            //using the grayscale color matrix
            g.DrawImage(original, new Rectangle(0, 0, original.Width, original.Height),
               0, 0, original.Width, original.Height, GraphicsUnit.Pixel, attributes);

            //dispose the Graphics object
            g.Dispose();
            return newBitmap;
        }
        private void checkBox42_CheckedChanged(object sender, EventArgs e)
        {
            checkBox41.Checked = false;
            players.ShowGroups = checkBox42.Checked;

        }


        private string stripcolor(string pt)
        {



            for (int i = 0; i <= 9; i++)
            {
                pt = pt.Replace("^" + i.ToString(), "");
            }

            pt = pt.Replace("^^", "");
            pt = pt.Replace("^a", "");
            pt = pt.Replace("^b", "");
            pt = pt.Replace("^c", "");
            pt = pt.Replace("^d", "");
            pt = pt.Replace("^e", "");
            pt = pt.Replace("^f", "");
            pt = pt.Replace("^g", "");
            pt = pt.Replace("^h", "");
            pt = pt.Replace("^i", "");
            pt = pt.Replace("^j", "");
            pt = pt.Replace("^l", "");
            pt = pt.Replace("^k", "");
            pt = pt.Replace("^m", "");
            pt = pt.Replace("^n", "");
            pt = pt.Replace("^o", "");
            pt = pt.Replace("^p", "");
            pt = pt.Replace("^q", "");
            pt = pt.Replace("^r", "");
            pt = pt.Replace("^s", "");
            pt = pt.Replace("^t", "");
            pt = pt.Replace("^u", "");
            pt = pt.Replace("^v", "");
            pt = pt.Replace("^w", "");
            pt = pt.Replace("^x", "");
            pt = pt.Replace("^y", "");
            pt = pt.Replace("^z", "");

            pt = pt.Replace("^A", "");
            pt = pt.Replace("^B", "");
            pt = pt.Replace("^C", "");
            pt = pt.Replace("^D", "");
            pt = pt.Replace("^E", "");
            pt = pt.Replace("^F", "");
            pt = pt.Replace("^G", "");
            pt = pt.Replace("^H", "");
            pt = pt.Replace("^I", "");
            pt = pt.Replace("^J", "");
            pt = pt.Replace("^L", "");
            pt = pt.Replace("^K", "");
            pt = pt.Replace("^M", "");
            pt = pt.Replace("^N", "");
            pt = pt.Replace("^O", "");
            pt = pt.Replace("^P", "");
            pt = pt.Replace("^Q", "");
            pt = pt.Replace("^R", "");
            pt = pt.Replace("^S", "");
            pt = pt.Replace("^T", "");
            pt = pt.Replace("^U", "");
            pt = pt.Replace("^V", "");
            pt = pt.Replace("^W", "");
            pt = pt.Replace("^X", "");
            pt = pt.Replace("^Y", "");
            pt = pt.Replace("^Z", "");
            pt = pt.Replace("^?", "");
            pt = pt.Replace("^>", "");
            pt = pt.Replace("^<", "");
            pt = pt.Replace("^[", "");
            pt = pt.Replace("^]", "");
            pt = pt.Replace("^(", "");
            pt = pt.Replace("^)", "");
            pt = pt.Replace("^!", "");
            pt = pt.Replace("^$", "");
            pt = pt.Replace("^%", "");
            pt = pt.Replace("^&", "");
            pt = pt.Replace("^/", "");
            pt = pt.Replace("^=", "");
            pt = pt.Replace("^ª", "");
            pt = pt.Replace("^º", "");
            pt = pt.Replace("^-", "");
            pt = pt.Replace("^@", "");
            pt = pt.Replace("^+", "");
            pt = pt.Replace("^*", "");
            pt = pt.Replace("^Ç", "");
            pt = pt.Replace("^|", "");
            pt = pt.Replace("^l", "");
            pt = pt.Replace("^:", "");
            pt = pt.Replace("^.", "");
            pt = pt.Replace("^;", "");
            pt = pt.Replace("^{", "");
            pt = pt.Replace("^}", "");
            pt = pt.Replace(@"^\", "");
            pt = pt.Replace("^'", "");
            pt = pt.Replace("^´", "");
            pt = pt.Replace("^`", "");
            pt = pt.Replace("^_", "");
            pt = pt.Replace("^Ç", "");
            pt = pt.Replace("^¨", "");
            pt = pt.Replace("^€", "");
            pt = pt.Replace("^~", "");
            return pt;
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
        private void errorlv_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                textBox44.Text = errorlv.SelectedItems[0].SubItems[2].Text;
            }
            catch
            {

            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            resetinfo();
            kickedplayers = "";
            ReleaseUnusedMemory();
            Application.DoEvents();

            if (button17.Text == "Start")
            {
                timer1.Interval = Convert.ToInt32(toolStripComboBox1.Text) * 1000;

                if (statsyes.Checked == true)
                {
                    button43_Click(null, null);
                }

                progressBar1.Style = ProgressBarStyle.Blocks;
                progressBar2.Style = ProgressBarStyle.Blocks;

                pictureBox3.SuspendLayout();
                pictureBox3.Image = null;

                updatepicture(0, 0, true);
                pictureBox3.ResumeLayout();

                spectslv.Items.Clear();
                try
                {
                    if (checkBox53.Checked)
                    {
                        sendrcon2(rcon.Text, "pb_sv_msgprefix", serverip.Text, Convert.ToInt32(serverport.Text), "", "");
                    }
                }

                catch
                {

                }

                if (checkBox7.Checked == true)
                {
                    sendrcon(rcon.Text, "say '" + textBox11.Text.Replace("$localtime", DateTime.Parse(DateTime.UtcNow.ToUniversalTime().ToString()).ToString("MM/dd/yy hh:mm:ss tt")).Replace("$user", textBox15.Text) + "'", serverip.Text, Convert.ToInt32(serverport.Text), "","");
                }


                scan2();
                if (checkBox14.Checked == true)
                {
                    pbnow.Items.Clear();
                    sendrcon2(rcon.Text, "pb_sv_plist", serverip.Text, Convert.ToInt32(serverport.Text), "", "");
                }
                    button17.Text = "Stop";
                button17.Image = imageList2.Images[1];
                timer1.Enabled = true;
                timer2.Enabled = true;


            }
            else
            {
                progressBar1.Style = ProgressBarStyle.Marquee;
                progressBar2.Style = ProgressBarStyle.Marquee;

                timer1.Enabled = false;
                timer2.Enabled = false;
                spectslv.Items.Clear();
                if (checkBox8.Checked == true)
                {
                    sendrcon(rcon.Text, "say '" + textBox12.Text.Replace("$localtime", DateTime.Parse(DateTime.UtcNow.ToUniversalTime().ToString()).ToString("MM/dd/yy hh:mm:ss tt")).Replace("$user", textBox15.Text) + "'", serverip.Text, Convert.ToInt32(serverport.Text), "","");
                }
                button17.Text = "Start";
                button17.Image = imageList2.Images[0];

                pictureBox3.SuspendLayout();
                pictureBox3.Image = null;

                updatepicture(0, 0, false);
                pictureBox3.ResumeLayout();





            }
        }

        private string replacemsg(string what)
        {

            what = what.Replace("$utctime", DateTime.Parse(DateTime.UtcNow.ToUniversalTime().ToString()).ToString("MM/dd/yy hh:mm:ss tt"));
            what = what.Replace("$kicknumber", comboBox3.Value.ToString());
            what = what.Replace("$kicktime", numericUpDown2.Value.ToString());
            what = what.Replace("$numberofwarnings", Convert.ToString((numericUpDown2.Value + 1) - comboBox2.Value));
            what = what.Replace("$user", textBox15.Text);
            return what;

        }
        public static void ReleaseUnusedMemory()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            SetProcessWorkingSetSize(
                System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1);
        }
        private string loadhtmlbase(string filename)
        {
            FileStream fs = new FileStream(Application.StartupPath + "\\" + filename, FileMode.Open, FileAccess.Read);
            StreamReader m_streamReader = new StreamReader(fs);
            string html1 = "";
            m_streamReader.BaseStream.Seek(0, SeekOrigin.Begin);
            string strLine = m_streamReader.ReadLine();
            while (strLine != null)
            {
                html1 = html1 + strLine + "\r\n";
                strLine = m_streamReader.ReadLine();
            }
            m_streamReader.Close();
            fs.Dispose();
            m_streamReader.Dispose();
            return html1;

        }
        private void sessiontohtml()
        {
            Application.DoEvents();
            char[] r = { '\t' };
            string ntbhtmlcode = "";
            string htmlbody = loadhtmlbase("sessionstable.dat");

            try
            {
                FileStream fs = new FileStream(Application.StartupPath + "\\stats\\sessions.txt", FileMode.Open, FileAccess.Read);
                StreamReader m_streamReader = new StreamReader(fs);

                // Write to the file using StreamWriter class 
                m_streamReader.BaseStream.Seek(0, SeekOrigin.Begin);
                string strLine = m_streamReader.ReadLine();

                FileStream fs19 = new FileStream(Application.StartupPath + "\\stats\\sessions.html", FileMode.Create, FileAccess.Write);
                StreamWriter m_streamWriter = new StreamWriter(fs19);
                m_streamWriter.Flush();

                m_streamWriter.BaseStream.Seek(0, SeekOrigin.Begin);



                htmlbody = htmlbody.Replace("$$$headertag$$$", colorhtml(textBox67.Text + "Recorded Sessions"));


                while (strLine != null)
                {


                    string[] arr = strLine.Split(r);
                    ntbhtmlcode = ntbhtmlcode + "<tr>" + "\r\n";
                    ntbhtmlcode = ntbhtmlcode + "<td><div align=\"center\">" + arr[0] + "</FONT></div></td>" + "\r\n";
                    ntbhtmlcode = ntbhtmlcode + "<td><div align=\"center\">" + arr[1] + "</FONT></div></td>" + "\r\n";
                    ntbhtmlcode = ntbhtmlcode + "<td><div align=\"center\"><a href=" + arr[2] + ">link</a></div></td>" + "\r\n";
                    ntbhtmlcode = ntbhtmlcode + "</tr>" + "\r\n";
                    strLine = m_streamReader.ReadLine();




                }

                htmlbody = htmlbody.Replace("$$$$$NTBHTMLCODE$$$$$", ntbhtmlcode);

                m_streamWriter.Write(htmlbody);
                m_streamWriter.WriteLine();


                m_streamWriter.Flush();
                m_streamWriter.Close();
                fs19.Dispose();
                m_streamWriter.Dispose();

                m_streamReader.Close();
                fs.Dispose();
                m_streamReader.Dispose();

            }
            catch
            {
            }
        }
        private void addsession(string loghtml)
        {
            try
            {
                FileStream session = new FileStream(Application.StartupPath + "\\stats\\sessions.txt", FileMode.Append, FileAccess.Write);
                StreamWriter m_sessionWriter = new StreamWriter(session);
                m_sessionWriter.Flush();

                m_sessionWriter.Write(DateTime.Parse(DateTime.UtcNow.ToUniversalTime().ToString()).ToString("MM/dd/yy hh:mm:ss tt") + "\t" + info.Items[2].SubItems[2].Text + "\t" + loghtml);
                m_sessionWriter.WriteLine();
                m_sessionWriter.Close();
                m_sessionWriter.Dispose();
                session.Dispose();
                sessiontohtml();
            }
            catch
            {

            }

        }
        private void clonalistview1()
        {

            Application.DoEvents();
            string s = "0";
            string g = "0";
            string htmlbody = loadhtmlbase("sessions.dat");
            string ntbhtmlcode = "";
            string loghtml = "LOG_" + DateTime.Parse(DateTime.UtcNow.ToUniversalTime().ToString()).ToString("yyyyMMdd_hhmmss");


            addsession(loghtml + ".html");
            log.AppendText(DateTime.Now.ToLocalTime() + "\t" + "Updating Session HTML" + "\r\n");
            log.AppendText(DateTime.Now.ToLocalTime() + "\t" + "Updating Session TXT" + "\r\n");

            try
            {
                FileStream fs19 = new FileStream(Application.StartupPath + "\\stats\\" + loghtml + ".html", FileMode.Create, FileAccess.Write);
                StreamWriter m_streamWriter = new StreamWriter(fs19);
                m_streamWriter.Flush();
                m_streamWriter.BaseStream.Seek(0, SeekOrigin.Begin);

                htmlbody = htmlbody.Replace("$$mapa$$", info.Items[2].SubItems[1].Text.ToLower());
                htmlbody = htmlbody.Replace("$$$servername$$$", colorhtml(info.Items[0].SubItems[2].Text));
                htmlbody = htmlbody.Replace("$$$mapname$$$", colorhtml(textBox61.Text + info.Items[2].SubItems[1].Text));
                htmlbody = htmlbody.Replace("$$$players$$$", colorhtml(textBox61.Text + info.Items[6].SubItems[1].Text));
                htmlbody = htmlbody.Replace("$$$status$$$", colorhtml(textBox62.Text + info.Items[1].SubItems[1].Text));
                htmlbody = htmlbody.Replace("$$$time$$$", colorhtml(textBox62.Text +info.Items[3].SubItems[1].Text));
                htmlbody = htmlbody.Replace("$$$round$$$", colorhtml(textBox63.Text +info.Items[4].SubItems[1].Text));
                htmlbody = htmlbody.Replace("$$$score$$$", colorhtml(textBox63.Text +info.Items[5].SubItems[1].Text));
                htmlbody = htmlbody.Replace("$$$headertag$$$", colorhtml(textBox60.Text + "Stats Information. Last Update: " + DateTime.Parse(DateTime.UtcNow.ToUniversalTime().ToString()).ToString("MM/dd/yy hh:mm:ss tt") + " UTC"));

                log.AppendText(DateTime.Now.ToLocalTime() + "\t" + "Updating Global Database" + "\r\n");
                int i = -1;
                foreach (ListViewItem h in players.Items)
                {

                    



                    s = "0";
                    g = "0";
                    i = -1;

                    if (h.SubItems[3].Text != "")
                    {

                        i = globallv.FindItem(h.SubItems[2].Text.ToLower().Replace(h.SubItems[3].Text.ToLower(), ""));
                    }

                    else
                    {
                        i = globallv.FindItem(h.SubItems[2].Text.ToLower());

                    }


                    if (h.SubItems[6].Text == "strogg")
                    {
                        s = "1";
                        g = "0";
                    }
                    else if (h.SubItems[6].Text.ToLower() == "gdf")
                    {
                        s = "0";
                        g = "1";
                    }



                    if (i == -1)
                    {
                        if (h.SubItems[2].Text != "Connecting...")
                        {

                            if (Convert.ToString(GCD(Convert.ToInt32(h.SubItems[7].Text), Convert.ToInt32(h.SubItems[8].Text))).Length > 3)
                            {

                                if (h.SubItems[3].Text != "")
                                {

                                    globallv.Items.Add(new ListViewItem(new string[] { h.SubItems[2].Text.ToLower().Replace(h.SubItems[3].Text.ToLower(), ""), h.SubItems[9].Text, h.SubItems[3].Text, h.SubItems[5].Text, h.SubItems[7].Text, h.SubItems[8].Text, Convert.ToString(GCD(Convert.ToInt32(h.SubItems[7].Text), Convert.ToInt32(h.SubItems[8].Text))).Substring(0, 4).Replace(",", "."), s, g, h.SubItems[4].Text, DateTime.Parse(DateTime.UtcNow.ToUniversalTime().ToString()).ToString("MM/dd/yy hh:mm:ss tt"), Convert.ToString(Convert.ToInt32(s) + Convert.ToInt32(g))}));
                                }
                                else
                                {
                                    globallv.Items.Add(new ListViewItem(new string[] { h.SubItems[2].Text.ToLower(), h.SubItems[9].Text, h.SubItems[3].Text, h.SubItems[5].Text, h.SubItems[7].Text, h.SubItems[8].Text, Convert.ToString(GCD(Convert.ToInt32(h.SubItems[7].Text), Convert.ToInt32(h.SubItems[8].Text))).Substring(0, 4).Replace(",", "."), s, g, h.SubItems[4].Text, DateTime.Parse(DateTime.UtcNow.ToUniversalTime().ToString()).ToString("MM/dd/yy hh:mm:ss tt"), Convert.ToString(Convert.ToInt32(s) + Convert.ToInt32(g)) }));

                                }
                            }

                            else
                            {
                                if (h.SubItems[3].Text != "")
                                {

                                    globallv.Items.Add(new ListViewItem(new string[] { h.SubItems[2].Text.ToLower().Replace(h.SubItems[3].Text.ToLower(), ""), h.SubItems[9].Text, h.SubItems[3].Text, h.SubItems[5].Text, h.SubItems[7].Text, h.SubItems[8].Text, Convert.ToString(GCD(Convert.ToInt32(h.SubItems[7].Text), Convert.ToInt32(h.SubItems[8].Text))).Replace(",", ","), s, g, h.SubItems[4].Text, DateTime.Parse(DateTime.UtcNow.ToUniversalTime().ToString()).ToString("MM/dd/yy hh:mm:ss tt"), Convert.ToString(Convert.ToInt32(s) + Convert.ToInt32(g)) }));
                                }
                                else
                                {
                                    globallv.Items.Add(new ListViewItem(new string[] { h.SubItems[2].Text.ToLower(), h.SubItems[9].Text, h.SubItems[3].Text, h.SubItems[5].Text, h.SubItems[7].Text, h.SubItems[8].Text, Convert.ToString(GCD(Convert.ToInt32(h.SubItems[7].Text), Convert.ToInt32(h.SubItems[8].Text))).Replace(",", "."), s, g, h.SubItems[4].Text, DateTime.Parse(DateTime.UtcNow.ToUniversalTime().ToString()).ToString("MM/dd/yy hh:mm:ss tt"), Convert.ToString(Convert.ToInt32(s) + Convert.ToInt32(g)) }));

                                }

                            }




                        }

                    }
                    else
                    {
                        //agregue esto
                        globallv.Items[i].SubItems[1].Text = h.SubItems[9].Text;
                        globallv.Items[i].SubItems[2].Text = h.SubItems[3].Text;

                        globallv.Items[i].SubItems[3].Text = Convert.ToString(Convert.ToInt32(globallv.Items[i].SubItems[3].Text) + Convert.ToInt32(h.SubItems[5].Text));
                        globallv.Items[i].SubItems[4].Text = Convert.ToString(Convert.ToInt32(globallv.Items[i].SubItems[4].Text) + Convert.ToInt32(h.SubItems[7].Text));
                        globallv.Items[i].SubItems[5].Text = Convert.ToString(Convert.ToInt32(globallv.Items[i].SubItems[5].Text) + Convert.ToInt32(h.SubItems[8].Text));
                        globallv.Items[i].SubItems[7].Text = Convert.ToString(Convert.ToInt32(globallv.Items[i].SubItems[7].Text) + Convert.ToInt32(s));
                        globallv.Items[i].SubItems[8].Text = Convert.ToString(Convert.ToInt32(globallv.Items[i].SubItems[8].Text) + Convert.ToInt32(g));
                        globallv.Items[i].SubItems[9].Text = h.SubItems[4].Text;
                        globallv.Items[i].SubItems[10].Text = DateTime.Parse(DateTime.UtcNow.ToUniversalTime().ToString()).ToString("MM/dd/yy hh:mm:ss tt");
                        globallv.Items[i].SubItems[11].Text = Convert.ToString(Convert.ToInt32(globallv.Items[i].SubItems[7].Text) + Convert.ToInt32(globallv.Items[i].SubItems[8].Text)); 

                        if (Convert.ToString(GCD(Convert.ToInt32(globallv.Items[i].SubItems[4].Text), Convert.ToInt32(globallv.Items[i].SubItems[5].Text))).Length > 3)
                        {
                            globallv.Items[i].SubItems[6].Text = Convert.ToString(GCD(Convert.ToInt32(globallv.Items[i].SubItems[4].Text), Convert.ToInt32(globallv.Items[i].SubItems[5].Text))).Substring(0, 4).Replace(",", ".");
                        }
                        else
                        {
                            globallv.Items[i].SubItems[6].Text = Convert.ToString(GCD(Convert.ToInt32(globallv.Items[i].SubItems[4].Text), Convert.ToInt32(globallv.Items[i].SubItems[5].Text))).Replace(",",".");
                        }


                    }



                    ntbhtmlcode = ntbhtmlcode + "<tr>" + "\r\n";
                    ntbhtmlcode = ntbhtmlcode + "<td><div align=\"center\"><b>" + colorhtml(h.SubItems[9].Text) + "</b></FONT></div></td>" + "\r\n";
                    ntbhtmlcode = ntbhtmlcode + "<td><div align=\"center\">" + colorhtml(h.SubItems[3].Text) + "</div></td>" + "\r\n";
                    ntbhtmlcode = ntbhtmlcode + "<td><div align=\"center\">" + h.SubItems[1].Text + "</div></td>" + "\r\n";
                    ntbhtmlcode = ntbhtmlcode + "<td><div align=\"center\">" + h.SubItems[5].Text + "</div></td>" + "\r\n";
                    ntbhtmlcode = ntbhtmlcode + "<td><div align=\"center\">" + h.SubItems[7].Text + "</div></td>" + "\r\n";
                    ntbhtmlcode = ntbhtmlcode + "<td><div align=\"center\">" + h.SubItems[8].Text + "</div></td>" + "\r\n";

                    if (Convert.ToString(GCD(Convert.ToInt32(h.SubItems[7].Text), Convert.ToInt32(h.SubItems[8].Text))).Length > 3)
                    {
                        ntbhtmlcode = ntbhtmlcode + "<td><div align=\"center\">" + Convert.ToString(GCD(Convert.ToInt32(h.SubItems[7].Text), Convert.ToInt32(h.SubItems[8].Text))).Substring(0, 4).Replace(",", ".") + "</div></td>" + "\r\n";

                    }

                    else
                    {
                        ntbhtmlcode = ntbhtmlcode + "<td><div align=\"center\">" + Convert.ToString(GCD(Convert.ToInt32(h.SubItems[7].Text), Convert.ToInt32(h.SubItems[8].Text))).Replace(",", ".") + "</div></td>" + "\r\n";

                    }



                    ntbhtmlcode = ntbhtmlcode + "<td><div align=\"center\">" + h.SubItems[6].Text + "</div></td>" + "\r\n";
                    ntbhtmlcode = ntbhtmlcode + "<td><div align=\"center\">" + h.SubItems[4].Text + "</div></td>" + "\r\n";
                    ntbhtmlcode = ntbhtmlcode + "<td><div align=\"center\">" + h.Text + "</div></td>" + "\r\n";
                    ntbhtmlcode = ntbhtmlcode + "</tr>" + "\r\n";

                }

                htmlbody = htmlbody.Replace("$$namekills", colorhtml(info.Items[8].SubItems[1].Text)).Replace("($$kills)", colorhtml(textBox59.Text + "(" + info.Items[8].SubItems[2].Text + ")"));
                htmlbody = htmlbody.Replace("$$namekdratio", colorhtml(info.Items[10].SubItems[1].Text)).Replace("($$kdratio)", colorhtml(textBox59.Text + "(" + info.Items[10].SubItems[2].Text + ")"));
                htmlbody = htmlbody.Replace("$$namedeaths", colorhtml(info.Items[9].SubItems[1].Text)).Replace("($$deaths)", colorhtml(textBox59.Text +  "(" + info.Items[9].SubItems[2].Text + ")"));

                htmlbody = htmlbody.Replace("$$$$$NTBHTMLCODE$$$$$", ntbhtmlcode);

                m_streamWriter.Write(htmlbody);
                m_streamWriter.WriteLine();

                m_streamWriter.Flush();


                m_streamWriter.Close();

                fs19.Dispose();
                m_streamWriter.Dispose();

                label37.Text = globallv.Items.Count.ToString();

            }
            catch (Exception error)
            {
                errorlv.Items.Add(new ListViewItem(new string[] { DateTime.Now.ToLocalTime().ToString(), error.TargetSite.Name.ToString(), error.Message + "\r\n" + error.StackTrace }));

            }




        }
        private string colorhtml(string pt)
        {


             foreach (ListViewItem col in carlitos)
            {
             pt = pt.Replace(col.Text,col.SubItems[1].Text);
            }

            return pt;

        
        }
        void scan2()
        {
            string loghtml = "";
            try
            {
                socket.Close();
            }

            catch
            {

            }


            try
            {


                IPAddress ip = IPAddress.Parse(serverip.Text);
                IPEndPoint iep = new IPEndPoint(IPAddress.Any, Convert.ToInt32(textBox43.Text));
                IPEndPoint endpoint = new IPEndPoint(ip, Convert.ToInt32(serverport.Text));
                socket = new Socket(endpoint.AddressFamily, SocketType.Dgram, ProtocolType.Udp);
                socket.SendTimeout = Convert.ToInt32(sendtimeout.Text);
                socket.ReceiveTimeout = Convert.ToInt32(responsetimeout.Text);
                socket.Bind(iep);
                socket.Connect(endpoint);
                byte[] output = StringToByteArray("\xFF\xFFgetinfoEx\xFF\xFF"); //trigger packet to send
                log.AppendText(DateTime.Now.ToLocalTime() + "\t" + "Sending query (" + output.Length.ToString() + " bytes)" + "\r\n");
                bd.Text = Convert.ToString(Convert.ToInt32(bd.Text) + output.Length);
                l1.Text = (Convert.ToInt32(l1.Text) + 1).ToString();  
                socket.Send(output, output.Length, 0);
                Application.DoEvents();

                byte[] data = new byte[30000]; //container packet
                int received = socket.Receive(data, data.Length, 0); //get the lenght of the received packet
                byte[] packet = new byte[received]; // creates an array to fill with the information received
                socket.Close();
                Array.Copy(data, 0, packet, 0, received);  //copy the raw packet to the working packet
                log.AppendText(DateTime.Now.ToLocalTime() + "\t" + "Received Information request" + "\t" + "(" + received.ToString() + " bytes)" + "\r\n");
                br.Text = Convert.ToString(Convert.ToInt32(br.Text) + received);
                l2.Text = (Convert.ToInt32(l2.Text) + 1).ToString();  
                parsepacket(packet);


                Application.DoEvents();
                switch (info.Items[1].SubItems[1].Text.ToLower())
                {
                    case "playing":
                        savestats.Text = "si";
                        break;
                    case "reviewing":

                        try
                        {
                          //  sendrcon(rcon.Text, "si_disablevoting 0", serverip.Text, Convert.ToInt32(serverport.Text), "", "");
                        }
                        catch
                        {
                        }

                        if ((info.Items[8].SubItems[1].Text != "-") & (info.Items[8].SubItems[2].Text != "0") & (info.Items[9].SubItems[1].Text != "-") & (info.Items[9].SubItems[2].Text != "0") & (info.Items[10].SubItems[1].Text != "-") & (info.Items[10].SubItems[2].Text != "0") & (checkBox50.Checked == true))
                        {
                            Application.DoEvents();
                            try
                            {
//                                sendrcon(rcon.Text, "say '" + textBox1.Text.Replace("$killername1", players.Items[Convert.ToInt32(info.Items[18].SubItems[2].Text)].SubItems[9].Text).Replace("$kills1", players.Items[Convert.ToInt32(info.Items[18].SubItems[2].Text)].SubItems[7].Text).Replace("$killerkd1", players.Items[Convert.ToInt32(info.Items[18].SubItems[2].Text)].SubItems[10].Text + "'") + "\r" + "say '" + textBox2.Text.Replace("$killername2", players.Items[Convert.ToInt32(info.Items[19].SubItems[2].Text)].SubItems[9].Text).Replace("$kills2", players.Items[Convert.ToInt32(info.Items[19].SubItems[2].Text)].SubItems[7].Text).Replace("$killerkd2", players.Items[Convert.ToInt32(info.Items[19].SubItems[2].Text)].SubItems[10].Text + "'") + "\r" + "say '" + textBox9.Text.Replace("$killername3", players.Items[Convert.ToInt32(info.Items[20].SubItems[2].Text)].SubItems[9].Text).Replace("$kills3", players.Items[Convert.ToInt32(info.Items[20].SubItems[2].Text)].SubItems[7].Text).Replace("$killerkd3", players.Items[Convert.ToInt32(info.Items[20].SubItems[2].Text)].SubItems[10].Text + "'"), serverip.Text, Convert.ToInt32(serverport.Text), "","");
//                                sendrcon(rcon.Text, "say '" + textBox18.Text.Replace("$kdrname1", players.Items[Convert.ToInt32(info.Items[15].SubItems[2].Text)].SubItems[9].Text).Replace("$kd1", players.Items[Convert.ToInt32(info.Items[15].SubItems[2].Text)].SubItems[10].Text) + "'" + "\r" + "say '" + textBox17.Text.Replace("$kdrname2", players.Items[Convert.ToInt32(info.Items[16].SubItems[2].Text)].SubItems[9].Text).Replace("$kd2", players.Items[Convert.ToInt32(info.Items[16].SubItems[2].Text)].SubItems[10].Text) + "'" + "\r" + "say '" + textBox16.Text.Replace("$kdrname3", players.Items[Convert.ToInt32(info.Items[17].SubItems[2].Text)].SubItems[9].Text).Replace("$kd3", players.Items[Convert.ToInt32(info.Items[17].SubItems[2].Text)].SubItems[10].Text) + "'", serverip.Text, Convert.ToInt32(serverport.Text), "","");
//                                sendrcon(rcon.Text, "say '" + textBox3.Text.Replace("$lemming", info.Items[9].SubItems[1].Text).Replace("$deaths", info.Items[9].SubItems[2].Text) + "'", serverip.Text, Convert.ToInt32(serverport.Text), "","");

                                sendrcon(rcon.Text, "say '" + textBox45.Text.Replace("$killername1", players.Items[Convert.ToInt32(info.Items[18].SubItems[2].Text)].SubItems[9].Text.Replace("'", "`")).Replace("$kills1", players.Items[Convert.ToInt32(info.Items[18].SubItems[2].Text)].SubItems[7].Text).Replace("$killerkd1", players.Items[Convert.ToInt32(info.Items[18].SubItems[2].Text)].SubItems[10].Text) + "'" + "\r" + "say '" + textBox46.Text.Replace("$killername2", players.Items[Convert.ToInt32(info.Items[19].SubItems[2].Text)].SubItems[9].Text.Replace("'", "`")).Replace("$kills2", players.Items[Convert.ToInt32(info.Items[19].SubItems[2].Text)].SubItems[7].Text).Replace("$killerkd2", players.Items[Convert.ToInt32(info.Items[19].SubItems[2].Text)].SubItems[10].Text) + "'" + "\r" + "say '" + textBox47.Text.Replace("$killername3", players.Items[Convert.ToInt32(info.Items[20].SubItems[2].Text)].SubItems[9].Text.Replace("'", "`")).Replace("$kills3", players.Items[Convert.ToInt32(info.Items[20].SubItems[2].Text)].SubItems[7].Text).Replace("$killerkd3", players.Items[Convert.ToInt32(info.Items[20].SubItems[2].Text)].SubItems[10].Text) + "'", serverip.Text, Convert.ToInt32(serverport.Text), "", "");
                                System.Threading.Thread.Sleep(Convert.ToInt32(numericUpDown7.Value));  
                                sendrcon(rcon.Text, "say '" + textBox48.Text.Replace("$kdrname1", players.Items[Convert.ToInt32(info.Items[15].SubItems[2].Text)].SubItems[9].Text.Replace("'", "`")).Replace("$kd1", players.Items[Convert.ToInt32(info.Items[15].SubItems[2].Text)].SubItems[10].Text) + "'" + "\r" + "say '" + textBox49.Text.Replace("$kdrname2", players.Items[Convert.ToInt32(info.Items[16].SubItems[2].Text)].SubItems[9].Text.Replace("'", "`")).Replace("$kd2", players.Items[Convert.ToInt32(info.Items[16].SubItems[2].Text)].SubItems[10].Text) + "'" + "\r" + "say '" + textBox50.Text.Replace("$kdrname3", players.Items[Convert.ToInt32(info.Items[17].SubItems[2].Text)].SubItems[9].Text.Replace("'", "`")).Replace("$kd3", players.Items[Convert.ToInt32(info.Items[17].SubItems[2].Text)].SubItems[10].Text) + "'", serverip.Text, Convert.ToInt32(serverport.Text), "", "");
                                sendrcon(rcon.Text, "say '" + textBox52.Text.Replace("$lemming", info.Items[9].SubItems[1].Text.Replace("'", "`")).Replace("$deaths", info.Items[9].SubItems[2].Text) + "'", serverip.Text, Convert.ToInt32(serverport.Text), "", "");

                            
                            
                            }
                            catch
                            {

                            }
                            }

                        if ((info.Items[11].SubItems[1].Text != "0") & (info.Items[11].SubItems[2].Text != "0") & (info.Items[12].SubItems[1].Text != "0") & (info.Items[12].SubItems[2].Text != "0") & (checkBox52.Checked == true))
                        {
                            Application.DoEvents();
                            sendrcon(rcon.Text, "say '" + textBox51.Text.Replace("$gdfkills", info.Items[11].SubItems[1].Text).Replace("$gdfdeaths", info.Items[11].SubItems[2].Text) + "'" + "\r" + "say '" + textBox53.Text.Replace("$stroggkills", info.Items[12].SubItems[1].Text).Replace("$stroggdeaths", info.Items[12].SubItems[2].Text) + "'", serverip.Text, Convert.ToInt32(serverport.Text), "","");
                        }


                        if (savestats.Text == "si")
                        {
                            savestats.Text = "no";
                            if (statsyes.CheckState == CheckState.Checked)
                            {

                                loghtml = "LOG_" + DateTime.Parse(DateTime.UtcNow.ToUniversalTime().ToString()).ToString("yyyyMMdd_hhmmss");
                                clonalistview1();
                                globaltohtml();
                                if (checkBox1.Checked == false)
                                {
                                    if (checkBox9.Checked == false )
                                    {
                                        uploadtostatsfolder(new string[] { Application.StartupPath + "\\stats\\" + loghtml + ".html", Application.StartupPath + "\\stats\\sessions.html", Application.StartupPath + "\\stats\\sessions.txt", Application.StartupPath + "\\stats\\global.html", Application.StartupPath + "\\stats\\global.txt" });
                                    }
                                    else
                                    {
                                        uploadtostatsfolder(new string[] { Application.StartupPath + "\\stats\\global.html", Application.StartupPath + "\\stats\\global.txt" });

                                    }
                                    }
                                }
                        }




                        break;
                    case "warmup":

                        try
                        {
                        //    sendrcon(rcon.Text, "si_disablevoting 0", serverip.Text, Convert.ToInt32(serverport.Text), "", "");
                        }
                        catch
                        {
                        }


                        if (savestats.Text == "si")
                        {
                            savestats.Text = "no";
                            if (statsyes.CheckState == CheckState.Checked)
                            {

                                loghtml = "LOG_" + DateTime.Parse(DateTime.UtcNow.ToUniversalTime().ToString()).ToString("yyyyMMdd_hhmmss");
                                File.Copy(Application.StartupPath + "\\stats\\now.html", Application.StartupPath + "\\stats\\" + loghtml + ".html", true);
                                addsession(loghtml + ".html");
                                leenow();
                                Application.DoEvents();
                                globaltohtml();
                                if (checkBox1.Checked == false)
                                {
                                    if (checkBox9.Checked == false )
                                    {
                                        uploadtostatsfolder(new string[] { Application.StartupPath + "\\stats\\global.html", Application.StartupPath + "\\stats\\global.txt" });
                                    }
                                    else
                                    {
                                        uploadtostatsfolder(new string[] { Application.StartupPath + "\\stats\\" + loghtml + ".html", Application.StartupPath + "\\stats\\sessions.html", Application.StartupPath + "\\stats\\sessions.txt", Application.StartupPath + "\\stats\\global.html", Application.StartupPath + "\\stats\\global.txt" });

                                    }



                                }
                                                                }
                        }
                        break;
                    case "unknown":

                        try
                        {
                   //         sendrcon(rcon.Text, "si_disablevoting 0", serverip.Text, Convert.ToInt32(serverport.Text), "", "");
                        }
                        catch
                        {
                        }


                        if (savestats.Text == "si")
                        {
                            savestats.Text = "no";
                            if (statsyes.CheckState == CheckState.Checked)
                            {

                                loghtml = "LOG_" + DateTime.Parse(DateTime.UtcNow.ToUniversalTime().ToString()).ToString("yyyyMMdd_hhmmss");
                                File.Copy(Application.StartupPath + "\\stats\\now.html", Application.StartupPath + "\\stats\\" + loghtml + ".html", true);
                                addsession(loghtml + ".html");
                                leenow();
                                Application.DoEvents();

                                globaltohtml();
                                if (checkBox1.Checked == false)
                                {
                                    if (checkBox9.Checked == false )
                                    {
                                    uploadtostatsfolder(new string[] { Application.StartupPath + "\\stats\\global.html", Application.StartupPath + "\\stats\\global.txt" });
                                    }
                                    else
                                    {
                                        uploadtostatsfolder(new string[] { Application.StartupPath + "\\stats\\" + loghtml + ".html", Application.StartupPath + "\\stats\\sessions.html", Application.StartupPath + "\\stats\\sessions.txt", Application.StartupPath + "\\stats\\global.html", Application.StartupPath + "\\stats\\global.txt" });

                                    }
                                    }
                                                                }
                        }
                        break;
                }
                statsnow();
                if (checkBox25.Checked == true)
                {
                    doadmin();
                }

            }



            catch (SocketException error)
            {
                //       socket.Close();
                errorlv.Items.Add(new ListViewItem(new string[] { DateTime.Now.ToLocalTime().ToString(), error.TargetSite.Name.ToString(), error.Message + "\r\n" + error.StackTrace }));


                Application.DoEvents();

                log.AppendText(DateTime.Now.ToLocalTime() + "\t" + "Error: No response" + "\r\n");
                socket.Close();

                l3.Text = (Convert.ToInt32(l3.Text) + 1).ToString();  

                if (tryagain <= Convert.ToInt32(retriestimes.Value))
                {
                    log.AppendText(DateTime.Now.ToLocalTime() + "\t" + "Retrying...(" + tryagain.ToString() + ")" + "\r\n");
                    tryagain = tryagain + 1;
                    l1.Text = (Convert.ToInt32(l1.Text) + 1).ToString();
                    l4.Text = (Convert.ToInt32(l4.Text) + 1).ToString();  

                    scan2();
                }
                else
                {

                    log.AppendText(DateTime.Now.ToLocalTime() + "\t" + "NTB gave up after (" + retriestimes.Value.ToString() + ") times" + "\r\n");

                }

            }




        }

        private void loadpersonalsettings()
        {

            TypeConverter cfont = TypeDescriptor.GetConverter(typeof(Font));
            TypeConverter ccolor = TypeDescriptor.GetConverter(typeof(Color));
            try
            {


                char[] r = { '\t' };
                //  string country;
                FileStream fs3 = new FileStream(Application.StartupPath + "\\user.ini", FileMode.Open, FileAccess.Read);
                StreamReader m_streamReader = new StreamReader(fs3);

                // Write to the file using StreamWriter class 
                m_streamReader.BaseStream.Seek(0, SeekOrigin.Begin);

                string strLine = m_streamReader.ReadLine();
                string[] arr = strLine.Split(r);

                textBox15.Text = arr[1];
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                rcon.Text = Decrypt(arr[1]);
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                checkBox4.Checked = Convert.ToBoolean(arr[1]);
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                checkBox3.Checked = Convert.ToBoolean(arr[1]);
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                checkBox2.Checked = Convert.ToBoolean(arr[1]);
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                checkBox41.Checked = Convert.ToBoolean(arr[1]);
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                checkBox42.Checked = Convert.ToBoolean(arr[1]);
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                string[] arr2 = arr[1].Split('-');
                for (int i = 0; i < arr2.Length - 1; i++)
                {
                    string[] arr3 = arr2[i].Split(',');
                    players.Columns[Convert.ToInt32(arr3[0])].DisplayIndex = Convert.ToInt32(arr3[1]);
                    players.Columns[Convert.ToInt32(arr3[0])].Width = Convert.ToInt32(arr3[2]);

                }
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                string[] arr21 = arr[1].Split('-');
                for (int h = 0; h < arr21.Length - 1; h++)
                {
                    string[] arr31 = arr21[h].Split(',');
                    spectslv.Columns[Convert.ToInt32(arr31[0])].DisplayIndex = Convert.ToInt32(arr31[1]);
                    spectslv.Columns[Convert.ToInt32(arr31[0])].Width = Convert.ToInt32(arr31[2]);
                }


                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                string[] arr22 = arr[1].Split('-');
                for (int j = 0; j < arr22.Length - 1; j++)
                {
                    string[] arr32 = arr22[j].Split(',');
                    serversettings.Columns[Convert.ToInt32(arr32[0])].DisplayIndex = Convert.ToInt32(arr32[1]);
                    serversettings.Columns[Convert.ToInt32(arr32[0])].Width = Convert.ToInt32(arr32[2]);
                }
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                string[] arr23 = arr[1].Split('-');
                for (int k = 0; k < arr23.Length - 1; k++)
                {
                    string[] arr33 = arr23[k].Split(',');
                    serversettings.Columns[Convert.ToInt32(arr33[0])].DisplayIndex = Convert.ToInt32(arr33[1]);
                    serversettings.Columns[Convert.ToInt32(arr33[0])].Width = Convert.ToInt32(arr33[2]);
                }
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                checkBox21.Checked = Convert.ToBoolean(arr[1]);
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                checkBox16.Checked = Convert.ToBoolean(arr[1]);
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                lcb3.Checked = Convert.ToBoolean(arr[1]);
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                checkBox18.Checked = Convert.ToBoolean(arr[1]);
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                checkBox35.Checked = Convert.ToBoolean(arr[1]);
                checkBox31.Checked = AutoStarter.IsAutoStartEnabled; //autostarter
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                checkBox37.Checked = Convert.ToBoolean(arr[1]);
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                checkBox34.Checked = Convert.ToBoolean(arr[1]);
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                checkBox33.Checked = Convert.ToBoolean(arr[1]);
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                checkBox32.Checked = Convert.ToBoolean(arr[1]);
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                textBox43.Text = arr[1];
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                textBox42.Text = arr[1];
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                responsetimeout.Text = arr[1];
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                sendtimeout.Text = arr[1];
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                retriestimes.Value = Convert.ToDecimal(arr[1]);
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                textBox39.Text = arr[1];
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                checkBox40.Checked = Convert.ToBoolean(arr[1]);
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                numericUpDown6.Value = Convert.ToDecimal(arr[1]);
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                checkBox39.Checked = Convert.ToBoolean(arr[1]);
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                checkBox38.Checked = Convert.ToBoolean(arr[1]);
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                blend.Value = Convert.ToDecimal(arr[1]);
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                speed.Value = Convert.ToDecimal(arr[1]);
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                step.Value = Convert.ToDecimal(arr[1]);
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                ftph.Text = arr[1];
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                ftpu.Text = arr[1];
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                ftpp.Text = Decrypt(arr[1]);
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                ftps.Text = arr[1];
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                ftpl.Text = arr[1];
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                ftpfolder.Text = arr[1];
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                ftpstats.Text = arr[1];
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                comboBox12.Text = arr[1];

                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                textBox31.Text = arr[1];
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                textBox30.Text = arr[1];
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                textBox29.Text = arr[1];
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                textBox28.Text = arr[1];

                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                textBox31.Font = (Font)cfont.ConvertFromString(arr[1]);
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                textBox30.Font = (Font)cfont.ConvertFromString(arr[1]);
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                textBox29.Font = (Font)cfont.ConvertFromString(arr[1]);
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                textBox28.Font = (Font)cfont.ConvertFromString(arr[1]);

                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                textBox31.ForeColor = (Color)ccolor.ConvertFromString(arr[1]);
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                textBox30.ForeColor = (Color)ccolor.ConvertFromString(arr[1]);
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                textBox29.ForeColor = (Color)ccolor.ConvertFromString(arr[1]);
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                textBox28.ForeColor = (Color)ccolor.ConvertFromString(arr[1]);

                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                button46.BackColor = (Color)ccolor.ConvertFromString(arr[1]);
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                button47.BackColor = (Color)ccolor.ConvertFromString(arr[1]);
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                button48.BackColor = (Color)ccolor.ConvertFromString(arr[1]);
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                button45.BackColor = (Color)ccolor.ConvertFromString(arr[1]);

                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                button49.BackColor = (Color)ccolor.ConvertFromString(arr[1]);
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                button60.BackColor = (Color)ccolor.ConvertFromString(arr[1]);
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                button61.BackColor = (Color)ccolor.ConvertFromString(arr[1]);
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                checkBox13.Checked = Convert.ToBoolean(arr[1]);
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                checkBox1.Checked = Convert.ToBoolean(arr[1]);
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                checkBox6.Checked = Convert.ToBoolean(arr[1]);
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                checkBox9.Checked = Convert.ToBoolean(arr[1]);
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                checkBox20.Checked = Convert.ToBoolean(arr[1]);
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                checkBox17.Checked = Convert.ToBoolean(arr[1]);
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                checkBox19.Checked = Convert.ToBoolean(arr[1]);


                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                hidekeycombo.Text = arr[1];
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                hidectrlcheck.Checked = Convert.ToBoolean(arr[1]);

                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                hidealtcheck.Checked = Convert.ToBoolean(arr[1]);

                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                hideshiftcheck.Checked = Convert.ToBoolean(arr[1]);

                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                checkBox23.Checked = Convert.ToBoolean(arr[1]);
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                comboBox13.Text = arr[1];
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                checkBox28.Checked = Convert.ToBoolean(arr[1]);
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                checkBox27.Checked = Convert.ToBoolean(arr[1]);
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                checkBox26.Checked = Convert.ToBoolean(arr[1]);
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                checkBox24.Checked = Convert.ToBoolean(arr[1]);
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                textBox35.Text = arr[1];
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                textBox37.Text = arr[1];


                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                comboBox14.Text = arr[1];
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                checkBox43.Checked = Convert.ToBoolean(arr[1]);
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                checkBox36.Checked = Convert.ToBoolean(arr[1]);
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                checkBox30.Checked = Convert.ToBoolean(arr[1]);
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                checkBox29.Checked = Convert.ToBoolean(arr[1]);
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                textBox38.Text = arr[1];
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                textBox36.Text = arr[1];


                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                comboBox15.Text = arr[1];
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                checkBox47.Checked = Convert.ToBoolean(arr[1]);
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                checkBox46.Checked = Convert.ToBoolean(arr[1]);
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                checkBox45.Checked = Convert.ToBoolean(arr[1]);
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                checkBox44.Checked = Convert.ToBoolean(arr[1]);


                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                checkBox22.Checked = Convert.ToBoolean(arr[1]);

                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                ctu1.Checked = Convert.ToBoolean(arr[1]);
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                ctu2.Checked = Convert.ToBoolean(arr[1]);
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                ctu3.Checked = Convert.ToBoolean(arr[1]);

                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                textBox54.Text = arr[1];
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                textBox55.Text = arr[1];
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                checkBox51.Checked = Convert.ToBoolean(arr[1]);
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                checkBox54.Checked = Convert.ToBoolean(arr[1]);
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                checkBox53.Checked = Convert.ToBoolean(arr[1]);
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                textBox56.Text = arr[1];
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                textBox59.Text = arr[1];
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                textBox60.Text = arr[1];
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                textBox61.Text = arr[1];
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                textBox62.Text = arr[1];
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                textBox63.Text = arr[1];
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                textBox65.Text = arr[1];
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                textBox67.Text = arr[1];
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                textBox68.Text = arr[1];

                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                checkBox55.Checked = Convert.ToBoolean(arr[1]);
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                numericUpDown9.Value = Convert.ToDecimal(arr[1]);


                m_streamReader.Close();
                m_streamReader.Dispose();
                fs3.Dispose();
                r = null;
                arr = null;


            }

            catch
            {

            }

        }
        private void updateftpdata()
        {
            try
            {

                ftpConnection1.ServerAddress = ftph.Text;
                ftpConnection1.UserName = ftpu.Text;
                ftpConnection1.Password = ftpp.Text;

                if (comboBox12.Text == "Pasive")
                {
                    ftpConnection1.ConnectMode = EnterpriseDT.Net.Ftp.FTPConnectMode.PASV;
                }
                else
                {
                    ftpConnection1.ConnectMode = EnterpriseDT.Net.Ftp.FTPConnectMode.ACTIVE;
                }



            }

            catch
            {

            }



        }
        private void savesettings()
        {
            try
            {
                FileStream fs19 = new FileStream(Application.StartupPath + "\\" + ftps.Text.Replace("\\", ""), FileMode.Create, FileAccess.Write);
                StreamWriter m_streamWriter = new StreamWriter(fs19);
                m_streamWriter.Flush();
                m_streamWriter.BaseStream.Seek(0, SeekOrigin.Begin);
                m_streamWriter.Write("Server IP:" + "\t" + serverip.Text);
                m_streamWriter.WriteLine();
                m_streamWriter.Write("Server Port" + "\t" + serverport.Text);
                m_streamWriter.WriteLine();
                m_streamWriter.Write("Scan Interval" + "\t" + toolStripComboBox1.Text);
                m_streamWriter.WriteLine();

                m_streamWriter.Write("Server Settings#1" + "\t" + scb1.Checked.ToString());
                m_streamWriter.WriteLine();
                m_streamWriter.Write("Server Settings#2" + "\t" + checkBox11.Checked.ToString());
                m_streamWriter.WriteLine();
                m_streamWriter.Write("Server Settings#3" + "\t" + checkBox10.Checked.ToString());
                m_streamWriter.WriteLine();
                m_streamWriter.Write("Server Settings#4" + "\t" + comboBox3.Value.ToString());
                m_streamWriter.WriteLine();
                m_streamWriter.Write("Server Settings#5" + "\t" + numericUpDown2.Value.ToString());
                m_streamWriter.WriteLine();
                m_streamWriter.Write("Server Settings#6" + "\t" + comboBox2.Value.ToString());
                m_streamWriter.WriteLine();
                m_streamWriter.Write("Server Settings#7" + "\t" + checkBox7.Checked.ToString());
                m_streamWriter.WriteLine();
                m_streamWriter.Write("Server Settings#8" + "\t" + checkBox8.Checked.ToString());
                m_streamWriter.WriteLine();
                m_streamWriter.Write("Server Settings#9" + "\t" + textBox11.Text);
                m_streamWriter.WriteLine();
                m_streamWriter.Write("Server Settings#10" + "\t" + textBox12.Text);
                m_streamWriter.WriteLine();
                m_streamWriter.Write("Server Settings#11" + "\t" + warn.Text);
                m_streamWriter.WriteLine();
                m_streamWriter.Write("Server Settings#12" + "\t" + textBox10.Text);
                m_streamWriter.WriteLine();
                m_streamWriter.Write("Server Settings#13" + "\t" + textBox1.Text);
                m_streamWriter.WriteLine();
                m_streamWriter.Write("Server Settings#14" + "\t" + textBox2.Text);
                m_streamWriter.WriteLine();
                m_streamWriter.Write("Server Settings#15" + "\t" + textBox3.Text);
                m_streamWriter.WriteLine();
                m_streamWriter.Write("Server Settings#16" + "\t" + comboBox1.Value.ToString());
                m_streamWriter.WriteLine();
                m_streamWriter.Write("Server Settings#17" + "\t" + checkBox50.Checked.ToString());
                m_streamWriter.WriteLine();
                m_streamWriter.Write("Server Settings#18" + "\t" + checkBox52.Checked.ToString());
                m_streamWriter.WriteLine();
                m_streamWriter.Write("Server Settings#19" + "\t" + textBox23.Text);
                m_streamWriter.WriteLine();
                m_streamWriter.Write("Server Settings#20" + "\t" + textBox22.Text);
                m_streamWriter.WriteLine();
                m_streamWriter.Write("Server Settings#21" + "\t" + comboBox6.Value.ToString());
                m_streamWriter.WriteLine();
                m_streamWriter.Write("Server Settings#22" + "\t" + ddm3.Checked.ToString());
                m_streamWriter.WriteLine();
                m_streamWriter.Write("Server Settings#23" + "\t" + ddm4.Checked.ToString());
                m_streamWriter.WriteLine();
                m_streamWriter.Write("Server Settings#24" + "\t" + textBox4.Text);
                m_streamWriter.WriteLine();
                m_streamWriter.Write("Server Settings#25" + "\t" + textBox5.Text);
                m_streamWriter.WriteLine();
                m_streamWriter.Write("Server Settings#26" + "\t" + textBox6.Text);
                m_streamWriter.WriteLine();
                m_streamWriter.Write("Server Settings#27" + "\t" + textBox7.Text);
                m_streamWriter.WriteLine();
                m_streamWriter.Write("Server Settings#28" + "\t" + textBox8.Value.ToString());
                m_streamWriter.WriteLine();
                m_streamWriter.Write("Server Settings#29" + "\t" + checkBox5.Checked.ToString());
                m_streamWriter.WriteLine();
                m_streamWriter.Write("Server Settings#30" + "\t" + checkBox25.Checked.ToString());
                m_streamWriter.WriteLine();
                m_streamWriter.Write("Server Settings#31" + "\t" + statsyes.Checked.ToString());
                m_streamWriter.WriteLine();
                m_streamWriter.Write("Server Settings#32" + "\t" + statstime.Value.ToString());
                m_streamWriter.WriteLine();
                string pro = "";

                foreach (ListViewItem key in protectedlv.Items)
                {
                    pro = key.Text + "\t" + pro;
                }

                m_streamWriter.Write(pro);
                m_streamWriter.WriteLine();

                string pt = "";
                if (listView3.Items.Count != 0)
                {
                    foreach (ListViewItem bu in listView3.Items)
                    {
                        pt = pt + bu.Text + "\t" + bu.SubItems[1].Text + "\t" + bu.SubItems[2].Text + "\t";
                    }
                }
                else
                {
                    pt = "nothing";
                }
                m_streamWriter.Write("AdminRules" + "\t" + pt);
                m_streamWriter.WriteLine();
                m_streamWriter.Write("Server Settings#33" + "\t" + textBox9.Text);
                m_streamWriter.WriteLine();
                m_streamWriter.Write("Server Settings#34" + "\t" + textBox18.Text);
                m_streamWriter.WriteLine();
                m_streamWriter.Write("Server Settings#35" + "\t" + textBox17.Text);
                m_streamWriter.WriteLine();
                m_streamWriter.Write("Server Settings#36" + "\t" + textBox16.Text);
                m_streamWriter.WriteLine();
                m_streamWriter.Write("Server Settings#37" + "\t" + numericUpDown1.Value.ToString());
                m_streamWriter.WriteLine();

                m_streamWriter.Write("Server Settings#38" + "\t" + comboBox7.Text);
                m_streamWriter.WriteLine();
                m_streamWriter.Write("Server Settings#39" + "\t" + numericUpDown3.Value.ToString());
                m_streamWriter.WriteLine();
      
                m_streamWriter.Write("Server Settings#40" + "\t" + checkBox12.Checked.ToString());
                m_streamWriter.WriteLine();
                m_streamWriter.Write("Server Settings#41" + "\t" + checkBox14.Checked.ToString());
                m_streamWriter.WriteLine();
                m_streamWriter.Write("Server Settings#42" + "\t" + numericUpDown4.Value.ToString());
                m_streamWriter.WriteLine();
                m_streamWriter.Write("Server Settings#43" + "\t" + checkBox15.Checked.ToString());
                m_streamWriter.WriteLine();
                pt = "";
                if (listViewFind2.Items.Count != 0)
                {
                    foreach (ListViewItem bu in listViewFind2.Items)
                    {
                        pt = pt + bu.Text + "\t" + bu.SubItems[1].Text + "\t" + bu.SubItems[2].Text + "\t";
                    }
                }
                else
                {
                    pt = "nothing";
                }
                m_streamWriter.Write("AdminRulesGUID" + "\t" + pt);
                m_streamWriter.WriteLine();
                m_streamWriter.Write("Server Settings#44" + "\t" + checkBox48.Checked.ToString());
                m_streamWriter.WriteLine();
                m_streamWriter.Write("Server Settings#45" + "\t" + checkBox49.Checked.ToString());
                m_streamWriter.WriteLine();


                pt = "";
                foreach (object item2 in comboBox17.Items)
                {
                    pt =  item2 + "\t" + pt;
                }
                m_streamWriter.Write(pt);
                m_streamWriter.WriteLine();

                m_streamWriter.Write("Server Settings#46" + "\t" + textBox45.Text);
                m_streamWriter.WriteLine();
                m_streamWriter.Write("Server Settings#47" + "\t" + textBox46.Text);
                m_streamWriter.WriteLine();
                m_streamWriter.Write("Server Settings#48" + "\t" + textBox47.Text);
                m_streamWriter.WriteLine();
                m_streamWriter.Write("Server Settings#49" + "\t" + numericUpDown8.Value.ToString());
                m_streamWriter.WriteLine();
                m_streamWriter.Write("Server Settings#50" + "\t" + numericUpDown7.Value.ToString());
                m_streamWriter.WriteLine();
                m_streamWriter.Write("Server Settings#51" + "\t" + textBox48.Text);
                m_streamWriter.WriteLine();
                m_streamWriter.Write("Server Settings#52" + "\t" + textBox49.Text);
                m_streamWriter.WriteLine();
                m_streamWriter.Write("Server Settings#53" + "\t" + textBox50.Text);
                m_streamWriter.WriteLine();
                m_streamWriter.Write("Server Settings#54" + "\t" + textBox52.Text);
                m_streamWriter.WriteLine();
                m_streamWriter.Write("Server Settings#55" + "\t" + textBox51.Text);
                m_streamWriter.WriteLine();
                m_streamWriter.Write("Server Settings#56" + "\t" + textBox53.Text);
                m_streamWriter.WriteLine();
                m_streamWriter.Write("Server Settings#57" + "\t" + numericUpDown5.Value.ToString());
                m_streamWriter.WriteLine();



                m_streamWriter.Flush();
                m_streamWriter.Close();
                pt = null;
                pro = null;
                fs19.Dispose();
                m_streamWriter.Dispose();

            }

            catch
            {

            }
        }




        private void loadpb()
        {

            int k = -1;
            if (File.Exists(Application.StartupPath + "\\pblist.txt"))
            {
            }
            else
            {
                return;
            }



            pbh.Items.Clear();


            listViewFind3.Items.Clear();

            Cursor = Cursors.WaitCursor;



            
            char[] r = { '\t' };
            //  string country;
            FileStream fs2 = new FileStream(Application.StartupPath + "\\" + "pblist.txt", FileMode.Open, FileAccess.Read);
            StreamReader m_streamReader = new StreamReader(fs2);



            // Write to the file using StreamWriter class 
            m_streamReader.BaseStream.Seek(0, SeekOrigin.Begin);
  

            try
            {


                //  string country;
              //  CountryLookup cl = new CountryLookup(Application.StartupPath + "\\GeoIP.dat");
              string strLine = m_streamReader.ReadLine();


              List<ListViewItem> ariel = new List<ListViewItem>();
              ListViewItem ju;

                while (strLine != null)
                {
                //   country = cl.lookupCountryCode(sas[2].Substring(0, sas[2].IndexOf(":")));
                    string[] sas = strLine.Split(r);
                    ju = new ListViewItem(new string[] { sas[0], sas[1], sas[2], sas[3], sas[4], sas[5], sas[6], sas[7], sas[8], sas[9], sas[10] }, _iconListManager.AddFileIcon(Application.StartupPath + "\\Flags\\" + sas[9].ToLower() + ".ico", true));
                    ju.ImageIndex = _iconListManager.AddFileIcon(Application.StartupPath + "\\Flags\\" + sas[9].ToLower() + ".ico", true);
                    if (sas[11] == "True")
                    {
                        ju.Checked = true;
                        ju.BackColor = Color.LightSkyBlue ;
                        k = -1;
                                k = listViewFind3.FindItem(sas[0]);
                                if (k == -1)
                                {
                                    listViewFind3.Items.Add(new ListViewItem(new string[] { sas[0], sas[1], sas[8] }, ju.ImageIndex));
                                }
                    }
                    


            ariel.Add(ju);
                        strLine = m_streamReader.ReadLine();
                 
                }
                pbh.Items.AddRange(ariel.ToArray());
                ariel.Clear();
                m_streamReader.Close();
                m_streamReader.Dispose();
                fs2.Dispose();
                r = null;
                label62.Text = pbh.Items.Count.ToString();
                Cursor = Cursors.Default ;

                        }

            catch
            {
                m_streamReader.Close();
                m_streamReader.Dispose();
                fs2.Dispose();
                r = null;
                label62.Text = pbh.Items.Count.ToString();

                Cursor = Cursors.Default;

            }
        }
        private void loadpbfromfile(string file)
        {

            int k = -1;


                     

            Cursor = Cursors.WaitCursor;




            char[] r = { '\t' };
            //  string country;
            FileStream fs2 = new FileStream(file, FileMode.Open, FileAccess.Read);
            StreamReader m_streamReader = new StreamReader(fs2);



            // Write to the file using StreamWriter class 
            m_streamReader.BaseStream.Seek(0, SeekOrigin.Begin);


            try
            {


                //  string country;
                //  CountryLookup cl = new CountryLookup(Application.StartupPath + "\\GeoIP.dat");
                string strLine = m_streamReader.ReadLine();


                List<ListViewItem> ariel = new List<ListViewItem>();
                ListViewItem ju;

                while (strLine != null)
                {
                    //   country = cl.lookupCountryCode(sas[2].Substring(0, sas[2].IndexOf(":")));
                    string[] sas = strLine.Split(r);
                    ju = new ListViewItem(new string[] { sas[0], sas[1], sas[2], sas[3], sas[4], sas[5], sas[6], sas[7], sas[8], sas[9], sas[10] }, _iconListManager.AddFileIcon(Application.StartupPath + "\\Flags\\" + sas[9].ToLower() + ".ico", true));
                    ju.ImageIndex = _iconListManager.AddFileIcon(Application.StartupPath + "\\Flags\\" + sas[9].ToLower() + ".ico", true);


                   




                    if (sas[11] == "True")
                    {
                        ju.Checked = true;
                        ju.BackColor = Color.LightSkyBlue;
                        k = -1;
                        k = pbh.FindItem(sas[0]);


                        if (k == -1)
                        {
                            ariel.Add(ju);
                        }

                        else
                        {
                            pbh.Items[k].Checked = true;

                            }

                        k = -1;
                        k = listViewFind3.FindItem(sas[0]);

                        if (k == -1)
                        {
                            listViewFind3.Items.Add(new ListViewItem(new string[] { sas[0], sas[1], sas[8] }, ju.ImageIndex));

                        }


                    }
                    else
                    {
                     
                        k = -1;
                        k = pbh.FindItem(sas[0]);
                        if (k == -1)
                        {
                            ariel.Add(ju);
                        }
                    }
                    
                    
                                      



                    strLine = m_streamReader.ReadLine();

                }

                pbh.Items.AddRange(ariel.ToArray());
                ariel.Clear();
                m_streamReader.Close();
                m_streamReader.Dispose();
                fs2.Dispose();
                r = null;
                label62.Text = pbh.Items.Count.ToString();
                Cursor = Cursors.Default;

            }

            catch
            {
                m_streamReader.Close();
                m_streamReader.Dispose();
                fs2.Dispose();
                r = null;
                label62.Text = pbh.Items.Count.ToString();

                Cursor = Cursors.Default;

            }
        }

        private void loadsettings()
        {

            protectedlv.Items.Clear();
            listView3.Items.Clear();
            listViewFind2.Items.Clear();
            try
            {
                char[] r = { '\t' };
                //  string country;
                FileStream fs2 = new FileStream(Application.StartupPath + "\\" + ftps.Text.Replace("\\", ""), FileMode.Open, FileAccess.Read);
                StreamReader m_streamReader = new StreamReader(fs2);

                // Write to the file using StreamWriter class 
                m_streamReader.BaseStream.Seek(0, SeekOrigin.Begin);

                string strLine = m_streamReader.ReadLine();
                string[] arr = strLine.Split(r);

                serverip.Text = arr[1];
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                serverport.Text = arr[1];
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                toolStripComboBox1.Text = arr[1];

                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                scb1.Checked = Convert.ToBoolean(arr[1]);

                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                checkBox11.Checked = Convert.ToBoolean(arr[1]);

                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                checkBox10.Checked = Convert.ToBoolean(arr[1]);

                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                comboBox3.Value = Convert.ToDecimal(arr[1]);

                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                numericUpDown2.Value = Convert.ToDecimal(arr[1]);

                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                comboBox2.Value = Convert.ToDecimal(arr[1]);

                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                checkBox7.Checked = Convert.ToBoolean(arr[1]);
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                checkBox8.Checked = Convert.ToBoolean(arr[1]);
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                textBox11.Text = arr[1];
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                textBox12.Text = arr[1];
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                warn.Text = arr[1];
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                textBox10.Text = arr[1];
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                textBox1.Text = arr[1];
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                textBox2.Text = arr[1];
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                textBox3.Text = arr[1];
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                comboBox1.Value = Convert.ToDecimal(arr[1]);
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                checkBox50.Checked = Convert.ToBoolean(arr[1]);
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                checkBox52.Checked = Convert.ToBoolean(arr[1]);
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                textBox23.Text = arr[1];
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                textBox22.Text = arr[1];
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                comboBox6.Value = Convert.ToDecimal(arr[1]);
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                ddm3.Checked = Convert.ToBoolean(arr[1]);
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                ddm4.Checked = Convert.ToBoolean(arr[1]);
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                textBox4.Text = arr[1];
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                textBox5.Text = arr[1];
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                textBox6.Text = arr[1];
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                textBox7.Text = arr[1];

                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                textBox8.Value = Convert.ToDecimal(arr[1]);
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                checkBox5.Checked = Convert.ToBoolean(arr[1]);
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                checkBox25.Checked = Convert.ToBoolean(arr[1]);
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                statsyes.Checked = Convert.ToBoolean(arr[1]);
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                statstime.Value = Convert.ToDecimal(arr[1]);

                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);

                foreach (string s in arr.Reverse<string>())
                {
                    if (s != "")
                    {
                        if (protectedlv.FindItem(s.ToLower()) == -1)
                        {
                            protectedlv.Items.Add(new ListViewItem(new string[] { s.ToLower() }, 17));
                        }
                    }
                }

                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);

                if (arr[1] != "nothing")
                {
                    try
                    {
                        for (int i = 1; i < arr.Length; i++)
                        {
                            
                                try
                                {
                                    listView3.Items.Add(new ListViewItem(new string[] { arr[i], arr[i + 1], arr[i + 2], "waiting", "0" }, 14));
                                }
                                catch
                                {
                                }
                                
                            i = i + 2;
                        }
                    }
                    catch
                    {

                    }

                }
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                textBox9.Text =arr[1];
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                textBox18.Text = arr[1];
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                textBox17.Text = arr[1];
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                textBox16.Text = arr[1];
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                numericUpDown1.Value = Convert.ToDecimal(arr[1]);
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                comboBox7.Text = arr[1];
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                numericUpDown3.Value = Convert.ToDecimal(arr[1]);
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                checkBox12.Checked = Convert.ToBoolean(arr[1]);
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                checkBox14.Checked = Convert.ToBoolean(arr[1]);
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                numericUpDown4.Value = Convert.ToDecimal(arr[1]);
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                checkBox15.Checked = Convert.ToBoolean(arr[1]);
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                int ju = -1;
                if (arr[1] != "nothing")
                {
                    try
                    {
                        for (int f = 1; f < arr.Length; f++)
                        {


                            try
                            {
                                ju = -1;
                                ju = pbh.FindItem(arr[f]);

                            }
                            catch
                            {

                            }

                            try
                            {
                                if (ju == -1)
                                {
                                    listViewFind2.Items.Add(new ListViewItem(new string[] { arr[f], arr[f + 1], arr[f + 2], "waiting", "0", "" }));
                                }
                                else
                                {
                                    listViewFind2.Items.Add(new ListViewItem(new string[] { arr[f], arr[f + 1], arr[f + 2], "waiting", "0", pbh.Items[ju].SubItems[1].Text }, pbh.Items[ju].ImageIndex ));

                                }
                                }
                            catch
                            {
                            }

                            f = f + 2;
                        }
                    }
                    catch
                    {

                    }

                }
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                checkBox48.Checked = Convert.ToBoolean(arr[1]);

                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                checkBox49.Checked = Convert.ToBoolean(arr[1]);


                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                comboBox17.Items.Clear();
                foreach (string ss in arr.Reverse<string>())
                {
                    if (ss != "")
                    {
                        comboBox17.Items.Insert(0, ss);
                    }
                }


                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                textBox45.Text = arr[1];
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                textBox46.Text = arr[1];
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                textBox47.Text = arr[1];
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                numericUpDown8.Value = Convert.ToDecimal(arr[1]);
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                numericUpDown7.Value = Convert.ToDecimal(arr[1]);

                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                textBox48.Text = arr[1];
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                textBox49.Text = arr[1];
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                textBox50.Text = arr[1];
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                textBox52.Text = arr[1];
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                textBox51.Text = arr[1];
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                textBox53.Text = arr[1];
                strLine = m_streamReader.ReadLine();
                arr = strLine.Split(r);
                numericUpDown5.Value = Convert.ToDecimal(arr[1]);




                m_streamReader.Close();
                fs2.Dispose();
                m_streamReader.Dispose();
                arr = null;
                r = null;




            }

            catch
            {

            }

        }

        private void savepersonalsettings()
        {
            try
            {

                TypeConverter cfont = TypeDescriptor.GetConverter(typeof(Font));
                TypeConverter ccolor = TypeDescriptor.GetConverter(typeof(Color));
                FileStream fs2 = new FileStream(Application.StartupPath + "\\user.ini", FileMode.Create, FileAccess.Write);
                StreamWriter m_streamWriter = new StreamWriter(fs2);
                m_streamWriter.Flush();

                m_streamWriter.BaseStream.Seek(0, SeekOrigin.Begin);
                m_streamWriter.Write( "User:" + "\t" + textBox15.Text);
                m_streamWriter.WriteLine();
                m_streamWriter.Write("Rcon:" + "\t" + Encrypt(rcon.Text));
                m_streamWriter.WriteLine();
                m_streamWriter.Write("Main Checkbox1:" + "\t" + checkBox4.Checked.ToString());
                m_streamWriter.WriteLine();
                m_streamWriter.Write("Main Checkbox2:" + "\t" + checkBox3.Checked.ToString());
                m_streamWriter.WriteLine();
                m_streamWriter.Write("Main Checkbox3:" + "\t" + checkBox2.Checked.ToString());
                m_streamWriter.WriteLine();
                m_streamWriter.Write("Main Checkbox4:" + "\t" + checkBox41.Checked.ToString());
                m_streamWriter.WriteLine();
                m_streamWriter.Write("Main Checkbox5:" + "\t" + checkBox42.Checked.ToString());
                m_streamWriter.WriteLine();
                m_streamWriter.Write("players" + "\t");
                for (int i = 0; i < players.Columns.Count; i++)
                {
                    m_streamWriter.Write(players.Columns[i].Index.ToString() + "," + players.Columns[i].DisplayIndex.ToString() + "," + players.Columns[i].Width.ToString() + "-");
                }
                m_streamWriter.WriteLine();
                m_streamWriter.Write("spectslv" + "\t");

                for (int h = 0; h < spectslv.Columns.Count; h++)
                {
                    m_streamWriter.Write(spectslv.Columns[h].Index.ToString() + "," + spectslv.Columns[h].DisplayIndex.ToString() + "," + spectslv.Columns[h].Width.ToString() + "-");
                }
                m_streamWriter.WriteLine();
                m_streamWriter.Write("serversettings" + "\t");

                for (int j = 0; j < serversettings.Columns.Count; j++)
                {
                    m_streamWriter.Write(serversettings.Columns[j].Index.ToString() + "," + serversettings.Columns[j].DisplayIndex.ToString() + "," + serversettings.Columns[j].Width.ToString() + "-");
                }
                m_streamWriter.WriteLine();
                m_streamWriter.Write("protectedlv" + "\t");

                for (int k = 0; k < protectedlv.Columns.Count; k++)
                {
                    m_streamWriter.Write(protectedlv.Columns[k].Index.ToString() + "," + protectedlv.Columns[k].DisplayIndex.ToString() + "," + protectedlv.Columns[k].Width.ToString() + "-");
                }
                m_streamWriter.WriteLine();
                m_streamWriter.Write("Log Checkbox1:" + "\t" + checkBox21.Checked.ToString());
                m_streamWriter.WriteLine();
                m_streamWriter.Write("Log Checkbox2:" + "\t" + checkBox16.Checked.ToString());
                m_streamWriter.WriteLine();
                m_streamWriter.Write("Log Checkbox3:" + "\t" + lcb3.Checked.ToString());
                m_streamWriter.WriteLine();
                m_streamWriter.Write("Log Checkbox4:" + "\t" + checkBox18.Checked.ToString());
                m_streamWriter.WriteLine();
                m_streamWriter.Write("Log Checkbox1:" + "\t" + checkBox35.Checked.ToString());
                m_streamWriter.WriteLine();
                m_streamWriter.Write("Local Settings CB2:" + "\t" + checkBox37.Checked.ToString());
                m_streamWriter.WriteLine();
                m_streamWriter.Write("Local Settings:CB3" + "\t" + checkBox34.Checked.ToString());
                m_streamWriter.WriteLine();
                m_streamWriter.Write("Local Settings:CB4" + "\t" + checkBox33.Checked.ToString());
                m_streamWriter.WriteLine();
                m_streamWriter.Write("Local Settings:CB5" + "\t" + checkBox32.Checked.ToString());
                m_streamWriter.WriteLine();
                m_streamWriter.Write("Local Settings:TB1" + "\t" + textBox43.Text);
                m_streamWriter.WriteLine();
                m_streamWriter.Write("Local Settings:TB2" + "\t" + textBox42.Text);
                m_streamWriter.WriteLine();
                m_streamWriter.Write("Local Settings:TB3" + "\t" + responsetimeout.Text);
                m_streamWriter.WriteLine();
                m_streamWriter.Write("Local Settings:TB4" + "\t" + sendtimeout.Text);
                m_streamWriter.WriteLine();
                m_streamWriter.Write("Local Settings:NUD1" + "\t" + retriestimes.Value.ToString());
                m_streamWriter.WriteLine();
                m_streamWriter.Write("Local Settings:TB5" + "\t" + textBox39.Text);
                m_streamWriter.WriteLine();
                m_streamWriter.Write("Local Settings:CB6" + "\t" + checkBox40.Checked.ToString());
                m_streamWriter.WriteLine();
                m_streamWriter.Write("Local Settings:NUD2" + "\t" + numericUpDown6.Value.ToString());
                m_streamWriter.WriteLine();
                m_streamWriter.Write("Local Settings:CB7" + "\t" + checkBox39.Checked.ToString());
                m_streamWriter.WriteLine();
                m_streamWriter.Write("Local Settings:CB8" + "\t" + checkBox38.Checked.ToString());
                m_streamWriter.WriteLine();
                m_streamWriter.Write("Local Settings:NUD3" + "\t" + blend.Value.ToString());
                m_streamWriter.WriteLine();
                m_streamWriter.Write("Local Settings:NUD4" + "\t" + speed.Value.ToString());
                m_streamWriter.WriteLine();
                m_streamWriter.Write("Local Settings:NUD5" + "\t" + step.Value.ToString());
                m_streamWriter.WriteLine();
                m_streamWriter.Write("Local Settings:TB6" + "\t" + ftph.Text);
                m_streamWriter.WriteLine();
                m_streamWriter.Write("Local Settings:TB7" + "\t" + ftpu.Text);
                m_streamWriter.WriteLine();
                m_streamWriter.Write("Local Settings:TB8" + "\t" + Encrypt(ftpp.Text));
                m_streamWriter.WriteLine();
                m_streamWriter.Write("Local Settings:TB9" + "\t" + ftps.Text);
                m_streamWriter.WriteLine();
                m_streamWriter.Write("Local Settings:TB10" + "\t" + ftpl.Text);
                m_streamWriter.WriteLine();
                m_streamWriter.Write("Local Settings:TB11" + "\t" + ftpfolder.Text);
                m_streamWriter.WriteLine();
                m_streamWriter.Write("Local Settings:TB12" + "\t" + ftpstats.Text);
                m_streamWriter.WriteLine();
                m_streamWriter.Write("Local Settings:ComboBox1" + "\t" + comboBox1.Text);
                m_streamWriter.WriteLine();

                m_streamWriter.Write("GL1" + "\t" + textBox31.Text);
                m_streamWriter.WriteLine();
                m_streamWriter.Write("GL2" + "\t" + textBox30.Text);
                m_streamWriter.WriteLine();
                m_streamWriter.Write("GL3" + "\t" + textBox29.Text);
                m_streamWriter.WriteLine();
                m_streamWriter.Write("GL4" + "\t" + textBox28.Text);
                m_streamWriter.WriteLine();

                m_streamWriter.Write("FL1" + "\t" + cfont.ConvertToString(textBox31.Font));
                m_streamWriter.WriteLine();
                m_streamWriter.Write("FL2" + "\t" + cfont.ConvertToString(textBox30.Font));
                m_streamWriter.WriteLine();
                m_streamWriter.Write("FL3" + "\t" + cfont.ConvertToString(textBox29.Font));
                m_streamWriter.WriteLine();
                m_streamWriter.Write("FL4" + "\t" + cfont.ConvertToString(textBox28.Font));
                m_streamWriter.WriteLine();

                m_streamWriter.Write("CF1" + "\t" + ccolor.ConvertToString(textBox31.ForeColor));  // button15.BackColor.R.ToString() +"," + button15.BackColor.G.ToString() + "," +button15.BackColor.B.ToString() );
                m_streamWriter.WriteLine();
                m_streamWriter.Write("CF2" + "\t" + ccolor.ConvertToString(textBox30.ForeColor));  // button15.BackColor.R.ToString() +"," + button15.BackColor.G.ToString() + "," +button15.BackColor.B.ToString() );
                m_streamWriter.WriteLine();
                m_streamWriter.Write("CF3" + "\t" + ccolor.ConvertToString(textBox29.ForeColor));  // button15.BackColor.R.ToString() +"," + button15.BackColor.G.ToString() + "," +button15.BackColor.B.ToString() );
                m_streamWriter.WriteLine();
                m_streamWriter.Write("CF4" + "\t" + ccolor.ConvertToString(textBox28.ForeColor));  // button15.BackColor.R.ToString() +"," + button15.BackColor.G.ToString() + "," +button15.BackColor.B.ToString() );
                m_streamWriter.WriteLine();

                m_streamWriter.Write("GC1" + "\t" + ccolor.ConvertToString(button46.BackColor));  // button15.BackColor.R.ToString() +"," + button15.BackColor.G.ToString() + "," +button15.BackColor.B.ToString() );
                m_streamWriter.WriteLine();
                m_streamWriter.Write("GC2" + "\t" + ccolor.ConvertToString(button47.BackColor));  // button15.BackColor.R.ToString() +"," + button15.BackColor.G.ToString() + "," +button15.BackColor.B.ToString() );
                m_streamWriter.WriteLine();
                m_streamWriter.Write("GC3" + "\t" + ccolor.ConvertToString(button48.BackColor));  // button15.BackColor.R.ToString() +"," + button15.BackColor.G.ToString() + "," +button15.BackColor.B.ToString() );
                m_streamWriter.WriteLine();
                m_streamWriter.Write("GC4" + "\t" + ccolor.ConvertToString(button45.BackColor));  // button15.BackColor.R.ToString() +"," + button15.BackColor.G.ToString() + "," +button15.BackColor.B.ToString() );
                m_streamWriter.WriteLine();

                m_streamWriter.Write("GC5" + "\t" + ccolor.ConvertToString(button49.BackColor));  // button15.BackColor.R.ToString() +"," + button15.BackColor.G.ToString() + "," +button15.BackColor.B.ToString() );
                m_streamWriter.WriteLine();
                m_streamWriter.Write("GC6" + "\t" + ccolor.ConvertToString(button60.BackColor));  // button15.BackColor.R.ToString() +"," + button15.BackColor.G.ToString() + "," +button15.BackColor.B.ToString() );
                m_streamWriter.WriteLine();
                m_streamWriter.Write("GC7" + "\t" + ccolor.ConvertToString(button61.BackColor));  // button15.BackColor.R.ToString() +"," + button15.BackColor.G.ToString() + "," +button15.BackColor.B.ToString() );
                m_streamWriter.WriteLine();
                m_streamWriter.Write("FTPLOG Checkbox1:" + "\t" + checkBox13.Checked.ToString());
                m_streamWriter.WriteLine();
                m_streamWriter.Write("Local Settings:CBLast" + "\t" + checkBox1.Checked.ToString());
                m_streamWriter.WriteLine();
                m_streamWriter.Write("Backup Stats:" + "\t" + checkBox6.Checked.ToString());
                m_streamWriter.WriteLine();
                m_streamWriter.Write("Upload Sessions:" + "\t" + checkBox9.Checked.ToString());
                m_streamWriter.WriteLine();

                m_streamWriter.Write("Incremental PBLB:" + "\t" + checkBox20.Checked.ToString());
                m_streamWriter.WriteLine();

                m_streamWriter.Write("Upload PBL:" + "\t" + checkBox17.Checked.ToString());
                m_streamWriter.WriteLine();
                m_streamWriter.Write("Download PBL:" + "\t" + checkBox19.Checked.ToString());
                m_streamWriter.WriteLine();
                m_streamWriter.Write("HKs:" + "\t" + hidekeycombo.Text);
                m_streamWriter.WriteLine();
                m_streamWriter.Write("HKs:" + "\t" + hidectrlcheck.Checked.ToString());
                m_streamWriter.WriteLine();
                m_streamWriter.Write("HKs:" + "\t" + hidealtcheck.Checked.ToString());
                m_streamWriter.WriteLine();
                m_streamWriter.Write("HKs:" + "\t" + hideshiftcheck.Checked.ToString());
                m_streamWriter.WriteLine();
                m_streamWriter.Write("HKs:" + "\t" + checkBox23.Checked.ToString());
                m_streamWriter.WriteLine();

                m_streamWriter.Write("HKs:" + "\t" + comboBox13.Text);
                m_streamWriter.WriteLine();
                m_streamWriter.Write("HKs:" + "\t" + checkBox28.Checked.ToString());
                m_streamWriter.WriteLine();
                m_streamWriter.Write("HKs:" + "\t" + checkBox27.Checked.ToString());
                m_streamWriter.WriteLine();
                m_streamWriter.Write("HKs:" + "\t" + checkBox26.Checked.ToString());
                m_streamWriter.WriteLine();
                m_streamWriter.Write("HKs:" + "\t" + checkBox24.Checked.ToString());
                m_streamWriter.WriteLine();
                m_streamWriter.Write("HKs" + "\t" + textBox35.Text);
                m_streamWriter.WriteLine();
                m_streamWriter.Write("HKs" + "\t" + textBox37.Text);
                m_streamWriter.WriteLine();

                m_streamWriter.Write("HKs:" + "\t" + comboBox14.Text);
                m_streamWriter.WriteLine();
                m_streamWriter.Write("HKs:" + "\t" + checkBox43.Checked.ToString());
                m_streamWriter.WriteLine();
                m_streamWriter.Write("HKs:" + "\t" + checkBox36.Checked.ToString());
                m_streamWriter.WriteLine();
                m_streamWriter.Write("HKs:" + "\t" + checkBox30.Checked.ToString());
                m_streamWriter.WriteLine();
                m_streamWriter.Write("HKs:" + "\t" + checkBox29.Checked.ToString());
                m_streamWriter.WriteLine();
                m_streamWriter.Write("HKs" + "\t" + textBox38.Text);
                m_streamWriter.WriteLine();
                m_streamWriter.Write("HKs" + "\t" + textBox36.Text);
                m_streamWriter.WriteLine();


                m_streamWriter.Write("HKs:" + "\t" + comboBox15.Text);
                m_streamWriter.WriteLine();
                m_streamWriter.Write("HKs:" + "\t" + checkBox47.Checked.ToString());
                m_streamWriter.WriteLine();
                m_streamWriter.Write("HKs:" + "\t" + checkBox46.Checked.ToString());
                m_streamWriter.WriteLine();
                m_streamWriter.Write("HKs:" + "\t" + checkBox45.Checked.ToString());
                m_streamWriter.WriteLine();
                m_streamWriter.Write("HKs:" + "\t" + checkBox44.Checked.ToString());
                m_streamWriter.WriteLine();




                m_streamWriter.Write("HKs:" + "\t" + checkBox22.Checked.ToString());
                m_streamWriter.WriteLine();
                m_streamWriter.Write("CTU1:" + "\t" + ctu1.Checked.ToString());
                m_streamWriter.WriteLine();
                m_streamWriter.Write("CTU2:" + "\t" + ctu2.Checked.ToString());
                m_streamWriter.WriteLine();
                m_streamWriter.Write("CTU3:" + "\t" + ctu3.Checked.ToString());
                m_streamWriter.WriteLine();
                m_streamWriter.Write("RconPort#2" + "\t" + textBox54.Text);
                m_streamWriter.WriteLine();
                m_streamWriter.Write("RconPort#3" + "\t" + textBox55.Text);
                m_streamWriter.WriteLine();
                m_streamWriter.Write("DPC:" + "\t" + checkBox51.Checked.ToString());
                m_streamWriter.WriteLine();
                m_streamWriter.Write("STBG:" + "\t" + checkBox54.Checked.ToString());
                m_streamWriter.WriteLine();
                m_streamWriter.Write("AUPBMSG:" + "\t" + checkBox53.Checked.ToString());
                m_streamWriter.WriteLine();
                m_streamWriter.Write("PBMSG" + "\t" + textBox56.Text);
                m_streamWriter.WriteLine();
                m_streamWriter.Write("C1" + "\t" + textBox59.Text);
                m_streamWriter.WriteLine();
                m_streamWriter.Write("C2" + "\t" + textBox60.Text);
                m_streamWriter.WriteLine();
                m_streamWriter.Write("C3" + "\t" + textBox61.Text);
                m_streamWriter.WriteLine();
                m_streamWriter.Write("C4" + "\t" + textBox62.Text);
                m_streamWriter.WriteLine();
                m_streamWriter.Write("C5" + "\t" + textBox63.Text);
                m_streamWriter.WriteLine();
                m_streamWriter.Write("C6" + "\t" + textBox65.Text);
                m_streamWriter.WriteLine();
                m_streamWriter.Write("C7" + "\t" + textBox67.Text);
                m_streamWriter.WriteLine();
                m_streamWriter.Write("C8" + "\t" + textBox68.Text);
                m_streamWriter.WriteLine();
                m_streamWriter.Write("C9" + "\t" + checkBox55.Checked.ToString());
                m_streamWriter.WriteLine();
                m_streamWriter.Write("C10" + "\t" + numericUpDown9.Value.ToString());
                m_streamWriter.WriteLine();

                m_streamWriter.Flush();
                m_streamWriter.Close();
                fs2.Dispose();
                m_streamWriter.Dispose();

            }
            catch
            {
            }
        }
        void doadmin()
        {
            if (listView3.Items.Count != 0)
            {
                foreach (ListViewItem h in listView3.Items)
                {

                    if ((pablo.ContainsKey(h.Text)) & (h.SubItems[3].Text == "waiting"))
                    {
                        if (h.SubItems[1].Text != "canclosentb")
                        {
                            h.SubItems[3].Text = "done!";
                            h.SubItems[4].Text = Convert.ToString(Convert.ToDecimal(h.SubItems[4].Text) + 1);
                        }
                            try
                        {
                            dowhat(h.SubItems[1].Text, stripcolor(pablo[h.Text].ToString()), pablo[h.Text].ToString(), h.SubItems[2].Text, h.Text);
                        }
                        catch
                        {
                        }
                    }
                    else if ((!pablo.ContainsKey(h.Text)) & (h.SubItems[3].Text == "done!"))
                    {
                        h.SubItems[3].Text = "waiting";

                        if (checkBox12.Checked == true)
                        {
                            sendrcon(rcon.Text, "si_disablevoting 0", serverip.Text, Convert.ToInt32(serverport.Text), "", "");
                        }
             


                        foreach (ListViewItem h1 in listView3.Items)
                        {

                            if ((h1.SubItems[1].Text == "disablevotes") & (h1.SubItems[3].Text == "done!"))
                            {
                                h1.SubItems[3].Text = "waiting";

                            }
                        }

                    }
       

                }
     

            }


        }
        void doadminguid()
        {
            if (listViewFind2.Items.Count != 0)
            {
                foreach (ListViewItem h in listViewFind2.Items)
                {

                    if ((pabloguid.ContainsKey(h.Text)) & (h.SubItems[3].Text == "waiting"))
                    {
                        if (h.SubItems[1].Text != "canclosentb")
                        {
                            h.SubItems[3].Text = "done!";
                            h.SubItems[4].Text = Convert.ToString(Convert.ToDecimal(h.SubItems[4].Text) + 1);
                        }
                        try
                        {
                            dowhatguid(h.SubItems[1].Text, stripcolor(pabloguid[h.Text].ToString()), pabloguid[h.Text].ToString(), h.SubItems[2].Text, h.Text);
                        }
                        catch
                        {
                        }
                    }
                    else if ((!pabloguid.ContainsKey(h.Text)) & (h.SubItems[3].Text == "done!"))
                    {
                        h.SubItems[3].Text = "waiting";

                        if (checkBox48.Checked == true)
                        {
                            sendrcon(rcon.Text, "si_disablevoting 0", serverip.Text, Convert.ToInt32(serverport.Text), "", "");
                        }



                        foreach (ListViewItem h1 in listViewFind2.Items)
                        {

                            if ((h1.SubItems[1].Text == "disablevotes") & (h1.SubItems[3].Text == "done!"))
                            {
                                h1.SubItems[3].Text = "waiting";

                            }
                        }

                    }


                }


            }


        }
        void dowhat(string what, string who, string whocolor, string message, string user)
        {

            try
            {
                if (what.Substring(0, 14) == "join usergroup")
                {
                    sendrcon(rcon.Text, "admin changeusergroup '" + who + "'" + " " +  what.Substring(15, what.Length - 15), serverip.Text, Convert.ToInt32(serverport.Text), "","");
                    return;

                }
            }
            catch
            {

            }
               


            switch (what)
            {
                case "say":
                    sendrcon(rcon.Text, "say '" + whocolor + ": ^7" + message + "'", serverip.Text, Convert.ToInt32(serverport.Text), "","");

                    break;

                case "beep":

                    Beep(1000, 100);
                    Beep(900, 100);
                    Beep(800, 100);
                    Beep(1000, 1000);

                    break;


                case "kick":

                    sendrcon(rcon.Text, "admin kick '" + who + "'", serverip.Text, Convert.ToInt32(serverport.Text), "","");
                    kickedplayers = kickedplayers + who + " (Kicked Admin Rules)"  + "\r\n";
                    break;
                case "disablevotes":
                    sendrcon(rcon.Text, "si_disablevoting 1", serverip.Text, Convert.ToInt32(serverport.Text), "", "");
                    break;

                case "warn":

                    sendrcon(rcon.Text, "admin warn '" + who + "'", serverip.Text, Convert.ToInt32(serverport.Text), "","");

                    break;
                case "ban":

                    sendrcon(rcon.Text, "admin ban '" + who + "'", serverip.Text, Convert.ToInt32(serverport.Text), "","");
                    kickedplayers = kickedplayers + who + " (Banned Admin Rules)" + "\r\n";

                    break;
                case "unban":

                    sendrcon(rcon.Text, "admin unban '" + who + "'", serverip.Text, Convert.ToInt32(serverport.Text), "","");

                    break;
                case "mutetext":

                    sendrcon(rcon.Text, "admin PlayerMute '" + who + "'", serverip.Text, Convert.ToInt32(serverport.Text), "","");

                    break;
                case "mutevoip":

                    sendrcon(rcon.Text, "admin PlayerVOIPMute '" + who + "'", serverip.Text, Convert.ToInt32(serverport.Text), "","");

                    break;
                case "wait 2000 ms":

                    System.Threading.Thread.Sleep(2000);

                    break;
                case "canclosentb":

                    if (stripcolor(whocolor.ToLower()).Replace(user.ToLower(),"") == textBox39.Text.ToLower())
                    {
                        sendrcon(rcon.Text, "say '" + whocolor + ": ^7Stopped application'", serverip.Text, Convert.ToInt32(serverport.Text), "","");
                        checkntbstatus();
                    }
                    break;
              



            }


        }
        void dowhatguid(string what, string who, string whocolor, string message, string user)
        {

            try
            {
                if (what.Substring(0, 14) == "join usergroup")
                {
                    sendrcon(rcon.Text, "admin changeusergroup '" + who + "'" + " " + what.Substring(15, what.Length - 15), serverip.Text, Convert.ToInt32(serverport.Text), "", "");
                    return;

                }
            }
            catch
            {

            }



            switch (what)
            {
                case "say":
                    sendrcon(rcon.Text, "say '" + whocolor + ": ^7" + message + "'", serverip.Text, Convert.ToInt32(serverport.Text), "", "");

                    break;

                case "beep":

                    Beep(1000, 100);
                    Beep(900, 100);
                    Beep(800, 100);
                    Beep(1000, 1000);

                    break;


                case "kick":

                    sendrcon(rcon.Text, "admin kick '" + who + "'", serverip.Text, Convert.ToInt32(serverport.Text), "", "");
                    kickedplayers = kickedplayers + who + " (Kicked Admin Rules)" + "\r\n";
                    break;
                case "disablevotes":
                    sendrcon(rcon.Text, "si_disablevoting 1", serverip.Text, Convert.ToInt32(serverport.Text), "", "");
                    break;

                case "warn":

                    sendrcon(rcon.Text, "admin warn '" + who + "'", serverip.Text, Convert.ToInt32(serverport.Text), "", "");

                    break;
                case "ban":

                    sendrcon(rcon.Text, "admin ban '" + who + "'", serverip.Text, Convert.ToInt32(serverport.Text), "", "");
                    kickedplayers = kickedplayers + who + " (Banned Admin Rules)" + "\r\n";

                    break;
                case "unban":

                    sendrcon(rcon.Text, "admin unban '" + who + "'", serverip.Text, Convert.ToInt32(serverport.Text), "", "");

                    break;
                case "mutetext":

                    sendrcon(rcon.Text, "admin PlayerMute '" + who + "'", serverip.Text, Convert.ToInt32(serverport.Text), "", "");

                    break;
                case "mutevoip":

                    sendrcon(rcon.Text, "admin PlayerVOIPMute '" + who + "'", serverip.Text, Convert.ToInt32(serverport.Text), "", "");

                    break;
                case "wait 2000 ms":

                    System.Threading.Thread.Sleep(2000);

                    break;
                case "canclosentb":

                    if (stripcolor(whocolor.ToLower()).Replace(user.ToLower(), "") == textBox39.Text.ToLower())
                    {
                        sendrcon(rcon.Text, "say '" + whocolor + ": ^7Stopped application'", serverip.Text, Convert.ToInt32(serverport.Text), "", "");
                        checkntbstatus();
                    }
                    break;




            }


        }
        private void leenow()
        {

            //       log.AppendText(DateTime.Now.ToLocalTime() + "\t" + "####Updating Global Database" + "\r\n");

            string s = "0";
            string g = "0";
            int i = -1;
            Application.DoEvents();
            //   barra.Style = ProgressBarStyle.Marquee;
            //     CountryLookup cl = new CountryLookup(Application.StartupPath + "\\GeoIP.dat");
            char[] r = { '\t' };
            string[] arr;
            //  string country;
            try
            {
                FileStream fs = new FileStream(Application.StartupPath + "\\stats\\now.txt", FileMode.Open, FileAccess.Read);
                StreamReader m_streamReader = new StreamReader(fs);

                // Write to the file using StreamWriter class 
                m_streamReader.BaseStream.Seek(0, SeekOrigin.Begin);
                string strLine = m_streamReader.ReadLine();
                strLine = m_streamReader.ReadLine();
                strLine = m_streamReader.ReadLine();
                strLine = m_streamReader.ReadLine();
                strLine = m_streamReader.ReadLine();
                strLine = m_streamReader.ReadLine();
                strLine = m_streamReader.ReadLine();

                while (strLine != null)
                {

                    s = "0";
                    g = "0";

                    arr = strLine.Split(r);



                    i = -1;
                    i = globallv.FindItem(arr[0]);


                    if (arr[8].ToLower() == "strogg")
                    {
                        s = "1";
                        g = "0";
                    }
                    else if (arr[8].ToLower() == "gdf")
                    {
                        s = "0";
                        g = "1";
                    }



                    if (i == -1)
                    {
                        if (arr[0].ToLower() != "connecting...")
                        {


                            globallv.Items.Add(new ListViewItem(new string[] { arr[0], arr[1], arr[2], arr[4], arr[5], arr[6], arr[7].Replace(",","."), s, g, arr[9], arr[11], (Convert.ToInt32(s) + Convert.ToInt32(g) ).ToString()  }));





                        }

                    }
                    else
                    {


                        //agregue esto
                        globallv.Items[i].SubItems[1].Text = arr[1];
                        globallv.Items[i].SubItems[2].Text = arr[2];


                        globallv.Items[i].SubItems[3].Text = Convert.ToString(Convert.ToInt32(globallv.Items[i].SubItems[3].Text) + Convert.ToInt32(arr[4]));
                        globallv.Items[i].SubItems[4].Text = Convert.ToString(Convert.ToInt32(globallv.Items[i].SubItems[4].Text) + Convert.ToInt32(arr[5]));
                        globallv.Items[i].SubItems[5].Text = Convert.ToString(Convert.ToInt32(globallv.Items[i].SubItems[5].Text) + Convert.ToInt32(arr[6]));
                        globallv.Items[i].SubItems[7].Text = Convert.ToString(Convert.ToInt32(globallv.Items[i].SubItems[7].Text) + Convert.ToInt32(s));
                        globallv.Items[i].SubItems[8].Text = Convert.ToString(Convert.ToInt32(globallv.Items[i].SubItems[8].Text) + Convert.ToInt32(g));
                        globallv.Items[i].SubItems[9].Text = arr[9];
                        globallv.Items[i].SubItems[10].Text = arr[11];
                        globallv.Items[i].SubItems[11].Text = Convert.ToString(Convert.ToInt32(globallv.Items[i].SubItems[11].Text) + (Convert.ToInt32(s) + Convert.ToInt32(g)) );

                        if (Convert.ToString(GCD(Convert.ToInt32(globallv.Items[i].SubItems[4].Text), Convert.ToInt32(globallv.Items[i].SubItems[5].Text))).Length > 3)
                        {
                            globallv.Items[i].SubItems[6].Text = Convert.ToString(GCD(Convert.ToInt32(globallv.Items[i].SubItems[4].Text), Convert.ToInt32(globallv.Items[i].SubItems[5].Text))).Substring(0, 4).Replace(",",".");

                        }
                        else
                        {
                            globallv.Items[i].SubItems[6].Text = Convert.ToString(GCD(Convert.ToInt32(globallv.Items[i].SubItems[4].Text), Convert.ToInt32(globallv.Items[i].SubItems[5].Text))).Replace(",", ".");

                        }


                    }

                    strLine = m_streamReader.ReadLine();


                }

                m_streamReader.Close();
                fs.Dispose();
                m_streamReader.Dispose();
                label37.Text = globallv.Items.Count.ToString();

                if (checkBox6.Checked == true)
                {
                    try
                    {
                        File.Copy(Application.StartupPath + "\\stats\\global.txt", Application.StartupPath + "\\stats\\backup\\" + "global_" + DateTime.Parse(DateTime.UtcNow.ToUniversalTime().ToString()).ToString("yyyyMMdd_hhmmss") + ".txt", true);
                    }
                    catch
                    {

                    }
                }


            }

            catch (Exception error)
            {
                errorlv.Items.Add(new ListViewItem(new string[] { DateTime.Now.ToLocalTime().ToString(), error.TargetSite.Name.ToString(), error.Message + "\r\n" + error.StackTrace }));

            }
        }
        private void statsnow()
        {


          




            string ntbhtmlcode = "";
            string htmlbody = loadhtmlbase("now.dat");
            try
            {
                FileStream fs19 = new FileStream(Application.StartupPath + "\\stats\\now.html", FileMode.Create, FileAccess.Write);
                StreamWriter m_streamWriter = new StreamWriter(fs19);
                m_streamWriter.Flush();

                m_streamWriter.BaseStream.Seek(0, SeekOrigin.Begin);

                FileStream log1 = new FileStream(Application.StartupPath + "\\stats\\now.txt", FileMode.Create, FileAccess.Write);
                StreamWriter m_logWriter = new StreamWriter(log1);
                m_logWriter.Flush();

                m_logWriter.BaseStream.Seek(0, SeekOrigin.Begin);



                htmlbody = htmlbody.Replace("$$$headertag$$$", colorhtml(textBox60.Text + "Stats Information. Last Update: " + DateTime.Parse(DateTime.UtcNow.ToUniversalTime().ToString()).ToString("MM/dd/yy hh:mm:ss tt") + " UTC"));



                htmlbody = htmlbody.Replace("$$mapa$$", info.Items[2].SubItems[1].Text.ToLower());
                htmlbody = htmlbody.Replace("$$$servername$$$", colorhtml(info.Items[0].SubItems[2].Text));
                m_logWriter.Write("Name:" + "\t" + info.Items[0].SubItems[1].Text);
                m_logWriter.WriteLine();
                htmlbody = htmlbody.Replace("$$$mapname$$$", colorhtml(textBox61.Text +info.Items[2].SubItems[1].Text));
                htmlbody = htmlbody.Replace("$$$players$$$", colorhtml(textBox61.Text +info.Items[6].SubItems[1].Text));
                m_logWriter.Write("Map:" + "\t" + info.Items[2].SubItems[1].Text);
                m_logWriter.WriteLine();
                htmlbody = htmlbody.Replace("$$$status$$$", colorhtml(textBox62.Text +info.Items[1].SubItems[1].Text));
                htmlbody = htmlbody.Replace("$$$time$$$", colorhtml(textBox62.Text +info.Items[3].SubItems[1].Text));
                m_logWriter.Write("Status:" + "\t" + info.Items[1].SubItems[1].Text + " - " + info.Items[3].SubItems[1].Text);
                m_logWriter.WriteLine();
                m_logWriter.Write("Players:" + "\t" + info.Items[6].SubItems[1].Text);
                m_logWriter.WriteLine();
                htmlbody = htmlbody.Replace("$$$round$$$", colorhtml(textBox63.Text +info.Items[4].SubItems[1].Text));
                htmlbody = htmlbody.Replace("$$$score$$$", colorhtml(textBox63.Text +info.Items[5].SubItems[1].Text));
                m_logWriter.Write("Round:" + "\t" + info.Items[4].SubItems[1].Text);
                m_logWriter.WriteLine();
                m_logWriter.Write("Score:" + "\t" + info.Items[5].SubItems[1].Text);
                m_logWriter.WriteLine();
                htmlbody = htmlbody.Replace("$$$headertag$$$", colorhtml(textBox60.Text +"Stats Information. Last Update: " + DateTime.Parse(DateTime.UtcNow.ToUniversalTime().ToString()).ToString("MM/dd/yy hh:mm:ss tt") + " UTC"));

                foreach (ListViewItem h in players.Items)
                {
             
                    ntbhtmlcode = ntbhtmlcode + "<tr>" + "\r\n";
                    ntbhtmlcode = ntbhtmlcode + "<td><div align=\"center\"><b>" + colorhtml(h.SubItems[9].Text) + "</b></FONT></div></td>" + "\r\n";
                    ntbhtmlcode = ntbhtmlcode + "<td><div align=\"center\">" + colorhtml(h.SubItems[3].Text) + "</div></td>" + "\r\n";
                    ntbhtmlcode = ntbhtmlcode + "<td><div align=\"center\">" + h.SubItems[1].Text + "</div></td>" + "\r\n";
                    ntbhtmlcode = ntbhtmlcode + "<td><div align=\"center\">" + h.SubItems[5].Text + "</div></td>" + "\r\n";
                    ntbhtmlcode = ntbhtmlcode + "<td><div align=\"center\">" + h.SubItems[7].Text + "</div></td>" + "\r\n";
                    ntbhtmlcode = ntbhtmlcode + "<td><div align=\"center\">" + h.SubItems[8].Text + "</div></td>" + "\r\n";
                  
                    if (Convert.ToString(GCD(Convert.ToInt32(h.SubItems[7].Text), Convert.ToInt32(h.SubItems[8].Text))).Length > 3)
                    {
                        ntbhtmlcode = ntbhtmlcode + "<td><div align=\"center\">" + Convert.ToString(GCD(Convert.ToInt32(h.SubItems[7].Text), Convert.ToInt32(h.SubItems[8].Text))).Substring(0, 4).Replace(",", ".") + "</div></td>" + "\r\n";
                        if (h.SubItems[3].Text != "")
                        {
                            m_logWriter.Write(h.SubItems[2].Text.ToLower().Replace(h.SubItems[3].Text.ToLower(), "") + "\t" + h.SubItems[9].Text + "\t" + h.SubItems[3].Text + "\t" + h.SubItems[1].Text + "\t" + h.SubItems[5].Text + "\t" + h.SubItems[7].Text + "\t" + h.SubItems[8].Text + "\t" + Convert.ToString(GCD(Convert.ToInt32(h.SubItems[7].Text), Convert.ToInt32(h.SubItems[8].Text))).Substring(0, 4).Replace(co.Text, pu.Text) + "\t" + h.SubItems[6].Text + "\t" + h.SubItems[4].Text + "\t" + h.Text + "\t" + DateTime.Parse(DateTime.UtcNow.ToUniversalTime().ToString()).ToString("MM/dd/yy hh:mm:ss tt"));
                            m_logWriter.WriteLine();
                        }
                        else
                        {
                            m_logWriter.Write(h.SubItems[2].Text.ToLower() + "\t" + h.SubItems[9].Text + "\t" + h.SubItems[3].Text + "\t" + h.SubItems[1].Text + "\t" + h.SubItems[5].Text + "\t" + h.SubItems[7].Text + "\t" + h.SubItems[8].Text + "\t" + Convert.ToString(GCD(Convert.ToInt32(h.SubItems[7].Text), Convert.ToInt32(h.SubItems[8].Text))).Substring(0, 4).Replace(co.Text, pu.Text) + "\t" + h.SubItems[6].Text + "\t" + h.SubItems[4].Text + "\t" + h.Text + "\t" + DateTime.Parse(DateTime.UtcNow.ToUniversalTime().ToString()).ToString("MM/dd/yy hh:mm:ss tt"));
                            m_logWriter.WriteLine();

                        }
                    }

                    else
                    {
                        ntbhtmlcode = ntbhtmlcode + "<td><div align=\"center\">" + Convert.ToString(GCD(Convert.ToInt32(h.SubItems[7].Text), Convert.ToInt32(h.SubItems[8].Text))).Replace(",", ".") + "</div></td>" + "\r\n";
                        if (h.SubItems[3].Text != "")
                        {
                            m_logWriter.Write(h.SubItems[2].Text.ToLower().Replace(h.SubItems[3].Text.ToLower(), "") + "\t" + h.SubItems[9].Text + "\t" + h.SubItems[3].Text + "\t" + h.SubItems[1].Text + "\t" + h.SubItems[5].Text + "\t" + h.SubItems[7].Text + "\t" + h.SubItems[8].Text + "\t" + Convert.ToString(GCD(Convert.ToInt32(h.SubItems[7].Text), Convert.ToInt32(h.SubItems[8].Text))).Replace(co.Text, pu.Text) + "\t" + h.SubItems[6].Text + "\t" + h.SubItems[4].Text + "\t" + h.Text + "\t" + DateTime.Parse(DateTime.UtcNow.ToUniversalTime().ToString()).ToString("MM/dd/yy hh:mm:ss tt"));
                            m_logWriter.WriteLine();
                        }
                        else
                        {
                            m_logWriter.Write(h.SubItems[2].Text.ToLower() + "\t" + h.SubItems[9].Text + "\t" + h.SubItems[3].Text + "\t" + h.SubItems[1].Text + "\t" + h.SubItems[5].Text + "\t" + h.SubItems[7].Text + "\t" + h.SubItems[8].Text + "\t" + Convert.ToString(GCD(Convert.ToInt32(h.SubItems[7].Text), Convert.ToInt32(h.SubItems[8].Text))).Replace(co.Text, pu.Text) + "\t" + h.SubItems[6].Text + "\t" + h.SubItems[4].Text + "\t" + h.Text + "\t" + DateTime.Parse(DateTime.UtcNow.ToUniversalTime().ToString()).ToString("MM/dd/yy hh:mm:ss tt"));
                            m_logWriter.WriteLine();

                        }

                    }

                    ntbhtmlcode = ntbhtmlcode + "<td><div align=\"center\">" + h.SubItems[6].Text + "</div></td>" + "\r\n";
                    ntbhtmlcode = ntbhtmlcode + "<td><div align=\"center\">" + h.SubItems[4].Text + "</div></td>" + "\r\n";
                    ntbhtmlcode = ntbhtmlcode + "<td><div align=\"center\">" + h.Text + "</div></td>" + "\r\n";
                    ntbhtmlcode = ntbhtmlcode + "</tr>" + "\r\n";

                }

                htmlbody = htmlbody.Replace("$$namekills", colorhtml(info.Items[8].SubItems[1].Text)).Replace("($$kills)", colorhtml(textBox59.Text + "(" + info.Items[8].SubItems[2].Text + ")"));
                htmlbody = htmlbody.Replace("$$namekdratio", colorhtml(info.Items[10].SubItems[1].Text)).Replace("($$kdratio)", colorhtml(textBox59.Text + "(" + info.Items[10].SubItems[2].Text + ")"));
                htmlbody = htmlbody.Replace("$$namedeaths", colorhtml(info.Items[9].SubItems[1].Text)).Replace("($$deaths)", colorhtml(textBox59.Text + "(" + info.Items[9].SubItems[2].Text + ")"));

                htmlbody = htmlbody.Replace("$$$$$NTBHTMLCODE$$$$$", ntbhtmlcode);


                m_streamWriter.Write(htmlbody);
                m_streamWriter.WriteLine();

                htmlbody = null;
                ntbhtmlcode = null;

                m_streamWriter.Flush();

                m_logWriter.Flush();



                m_logWriter.Close();
                m_streamWriter.Close();

                log1.Dispose();
                fs19.Dispose();
                m_streamWriter.Dispose();
                m_logWriter.Dispose();
            }
            catch (Exception error)
            {
                errorlv.Items.Add(new ListViewItem(new string[] { DateTime.Now.ToLocalTime().ToString(), error.TargetSite.Name.ToString(), error.Message + "\r\n" + error.StackTrace }));

            }



        }
        private void globaltohtml()
        {
            int xp = 0;
            int kills = 0;
            int deaths = 0;
            decimal kdratio = 0;
            string namexp = "";
            string namekills = "";
            string namedeaths = "";
            string namekdratio = ""; 


            string ntbhtmlcode = "";
            string htmlbody = loadhtmlbase("global.dat");

            try
            {
                FileStream fs19 = new FileStream(Application.StartupPath + "\\stats\\global.html", FileMode.Create, FileAccess.Write);
                StreamWriter m_streamWriter = new StreamWriter(fs19);
                m_streamWriter.Flush();

                m_streamWriter.BaseStream.Seek(0, SeekOrigin.Begin);

                FileStream global = new FileStream(Application.StartupPath + "\\stats\\global.txt", FileMode.Create, FileAccess.Write);
                StreamWriter m_globalWriter = new StreamWriter(global);
                m_globalWriter.Flush();

                m_globalWriter.BaseStream.Seek(0, SeekOrigin.Begin);



                htmlbody = htmlbody.Replace("$$$headertag$$$", colorhtml(textBox68.Text + "Global Information. Last Update: " + DateTime.Parse(DateTime.UtcNow.ToUniversalTime().ToString()).ToString("MM/dd/yy hh:mm:ss tt") + " UTC"));






                foreach (ListViewItem h in globallv.Items)
                {

                    if (Convert.ToInt32(h.SubItems[3].Text) > xp)
                    {
                        xp = Convert.ToInt32(h.SubItems[3].Text);
                        namexp = h.SubItems[1].Text;
                    }
                    if (Convert.ToInt32(h.SubItems[4].Text) > kills)
                    {
                        kills = Convert.ToInt32(h.SubItems[4].Text);
                        namekills = h.SubItems[1].Text;

                    }



                    if (Convert.ToInt32(h.SubItems[5].Text) > deaths )
                    {
                        deaths  = Convert.ToInt32(h.SubItems[5].Text);
                        namedeaths = h.SubItems[1].Text;
                    }

                    if (Convert.ToDecimal(h.SubItems[6].Text.Replace(pu.Text ,co.Text) ) > kdratio)
                    {
                        kdratio = Convert.ToDecimal(h.SubItems[6].Text.Replace(pu.Text, co.Text));
                        namekdratio = h.SubItems[1].Text;
                    }

                    
                    ntbhtmlcode = ntbhtmlcode + "<tr>" + "\r\n";
                    ntbhtmlcode = ntbhtmlcode + "<td><div align=\"center\"><b>" + colorhtml(h.SubItems[1].Text) + "</b></FONT></div></td>" + "\r\n";
                    ntbhtmlcode = ntbhtmlcode + "<td><div align=\"center\">" + colorhtml(h.SubItems[2].Text) + "</div></td>" + "\r\n";
                    ntbhtmlcode = ntbhtmlcode + "<td><div align=\"center\">" + h.SubItems[3].Text + "</div></td>" + "\r\n";
                    try
                    {
                        ntbhtmlcode = ntbhtmlcode + "<td><div align=\"center\">" + Convert.ToString((Convert.ToInt32(h.SubItems[3].Text) / Convert.ToInt32(h.SubItems[11].Text))) + "</div></td>" + "\r\n";
                    }
                    catch
                    {
                        ntbhtmlcode = ntbhtmlcode + "<td><div align=\"center\">" + h.SubItems[3].Text + "</div></td>" + "\r\n";

                    }
                        ntbhtmlcode = ntbhtmlcode + "<td><div align=\"center\">" + h.SubItems[4].Text + "</div></td>" + "\r\n";
                    ntbhtmlcode = ntbhtmlcode + "<td><div align=\"center\">" + h.SubItems[5].Text + "</div></td>" + "\r\n";
                    ntbhtmlcode = ntbhtmlcode + "<td><div align=\"center\">" + h.SubItems[6].Text + "</div></td>" + "\r\n";
                    ntbhtmlcode = ntbhtmlcode + "<td><div align=\"center\">" + h.SubItems[7].Text + "</div></td>" + "\r\n";
                    ntbhtmlcode = ntbhtmlcode + "<td><div align=\"center\">" + h.SubItems[8].Text + "</div></td>" + "\r\n";
                    ntbhtmlcode = ntbhtmlcode + "<td><div align=\"center\">" + h.SubItems[9].Text + "</div></td>" + "\r\n";
                    ntbhtmlcode = ntbhtmlcode + "<td><div align=\"center\">" + h.SubItems[10].Text + "</div></td>" + "\r\n";
                    ntbhtmlcode = ntbhtmlcode + "<td><div align=\"center\">" + h.SubItems[11].Text + "</div></td>" + "\r\n";
                    ntbhtmlcode = ntbhtmlcode + "</tr>" + "\r\n";
                    m_globalWriter.Write(h.Text + "\t" + h.SubItems[1].Text + "\t" + h.SubItems[2].Text + "\t" + h.SubItems[3].Text + "\t" + h.SubItems[4].Text + "\t" + h.SubItems[5].Text + "\t" + h.SubItems[6].Text + "\t" + h.SubItems[7].Text + "\t" + h.SubItems[8].Text + "\t" + h.SubItems[9].Text + "\t" + h.SubItems[10].Text + "\t" + h.SubItems[11].Text);
                    m_globalWriter.WriteLine();


                }

                htmlbody = htmlbody.Replace("$$namekills", colorhtml(namekills));
                htmlbody = htmlbody.Replace("($$kills$$)", colorhtml(textBox65.Text + "(" + kills.ToString() + ")"));

                htmlbody = htmlbody.Replace("$$namekdratio", colorhtml(namekdratio));
                htmlbody = htmlbody.Replace("($$kdratio$$)", colorhtml(textBox65.Text + "(" + kdratio.ToString() + ")"));

                htmlbody = htmlbody.Replace("$$namedeaths", colorhtml(namedeaths));
                htmlbody = htmlbody.Replace("($$deaths$$)", colorhtml(textBox65.Text + "(" + deaths.ToString() + ")"));

                htmlbody = htmlbody.Replace("$$namexp", colorhtml(namexp));
                htmlbody = htmlbody.Replace("($$xp$$)", colorhtml(textBox65.Text + "(" + xp.ToString() + ")"));


                htmlbody = htmlbody.Replace("$$$$$NTBHTMLCODE$$$$$", ntbhtmlcode);

                m_streamWriter.Write(htmlbody);
                m_streamWriter.WriteLine();


                m_streamWriter.Flush();

                m_globalWriter.Flush();

                m_streamWriter.Close();
                m_globalWriter.Close();

                global.Dispose();

                fs19.Dispose();
                m_streamWriter.Dispose();
                m_globalWriter.Dispose();

                if (checkBox6.Checked == true)
                {
                    try
                    {
                        File.Copy(Application.StartupPath + "\\stats\\global.txt", Application.StartupPath + "\\stats\\backup\\" + "global_" + DateTime.Parse(DateTime.UtcNow.ToUniversalTime().ToString()).ToString("yyyyMMdd_hhmmss") + ".txt", true);
                    }
                    catch
                    {

                    }
                }
            }
            catch (Exception error)
            {
                errorlv.Items.Add(new ListViewItem(new string[] { DateTime.Now.ToLocalTime().ToString(), error.TargetSite.Name.ToString(), error.Message + "\r\n" + error.StackTrace }));

            }

        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            tryagain = 0;
            scan2();

            ReleaseUnusedMemory();
        }

        void resetinfo()
        {

            string lastmap = info.Items[2].SubItems[1].Text;
            string lastslots = info.Items[13].SubItems[1].Text;

            info.Items.Clear();
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "Server Name",
            "-",
            "-"}, -1);
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem(new string[] {
            "Server Status",
            "-",
            "-"}, -1);
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem(new string[] {
            "Server Map",
            "Unknown",
            lastmap}, -1);
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem(new string[] {
            "Map Time:",
            "00:00",
            "-"}, -1);
            System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem(new string[] {
            "Campaign Round",
            "-",
            "-"}, -1);
            System.Windows.Forms.ListViewItem listViewItem6 = new System.Windows.Forms.ListViewItem(new string[] {
            "Campaign Score",
            "-",
            "-"}, -1);
            System.Windows.Forms.ListViewItem listViewItem7 = new System.Windows.Forms.ListViewItem(new string[] {
            "Server Players",
            "0/0",
            "0"}, -1);
            System.Windows.Forms.ListViewItem listViewItem8 = new System.Windows.Forms.ListViewItem(new string[] {
            "Most XP",
            "-",
            "0",
            ""}, -1);
            System.Windows.Forms.ListViewItem listViewItem9 = new System.Windows.Forms.ListViewItem(new string[] {
            "Most Kills",
            "-",
            "0"}, -1);
            System.Windows.Forms.ListViewItem listViewItem10 = new System.Windows.Forms.ListViewItem(new string[] {
            "Most Deaths",
            "-",
            "0"}, -1);
            System.Windows.Forms.ListViewItem listViewItem11 = new System.Windows.Forms.ListViewItem(new string[] {
            "Best K/D Ratio",
            "-",
            "0"}, -1);
            System.Windows.Forms.ListViewItem listViewItem12 = new System.Windows.Forms.ListViewItem(new string[] {
            "GDF Kills / Deaths",
            "0",
            "0"}, -1);
            System.Windows.Forms.ListViewItem listViewItem13 = new System.Windows.Forms.ListViewItem(new string[] {
            "Strogg Kills / Deaths",
            "0",
            "0"}, -1);
            System.Windows.Forms.ListViewItem listViewItem14 = new System.Windows.Forms.ListViewItem(new string[] {
            "Slots Used",
            "0",
            lastslots}, -1);
            System.Windows.Forms.ListViewItem listViewItem15 = new System.Windows.Forms.ListViewItem(new string[] {
            "XP Whore",
            "-",
            "0"}, -1);
            System.Windows.Forms.ListViewItem listViewItem16 = new System.Windows.Forms.ListViewItem(new string[] {
            "KD/R#1",
            "0",
            "-1"}, -1);
            System.Windows.Forms.ListViewItem listViewItem17 = new System.Windows.Forms.ListViewItem(new string[] {
            "KD/R#2",
            "0",
            "-1"}, -1);
            System.Windows.Forms.ListViewItem listViewItem18 = new System.Windows.Forms.ListViewItem(new string[] {
            "KD/R#3",
            "0",
            "-1"}, -1);
            System.Windows.Forms.ListViewItem listViewItem19 = new System.Windows.Forms.ListViewItem(new string[] {
            "Killer#1",
            "0",
            "-1"}, -1);
            System.Windows.Forms.ListViewItem listViewItem20 = new System.Windows.Forms.ListViewItem(new string[] {
            "Killer#2",
            "0",
            "-1"}, -1);
            System.Windows.Forms.ListViewItem listViewItem21 = new System.Windows.Forms.ListViewItem(new string[] {
            "Killer#3",
            "0",
            "-1"}, -1);

            this.info.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3,
            listViewItem4,
            listViewItem5,
            listViewItem6,
            listViewItem7,
            listViewItem8,
            listViewItem9,
            listViewItem10,
            listViewItem11,
            listViewItem12,
            listViewItem13,
            listViewItem14,
            listViewItem15,
            listViewItem16,
            listViewItem17,
            listViewItem18,
            listViewItem19,
            listViewItem20,
            listViewItem21});



        }

        private void players_ColumnClick(object sender, ColumnClickEventArgs e)
        {

        }





        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

            this.Opacity = 1;
            serverip.BorderStyle = BorderStyle.Fixed3D;
            serverport.BorderStyle = BorderStyle.Fixed3D;
            rcon.BorderStyle = BorderStyle.Fixed3D;
            textBox15.BorderStyle = BorderStyle.Fixed3D;

        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            serverip.BorderStyle = BorderStyle.FixedSingle;
            serverport.BorderStyle = BorderStyle.FixedSingle;
            rcon.BorderStyle = BorderStyle.FixedSingle;
            textBox15.BorderStyle = BorderStyle.FixedSingle;
            this.Opacity = 0.75;

        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            serverip.BorderStyle = BorderStyle.FixedSingle;
            serverport.BorderStyle = BorderStyle.FixedSingle;
            rcon.BorderStyle = BorderStyle.FixedSingle;
            textBox15.BorderStyle = BorderStyle.FixedSingle;

            this.Opacity = 0.5;

        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            serverip.BorderStyle = BorderStyle.FixedSingle;
            serverport.BorderStyle = BorderStyle.FixedSingle;
            rcon.BorderStyle = BorderStyle.FixedSingle;
            textBox15.BorderStyle = BorderStyle.FixedSingle;

            this.Opacity = 0.25;

        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            Application.DoEvents();
            this.Hide();

            this.notifyIcon1.Visible = true;
        }

        private void button18_Click(object sender, EventArgs e)
        {
            if (comboBox5.Text == "") { return; }
            lcb3.Checked = true;
            try
            {
                if (!comboBox5.Items.Contains(comboBox5.Text))
                {
                    comboBox5.Items.Insert(0, comboBox5.Text);
                }
                sendrcon(rcon.Text, comboBox5.Text, serverip.Text, Convert.ToInt32(serverport.Text), "","");
                comboBox5.Text = "";
            }
            catch
            {
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            log.Clear();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(log.Text);
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            splitContainer2.Panel2Collapsed = !(checkBox4.Checked);

        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            splitContainer3.Panel2Collapsed = !(checkBox3.Checked);
            if ((checkBox2.Checked == false) & (checkBox3.Checked == false))
            {
                splitContainer1.Panel2Collapsed = true;
            }
            else
            {
                splitContainer1.Panel2Collapsed = false;

                if (checkBox2.Checked == false)
                {
                    splitContainer3.Panel1Collapsed = true;
                }



            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            splitContainer3.Panel1Collapsed = !(checkBox2.Checked);

            if ((checkBox2.Checked == false) & (checkBox3.Checked == false))
            {
                splitContainer1.Panel2Collapsed = true;
            }
            else
            {
                splitContainer1.Panel2Collapsed = false;

                if (checkBox3.Checked == false)
                {
                    splitContainer3.Panel2Collapsed = true;
                }
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Application.DoEvents();
            Application.Exit();
        }
        private void savelog()
        {
            try
            {
                FileStream fs22 = new FileStream(Application.StartupPath + "\\logs\\" + "console_" + DateTime.Parse(DateTime.UtcNow.ToUniversalTime().ToString()).ToString("yyyyMMdd_hhmmss") + ".txt", FileMode.Create, FileAccess.Write);
                StreamWriter m_streamWriter = new StreamWriter(fs22);
                m_streamWriter.Flush();
                m_streamWriter.BaseStream.Seek(0, SeekOrigin.Begin);
                m_streamWriter.Write(log.Text);
                m_streamWriter.WriteLine();
                m_streamWriter.Flush();
                m_streamWriter.Close();
                fs22.Dispose();
                m_streamWriter.Dispose();
                Cursor = Cursors.Default;
            }
            catch
            {
                Cursor = Cursors.Default;
            }
        }
        private void savepb()
        {

            if (pbh.Items.Count == 0)
            {
                return;
            }


            try
            {
                FileStream fs22 = new FileStream(Application.StartupPath + "\\" + "pblist.txt", FileMode.Create, FileAccess.Write);
                StreamWriter m_streamWriter = new StreamWriter(fs22);
                m_streamWriter.Flush();
                m_streamWriter.BaseStream.Seek(0, SeekOrigin.Begin);

                foreach (ListViewItem pe in pbh.Items )
                {
                    m_streamWriter.Write(pe.Text + "\t" + pe.SubItems[1].Text + "\t" + pe.SubItems[2].Text + "\t" + pe.SubItems[3].Text + "\t" + pe.SubItems[4].Text + "\t" + pe.SubItems[5].Text + "\t" + pe.SubItems[6].Text + "\t" + pe.SubItems[7].Text + "\t" + pe.SubItems[8].Text + "\t" + pe.SubItems[9].Text + "\t" + pe.SubItems[10].Text + "\t" + pe.Checked.ToString());
                    m_streamWriter.WriteLine();


                }
                
                
                m_streamWriter.Flush();
                m_streamWriter.Close();
                fs22.Dispose();
                m_streamWriter.Dispose();
                Cursor = Cursors.Default;

                if ((checkBox17.Checked) & (!checkBox1.Checked))
                {
                    backuppblist();
                }


                if (checkBox20.Checked == true)
                {
                    if (Directory.Exists(Application.StartupPath + "\\" + "pblistbackup"))
                    {
                      string loghtml = "PBL_" + DateTime.Parse(DateTime.UtcNow.ToUniversalTime().ToString()).ToString("yyyyMMdd_hhmmss");
                      File.Copy(Application.StartupPath + "\\pblist.txt", Application.StartupPath + "\\pblistbackup\\" + loghtml + ".txt", true);




                    }
                    else
                    {
                        Directory.CreateDirectory(Application.StartupPath + "\\" + "pblistbackup");
                        string loghtml = "PBL_" + DateTime.Parse(DateTime.UtcNow.ToUniversalTime().ToString()).ToString("yyyyMMdd_hhmmss");
                        File.Copy(Application.StartupPath + "\\pblist.txt", Application.StartupPath + "\\pblistbackup\\" + loghtml + ".txt", true);

                   


                    }



                }




            }
            catch
            {
                Cursor = Cursors.Default;
            }
        }
        private void savepb2()
        {
            if (pbh.Items.Count == 0)
            {
                return;
            }


            try
            {
                FileStream fs22 = new FileStream(Application.StartupPath + "\\" + "pblist.txt", FileMode.Create, FileAccess.Write);
                StreamWriter m_streamWriter = new StreamWriter(fs22);
                m_streamWriter.Flush();
                m_streamWriter.BaseStream.Seek(0, SeekOrigin.Begin);

                foreach (ListViewItem pe in pbh.Items)
                {
                    m_streamWriter.Write(pe.Text + "\t" + pe.SubItems[1].Text + "\t" + pe.SubItems[2].Text + "\t" + pe.SubItems[3].Text + "\t" + pe.SubItems[4].Text + "\t" + pe.SubItems[5].Text + "\t" + pe.SubItems[6].Text + "\t" + pe.SubItems[7].Text + "\t" + pe.SubItems[8].Text + "\t" + pe.SubItems[9].Text + "\t" + pe.SubItems[10].Text + "\t" + pe.Checked.ToString());
                    m_streamWriter.WriteLine();


                }


                m_streamWriter.Flush();
                m_streamWriter.Close();
                fs22.Dispose();
                m_streamWriter.Dispose();
                Cursor = Cursors.Default;

           

               




            }
            catch
            {
                Cursor = Cursors.Default;
            }
        }
        private void uploadsignature()
        {
            pictureBox1.Image.Save(Application.StartupPath + "\\" + textBox15.Text + ".png", ImageFormat.Png);
            if (ftpConnection1.IsTransferring == false)
            {
                uploadtomainfolder(new string[] { Application.StartupPath + "\\" + textBox15.Text + ".png" });
            }

        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            if (ftplog.Lines.Length >= 300) { ftplog.Clear(); }
            if (errorlv.Items.Count >= 150) { errorlv.Items.Clear(); }

            if (checkBox21.Checked == true)
            {
                if (log.Lines.Length >= 250)
                {
                    if (checkBox16.Checked == true)
                    {
                        savelog();
                    }
                    log.Clear();
                }
            }

            p1.Text = Convert.ToString((Convert.ToInt32(p1.Text) + 1));
            p2.Text = Convert.ToString((Convert.ToInt32(p2.Text) + 1));
            p3.Text = Convert.ToString((Convert.ToInt32(p3.Text) + 1));
            p4.Text = Convert.ToString((Convert.ToInt32(p4.Text) + 1));
            p5.Text = Convert.ToString((Convert.ToInt32(p5.Text) + 1));
            p6.Text = Convert.ToString((Convert.ToInt32(p6.Text) + 1));
            p7.Text = Convert.ToString((Convert.ToInt32(p7.Text) + 1));


        

            if (ddm4.Checked == true)
            {
                if (Convert.ToInt32(p2.Text) >= comboBox1.Value)
                {
                    p2.Text = "0";
                    if (info.Items[1].SubItems[1].Text.ToLower() == "playing")
                    {
                        if ((info.Items[8].SubItems[1].Text != "-") & (info.Items[8].SubItems[2].Text != "0") & (info.Items[9].SubItems[1].Text != "-") & (info.Items[9].SubItems[2].Text != "0") & (info.Items[10].SubItems[1].Text != "-") & (info.Items[10].SubItems[2].Text != "0"))
                        {
                            Application.DoEvents();
                            try
                            {
                                sendrcon(rcon.Text, "say '" + textBox1.Text.Replace("$killername1", players.Items[Convert.ToInt32(info.Items[18].SubItems[2].Text)].SubItems[9].Text.Replace("'", "`")).Replace("$kills1", players.Items[Convert.ToInt32(info.Items[18].SubItems[2].Text)].SubItems[7].Text).Replace("$killerkd1", players.Items[Convert.ToInt32(info.Items[18].SubItems[2].Text)].SubItems[10].Text) + "'" + "\r" + "say '" + textBox2.Text.Replace("$killername2", players.Items[Convert.ToInt32(info.Items[19].SubItems[2].Text)].SubItems[9].Text.Replace("'", "`")).Replace("$kills2", players.Items[Convert.ToInt32(info.Items[19].SubItems[2].Text)].SubItems[7].Text).Replace("$killerkd2", players.Items[Convert.ToInt32(info.Items[19].SubItems[2].Text)].SubItems[10].Text)+ "'" + "\r" + "say '" + textBox9.Text.Replace("$killername3", players.Items[Convert.ToInt32(info.Items[20].SubItems[2].Text)].SubItems[9].Text.Replace("'", "`")).Replace("$kills3", players.Items[Convert.ToInt32(info.Items[20].SubItems[2].Text)].SubItems[7].Text).Replace("$killerkd3", players.Items[Convert.ToInt32(info.Items[20].SubItems[2].Text)].SubItems[10].Text )+ "'", serverip.Text, Convert.ToInt32(serverport.Text), "","");
                                System.Threading.Thread.Sleep(Convert.ToInt32(numericUpDown1.Value));
                                sendrcon(rcon.Text, "say '" + textBox18.Text.Replace("$kdrname1", players.Items[Convert.ToInt32(info.Items[15].SubItems[2].Text)].SubItems[9].Text.Replace("'", "`")).Replace("$kd1", players.Items[Convert.ToInt32(info.Items[15].SubItems[2].Text)].SubItems[10].Text) + "'" + "\r" + "say '" + textBox17.Text.Replace("$kdrname2", players.Items[Convert.ToInt32(info.Items[16].SubItems[2].Text)].SubItems[9].Text.Replace("'", "`")).Replace("$kd2", players.Items[Convert.ToInt32(info.Items[16].SubItems[2].Text)].SubItems[10].Text) + "'" + "\r" + "say '" + textBox16.Text.Replace("$kdrname3", players.Items[Convert.ToInt32(info.Items[17].SubItems[2].Text)].SubItems[9].Text.Replace("'", "`")).Replace("$kd3", players.Items[Convert.ToInt32(info.Items[17].SubItems[2].Text)].SubItems[10].Text) + "'", serverip.Text, Convert.ToInt32(serverport.Text), "","");
                                sendrcon(rcon.Text, "say '" + textBox3.Text.Replace("$lemming", info.Items[9].SubItems[1].Text.Replace("'", "`")).Replace("$deaths", info.Items[9].SubItems[2].Text) + "'", serverip.Text, Convert.ToInt32(serverport.Text), "","");
                            }
                            catch
                            {

                            }
                                if (Convert.ToInt32(p1.Text) >= textBox8.Value)
                            {
                                p1.Text = (Convert.ToInt32(p1.Text) - 1).ToString();
                            }
                        }
                    }
                }
            }

            if (checkBox5.Checked == true)
            {
                if (Convert.ToInt32(p1.Text) >= textBox8.Value)
                {
                    p1.Text = "0";
                    Application.DoEvents();
                    sendrcon(rcon.Text, "say '" + replacemsg(textBox4.Text) + "'" + "\r" + "say '" + replacemsg(textBox5.Text) + "'" + "\r" + "say '" + replacemsg(textBox6.Text) + "'" + "\r" + "say '" + replacemsg(textBox7.Text) + "'", serverip.Text, Convert.ToInt32(serverport.Text), "","");
                }
            }


            if (checkBox55.Checked == true)
            {
                if (Convert.ToInt32(p7.Text) >= ((numericUpDown9.Value *60)*60))
                {

                    savepb();
                    p7.Text = "0";
                    Application.DoEvents();

                //    sendrcon(rcon.Text, "say '" + replacemsg(textBox4.Text) + "'" + "\r" + "say '" + replacemsg(textBox5.Text) + "'" + "\r" + "say '" + replacemsg(textBox6.Text) + "'" + "\r" + "say '" + replacemsg(textBox7.Text) + "'", serverip.Text, Convert.ToInt32(serverport.Text), "", "");
                }
            }

            if (checkBox14.Checked == true)
            {
                if (Convert.ToInt32(p6.Text) >= numericUpDown4.Value)
                {
                    p6.Text = "0";
                    Application.DoEvents();
                    sendrcon2(rcon.Text, "pb_sv_plist", serverip.Text, Convert.ToInt32(serverport.Text), "", "");
                }
            }
            if (ddm3.Checked == true)
            {
                if (Convert.ToInt32(p3.Text) >= comboBox6.Value)
                {
                    p3.Text = "0";
                    if (info.Items[1].SubItems[1].Text.ToLower() == "playing")
                    {
                        if ((info.Items[11].SubItems[1].Text != "0") & (info.Items[11].SubItems[2].Text != "0") & (info.Items[12].SubItems[1].Text != "0") & (info.Items[12].SubItems[2].Text != "0"))
                        {
                            Application.DoEvents();
                            sendrcon(rcon.Text, "say '" + textBox23.Text.Replace("$gdfkills", info.Items[11].SubItems[1].Text).Replace("$gdfdeaths", info.Items[11].SubItems[2].Text) + "'" + "\r" + "say '" + textBox22.Text.Replace("$stroggkills", info.Items[12].SubItems[1].Text).Replace("$stroggdeaths", info.Items[12].SubItems[2].Text) + "'", serverip.Text, Convert.ToInt32(serverport.Text), "","");
                        }
                    }
                }
            }

            if ((checkBox40.CheckState == CheckState.Checked) & (checkBox1.Checked == false) )
            {
                if (Convert.ToInt32(p4.Text) >= numericUpDown6.Value)
                {
                    p4.Text = "0";
                    uploadsignature();
                }
            }

            if ((statsyes.CheckState == CheckState.Checked) & (checkBox1.Checked == false ))
            {
                if (Convert.ToInt32(p5.Text) >= statstime.Value)
                {
                    if (ftpConnection1.IsTransferring == false)
                    {
                        p5.Text = "0";
                        uploadtostatsfolder(new string[] { Application.StartupPath + "\\stats\\now.html" });
                    }

                }
            }

            

        }
        private void downloadtostatsfolder(string[] whatfiles)
        {

            Cursor = Cursors.WaitCursor;


            foreach (string q in whatfiles)
            {
                try
                {
                    if (!ftpConnection1.IsConnected) { ftpConnection1.Connect(); }

                    if (ftpConnection1.ServerDirectory != ftpstats.Text) { ftpConnection1.ChangeWorkingDirectory(ftpstats.Text); }





                    ftpConnection1.DownloadFile(Application.StartupPath + "\\stats\\", q);
                    if (checkBox35.Checked) { log.AppendText(DateTime.Now.ToLocalTime() + "\t" + "Downloading: " + q + "\r\n"); }

                }

                catch
                {

                    try
                    {

                        if (checkBox35.Checked) { log.AppendText(DateTime.Now.ToLocalTime() + "\t" + "Reconnecting..." + "\r\n"); }
                        if (!ftpConnection1.IsConnected) { ftpConnection1.Connect(); }
                        if (ftpConnection1.ServerDirectory != ftpstats.Text) { ftpConnection1.ChangeWorkingDirectory(ftpstats.Text); }
                        ftpConnection1.DownloadFile(Application.StartupPath + "\\stats\\", q);
                        if (checkBox35.Checked) { log.AppendText(DateTime.Now.ToLocalTime() + "\t" + "Downloading: " + q + "\r\n"); }
                    }

                    catch
                    {
                        if (checkBox35.Checked) { log.AppendText(DateTime.Now.ToLocalTime() + "\t" + "Reconnecting Failed" + "\r\n"); }
                        try
                        {
                            ftpConnection1.Close(true);
                        }
                        catch
                        {
                            if (checkBox35.Checked) { log.AppendText(DateTime.Now.ToLocalTime() + "\t" + "Error in FTP" + "\r\n"); }

                        }
                    }



                }





            }



            Cursor = Cursors.Default;


        }
        private void uploadtostatsfolder(string[] whatfiles)
        {
            Cursor = Cursors.WaitCursor;


            foreach (string q in whatfiles)
            {
                try
                {
                    if (!ftpConnection1.IsConnected) { ftpConnection1.Connect(); }

                    if (ftpConnection1.ServerDirectory != ftpstats.Text) { ftpConnection1.ChangeWorkingDirectory(ftpstats.Text); }





                    ftpConnection1.UploadFile(q, q.Substring(q.LastIndexOf("\\") + 1, (q.Length - q.LastIndexOf("\\") - 1)), false);//  .DownloadFile(Application.StartupPath + "\\stats\\", q);
                    if (checkBox35.Checked) { log.AppendText(DateTime.Now.ToLocalTime() + "\t" + "Uploading: " + q.Substring(q.LastIndexOf("\\") + 1, (q.Length - q.LastIndexOf("\\") - 1)) + "\r\n"); }


                }
                catch
                {

                    try
                    {

                        if (checkBox35.Checked) { log.AppendText(DateTime.Now.ToLocalTime() + "\t" + "Reconnecting..." + "\r\n"); };
                        if (!ftpConnection1.IsConnected) { ftpConnection1.Connect(); }
                        if (ftpConnection1.ServerDirectory != ftpstats.Text) { ftpConnection1.ChangeWorkingDirectory(ftpstats.Text); }
                        ftpConnection1.UploadFile(q, q.Substring(q.LastIndexOf("\\") + 1, (q.Length - q.LastIndexOf("\\") - 1)), false);//  .DownloadFile(Application.StartupPath + "\\stats\\", q);
                        if (checkBox35.Checked) { log.AppendText(DateTime.Now.ToLocalTime() + "\t" + "Uploading: " + q.Substring(q.LastIndexOf("\\") + 1, (q.Length - q.LastIndexOf("\\") - 1)) + "\r\n"); }
                    }

                    catch
                    {
                        if (checkBox35.Checked) { log.AppendText(DateTime.Now.ToLocalTime() + "\t" + "Reconnecting Failed" + "\r\n"); }
                        try
                        {
                            ftpConnection1.Close(true);
                        }
                        catch
                        {
                            if (checkBox35.Checked) { log.AppendText(DateTime.Now.ToLocalTime() + "\t" + "Error in FTP" + "\r\n"); }

                        }
                    }



                }







            }



            Cursor = Cursors.Default;


        }
        private void comboBox8_DropDown(object sender, EventArgs e)
        {
            comboBox8.Items.Clear();
            foreach (ListViewItem m in players.Items)
            {
                if (m.SubItems[3].Text == "")
                {
                    comboBox8.Items.Add(m.SubItems[2].Text.ToLower());
                }
                else
                {
                    comboBox8.Items.Add(m.SubItems[2].Text.Replace(m.SubItems[3].Text, "").ToLower());
                }
            }

        }

        private void comboBox9_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox9.Text.ToLower() == "say")
            {
                textBox27.Enabled = true;
                label83.Enabled = true;
                textBox27.Text = "";
            }
            else if (comboBox9.Text.ToLower() == "join usergroup")
            {
                comboBox4.Enabled  = true;
                label22.Enabled = true;
                textBox27.Enabled = false;
                label83.Enabled = false;
                textBox27.Text = "";

            }
            else
            {
                textBox27.Enabled = false;
                label83.Enabled = false;
                textBox27.Text = "";
                comboBox4.Enabled = false ;
                label22.Enabled = false ;
            }
        }

        private void button29_Click(object sender, EventArgs e)
        {
            if ((comboBox8.Text != "") & comboBox9.Text != "")
            {
                if (comboBox9.Text == "join usergroup")
                {
                    if (comboBox4.Text == "")
                    {
                        MessageBox.Show("Usergroup Empty");
                        return;
                    }
                    else
                    {
                        listView3.Items.Add(new ListViewItem(new string[] { comboBox8.Text.ToLower(), comboBox9.Text + " " + comboBox4.Text, textBox27.Text.Replace("\t", " "), "waiting", "0" }, 14));

                        

                    
                    
                    }
                    }
                else
                {
                    listView3.Items.Add(new ListViewItem(new string[] { comboBox8.Text.ToLower(), comboBox9.Text, textBox27.Text.Replace("\t", " "), "waiting", "0" }, 14));

                }
                }

        }

        private void button30_Click(object sender, EventArgs e)
        {
            try
            {
                listView3.Items.Remove(listView3.SelectedItems[0]);
            }
            catch
            {
            }

        }

        private void button31_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox9.Text == "join usergroup")
                {
                    if (comboBox4.Text == "")
                    {
                        MessageBox.Show("Usergroup Empty");
                        return;
                    }
                    else
                    {
                        listView3.SelectedItems[0].Text = comboBox8.Text;
                        listView3.SelectedItems[0].SubItems[1].Text = comboBox9.Text + " " + comboBox4.Text ;
                        listView3.SelectedItems[0].SubItems[2].Text = textBox27.Text.Replace("\t", " ");
                    }
                }
                else
                {
                    listView3.SelectedItems[0].Text = comboBox8.Text;
                    listView3.SelectedItems[0].SubItems[1].Text = comboBox9.Text;
                    listView3.SelectedItems[0].SubItems[2].Text = textBox27.Text.Replace("\t", " ");

                }




            }

            catch
            {

            }

        }

        private void listView3_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                try
                {
                    if (listView3.Items[listView3.HitTest(e.X, e.Y).Item.Index].SubItems[1].Text.Substring(0, 14) == "join usergroup")
                    {
                        comboBox9.Text = "join usergroup";
                        comboBox4.Text = listView3.Items[listView3.HitTest(e.X, e.Y).Item.Index].SubItems[1].Text.Substring(15, listView3.Items[listView3.HitTest(e.X, e.Y).Item.Index].SubItems[1].Text.Length - 15);
                    }

                    else
                    {
                        comboBox9.Text = listView3.Items[listView3.HitTest(e.X, e.Y).Item.Index].SubItems[1].Text;
                    }
                }
                catch
                {

                    comboBox9.Text = listView3.Items[listView3.HitTest(e.X, e.Y).Item.Index].SubItems[1].Text;

                }

                textBox27.Text = listView3.Items[listView3.HitTest(e.X, e.Y).Item.Index].SubItems[2].Text;
                comboBox8.Text = listView3.Items[listView3.HitTest(e.X, e.Y).Item.Index].Text;
            }
            catch
            {

            }

        }

        private void button9_Click(object sender, EventArgs e)
        {
            sendrcon(rcon.Text, "say '" + replacemsg(textBox4.Text) + "'" + "\r" + "say '" + replacemsg(textBox5.Text) + "'" + "\r" + "say '" + replacemsg(textBox6.Text) + "'" + "\r" + "say '" + replacemsg(textBox7.Text) + "'", serverip.Text, Convert.ToInt32(serverport.Text), "","");

        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                sendrcon(rcon.Text, "say '" + textBox1.Text.Replace("$killername1", players.Items[Convert.ToInt32(info.Items[18].SubItems[2].Text)].SubItems[9].Text).Replace("$kills1", players.Items[Convert.ToInt32(info.Items[18].SubItems[2].Text)].SubItems[7].Text).Replace("$killerkd1", players.Items[Convert.ToInt32(info.Items[18].SubItems[2].Text)].SubItems[10].Text + "'") + "\r" + "say '" + textBox2.Text.Replace("$killername2", players.Items[Convert.ToInt32(info.Items[19].SubItems[2].Text)].SubItems[9].Text).Replace("$kills2", players.Items[Convert.ToInt32(info.Items[19].SubItems[2].Text)].SubItems[7].Text).Replace("$killerkd2", players.Items[Convert.ToInt32(info.Items[19].SubItems[2].Text)].SubItems[10].Text + "'") + "\r" + "say '" + textBox9.Text.Replace("$killername3", players.Items[Convert.ToInt32(info.Items[20].SubItems[2].Text)].SubItems[9].Text).Replace("$kills3", players.Items[Convert.ToInt32(info.Items[20].SubItems[2].Text)].SubItems[7].Text).Replace("$killerkd3", players.Items[Convert.ToInt32(info.Items[20].SubItems[2].Text)].SubItems[10].Text + "'"), serverip.Text, Convert.ToInt32(serverport.Text), "","");
                sendrcon(rcon.Text, "say '" + textBox18.Text.Replace("$kdrname1", players.Items[Convert.ToInt32(info.Items[15].SubItems[2].Text)].SubItems[9].Text).Replace("$kd1", players.Items[Convert.ToInt32(info.Items[15].SubItems[2].Text)].SubItems[10].Text) + "'" + "\r" + "say '" + textBox17.Text.Replace("$kdrname2", players.Items[Convert.ToInt32(info.Items[16].SubItems[2].Text)].SubItems[9].Text).Replace("$kd2", players.Items[Convert.ToInt32(info.Items[16].SubItems[2].Text)].SubItems[10].Text) + "'" + "\r" + "say '" + textBox16.Text.Replace("$kdrname3", players.Items[Convert.ToInt32(info.Items[17].SubItems[2].Text)].SubItems[9].Text).Replace("$kd3", players.Items[Convert.ToInt32(info.Items[17].SubItems[2].Text)].SubItems[10].Text) + "'", serverip.Text, Convert.ToInt32(serverport.Text), "","");
                sendrcon(rcon.Text, "say '" + textBox3.Text.Replace("$lemming", info.Items[9].SubItems[1].Text).Replace("$deaths", info.Items[9].SubItems[2].Text) + "'", serverip.Text, Convert.ToInt32(serverport.Text), "","");
            }

            catch
            {

            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            sendrcon(rcon.Text, "say '" + textBox23.Text.Replace("$gdfkills", info.Items[11].SubItems[1].Text).Replace("$gdfdeaths", info.Items[11].SubItems[2].Text) + "'" + "\r" + "say '" + textBox22.Text.Replace("$stroggkills", info.Items[12].SubItems[1].Text).Replace("$stroggdeaths", info.Items[12].SubItems[2].Text) + "'", serverip.Text, Convert.ToInt32(serverport.Text), "","");

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tabControl1.SelectedIndex)
            {

                case 0:
                    try
                    {
                        pictureBox5.Image = null;
                    }
                    catch
                    {

                    }
                    break;
                case 3:
                    pictureBox5.Image = pictureBox1.Image;
                    updatepicture2(1);
                    break;

                case 2:
                    log.ScrollToCaret();
                    break;
                case 6:
                    ftplog.ScrollToCaret();
                    break;



            }
        }

        private void textBox31_TextChanged(object sender, EventArgs e)
        {
            updatepicture2(1);
        }

        private void textBox30_TextChanged(object sender, EventArgs e)
        {
            updatepicture2(1);

        }

        private void textBox29_TextChanged(object sender, EventArgs e)
        {
            updatepicture2(1);

        }

        private void textBox28_TextChanged(object sender, EventArgs e)
        {
            updatepicture2(1);

        }

        private void button58_Click(object sender, EventArgs e)
        {
            try
            {
                fontDialog1.Font = textBox31.Font;

                if (fontDialog1.ShowDialog() == DialogResult.OK)
                {


                    textBox31.Font = fontDialog1.Font;
                    updatepicture2(1);

                }

            }

            catch
            {

            }
        }

        private void button56_Click(object sender, EventArgs e)
        {
            try
            {
                fontDialog1.Font = textBox30.Font;

                if (fontDialog1.ShowDialog() == DialogResult.OK)
                {


                    textBox30.Font = fontDialog1.Font;
                    updatepicture2(1);

                }

            }

            catch
            {

            }
        }

        private void button54_Click(object sender, EventArgs e)
        {
            try
            {
                fontDialog1.Font = textBox29.Font;

                if (fontDialog1.ShowDialog() == DialogResult.OK)
                {


                    textBox29.Font = fontDialog1.Font;
                    updatepicture2(1);

                }

            }

            catch
            {

            }
        }

        private void button53_Click(object sender, EventArgs e)
        {
            try
            {
                fontDialog1.Font = textBox28.Font;

                if (fontDialog1.ShowDialog() == DialogResult.OK)
                {


                    textBox28.Font = fontDialog1.Font;
                    updatepicture2(1);

                }

            }

            catch
            {

            }
        }

        private void button57_Click(object sender, EventArgs e)
        {
            try
            {
                colorDialog1.Color = textBox31.ForeColor;

                if (colorDialog1.ShowDialog() == DialogResult.OK)
                {


                    textBox31.ForeColor = colorDialog1.Color;
                    updatepicture2(1);

                }

            }

            catch
            {

            }
        }

        private void button55_Click(object sender, EventArgs e)
        {
            try
            {
                colorDialog1.Color = textBox30.ForeColor;

                if (colorDialog1.ShowDialog() == DialogResult.OK)
                {


                    textBox30.ForeColor = colorDialog1.Color;
                    updatepicture2(1);

                }

            }

            catch
            {

            }

        }

        private void button52_Click(object sender, EventArgs e)
        {
            try
            {
                colorDialog1.Color = textBox29.ForeColor;

                if (colorDialog1.ShowDialog() == DialogResult.OK)
                {


                    textBox29.ForeColor = colorDialog1.Color;
                    updatepicture2(1);

                }

            }

            catch
            {

            }

        }

        private void button51_Click(object sender, EventArgs e)
        {
            try
            {
                colorDialog1.Color = textBox28.ForeColor;

                if (colorDialog1.ShowDialog() == DialogResult.OK)
                {


                    textBox28.ForeColor = colorDialog1.Color;
                    updatepicture2(1);

                }

            }

            catch
            {

            }

        }

        private void button49_Click(object sender, EventArgs e)
        {
            try
            {
                colorDialog1.Color = button49.BackColor;

                if (colorDialog1.ShowDialog() == DialogResult.OK)
                {


                    button49.BackColor = colorDialog1.Color;
                    updatepicture2(1);

                }

            }

            catch
            {

            }
        }

        private void button60_Click(object sender, EventArgs e)
        {
            try
            {
                colorDialog1.Color = button60.BackColor;

                if (colorDialog1.ShowDialog() == DialogResult.OK)
                {


                    button60.BackColor = colorDialog1.Color;
                    updatepicture2(1);

                }

            }

            catch
            {

            }
        }

        private void button61_Click(object sender, EventArgs e)
        {
            try
            {
                colorDialog1.Color = button61.BackColor;

                if (colorDialog1.ShowDialog() == DialogResult.OK)
                {


                    button61.BackColor = colorDialog1.Color;
                    updatepicture2(1);

                }

            }

            catch
            {

            }
        }

        private void blend_ValueChanged(object sender, EventArgs e)
        {
            updatepicture2(1);

        }

        private void speed_ValueChanged(object sender, EventArgs e)
        {
            updatepicture2(1);

        }

        private void button59_Click(object sender, EventArgs e)
        {
            button60.BackColor = Color.White;
            button61.BackColor = Color.DarkOliveGreen;
            button49.BackColor = Color.White;
            button46.BackColor = Color.DarkOliveGreen;
            button47.BackColor = Color.Black;
            button48.BackColor = Color.YellowGreen;
            button45.BackColor = Color.DarkOliveGreen;

            textBox31.ForeColor = Color.FromKnownColor(KnownColor.WindowText);
            textBox30.ForeColor = Color.FromKnownColor(KnownColor.WindowText);
            textBox29.ForeColor = Color.FromKnownColor(KnownColor.WindowText);
            textBox28.ForeColor = Color.FromKnownColor(KnownColor.WindowText);

            updatepicture2(1);
        }

        private void button50_Click(object sender, EventArgs e)
        {
            textBox31.Text = "$servername";
            textBox30.Text = "$serverstatus - $servertime - $serverplayers";
            textBox29.Text = "$servermap - $serverround | $serverscore";
            textBox28.Text = "User: $user - $utctime UTC";
            updatepicture2(1);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                showall();
                winapi.UnregisterHotKey(this.Handle, 1729);
                winapi.UnregisterHotKey(this.Handle, 1730);
                winapi.UnregisterHotKey(this.Handle, 1731);

                Resolution.CResolution ChangeRes800 = new Resolution.CResolution(moniwi, monihe);
            }
            catch
            {
            }

            try
            {

                if (checkBox12.Checked == true)
                {
                    sendrcon(rcon.Text, "si_disablevoting 0", serverip.Text, Convert.ToInt32(serverport.Text), "", "");
                }
            }
            catch
            {
            }


            try
            {
                if (button3.Text == "Logout") { createemptylogin(); }
                savepersonalsettings();
                savesettings();
                savepb(); 
                ftpConnection1.Close(true);

                if (checkBox1.Checked == false)
                {
                   // File.Delete(Application.StartupPath + "\\stats\\global.txt");
                   // File.Delete(Application.StartupPath + "\\stats\\sessions.txt");
                }
            }
            catch
            {
                try
                {
                }
                catch
                {
                    try
                    {
                        if (checkBox1.Checked == false)
                        {
                         //   File.Delete(Application.StartupPath + "\\stats\\global.txt");
                         //   File.Delete(Application.StartupPath + "\\stats\\sessions.txt");
                        }
                    }
                    catch
                    {

                    }
                }
            }


            Application.DoEvents();


        }

        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (ListViewItem h in players.SelectedItems)
                {
                    sendrcon(rcon.Text, "admin warn '" + h.SubItems[2].Text + "'", serverip.Text, Convert.ToInt32(serverport.Text), "","");
                }
            }
            catch
            {
            }
        }

        private void toolStripMenuItem10_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (ListViewItem h in players.SelectedItems)
                {
                    sendrcon(rcon.Text, "admin kick '" + h.SubItems[2].Text + "'", serverip.Text, Convert.ToInt32(serverport.Text), "","");
                    kickedplayers = kickedplayers + h.SubItems[2].Text + " (Kicked Admin Decision)" + "\r\n";

                }
            }
            catch
            {
            }

        }

        private void toolStripMenuItem11_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (ListViewItem h in players.SelectedItems)
                {
                    sendrcon(rcon.Text, "admin ban '" + h.SubItems[2].Text + "'", serverip.Text, Convert.ToInt32(serverport.Text), "","");
                    kickedplayers = kickedplayers + h.SubItems[2].Text + " (Banned Admin Decision)" + "\r\n";

                }
            }
            catch
            {
            }

        }

        private void toolStripMenuItem13_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (ListViewItem h in players.SelectedItems)
                {
                    sendrcon(rcon.Text, "admin setTeam '" + h.SubItems[2].Text + "' gdf", serverip.Text, Convert.ToInt32(serverport.Text), "","");
                }
            }
            catch
            {
            }

        }

        private void toolStripMenuItem14_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (ListViewItem h in players.SelectedItems)
                {
                    sendrcon(rcon.Text, "admin setTeam '" + h.SubItems[2].Text + "' strogg", serverip.Text, Convert.ToInt32(serverport.Text), "","");
                }
            }
            catch
            {
            }

        }

        private void toolStripMenuItem16_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (ListViewItem h in players.SelectedItems)
                {
                    sendrcon(rcon.Text, "admin setTeam '" + h.SubItems[2].Text + "' spec", serverip.Text, Convert.ToInt32(serverport.Text), "","");
                }
            }
            catch
            {
            }

        }

        private void toolStripMenuItem17_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (ListViewItem h in players.SelectedItems)
                {
                    sendrcon(rcon.Text, "admin changeusergroup '" + h.SubItems[2].Text + "' default", serverip.Text, Convert.ToInt32(serverport.Text), "","");
                }
            }
            catch
            {
            }

        }

        private void toolStripMenuItem18_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (ListViewItem h in players.SelectedItems)
                {
                    sendrcon(rcon.Text, "admin changeusergroup '" + h.SubItems[2].Text + "' protected", serverip.Text, Convert.ToInt32(serverport.Text), "","");
                }
            }
            catch
            {
            }

        }

        private void toolStripMenuItem19_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (ListViewItem h in players.SelectedItems)
                {
                    sendrcon(rcon.Text, "admin changeusergroup '" + h.SubItems[2].Text + "' trusted", serverip.Text, Convert.ToInt32(serverport.Text), "","");
                }
            }
            catch
            {
            }

        }

        private void toolStripMenuItem20_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (ListViewItem h in players.SelectedItems)
                {
                    sendrcon(rcon.Text, "admin changeusergroup '" + h.SubItems[2].Text + "' admin", serverip.Text, Convert.ToInt32(serverport.Text), "","");
                }
            }
            catch
            {
            }

        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            spectslv.Items.Clear();
            try
            {
                foreach (ListViewItem h in players.SelectedItems)
                {

                    if (h.SubItems[3].Text != "")
                    {
                        if (protectedlv.FindItem(h.SubItems[2].Text.Replace(h.SubItems[3].Text, "")) == -1)
                        {                       
                            protectedlv.Items.Add(new ListViewItem(new string[] { h.SubItems[2].Text.Replace(h.SubItems[3].Text, "") }, 17));
                            h.ImageIndex = 17;
                        }

                    }


                    else
                    {
                        if (protectedlv.FindItem(h.SubItems[2].Text) == -1)
                        {
                            protectedlv.Items.Add(new ListViewItem(new string[] { h.SubItems[2].Text}, 17));
                            h.ImageIndex = 17;
                        }

                    }

                
                }
            }

            catch
            {

            }
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            spectslv.Items.Clear();
            int i = -1;
            try
            {
                foreach (ListViewItem bu in players.SelectedItems)
                {
                    i = -1;
                    if (bu.SubItems[3].Text != "")
                    {
                        i = protectedlv.FindItem(bu.SubItems[2].Text.Replace(bu.SubItems[3].Text,""));

                    }
                    else
                    {
                        i = protectedlv.FindItem(bu.SubItems[2].Text);

                    }


                    if (i != -1)
                    {

                        protectedlv.Items[i].Remove();
                        bu.ImageIndex = 15;
                    }


                }
            }


            catch
            {

            }
        }

        private void toolStripMenuItem21_Click(object sender, EventArgs e)
        {

            spectslv.Items.Clear();
            try
            {
                foreach (ListViewItem bu in protectedlv.SelectedItems)
                {

                    protectedlv.Items.Remove(bu);


                }
            }
            catch
            {
            }
        }
        public static string Encrypt(string toEncrypt)
        {

            byte[] keyArray;

            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);



            string key = ")(*&";



            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();

            keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));

            //Always release the resources and flush data of the Cryptographic service provide. Best Practice



            hashmd5.Clear();





            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();

            //set the secret key for the tripleDES algorithm

            tdes.Key = keyArray;

            //mode of operation. there are other 4 modes. We choose ECB(Electronic code Book)

            tdes.Mode = CipherMode.ECB;

            //padding mode(if any extra byte added)



            tdes.Padding = PaddingMode.PKCS7;



            ICryptoTransform cTransform = tdes.CreateEncryptor();

            //transform the specified region of bytes array to resultArray

            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            //Release resources held by TripleDes Encryptor

            tdes.Clear();

            //Return the encrypted data into unreadable string format

            return Convert.ToBase64String(resultArray, 0, resultArray.Length);

        }
       
        private void button6_Click(object sender, EventArgs e)
        {
            savesettings();
            savepersonalsettings();
            savepb(); 
        }

        private void button5_Click(object sender, EventArgs e)
        {

            if (checkftpfilemain(ftps.Text) == false)
            {
                if (MessageBox.Show("File " + ftps.Text + " does not exist in FTP, create it?", "NTB", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    button8_Click(null, null);
                    return;

                }
                else
                {
                    return;

                }
            }


            
                downloadtomainfolder(new string[] { ftps.Text});

        
            loadsettings();
          //  loadpb();
            status.Text = DateTime.Now.ToLocalTime() + " Settings Download Success";

        }

        private void button7_Click(object sender, EventArgs e)
        {
            contextMenuStrip3.Show(MousePosition.X, MousePosition.Y);//,button7.Location.X + button7.Width,button7.Location.Y + button7.Height      );
        }



        private void checkBox4_CheckStateChanged(object sender, EventArgs e)
        {
            splitContainer2.Panel2Collapsed = !(checkBox4.Checked);

        }

        private void checkBox31_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox31.Checked == true)
            {
                AutoStarter.SetAutoStart();
            }
            else
            {
                AutoStarter.UnSetAutoStart();
            }

        }

        private void button46_Click(object sender, EventArgs e)
        {

            colorDialog1.Color = button46.BackColor;
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                button46.BackColor = colorDialog1.Color;
            }

        }

        private void button47_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = button47.BackColor;
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                button47.BackColor = colorDialog1.Color;
            }
        }

        private void button48_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = button48.BackColor;
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                button48.BackColor = colorDialog1.Color;
            }
        }

        private void button45_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = button45.BackColor;
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                button45.BackColor = colorDialog1.Color;
            }
        }

        private void listView3_MouseDown_1(object sender, MouseEventArgs e)
        {
            this.Text = listView3.Items[listView3.HitTest(e.X, e.Y).Item.Index].SubItems[1].Text.Substring(0,14);

            try
            {
                comboBox8.Text = listView3.Items[listView3.HitTest(e.X, e.Y).Item.Index].Text;

                if (listView3.Items[listView3.HitTest(e.X, e.Y).Item.Index].SubItems[1].Text.Substring(0, 13) == "join usergroup")
                {
                    comboBox9.Text = "join usergroup";
                    comboBox4.Text = listView3.Items[listView3.HitTest(e.X, e.Y).Item.Index].SubItems[1].Text.Substring(14, listView3.Items[listView3.HitTest(e.X, e.Y).Item.Index].SubItems[1].Text.Length - 14);
                }

                else
                {


                    comboBox9.Text = listView3.Items[listView3.HitTest(e.X, e.Y).Item.Index].SubItems[1].Text;
                }
                    textBox27.Text = listView3.Items[listView3.HitTest(e.X, e.Y).Item.Index].SubItems[2].Text;
            }

            catch
            {

            }


        }

        private void ftpConnection1_ReplyReceived(object sender, EnterpriseDT.Net.Ftp.FTPMessageEventArgs e)
        {
            if (checkBox13.Checked) 
            { 
                ftplog.AppendText(e.Message + "\r\n");


           
            }

        }
        private void uploadtomainfolder(string[] whatfiles)
        {
            Cursor = Cursors.WaitCursor;

            foreach (string q in whatfiles)
            {
                try
                {
                    if (!ftpConnection1.IsConnected) { ftpConnection1.Connect(); }

                    if (ftpConnection1.ServerDirectory != ftpfolder.Text) { ftpConnection1.ChangeWorkingDirectory(ftpfolder.Text); }





                    ftpConnection1.UploadFile(q, q.Substring(q.LastIndexOf("\\") + 1, (q.Length - q.LastIndexOf("\\") - 1)), false);//  .DownloadFile(Application.StartupPath + "\\stats\\", q);
                    if (checkBox35.Checked) { log.AppendText(DateTime.Now.ToLocalTime() + "\t" + "Uploading: " + q.Substring(q.LastIndexOf("\\") + 1, (q.Length - q.LastIndexOf("\\") - 1)) + "\r\n"); }


                }
                catch
                {

                    try
                    {

                        if (checkBox35.Checked) { log.AppendText(DateTime.Now.ToLocalTime() + "\t" + "Reconnecting..." + "\r\n"); };
                        if (!ftpConnection1.IsConnected) { ftpConnection1.Connect(); }
                        if (ftpConnection1.ServerDirectory != ftpfolder.Text) { ftpConnection1.ChangeWorkingDirectory(ftpfolder.Text); }
                        ftpConnection1.UploadFile(q, q.Substring(q.LastIndexOf("\\") + 1, (q.Length - q.LastIndexOf("\\") - 1)), false);//  .DownloadFile(Application.StartupPath + "\\stats\\", q);
                        if (checkBox35.Checked) { log.AppendText(DateTime.Now.ToLocalTime() + "\t" + "Uploading: " + q.Substring(q.LastIndexOf("\\") + 1, (q.Length - q.LastIndexOf("\\") - 1)) + "\r\n"); }
                    }

                    catch
                    {
                        if (checkBox35.Checked) { log.AppendText(DateTime.Now.ToLocalTime() + "\t" + "Reconnecting Failed" + "\r\n"); }
                        try
                        {
                            ftpConnection1.Close(true);
                        }
                        catch
                        {
                            if (checkBox35.Checked) { log.AppendText(DateTime.Now.ToLocalTime() + "\t" + "Error in FTP" + "\r\n"); }

                        }
                    }



                }







            }


            Cursor = Cursors.Default;


        }
        private void button8_Click(object sender, EventArgs e)
        {
            savesettings();
            savepersonalsettings();
            savepb();
            try
            {
                Application.DoEvents();


                uploadtomainfolder(new string[] { Application.StartupPath + "\\" + ftps.Text });

                status.Text = DateTime.Now.ToLocalTime() + " Settings Upload Success";

            }

            catch
            {


            }
        }


        private void createemptylogin()
        {

            FileStream fst = new FileStream(Application.StartupPath + "\\" + ftpl.Text.Replace("\\", ""), FileMode.Create, FileAccess.Write);
            StreamWriter m_streamWriter = new StreamWriter(fst);
            m_streamWriter.Flush();
            m_streamWriter.BaseStream.Seek(0, SeekOrigin.Begin);
            m_streamWriter.Write("0");
            m_streamWriter.WriteLine();
            m_streamWriter.Flush();
            m_streamWriter.Close();
            Application.DoEvents();
            uploadtomainfolder(new string[] { Application.StartupPath + "\\" + ftpl.Text });

        }

        private void backuppblist()
        {
            
            uploadtomainfolder(new string[] { Application.StartupPath + "\\pblist.txt"});

        }
        private void createemptymsg()
        {

            FileStream fst = new FileStream(Application.StartupPath + "\\" + "interop.msg", FileMode.Create, FileAccess.Write);
            StreamWriter m_streamWriter = new StreamWriter(fst);
            m_streamWriter.Flush();
            m_streamWriter.BaseStream.Seek(0, SeekOrigin.Begin);
            m_streamWriter.Write(messages.Text);
            m_streamWriter.WriteLine();
            m_streamWriter.Flush();
            m_streamWriter.Close();
            Application.DoEvents();
            uploadtomainfolder(new string[] { Application.StartupPath + "\\" + "interop.msg" });

        }
        private void dologin()
        {

            FileStream fst14 = new FileStream(Application.StartupPath + "\\" + ftpl.Text.Replace("\\", ""), FileMode.Create, FileAccess.Write);
            StreamWriter m_streamWriter2 = new StreamWriter(fst14);
            m_streamWriter2.Flush();
            m_streamWriter2.BaseStream.Seek(0, SeekOrigin.Begin);
            m_streamWriter2.Write("User: " + textBox15.Text + " already logged at: " + DateTime.Parse(DateTime.UtcNow.ToUniversalTime().ToString()).ToString("MM/dd/yy hh:mm:ss tt") + " | Shutdown ClanTag: " + textBox39.Text );
            m_streamWriter2.WriteLine();
            m_streamWriter2.Flush();
            m_streamWriter2.Close();
            uploadtomainfolder(new string[] { Application.StartupPath + "\\" + ftpl.Text });

        }

        void checkntbstatus()
        {
           
                System.Diagnostics.Process[] myProcesses;
                myProcesses = System.Diagnostics.Process.GetProcessesByName("NTB");

                savelog();
                if (checkBox1.Checked == false)
                {

                    createemptylogin();
                }
                if ((button17.Enabled == true) & (button17.Text == "Stop"))
                {
                        button17_Click(null, null);
                    }

                if (checkBox40.CheckState == CheckState.Checked)
                {
                    if (checkBox1.Checked == false)
                    {

                        uploadsignature();
                    }
                }
                if (checkBox1.Checked == false)
                {

                    button17.Enabled = false;
                }
                button17.Text = "Start";

                button3.Text = "Login";
                foreach (System.Diagnostics.Process instance in myProcesses)
                {


                    instance.CloseMainWindow();

                    instance.Kill();

                    instance.Close();
                }


            


        }


        private void button3_Click(object sender, EventArgs e)
        {
            if (button3.Text == "Logout")
            {
                createemptylogin();


                if ((button17.Enabled == true) & (button17.Text == "Stop"))
                {
                    button17_Click(null, null);
                }
                if (checkBox40.CheckState == CheckState.Checked)
                {
                    uploadsignature();
                }
                button17.Enabled = false;
                button17.Text = "Start";
                button3.Text = "Login";


                return;

            }

            if (ftpp.Text == "")
            {
                MessageBox.Show ("No FTP password set");
                return;

            }




            if (checkftpfilemain(ftpl.Text) == false)
            {
                if (MessageBox.Show("File " + ftpl.Text + " does not exist in FTP, create it?", "NTB", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    createemptylogin();
                    button17.Enabled = true;
                    button3.Text = "Logout";
                    timer3.Enabled = false;
                    return;

                }
                else
                {
                    button3.Text = "Login";

                    return;
                }
            }

            else
            {
                downloadtomainfolder(new string[] { ftpl.Text });

                if (readlogin() == true)
                {
                    dologin();
                    button17.Enabled = true;
                    button3.Text = "Logout";
                    timer3.Enabled = false;
                    if (checkBox34.Checked == true) { button17_Click(null, null); }
                    return;
                }

                else
                {
                    button3.Text = "Login";
                    timer3.Enabled = false;
                    return;
                }

            }






        }

        private bool readlogin()
        {
            string strLine = "Error";
            try
            {
                FileStream fs13 = new FileStream(Application.StartupPath + "\\" + ftpl.Text.Replace("\\", ""), FileMode.Open, FileAccess.Read);
                StreamReader m_streamReader1 = new StreamReader(fs13);
                m_streamReader1.BaseStream.Seek(0, SeekOrigin.Begin);
                strLine = m_streamReader1.ReadLine();
                m_streamReader1.Close();
                fs13.Dispose();
                m_streamReader1.Dispose();
            }
            catch
            {

            }
            if ((strLine == null) || (strLine == "") || (strLine == "0"))
            {
                return true;
            }
            else
            {
                timer3.Enabled = false;
                toolTip1.ToolTipIcon = ToolTipIcon.Error;

                toolTip1.ToolTipTitle = "Login ERROR";
                toolTip1.Show(strLine, this.players, players.Left, players.Top - 20, 6000);
                return false;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("This option will let you login, but it WILL NOT STOP other users NTB process, continue?", "NTB", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                createemptylogin();
                if ((button17.Enabled == true) & (button17.Text == "Stop"))
                {
                    button17_Click(null, null);
                }

                if (checkBox40.CheckState == CheckState.Checked)
                {
                    uploadsignature();
                }


                button17.Enabled = false;
                button17.Text = "Start";

                button3.Text = "Login";
            }

            else
            {
                return;

            }


        }

        private void checkBox37_CheckedChanged(object sender, EventArgs e)
        {
            checkBox34.Enabled = checkBox37.Checked;

        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            if (ConnectionExists() == false)
            {
                return;
            }
            Form1_Load(null,null);// button3_Click(null, null);

        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            if (checkBox32.Checked == true)
            {
                TrayBtn_clicked(null, null); // this.WindowState = FormWindowState.Minimized;// TrayBtn_clicked(null, null);
            }

        }
        private void leeglobal()
        {


            
            














            log.AppendText(DateTime.Now.ToLocalTime() + "\t" + "Updating Global Database" + "\r\n");

            Application.DoEvents();
            char[] r = { '\t' };
            string[] arr;

            FileStream fs = new FileStream(Application.StartupPath + "\\stats\\global.txt", FileMode.Open, FileAccess.Read);
            StreamReader m_streamReader = new StreamReader(fs);

            try
            {

                // Write to the file using StreamWriter class 
                m_streamReader.BaseStream.Seek(0, SeekOrigin.Begin);
                string strLine = m_streamReader.ReadLine();
                List<ListViewItem> ariel = new List<ListViewItem>();
                ListViewItem ju;

                while (strLine != null)
                {

                    arr = strLine.Split(r);


                    ju = new ListViewItem(new string[] { arr[0], arr[1], arr[2], arr[3], arr[4], arr[5], arr[6], arr[7], arr[8], arr[9], arr[10],arr[11] });

                                    ariel.Add(ju);



                    strLine = m_streamReader.ReadLine();




                }

                            globallv.Items.AddRange(ariel.ToArray());
                ariel.Clear();
               
                m_streamReader.Close();
                fs.Dispose();
                m_streamReader.Dispose();


            }



            catch (Exception error)
            {
                m_streamReader.Close();
                fs.Dispose();
                m_streamReader.Dispose();

                errorlv.Items.Add(new ListViewItem(new string[] { DateTime.Now.ToLocalTime().ToString(), error.TargetSite.Name.ToString(), error.Message + "\r\n" + error.StackTrace }));

            }

        }
        private void button43_Click(object sender, EventArgs e)
        {
            try
            {
                status.Text = DateTime.Now.ToLocalTime().ToString() + " " + "Downloading stats database...";
                Application.DoEvents();

                if (checkBox1.Checked == false)
                {
                    try
                    {
                        File.Copy(Application.StartupPath + "\\stats\\global.txt", Application.StartupPath + "\\stats\\global.temp", true);
                        File.Copy(Application.StartupPath + "\\stats\\sessions.txt", Application.StartupPath + "\\stats\\sessions.temp", true);
                    }

                    catch
                    {

                    }
                    downloadtostatsfolder(new string[] { "global.txt", "sessions.txt" });


                    try
                    {


                        FileInfo f1 = new FileInfo(Application.StartupPath + "\\stats\\global.txt");
                        FileInfo f2 = new FileInfo(Application.StartupPath + "\\stats\\global.temp");
                        FileInfo f1a = new FileInfo(Application.StartupPath + "\\stats\\sessions.txt");
                        FileInfo f2a = new FileInfo(Application.StartupPath + "\\stats\\sessions.temp");

                        if (f1.Length >= f2.Length)
                        {
                            status.Text = DateTime.Now.ToLocalTime() + " Database Downloaded!";

                        }
                        else
                        {
                            try
                            {
                                File.Copy(Application.StartupPath + "\\stats\\global.temp", Application.StartupPath + "\\stats\\global.txt", true);
                                status.Text = DateTime.Now.ToLocalTime() + " Restored!";
                            }
                            catch
                            {
                            }
                        }
                        if (f1a.Length >= f2a.Length)
                        {

                        }
                        else
                        {
                            try
                            {
                                File.Copy(Application.StartupPath + "\\stats\\sessions.temp", Application.StartupPath + "\\stats\\sessions.txt", true);
                            }
                            catch
                            {

                            }
                        }
                    }
                    catch
                    {
                    }

                }

                globallv.Items.Clear();
                if (checkBox6.Checked == true)
                {
                    try
                    {
                        File.Copy(Application.StartupPath + "\\stats\\global.txt", Application.StartupPath + "\\stats\\backup\\" + "global_" + DateTime.Parse(DateTime.UtcNow.ToUniversalTime().ToString()).ToString("yyyyMMdd_hhmmss") + ".txt", true);
                    }
                    catch
                    {

                    }
                }
                leeglobal();
                label37.Text = globallv.Items.Count.ToString();
            }
            catch
            {

            }
        }

        private void statsyes_Click(object sender, EventArgs e)
        {
            if (statsyes.Checked == true)
            {
                if ((button17.Enabled == true) & (button17.Text == "Stop"))
                {

                    button43_Click(null, null);
                }
            }
        }

        private void statsyes_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void ftpConnection1_BytesTransferred(object sender, EnterpriseDT.Net.Ftp.BytesTransferredEventArgs e)
        {
            
        }

        private void checkBox50_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void ddm3_CheckedChanged(object sender, EventArgs e)
        {
            groupBox10.Enabled = ddm3.Checked; 

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
     
            AboutBox1  form2 = new AboutBox1();

            form2.Show();
        }

        private void button20_Click(object sender, EventArgs e)
        {
            downloadtomainfolder(new string[] { "interop.msg" });


            try
            {

                FileStream fs13 = new FileStream(Application.StartupPath + "\\" + "interop.msg", FileMode.Open, FileAccess.Read);
                StreamReader m_streamReader1 = new StreamReader(fs13);
                m_streamReader1.BaseStream.Seek(0, SeekOrigin.Begin);
                string strLine = m_streamReader1.ReadLine();
                messages.Clear();
                while (strLine != null)
                {
                    messages.AppendText (strLine + "\r\n");
                    strLine = m_streamReader1.ReadLine();
                }
                m_streamReader1.Close();
                fs13.Dispose();
                m_streamReader1.Dispose();

            }
            catch
            {

            }
            button19.Enabled = true; 
            
        }

        private void button19_Click(object sender, EventArgs e)
        {
            createemptymsg();

        }
       

        private void button44_Click(object sender, EventArgs e)
        {

            contextMenuStrip5.Show(MousePosition.X, MousePosition.Y);
        }

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            timer1.Interval = Convert.ToInt32(toolStripComboBox1.Text) * 1000;

        }

        private void ftpp_TextChanged(object sender, EventArgs e)
        {

            try
            {
                ftpConnection1.Close(true);
            }

            catch
            {
            }

            try
            {
                ftpConnection1.Password = ftpp.Text;

            }
            catch
            {

            }

        }

        private void ftpu_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ftpConnection1.Close(true);
            }

            catch
            {
            }

            try
            {
                ftpConnection1.UserName = ftpu.Text;

            }
            catch
            {

            }
        }

        private void ftph_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ftpConnection1.Close(true);
            }

            catch
            {
            }

            try
            {
                ftpConnection1.ServerAddress = ftph.Text;

            }
            catch
            {

            }
        }

        private void comboBox12_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ftpConnection1.Close(true);
            }

            catch
            {
            }

            try
            {
                if (comboBox12.Text == "Pasive")
                {
                    ftpConnection1.ConnectMode = EnterpriseDT.Net.Ftp.FTPConnectMode.PASV;

                }
                else
                {
                    ftpConnection1.ConnectMode = EnterpriseDT.Net.Ftp.FTPConnectMode.ACTIVE;
                }
            }
            catch
            {
      
            }
        }
        public static string Decrypt(string cipherString)
        {

            cipherString = cipherString.Replace(" ", "+");

            byte[] keyArray;

            //get the byte code of the string



            byte[] toEncryptArray = Convert.FromBase64String(cipherString);



            string key = ")(*&";



            //if hashing was used get the hash code with regards to your key

            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();

            keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));

            //release any resource held by the MD5CryptoServiceProvider



            hashmd5.Clear();





            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();

            //set the secret key for the tripleDES algorithm

            tdes.Key = keyArray;

            //mode of operation. there are other 4 modes. We choose ECB(Electronic code Book)



            tdes.Mode = CipherMode.ECB;

            //padding mode(if any extra byte added)

            tdes.Padding = PaddingMode.PKCS7;



            ICryptoTransform cTransform = tdes.CreateDecryptor();

            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            //Release resources held by TripleDes Encryptor                

            tdes.Clear();

            //return the Clear decrypted TEXT

            return UTF8Encoding.UTF8.GetString(resultArray);

        }
        private void button1_Click(object sender, EventArgs e)
        {
            leenow();
            globaltohtml();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

            if (checkBox1.Checked == true)
            {
                button17.Enabled = true;
                button3.Enabled = false;
                button4.Enabled = false;
                button5.Enabled = false;
                button8.Enabled = false;
                button19.Enabled = false;
                button20.Enabled = false;

            }
            else
            {
                if (button17.Text == "Start")
                {
                    button17.Enabled = false ;
                }
                button3.Enabled = true;
                button4.Enabled = true ;
                button5.Enabled = true;
                button8.Enabled = true;
                button19.Enabled = false;
                button20.Enabled = false;

            }

        }

       

        private void toolStripMenuItem23_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void toolStripMenuItem22_Click(object sender, EventArgs e)
        {
            this.Show();
            this.notifyIcon1.Visible = false;
            this.WindowState = FormWindowState.Normal;

        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            this.notifyIcon1.Visible = false;
            this.WindowState = FormWindowState.Normal;

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
        listView3.Items.Clear();
        }

        private void listView3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button21_Click(object sender, EventArgs e)
        {
Beep(660,100);
System.Threading.Thread.Sleep(70);

Beep(660,100);
System.Threading.Thread.Sleep(150);

Beep(660,100);
System.Threading.Thread.Sleep(150);

Beep(510,100);
System.Threading.Thread.Sleep(50);

Beep(660,100);
System.Threading.Thread.Sleep(150);

Beep(770,100);
System.Threading.Thread.Sleep(225);

Beep(380,100);
System.Threading.Thread.Sleep(225);

        }

        private void button15_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(label26.Text + "\t" + l1.Text + "\r\n" + label25.Text + "\t" + l2.Text + "\r\n" + label24.Text + "\t" + l3.Text + "\r\n" + label21.Text + "\t" + l4.Text + "\r\n" + label19.Text + "\t" + l5.Text + "\r\n" + label17.Text + "\t" + l6.Text + "\r\n" + label15.Text + "\t" + l7.Text + "\r\n" + label1.Text + "\t" + l8.Text); 
        }

        private void button16_Click(object sender, EventArgs e)
        {
            l1.Text = "0";
            l2.Text = "0";
            l3.Text = "0";
            l4.Text = "0";
            l5.Text = "0";
            l6.Text = "0";
            l7.Text = "0";
            l8.Text = "0";

        }

        private void toolStripMenuItem24_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                System.Diagnostics.Process.Start("notepad.exe", Application.StartupPath + "\\now.dat");
                Cursor = Cursors.Default;

            }

            catch
            {
                Cursor = Cursors.Default;

            }

        }

        private void toolStripMenuItem25_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                System.Diagnostics.Process.Start("notepad.exe", Application.StartupPath + "\\global.dat");
                Cursor = Cursors.Default;

            }

            catch
            {
                Cursor = Cursors.Default;

            }

        }

        private void toolStripMenuItem26_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                System.Diagnostics.Process.Start("notepad.exe", Application.StartupPath + "\\sessions.dat");
                Cursor = Cursors.Default;

            }

            catch
            {
                Cursor = Cursors.Default;

            }

        }

        private void toolStripMenuItem27_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                System.Diagnostics.Process.Start("notepad.exe", Application.StartupPath + "\\sessionstable.dat");
                Cursor = Cursors.Default;

            }

            catch
            {
                Cursor = Cursors.Default;

            }

        }

        private void toolStripMenuItem28_Click(object sender, EventArgs e)
        {
            try
            {

                tabControl1.TabPages.Remove(tabPage8);
            }
            catch
            {

            }
            
            tabControl1.TabPages.Add(tabPage8);
            tabControl1.SelectedIndex = 9;
        }

        private void button21_Click_1(object sender, EventArgs e)
        {
            try
            {
                openFileDialog1.InitialDirectory = Application.StartupPath + "\\stats\\backup";

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {

                    File.Copy(openFileDialog1.FileName, Application.StartupPath + "\\stats\\" + "global.txt", true);

                    status.Text = DateTime.Now.ToLocalTime().ToString() + " " + "Restoring stats database...";
                    Application.DoEvents();

                   
                    globallv.Items.Clear();
                    leeglobal();

                    if (globallv.Items.Count > 1)
                    {
                        MessageBox.Show("Database restored, please consider that starting NTB may download old database, in that case, restore it while NTB is scanning the server", "NTB", MessageBoxButtons.OK, MessageBoxIcon.Information); 
                    }


                }

            }

            catch
            {

            }
        }

        private void button22_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog1.InitialDirectory = Application.StartupPath + "\\logs";

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {

                    try
                    {
                        Cursor = Cursors.WaitCursor;

                        System.Diagnostics.Process.Start("notepad.exe", openFileDialog1.FileName);
                        Cursor = Cursors.Default;

                    }

                    catch
                    {
                        Cursor = Cursors.Default;

                    }

                }

            }

            catch
            {

            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            savelog();
        }

        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (comboBox7.Text == "PunkBuster")
                {
                    numericUpDown3.Enabled = true;
                    label13.Enabled = true;

                    toolTip1.ToolTipIcon = ToolTipIcon.Info;
                    toolTip1.ToolTipTitle = "Hint";
                    toolTip1.Show("Using Punkbuster method will also kick players under a specific usergroup, prevent this by adding them on the protected list", this.comboBox7, 6000);


                }
                else
                {
                    numericUpDown3.Enabled = false;
                    label13.Enabled = false;

                }
            }
            catch
            {

            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            try
            {

                if (kickedplayers == "")
                {
                    MessageBox.Show("None", "kicked Players", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    MessageBox.Show(kickedplayers, "kicked Players", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch
            {

            }
            }

        private void button23_Click(object sender, EventArgs e)
        {
        
            if (button23.Text == "Update")
            {
                button23.Text = "Updating";
                sendrcon2(rcon.Text, "pb_sv_banlist", serverip.Text, Convert.ToInt32(serverport.Text), "","");
                button23.Text = "Update";

                
            }
            else
            {
                button23.Text = "Update";

            }
        }

        private void unBanToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void textBox13_TextChanged(object sender, EventArgs e)
        {
            if (textBox13.Text == "")
            {
                return;
            }
            
            
            try
            {
                foreach (ListViewItem item in banlist.Items)
                {

                    try
                    {
                        if (comboBox10.Text == "GUID")
                        {
                            if (item.SubItems[1].Text.Substring(0, textBox13.Text.Length).ToLower() == textBox13.Text.ToLower())
                            {
                                item.Selected = true;
                                item.EnsureVisible();
                                return;
                            }


                        }
                        else if (comboBox10.Text == "Name")
                        {
                            if (item.SubItems[2].Text.Substring(0, textBox13.Text.Length).ToLower() == textBox13.Text.ToLower())
                            {
                                item.Selected = true;
                                item.EnsureVisible();
                                return;
                            }
                        }
                        else if (comboBox10.Text == "Address")
                        {
                            if (item.SubItems[3].Text.Substring(0, textBox13.Text.Length) == textBox13.Text)
                            {
                                item.Selected = true;
                                item.EnsureVisible();
                                return;
                            }
                        }
                    }
                    catch
                    {
                    }
                }
            }

            catch
            {

            }
            }

        private void comboBox5_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyValue)
            {
                case 13:
                    button18_Click(sender, e);

                    break;
            }

          
        }

        private void getAliasToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

      

        private void button25_Click(object sender, EventArgs e)
        {
            sendrcon2(rcon.Text, "pb_sv_plist", serverip.Text, Convert.ToInt32(serverport.Text), "", "");

        }

       

        private void button27_Click(object sender, EventArgs e)
        {
            savepb();
        }

        private void button28_Click(object sender, EventArgs e)
        {
            loadpb();
        }

        private void banlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {


                aliases.Items.Clear();

                textBox14.Text = banlist.SelectedItems[0].SubItems[1].Text;
                textBox19.Text = banlist.SelectedItems[0].SubItems[2].Text;
                textBox20.Text = banlist.SelectedItems[0].SubItems[3].Text;
                textBox21.Text = banlist.SelectedItems[0].SubItems[4].Text;
                if (ctu3.Checked)
                {
                    sendrcon2(rcon.Text, "pb_sv_alist " + banlist.SelectedItems[0].SubItems[1].Text, serverip.Text, Convert.ToInt32(serverport.Text), "", "");
                }
            }
            catch
            {
            }
        }

        private void button32_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("UnBan " + banlist.SelectedItems[0].SubItems[2].Text + "?", "NTB", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    sendrcon2(rcon.Text, "pb_sv_unban " + banlist.SelectedItems[0].Text, serverip.Text, Convert.ToInt32(serverport.Text), "", "");

                    return;

                }
                else
                {
                    return;

                }
            }
            catch
            {
            }
        }

        private void button24_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Ban GUID " + textBox14.Text + "?", "NTB", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                if (textBox19.Text == "")
                {
                    textBox19.Text = "???";
                }

                if (textBox20.Text == "")
                {
                    textBox20.Text = "???";
                }

                if (textBox21.Text == "")
                {
                    textBox21.Text = "unknown";
                }

                if (textBox20.Text == " ")
                {
                    textBox20.Text = "???";
                }

                if (textBox21.Text == " ")
                {
                    textBox21.Text = "unknown";
                }

                sendrcon2(rcon.Text, "pb_sv_banguid " + textBox14.Text + " " + textBox20.Text + " " + textBox19.Text + " " + textBox21.Text, serverip.Text, Convert.ToInt32(serverport.Text), "", "");

                return;

            }
            else
            {
                return;

            }
        }

        private void button33_Click(object sender, EventArgs e)
        {
            try
            {

                if (MessageBox.Show("ReBan " + banlist.SelectedItems[0].SubItems[2].Text + "?", "NTB", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    sendrcon2(rcon.Text, "pb_sv_reban " + banlist.SelectedItems[0].Text, serverip.Text, Convert.ToInt32(serverport.Text), "", "");

                    return;

                }
                else
                {
                    return;

                }
            }
            catch
            {
            }
        }

        private void button34_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Ban Name " + textBox19.Text + "?", "NTB", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {




                sendrcon2(rcon.Text, "pb_sv_ban " + textBox19.Text, serverip.Text, Convert.ToInt32(serverport.Text), "", "");

                return;

            }
            else
            {
                return;

            }
        }

        private void button35_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Ban IP " + textBox20.Text + "?", "NTB", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {




                sendrcon2(rcon.Text, "pb_sv_banmask " + textBox20.Text, serverip.Text, Convert.ToInt32(serverport.Text), "", "");

                return;

            }
            else
            {
                return;

            }
        }

        private void pbh_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Application.DoEvents();
                pbh.EndUpdate();
                pbh.ResumeLayout();


                aliases3.Items.Clear();

                if (ctu1.Checked)
                {
                    sendrcon2(rcon.Text, "pb_sv_alist " + pbh.SelectedItems[0].Text, serverip.Text, Convert.ToInt32(serverport.Text), "", "");
                }
                else
                {
                    string[] bb = pbh.SelectedItems[0].SubItems[10].Text.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string q in bb)
                    {

                        aliases3.Items.Add(new ListViewItem(new string[] { "?","?","?",q.Replace(@"""","") }));

                    }


                }

            }
            catch
            {
            }
        }

        private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void pbnow_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {


                aliases2.Items.Clear();
                textBox32.Text = pbnow.SelectedItems[0].SubItems[1].Text;
                textBox26.Text = pbnow.SelectedItems[0].SubItems[8].Text;
                textBox25.Text = pbnow.SelectedItems[0].SubItems[2].Text;
           //     textBox21.Text = banlist.SelectedItems[0].SubItems[4].Text;

                if (ctu2.Checked)
                {
                    sendrcon2(rcon.Text, "pb_sv_alist " + pbnow.SelectedItems[0].SubItems[1].Text, serverip.Text, Convert.ToInt32(serverport.Text), "", "");
                }
            }
            catch
            {
            }



        }

        private void textBox24_TextChanged(object sender, EventArgs e)
        {
            if (textBox24.Text == "")
            {
                return;
            }

            try
            {
                foreach (ListViewItem item in pbh.Items)
                {

                    try
                    {
                        if (comboBox11.Text == "GUID")
                        {
                            if (item.Text.Substring(0, textBox24.Text.Length).ToLower() == textBox24.Text.ToLower())
                            {
                                item.Selected = true;
                                item.EnsureVisible();
                                return;
                            }
                    

                        }
                        else if (comboBox11.Text == "Name")
                        {
                            if (item.SubItems[1].Text.Substring(0, textBox24.Text.Length).ToLower() == textBox24.Text.ToLower())
                            {
                                item.Selected = true;
                                item.EnsureVisible();
                                return;
                            }
                        }
                        else if (comboBox11.Text == "Address")
                        {
                            if (item.SubItems[2].Text.Substring(0, textBox24.Text.Length) == textBox24.Text)
                            {
                                item.Selected = true;
                                item.EnsureVisible();
                                return;
                            }
                        }
                        else if (comboBox11.Text == "Alias")
                        {
                            if (item.SubItems[10].Text.ToLower().Contains(textBox24.Text.ToLower()))                           {
                                item.Selected = true;
                                item.EnsureVisible();
                         
                                return;
                            }
                        }
                 
                    }
                    catch
                    {
                     
 
                    }
                }
            }

            catch
            {

            }
        }

        private void button37_Click(object sender, EventArgs e)
        {

            if (textBox32.Text == "")
            {
                return;
            }

            try
            {
                if (MessageBox.Show("Ban GUID " + textBox32.Text + "?", "NTB", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    if (textBox26.Text == "")
                    {
                        textBox26.Text = "???";
                    }

                    if (textBox25.Text == "")
                    {
                        textBox25.Text = "???";
                    }

                    if (textBox25.Text == " ")
                    {
                        textBox25.Text = "???";
                    }
                    string value = "unknown";
                    if (Form1.InputBox("NTB", "Reason:", ref value) == DialogResult.OK)
                    {
                        sendrcon2(rcon.Text, "pb_sv_banguid " + textBox32.Text + " " + textBox26.Text + " " + textBox25.Text +" \"" + value + "\"", serverip.Text, Convert.ToInt32(serverport.Text), "", "");
                    }


                    return;

                }
                else
                {
                    return;

                }
            }
            catch
            {

            }
        }

        private void button27_Click_1(object sender, EventArgs e)
        {

            try
            {
                if (MessageBox.Show("Ban Name " + textBox26.Text + "?", "NTB", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {




                    sendrcon2(rcon.Text, "pb_sv_ban " + textBox26.Text, serverip.Text, Convert.ToInt32(serverport.Text), "", "");

                    return;

                }
                else
                {
                    return;

                }
            }
            catch
            {

            }
        }

        private void button26_Click(object sender, EventArgs e)
        {
            try
            {
                if (button26.Text == "Cancel")
                {
                    goto pablo;

                }

                button26.Text = "Cancel";

                Cursor = Cursors.WaitCursor;
                progressBar3.Value = 0;
                progressBar3.Maximum = pbh.Items.Count;

                foreach (ListViewItem otto in pbh.Items)
                {
                    progressBar3.Value = otto.Index;
                    Application.DoEvents();

                    sendrcon2(rcon.Text, "pb_sv_alist " + otto.Text, serverip.Text, Convert.ToInt32(serverport.Text), "", "");
                    if (button26.Text == "Search new Aliases")
                    {
                        Cursor = Cursors.Default;
                        progressBar3.Value = 0;

                        break;
                    }
                    Application.DoEvents();

                }

            pablo:
                button26.Text = "Search new Aliases";
                Cursor = Cursors.Default;
                progressBar3.Value = 0;

            }
            catch
            {
                button26.Text = "Search new Aliases";
                Cursor = Cursors.Default;
                progressBar3.Value = 0;


            }
        }

        private void button28_Click_1(object sender, EventArgs e)
        {
            
        }

        private void panel28_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button36_Click(object sender, EventArgs e)
        {
            pbreport();
        }



        private void sameip()
        {
            Cursor = Cursors.WaitCursor;
            progressBar3.Value = 0;
            try
            {
                progressBar3.Maximum = pbh.Items.Count;
 

 
                Hashtable norma = new Hashtable();


                foreach (ListViewItem pe in pbh.Items)
                {



                    progressBar3.Value = pe.Index;

                    try
                    {
                        norma.Add(pe.SubItems[2].Text.Substring(0, pe.SubItems[2].Text.LastIndexOf(":")), pe.Index);
                    }
                    catch
                    {

                        pbh.Items[Convert.ToInt32(norma[pe.SubItems[2].Text.Substring(0, pe.SubItems[2].Text.LastIndexOf(":"))])].BackColor = Color.Salmon;
                        pe.BackColor = Color.Salmon;

                    }

                }

                        lvwColumnSorter.SortColumn = 2;
                lvwColumnSorter.Order = SortOrder.Ascending;
            this.pbh.Sort();

            string jh = "";
            Clipboard.Clear();
            foreach (ListViewItem pe2 in pbh.Items)
            {
                if (pe2.BackColor == Color.Salmon)
                {
                    if (jh != pe2.SubItems[2].Text.Substring(0, pe2.SubItems[2].Text.LastIndexOf(":")))
                    {
                        if (jh == "")
                        {
                            Clipboard.SetText(Clipboard.GetText() + "[" + pe2.SubItems[2].Text.Substring(0, pe2.SubItems[2].Text.LastIndexOf(":"))+ "]");
                        }
                        else
                        {
                            Clipboard.SetText(Clipboard.GetText() + "\r\n" + "\r\n" +"["+ pe2.SubItems[2].Text.Substring(0, pe2.SubItems[2].Text.LastIndexOf(":"))+ "]");

                        }

                        jh = pe2.SubItems[2].Text.Substring(0, pe2.SubItems[2].Text.LastIndexOf(":"));


                    }

                }


                if (pe2.BackColor == Color.Salmon)
                {
                    if (pe2.SubItems[10].Text == "")
                    {
                        Clipboard.SetText(Clipboard.GetText() + "\r\n" + pe2.Text + "\t" + pe2.SubItems[1].Text);

                    }
                    else
                    {
                        Clipboard.SetText(Clipboard.GetText() + "\r\n" + pe2.Text + "\t" + pe2.SubItems[10].Text);
                    }
                }

            }

               
                Cursor = Cursors.Default;

                progressBar3.Value = 0;

                MessageBox.Show(this, "IP Smurfs copied to clipboard", "NTB", MessageBoxButtons.OK, MessageBoxIcon.Information); 

            }
            catch
            {
                progressBar3.Value = 0;

                Cursor = Cursors.Default;
            }
        }

        private void pbreport()
        {
            Cursor = Cursors.WaitCursor;
            progressBar3.Value = 0;
            try
            {
                progressBar3.Maximum = pbh.Items.Count;
                FileStream fs22 = new FileStream(Application.StartupPath + "\\" + "report.txt", FileMode.Create, FileAccess.Write);
                StreamWriter m_streamWriter = new StreamWriter(fs22);
                m_streamWriter.Flush();
                m_streamWriter.BaseStream.Seek(0, SeekOrigin.Begin);

              
                    LookupService ls = new LookupService(Application.StartupPath + "\\GeoLiteCity.dat", LookupService.GEOIP_STANDARD);
                

                foreach (ListViewItem pe in pbh.Items)
                {
                    progressBar3.Value = pe.Index;
                    m_streamWriter.Write("GUID:" + "\t" + "\t" + pe.Text);
                    m_streamWriter.WriteLine();
                    m_streamWriter.Write("Last Account:" + "\t" + pe.SubItems[1].Text);
                    m_streamWriter.WriteLine();
                    m_streamWriter.Write("IP Address:" + "\t" + pe.SubItems[2].Text);
                    m_streamWriter.WriteLine();
                    m_streamWriter.Write("Location:" + "\t" + pe.SubItems[8].Text + " (" + pe.SubItems[9].Text + ")" );
                    m_streamWriter.WriteLine();
                    m_streamWriter.Write("O/S:" + "\t" + "\t" + pe.SubItems[7].Text);
                    m_streamWriter.WriteLine();
                    string[] guid = pe.SubItems[10].Text.Split(new string[] { "\"" }, StringSplitOptions.RemoveEmptyEntries);

                    try
                    {

                        Location l = ls.getLocation(pe.SubItems[2].Text.Substring(0, pe.SubItems[2].Text.IndexOf(":")));
                        m_streamWriter.Write("City:" + "\t" + "\t" + l.city);
                        m_streamWriter.WriteLine();
                        m_streamWriter.Write("Region:" + "\t" + "\t" + l.regionName);
                        m_streamWriter.WriteLine();
                    }
                    catch
                    {
                    }

                    if (guid.Length == 0)
                    {
                        m_streamWriter.Write("Aliases:" + "\t" + pe.SubItems[1].Text);
                        m_streamWriter.WriteLine();

                        goto pepe;
                    }
                        
                        m_streamWriter.Write("Aliases:" + "\t" + guid[0]);
                        m_streamWriter.WriteLine();
                    
                                    for (int i = 1; i < guid.Length -1; i++)
                {
                    if (guid[i] != " ")
                    {
                        m_streamWriter.Write("\t" + "\t" + guid[i]);
                        m_streamWriter.WriteLine();
                    }
                                    }
                pepe:

                                    m_streamWriter.Write("");
                                    m_streamWriter.WriteLine();
                                    m_streamWriter.Write("");
                                    m_streamWriter.WriteLine();


                }


                m_streamWriter.Flush();
                m_streamWriter.Close();
                fs22.Dispose();
                m_streamWriter.Dispose();
                Cursor = Cursors.Default;

                progressBar3.Value = 0;


            }
            catch
            {
                progressBar3.Value = 0;

                Cursor = Cursors.Default;
            }
        }

        private void button40_Click(object sender, EventArgs e)
        {
            try
            {
                sendrcon2(rcon.Text, "pb_sv_getss " + pbnow.SelectedItems[0].Text, serverip.Text, Convert.ToInt32(serverport.Text), "", "");
            }

            catch
            {

            }
        }

        private void button38_Click(object sender, EventArgs e)
        {
            try
            {
                sendrcon2(rcon.Text, "pb_sv_say " + pbnow.SelectedItems[0].Text + " " + textBox34.Text  , serverip.Text, Convert.ToInt32(serverport.Text), "", "");
            }

            catch
            {

            }

        }
        public static DialogResult InputBox(string title, string promptText, ref string value)
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.Text = title;
            label.Text = promptText;
            textBox.Text = value;

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(9, 20, 372, 13);
            textBox.SetBounds(12, 36, 372, 20);
            buttonOk.SetBounds(228, 72, 75, 23);
            buttonCancel.SetBounds(309, 72, 75, 23);

            label.AutoSize = true;
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            value = textBox.Text;
            return dialogResult;
        }
        private void button39_Click(object sender, EventArgs e)
        {

            try
            {
                if (MessageBox.Show("Ban GUID " + pbh.SelectedItems[0].Text + " ?", "NTB", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    string value = "unknown";
                    if (Form1.InputBox("NTB", "Reason:", ref value) == DialogResult.OK)
                    {




                        sendrcon2(rcon.Text, "pb_sv_banguid " + pbh.SelectedItems[0].Text + " " + pbh.SelectedItems[0].SubItems[1].Text + " " + "???" + " \"" + value + "\"" , serverip.Text, Convert.ToInt32(serverport.Text), "", "");
                    }
                    return;

                }
                else
                {
                    return;

                }
            }
            catch
            {

            }
        }

        private void button27_Click_2(object sender, EventArgs e)
        {
            try
            {
                FileStream fs22 = new FileStream(Application.StartupPath + "\\" + "pblist.txt", FileMode.Create, FileAccess.Write);
                StreamWriter m_streamWriter = new StreamWriter(fs22);
                m_streamWriter.Flush();
                m_streamWriter.BaseStream.Seek(0, SeekOrigin.Begin);

                foreach (ListViewItem pe in pbh.Items)
                {
                    m_streamWriter.Write(pe.Text + "\t" + pe.SubItems[1].Text + "\t" + pe.SubItems[2].Text + "\t" + pe.SubItems[3].Text + "\t" + pe.SubItems[4].Text + "\t" + pe.SubItems[5].Text + "\t" + pe.SubItems[6].Text + "\t" + pe.SubItems[7].Text + "\t" + pe.SubItems[8].Text + "\t" + pe.SubItems[9].Text + "\t" + pe.SubItems[10].Text + "\t" + pe.Checked.ToString());
                    m_streamWriter.WriteLine();


                }


                m_streamWriter.Flush();
                m_streamWriter.Close();
                fs22.Dispose();
                m_streamWriter.Dispose();
                Cursor = Cursors.Default;


                if (!checkBox1.Checked)
                {

                    backuppblist();
                }
                else
                {
                    MessageBox.Show("FTP is disabled", "NTB", MessageBoxButtons.OK);

                }



    




            }
            catch
            {
                Cursor = Cursors.Default;
            }
        }

        private void button41_Click(object sender, EventArgs e)
        {
          


            Cursor = Cursors.WaitCursor;


            if (checkBox1.Checked)
            {
                MessageBox.Show("FTP is disabled", "NTB", MessageBoxButtons.OK);
                Cursor = Cursors.Default;
                return;
      
            }


            //    savepb2();

    


            try
            {
                //   File.Copy(Application.StartupPath + "\\pblist.txt", Application.StartupPath + "\\pblist.temp", true);
                downloadtomainfolder(new string[] { "pblist.txt" });

              
                loadpb();

                Cursor = Cursors.Default;


                

            }
            catch
            {
                try
                {
                    downloadtomainfolder(new string[] { "pblist.txt" });


                    if (File.Exists(Application.StartupPath + "\\pblist.txt"))
                    {
                        loadpb();

                        Cursor = Cursors.Default;

                    }
                }
                catch
                {
                    Cursor = Cursors.Default;


                }
            }
        }




        void cargopbl()
        {
            if (checkBox1.Checked)
            {
                return;
            }


            savepb2();


            try
            {
                File.Copy(Application.StartupPath + "\\pblist.txt", Application.StartupPath + "\\pblist.temp", true);
                downloadtomainfolder(new string[] { "pblist.txt" });


                FileInfo f1 = new FileInfo(Application.StartupPath + "\\pblist.txt");
                FileInfo f2 = new FileInfo(Application.StartupPath + "\\pblist.temp");

                if (f1.Length >= f2.Length)
                {
                    loadpb();
        

                }

                else
                {
                    File.Copy(Application.StartupPath + "\\pblist.temp", Application.StartupPath + "\\pblist.txt", true);

                }


            }

            catch
            {
                try
                {
                    downloadtomainfolder(new string[] { "pblist.txt" });

                    Application.DoEvents();
                    if (File.Exists(Application.StartupPath + "\\pblist.txt"))
                    {
                        loadpb();
                    }
                }
                catch
                {

                }
            }
        }

        private void pbh_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == lvwColumnSorter.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (lvwColumnSorter.Order == SortOrder.Ascending)
                {
                    lvwColumnSorter.Order = SortOrder.Descending;
                }
                else
                {
                    lvwColumnSorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                lvwColumnSorter.SortColumn = e.Column;
                lvwColumnSorter.Order = SortOrder.Ascending;
            }

            // Perform the sort with these new sort options.
            this.pbh.Sort();

        }

        private void pbnow_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == lvwColumnSorter2.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (lvwColumnSorter2.Order == SortOrder.Ascending)
                {
                    lvwColumnSorter2.Order = SortOrder.Descending;
                }
                else
                {
                    lvwColumnSorter2.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                lvwColumnSorter2.SortColumn = e.Column;
                lvwColumnSorter2.Order = SortOrder.Ascending;
            }

            // Perform the sort with these new sort options.
            this.pbnow.Sort();
        }

        private void banlist_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == lvwColumnSorter3.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (lvwColumnSorter3.Order == SortOrder.Ascending)
                {
                    lvwColumnSorter3.Order = SortOrder.Descending;
                }
                else
                {
                    lvwColumnSorter3.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                lvwColumnSorter3.SortColumn = e.Column;
                lvwColumnSorter3.Order = SortOrder.Ascending;
            }

            // Perform the sort with these new sort options.
            this.banlist.Sort();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ftplog.Clear();
        }

        private void toolStripMenuItem29_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetText(pbh.SelectedItems[0].Text) ;
            }

            catch
            {

            }
        }

        private void toolStripMenuItem30_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetText(pbh.SelectedItems[0].SubItems[1].Text);
            }

            catch
            {

            }

        }

        private void toolStripMenuItem31_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetText(pbnow.SelectedItems[0].SubItems[1].Text);
            }

            catch
            {

            }
        }

        private void toolStripMenuItem32_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetText(pbnow.SelectedItems[0].SubItems[8].Text);
            }

            catch
            {

            }
        }

        private void button42_Click(object sender, EventArgs e)
        {

            Clipboard.Clear();
            try
            {
                foreach (ListViewItem item in banlist.Items)
                {
                    Clipboard.SetText(Clipboard.GetText() + item.Text + "\t" + item.SubItems[1].Text + "\t" + item.SubItems[2].Text + "\t" + item.SubItems[3].Text + "\t" + item.SubItems[4].Text + "\r\n" );
                   
                }
            }

            catch
            {

            }
        }

        private void pbh_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
   contextMenuStrip6.Show(pbh,e.X,e.Y  );
            }

        }

        private void pbnow_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip7.Show(pbnow, e.X, e.Y);
            }

        }

        private void toolStripMenuItem33_Click(object sender, EventArgs e)
        {
            button39_Click(null, null); 
        }

        private void toolStripMenuItem34_Click(object sender, EventArgs e)
        {
            button37_Click(null, null); 

        }

        private void toolStripMenuItem35_Click(object sender, EventArgs e)
        {
            button40_Click(null, null); 

        }

        private void button62_Click(object sender, EventArgs e)
        {
                        Cursor = Cursors.WaitCursor;
            progressBar3.Value = 0;
            progressBar3.Maximum = pbh.Items.Count;
            FileStream fs22 = new FileStream(Application.StartupPath + "\\" + "misc.txt", FileMode.Create, FileAccess.Write);
            StreamWriter m_streamWriter = new StreamWriter(fs22);
            m_streamWriter.Flush();
            m_streamWriter.BaseStream.Seek(0, SeekOrigin.Begin);

           try
           {
                Hashtable roco = new Hashtable();
                Hashtable oss = new Hashtable();

                progressBar3.Maximum = pbh.Items.Count;
 

                m_streamWriter.Write("Total Unique Players:" + "\t" + pbh.Items.Count.ToString());
                m_streamWriter.WriteLine();
                m_streamWriter.Write("");
                m_streamWriter.WriteLine();
                m_streamWriter.Write("Countries:");
                m_streamWriter.WriteLine();
                m_streamWriter.Write("----------");
                m_streamWriter.WriteLine();

                m_streamWriter.Write("");
                m_streamWriter.WriteLine();
                
                foreach (ListViewItem pe in pbh.Items)
                {
                    progressBar3.Value = pe.Index;

                    if (roco.ContainsKey(pe.SubItems[8].Text))
                    {
                        roco[pe.SubItems[8].Text] = Convert.ToInt32(roco[pe.SubItems[8].Text]) + 1;
                    }
                    else
                    {
                        roco.Add(pe.SubItems[8].Text, 1);

                    }

                    if (oss.ContainsKey(pe.SubItems[7].Text))
                    {
                        oss[pe.SubItems[7].Text] = Convert.ToInt32(oss[pe.SubItems[7].Text]) + 1;
                    }
                    else
                    {
                        oss.Add(pe.SubItems[7].Text, 1);

                    }

                }

             String[]  keys = new String[roco.Count];
                roco.Keys.CopyTo(keys, 0);

                Int32[] Values = new Int32[roco.Count];
                roco.Values.CopyTo(Values, 0);

                System.Array.Sort(Values, keys);



                String[] keys2 = new String[oss.Count];
                oss.Keys.CopyTo(keys2, 0);

                Int32[] Values2 = new Int32[oss.Count];
                oss.Values.CopyTo(Values2, 0);

                System.Array.Sort(Values2, keys2);


                for (int i = Values.Length-1; i >= 0; i--)
             //   for (int i = 0; i < Values.Length ; ++i)
                {
                    m_streamWriter.Write("{0}: {1}", keys[i], Values[i] );
                    m_streamWriter.WriteLine();

                }


                m_streamWriter.Write("");
                m_streamWriter.WriteLine();

                m_streamWriter.Write("O/S:");
                m_streamWriter.WriteLine();
                m_streamWriter.Write("----");
                m_streamWriter.WriteLine(); 
                m_streamWriter.Write("");
                m_streamWriter.WriteLine();

                for (int i = Values2.Length - 1; i >= 0; i--)
                //   for (int i = 0; i < Values.Length ; ++i)
                {
                    m_streamWriter.Write("{0}: {1}", keys2[i].Replace("(W)", "(Windows)").Replace("(L)", "(Linux)").Replace("(M)", "(Macintosh)").Replace("(?)", "(Unknown)"), Values2[i]);
                    m_streamWriter.WriteLine();

                }


                m_streamWriter.Flush();
                m_streamWriter.Close();
                fs22.Dispose();
                m_streamWriter.Dispose();
                Cursor = Cursors.Default;
                progressBar3.Value = 0;
        }
            catch
            {
                      m_streamWriter.Flush();
                m_streamWriter.Close();
                fs22.Dispose();
                m_streamWriter.Dispose();
                Cursor = Cursors.Default;
                progressBar3.Value = 0;
            }


           try
           {
               

                   try
                   {
                       Cursor = Cursors.WaitCursor;

                       System.Diagnostics.Process.Start("notepad.exe", Application.StartupPath + "\\misc.txt" );
                       Cursor = Cursors.Default;

                   }

                   catch
                   {
                       Cursor = Cursors.Default;

                   }

               

           }

           catch
           {

           }


         
        }
        private void showall()
        {
            int count = this.components.Components.Count;
            for (int i = 1; i < count; i++)
            {
                int index = this.components.Components.Count;
                if (this.components.Components[index - 1] is NotifyIcon)
                {
                    NotifyIcon temp = this.components.Components[index - 1] as NotifyIcon;
                    if (temp.Tag != null)
                    {
                        tray_Click(temp, null);
                    }
                }
            }
        }
        private void button63_Click(object sender, EventArgs e)
        {
           

        }

        private void tabPage13_Click(object sender, EventArgs e)
        {

        }

        private void checkBox22_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (checkBox22.Checked)
                {
                    getnewhotkey();
                }
                else
                {
                    showall();
                    winapi.UnregisterHotKey(this.Handle, 1729);
                    winapi.UnregisterHotKey(this.Handle, 1730);
                    winapi.UnregisterHotKey(this.Handle, 1731);
                    winapi.UnregisterHotKey(this.Handle, 1732);

                    Resolution.CResolution ChangeRes800 = new Resolution.CResolution(moniwi, monihe);
             

                }
            }
            catch
            {

            }
        }

        private void checkBox23_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox22.CheckState == CheckState.Checked)
            {
                showall();
                winapi.UnregisterHotKey(this.Handle, 1729);
                winapi.UnregisterHotKey(this.Handle, 1730);
                winapi.UnregisterHotKey(this.Handle, 1731);

                Resolution.CResolution ChangeRes800 = new Resolution.CResolution(moniwi, monihe);
                getnewhotkey();
            }
            }

        private void checkBox24_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox22.CheckState == CheckState.Checked)
            {
                showall();
                winapi.UnregisterHotKey(this.Handle, 1729);
                winapi.UnregisterHotKey(this.Handle, 1730);
                winapi.UnregisterHotKey(this.Handle, 1731);

                Resolution.CResolution ChangeRes800 = new Resolution.CResolution(moniwi, monihe);
                getnewhotkey();
            }

        }

        private void checkBox29_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox22.CheckState == CheckState.Checked)
            {
                showall();
                winapi.UnregisterHotKey(this.Handle, 1729);
                winapi.UnregisterHotKey(this.Handle, 1730);
                winapi.UnregisterHotKey(this.Handle, 1731);

                Resolution.CResolution ChangeRes800 = new Resolution.CResolution(moniwi, monihe);
                getnewhotkey();
            }

        }

        private void checkBox30_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox22.CheckState == CheckState.Checked)
            {
                showall();
                winapi.UnregisterHotKey(this.Handle, 1729);
                winapi.UnregisterHotKey(this.Handle, 1730);
                winapi.UnregisterHotKey(this.Handle, 1731);

                Resolution.CResolution ChangeRes800 = new Resolution.CResolution(moniwi, monihe);
                getnewhotkey();
            }

        }

        private void checkBox36_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox22.CheckState == CheckState.Checked)
            {
                showall();
                winapi.UnregisterHotKey(this.Handle, 1729);
                winapi.UnregisterHotKey(this.Handle, 1730);
                winapi.UnregisterHotKey(this.Handle, 1731);

                Resolution.CResolution ChangeRes800 = new Resolution.CResolution(moniwi, monihe);
                getnewhotkey();
            }

        }

        private void groupBox15_Enter(object sender, EventArgs e)
        {

        }

        private void checkBox43_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox22.CheckState == CheckState.Checked)
            {
                showall();
                winapi.UnregisterHotKey(this.Handle, 1729);
                winapi.UnregisterHotKey(this.Handle, 1730);
                winapi.UnregisterHotKey(this.Handle, 1731);

                Resolution.CResolution ChangeRes800 = new Resolution.CResolution(moniwi, monihe);
                getnewhotkey();
            }

        }

        private void comboBox14_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (checkBox22.CheckState == CheckState.Checked)
            {
                showall();
                winapi.UnregisterHotKey(this.Handle, 1729);
                winapi.UnregisterHotKey(this.Handle, 1730);
                winapi.UnregisterHotKey(this.Handle, 1731);

                Resolution.CResolution ChangeRes800 = new Resolution.CResolution(moniwi, monihe);
                getnewhotkey();
            }

        }

        private void checkBox26_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox22.CheckState == CheckState.Checked)
            {
                showall();
                winapi.UnregisterHotKey(this.Handle, 1729);
                winapi.UnregisterHotKey(this.Handle, 1730);
                winapi.UnregisterHotKey(this.Handle, 1731);

                Resolution.CResolution ChangeRes800 = new Resolution.CResolution(moniwi, monihe);
                getnewhotkey();
            }

        }

        private void checkBox27_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox22.CheckState == CheckState.Checked)
            {
                showall();
                winapi.UnregisterHotKey(this.Handle, 1729);
                winapi.UnregisterHotKey(this.Handle, 1730);
                winapi.UnregisterHotKey(this.Handle, 1731);

                Resolution.CResolution ChangeRes800 = new Resolution.CResolution(moniwi, monihe);
                getnewhotkey();
            }

        }

        private void checkBox28_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox22.CheckState == CheckState.Checked)
            {
                showall();
                winapi.UnregisterHotKey(this.Handle, 1729);
                winapi.UnregisterHotKey(this.Handle, 1730);
                winapi.UnregisterHotKey(this.Handle, 1731);

                Resolution.CResolution ChangeRes800 = new Resolution.CResolution(moniwi, monihe);
                getnewhotkey();
            }

        }

        private void comboBox13_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (checkBox22.CheckState == CheckState.Checked)
            {
                showall();
                winapi.UnregisterHotKey(this.Handle, 1729);
                winapi.UnregisterHotKey(this.Handle, 1730);
                winapi.UnregisterHotKey(this.Handle, 1731);

                Resolution.CResolution ChangeRes800 = new Resolution.CResolution(moniwi, monihe);
                getnewhotkey();
            }

        }

        private void hideshiftcheck_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox22.CheckState == CheckState.Checked)
            {
                showall();
                winapi.UnregisterHotKey(this.Handle, 1729);
                winapi.UnregisterHotKey(this.Handle, 1730);
                winapi.UnregisterHotKey(this.Handle, 1731);

                Resolution.CResolution ChangeRes800 = new Resolution.CResolution(moniwi, monihe);
                getnewhotkey();
            }

        }

        private void hidealtcheck_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox22.CheckState == CheckState.Checked)
            {
                showall();
                winapi.UnregisterHotKey(this.Handle, 1729);
                winapi.UnregisterHotKey(this.Handle, 1730);
                winapi.UnregisterHotKey(this.Handle, 1731);

                Resolution.CResolution ChangeRes800 = new Resolution.CResolution(moniwi, monihe);
                getnewhotkey();
            }

        }

        private void hidectrlcheck_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox22.CheckState == CheckState.Checked)
            {
                showall();
                winapi.UnregisterHotKey(this.Handle, 1729);
                winapi.UnregisterHotKey(this.Handle, 1730);
                winapi.UnregisterHotKey(this.Handle, 1731);

                Resolution.CResolution ChangeRes800 = new Resolution.CResolution(moniwi, monihe);
                getnewhotkey();
            }

        }

        private void hidekeycombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (checkBox22.CheckState == CheckState.Checked)
            {
                showall();
                winapi.UnregisterHotKey(this.Handle, 1729);
                winapi.UnregisterHotKey(this.Handle, 1730);
                winapi.UnregisterHotKey(this.Handle, 1731);

                Resolution.CResolution ChangeRes800 = new Resolution.CResolution(moniwi, monihe);
                getnewhotkey();
            }

        }

        private void checkBox44_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox22.CheckState == CheckState.Checked)
            {
                showall();
                winapi.UnregisterHotKey(this.Handle, 1729);
                winapi.UnregisterHotKey(this.Handle, 1730);
                winapi.UnregisterHotKey(this.Handle, 1731);

                Resolution.CResolution ChangeRes800 = new Resolution.CResolution(moniwi, monihe);
                getnewhotkey();
            }
        }

        private void checkBox45_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox22.CheckState == CheckState.Checked)
            {
                showall();
                winapi.UnregisterHotKey(this.Handle, 1729);
                winapi.UnregisterHotKey(this.Handle, 1730);
                winapi.UnregisterHotKey(this.Handle, 1731);

                Resolution.CResolution ChangeRes800 = new Resolution.CResolution(moniwi, monihe);
                getnewhotkey();
            }
        }

        private void checkBox46_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox22.CheckState == CheckState.Checked)
            {
                showall();
                winapi.UnregisterHotKey(this.Handle, 1729);
                winapi.UnregisterHotKey(this.Handle, 1730);
                winapi.UnregisterHotKey(this.Handle, 1731);

                Resolution.CResolution ChangeRes800 = new Resolution.CResolution(moniwi, monihe);
                getnewhotkey();
            }
        }

        private void checkBox47_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox22.CheckState == CheckState.Checked)
            {
                showall();
                winapi.UnregisterHotKey(this.Handle, 1729);
                winapi.UnregisterHotKey(this.Handle, 1730);
                winapi.UnregisterHotKey(this.Handle, 1731);

                Resolution.CResolution ChangeRes800 = new Resolution.CResolution(moniwi, monihe);
                getnewhotkey();
            }
        }

        private void comboBox15_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (checkBox22.CheckState == CheckState.Checked)
            {
                showall();
                winapi.UnregisterHotKey(this.Handle, 1729);
                winapi.UnregisterHotKey(this.Handle, 1730);
                winapi.UnregisterHotKey(this.Handle, 1731);

                Resolution.CResolution ChangeRes800 = new Resolution.CResolution(moniwi, monihe);
                getnewhotkey();
            }
        }

        private void toolStripMenuItem36_Click(object sender, EventArgs e)
        {
            
        }

        private void pbnow_ItemCheck(object sender, ItemCheckEventArgs e)
        {

            try
            {
                int x = -1;
                x = pbh.FindItem(pbnow.Items[e.Index].SubItems[1].Text);

                if (x != -1)
                {


                    pbh.Items[x].Checked = Convert.ToBoolean(e.NewValue);

                    if (e.NewValue == CheckState.Checked)
                    {
                        pbnow.Items[e.Index].BackColor = Color.LightSkyBlue;
                    }
                    else
                    {
                        pbnow.Items[e.Index].BackColor = pbnow.BackColor;

                    }

                }

            }
            catch
            {

            }
        }

        

        private void pbh_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            try
            {

                int x = -1;
                if (e.NewValue == CheckState.Checked)
                {
             
                          pbh.Items[e.Index].BackColor = Color.LightSkyBlue;

                          x = listViewFind3.FindItem(pbh.Items[e.Index].Text);
                          if (x == -1)
                          {
                              listViewFind3.Items.Add(new ListViewItem(new string[] { pbh.Items[e.Index].Text, pbh.Items[e.Index].SubItems[1].Text, pbh.Items[e.Index].SubItems[8].Text }, pbh.Items[e.Index].ImageIndex));
                          }
                          
                    }
                else
                {
                    
                                      pbh.Items[e.Index].BackColor = pbh.BackColor;
                                      listViewFind3.Items[listViewFind3.FindItem(pbh.Items[e.Index].Text)].Remove();
               

                                      



                }
            }
            catch
            {
            }
        }

        private void toolStripMenuItem37_Click(object sender, EventArgs e)
        {
            try
            {
                pbh.SelectedItems[0].Checked = false ;
            }
            catch
            {

            }
        }

        private void button64_Click(object sender, EventArgs e)
        {
            sendrcon3(rcon.Text, "admin listbans", serverip.Text, Convert.ToInt32(serverport.Text), "", "");

        }

        private void toolStripMenuItem38_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetText(listViewFind1.SelectedItems[0].SubItems[2].Text);
            }

            catch
            {

            }
        }

        private void toolStripMenuItem39_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetText(listViewFind1.SelectedItems[0].SubItems[1].Text);

            }

            catch
            {

            }

        }

        private void toolStripMenuItem40_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("UnBan Item: " + listViewFind1.SelectedItems[0].Text + "?", "NTB", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    sendrcon(rcon.Text, "admin unban " + listViewFind1.SelectedItems[0].Text, serverip.Text, Convert.ToInt32(serverport.Text), "", "");

                    return;

                }
                else
                {
                    return;

                }
            }
            catch
            {
            }
        }

        

        private void ftpConnection1_Uploaded(object sender, EnterpriseDT.Net.Ftp.FTPFileTransferEventArgs e)
        {
            if (checkBox13.Checked)
            {
                ftplog.AppendText("Uploaded: <<< " + e.LocalFile + " >>>" + "\r\n");



            }

        }

        private void ftpConnection1_Downloaded(object sender, EnterpriseDT.Net.Ftp.FTPFileTransferEventArgs e)
        {

            if (checkBox13.Checked)
            {
                ftplog.AppendText("Downloaded: <<< " + e.RemoteFile  + " >>>" + "\r\n");



            }
        }

        private void button67_Click(object sender, EventArgs e)
        {
            int x = -1;

            if ((textBox41.Text != "") & comboBox18.Text != "")
            {

                try
                {
                
                    x = pbh.FindItem(textBox41.Text);
                 
                }
                catch
                {

                }
                
                
                
                
                if (comboBox18.Text == "join usergroup")
                {
                    if (comboBox16.Text == "")
                    {
                        MessageBox.Show("Usergroup Empty");
                        return;
                    }
                    else
                    {
                        if (x != -1)
                        {
                            listViewFind2.Items.Add(new ListViewItem(new string[] { textBox41.Text.ToLower(), comboBox18.Text + " " + comboBox16.Text, textBox40.Text.Replace("\t", " "), "waiting", "0", pbh.Items[x].SubItems[1].Text }, pbh.Items[x].ImageIndex ));

                        }
                        else
                        {
                            listViewFind2.Items.Add(new ListViewItem(new string[] { textBox41.Text.ToLower(), comboBox18.Text + " " + comboBox16.Text, textBox40.Text.Replace("\t", " "), "waiting", "0" ,""}, 14));

                        }

                        


                    }
                }
                else
                {
                    if (x != -1)
                    {
                        listViewFind2.Items.Add(new ListViewItem(new string[] { textBox41.Text.ToLower(), comboBox18.Text, textBox40.Text.Replace("\t", " "), "waiting", "0", pbh.Items[x].SubItems[1].Text }, pbh.Items[x].ImageIndex));

                    }
                    else
                    {
                        listViewFind2.Items.Add(new ListViewItem(new string[] { textBox41.Text.ToLower(), comboBox18.Text, textBox40.Text.Replace("\t", " "), "waiting", "0","" }, 14));

                    }



                }
            }
        }

        private void button66_Click(object sender, EventArgs e)
        {
            try
            {
              listViewFind2.Items.Remove(listViewFind2.SelectedItems[0]);
            }
            catch
            {
            }
        }

        private void button65_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox18.Text == "join usergroup")
                {
                    if (comboBox16.Text == "")
                    {
                        MessageBox.Show("Usergroup Empty");
                        return;
                    }
                    else
                    {
                      listViewFind2.SelectedItems[0].Text = textBox41.Text;
                      listViewFind2.SelectedItems[0].SubItems[1].Text = comboBox18.Text + " " + comboBox16.Text;
                      listViewFind2.SelectedItems[0].SubItems[2].Text = textBox40.Text.Replace("\t", " ");
                    }
                }
                else
                {
                    listViewFind2.SelectedItems[0].Text = textBox41.Text;
                    listViewFind2.SelectedItems[0].SubItems[1].Text = comboBox18.Text;
                    listViewFind2.SelectedItems[0].SubItems[2].Text = textBox40.Text.Replace("\t", " ");

                }




            }

            catch
            {

            }
        }

        private void button63_Click_1(object sender, EventArgs e)
        {
            listViewFind2.Items.Clear();
        }

        private void comboBox18_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox18.Text.ToLower() == "say")
            {
                textBox40.Enabled = true;
                label85.Enabled = true;
                textBox40.Text = "";
                comboBox16.Enabled = false ;
                label81.Enabled = false ;
            }
            else if (comboBox18.Text.ToLower() == "join usergroup")
            {
                comboBox16.Enabled = true;
                label81.Enabled = true;
                textBox40.Enabled = false;
                label85.Enabled = false;
                textBox40.Text = "";

            }
            else
            {
                textBox40.Enabled = false;
                label85.Enabled = false;
                textBox40.Text = "";
                comboBox16.Enabled = false;
                label81.Enabled = false;
            }
        }

        private void listViewFind2_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                try
                {
                    if (listViewFind2.Items[listViewFind2.HitTest(e.X, e.Y).Item.Index].SubItems[1].Text.Substring(0, 14) == "join usergroup")
                    {
                        comboBox18.Text = "join usergroup";
                        comboBox16.Text = listViewFind2.Items[listViewFind2.HitTest(e.X, e.Y).Item.Index].SubItems[1].Text.Substring(15, listViewFind2.Items[listViewFind2.HitTest(e.X, e.Y).Item.Index].SubItems[1].Text.Length - 15);
                    }

                    else
                    {
                        comboBox18.Text = listViewFind2.Items[listViewFind2.HitTest(e.X, e.Y).Item.Index].SubItems[1].Text;
                    }
                }
                catch
                {

                    comboBox18.Text = listViewFind2.Items[listViewFind2.HitTest(e.X, e.Y).Item.Index].SubItems[1].Text;

                }

                textBox40.Text = listViewFind2.Items[listViewFind2.HitTest(e.X, e.Y).Item.Index].SubItems[2].Text;
                textBox41.Text = listViewFind2.Items[listViewFind2.HitTest(e.X, e.Y).Item.Index].Text;
            }
            catch
            {

            }
        }

        private void button68_Click(object sender, EventArgs e)
        {
            if (!comboBox17.Items.Contains(comboBox17.Text))
            {
                comboBox17.Items.Insert(0, comboBox17.Text);
            }
        }

        private void button69_Click(object sender, EventArgs e)
        {
            if (comboBox17.Items.Contains(comboBox17.Text))
            {
                comboBox17.Items.Remove(comboBox17.Text );
            }
        }

        private void comboBox4_DropDown(object sender, EventArgs e)
        {
            comboBox4.Items.Clear();

            object[] obj = new object[comboBox17.Items.Count];
            comboBox17.Items.CopyTo(obj, 0);
            comboBox4.Items.AddRange(obj);
        }

        private void comboBox16_DropDown(object sender, EventArgs e)
        {
            comboBox16.Items.Clear();

            object[] obj = new object[comboBox17.Items.Count];
            comboBox17.Items.CopyTo(obj, 0);
            comboBox16.Items.AddRange(obj);
        }

        private void toolStripMenuItem41_Click(object sender, EventArgs e)
        {
            try
            {
                pbh.SelectedItems[0].Checked = true;
            }
            catch
            {

            }
        }

        private void toolStripMenuItem42_Click(object sender, EventArgs e)
        {
            try
            {
                protectedlv.Items.Add(new ListViewItem(new string[] { pbh.SelectedItems[0].SubItems[1].Text  }, 17));

            }
            catch
            {

            }
        }

        private void toolStripMenuItem44_Click(object sender, EventArgs e)
        {
            try
            {
                pbnow.SelectedItems[0].Checked = true;
            }
            catch
            {

            }
        }

        private void toolStripMenuItem45_Click(object sender, EventArgs e)
        {
            try
            {
                protectedlv.Items.Add(new ListViewItem(new string[] { pbnow.SelectedItems[0].SubItems[8].Text }, 17));

            }
            catch
            {

            }
        }

        private void toolStripMenuItem46_Click(object sender, EventArgs e)
        {
            try
            {
                pbnow.SelectedItems[0].Checked = false;
            }
            catch
            {

            }
        }

        private void listViewFind1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == lvwColumnSorter4.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (lvwColumnSorter4.Order == SortOrder.Ascending)
                {
                    lvwColumnSorter4.Order = SortOrder.Descending;
                }
                else
                {
                    lvwColumnSorter4.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                lvwColumnSorter4.SortColumn = e.Column;
                lvwColumnSorter4.Order = SortOrder.Ascending;
            }

            // Perform the sort with these new sort options.
            this.listViewFind1.Sort();
        }

        private void toolStripMenuItem47_Click(object sender, EventArgs e)
        {
            try
            {
                tabControl2.SelectedIndex = 4;
             textBox41.Text = pbnow.SelectedItems[0].SubItems[1].Text;
            }
            catch
            {

            }
            }

        private void toolStripMenuItem48_Click(object sender, EventArgs e)
        {
            try
            {
                tabControl2.SelectedIndex = 4;
                textBox41.Text = pbh.SelectedItems[0].Text;
            }
            catch
            {

            }
        }

        private void toolStripMenuItem49_Click(object sender, EventArgs e)
        {
            try
            {
                tabControl2.SelectedIndex = 4;
                textBox41.Text = banlist.SelectedItems[0].SubItems[1].Text;
            }
            catch
            {

            }
        }

        private void banlist_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip9.Show(banlist, e.X, e.Y);
            }
        }

        private void button70_Click(object sender, EventArgs e)
        {
            Clipboard.Clear();
            try
            {
                foreach (ListViewItem item in listViewFind1.Items)
                {
                    Clipboard.SetText(Clipboard.GetText() + item.Text + "\t" + item.SubItems[1].Text + "\t" + item.SubItems[2].Text + "\r\n");

                }
            }

            catch
            {

            }
        }

        private void toolStripMenuItem51_Click(object sender, EventArgs e)
        {
            button64_Click(null, null);
        }

        private void toolStripMenuItem50_Click(object sender, EventArgs e)
        {
            button70_Click(null, null);
        }

        private void toolStripMenuItem54_Click(object sender, EventArgs e)
        {
            button42_Click(null, null);
        }

        private void toolStripMenuItem55_Click(object sender, EventArgs e)
        {
            button23_Click(null, null);
        }

        private void toolStripMenuItem52_Click(object sender, EventArgs e)
        {
            button32_Click(null, null);
        }

        private void toolStripMenuItem53_Click(object sender, EventArgs e)
        {
            button33_Click(null, null);
        }

        private void toolStripMenuItem56_Click(object sender, EventArgs e)
        {
            try
            {
                tabControl2.SelectedIndex = 1;

                pbh.Items[pbh.FindItem(listViewFind2.SelectedItems[0].Text)].Selected = true;
                pbh.Items[pbh.FindItem(listViewFind2.SelectedItems[0].Text)].EnsureVisible();
                pbh.Focus();
            }
            catch
            {

            }
        }

        private void toolStripMenuItem57_Click(object sender, EventArgs e)
        {
            button66_Click(null, null);
        }

        private void button71_Click(object sender, EventArgs e)
        {

           
        }

        private void toolStripMenuItem58_Click(object sender, EventArgs e)
        {
            button71_Click(null, null);  
        }

     

        private void listViewFind3_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            try
            {

                if (e.NewValue == CheckState.Checked)
                {

                    pbh.Items[pbh.FindItem(listViewFind3.Items[e.Index].Text)].BackColor = Color.LightSkyBlue;

           
                }
                else
                {

                    pbh.Items[pbh.FindItem(listViewFind3.Items[e.Index].Text)].BackColor = pbh.BackColor;
                    pbh.Items[pbh.FindItem(listViewFind3.Items[e.Index].Text)].Checked  = false;

                }
            }
            catch
            {

            }
        }

        private void toolStripMenuItem59_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (ListViewItem ca in listViewFind3.SelectedItems)
                {


                    pbh.Items[pbh.FindItem(ca.Text)].Checked = false;
                    ca.Remove();// listViewFind3.SelectedItems[0].Remove();
                }
                }

            catch
            {

            }
   
        }

        private void listViewFind3_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == lvwColumnSorter5.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (lvwColumnSorter5.Order == SortOrder.Ascending)
                {
                    lvwColumnSorter5.Order = SortOrder.Descending;
                }
                else
                {
                    lvwColumnSorter5.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                lvwColumnSorter5.SortColumn = e.Column;
                lvwColumnSorter5.Order = SortOrder.Ascending;
            }

            // Perform the sort with these new sort options.
            this.listViewFind3.Sort();
        }

        private void players_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem58_Click_1(object sender, EventArgs e)
        {
            try
            {
         
                tabControl2.SelectedIndex = 4;
                textBox41.Text = listViewFind3.SelectedItems[0].Text;
            }
            catch
            {

            }
        }

        private void toolStripMenuItem60_Click(object sender, EventArgs e)
        {
            try
            {
                lvwColumnSorter.Order = SortOrder.None;
                foreach (ListViewItem kk in pbh.CheckedItems)
                {
                    kk.Remove();
                    pbh.Items.Insert(0, kk);

                }
            }
            catch
            {

            }
        }

        private void checkBox14_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox14.Checked == false )
            {
                MessageBox.Show("AutoRefresh disabled, some functions on this tab may not work properly","NTB", MessageBoxButtons.OK);
     

            }
        }

        private void textBox24_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                  try
            {
                           for (int i = pbh.SelectedItems[0].Index +1; i < pbh.Items.Count; i++)

                {

                    try
                    {
                        if (comboBox11.Text == "GUID")
                        {
                            if (pbh.Items[i].Text.Substring(0, textBox24.Text.Length).ToLower() == textBox24.Text.ToLower())
                            {
                                pbh.Items[i].Selected = true;
                                pbh.Items[i].EnsureVisible();
                                return;
                            }
                    

                        }
                        else if (comboBox11.Text == "Name")
                        {
                            if (pbh.Items[i].SubItems[1].Text.Substring(0, textBox24.Text.Length).ToLower() == textBox24.Text.ToLower())
                            {
                                pbh.Items[i].Selected = true;
                                pbh.Items[i].EnsureVisible();
                                return;
                            }
                        }
                        else if (comboBox11.Text == "Address")
                        {
                            if (pbh.Items[i].SubItems[2].Text.Substring(0, textBox24.Text.Length) == textBox24.Text)
                            {
                                pbh.Items[i].Selected = true;
                                pbh.Items[i].EnsureVisible();
                                return;
                            }
                        }
                        else if (comboBox11.Text == "Alias")
                        {
                            if (pbh.Items[i].SubItems[10].Text.ToLower().Contains(textBox24.Text.ToLower()))
                            {
                                pbh.Items[i].Selected = true;
                                pbh.Items[i].EnsureVisible();
                         
                                return;
                            }
                        }
                 
                    }
                    catch
                    {
                     
 
                    }
                }
            }

            catch
            {

            }
            }
        }

        private void toolStripMenuItem61_Click(object sender, EventArgs e)
        {
            try
            {

                progressBar3.Maximum = pbh.Items.Count;
                progressBar3.Value = 0;
                if (button28.Text == "Update aliases list")
                {
                    if (MessageBox.Show("Scanning " + pbh.Items.Count.ToString() + " players for aliases will take a long time and may cause serious lag on the server. Continue?", "NTB", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                    }
                    else
                    {
                        return;
                    }
                    button28.Text = "Cancel";
                    foreach (ListViewItem otto in pbh.Items)
                    {

                        sendrcon2(rcon.Text, "pb_sv_alist " + otto.Text, serverip.Text, Convert.ToInt32(serverport.Text), "", "");
                        progressBar3.Value = otto.Index;
                        if (button28.Text == "Update aliases list")
                        {
                            progressBar3.Value = 0;

                            return;
                        }

                    }
                    button28.Text = "Update aliases list";
                    progressBar3.Value = 0;


                }
                else
                {
                    button28.Text = "Update aliases list";
                    progressBar3.Value = 0;

                }
            }

            catch
            {

            }
        }

        private void toolStripMenuItem62_Click(object sender, EventArgs e)
        {
            try
            {

                progressBar3.Maximum = pbh.Items.Count;
                progressBar3.Value = 0;
                if (button28.Text == "Update aliases list")
                {
                   
                    button28.Text = "Cancel";
                    foreach (ListViewItem otto in pbh.Items)
                    {
                        if (otto.SubItems[10].Text  == "")
                        {
                            sendrcon2(rcon.Text, "pb_sv_alist " + otto.Text, serverip.Text, Convert.ToInt32(serverport.Text), "", "");
                        }
                            progressBar3.Value = otto.Index;
                        if (button28.Text == "Update aliases list")
                        {
                            progressBar3.Value = 0;

                            return;
                        }

                    }
                    button28.Text = "Update aliases list";
                    progressBar3.Value = 0;


                }
                else
                {
                    button28.Text = "Update aliases list";
                    progressBar3.Value = 0;

                }
            }

            catch
            {

            }
        }

        private void button28_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {

                if (button28.Text == "Update aliases list")
                {

                    contextMenuStrip12.Show(button28, e.X ,e.Y);

                }
                else
                {
                    button28.Text = "Update aliases list";
                    progressBar3.Value = 0;

                }
            }

            catch
            {

            }
        }

        private void ctu1_Click(object sender, EventArgs e)
        {
            ctu1.Checked = !ctu1.Checked;
        }

        private void ctu2_Click(object sender, EventArgs e)
        {
            ctu2.Checked = !ctu2.Checked; 
        }

        private void ctu3_Click(object sender, EventArgs e)
        {
            ctu3.Checked = !ctu3.Checked; 

        }

        private void ddm4_CheckedChanged(object sender, EventArgs e)
        {
            groupBox18.Enabled = ddm4.Checked;
       
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            groupBox1.Enabled = checkBox5.Checked; 
        }

        private void button71_Click_1(object sender, EventArgs e)
        {
            sendrcon(rcon.Text, "say '" + textBox45.Text.Replace("$killername1", players.Items[Convert.ToInt32(info.Items[18].SubItems[2].Text)].SubItems[9].Text.Replace("'", "`")).Replace("$kills1", players.Items[Convert.ToInt32(info.Items[18].SubItems[2].Text)].SubItems[7].Text).Replace("$killerkd1", players.Items[Convert.ToInt32(info.Items[18].SubItems[2].Text)].SubItems[10].Text) + "'" + "\r" + "say '" + textBox46.Text.Replace("$killername2", players.Items[Convert.ToInt32(info.Items[19].SubItems[2].Text)].SubItems[9].Text.Replace("'", "`")).Replace("$kills2", players.Items[Convert.ToInt32(info.Items[19].SubItems[2].Text)].SubItems[7].Text).Replace("$killerkd2", players.Items[Convert.ToInt32(info.Items[19].SubItems[2].Text)].SubItems[10].Text) + "'" + "\r" + "say '" + textBox47.Text.Replace("$killername3", players.Items[Convert.ToInt32(info.Items[20].SubItems[2].Text)].SubItems[9].Text.Replace("'", "`")).Replace("$kills3", players.Items[Convert.ToInt32(info.Items[20].SubItems[2].Text)].SubItems[7].Text).Replace("$killerkd3", players.Items[Convert.ToInt32(info.Items[20].SubItems[2].Text)].SubItems[10].Text) + "'", serverip.Text, Convert.ToInt32(serverport.Text), "", "");
            System.Threading.Thread.Sleep(Convert.ToInt32(numericUpDown7.Value));
            sendrcon(rcon.Text, "say '" + textBox48.Text.Replace("$kdrname1", players.Items[Convert.ToInt32(info.Items[15].SubItems[2].Text)].SubItems[9].Text.Replace("'", "`")).Replace("$kd1", players.Items[Convert.ToInt32(info.Items[15].SubItems[2].Text)].SubItems[10].Text) + "'" + "\r" + "say '" + textBox49.Text.Replace("$kdrname2", players.Items[Convert.ToInt32(info.Items[16].SubItems[2].Text)].SubItems[9].Text.Replace("'", "`")).Replace("$kd2", players.Items[Convert.ToInt32(info.Items[16].SubItems[2].Text)].SubItems[10].Text) + "'" + "\r" + "say '" + textBox50.Text.Replace("$kdrname3", players.Items[Convert.ToInt32(info.Items[17].SubItems[2].Text)].SubItems[9].Text.Replace("'", "`")).Replace("$kd3", players.Items[Convert.ToInt32(info.Items[17].SubItems[2].Text)].SubItems[10].Text) + "'", serverip.Text, Convert.ToInt32(serverport.Text), "", "");
            sendrcon(rcon.Text, "say '" + textBox52.Text.Replace("$lemming", info.Items[9].SubItems[1].Text.Replace("'", "`")).Replace("$deaths", info.Items[9].SubItems[2].Text) + "'", serverip.Text, Convert.ToInt32(serverport.Text), "", "");

        }

        private void button72_Click(object sender, EventArgs e)
        {
            sendrcon(rcon.Text, "say '" + textBox51.Text.Replace("$gdfkills", info.Items[11].SubItems[1].Text).Replace("$gdfdeaths", info.Items[11].SubItems[2].Text) + "'" + "\r" + "say '" + textBox53.Text.Replace("$stroggkills", info.Items[12].SubItems[1].Text).Replace("$stroggdeaths", info.Items[12].SubItems[2].Text) + "'", serverip.Text, Convert.ToInt32(serverport.Text), "", "");

        }

        private void checkBox50_CheckedChanged_1(object sender, EventArgs e)
        {
            panel33.Enabled = checkBox50.Checked; 
        }

        private void checkBox52_CheckedChanged(object sender, EventArgs e)
        {
            panel34.Enabled = checkBox52.Checked; 

        }

        private void textBox13_KeyDown(object sender, KeyEventArgs e)
        {
            if (textBox13.Text == "")
            {
                return;
            }
            if (e.KeyCode == Keys.Enter)
            {
                try
                {

                    for (int i = banlist.SelectedItems[0].Index + 1; i < banlist.Items.Count; i++)
                    {

                        try
                        {
                            if (comboBox10.Text == "GUID")
                            {
                                if (banlist.Items[i].SubItems[1].Text.Substring(0, textBox13.Text.Length).ToLower() == textBox13.Text.ToLower())
                                {
                                    banlist.Items[i].Selected = true;
                                    banlist.Items[i].EnsureVisible();
                                    return;
                                }


                            }
                            else if (comboBox10.Text == "Name")
                            {
                                if (banlist.Items[i].SubItems[2].Text.Substring(0, textBox13.Text.Length).ToLower() == textBox13.Text.ToLower())
                                {
                                    banlist.Items[i].Selected = true;
                                    banlist.Items[i].EnsureVisible();
                                    return;
                                }
                            }
                            else if (comboBox10.Text == "Address")
                            {
                                if (banlist.Items[i].SubItems[3].Text.Substring(0, textBox13.Text.Length) == textBox13.Text)
                                {
                                    banlist.Items[i].Selected = true;
                                    banlist.Items[i].EnsureVisible();
                                    return;
                                }
                            }
                        }
                        catch
                        {
                        }
                    }
                }

                catch
                {

                }
            }
        }

        private void checkBox54_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox54.Checked)
            {
                log.BackColor = Color.Black;
                log.ForeColor = Color.GreenYellow;
            }
            else
            {
                log.BackColor = textBox54.BackColor;
                log.ForeColor = textBox54.ForeColor;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Possible Errors:" + "\r\n" + "\r\n" + "Alias Tracking: If no players alias are found check \"PB_SV_ALIASMAX\", set it to 5 or more." + "\r\n" + "\r\n" + "No players listed: Check if the PB_SV_MsgPrefix matchs with the one on the \"Local Settings\" options.", "NTB", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button73_Click(object sender, EventArgs e)
        {
            try
            {
                
                    sendrcon2(rcon.Text, "pb_sv_msgprefix", serverip.Text, Convert.ToInt32(serverport.Text), "", "");
                
            }

            catch
            {

            }
        }

        private void button74_Click(object sender, EventArgs e)
        {
            globaltohtml();
        }

        private void listViewFind4_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                textBox57.Text = listViewFind4.SelectedItems[0].Text;
                textBox58.Text = listViewFind4.SelectedItems[0].SubItems[1].Text;
            }
            catch
            {

            }
        }

        private void button77_Click(object sender, EventArgs e)
        {
          listViewFind4.Items.Add(new ListViewItem(new string[] { textBox57.Text,textBox58.Text}));

        }

        private void button76_Click(object sender, EventArgs e)
        {
            listViewFind4.Items.Remove(listViewFind4.SelectedItems[0]);   
        }

        private void button75_Click(object sender, EventArgs e)
        {
            try
            {
                listViewFind4.SelectedItems[0].Text = textBox57.Text;
                listViewFind4.SelectedItems[0].SubItems[1].Text = textBox58.Text;
            }

            catch
            {
                MessageBox.Show("Select the Item", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button78_Click(object sender, EventArgs e)
        {
            try
            {
                FileStream fs22 = new FileStream(Application.StartupPath + "\\" + "htmlcolorcodes.dat", FileMode.Create, FileAccess.Write);
                StreamWriter m_streamWriter = new StreamWriter(fs22);
                m_streamWriter.Flush();
                m_streamWriter.BaseStream.Seek(0, SeekOrigin.Begin);

                foreach (ListViewItem pe in listViewFind4.Items)
                {
                    m_streamWriter.Write(pe.Text + "\t" + pe.SubItems[1].Text);
                    m_streamWriter.WriteLine();


                }


                m_streamWriter.Flush();
                m_streamWriter.Close();
                fs22.Dispose();
                m_streamWriter.Dispose();
                Cursor = Cursors.Default;





            }
            catch
            {
                Cursor = Cursors.Default;
            }

            listViewFind4.Items.Clear();
            carlitos.Clear();
            loadcolors();


        }

        private void button79_Click(object sender, EventArgs e)
        {
            sessiontohtml();
        }

        private void button79_Click_1(object sender, EventArgs e)
        {
            try
            {
                openFileDialog1.InitialDirectory = Application.StartupPath + "\\pblistbackup";
                openFileDialog1.Filter = openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog1.FilterIndex = 1;
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {


                    loadpbfromfile(openFileDialog1.FileName);

                    status.Text = DateTime.Now.ToLocalTime().ToString() + " " + "Rebuilding Database...";
                    
                    
                    
                    Application.DoEvents();



                  


                }

            }

            catch
            {

            }
        }

        private void button80_Click(object sender, EventArgs e)
        {
            savepb2();
            pbh.Items.Clear();
            listViewFind3.Items.Clear();
        }

        private void button81_Click(object sender, EventArgs e)
        {
            sameip();
        }

        

    

       

    

      
       

       

     

        
       

       

        
        
      




    }
}
