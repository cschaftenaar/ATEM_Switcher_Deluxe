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
using System.Windows.Forms;
using System.Threading;
using Sanford.Multimedia;
using Sanford.Multimedia.Midi;
using Sanford.Multimedia.Midi.UI;

namespace BMDMidiLib
{
    #region ICON iPad EventArgs
    public class iCON_MIDI_iPad_EventArgs : EventArgs
    {
        public int TouchPadX { get; set; }
        public int TouchPadY { get; set; }
        public int Preset { get; set; }
        public int FaderF1 { get; set; }
        public long ButtonNr { get; set; }
    }
    #endregion
    #region iCON iPad Delegate
    public delegate void MIDI_iCON_iPad_EventHandler(object s, iCON_MIDI_iPad_EventArgs e);
    #endregion

    public class iCON_MIDI_iPad_Controller
    {
        private System.Windows.Forms.Form _thisForm;
        private SynchronizationContext context;

        private const int SysExBufferSize = 128;
        private InputDevice inDevice = null;
        private OutputDevice outDevice = null;
        private InputDeviceDialog inpdev = new InputDeviceDialog();
        private OutputDeviceDialog outpdev = new OutputDeviceDialog();
        private DeviceDialog mididev = new DeviceDialog();

        #region iCON iPad Events
        public event MIDI_iCON_iPad_EventHandler OnTouchpad;
        public event MIDI_iCON_iPad_EventHandler OnButton;
        public event MIDI_iCON_iPad_EventHandler OnFaderF1;
        #endregion
        #region iCON iPad Variables
        private int _touchPadX;
        private int _touchPadY;
        private int _touchPadXold;
        private int _preset;
        private long _buttonNr;
        private int _faderF1;
        #endregion

        public iCON_MIDI_iPad_Controller(System.Windows.Forms.Form frm)
        {
            this._thisForm = frm;
        }

        public void Connect()
        {
            if (InputDevice.DeviceCount == 0)
            {
                MessageBox.Show("No MIDI input devices available.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                this.Disconnect();
            }
            else
            {
                try
                {
                    this.context = SynchronizationContext.Current;

                    this.mididev.ShowDialog();
                    this.inDevice = new InputDevice(mididev.InputDeviceID);
                    this.inDevice.ChannelMessageReceived += OnHandleChannelMessageReceived;
                    this.inDevice.Error += new EventHandler<ErrorEventArgs>(OnDevice_Error);
                    this.outDevice = new OutputDevice(mididev.OutputDeviceID);
                    this.outDevice.Error += new EventHandler<ErrorEventArgs>(OnDevice_Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    this.Disconnect();
                }
            }
        }
        public void Disconnect()
        {
            if (this.inDevice != null) this.inDevice.Close();
            if (this.outDevice != null) this.outDevice.Close();
            this.inpdev = null;
            this.outpdev = null;
        }

        private void OnHandleChannelMessageReceived(object sender, ChannelMessageEventArgs e)
        {
            iCON_MIDI_iPad_EventArgs args = new iCON_MIDI_iPad_EventArgs();

            this._preset = e.Message.MidiChannel + 1;
            args.Preset = this._preset;

            switch (e.Message.Command)
            {
                case ChannelCommand.Controller:
                    switch (e.Message.Data1)
                    {
                        // Touchpad Y
                        case 8:
                            this._touchPadY = e.Message.Data2;
                            args.TouchPadY = this._touchPadY;
                            this.OnTouchpad?.Invoke(this, args);
                            break;

                        // FaderF1
                        case 7:
                            this._faderF1 = e.Message.Data2;
                            args.FaderF1 = this._faderF1;
                            this.OnFaderF1?.Invoke(this, args);
                            break;
                    }
                    break;

                case ChannelCommand.PitchWheel:
                    switch (e.Message.Data1)
                    {
                        // Touchpad X
                        default:
                            this._touchPadXold = this._touchPadX;
                            if ((e.Message.Data2 == 64 & this._touchPadXold == 127) | (e.Message.Data2 == 64 & this._touchPadY == 0))
                            {
                                this._touchPadX = 0;
                                args.TouchPadX = this._touchPadX;
                            }
                            else
                            {
                                this._touchPadX = e.Message.Data2;
                                args.TouchPadX = this._touchPadX;
                            }
                            this.OnTouchpad?.Invoke(this, args);
                            break;
                    }
                    break;

                case ChannelCommand.NoteOn:
                    this._buttonNr = this._buttonNr | (long)1 << e.Message.Data1;
                    args.ButtonNr = this._buttonNr;
                    this.OnButton?.Invoke(this, args);
                    break;

                case ChannelCommand.NoteOff:
                    this._buttonNr = this._buttonNr & (long)0xFFF - (1 << e.Message.Data1);
                    args.ButtonNr = this._buttonNr;
                    this.OnButton?.Invoke(this, args);
                    break;
            }
        }
        private void OnDevice_Error(object sender, ErrorEventArgs e)
        {
            MessageBox.Show(e.Error.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        }

        public void StartRecording()
        {
            try
            {
                this.inDevice.StartRecording();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
        public void StopRecording()
        {
            try
            {
                this.inDevice.StopRecording();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
        public void Reset()
        {
            try
            {
                this.inDevice.Reset();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        public void MIDI_Send(ChannelCommand miditype, int midichdev, int midiparm, int mididata1, int mididata2)
        {
            try
            {
                this.outDevice.Send(new ChannelMessage(miditype, (int)midichdev, (int)mididata1, (int)mididata2));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
    }
}
