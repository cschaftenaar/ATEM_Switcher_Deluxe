﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NETComp_Csharp_Sample
{
    //The following is a sample showing how to interact with an XK-24 device using the X-keys XK-24 .NET component.

    public partial class Form1 : Form
    {
        //Variables to track the device lights, initial states here based on device defaults
        Boolean IndicatorRedFlash = false;
        Boolean IndicatorGreenFlash = false;

        long saveabsolutetime=-1;  //for timestamp demo
       
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //When valid XK-24 devices are connected they are added to the ConnectedDevices list
            //If this list is empty, then no valid devices are connected currently
           
            if (xk12SI_1.ConnectedDevices == null)
            {
                deviceStatus.Text = "No XK-12 Switch Interface Connected";
                deviceStatus.ForeColor = Color.Red;
            }
            else
            {
                deviceStatus.Text = "XK-12 Switch Interface Connected";
                deviceStatus.ForeColor = Color.Green;

                //Gets the Unit ID from the device
                lblUID.Text = "Unit ID: " + xk12SI_1.GetDeviceUID(0);

                //Gets the OEM Version ID from the device
                lblOEM.Text = "OEM ID: " + xk12SI_1.GetOEMVersionID(0);

                //Gets the Product ID from the device
                lblProductID.Text = "Product ID: " + xk12SI_1.GetProductID(0);
            }
        }

        private void HandleButtons(Xk12SI.XKeyEventArgs e)
        {
            //Handle state changes of the buttons
            if (e.PressState == true) //button press
            {
                switch (e.CID)
                {
                    case 1001:
                        lblButton01.Text = "Jack 1R-dn";
                        break;
                    case 1002:
                        lblButton02.Text = "Jack 1L-dn";
                        break;
                    case 1003:
                        lblButton03.Text = "Jack 2R-dn";
                        break;
                    case 1004:
                        lblButton04.Text = "Jack 2L-dn";
                        break;
                    case 1005:
                        lblButton05.Text = "Jack 3R-dn";
                        break;
                    case 1006:
                        lblButton06.Text = "Jack 3L-dn";
                        break;
                    case 1007:
                        lblButton07.Text = "Jack 4R-dn";
                        break;
                    case 1008:
                        lblButton08.Text = "Jack 4L-dn";
                        break;
                    case 1009:
                        lblButton09.Text = "Jack 5R-dn";
                        break;
                    case 1010:
                        lblButton10.Text = "Jack 5L-dn";
                        break;
                    case 1011:
                        lblButton11.Text = "Jack 6R-dn";
                        break;
                    case 1012:
                        lblButton12.Text = "Jack 6L-dn";
                        break;
                }
            }
            else //button release
            {
                switch (e.CID)
                {
                    case 1001:
                        lblButton01.Text = "Jack 1R-up";
                        break;
                    case 1002:
                        lblButton02.Text = "Jack 1L-up";
                        break;
                    case 1003:
                        lblButton03.Text = "Jack 2R-up";
                        break;
                    case 1004:
                        lblButton04.Text = "Jack 2L-up";
                        break;
                    case 1005:
                        lblButton05.Text = "Jack 3R-up";
                        break;
                    case 1006:
                        lblButton06.Text = "Jack 3L-up";
                        break;
                    case 1007:
                        lblButton07.Text = "Jack 4R-up";
                        break;
                    case 1008:
                        lblButton08.Text = "Jack 4L-up";
                        break;
                    case 1009:
                        lblButton09.Text = "Jack 5R-up";
                        break;
                    case 1010:
                        lblButton10.Text = "Jack 5L-up";
                        break;
                    case 1011:
                        lblButton11.Text = "Jack 6R-up";
                        break;
                    case 1012:
                        lblButton12.Text = "Jack 6L-up";
                        break;
                }
            }
            lblUID.Text = "Unit ID: " + xk12SI_1.ConnectedDevices[0].UnitID.ToString();
            //Time Stamp Info
            long absolutetime = e.TimeStamp; //gives time in ms since boot of X-keys unit
            long absolutetimesec = absolutetime / 1000; //convert to seconds
            lblATime.Text = "Absolute Time: " + absolutetimesec.ToString() + " s";
            if (saveabsolutetime != -1)
            {
                lblDTime.Text = "Delta Time: " + (absolutetime-saveabsolutetime).ToString() + " ms"; //this gives the time between button presses or between any generated data reports
            }
            saveabsolutetime = absolutetime;
        }

        private void btnGreenLED_Click(object sender, EventArgs e)
        {
            //Toggles the green indicator LED
            if (xk12SI_1.GetGreenIndicatorLED(0) == false)
            {
                xk12SI_1.SetGreenIndicator(0, 1);
            }
            else
            {
                xk12SI_1.SetGreenIndicator(0, 0);
            }
        }

        private void btnRedLED_Click(object sender, EventArgs e)
        {
            //Toggles the red indicator LED
            if (xk12SI_1.GetRedIndicatorLED(0) == false)
            {
                xk12SI_1.SetRedIndicator(0, 1);
            }
            else
            {
                xk12SI_1.SetRedIndicator(0, 0);
            }
        }

        private void btnGreenLEDFlash_Click(object sender, EventArgs e)
        {
            //Toggles the green indicator LED between flashing and on
            if (IndicatorGreenFlash == false)
            {
                xk12SI_1.SetGreenIndicator(0, 2);
                IndicatorGreenFlash = true;
            }
            else
            {
                xk12SI_1.SetGreenIndicator(0, 1);
                IndicatorGreenFlash = false;
            }
        }

        private void btnRedLEDFlash_Click(object sender, EventArgs e)
        {
            //Toggles the red indicator LED between flashing and off
            if (IndicatorRedFlash == false)
            {
                xk12SI_1.SetRedIndicator(0, 2);
                IndicatorRedFlash = true;
            }
            else
            {
                xk12SI_1.SetRedIndicator(0, 0);
                IndicatorRedFlash = false;
            }
        }
        
        private void btnUID_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtUID.Text);

            //Sets device Unit ID
            xk12SI_1.SetDeviceUID(0, id);
            lblUID.Text = "Unit ID: " + xk12SI_1.GetDeviceUID(0).ToString();
        }

        private void btnOEM_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtOEM.Text);

            //Sets device OEM Version ID
            xk12SI_1.SetOEMVersionID(0, id);
            //after sending this command the device will disconnect and reconnect
        }

        private void btnFrequency_Click(object sender, EventArgs e)
        {
            int Frequency = Convert.ToInt32(spnFrequency.Value);

            //Sets the flash frequency for the device
            xk12SI_1.SetFlashFrequency(0, Frequency);
        }

        private void btn1195_Click(object sender, EventArgs e)
        {
            //Changes device to the 1195 Product ID (device will "replug" itself)
            //Change to this Product ID if you desire Keyboard and Mouse endpoints
            xk12SI_1.ChangePID(0, 1195);
        }

        private void btn1192_Click(object sender, EventArgs e)
        {
            //Changes device to the 1192 Product ID (device will "replug" itself)
            //Change to this Product ID if you desire Mouse and Joystick endpoints
            xk12SI_1.ChangePID(0, 1192);
        }

        private void btnKeyboard_Click(object sender, EventArgs e)
        {
            //Keyboard endpoint required therefore must be in Product ID 1195
            if (xk12SI_1.ConnectedDevices[0].KeyboardInterface == true)
            {
                txtKeyboard.Focus();
                //Sends the letters 'a' 'b' and 'c'
                xk12SI_1.SendKeystrokes(0, false, false, false, false, false, false, false, false, 4, 5, 6, 0, 0, 0);
                //Releases all sent keys
                xk12SI_1.SendKeystrokes(0, false, false, false, false, false, false, false, false, 0, 0, 0, 0, 0, 0);
            }
            else
            {
                MessageBox.Show("No keyboard endpoint available, change to pid 1195");

            }
        }

        private void btnMouse_Click(object sender, EventArgs e)
        {
            //Moves mouse cursor right and up from its current position
            //Mouse endpoint required, both Product ID have mouse endpoint
            if (xk12SI_1.ConnectedDevices[0].MouseInterface == true)
            {
                xk12SI_1.SendMouse(0, false, false, false, false, false, 20, 20, 0);
            }
            else
            {
                MessageBox.Show("No mouse endpoint available");
            }
        }

        private void btnJoystick_Click(object sender, EventArgs e)
        {
            //Sends game controller message, open Game Controllers control panel to see
            //Joystick endpoint required therefore must be in Product ID 1192
            if (xk12SI_1.ConnectedDevices[0].JoystickInterface == true)
            {
                bool[] buttons = { true, true, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false };
                xk12SI_1.SendJoystick(0, 255, 127, 127, 127, 127, 6, buttons);
            }
            else
            {
                MessageBox.Show("No joystick endpoint available, change to pid 1192");
            }
        }

        private void btnCheckDongle_Click(object sender, EventArgs e)
        {
            //Enter the 4 bytes used when SetSecurityKey was called
            byte byte1 = Convert.ToByte(txtByte1.Text);
            byte byte2 = Convert.ToByte(txtByte2.Text);
            byte byte3 = Convert.ToByte(txtByte3.Text);
            byte byte4 = Convert.ToByte(txtByte4.Text);

            //Checks the security key by sending the selected bytes to the device; bytes were valid if it returns true
            bool dongle = xk12SI_1.GetSecurityKey(0, byte1, byte2, byte3, byte4);

            if (dongle == true) //correct dongle was entered
            {
                grpDongle.BackColor = Color.Green;
            }
            if (dongle == false) //incorrect dongle was entered
            {
                grpDongle.BackColor = Color.Red;
            }
        }

        private void btnSetDongle_Click(object sender, EventArgs e)
        {
            //Enter in 4 bytes. REMEMBER these 4 bytes they will be required to check the dongle
            byte byte1 = Convert.ToByte(txtByte1.Text);
            byte byte2 = Convert.ToByte(txtByte2.Text);
            byte byte3 = Convert.ToByte(txtByte3.Text);
            byte byte4 = Convert.ToByte(txtByte4.Text);

            xk12SI_1.SetSecurityKey(0, byte1, byte2, byte3, byte4);
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            //Use this to generate a report which gives the state of the buttons even if there was no change in their states, this is often useful on start of application to get initial states of the switches (buttons)
            xk12SI_1.GenerateReport(0);
        }

        private void xk12SI_1_ButtonChange(Xk12SI.XKeyEventArgs e)
        {
            HandleButtons(e);
        }

        private void xk12SI_1_DevicePlug(Xk12SI.XKeyPlugEventArgs e)
        {
            deviceStatus.Text = "XK-12 Switch Interface Plugged, Unit ID: " + e.UID.ToString() + ", OEM ID: " + e.OEMVersionID.ToString() + ", Product ID: " + e.PID.ToString();
            deviceStatus.ForeColor = Color.Green;

            lblUID.Text = "Unit ID: " + e.UID.ToString();
            lblOEM.Text = "OEM ID: " + e.OEMVersionID.ToString();
            lblProductID.Text = "Product ID: " + e.PID.ToString();
        }

        private void xk12SI_1_DeviceUnplug(Xk12SI.XKeyPlugEventArgs e)
        {
            deviceStatus.Text = "XK-12 Switch Interface Unplugged, Unit ID: " + e.UID.ToString() + ", OEM ID: " + e.OEMVersionID.ToString() + ", Product ID: " + e.PID.ToString();
            deviceStatus.ForeColor = Color.Red;

            lblUID.Text = "Unit ID: ";
            lblOEM.Text = "OEM ID: ";
            lblProductID.Text = "Product ID: ";
        }

        private void xk12SI_1_GenerateReportData(Xk12SI.XKeyEventArgs e)
        {
            HandleButtons(e);
        }

        


    }
}
