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
        Boolean AllBlue = true;
        Boolean AllRed = true;
        long saveabsolutetime = -1;  //for timestamp demo

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //When valid XK-24 devices are connected they are added to the ConnectedDevices list
            //If this list is empty, then no valid devices are connected currently
            if (xk60_80_1.ConnectedDevices == null)
            {
                deviceStatus.Text = "No XK-80/XK-60 Connected";
                deviceStatus.ForeColor = Color.Red;
            }
            else
            {
                deviceStatus.Text = "XK-80/XK-60 Connected";
                deviceStatus.ForeColor = Color.Green;

                //Gets the Unit ID from the device
                lblUID.Text = "Unit ID: " + xk60_80_1.GetDeviceUID(0);

                //Gets the OEM Version ID from the device
                lblOEM.Text = "OEM ID: " + xk60_80_1.GetOEMVersionID(0);

                //Gets the Product ID from the device
                lblProductID.Text = "Product ID: " + xk60_80_1.GetProductID(0);
            }
        }

        private void HandleButtons(XK_60_80.XKeyEventArgs e)
        {
            //This method handles the button change event for the device

            //Gets the button number (CID) of the button that has changed state
            if (e.CID > 1000)
            {
                String ButtonNum = (e.CID - 1000).ToString();

                //Logic structure to handle both "press" (true) and "release" (false) states
                if (e.PressState == true)
                {
                    lblPress.Text = "Button #" + ButtonNum + " Down";
                }
                else if (e.PressState == false)
                {
                    lblRelease.Text = "Button #" + ButtonNum + " Up";
                }
            }

            if (e.PressState == true) //button press
            {
                switch (e.CID)
                {
                    case 1001:
                        lblButton01.BackColor = Color.LimeGreen;
                        break;
                    case 1002:
                        lblButton02.BackColor = Color.LimeGreen;
                        break;
                    case 1003:
                        lblButton03.BackColor = Color.LimeGreen;
                        break;
                    case 1004:
                        lblButton04.BackColor = Color.LimeGreen;
                        break;
                    case 1005:
                        lblButton05.BackColor = Color.LimeGreen;
                        break;
                    case 1006:
                        lblButton06.BackColor = Color.LimeGreen;
                        break;
                    case 1007:
                        lblButton07.BackColor = Color.LimeGreen;
                        break;
                    case 1008:
                        lblButton08.BackColor = Color.LimeGreen;
                        break;
                    case 1009:
                        lblButton09.BackColor = Color.LimeGreen;
                        break;
                    case 1010:
                        lblButton10.BackColor = Color.LimeGreen;
                        break;
                    case 1011:
                        lblButton11.BackColor = Color.LimeGreen;
                        break;
                    case 1012:
                        lblButton12.BackColor = Color.LimeGreen;
                        break;
                    case 1013:
                        lblButton13.BackColor = Color.LimeGreen;
                        break;
                    case 1014:
                        lblButton14.BackColor = Color.LimeGreen;
                        break;
                    case 1015:
                        lblButton15.BackColor = Color.LimeGreen;
                        break;
                    case 1016:
                        lblButton16.BackColor = Color.LimeGreen;
                        break;
                    case 1017:
                        lblButton17.BackColor = Color.LimeGreen;
                        break;
                    case 1018:
                        lblButton18.BackColor = Color.LimeGreen;
                        break;
                    case 1019:
                        lblButton19.BackColor = Color.LimeGreen;
                        break;
                    case 1020:
                        lblButton20.BackColor = Color.LimeGreen;
                        break;
                    case 1021:
                        lblButton21.BackColor = Color.LimeGreen;
                        break;
                    case 1022:
                        lblButton22.BackColor = Color.LimeGreen;
                        break;
                    case 1023:
                        lblButton23.BackColor = Color.LimeGreen;
                        break;
                    case 1024:
                        lblButton24.BackColor = Color.LimeGreen;
                        break;
                    case 1025:
                        lblButton25.BackColor = Color.LimeGreen;
                        break;
                    case 1026:
                        lblButton26.BackColor = Color.LimeGreen;
                        break;
                    case 1027:
                        lblButton27.BackColor = Color.LimeGreen;
                        break;
                    case 1028:
                        lblButton28.BackColor = Color.LimeGreen;
                        break;
                    case 1029:
                        lblButton29.BackColor = Color.LimeGreen;
                        break;
                    case 1030:
                        lblButton30.BackColor = Color.LimeGreen;
                        break;
                    case 1031:
                        lblButton31.BackColor = Color.LimeGreen;
                        break;
                    case 1032:
                        lblButton32.BackColor = Color.LimeGreen;
                        break;
                    case 1033:
                        lblButton33.BackColor = Color.LimeGreen;
                        break;
                    case 1034:
                        lblButton34.BackColor = Color.LimeGreen;
                        break;
                    case 1035:
                        lblButton35.BackColor = Color.LimeGreen;
                        break;
                    case 1036:
                        lblButton36.BackColor = Color.LimeGreen;
                        break;
                    case 1037:
                        lblButton37.BackColor = Color.LimeGreen;
                        break;
                    case 1038:
                        lblButton38.BackColor = Color.LimeGreen;
                        break;
                    case 1039:
                        lblButton39.BackColor = Color.LimeGreen;
                        break;
                    case 1040:
                        lblButton40.BackColor = Color.LimeGreen;
                        break;
                    case 1041:
                        lblButton41.BackColor = Color.LimeGreen;
                        break;
                    case 1042:
                        lblButton42.BackColor = Color.LimeGreen;
                        break;
                    case 1043:
                        lblButton43.BackColor = Color.LimeGreen;
                        break;
                    case 1044:
                        lblButton44.BackColor = Color.LimeGreen;
                        break;
                    case 1045:
                        lblButton45.BackColor = Color.LimeGreen;
                        break;
                    case 1046:
                        lblButton46.BackColor = Color.LimeGreen;
                        break;
                    case 1047:
                        lblButton47.BackColor = Color.LimeGreen;
                        break;
                    case 1048:
                        lblButton48.BackColor = Color.LimeGreen;
                        break;
                    case 1049:
                        lblButton49.BackColor = Color.LimeGreen;
                        break;
                    case 1050:
                        lblButton50.BackColor = Color.LimeGreen;
                        break;
                    case 1051:
                        lblButton51.BackColor = Color.LimeGreen;
                        break;
                    case 1052:
                        lblButton52.BackColor = Color.LimeGreen;
                        break;
                    case 1053:
                        lblButton53.BackColor = Color.LimeGreen;
                        break;
                    case 1054:
                        lblButton54.BackColor = Color.LimeGreen;
                        break;
                    case 1055:
                        lblButton55.BackColor = Color.LimeGreen;
                        break;
                    case 1056:
                        lblButton56.BackColor = Color.LimeGreen;
                        break;
                    case 1057:
                        lblButton57.BackColor = Color.LimeGreen;
                        break;
                    case 1058:
                        lblButton58.BackColor = Color.LimeGreen;
                        break;
                    case 1059:
                        lblButton59.BackColor = Color.LimeGreen;
                        break;
                    case 1060:
                        lblButton60.BackColor = Color.LimeGreen;
                        break;
                    case 1061:
                        lblButton61.BackColor = Color.LimeGreen;
                        break;
                    case 1062:
                        lblButton62.BackColor = Color.LimeGreen;
                        break;
                    case 1063:
                        lblButton63.BackColor = Color.LimeGreen;
                        break;
                    case 1064:
                        lblButton64.BackColor = Color.LimeGreen;
                        break;
                    case 1065:
                        lblButton65.BackColor = Color.LimeGreen;
                        break;
                    case 1066:
                        lblButton66.BackColor = Color.LimeGreen;
                        break;
                    case 1067:
                        lblButton67.BackColor = Color.LimeGreen;
                        break;
                    case 1068:
                        lblButton68.BackColor = Color.LimeGreen;
                        break;
                    case 1069:
                        lblButton69.BackColor = Color.LimeGreen;
                        break;
                    case 1070:
                        lblButton70.BackColor = Color.LimeGreen;
                        break;
                    case 1071:
                        lblButton71.BackColor = Color.LimeGreen;
                        break;
                    case 1072:
                        lblButton72.BackColor = Color.LimeGreen;
                        break;
                    case 1073:
                        lblButton73.BackColor = Color.LimeGreen;
                        break;
                    case 1074:
                        lblButton74.BackColor = Color.LimeGreen;
                        break;
                    case 1075:
                        lblButton75.BackColor = Color.LimeGreen;
                        break;
                    case 1076:
                        lblButton76.BackColor = Color.LimeGreen;
                        break;
                    case 1077:
                        lblButton77.BackColor = Color.LimeGreen;
                        break;
                    case 1078:
                        lblButton78.BackColor = Color.LimeGreen;
                        break;
                    case 1079:
                        lblButton79.BackColor = Color.LimeGreen;
                        break;
                    case 1080:
                        lblButton80.BackColor = Color.LimeGreen;
                        break;
                    case 1081:
                        lblProgSwitch.BackColor = Color.LimeGreen;
                        break;
                }
            }
            else //button release
            {
                switch (e.CID)
                {
                    case 1001:
                        lblButton01.BackColor = default(Color);
                        break;
                    case 1002:
                        lblButton02.BackColor = default(Color);
                        break;
                    case 1003:
                        lblButton03.BackColor = default(Color);
                        break;
                    case 1004:
                        lblButton04.BackColor = default(Color);
                        break;
                    case 1005:
                        lblButton05.BackColor = default(Color);
                        break;
                    case 1006:
                        lblButton06.BackColor = default(Color);
                        break;
                    case 1007:
                        lblButton07.BackColor = default(Color);
                        break;
                    case 1008:
                        lblButton08.BackColor = default(Color);
                        break;
                    case 1009:
                        lblButton09.BackColor = default(Color);
                        break;
                    case 1010:
                        lblButton10.BackColor = default(Color);
                        break;
                    case 1011:
                        lblButton11.BackColor = default(Color);
                        break;
                    case 1012:
                        lblButton12.BackColor = default(Color);
                        break;
                    case 1013:
                        lblButton13.BackColor = default(Color);
                        break;
                    case 1014:
                        lblButton14.BackColor = default(Color);
                        break;
                    case 1015:
                        lblButton15.BackColor = default(Color);
                        break;
                    case 1016:
                        lblButton16.BackColor = default(Color);
                        break;
                    case 1017:
                        lblButton17.BackColor = default(Color);
                        break;
                    case 1018:
                        lblButton18.BackColor = default(Color);
                        break;
                    case 1019:
                        lblButton19.BackColor = default(Color);
                        break;
                    case 1020:
                        lblButton20.BackColor = default(Color);
                        break;
                    case 1021:
                        lblButton21.BackColor = default(Color);
                        break;
                    case 1022:
                        lblButton22.BackColor = default(Color);
                        break;
                    case 1023:
                        lblButton23.BackColor = default(Color);
                        break;
                    case 1024:
                        lblButton24.BackColor = default(Color);
                        break;
                    case 1025:
                        lblButton25.BackColor = default(Color);
                        break;
                    case 1026:
                        lblButton26.BackColor = default(Color);
                        break;
                    case 1027:
                        lblButton27.BackColor = default(Color);
                        break;
                    case 1028:
                        lblButton28.BackColor = default(Color);
                        break;
                    case 1029:
                        lblButton29.BackColor = default(Color);
                        break;
                    case 1030:
                        lblButton30.BackColor = default(Color);
                        break;
                    case 1031:
                        lblButton31.BackColor = default(Color);
                        break;
                    case 1032:
                        lblButton32.BackColor = default(Color);
                        break;
                    case 1033:
                        lblButton33.BackColor = default(Color);
                        break;
                    case 1034:
                        lblButton34.BackColor = default(Color);
                        break;
                    case 1035:
                        lblButton35.BackColor = default(Color);
                        break;
                    case 1036:
                        lblButton36.BackColor = default(Color);
                        break;
                    case 1037:
                        lblButton37.BackColor = default(Color);
                        break;
                    case 1038:
                        lblButton38.BackColor = default(Color);
                        break;
                    case 1039:
                        lblButton39.BackColor = default(Color);
                        break;
                    case 1040:
                        lblButton40.BackColor = default(Color);
                        break;
                    case 1041:
                        lblButton41.BackColor = default(Color);
                        break;
                    case 1042:
                        lblButton42.BackColor = default(Color);
                        break;
                    case 1043:
                        lblButton43.BackColor = default(Color);
                        break;
                    case 1044:
                        lblButton44.BackColor = default(Color);
                        break;
                    case 1045:
                        lblButton45.BackColor = default(Color);
                        break;
                    case 1046:
                        lblButton46.BackColor = default(Color);
                        break;
                    case 1047:
                        lblButton47.BackColor = default(Color);
                        break;
                    case 1048:
                        lblButton48.BackColor = default(Color);
                        break;
                    case 1049:
                        lblButton49.BackColor = default(Color);
                        break;
                    case 1050:
                        lblButton50.BackColor = default(Color);
                        break;
                    case 1051:
                        lblButton51.BackColor = default(Color);
                        break;
                    case 1052:
                        lblButton52.BackColor = default(Color);
                        break;
                    case 1053:
                        lblButton53.BackColor = default(Color);
                        break;
                    case 1054:
                        lblButton54.BackColor = default(Color);
                        break;
                    case 1055:
                        lblButton55.BackColor = default(Color);
                        break;
                    case 1056:
                        lblButton56.BackColor = default(Color);
                        break;
                    case 1057:
                        lblButton57.BackColor = default(Color);
                        break;
                    case 1058:
                        lblButton58.BackColor = default(Color);
                        break;
                    case 1059:
                        lblButton59.BackColor = default(Color);
                        break;
                    case 1060:
                        lblButton60.BackColor = default(Color);
                        break;
                    case 1061:
                        lblButton61.BackColor = default(Color);
                        break;
                    case 1062:
                        lblButton62.BackColor = default(Color);
                        break;
                    case 1063:
                        lblButton63.BackColor = default(Color);
                        break;
                    case 1064:
                        lblButton64.BackColor = default(Color);
                        break;
                    case 1065:
                        lblButton65.BackColor = default(Color);
                        break;
                    case 1066:
                        lblButton66.BackColor = default(Color);
                        break;
                    case 1067:
                        lblButton67.BackColor = default(Color);
                        break;
                    case 1068:
                        lblButton68.BackColor = default(Color);
                        break;
                    case 1069:
                        lblButton69.BackColor = default(Color);
                        break;
                    case 1070:
                        lblButton70.BackColor = default(Color);
                        break;
                    case 1071:
                        lblButton71.BackColor = default(Color);
                        break;
                    case 1072:
                        lblButton72.BackColor = default(Color);
                        break;
                    case 1073:
                        lblButton73.BackColor = default(Color);
                        break;
                    case 1074:
                        lblButton74.BackColor = default(Color);
                        break;
                    case 1075:
                        lblButton75.BackColor = default(Color);
                        break;
                    case 1076:
                        lblButton76.BackColor = default(Color);
                        break;
                    case 1077:
                        lblButton77.BackColor = default(Color);
                        break;
                    case 1078:
                        lblButton78.BackColor = default(Color);
                        break;
                    case 1079:
                        lblButton79.BackColor = default(Color);
                        break;
                    case 1080:
                        lblButton80.BackColor = default(Color);
                        break;
                    case 1081:
                        lblProgSwitch.BackColor = default(Color);
                        break;
                }
            }
            lblUID.Text = "Unit ID: " + xk60_80_1.ConnectedDevices[0].UnitID.ToString();
            //Time Stamp Info
            long absolutetime = e.TimeStamp; //gives time in ms since boot of X-keys unit
            long absolutetimesec = absolutetime / 1000; //convert to seconds
            lblATime.Text = "Absolute Time: " + absolutetimesec.ToString() + " s";
            if (saveabsolutetime != -1)
            {
                lblDTime.Text = "Delta Time: " + (absolutetime - saveabsolutetime).ToString() + " ms"; //this gives the time between button presses or between any generated data reports
            }
            else
            {
                lblDTime.Text = "Delta Time: "; 
            }
            saveabsolutetime = absolutetime;
        }

        private void btnGreenLED_Click(object sender, EventArgs e)
        {
            //Controls the green indicator LED
            if (xk60_80_1.GetGreenIndicatorLED(0) == false)
            {
                xk60_80_1.SetGreenIndicator(0, 1);
            }
            else
            {
                xk60_80_1.SetGreenIndicator(0, 0);
            }
        }

        private void btnRedLED_Click(object sender, EventArgs e)
        {
            //Controls the red indicator LED
            if (xk60_80_1.GetRedIndicatorLED(0) == false)
            {
                xk60_80_1.SetRedIndicator(0, 1);
            }
            else
            {
                xk60_80_1.SetRedIndicator(0, 0);
            }
        }

        private void btnGreenLEDFlash_Click(object sender, EventArgs e)
        {
            //Controls the green indicator LED between flashing and on
            if (IndicatorGreenFlash == false)
            {
                xk60_80_1.SetGreenIndicator(0, 2);
                IndicatorGreenFlash = true;
            }
            else
            {
                xk60_80_1.SetGreenIndicator(0, 1);
                IndicatorGreenFlash = false;
            }
        }

        private void btnRedLEDFlash_Click(object sender, EventArgs e)
        {
            //Controls the red indicator LED between flashing and off
            if (IndicatorRedFlash == false)
            {
                xk60_80_1.SetRedIndicator(0, 2);
                IndicatorRedFlash = true;
            }
            else
            {
                xk60_80_1.SetRedIndicator(0, 0);
                IndicatorRedFlash = false;
            }
        }

        private void btnBlue_Click(object sender, EventArgs e)
        {
            int LightState;

            if (cboBacklightStatus.Text == "On")
            {
                LightState = 1;
            }
            else if (cboBacklightStatus.Text == "Off")
            {
                LightState = 0;
            }
            else if (cboBacklightStatus.Text == "Flash")
            {
                LightState = 2;
            }
            else
            {
                LightState = 1;
            }

            int ButtonID = Convert.ToInt32(spnButton.Value);

            //Sets an individual LED (on bank 0) based on chosen ButtonID and LightState
            xk60_80_1.SetBacklightLED(0, ButtonID, 0, LightState);
        }

        private void btnRed_Click(object sender, EventArgs e)
        {
            int LightState;

            if (cboBacklightStatus.Text == "On")
            {
                LightState = 1;
            }
            else if (cboBacklightStatus.Text == "Off")
            {
                LightState = 0;
            }
            else if (cboBacklightStatus.Text == "Flash")
            {
                LightState = 2;
            }
            else
            {
                LightState = 1;
            }

            int ButtonID = Convert.ToInt32(spnButton.Value);

            //Sets an individual LED (on bank 1) based on chosen ButtonID and LightState
            xk60_80_1.SetBacklightLED(0, ButtonID, 1, LightState);
        }

        private void btnSaveState_Click(object sender, EventArgs e)
        {
            //Saves backlighting state to the memory of the device, when called the current state of the backlights will be the default state when device is booted
            xk60_80_1.SaveBacklightState(0);
        }

        private void btnUID_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtUID.Text);

            //Sets device Unit ID
            xk60_80_1.SetDeviceUID(0, id);
            lblUID.Text = "Unit ID: " + xk60_80_1.GetDeviceUID(0).ToString();
        }

        private void btnOEM_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtOEM.Text);

            //Sets device OEM Version ID
            xk60_80_1.SetOEMVersionID(0, id);
            //after sending this command the device will disconnect and reconnect
        }

        private void btnIntensity_Click(object sender, EventArgs e)
        {
            int Intensity1 = Convert.ToInt32(spnBlueIntensity.Value);
            int Intensity2 = Convert.ToInt32(spnRedIntensity.Value);

            //Sets the intensity for both backlighting banks
            xk60_80_1.SetBacklightIntensity(0, Intensity1, Intensity2);
        }

        private void btnFrequency_Click(object sender, EventArgs e)
        {
            int Frequency = Convert.ToInt32(spnFrequency.Value);

            //Sets the flash frequency for the device
            xk60_80_1.SetFlashFrequency(0, Frequency);
        }

        private void btnRowsBlue_Click(object sender, EventArgs e)
        {
            Boolean row1, row2, row3, row4, row5, row6, row7, row8;

            if (cboRow1.Text == "On")
            {
                row1 = true;
            }
            else
            {
                row1 = false;
            }

            if (cboRow2.Text == "On")
            {
                row2 = true;
            }
            else
            {
                row2 = false;
            }

            if (cboRow3.Text == "On")
            {
                row3 = true;
            }
            else
            {
                row3 = false;
            }

            if (cboRow4.Text == "On")
            {
                row4 = true;
            }
            else
            {
                row4 = false;
            }

            if (cboRow5.Text == "On")
            {
                row5 = true;
            }
            else
            {
                row5 = false;
            }

            if (cboRow6.Text == "On")
            {
                row6 = true;
            }
            else
            {
                row6 = false;
            }
            if (cboRow7.Text == "On")
            {
                row7 = true;
            }
            else
            {
                row7 = false;
            }

            if (cboRow8.Text == "On")
            {
                row8 = true;
            }
            else
            {
                row8 = false;
            }

            //Sets individual rows of backlights on bank 0
            xk60_80_1.SetRowsOfBacklights(0, 0, row1, row2, row3, row4, row5, row6, row7, row8);
        }

        private void btnRowsRed_Click(object sender, EventArgs e)
        {
            Boolean row1, row2, row3, row4, row5, row6, row7, row8;

            if (cboRow1.Text == "On")
            {
                row1 = true;
            }
            else
            {
                row1 = false;
            }

            if (cboRow2.Text == "On")
            {
                row2 = true;
            }
            else
            {
                row2 = false;
            }

            if (cboRow3.Text == "On")
            {
                row3 = true;
            }
            else
            {
                row3 = false;
            }

            if (cboRow4.Text == "On")
            {
                row4 = true;
            }
            else
            {
                row4 = false;
            }

            if (cboRow5.Text == "On")
            {
                row5 = true;
            }
            else
            {
                row5 = false;
            }

            if (cboRow6.Text == "On")
            {
                row6 = true;
            }
            else
            {
                row6 = false;
            }
            if (cboRow7.Text == "On")
            {
                row7 = true;
            }
            else
            {
                row7 = false;
            }

            if (cboRow8.Text == "On")
            {
                row8 = true;
            }
            else
            {
                row8 = false;
            }

            //Sets individual rows of backlights on bank 1
            xk60_80_1.SetRowsOfBacklights(0, 1, row1, row2, row3, row4, row5, row6, row7, row8);
        }

        private void btnToggleAll_Click(object sender, EventArgs e)
        {
            //Toggles current state of backlights on or off
            xk60_80_1.ToggleBacklights(0);
        }

        private void btnAllBlue_Click(object sender, EventArgs e)
        {
            //Sets all the blue on or off
            if (AllBlue == true)
            {
                xk60_80_1.SetAllBlue(0, false);
                AllBlue = false;
            }
            else
            {
                xk60_80_1.SetAllBlue(0, true);
                AllBlue = true;
            }
        }

        private void btnAllRed_Click(object sender, EventArgs e)
        {
            //Sets all of the red on or off
            if (AllRed == true)
            {
                xk60_80_1.SetAllRed(0, false);
                AllRed = false;
            }
            else
            {
                xk60_80_1.SetAllRed(0, true);
                AllRed = true;
            }
        }

        private void btn1089_Click(object sender, EventArgs e)
        {
            //Changes device to the 1089 Product ID (device will "replug" itself)
            //Change to this Product ID if you desire Keyboard and Mouse endpoints
            xk60_80_1.ChangePID(0, 1089);
        }

        private void btn1091_Click(object sender, EventArgs e)
        {
            //Changes device to the 1091 Product ID (device will "replug" itself)
            //Change to this Product ID if you desire Keyboard and Joystick endpoints
            xk60_80_1.ChangePID(0, 1091);
        }

        private void btn1121_Click(object sender, EventArgs e)
        {
            //Changes device to the 1121 Product ID (device will "replug" itself)
            //Change to this Product ID if you desire Keyboard and Mouse endpoints
            xk60_80_1.ChangePID(0, 1121);
        }

        private void btn1123_Click(object sender, EventArgs e)
        {
            //Changes device to the 1123 Product ID (device will "replug" itself)
            //Change to this Product ID if you desire Keyboard and Joystick endpoints
            xk60_80_1.ChangePID(0, 1123);
        }

        private void btnKeyboard_Click(object sender, EventArgs e)
        {
            txtKeyboard.Focus();
            //Sends the letters 'a' 'b' and 'c'
            xk60_80_1.SendKeystrokes(0, false, false, false, false, false, false, false, false, 4, 5, 6, 0, 0, 0);
            //Releases all sent keys
            xk60_80_1.SendKeystrokes(0, false, false, false, false, false, false, false, false, 0, 0, 0, 0, 0, 0);
        }

        private void btnMouse_Click(object sender, EventArgs e)
        {
            //Moves mouse cursor right and up from its current position
            //Mouse endpoint required therefore must be in Product ID 1029 to work
            if (xk60_80_1.ConnectedDevices[0].MouseInterface == true)
            {
                xk60_80_1.SendMouse(0, false, false, false, false, false, 20, 20, 0);
            }
            else
            {
                MessageBox.Show("No mouse endpoint available, change to pid 1029");
            }
        }

        private void btnJoystick_Click(object sender, EventArgs e)
        {
            //Sends game controller message, open Game Controllers control panel to see
            //Joystick endpoint required therefore must be in Product ID 1027 to work
            if (xk60_80_1.ConnectedDevices[0].JoystickInterface == true)
            {
                bool[] buttons = { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false };
                xk60_80_1.SendJoystick(0, 100, 100, 0, 0, 0, 8, buttons);
            }
            else
            {
                MessageBox.Show("No joystick endpoint available, change to pid 1027");
            }
        }

        private void btnCheckDongle_Click(object sender, EventArgs e)
        {
            //Enter the 4 bytes used when xk24_1.SetSecurityKey was called
            byte byte1 = Convert.ToByte(txtByte1.Text);
            byte byte2 = Convert.ToByte(txtByte2.Text);
            byte byte3 = Convert.ToByte(txtByte3.Text);
            byte byte4 = Convert.ToByte(txtByte4.Text);

            //Checks the security key by sending the selected bytes to the device; bytes were valid if it returns true
            bool dongle = xk60_80_1.GetSecurityKey(0, byte1, byte2, byte3, byte4);

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

            xk60_80_1.SetSecurityKey(0, byte1, byte2, byte3, byte4);
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            //Use this to get the state of the buttons even if there was no change in their states. This is often useful on start of application to get initial states of the buttons.
            //Sending this command will trigger the _GenerateReportData event
            xk60_80_1.GenerateReport(0);
           
        }

        private void xk60_80_1_ButtonChange(XK_60_80.XKeyEventArgs e)
        {
            //This method handles the button change event for the device
            HandleButtons(e);
        }

        private void xk60_80_1_DevicePlug(XK_60_80.XKeyPlugEventArgs e)
        {
            deviceStatus.Text = "XK-80/XK-60 Plugged, Unit ID: " + e.UID.ToString() + ", OEM ID: " + e.OEMVersionID.ToString() + ", Product ID: " + e.PID.ToString();
            deviceStatus.ForeColor = Color.Green;

            lblUID.Text = "Unit ID: " + e.UID.ToString();
            lblOEM.Text = "OEM ID: " + e.OEMVersionID.ToString();
            lblProductID.Text = "Product ID: " + e.PID.ToString();

            saveabsolutetime = -1;
        }

        private void xk60_80_1_DeviceUnplug(XK_60_80.XKeyPlugEventArgs e)
        {
            deviceStatus.Text = "XK-80/XK-60 Unplugged, Unit ID: " + e.UID.ToString() + ", OEM ID: " + e.OEMVersionID.ToString() + ", Product ID: " + e.PID.ToString();
            deviceStatus.ForeColor = Color.Red;

            lblUID.Text = "Unit ID: ";
            lblOEM.Text = "OEM ID: ";
            lblProductID.Text = "Product ID: ";
        }

        private void xk60_80_1_GenerateReportData(XK_60_80.XKeyEventArgs e)
        {
            HandleButtons(e);
        }

        

        


    }
}
