using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMDSwitcherLib
{
    public partial class DeviceForm : Form
    {
        public string DeviceAddress;

        public DeviceForm()
        {
            InitializeComponent();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.DeviceAddress = textBoxAddress.Text;
        }

        private void DeviceForm_Load(object sender, EventArgs e)
        {
            textBoxAddress.Text = this.DeviceAddress;
        }
    }
}
