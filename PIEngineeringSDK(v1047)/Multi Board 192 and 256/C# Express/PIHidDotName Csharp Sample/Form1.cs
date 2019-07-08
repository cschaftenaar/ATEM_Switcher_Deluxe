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

        int saveledstate;
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
                    //read the unit ID
                    c = this.LblUnitID;
                    this.SetText(data[1].ToString());

                    //write raw data to listbox1 in Hex
                    String output = "Callback: " + sourceDevice.Pid + ", ID: " + selecteddevice.ToString() + ", data=";
                    for (int i = 0; i < sourceDevice.ReadLength; i++)
                    {
                        output = output +BinToHex(data[i]) + " ";
                        

                    }
                    this.SetListBox(output);

                    
                    

                    //time stamp info 4 bytes
                    long absolutetime = 0;
                    if (sourceDevice.Pid == 1040 || sourceDevice.Pid == 1042)
                    {
                        absolutetime = 16777216 * data[27] + 65536 * data[28] + 256 * data[29] + data[30];  //ms
                    }
                    else if (sourceDevice.Pid == 1043 || sourceDevice.Pid == 1045)
                    {
                        absolutetime = 16777216 * data[35] + 65536 * data[36] + 256 * data[37] + data[38];  //ms
                    }
                    long absolutetime2 = absolutetime / 1000; //seconds
                    c = this.label19;
                    this.SetText("absolute time: " + absolutetime2.ToString() + " s");
                    long deltatime = absolutetime - saveabsolutetime;
                    c = this.label20;
                    this.SetText("delta time: " + deltatime + " ms");
                    saveabsolutetime = absolutetime;
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

        private void BtnEnumerate_Click(object sender, EventArgs e)
        {
            CboDevices.Items.Clear();
            cbotodevice = new int[128]; //128=max # of devices
            //enumerate and setupinterfaces for all devices
            devices = PIEHid32Net.PIEDevice.EnumeratePIE();
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
                            case 1040:
                                CboDevices.Items.Add("X-keys Multi Board 192 ("+devices[i].Pid+"=PID #1), ID: " + i);
                                cbotodevice[cbocount] = i;
                                cbocount++;
                                break;
                            case 1041:
                                CboDevices.Items.Add("X-keys Multi Board 192 (" + devices[i].Pid + "), ID: " + i);
                                cbotodevice[cbocount] = i;
                                cbocount++;
                                break;
                            case 1042:
                                CboDevices.Items.Add("X-keys Multi Board 192 (" + devices[i].Pid + "=PID #2), ID: " + i);
                                cbotodevice[cbocount] = i;
                                cbocount++;
                                break;
                            case 1043:
                                CboDevices.Items.Add("X-keys Multi Board 256 (" + devices[i].Pid + "=PID #1), ID: " + i);
                                cbotodevice[cbocount] = i;
                                cbocount++;
                                break;
                            case 1044:
                                CboDevices.Items.Add("X-keys Multi Board 256 (" + devices[i].Pid + "), ID: " + i);
                                cbotodevice[cbocount] = i;
                                cbocount++;
                                break;
                            case 1045:
                                CboDevices.Items.Add("X-keys Multi Board 256 (" + devices[i].Pid + "=PID #2), ID: " + i);
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
            BtnNoLEDS_Click(this, null);  //start with no leds
           
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            //closeinterfaces on all devices
            for (int i = 0; i < CboDevices.Items.Count; i++)
            {
                devices[cbotodevice[i]].CloseInterface();
            }
            System.Environment.Exit(0);
        }

        
        private void BtnNoLEDS_Click(object sender, EventArgs e)
        {
            //write to currently selected device in CboDevice - turn off all LEDs
            if (CboDevices.SelectedIndex != -1)
            {
                
                for (int j = 0; j < devices[selecteddevice].WriteLength; j++)  //clear out the LED byte
                {
                    wData[j] = 0;
                }
                
                wData[1] = 186;
                wData[2] = 0;
                saveledstate = wData[2];

                int result = 404;
                while (result == 404) { result = devices[selecteddevice].WriteData(wData); }

                if (result != 0)
                {
                    toolStripStatusLabel1.Text = "Write Fail: " + result;
                }
                else
                {
                    toolStripStatusLabel1.Text = "Write Success - No LEDs";
                }
            }
        }

        private void BtnGreen_Click(object sender, EventArgs e)
        {
            //write to currently selected device - turn off or on the NumsLock, CapsLock, ScrollLock LED control so user can control
            if (CboDevices.SelectedIndex != -1)
            {
                
                for (int j = 0; j < devices[selecteddevice].WriteLength; j++) 
                {
                    wData[j] = 0;
                }

                wData[1] = 184;
                
                if (BtnGreen.Text == "Disable Keyboard Control")
                {
                    wData[2] = 0;
                    BtnGreen.Text = "Enable Keyboard Control";
                }
                else
                {
                    wData[2] = 1;
                    BtnGreen.Text = "Disable Keyboard Control";

                }

                int result = 404;
                while (result == 404) { result = devices[selecteddevice].WriteData(wData); }

                if (result != 0)
                {
                    toolStripStatusLabel1.Text = "Write Fail: " + result;
                }
                else
                {
                    toolStripStatusLabel1.Text = "Write Success - Keyboard LED Control";
                }
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "";
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
                wData[1] = 0xbd;
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
       
        public static String BinToHex(Byte value)
        {
            StringBuilder sb =new StringBuilder("");
            sb.Append(value.ToString("X2"));  //the 2 means 2 digits
            return sb.ToString();
        }
        public static Byte HexToBin(String value)
        {
            String addup = "0x" + value;
            return (Byte) Convert.ToInt32(value,16);
        }
     

        private void BtnKBreflect_Click(object sender, EventArgs e)
        {
            //write some keys to the textbox, should be Abcd
            //write to currently selected device - turn on the Red LED
            if (CboDevices.SelectedIndex != -1) //do nothing if not enumerated
            {
                int result;
                textBox1.Focus();
                for (int j = 0; j < devices[selecteddevice].WriteLength; j++) 
                {
                    wData[j] = 0;
                }
                wData[0] = 0;
                wData[1] = 0xc9;    //201
                
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

       

        private void BtnTimeStamp_Click(object sender, EventArgs e)
        {
            for (int j = 0; j < devices[selecteddevice].WriteLength; j++)
            {
                wData[j] = 0;
            }
            wData[0] = 0;
            wData[1] = 210;  //0xd2
            wData[2] = 0; //off

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

        private void BtnDescriptor_Click(object sender, EventArgs e)
        {
            if (CboDevices.SelectedIndex != -1)
            {
                //turn off the callback
                devices[selecteddevice].callNever = true;
                
                for (int j = 0; j < devices[selecteddevice].WriteLength; j++)
                {
                    wData[j] = 0;
                }
                wData[0] = 0;
                wData[1] = 0xd6;

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
                data=new byte[80];
                data[1] = 0;
                int ret = devices[selecteddevice].BlockingReadData(ref data, 100);
                while ((ret == 0 && data[2] != 0xd6) || ret == 304)
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
                else if (data[3] == 2) listBox2.Items.Add("PID #2"); //0=PID #1, 1=HW Mode PID, 2=PID #2
                listBox2.Items.Add("Keymapstart=" + data[4].ToString());
                listBox2.Items.Add("Layer2offset=" + data[5].ToString());
                listBox2.Items.Add("OutSize=" + data[6].ToString());
                listBox2.Items.Add("ReportSize=" + data[7].ToString());
                listBox2.Items.Add("MaxCol=" + data[8].ToString());
                listBox2.Items.Add("RowCol=" + data[9].ToString());
                String ledon = "";
                if ((byte)(data[10] & 64) != 0) ledon = "Green LED ";
                if ((byte)(data[10] & 128) != 0) ledon = ledon+"Red LED ";
                if (ledon == "") ledon = "None";
                listBox2.Items.Add("LEDs=" + ledon);
                listBox2.Items.Add("Version=" + data[11].ToString());
                string temp ="PID=" + (data[13] * 256 + data[12]).ToString();
                listBox2.Items.Add(temp);
            }
        }

        private void BtnLED1_Click(object sender, EventArgs e)
        {
            //write to currently selected device - turn on LED1, only works if disable keyboard control

            if (CboDevices.SelectedIndex != -1) //do nothing if not enumerated
            {
                
                for (int j = 0; j < devices[selecteddevice].WriteLength; j++)
                {
                    wData[j] = 0;
                }
                wData[1] = 186;
                wData[2] = (byte)(saveledstate | 1);
                saveledstate = wData[2];

                int result = 404;
                while (result == 404) { result = devices[selecteddevice].WriteData(wData); }

                if (result != 0)
                {
                    toolStripStatusLabel1.Text = "Write Fail: " + result;
                }
                else
                {
                    toolStripStatusLabel1.Text = "Write Success-LED 1";
                }
            }
        }

        private void BtnLED2_Click(object sender, EventArgs e)
        {
            //write to currently selected device - turn on LED2, only works if disable keyboard control

            if (CboDevices.SelectedIndex != -1) //do nothing if not enumerated
            {
                
                for (int j = 0; j < devices[selecteddevice].WriteLength; j++)
                {
                    wData[j] = 0;
                }
                wData[1] = 186;
                wData[2] = (byte)(saveledstate | 2);
                saveledstate = wData[2];

                int result = 404;
                while (result == 404) { result = devices[selecteddevice].WriteData(wData); }

                if (result != 0)
                {
                    toolStripStatusLabel1.Text = "Write Fail: " + result;
                }
                else
                {
                    toolStripStatusLabel1.Text = "Write Success-LED 2";
                }
            }
        }

        private void BtnLED3_Click(object sender, EventArgs e)
        {
            //write to currently selected device - turn on LED3, only works if disable keyboard control

            if (CboDevices.SelectedIndex != -1) //do nothing if not enumerated
            {
                
                for (int j = 0; j < devices[selecteddevice].WriteLength; j++)
                {
                    wData[j] = 0;
                }
                wData[1] = 186;
                wData[2] = (byte)(saveledstate | 4);
                saveledstate = wData[2];

                int result = 404;
                while (result == 404) { result = devices[selecteddevice].WriteData(wData); }

                if (result != 0)
                {
                    toolStripStatusLabel1.Text = "Write Fail: " + result;
                }
                else
                {
                    toolStripStatusLabel1.Text = "Write Success-LED 3";
                }
            }
        }
        private void BtnLED4_Click(object sender, EventArgs e)
        {
            //write to currently selected device - turn on LED 4

            if (CboDevices.SelectedIndex != -1) //do nothing if not enumerated
            {
                
                for (int j = 0; j < devices[selecteddevice].WriteLength; j++)
                {
                    wData[j] = 0;
                }
                wData[1] = 186;
                wData[2] = (byte)(saveledstate | 8);
                
                saveledstate = wData[2];

                int result = 404;
                while (result == 404) { result = devices[selecteddevice].WriteData(wData); }

                if (result != 0)
                {
                    toolStripStatusLabel1.Text = "Write Fail: " + result;
                }
                else
                {
                    toolStripStatusLabel1.Text = "Write Success-LED 4";
                }
            }
        }
        private void BtnLED5_Click(object sender, EventArgs e)
        {
            //write to currently selected device - turn on LED 5

            if (CboDevices.SelectedIndex != -1) //do nothing if not enumerated
            {
                for (int j = 0; j < devices[selecteddevice].WriteLength; j++)
                {
                    wData[j] = 0;
                }
                wData[1] = 186;
                wData[2] = (byte)(saveledstate | 16);
                saveledstate = wData[2];

                int result = 404;
                while (result == 404) { result = devices[selecteddevice].WriteData(wData); }

                if (result != 0)
                {
                    toolStripStatusLabel1.Text = "Write Fail: " + result;
                }
                else
                {
                    toolStripStatusLabel1.Text = "Write Success-LED 5";
                }
            }
        }

        private void BtnLED6_Click(object sender, EventArgs e)
        {
            //write to currently selected device - turn on LED 6

            if (CboDevices.SelectedIndex != -1) //do nothing if not enumerated
            {
                for (int j = 0; j < devices[selecteddevice].WriteLength; j++)
                {
                    wData[j] = 0;
                }
                wData[1] = 186;
                wData[2] = (byte)(saveledstate | 32);
                saveledstate = wData[2];

                int result = 404;
                while (result == 404) { result = devices[selecteddevice].WriteData(wData); }

                if (result != 0)
                {
                    toolStripStatusLabel1.Text = "Write Fail: " + result;
                }
                else
                {
                    toolStripStatusLabel1.Text = "Write Success-LED 6";
                }
            }
        }

        private void BtnLED7_Click(object sender, EventArgs e)
        {
            //write to currently selected device - turn on LED 7

            if (CboDevices.SelectedIndex != -1) //do nothing if not enumerated
            {
                for (int j = 0; j < devices[selecteddevice].WriteLength; j++)
                {
                    wData[j] = 0;
                }
                wData[1] = 186;
                wData[2] = (byte)(saveledstate | 64);
                saveledstate = wData[2];

                int result = 404;
                while (result == 404) { result = devices[selecteddevice].WriteData(wData); }

                if (result != 0)
                {
                    toolStripStatusLabel1.Text = "Write Fail: " + result;
                }
                else
                {
                    toolStripStatusLabel1.Text = "Write Success-LED 7";
                }
            }
        }

        private void BtnLED8_Click(object sender, EventArgs e)
        {
            //write to currently selected device - turn on LED 8

            if (CboDevices.SelectedIndex != -1) //do nothing if not enumerated
            {
                for (int j = 0; j < devices[selecteddevice].WriteLength; j++)
                {
                    wData[j] = 0;
                }
                wData[1] = 186;
                wData[2] = (byte)(saveledstate | 128);
                saveledstate = wData[2];

                int result = 404;
                while (result == 404) { result = devices[selecteddevice].WriteData(wData); }

                if (result != 0)
                {
                    toolStripStatusLabel1.Text = "Write Fail: " + result;
                }
                else
                {
                    toolStripStatusLabel1.Text = "Write Success-LED 8";
                }
            }
        }

        private void BtnGenerateData_Click(object sender, EventArgs e)
        {
            //After sending this command a general incoming data report will be given with
            //the 3rd byte (Data Type) 2nd bit set.  If program switch is up byte 3 will be 2
            //and if it is pressed byte 3 will be 3.  This is useful for getting the initial state
            //or unit id of the device before it sends any data.
            if (CboDevices.SelectedIndex != -1) //do nothing if not enumerated
            {

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
                    toolStripStatusLabel1.Text = "Write Success - Generate Data";
                }
            }
        }

        private void BtnTimeStampOn_Click(object sender, EventArgs e)
        {
            for (int j = 0; j < devices[selecteddevice].WriteLength; j++)
            {
                wData[j] = 0;
            }
            wData[0] = 0;
            wData[1] = 210;  //0xd2
            wData[2] = 1; //on

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

       

        private void BtnPID1_Click(object sender, EventArgs e)
        {
            if (CboDevices.SelectedIndex != -1) //do nothing if not enumerated
            {

                for (int j = 0; j < devices[selecteddevice].WriteLength; j++)
                {
                    wData[j] = 0;
                }
                wData[0] = 0;
                wData[1] = 204; //0xcc
                wData[2] = 0;  //0=PID #1, 2=PID #2

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
                wData[1] = 204; //0xcc
                wData[2] = 2;  //0=PID #1, 2=PID #2

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
                wData[2] = Convert.ToByte(textBox14.Text); //Buttons; 1=Left, 2=Right, 4=Center, 8=XButton1, 16=XButton2
                wData[3] = Convert.ToByte(textBox16.Text); //Mouse X motion. 128=0 no motion, 1-127 is right, 255-129=left, finest inc (1 and 255) to coarsest (127 and 129).
                wData[4] = Convert.ToByte(textBox15.Text); //Mouse Y motion. 128=0 no motion, 1-127 is down, 255-129=up, finest inc (1 and 255) to coarsest (127 and 129).
                wData[5] = Convert.ToByte(textBox10.Text);//Wheel X. 128=0 no motion, 1-127 is up, 255-129=down, finest inc (1 and 255) to coarsest (127 and 129).
                wData[6] = Convert.ToByte(textBox11.Text);//Wheel Y. 128=0 no motion, 1-127 is up, 255-129=down, finest inc (1 and 255) to coarsest (127 and 129).

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

        private void BtnNoDiodes_Click(object sender, EventArgs e)
        {
            //send this command if no external diodes are present in the key matrix, only need to do once and setting is written to eeprom
            //factory default no diodes
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
                toolStripStatusLabel1.Text = "Write Success - No Diodes";
            }
        }

        private void BtnDiodes_Click(object sender, EventArgs e)
        {
            //send this command if external diodes in the key matrix
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
                toolStripStatusLabel1.Text = "Write Success - Diodes";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void ChkSuppress_CheckedChanged(object sender, EventArgs e)
        {
            if (CboDevices.SelectedIndex != -1) //do nothing if not enumerated
            {
                if (ChkSuppress.Checked == false)
                {
                    devices[selecteddevice].suppressDuplicateReports = false;
                }
                else
                {
                    devices[selecteddevice].suppressDuplicateReports = true;
                }
            }
        }

        

       
   

        
       
       
    }
    
    
}
