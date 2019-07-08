using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PIEHid32Net;


namespace PIHidDotName_Csharp_Sample
{
    public partial class Form1 : Form, PIEDataHandler, PIEErrorHandler
    {
        PIEDevice[] devices;
        
        int[] cbotodevice=null; //for each item in the CboDevice list maps this index to the device index.  Max devices =100 
        byte[] wData = null; //write data buffer
        int selecteddevice=-1; //set to the index of CboDevice
        
        //for thread-safe way to call a Windows Forms control
        // This delegate enables asynchronous calls for setting
        // the text property on a TextBox control.
        delegate void SetTextCallback(string text);
        Control c;
        //end thread-safe

       
        byte[] pdata = null; //previous read data
       
        
        int wheel = 1;
        bool triggerit = false;
        byte savelastsent;
        
        bool firstread=true;
        double runningtotal=0;
        double lastdegrees;

        public Form1()
        {
            InitializeComponent();
            pdata = new byte[80];
            CboWheel.SelectedIndex = 4;
            CboLever1.SelectedIndex = 0;
            CboLever2.SelectedIndex = 1;
            CboSw1.SelectedIndex = 3;
            CboSw2.SelectedIndex = 2;
        }

        private void BtnEnumerate_Click(object sender, EventArgs e)
        {
            CboDevices.Items.Clear();
            cbotodevice = new int[128]; //128=max # of devices
            //enumerate and setupinterfaces for all devices
            devices = PIEDevice.EnumeratePIE();
            if (devices.Length == 0)
            {
                toolStripStatusLabel1.Text = "No Devices Found";
            }
            else
            {
                //System.Media.SystemSounds.Beep.Play(); 
                int cbocount=0; //keeps track of how many valid devices were added to the CboDevice box
                for (int i = 0; i < devices.Length; i++)
                {
                    //information about device
                    //PID = devices[i].Pid);
                    //HID Usage = devices[i].HidUsage);
                    //HID Usage Page = devices[i].HidUsagePage);
                    //HID Version = devices[i].Version);
                    if (devices[i].HidUsagePage == 0xc)
                    {
                        switch (devices[i].Pid)
                        {
                            case 1055:
                                CboDevices.Items.Add("ShipDriver ("+devices[i].Pid+"), ID: " + i);
                                cbotodevice[cbocount] = i;
                                cbocount++;
                                break;
                            
                            default:
                                CboDevices.Items.Add("Unknown Device (" + devices[i].Pid + "), ID: " + i);
                                cbotodevice[cbocount] = i;
                                cbocount++;
                                break;
                        }
                        devices[i].SetupInterface();
                        devices[i].suppressDuplicateReports = false;
                    }
                }
            }
            if (CboDevices.Items.Count > 0)
            {
                CboDevices.SelectedIndex = 0;
                selecteddevice = cbotodevice[CboDevices.SelectedIndex];
                wData = new byte[devices[selecteddevice].WriteLength];//go ahead and setup for write
            }
           
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            timer1.Enabled = false;
            //closeinterfaces on all devices
            for (int i = 0; i < CboDevices.Items.Count; i++)
            {
                devices[cbotodevice[i]].CloseInterface();
            }
            System.Environment.Exit(0);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "";
        }

