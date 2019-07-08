using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using BMDSwitcherLib;
using System.Threading;

namespace Demos
{
    public partial class Form1 : Form
    {
        Switcher sw;

        public Form1()
        {
            InitializeComponent();
            sw = new Switcher("192.168.0.205");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void b_connect_Click(object sender, EventArgs e)
        {
            sw.Discover();
            sw.Connect();

            //Upload upload = new Upload(sw, "C:\\Users\\cschaftenaar\\Desktop\\logo.bmp", 10);
            //upload.Start();
            //while (upload.InProgress())
            //{
            //    Thread.Sleep(100);
            //}
        }

        private void b_disconnect_Click(object sender, EventArgs e)
        {
            sw.Disconnect();
            sw = null;
        }
    }
}
