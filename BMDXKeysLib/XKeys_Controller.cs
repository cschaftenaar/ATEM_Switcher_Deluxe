#region License
/* ======================================================================
     Copyright (c) 2017 - Schaftenaar Media Productions
                          www.schaftenaarmediaproductions.com
                          info@schaftenaarmediaproductions.com

     Permission is hereby granted, free of charge, to any person
     obtaining a copy of this software and associated documentation
     files (the "Software"), to deal in the Software without
     restriction, including without limitation the rights to use,
     copy, modify, merge, publish, distribute, sublicense, and/or sell
     copies of the Software, and to permit persons to whom the
     Software is furnished to do so, subject to the following
     conditions:

     The above copyright notice and this permission notice shall be
     included in all copies or substantial portions of the Software.

     THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
     EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
     OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
     NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
     HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
     WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
     FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
     OTHER DEALINGS IN THE SOFTWARE.
   ======================================================================*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using PIEHid64Net;

namespace BMDXKeysLib
{
    #region X-Keys EventArgs
    public class XKeys_EventArgs : EventArgs
    {
        public Byte[] Data { get; set; }
        public long ButtonNr { get; set; }
        public bool Switch { get; set; }
    }
    #endregion
    #region X-Keys Delegate
    public delegate void XKeys_EventHandler(object s, XKeys_EventArgs e);
    #endregion

    public class XKeys_Controller : PIEDataHandler, PIEErrorHandler
    {
        public XKeys_Controller(ref ComboBox lb)
        {
            this._cboDevices = lb;
        }

        #region X-Keys Events
        public event XKeys_EventHandler OnSwitch;
        public event XKeys_EventHandler OnButton;
        #endregion
        #region X-Keys Variables
        PIEDevice[] devices;

        int[] cbotodevice = null; //for each item in the CboDevice list maps this index to the device index.  Max devices =100 
        byte[] wData = null; //write data buffer
        byte[] XKeyButtons = null;

        int selecteddevice = -1; //set to the index of CboDevice

        private ComboBox _cboDevices;

        private bool _switch;
        private bool _old_switch;
        private long _buttonNr;
        #endregion

        public void HandlePIEHidData(Byte[] _data, PIEDevice sourceDevice, int error)
        {
            //check the sourceDevice and make sure it is the same device as selected in CboDevice   
            if (sourceDevice == devices[selecteddevice])
            {
                this._switch = (byte)(_data[6]) == 8 ? true : false;
                this._buttonNr = 0;
                this._buttonNr = this._buttonNr | (long)((byte)(_data[1] & 1));
                this._buttonNr = this._buttonNr | (long)((byte)(_data[2] & 1)) << 1;
                this._buttonNr = this._buttonNr | (long)((byte)(_data[3] & 1)) << 2;
                this._buttonNr = this._buttonNr | (long)((byte)(_data[4] & 1)) << 3;
                this._buttonNr = this._buttonNr | (long)((byte)(_data[1] & 2)) << 3;
                this._buttonNr = this._buttonNr | (long)((byte)(_data[2] & 2)) << 4;
                this._buttonNr = this._buttonNr | (long)((byte)(_data[3] & 2)) << 5;
                this._buttonNr = this._buttonNr | (long)((byte)(_data[4] & 2)) << 6;
                this._buttonNr = this._buttonNr | (long)((byte)(_data[1] & 4)) << 6;
                this._buttonNr = this._buttonNr | (long)((byte)(_data[2] & 4)) << 7;
                this._buttonNr = this._buttonNr | (long)((byte)(_data[3] & 4)) << 8;
                this._buttonNr = this._buttonNr | (long)((byte)(_data[4] & 4)) << 9;
                this._buttonNr = this._buttonNr | (long)((byte)(_data[1] & 8)) << 9;
                this._buttonNr = this._buttonNr | (long)((byte)(_data[2] & 8)) << 10;
                this._buttonNr = this._buttonNr | (long)((byte)(_data[3] & 8)) << 11;
                this._buttonNr = this._buttonNr | (long)((byte)(_data[4] & 8)) << 12;

                XKeys_EventArgs args = new XKeys_EventArgs { Data = _data, ButtonNr = this._buttonNr, Switch = this._switch };
                if (this._old_switch != this._switch) this.OnSwitch?.Invoke(this, args);
                this.OnButton?.Invoke(this, args);
                this._old_switch = this._switch;
            }
        }
        public void HandlePIEHidError(PIEDevice sourceDevice, long error)
        {
            MessageBox.Show("Error : " + error.ToString(), "X-Keys ERROR", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        }

        /// <summary>
        /// Get Devices
        /// </summary>
        /// <returns>
        /// True, they are exist
        /// </returns>
        public bool GetDevices()
        {
            _cboDevices.Items.Clear();
            cbotodevice = new int[128]; //128=max # of devices
            //enumerate and setupinterfaces for all devices
            devices = PIEDevice.EnumeratePIE();
            if (devices.Length == 0)
            {
                //No Devices Found
                return false;
            }
            else
            {
                int cbocount = 0; //keeps track of how many valid devices were added to the CboDevice box
                for (int i = 0; i < devices.Length; i++)
                {
                    //information about device
                    //PID = devices[i].Pid);
                    //HID Usage = devices[i].HidUsage);
                    //HID Usage Page = devices[i].HidUsagePage);
                    //HID Version = devices[i].Version);
                    long hidusagepg = devices[i].HidUsagePage;
                    long hidusage = devices[i].HidUsage;
                    if (devices[i].HidUsagePage == 0xc)
                    {
                        switch (devices[i].Pid)
                        {
                            case 693:
                                _cboDevices.Items.Add(devices[i].ProductString + "16 MWII (" + devices[i].Pid + ") - " + devices[i].ManufacturersString);
                                cbotodevice[cbocount] = i;
                                cbocount++;
                                break;

                            default:
                                _cboDevices.Items.Add(devices[i].ProductString + "(" + devices[i].Pid + ") " + devices[i].ManufacturersString);
                                cbotodevice[cbocount] = i;
                                cbocount++;
                                break;
                        }
                        devices[i].SetupInterface();
                        // true = No continues check, false = keep checking
                        devices[i].suppressDuplicateReports = true;
                    }
                }
            }
            if (_cboDevices.Items.Count > 0)
            {
                _cboDevices.SelectedIndex = 0;
                selecteddevice = cbotodevice[_cboDevices.SelectedIndex];
                wData = new byte[devices[selecteddevice].WriteLength];//go ahead and setup for write
            }
            return true;
        }
        public void Connect()
        {
            //setup callback if there are devices found for each device found
            if (_cboDevices.SelectedIndex != -1)
            {
                for (int i = 0; i < _cboDevices.Items.Count; i++)
                {
                    //use the cbotodevice array which contains the mapping of the devices in the CboDevices to the actual device IDs
                    devices[cbotodevice[i]].SetErrorCallback(this);
                    devices[cbotodevice[i]].SetDataCallback(this);
                    devices[cbotodevice[i]].callNever = false;
                }
            }
            XKeyButtons = new byte[17];
        }
        public void Disconnect()
        {
            //closeinterfaces on all devices that have been setup (SetupInterface called)
            for (int i = 0; i < _cboDevices.Items.Count; i++)
            {
                //if devices[].Connected=false don't call CloseInterface
                devices[cbotodevice[i]].CloseInterface();
            }
        }
    }
}