        private void CboDevices_SelectedIndexChanged(object sender, EventArgs e)
        {
            selecteddevice = cbotodevice[CboDevices.SelectedIndex];
            wData = new byte[devices[selecteddevice].WriteLength];//size write array 
        }
        private void BtnCallback_Click(object sender, EventArgs e)
        {
            //setup callback if there are devices found for each device found
      
            if (CboDevices.SelectedIndex != -1)
            {
                for (int i = 0; i < CboDevices.Items.Count; i++)
                {
                    //use the cbotodevice array which contains the mapping of the devices in the CboDevices to the actual device IDs
                    devices[cbotodevice[i]].SetErrorCallback(this);
                    devices[cbotodevice[i]].SetDataCallback(this);
                    devices[cbotodevice[i]].callNever = false;
                }
                
            }		 
        }
        //data callback    
        public void HandlePIEHidData(Byte[] data, PIEDevice sourceDevice, int error)
        {
            //check the sourceDevice and make sure it is the same device as selected in CboDevice   
            if (sourceDevice == devices[selecteddevice])
            {
                
                    //write raw data to listbox1
                    String output = "Callback: " + sourceDevice.Pid + ", ID: " + selecteddevice.ToString() + ", data=";
                    for (int i = 0; i < sourceDevice.ReadLength; i++)
                    {
                        output = output + data[i].ToString() + "  ";
                    }
                    //Wheel MSB = rdata[11]
                    //Wheel LSB = rdata[12]
                    //Lever 1 = rdata[13]
                    //Lever 2 = rdata[14]
                    //Lower Switch = rdata[15]
                    //Upper Switch = rdata[16]
                    //Ship Mode = rdata[17]  //
                    //Lever 1 calibrated = rdata[18]
                    //Lever 2 calibrated = rdata[19]
                    //Lower Switch calibrated = rdata[20]
                    //Upper Switch calibrated = rdata[21]

                    this.SetListBox(output);
                    
                    //read the unit ID
                    c = this.LblUnitID;
                    this.SetText(data[1].ToString());

                    c = this.TxtWheel;
                    double theta = data[11] * 256.0 + data[12];
                    theta = (theta / (256.0 * 256.0)) * 360.0;
                    theta = 360 - theta;
                    this.SetText(theta.ToString());
                   
                    c = this.TxtLever1;
                    this.SetText(data[18].ToString());

                    c = this.TxtLever2;
                    this.SetText(data[19].ToString());

                    c = this.TxtSwitch2;
                    this.SetText(data[20].ToString());

                    c = this.TxtSwitch1;
                    this.SetText(data[21].ToString());

                    //find the direction of the wheel
                    
                    double delta = theta - lastdegrees;
                    int halfrange = 180;
                    int fullrange = 360;
                    if (delta < -1 * halfrange)
                        delta = delta + fullrange;
                    else if (delta > halfrange)
                        delta = delta - fullrange;
                    c = this.LblWheelDir;
                    if (delta > 0)  
                    {
                        this.SetText("clockwise");
                    }
                    else 
                    {
                        this.SetText("counter clockwise");
                    }

                    //limit the wheel data to desired bounds of -180 to 180
                    if (firstread == false)
                    {
                        runningtotal = runningtotal + delta;

                        if (runningtotal > 180)
                            runningtotal = 180;

                        if (runningtotal < -180)
                            runningtotal = -180;

                        c = this.LblPos180;
                        this.SetText(runningtotal.ToString());
                    }
                    else
                    {
                        runningtotal = 0;
                    }
                    lastdegrees = theta;
                    firstread = false;
                }
            
        }
        //error callback
        public void HandlePIEHidError(PIEDevice sourceDevice, Int32 error)
        {
            this.SetToolStrip("Error: " + error.ToString());
        }
        
