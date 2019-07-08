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
       

        long saveabsolutetime;  //for timestamp demo
        //for thread-safe way to call a Windows Forms control
        // This delegate enables asynchronous calls for setting
        // the text property on a TextBox control.
        delegate void SetTextCallback(string text);
        Control c;
        
        //end thread-safe

       
        bool mouseon = false;
        byte lastval3 = 0;     
       
        public Form1()
        {
            InitializeComponent();
        }

        //data callback    
        public void HandlePIEHidData(Byte[] data, PIEDevice sourceDevice, int error)
        {

            //check the sourceDevice and make sure it is the same device as selected in CboDevice   
            if (sourceDevice == devices[selecteddevice])
            {
                
                    //check the switch byte 
                byte val2 = (byte)(data[2] & 1);
                if (val2 == 0)
                {
                    c = this.LblSwitchPos;
                    this.SetText("switch up");
                }
                else
                {
                    c = this.LblSwitchPos;
                    this.SetText("switch down");
                }
                //read the unit ID
                c = this.LblUnitID;
                this.SetText(data[1].ToString());

                //write raw data to listbox1
                String output = "Callback: " + sourceDevice.Pid + ", ID: " + selecteddevice.ToString() + ", data=";
                for (int i = 0; i < sourceDevice.ReadLength; i++)
                {
                    output = output + BinToHex(data[i]) + " ";
                }
                this.SetListBox(output);

                //time stamp info 4 bytes
                long absolutetime = 16777216 * data[14] + 65536 * data[15] + 256 * data[16] + data[17];  //ms
                long absolutetime2 = absolutetime / 1000; //seconds
                c = this.label19;
                this.SetText("absolute time: " + absolutetime2.ToString() + " s");
                long deltatime = absolutetime - saveabsolutetime;
                c = this.label20;
                this.SetText("delta time: " + deltatime + " ms");
                saveabsolutetime = absolutetime;

                c = label16;
                SetText("Touch Pad");
                c = TxtZ;
                SetText(data[7].ToString());
                sbyte deltax = (sbyte)data[8];
                c = TxtX;
                SetText(deltax.ToString());
                sbyte deltay = (sbyte)data[9];
                c = TxtY;
                SetText(deltay.ToString());
                c = LblX;
                SetText("dX:");
                c = LblY;
                SetText("dY:");
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
        //for threadsafe setting of Windows Forms control
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
        
        private void Form1_Load(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "";
            
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            //closeinterfaces on all devices that have been setup (SetupInterface called)
            for (int i = 0; i < CboDevices.Items.Count; i++)
            {
                //if devices[].Connected=false don't call CloseInterface
                devices[cbotodevice[i]].CloseInterface();

            }
            System.Environment.Exit(0);
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

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
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
                            case 1074:
                                CboDevices.Items.Add("XK-12 Touch (" + devices[i].Pid + "=PID #1), ID: " + i);
                                cbotodevice[cbocount] = i;
                                cbocount++;
                                break;
                            case 1075:
                                CboDevices.Items.Add("XK-12 Touch (" + devices[i].Pid + "), ID: " + i);
                                cbotodevice[cbocount] = i;
                                cbocount++;
                                break;
                            case 1076:
                                CboDevices.Items.Add("XK-12 Touch (" + devices[i].Pid + "=PID #2), ID: " + i);
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
                        devices[i].suppressDuplicateReports = ChkSuppress.Checked;
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

        private void BtnUnitID_Click(object sender, EventArgs e)
        {
            //Write Unit ID to the device
            if (CboDevices.SelectedIndex != -1) //do nothing if not enumerated
            {

                //write Unit ID given in the TxtSetUnitID box
                for (int j = 0; j < devices[selecteddevice].WriteLength; j++)
                {
                    wData[j] = 0;
                }

                wData[0] = 0;
                wData[1] = 189;
                wData[2] = (byte)(Convert.ToInt16(TxtSetUnitID.Text));

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

        private void BtnBL_Click(object sender, EventArgs e)
        {
           
            if (CboDevices.SelectedIndex != -1) //do nothing if not enumerated
            {
                for (int j = 0; j < devices[selecteddevice].WriteLength; j++) 
                {
                    wData[j] = 0;
                }
               
                wData[0] = 0;
                wData[1] = 187;
                wData[2] = 127; //0-255 for brightness of bank 1 backlights
                wData[3] = 64; //0-255 for brightness of bank 2 backlights

                int result = 404;
                while (result == 404) { result = devices[selecteddevice].WriteData(wData); }

                if (result != 0)
                {
                    toolStripStatusLabel1.Text = "Write Fail: " + result;
                }
                else
                {
                    toolStripStatusLabel1.Text = "Write Success-Backlighting Intensity";
                }
            }
        }
        private void BtnBLToggle_Click(object sender, EventArgs e)
        {
            //Sending this command toggles the backlights
            if (CboDevices.SelectedIndex != -1) //do nothing if not enumerated
            {
                for (int j = 0; j < devices[selecteddevice].WriteLength; j++)
                {
                    wData[j] = 0;
                }

                wData[0] = 0;
                wData[1] = 184;

                int result = 404;
                while (result == 404) { result = devices[selecteddevice].WriteData(wData); }

                if (result != 0)
                {
                    toolStripStatusLabel1.Text = "Write Fail: " + result;
                }
                else
                {
                    toolStripStatusLabel1.Text = "Write Success-Toggle BL";
                }
            }
        }

        private void ChkScrollLock_CheckedChanged(object sender, EventArgs e)
        {
            //If checked then the Scroll Lock key on the main keyboard will toggle the backlights
            if (CboDevices.SelectedIndex != -1) //do nothing if not enumerated
            {
                for (int j = 0; j < devices[selecteddevice].WriteLength; j++)
                {
                    wData[j] = 0;
                }
                byte sl = 0;
                if (ChkScrollLock.Checked == true) sl = 128; //set last bit
                wData[0] = 0;
                wData[1] = 183;
                wData[2] = (byte)sl; //0=disable scroll lock toggle, 128=endable scroll lock toggle

                int result = 404;
                while (result == 404) { result = devices[selecteddevice].WriteData(wData); }

                if (result != 0)
                {
                    toolStripStatusLabel1.Text = "Write Fail: " + result;
                }
                else
                {
                    toolStripStatusLabel1.Text = "Write Success-Scroll Lock Enable/Disable";
                }
            }
        }

        private void ChkGreenOnOff_CheckedChanged(object sender, EventArgs e)
        {
            //Turns on or off, depending on value of ChkGreenOnOff, ALL bank 1 BLs using current intensity
            if (CboDevices.SelectedIndex != -1) //do nothing if not enumerated
            {
                byte sl = 0;
                
                if (ChkGreenOnOff.Checked == true) sl = 255;
                for (int j = 0; j < devices[selecteddevice].WriteLength; j++)
                {
                    wData[j] = 0;
                }

                wData[0] = 0;
                wData[1] = 182; //0xb6
                wData[2] = 0;  //0 for bank 1, 1 for bank 2
                wData[3] = (byte)sl;  //OR turn individual rows on or off using bits.  1st bit=row 1, 2nd bit=row 2, 3rd bit =row 3

                int result = 404;
                while (result == 404) { result = devices[selecteddevice].WriteData(wData); }

                if (result != 0)
                {
                    toolStripStatusLabel1.Text = "Write Fail: " + result;
                }
                else
                {
                    toolStripStatusLabel1.Text = "Write Success-All bank 1 BL on/off";
                }
            }
        }

        private void ChkRedOnOff_CheckedChanged(object sender, EventArgs e)
        {
            //Turns on or off, depending on value of ChkRedOnOff, ALL bank 2 BLs using current intensity
            if (CboDevices.SelectedIndex != -1) //do nothing if not enumerated
            {
                byte sl = 0;
              
                if (ChkRedOnOff.Checked == true) sl = 255;
                for (int j = 0; j < devices[selecteddevice].WriteLength; j++)
                {
                    wData[j] = 0;
                }

                wData[0] = 0;
                wData[1] = 182; //0xb6
                wData[2] = 1;  //0 for bank 1, 1 for bank 2
                wData[3] = (byte)sl; //OR turn individual rows on or off using bits.  1st bit=row 1, 2nd bit=row 2, 3rd bit =row 3

                int result = 404;
                while (result == 404) { result = devices[selecteddevice].WriteData(wData); }

                if (result != 0)
                {
                    toolStripStatusLabel1.Text = "Write Fail: " + result;
                }
                else
                {
                    toolStripStatusLabel1.Text = "Write Success-All bank 2 BL on/off";
                }
            }
        }

        private void BtnSetFlash_Click(object sender, EventArgs e)
        {
            //Sets the frequency of flashing for both the LEDs and backlighting
            if (CboDevices.SelectedIndex != -1) //do nothing if not enumerated
            {

                
                for (int j = 0; j < devices[selecteddevice].WriteLength; j++)
                {
                    wData[j] = 0;
                }

                wData[0] = 0;
                wData[1] = 180; // 0xb4
                wData[2] = (byte)(Convert.ToInt16(TxtFlashFreq.Text));

                int result = 404;
                while (result == 404) { result = devices[selecteddevice].WriteData(wData); }

                if (result != 0)
                {
                    toolStripStatusLabel1.Text = "Write Fail: " + result;
                }
                else
                {
                    toolStripStatusLabel1.Text = "Write Success - Set Flash Frequency";
                }
            }
        }

        private void BtnFlashBL_Click(object sender, EventArgs e)
        {
           
        }

        private void BtnStopFlashBL_Click(object sender, EventArgs e)
        {
           
        }

        private void BtnSaveBL_Click(object sender, EventArgs e)
        {
            //Write current state of backlighting to EEPROM.  
            //NOTE: Is it not recommended to do this frequently as there are a finite number of writes to the EEPROM allowed
            if (CboDevices.SelectedIndex != -1)
            {

                for (int j = 0; j < devices[selecteddevice].WriteLength; j++)
                {
                    wData[j] = 0;
                }
                wData[0] = 0;
                wData[1] = 199;
                wData[2] = 1; //anything other than 0 will save bl state to eeprom, default is 0

                int result = 404;
                while (result == 404) { result = devices[selecteddevice].WriteData(wData); }
                
                if (result != 0)
                {
                    toolStripStatusLabel1.Text = "Write Fail: " + result;
                }
                else
                {
                    toolStripStatusLabel1.Text = "Write Success - Save Backlight to EEPROM";
                }
            }
        }

        private void BtnTimeStamp_Click(object sender, EventArgs e)
        {
            //Sending this command will turn off the 4 bytes of data which assembled give the time in ms from the start of the computer
            if (CboDevices.SelectedIndex != -1) //do nothing if not enumerated
            {
                for (int j = 0; j < devices[selecteddevice].WriteLength; j++)
                {
                    wData[j] = 0;
                }
                wData[0] = 0;
                wData[1] = 210;
                wData[2] = 0;

                int result = 404;
                while (result == 404) { result = devices[selecteddevice].WriteData(wData); }

                if (result != 0)
                {
                    toolStripStatusLabel1.Text = "Write Fail: " + result;
                }
                else
                {
                    toolStripStatusLabel1.Text = "Write Success-Time Stamp";
                }
            }
        }

        private void BtnTimeStampOn_Click(object sender, EventArgs e)
        {
            //Sending this command will turn on the 4 bytes of data which assembled give the time in ms from the start of the computer
            if (CboDevices.SelectedIndex != -1) //do nothing if not enumerated
            {
                for (int j = 0; j < devices[selecteddevice].WriteLength; j++)
                {
                    wData[j] = 0;
                }
                wData[0] = 0;
                wData[1] = 210;
                wData[2] = 1;  //default ON

                int result = 404;
                while (result == 404) { result = devices[selecteddevice].WriteData(wData); }

                if (result != 0)
                {
                    toolStripStatusLabel1.Text = "Write Fail: " + result;
                }
                else
                {
                    toolStripStatusLabel1.Text = "Write Success-Time Stamp";
                }
            }
        }

        private void BtnKBreflect_Click(object sender, EventArgs e)
        {
            //Sends native keyboard messages
            //Write some keys to the textbox, should be Abcd
            //send some hid codes to the textbox, these will be coming in on the native keyboard endpoint
            if (CboDevices.SelectedIndex != -1) //do nothing if not enumerated
            {
                int result;
                textBox1.Focus();
                for (int j = 0; j < devices[selecteddevice].WriteLength; j++)
                {
                    wData[j] = 0;
                }
                wData[0] = 0;
                wData[1] = 201;

                wData[2] = 2;       //modifiers
                wData[3] = 0;       //always 0
                wData[4] = 0x04;    //hid code = a down
                wData[5] = 0;
                wData[6] = 0;
                wData[7] = 0;
                wData[8] = 0;
                wData[9] = 0;
                
                result = 404;
                while (result == 404) { result = devices[selecteddevice].WriteData(wData); }

                wData[2] = 0;       //modifiers
                wData[3] = 0;       //always 0
                wData[4] = 0;    //hid code = a up
                wData[5] = 0x05;    //hid code = b down
                wData[6] = 0x06;    //hid code = c down
                wData[7] = 0x07;    //hid code = d down
                wData[8] = 0;
                wData[9] = 0;
               
                result = 404;
                while (result == 404) { result = devices[selecteddevice].WriteData(wData); }

                wData[2] = 0;
                wData[4] = 0;
                wData[5] = 0;  //b up
                wData[6] = 0;  //c up
                wData[7] = 0;  //d up
                wData[8] = 0;
                wData[9] = 0;
                
                result = 404;
                while (result == 404) { result = devices[selecteddevice].WriteData(wData); }

                //using this method in real application if want to exactly duplicate down and up strokes would
                //probably do one key at a time.
                //response seems good but maybe some machines may need a sleep between writes??

            }
        }

        private void BtnJoyreflect_Click(object sender, EventArgs e)
        {
            //Sends native joystick messages
            //Open up the game controller control panel to test these features, after clicking this button
            //go and make active the control panel properties and change will be seen
            if (CboDevices.SelectedIndex != -1) //do nothing if not enumerated
            {
                int result;
                for (int j = 0; j < devices[selecteddevice].WriteLength; j++)
                {
                    wData[j] = 0;
                }
                wData[0] = 0;
                wData[1] = 202;    //0xca
                wData[2] = (byte)Math.Abs((Convert.ToByte(textBox2.Text) ^ 127) - 255);  //X, in raw form 0 to 127 from center to right, 255 to 128 from center to left but I like to use 0-255 where 0 is max left, 255 is max right
                wData[3] = (byte)(Convert.ToByte(textBox3.Text) ^ 127); //Y, raw data 0 to 127 from center down, 255 to 128 from center up, I convert so I can enter 0-255
                wData[4] = (byte)(Convert.ToByte(textBox12.Text) ^ 127); //Z rotation, raw data 0 to 127 from center down, 255 to 128 from center up, I convert so I can enter 0-255
                wData[5] = (byte)(Convert.ToByte(textBox4.Text) ^ 127); //Z, raw data 0 to 127 from center down, 255 to 128 from center up, I convert so I can enter 0-255
                wData[6] = (byte)(Convert.ToByte(textBox13.Text) ^ 127); //Slider rotation, raw data 0 to 127 from center down, 255 to 128 from center up, I convert so I can enter 0-255

                wData[7] = Convert.ToByte(textBox5.Text); //buttons 1-8, where bit 1 is button 1, bit 1 is button 2, etc.
                wData[8] = Convert.ToByte(textBox7.Text); //buttons 9-16
                wData[9] = Convert.ToByte(textBox8.Text); //buttons 17-24
                wData[10] = Convert.ToByte(textBox9.Text); //buttons 25-32

                wData[11] = 0;

                wData[12] = Convert.ToByte(textBox6.Text); //hat, where 0 is straight up, 1 is 45deg cw, etc and 8 is no hat

                result = 404;
                while (result == 404) { result = devices[selecteddevice].WriteData(wData); }
                
                if (result != 0)
                {
                    toolStripStatusLabel1.Text = "Write Fail: " + result;
                }
                else
                {
                    toolStripStatusLabel1.Text = "Write Success - joystick reflector";
                }
            }
        }

        private void BtnDescriptor_Click(object sender, EventArgs e)
        {
            //Sending the command will make the device return information about it
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
                else if (data[3] == 2) listBox2.Items.Add("PID #2");
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

        private void ChkFGreenLED_CheckedChanged(object sender, EventArgs e)
        {
            //use the Set Flash Freq to control frequency of blink
            if (CboDevices.SelectedIndex != -1)
            {

                for (int j = 0; j < devices[selecteddevice].WriteLength; j++)
                {
                    wData[j] = 0;
                }
                wData[1] = 179; //0xb3
                wData[2] = 6; //6 for green, 7 for red

                if (ChkFGreenLED.Checked == true)
                {
                    wData[3] = 2; //0=off, 1=on, 2=flash
                }
                else
                {
                    if (ChkGreenLED.Checked == true)
                    {
                        wData[3] = 1; //0=off, 1=on, 2=flash
                    }
                    else
                    {
                        wData[3] = 0; //0=off, 1=on, 2=flash
                    }
                }

                int result = 404;
                while (result == 404) { result = devices[selecteddevice].WriteData(wData); }

                if (result != 0)
                {
                    toolStripStatusLabel1.Text = "Write Fail: " + result;
                }
                else
                {
                    toolStripStatusLabel1.Text = "Write Success - Flash LEDs";
                }
            }
        }

        private void ChkFRedLED_CheckedChanged(object sender, EventArgs e)
        {
            //use the Set Flash Freq to control frequency of blink
            if (CboDevices.SelectedIndex != -1)
            {

                for (int j = 0; j < devices[selecteddevice].WriteLength; j++)
                {
                    wData[j] = 0;
                }
                wData[1] = 179; //0xb3
                wData[2] = 7; //6 for green, 7 for red

                if (ChkFRedLED.Checked == true)
                {
                    wData[3] = 2; //0=off, 1=on, 2=flash
                }
                else
                {
                    if (ChkRedLED.Checked == true)
                    {
                        wData[3] = 1; //0=off, 1=on, 2=flash
                    }
                    else
                    {
                        wData[3] = 0; //0=off, 1=on, 2=flash
                    }
                }

                int result = 404;
                while (result == 404) { result = devices[selecteddevice].WriteData(wData); }

                if (result != 0)
                {
                    toolStripStatusLabel1.Text = "Write Fail: " + result;
                }
                else
                {
                    toolStripStatusLabel1.Text = "Write Success - Flash LEDs";
                }
            }
        }

        private void ChkGreenLED_CheckedChanged(object sender, EventArgs e)
        {
          
            if (CboDevices.SelectedIndex != -1)
            {

                for (int j = 0; j < devices[selecteddevice].WriteLength; j++)
                {
                    wData[j] = 0;
                }
                wData[1] = 179; //0xb3
                wData[2] = 6; //6 for green, 7 for red

            
                if (ChkGreenLED.Checked == true)
                {
                    wData[3] = 1; //0=off, 1=on, 2=flash
                }
                else
                {
                    wData[3] = 0; //0=off, 1=on, 2=flash
                }

                int result = 404;
                while (result == 404) { result = devices[selecteddevice].WriteData(wData); }

                if (result != 0)
                {
                    toolStripStatusLabel1.Text = "Write Fail: " + result;
                }
                else
                {
                    toolStripStatusLabel1.Text = "Write Success - Set LED";
                }
            }
        }

        private void ChkRedLED_CheckedChanged(object sender, EventArgs e)
        {
            if (CboDevices.SelectedIndex != -1)
            {

                for (int j = 0; j < devices[selecteddevice].WriteLength; j++)
                {
                    wData[j] = 0;
                }
                wData[1] = 179; //0xb3
                wData[2] = 7; //6 for green, 7 for red


                if (ChkRedLED.Checked == true)
                {
                    wData[3] = 1; //0=off, 1=on, 2=flash
                }
                else
                {
                    wData[3] = 0; //0=off, 1=on, 2=flash
                }

                int result = 404;
                while (result == 404) { result = devices[selecteddevice].WriteData(wData); }

                if (result != 0)
                {
                    toolStripStatusLabel1.Text = "Write Fail: " + result;
                }
                else
                {
                    toolStripStatusLabel1.Text = "Write Success - Set LED";
                }
            }
        }

        private void BtnGetDataNow_Click(object sender, EventArgs e)
        {
            //After sending this command a general incoming data report will be given with
            //the 3rd byte (Data Type) 2nd bit set.  If program switch is up byte 3 will be 2
            //and if it is pressed byte 3 will be 3.  This is useful for getting the initial state
            //or unit id of the device before it sends any data.
            if (CboDevices.SelectedIndex != -1) //do nothing if not enumerated
            {
                devices[selecteddevice].callNever = false; //turn on callback
                //write Unit ID given in the TxtSetUnitID box
                for (int j = 0; j < devices[selecteddevice].WriteLength; j++)
                {
                    wData[j] = 0;
                }

                wData[0] = 0;
                wData[1] = 177; //0xb1

                int result = 404;
                while (result == 404) { result = devices[selecteddevice].WriteData(wData); }

                if (result != 0)
                {
                    toolStripStatusLabel1.Text = "Write Fail: " + result;
                }
                else
                {
                    toolStripStatusLabel1.Text = "Write Success - General Data";
                }
            }
        }

        

        private void ChkBLOnOff_CheckedChanged(object sender, EventArgs e)
        {
            //Use the Set Flash Freq to control frequency of blink
            //Key Index for XK-12 Touch (in decimal)
            //Bank 1
            //Columns-->
            //  0   8   16  24
            //  1   9   17  25
            //  2   10  18  26

            //Bank 1
            //Columns-->
            //  32   40   48  56
            //  33   41   49  57
            //  34   42   50  58
           

            if (CboDevices.SelectedIndex != -1)
            {
                //first get selected index
                string sindex = TxtBLIndex.Text;
                int iindex;
                iindex = Convert.ToInt16(sindex);
               

                for (int j = 0; j < devices[selecteddevice].WriteLength; j++)
                {
                    wData[j] = 0;
                }
                //now get state
                int state = 0;
                if (ChkBLOnOff.Checked == true)
                {
                    if (ChkFlash.Checked == true) state = 2;
                    else state = 1;
                }

                wData[1] = 181; //b5
                wData[2] = (byte)(iindex); //Key Index
                wData[3] = (byte)state; //0=off, 1=on, 2=flash

                int result = 404;
                while (result == 404) { result = devices[selecteddevice].WriteData(wData); }

                if (result != 0)
                {
                    toolStripStatusLabel1.Text = "Write Fail: " + result;
                }
                else
                {
                    toolStripStatusLabel1.Text = "Write Success - Flash BL";
                }
            }
        }

        private void ChkFlash_CheckedChanged(object sender, EventArgs e)
        {
            //Use the Set Flash Freq to control frequency of blink
            //Key Index for XK-24 (in decimal)
            //Bank 1
            //Columns-->
            //  0   8   16  24
            //  1   9   17  25
            //  2   10  18  26

            //Bank 1
            //Columns-->
            //  32   40   48  56
            //  33   41   49  57
            //  34   42   50  58

            if (CboDevices.SelectedIndex != -1)
            {
                //first get selected index
                string sindex = TxtBLIndex.Text;
                int iindex;
                iindex = Convert.ToInt16(sindex);
               

                for (int j = 0; j < devices[selecteddevice].WriteLength; j++)
                {
                    wData[j] = 0;
                }
                //now get state
                int state = 0;
                if (ChkFlash.Checked == true)
                {
                    state = 2;
                }
                else
                {
                    if (ChkBLOnOff.Checked == true)
                    {
                        state = 1;
                    }
                }

                wData[1] = 181; //b5
                wData[2] = (byte)(iindex); //Key Index
                wData[3] = (byte)state; //0=off, 1=on, 2=flash

                int result = 404;
                while (result == 404) { result = devices[selecteddevice].WriteData(wData); }

                if (result != 0)
                {
                    toolStripStatusLabel1.Text = "Write Fail: " + result;
                }
                else
                {
                    toolStripStatusLabel1.Text = "Write Success - Flash BL";
                }
            }
        }

        private void CboBL_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

      

    

        private void BtnPID1_Click(object sender, EventArgs e)
        {

            if (CboDevices.SelectedIndex != -1) //do nothing if not enumerated
            {

                for (int j = 0; j < devices[selecteddevice].WriteLength; j++)
                {
                    wData[j] = 0;
                }
                wData[0] = 0;
                wData[1] = 204;
                wData[2] = 0;  //0=pid #1, 2=pid #2

                int result = 404;
                while (result == 404) { result = devices[selecteddevice].WriteData(wData); }

                if (result != 0)
                {
                    toolStripStatusLabel1.Text = "Write Fail: " + result;

                }
                else
                {
                    toolStripStatusLabel1.Text = "Write Success - to PID #1";
                }

            }
        }

        private void BtnPID2_Click(object sender, EventArgs e)
        {
            if (CboDevices.SelectedIndex != -1) //do nothing if not enumerated
            {

                for (int j = 0; j < devices[selecteddevice].WriteLength; j++)
                {
                    wData[j] = 0;
                }
                wData[0] = 0;
                wData[1] = 204;
                wData[2] = 2;  //0=pid #1, 2=pid #2

                int result = 404;
                while (result == 404) { result = devices[selecteddevice].WriteData(wData); }

                if (result != 0)
                {
                    toolStripStatusLabel1.Text = "Write Fail: " + result;
                }
                else
                {
                    toolStripStatusLabel1.Text = "Write Success - to PID #2";
                }

            }
        }

        private void BtnMousereflect_Click(object sender, EventArgs e)
        {
            if (CboDevices.SelectedIndex != -1) //do nothing if not enumerated
            {
                wData[0] = 0;
                wData[1] = 203;    //0xcb
                wData[2] = Convert.ToByte(TxtMouseButton.Text); //Buttons; 1=Left, 2=Right, 4=Center, 8=XButton1, 16=XButton2
                wData[3] = Convert.ToByte(TxtMouseX.Text); //Mouse X motion. 128=0 no motion, 1-127 is right, 255-129=left, finest inc (1 and 255) to coarsest (127 and 129).
                wData[4] = Convert.ToByte(TxtMouseY.Text); //Mouse Y motion. 128=0 no motion, 1-127 is down, 255-129=up, finest inc (1 and 255) to coarsest (127 and 129).
                wData[5] = Convert.ToByte(TxtMouseWheelX.Text);//Wheel X. 128=0 no motion, 1-127 is up, 255-129=down, finest inc (1 and 255) to coarsest (127 and 129).
                wData[6] = Convert.ToByte(TxtMouseWheel.Text);//Wheel Y. 128=0 no motion, 1-127 is up, 255-129=down, finest inc (1 and 255) to coarsest (127 and 129).
                
                int result = 404;
                while (result == 404) { result = devices[selecteddevice].WriteData(wData); }

                //now send all 0s
                wData[0] = 0;
                wData[1] = 203;    //0xcb
                wData[2] = 0; //buttons
                wData[3] = 0; //X
                wData[4] = 0; //Y
                wData[5] = 0; //wheel X
                wData[6] = 0; //wheel Y

                result = 404;
                while (result == 404) { result = devices[selecteddevice].WriteData(wData); }
            }
        }

    

        private void BtnEnable_Click(object sender, EventArgs e)
        {
            if (CboDevices.SelectedIndex != -1)
            {

                for (int j = 0; j < devices[selecteddevice].WriteLength; j++)
                {
                    wData[j] = 0;
                }
                wData[1] = 217; //0xd9
                //or 3
                wData[2] = 3;
                byte suppress = 0;
                byte flip = 0;
                byte enabled = 0;  //4 to enable, 0 to disable

                if (ChkFlip.Checked == true) flip = 1; //1st bit invert coordinates
                if (ChkSuppressClick.Checked == true) suppress = 2; //2nd bit suppress click
                if (ChkEnable.Checked == true) enabled = 4; //3rd bit suppress click

                wData[3] = (byte)(flip | suppress | enabled); 

                int result = devices[selecteddevice].WriteData(wData);
                if (result != 0)
                {
                    toolStripStatusLabel1.Text = "Write Fail: " + result;
                }
                else
                {
                    toolStripStatusLabel1.Text = "Write Success - Touchpad Options";
                }
            }
        }

      
        public static String BinToHex(Byte value)
        {
            StringBuilder sb = new StringBuilder("");
            sb.Append(value.ToString("X2"));  //the 2 means 2 digits
            return sb.ToString();
        }

        private void RdNMouse_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void BtnCustom_Click(object sender, EventArgs e)
        {
            //This report available only on v10 firmware and above
            //After sending this command a custom incoming data report will be given with
            //the 3rd byte (Data Type) set to 0xE0, the 4th byte set to the count given below when the command was sent
            //and the following bytes whatever the user wishes.  In this example we are send 3 bytes; 1, 2, 3

            if (CboDevices.SelectedIndex != -1) //do nothing if not enumerated
            {
                devices[selecteddevice].callNever = false; //turn on callback if it was turned off
                for (int j = 0; j < devices[selecteddevice].WriteLength; j++)
                {
                    wData[j] = 0;
                }

                wData[0] = 0;
                wData[1] = 224; //0xe0
                wData[2] = 3; //count of bytes to follow
                wData[3] = 1; //1st custom byte
                wData[4] = 2; //2nd custom byte
                wData[5] = 3; //3rd custom byte

                int result = devices[selecteddevice].WriteData(wData);
                if (result != 0)
                {
                    toolStripStatusLabel1.Text = "Write Fail: " + result;
                }
                else
                {
                    toolStripStatusLabel1.Text = "Write Success - Custom Data";
                }
            }
        }
        
     
        

        
 
       
    }
    
    
}