        //for threadsafe setting of Windows Forms control
        private void SetText(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.c.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.c.Text = text;
            }
        }
        private void SetListBox(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.listBox1.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetListBox);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.listBox1.Items.Add(text);
                this.listBox1.SelectedIndex = this.listBox1.Items.Count - 1;
            }
        }
        //for threadsafe setting of Windows Forms control
        private void SetToolStrip(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.statusStrip1.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetToolStrip);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.toolStripStatusLabel1.Text = text;
            }
        }    

        private void BtnClear_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
           
        }

        private void BtnWriteDisplay_Click(object sender, EventArgs e)
        {
            if (CboDevices.SelectedIndex != -1)
            {
                //write to the LED Segments
                for (int j = 0; j < devices[selecteddevice].WriteLength; j++)
                {
                    wData[j] = 0;
                }

                //do this first to stop auto writing to display (from step 7.)
                wData[1] = 195;

                int result = 404;
                while (result == 404) { result = devices[selecteddevice].WriteData(wData); }
                if (result != 0)
                {
                    toolStripStatusLabel1.Text = "Write Fail: " + result;
                }
                else
                {
                    toolStripStatusLabel1.Text = "Write Success - Write to Display";
                }

                //now write to display
                wData[1] = 187;
                wData[2] = (byte)(Convert.ToInt16(textBox1.Text));
                wData[3] = (byte)(Convert.ToInt16(textBox2.Text));
                wData[4] = (byte)(Convert.ToInt16(textBox3.Text));

                result = 404;
                while (result == 404) { result = devices[selecteddevice].WriteData(wData); }
                if (result != 0)
                {
                    toolStripStatusLabel1.Text = "Write Fail: " + result;
                }
                else
                {
                    toolStripStatusLabel1.Text = "Write Success - Write to Display";
                }
            }
        }

        private void BtnSpeakerOn_Click(object sender, EventArgs e)
        {
            if (CboDevices.SelectedIndex != -1)
            {
                //turn speaker on
                //the following will turn on the ShipDriver speaker and
                //If you wish to
                //hear only the ShipDriver speaker disconnect the Speaker
                //Pass Thru in the back of the unit.  Make sure the ShipDriver
                //power and speaker are plugged in.
                for (int j = 0; j < devices[selecteddevice].WriteLength; j++)
                {
                    wData[j] = 0;
                }

                wData[1] = 215;
                wData[2] = 1;

                int result = 404;
                while (result == 404) { result = devices[selecteddevice].WriteData(wData); }
                if (result != 0)
                {
                    toolStripStatusLabel1.Text = "Write Fail: " + result;
                }
                else
                {
                    toolStripStatusLabel1.Text = "Write Success - Speaker On";
                    
                }
            }
        }

        private void BtnSpeakerOff_Click(object sender, EventArgs e)
        {
            if (CboDevices.SelectedIndex != -1)
            {
                //turn speaker off
                
                for (int j = 0; j < devices[selecteddevice].WriteLength; j++)
                {
                    wData[j] = 0;
                }

                wData[1] = 215;
                wData[2] = 0;

                int result = 404;
                while (result == 404) { result = devices[selecteddevice].WriteData(wData); }
                if (result != 0)
                {
                    toolStripStatusLabel1.Text = "Write Fail: " + result;
                }
                else
                {
                    toolStripStatusLabel1.Text = "Write Success - Speaker Off";
                   
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
           
        }

        private void BtnCal_Click(object sender, EventArgs e)
        {
            //A calibration should be done at least once on each ShipDriver
            if (CboDevices.SelectedIndex != -1)
            {
                DialogResult dresult;
                dresult = MessageBox.Show("Move levers and switches over full range, leaving each in center.  Rotate wheel slowly two or three revoluations.  When all controls are centered press Save Cal.", "ShipDriver Sample", MessageBoxButtons.OKCancel);
                if (dresult == System.Windows.Forms.DialogResult.OK)
                {


                    for (int j = 0; j < devices[selecteddevice].WriteLength; j++)
                    {
                        wData[j] = 0;
                    }

                    wData[1] = 195;
                    wData[2] = 7;

                    int result = 404;
                    while (result == 404) { result = devices[selecteddevice].WriteData(wData); }
                    if (result != 0)
                    {
                        toolStripStatusLabel1.Text = "Write Fail: " + result;
                    }
                    else
                    {
                        toolStripStatusLabel1.Text = "Write Success - Start Cal";

                    }
                }
            }
        }

        private void BtnStopCal_Click(object sender, EventArgs e)
        {
            if (CboDevices.SelectedIndex != -1)
            {
               

                for (int j = 0; j < devices[selecteddevice].WriteLength; j++)
                {
                    wData[j] = 0;
                }

                wData[1] = 195;
                wData[2] = 9;

                int result = 404;
                while (result == 404) { result = devices[selecteddevice].WriteData(wData); }
                if (result != 0)
                {
                    toolStripStatusLabel1.Text = "Write Fail: " + result;
                }
                else
                {
                    toolStripStatusLabel1.Text = "Write Success - Save Cal";
                    
                }
            }
        }

        private void BtnUnitID_Click(object sender, EventArgs e)
        {
            if (CboDevices.SelectedIndex != -1)
            {
                //write a unique Unit ID to the device (0-255)
                for (int j = 0; j < devices[selecteddevice].WriteLength; j++)
                {
                    wData[j] = 0;
                }

                wData[1] = 189;
                wData[2] = (byte)(Convert.ToInt16(TxtUnitID.Text));

                int result = 404;
                while (result == 404) { result = devices[selecteddevice].WriteData(wData); }
                if (result != 0)
                {
                    toolStripStatusLabel1.Text = "Write Fail: " + result;
                }
                else
                {
                    toolStripStatusLabel1.Text = "Write Success - Write Unit ID";
                }
            }
        }

        private void BtnDisplayWheel_Click(object sender, EventArgs e)
        {
            if (CboDevices.SelectedIndex != -1)
            {
               
                for (int j = 0; j < devices[selecteddevice].WriteLength; j++)
                {
                    wData[j] = 0;
                }

                wData[1] = 195;
                wData[2] = 1;

                int result = 404;
                while (result == 404) { result = devices[selecteddevice].WriteData(wData); }
                if (result != 0)
                {
                    toolStripStatusLabel1.Text = "Write Fail: " + result;
                }
                else
                {
                    toolStripStatusLabel1.Text = "Write Success - Display Wheel";
                    
                }
            }
        }

        private void BtnDisplayLever1_Click(object sender, EventArgs e)
        {
            if (CboDevices.SelectedIndex != -1)
            {

                for (int j = 0; j < devices[selecteddevice].WriteLength; j++)
                {
                    wData[j] = 0;
                }

                wData[1] = 195;
                wData[2] = 2;

                int result = 404;
                while (result == 404) { result = devices[selecteddevice].WriteData(wData); }
                if (result != 0)
                {
                    toolStripStatusLabel1.Text = "Write Fail: " + result;
                }
                else
                {
                    toolStripStatusLabel1.Text = "Write Success - Display Lever 1";

                }
            }
        }

        private void BtnDisplayLever2_Click(object sender, EventArgs e)
        {
            if (CboDevices.SelectedIndex != -1)
            {

                for (int j = 0; j < devices[selecteddevice].WriteLength; j++)
                {
                    wData[j] = 0;
                }

                wData[1] = 195;
                wData[2] = 3;

                int result = 404;
                while (result == 404) { result = devices[selecteddevice].WriteData(wData); }
                if (result != 0)
                {
                    toolStripStatusLabel1.Text = "Write Fail: " + result;
                }
                else
                {
                    toolStripStatusLabel1.Text = "Write Success - Display Lever 2";

                }
            }
        }

        private void BtnDisplaySwitch1_Click(object sender, EventArgs e)
        {
            if (CboDevices.SelectedIndex != -1)
            {

                for (int j = 0; j < devices[selecteddevice].WriteLength; j++)
                {
                    wData[j] = 0;
                }

                wData[1] = 195;
                wData[2] = 5;

                int result = 404;
                while (result == 404) { result = devices[selecteddevice].WriteData(wData); }
                if (result != 0)
                {
                    toolStripStatusLabel1.Text = "Write Fail: " + result;
                }
                else
                {
                    toolStripStatusLabel1.Text = "Write Success - Display Switch 1";

                }
            }
        }

        private void BtnDisplaySwitch2_Click(object sender, EventArgs e)
        {
            if (CboDevices.SelectedIndex != -1)
            {

                for (int j = 0; j < devices[selecteddevice].WriteLength; j++)
                {
                    wData[j] = 0;
                }

                wData[1] = 195;
                wData[2] = 4;

                int result = 404;
                while (result == 404) { result = devices[selecteddevice].WriteData(wData); }
                if (result != 0)
                {
                    toolStripStatusLabel1.Text = "Write Fail: " + result;
                }
                else
                {
                    toolStripStatusLabel1.Text = "Write Success - Display Switch 2";

                }
            }
        }

        private void BtnKeyboardReflect_Click(object sender, EventArgs e)
        {
            if (CboDevices.SelectedIndex != -1)
            {
                int result = -1;
                TxtKeyboardReflect.Focus();  //make sure there is a place to receive keyboard input
                for (int j = 0; j < devices[selecteddevice].WriteLength; j++)
                {
                    wData[j] = 0;
                }
                //send key downs
                wData[1] = 201;
                //modifier, 0 and 6 keys
                wData[2] = 2; //modifier key, shift down
                wData[3] = 0; //always 0
                wData[4] = 0x04; //a down
                wData[5] = 0x05; //b down
                wData[6] = 0x06; //c down
                wData[7] = 0x07; //d down
                wData[8] = 0x08; //e down
                wData[9] = 0x09; //f down


                result = 404;
                while (result == 404) { result = devices[selecteddevice].WriteData(wData); }
               
                //send key ups
                wData[2] = 0; //modifier key, shift up
                wData[3] = 0; //always 0
                wData[4] = 0; //a up
                wData[5] = 0; //b up
                wData[6] = 0; //c up
                wData[7] = 0; //d up
                wData[8] = 0; //e up
                wData[9] = 0; //f up

                 result = 404;
                while (result == 404) { result = devices[selecteddevice].WriteData(wData); }
                if (result != 0)
                {
                    toolStripStatusLabel1.Text = "Write Fail: " + result;
                }
                else
                {
                    toolStripStatusLabel1.Text = "Write Success - Keyboard Reflector";
                }
            }
        }

        private void BtnJoystickReflect_Click(object sender, EventArgs e)
        {
            //NOTE user must set the analogs to data only in order to use this feature
            if (CboDevices.SelectedIndex != -1)
            {
                //after entering values in the text boxes and clicking this button make the Game Controller-Properties window active to see changes
                for (int j = 0; j < devices[selecteddevice].WriteLength; j++)
                {
                    wData[j] = 0;
                }

                wData[1] = 202;
                wData[2] = (byte)(Convert.ToInt16(TxtJoyX.Text)); //x 0-127=center to full right, 255-128=center to full left
                wData[3] = (byte)(Convert.ToInt16(TxtJoyY.Text)); //y 0-127=center to bottom, 255-128=center to top
                wData[4] = (byte)(Convert.ToInt16(TxtJoyZrot.Text)); //z rot 0-127=center to bottom, 255-128=center to top
                wData[5] = (byte)(Convert.ToInt16(TxtJoyZ.Text)); //z 0-127=center to bottom, 255-128=center to top
                wData[6] = (byte)(Convert.ToInt16(TxtJoySlid.Text)); //slider 0-127=center to bottom, 255-128=center to top
                wData[7] = (byte)(Convert.ToInt16(TxtJoyButtons.Text)); //buttons 1-8
                wData[8] = (byte)(Convert.ToInt16(TxtJoyButtons2.Text)); //buttons 9-16
                wData[9] = (byte)(Convert.ToInt16(TxtJoyButtons3.Text)); //buttons 17-24
                wData[10] = (byte)(Convert.ToInt16(TxtJoyButtons4.Text)); //buttons 25-32
                wData[12] = (byte)(Convert.ToInt16(TxtJoyHat.Text)); //hat

                int result = 404;
                while (result == 404) { result = devices[selecteddevice].WriteData(wData); }
                if (result != 0)
                {
                    toolStripStatusLabel1.Text = "Write Fail: " + result;
                }
                else
                {
                    toolStripStatusLabel1.Text = "Write Success - Joystick Reflector";
                }
            }
        }

       

        private void BtnDescriptor_Click(object sender, EventArgs e)
        {
            if (CboDevices.SelectedIndex != -1)
            {
                //IMPORTANT turn off the callback if going so data isn't grabbed there, turn it back on later (not done here)
                devices[selecteddevice].callNever = true;

                for (int j = 0; j < devices[selecteddevice].WriteLength; j++)
                {
                    wData[j] = 0;
                }
                wData[0] = 0;
                wData[1] = 214;
                int result = 404;
                while (result == 404) { result = devices[selecteddevice].WriteData(wData); }
                if (result != 0)
                {
                    toolStripStatusLabel1.Text = "Write Fail: " + result;
                }
                else
                {
                    toolStripStatusLabel1.Text = "Write Success-Descriptor";
                }


                byte[] data = null;
                int countout = 0;
                data = new byte[80];
                data[1] = 0;
                int ret = devices[selecteddevice].BlockingReadData(ref data, 100);
                while ((ret == 0 && data[2] != 214) || ret == 304)
                {
                    if (ret == 304)
                    {
                        // Didn't get any data for 100ms, increment the countout extra
                        countout += 99;
                    }
                    countout++;
                    if (countout > 1000) //increase this if have to check more than once
                        break;
                    ret = devices[selecteddevice].BlockingReadData(ref data, 100);
                }
                listBox2.Items.Clear();
                if (data[3] == 0) listBox2.Items.Add("PID #1");
                
                listBox2.Items.Add("Keymapstart=" + data[4].ToString());
                listBox2.Items.Add("Layer2offset=" + data[5].ToString());
                listBox2.Items.Add("OutSize=" + data[6].ToString());
                listBox2.Items.Add("ReportSize=" + data[7].ToString());
                listBox2.Items.Add("MaxCol=" + data[8].ToString());
                listBox2.Items.Add("MaxRow=" + data[9].ToString());
                String ledon = "";
                if ((byte)(data[10] & 64) != 0) ledon = "Green LED ";
                if ((byte)(data[10] & 128) != 0) ledon = ledon + "Red LED ";
                if (ledon == "") ledon = "None";
                listBox2.Items.Add("LEDs=" + ledon);
                listBox2.Items.Add("Version=" + data[11].ToString());
                string temp = "PID=" + (data[13] * 256 + data[12]).ToString();
                listBox2.Items.Add(temp);
            }
        }

        private void BtnAnalogDefault_Click(object sender, EventArgs e)
        {
            //note this setting is not permanent, ie the unit will return to its default after hotplugging or reboot.
            if (CboDevices.SelectedIndex != -1)
            {

                for (int j = 0; j < devices[selecteddevice].WriteLength; j++)
                {
                    wData[j] = 0;
                }

                wData[1] = 216;
                wData[2] = 1; //Joystick: Wheel=Slid. w/ wrapping, Lever 1=X, Lever 2=Y, Switch 1=Z Rot, Switch 2=Z Axis. 

                int result = 404;
                while (result == 404) { result = devices[selecteddevice].WriteData(wData); }
                if (result != 0)
                {
                    toolStripStatusLabel1.Text = "Write Fail: " + result;
                }
                else
                {
                    toolStripStatusLabel1.Text = "Write Success - Set Ship Mode";
                }
            }
        }
        private void BtnWheelNoWrap_Click(object sender, EventArgs e)
        {
            //note this setting is not permanent, ie the unit will return to its default after hotplugging or reboot.
            if (CboDevices.SelectedIndex != -1)
            {

                for (int j = 0; j < devices[selecteddevice].WriteLength; j++)
                {
                    wData[j] = 0;
                }

                wData[1] = 216;
                wData[2] = 129; //Joystick: Wheel=Slid. w/o wrapping, Lever 1=X, Lever 2=Y, Switch 1=Z Rot, Switch 2=Z Axis. 
                // data from wheel will not wrap but instead bottom or top out until direction of wheel is reversed
                //the sensitivity determines how many turns of the wheel from one end to the other of the range.

                int result = 404;
                while (result == 404) { result = devices[selecteddevice].WriteData(wData); }
                if (result != 0)
                {
                    toolStripStatusLabel1.Text = "Write Fail: " + result;
                }
                else
                {
                    toolStripStatusLabel1.Text = "Write Success - Set Ship Mode";
                }
            }
        }
        private void BtnAnalogDataOnly_Click(object sender, EventArgs e)
        {
            //note this setting is not permanent, ie the unit will return to its default after hotplugging or reboot.
            if (CboDevices.SelectedIndex != -1)
            {

                for (int j = 0; j < devices[selecteddevice].WriteLength; j++)
                {
                    wData[j] = 0;
                }

                wData[1] = 216;
                wData[2] = 0; //splat data only

                int result = 404;
                while (result == 404) { result = devices[selecteddevice].WriteData(wData); }
                if (result != 0)
                {
                    toolStripStatusLabel1.Text = "Write Fail: " + result;
                }
                else
                {
                    toolStripStatusLabel1.Text = "Write Success - Set Ship Mode";
                }
            }
        }

        private void BtnMyJoystick_Click(object sender, EventArgs e)
        {
            //NOTE: Make sure to put the unit into Analogs Data Only in order to utilize this feature
            //This is handling all of the analog data and converting it to joystick manually using Joystick Reflector commands
            //NOTE2: Make sure to perform a calibration at least once before using this as calibrated values are used
            
            if (CboDevices.SelectedIndex == -1) return;
            //try with polling instead
            devices[selecteddevice].callNever = true;

            
            if (BtnMyJoystick.Text == "My Joystick Off")
            {
                BtnMyJoystick.Text = "My Joystick On";
                timer1.Enabled = true;
            }
            else
            {
                BtnMyJoystick.Text = "My Joystick Off";
                timer1.Enabled = false;
            }
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            byte[] rldata = null;
        
            rldata = new byte[80];
            
            int ret2 = devices[selecteddevice].ReadLast(ref rldata);
            
            //check the calibrated analog bytes, if no changes then don't send anything
            bool different = false;
            for (int i = 18; i < 22; i++)
            {
                if (pdata[i] != rldata[i])
                {
                    different = true;
                    break;
                }
            }
            //check for wheel too
            if ((pdata[11] != rldata[11]) || (pdata[12] != rldata[12])) different = true;
            if (triggerit == true)
            {
                different = true;
                triggerit = false;
            }
            if (different == true)
            {
                for (int j = 0; j < devices[selecteddevice].WriteLength; j++)
                {
                    wData[j] = 0;
                }

                wData[1] = 202; //Joystick Reflector command

                //fill in the following bytes
                //wData[2] = X
                //wData[3] = Y
                //wData[4] = Z Rotation
                //wData[5] = Z Axis 
                //wData[6] = Slider

                //in the code below each combo box has these items in this order
                //Joystick X
                //Joystick Y
                //Z Rotation
                //Z Axis
                //Slider
                //(None)

                //Wheel
                if (CboWheel.SelectedIndex != 5)
                {
                    if (ChkNoWrap.Checked == false)
                    {
                        ushort wheeli = (ushort)(rldata[11] * 256 + rldata[12]);
                        wheeli = (ushort)((wheeli >> 6)&0xff);
                        if (ChkWheel.Checked == true) wheeli = (byte)(255 - wheeli);
                        wData[CboWheel.SelectedIndex + 2] = (byte)(wheeli);
                    }
                    else
                    {
                        //use only the msb of the wheel for this
                        if (pdata[11] != rldata[11]) //don't change wheel if no change
                        {
                            sbyte sdelta = (sbyte)(rldata[11] - savelastsent);
                           
                            if (ChkWheel.Checked == true) sdelta=(sbyte) (-1*sdelta);

                            if (sdelta > 5) 
                            {
                                wheel = (wheel + (Convert.ToInt16(TxtSens.Text)));
                                if (wheel > 255) wheel = 255; //stay put
                                wData[CboWheel.SelectedIndex + 2] = (byte)(wheel ^ 0x80);
                                savelastsent = rldata[11];
                            }
                            else if (sdelta < -5) 
                            {
                                wheel = (wheel - (Convert.ToInt16(TxtSens.Text)));
                                if (wheel < 0) wheel = 0; //stay put
                                wData[CboWheel.SelectedIndex + 2] = (byte)(wheel ^ 0x80);
                                savelastsent = rldata[11];
                            }
                            else
                            {
                                wData[CboWheel.SelectedIndex + 2] = (byte)(wheel ^ 0x80);
                            }
                        }
                        else
                        {
                            wData[CboWheel.SelectedIndex + 2] = (byte)(wheel ^ 0x80);
                        }
                    }
                }

                //Lever 1
                if (CboLever1.SelectedIndex != 5)
                {
                    byte lever1 = rldata[18];
                    if (ChkLever1.Checked == true) lever1 = (byte)(255 - lever1); //flip polarity
                    wData[CboLever1.SelectedIndex + 2] = (byte)(lever1 ^ 0x80);
                   // lbljunk.Text = lever1.ToString();
                   // lbljunk3.Text = wData[CboLever1.SelectedIndex + 2].ToString();
                }
                
                //Lever 2
                if (CboLever2.SelectedIndex != 5)
                {
                    byte lever2 = rldata[19];
                    if (ChkLever2.Checked == true) lever2 = (byte)(255 - lever2); //flip polarity
                    wData[CboLever2.SelectedIndex + 2] = (byte)(lever2 ^ 0x80); 
                    
                }
                //Sw1
                if (CboSw1.SelectedIndex != 5)
                {
                    byte sw1 = rldata[20];
                    if (ChkSw1.Checked == true) sw1 = (byte)(255 - sw1); //flip polarity
                    wData[CboSw1.SelectedIndex + 2] = (byte)(sw1 ^ 0x80); 
                   
                }
                //Sw2
                if (CboSw2.SelectedIndex != 5)
                {
                    byte sw2 = rldata[21];
                    if (ChkSw2.Checked == true) sw2 = (byte)(255 - sw2); //flip polarity
                    wData[CboSw2.SelectedIndex + 2] = (byte)(sw2 ^ 0x80); 
                   
                }
                //wData[2] = xaxis 0-127=center to full right, 255-128=center to full left
                //wData[3] = yaxis -127=center to bottom, 255-128=center to top
                //wData[4] = zrot 0-127=center to bottom, 255-128=center to top
                //wData[5] = zaxis 0-127=center to bottom, 255-128=center to top
                //wData[6] = slider 0-127=center to bottom, 255-128=center to top
                //wData[7] = buttons 1-8
                //wData[8] = buttons 9-16
                //wData[9] = buttons 17-24
                //wData[10] = buttons 25-32
                //wData[12] = hat
                //System.Media.SystemSounds.Beep.Play();
                int result = 404;
                while (result == 404) { result = devices[selecteddevice].WriteData(wData); }
            }
            pdata = rldata; //save this data for comparison next time
        }

        private void BtnZeroWheel_Click(object sender, EventArgs e)
        {
            //Only applicable to Wheel in No Wrap state
            if (CboDevices.SelectedIndex != -1)
            {
                for (int j = 0; j < devices[selecteddevice].WriteLength; j++)
                {
                    wData[j] = 0;
                }

                wData[1] = 217; //0xd9

                int result = 404;
                while (result == 404) { result = devices[selecteddevice].WriteData(wData); }
                if (result != 0)
                {
                    toolStripStatusLabel1.Text = "Write Fail: " + result;
                }
                else
                {
                    toolStripStatusLabel1.Text = "Write Success - Zero Wheel";
                }
            }
        }

        private void BtnSens_Click(object sender, EventArgs e)
        {
            //sets the wheel sensitivity, how much movement of the wheel correlates to movement of the slider in the game control panel
            //note this setting is not permanent, ie the unit will return to its default after hotplugging or reboot.
            if (CboDevices.SelectedIndex != -1)
            {

                for (int j = 0; j < devices[selecteddevice].WriteLength; j++)
                {
                    wData[j] = 0;
                }

                wData[1] = 218; //0xda
                wData[2] = (byte)(Convert.ToInt16(textBox4.Text)); //default=13, the bigger the number the more turning to fill the range

                int result = 404;
                while (result == 404) { result = devices[selecteddevice].WriteData(wData); }
                if (result != 0)
                {
                    toolStripStatusLabel1.Text = "Write Fail: " + result;
                }
                else
                {
                    toolStripStatusLabel1.Text = "Write Success - Wheel Sensitivity";
                }
            }
        }

        private void BtnZero_Click(object sender, EventArgs e)
        {
                wheel = 128;
                triggerit = true;
        }

        private void BtnSpeed_Click(object sender, EventArgs e)
        {
            //sets the tiller sensitivity, how much movement of the tiller correlates to movement of the slider in the game control panel
            //note this setting is not permanent, ie the unit will return to its default after hotplugging or reboot.
            if (CboDevices.SelectedIndex != -1)
            {

                for (int j = 0; j < devices[selecteddevice].WriteLength; j++)
                {
                    wData[j] = 0;
                }

                wData[1] = 219; //0xdb
                wData[2] = (byte)(Convert.ToInt16(textBox5.Text)); //default=50, the bigger the number the less turning to fill the range

                int result = 404;
                while (result == 404) { result = devices[selecteddevice].WriteData(wData); }
                if (result != 0)
                {
                    toolStripStatusLabel1.Text = "Write Fail: " + result;
                }
                else
                {
                    toolStripStatusLabel1.Text = "Write Success - Tiller Sensitivity";
                }
            }
        }

        private void BtnMacrosOn_Click(object sender, EventArgs e)
        {
            //If there are internal macros stored, make so will be executed on key presses.
            //Unit will default back to Macros On if hotplugged or rebooted
            //These macros programmed using the hardware mode feature of the MacroWorks 3 software

            if (CboDevices.SelectedIndex != -1)
            {
                for (int j = 0; j < devices[selecteddevice].WriteLength; j++)
                {
                    wData[j] = 0;
                }
                for (int j = 0; j < devices[selecteddevice].WriteLength; j++)
                {
                    wData[j] = 0;
                }
                //enable byte is bit 7=0, bit 6=0, bit 5=0, bit 4=Splat, bit 3=Mouse (N/A), bit 2=Joy, bit 1=Keyboard, bit 0=Stored Macros
                wData[0] = 0;
                wData[1] = 0xd3;
                wData[2] = 0x17; //default 0x17, set to 17 to have stored macro played back in addition to the splat report

                int result = 404;
                while (result == 404) { result = devices[selecteddevice].WriteData(wData); }
                if (result != 0)
                {
                    toolStripStatusLabel1.Text = "Write Fail: " + result;
                }
                else
                {
                    toolStripStatusLabel1.Text = "Write Success - Macros On";
                }
            }
        }

        private void BtnMacroOff_Click(object sender, EventArgs e)
        {
            //If there are internal macros stored, make so will not be executed on key presses.
            //Unit will default back to Macros On if hotplugged or rebooted
            //These macros programmed using the hardware mode feature of the MacroWorks 3 software

            if (CboDevices.SelectedIndex != -1)
            {
                for (int j = 0; j < devices[selecteddevice].WriteLength; j++)
                {
                    wData[j] = 0;
                }
                for (int j = 0; j < devices[selecteddevice].WriteLength; j++)
                {
                    wData[j] = 0;
                }
                //enable byte is bit 7=0, bit 6=0, bit 5=0, bit 4=Splat, bit 3=Mouse (N/A), bit 2=Joy, bit 1=Keyboard, bit 0=Stored Macros
                wData[0] = 0;
                wData[1] = 0xd3;
                wData[2] = 0x16; //default 0x17, set to 16 to have stored macro not executed on key presses

                int result = 404;
                while (result == 404) { result = devices[selecteddevice].WriteData(wData); }
                if (result != 0)
                {
                    toolStripStatusLabel1.Text = "Write Fail: " + result;
                }
                else
                {
                    toolStripStatusLabel1.Text = "Write Success - Macros Off";
                }
            }
        }

        private void BtnDeadzone_Click(object sender, EventArgs e)
        {
            //Sets the sensitivity of the "deadzone" of the switched.  0 means no deadzone in the middle, higher number are a wider deadzone.
            //note this setting is not permanent, ie the unit will return to its default after hotplugging or reboot.
            if (CboDevices.SelectedIndex != -1)
            {

                for (int j = 0; j < devices[selecteddevice].WriteLength; j++)
                {
                    wData[j] = 0;
                }
                int sw2dz = Convert.ToInt32(textBox7.Text); //5000 default
                int sw1dz = Convert.ToInt32(textBox6.Text); //5000 default

                wData[1] = 190; //0xbe
                wData[2] = (Byte)((Byte)sw2dz & 0xff);  //low byte top switch
                wData[3] = (Byte)(((Byte)(sw2dz >> 8)) & 0xff); //high byte top switch
                wData[4] = (Byte)((Byte)sw1dz & 0xff); //low byte bottom switch
                wData[5] = (Byte)(((Byte)(sw1dz >> 8)) & 0xff); //high byte bottom switch

                int result = 404;
                while (result == 404) { result = devices[selecteddevice].WriteData(wData); }
                if (result != 0)
                {
                    toolStripStatusLabel1.Text = "Write Fail: " + result;
                }
                else
                {
                    toolStripStatusLabel1.Text = "Write Success - Deadzone";
                }
            }
        }

        private void label35_Click(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void label34_Click(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }
        

        

        

        

        

      

      

     

     

        
       
       
    }
    
    
}
