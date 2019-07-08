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
    #region BCF2000 Delegate
    public delegate void MIDI_BCF2000_EventHandler(object s, Sanford.Multimedia.Midi.ChannelMessageEventArgs e);
    #endregion

    public class BCF2000_MIDI_Controller
    {
        private System.Windows.Forms.Form _thisForm;
        private SynchronizationContext context;

        private const int SysExBufferSize = 128;
        private InputDevice inDevice = null;
        private OutputDevice outDevice = null;
        private InputDeviceDialog inpdev = new InputDeviceDialog();
        private OutputDeviceDialog outpdev = new OutputDeviceDialog();
        private DeviceDialog mididev = new DeviceDialog();

        public event MIDI_BCF2000_EventHandler OnChannelMessageReceived;

        #region BCF2000 Events
        public event MIDI_BCF2000_EventHandler OnFaders;
        public event MIDI_BCF2000_EventHandler OnFader1;
        public event MIDI_BCF2000_EventHandler OnFader2;
        public event MIDI_BCF2000_EventHandler OnFader3;
        public event MIDI_BCF2000_EventHandler OnFader4;
        public event MIDI_BCF2000_EventHandler OnFader5;
        public event MIDI_BCF2000_EventHandler OnFader6;
        public event MIDI_BCF2000_EventHandler OnFader7;
        public event MIDI_BCF2000_EventHandler OnFader8;
        public event MIDI_BCF2000_EventHandler OnEncoders;
        public event MIDI_BCF2000_EventHandler OnEncoder1;
        public event MIDI_BCF2000_EventHandler OnEncoder2;
        public event MIDI_BCF2000_EventHandler OnEncoder3;
        public event MIDI_BCF2000_EventHandler OnEncoder4;
        public event MIDI_BCF2000_EventHandler OnEncoder5;
        public event MIDI_BCF2000_EventHandler OnEncoder6;
        public event MIDI_BCF2000_EventHandler OnEncoder7;
        public event MIDI_BCF2000_EventHandler OnEncoder8;
        public event MIDI_BCF2000_EventHandler OnEncoder9;
        public event MIDI_BCF2000_EventHandler OnEncoder10;
        public event MIDI_BCF2000_EventHandler OnEncoder11;
        public event MIDI_BCF2000_EventHandler OnEncoder12;
        public event MIDI_BCF2000_EventHandler OnEncoder13;
        public event MIDI_BCF2000_EventHandler OnEncoder14;
        public event MIDI_BCF2000_EventHandler OnEncoder15;
        public event MIDI_BCF2000_EventHandler OnEncoder16;
        //public event MIDI_BCF2000_EventHandler OnPushEncoderGroup;
        //public event MIDI_BCF2000_EventHandler OnPushEncoderGroup1;
        //public event MIDI_BCF2000_EventHandler OnPushEncoderGroup2;
        //public event MIDI_BCF2000_EventHandler OnPushEncoderGroup3;
        //public event MIDI_BCF2000_EventHandler OnPushEncoderGroup4;
        public event MIDI_BCF2000_EventHandler OnUpperRowKeys;
        public event MIDI_BCF2000_EventHandler OnUpperRowKey1;
        public event MIDI_BCF2000_EventHandler OnUpperRowKey2;
        public event MIDI_BCF2000_EventHandler OnUpperRowKey3;
        public event MIDI_BCF2000_EventHandler OnUpperRowKey4;
        public event MIDI_BCF2000_EventHandler OnUpperRowKey5;
        public event MIDI_BCF2000_EventHandler OnUpperRowKey6;
        public event MIDI_BCF2000_EventHandler OnUpperRowKey7;
        public event MIDI_BCF2000_EventHandler OnUpperRowKey8;
        public event MIDI_BCF2000_EventHandler OnLowerRowKeys;
        public event MIDI_BCF2000_EventHandler OnLowerRowKey1;
        public event MIDI_BCF2000_EventHandler OnLowerRowKey2;
        public event MIDI_BCF2000_EventHandler OnLowerRowKey3;
        public event MIDI_BCF2000_EventHandler OnLowerRowKey4;
        public event MIDI_BCF2000_EventHandler OnLowerRowKey5;
        public event MIDI_BCF2000_EventHandler OnLowerRowKey6;
        public event MIDI_BCF2000_EventHandler OnLowerRowKey7;
        public event MIDI_BCF2000_EventHandler OnLowerRowKey8;
        public event MIDI_BCF2000_EventHandler OnUserKeys;
        public event MIDI_BCF2000_EventHandler OnUserKey1;
        public event MIDI_BCF2000_EventHandler OnUserKey2;
        public event MIDI_BCF2000_EventHandler OnUserKey3;
        public event MIDI_BCF2000_EventHandler OnUserKey4;
        public event MIDI_BCF2000_EventHandler OnFunctionKeys;
        public event MIDI_BCF2000_EventHandler OnFunctionKeyStore;
        public event MIDI_BCF2000_EventHandler OnFunctionKeyLearn;
        public event MIDI_BCF2000_EventHandler OnFunctionKeyEdit;
        public event MIDI_BCF2000_EventHandler OnFunctionKeyExit;
        public event MIDI_BCF2000_EventHandler OnEncodergroups;
        public event MIDI_BCF2000_EventHandler OnEncodergroup1;
        public event MIDI_BCF2000_EventHandler OnEncodergroup2;
        public event MIDI_BCF2000_EventHandler OnEncodergroup3;
        public event MIDI_BCF2000_EventHandler OnEncodergroup4;
        //public event MIDI_BCF2000_EventHandler OnFootSwitch;
        //public event MIDI_BCF2000_EventHandler OnDYSFUNCTIONAL;
        public event MIDI_BCF2000_EventHandler OnPreset;
        public event MIDI_BCF2000_EventHandler OnPresetDecr;
        public event MIDI_BCF2000_EventHandler OnPresetIncr;
        #endregion
        #region BCF2000 Variables
        public bool IsInitActivated;

        private BCF2000Buttons _ChangedButton;
        private int _UpperRowKey1;
        private int _UpperRowKey2;
        private int _UpperRowKey3;
        private int _UpperRowKey4;
        private int _UpperRowKey5;
        private int _UpperRowKey6;
        private int _UpperRowKey7;
        private int _UpperRowKey8;
        private int _LowerRowKey1;
        private int _LowerRowKey2;
        private int _LowerRowKey3;
        private int _LowerRowKey4;
        private int _LowerRowKey5;
        private int _LowerRowKey6;
        private int _LowerRowKey7;
        private int _LowerRowKey8;
        private int _EncoderGroup1;
        private int _EncoderGroup2;
        private int _EncoderGroup3;
        private int _EncoderGroup4;
        private int _FunctionKeyStore;
        private int _FunctionKeyLearn;
        private int _FunctionKeyEdit;
        private int _FunctionKeyExit;
        private int _PresetDecr;
        private int _PresetIncr;
        private int _UserKey1;
        private int _UserKey2;
        private int _UserKey3;
        private int _UserKey4;
        private int _Fader1;
        private int _Fader2;
        private int _Fader3;
        private int _Fader4;
        private int _Fader5;
        private int _Fader6;
        private int _Fader7;
        private int _Fader8;
        private int _Encoder1;
        private int _Encoder2;
        private int _Encoder3;
        private int _Encoder4;
        private int _Encoder5;
        private int _Encoder6;
        private int _Encoder7;
        private int _Encoder8;
        private int _Encoder9;
        private int _Encoder10;
        private int _Encoder11;
        private int _Encoder12;
        private int _Encoder13;
        private int _Encoder14;
        private int _Encoder15;
        private int _Encoder16;
        private int _EncoderKey1Group1;
        private int _EncoderKey2Group1;
        private int _EncoderKey3Group1;
        private int _EncoderKey4Group1;
        private int _EncoderKey5Group1;
        private int _EncoderKey6Group1;
        private int _EncoderKey7Group1;
        private int _EncoderKey8Group1;
        private int _EncoderKey1Group2;
        private int _EncoderKey2Group2;
        private int _EncoderKey3Group2;
        private int _EncoderKey4Group2;
        private int _EncoderKey5Group2;
        private int _EncoderKey6Group2;
        private int _EncoderKey7Group2;
        private int _EncoderKey8Group2;
        private int _EncoderKey1Group3;
        private int _EncoderKey2Group3;
        private int _EncoderKey3Group3;
        private int _EncoderKey4Group3;
        private int _EncoderKey5Group3;
        private int _EncoderKey6Group3;
        private int _EncoderKey7Group3;
        private int _EncoderKey8Group3;
        private int _EncoderKey1Group4;
        private int _EncoderKey2Group4;
        private int _EncoderKey3Group4;
        private int _EncoderKey4Group4;
        private int _EncoderKey5Group4;
        private int _EncoderKey6Group4;
        private int _EncoderKey7Group4;
        private int _EncoderKey8Group4;

        public BCF2000Buttons ChangedButton
        {
            get
            {
                return this._ChangedButton;
            }
        }
        public int FunctionKeyStore
        {
            get
            {
                return this._FunctionKeyStore;
            }
            set
            {
                this._ChangedButton = BCF2000Buttons.FunctionKeyStore;
                this._FunctionKeyStore = value;
                this.outDevice.Send(new ChannelMessage(ChannelCommand.NoteOn, 0, 53, _FunctionKeyStore));
            }
        }
        public int FunctionKeyLearn
        {
            get
            {
                return this._FunctionKeyLearn;
            }
            set
            {
                this._ChangedButton = BCF2000Buttons.FunctionKeyLearn;
                this._FunctionKeyLearn = value;
                this.outDevice.Send(new ChannelMessage(ChannelCommand.NoteOn, 0, 54, _FunctionKeyLearn));
            }
        }
        public int FunctionKeyEdit
        {
            get
            {
                return this._FunctionKeyEdit;
            }
            set
            {
                this._ChangedButton = BCF2000Buttons.FunctionKeyEdit;
                this._FunctionKeyEdit = value;
                this.outDevice.Send(new ChannelMessage(ChannelCommand.NoteOn, 0, 55, _FunctionKeyEdit));
            }
        }
        public int FunctionKeyExit
        {
            get
            {
                return this._FunctionKeyExit;
            }
            set
            {
                this._ChangedButton = BCF2000Buttons.FunctionKeyExit;
                this._FunctionKeyExit = value;
                this.outDevice.Send(new ChannelMessage(ChannelCommand.NoteOn, 0, 56, _FunctionKeyExit));
            }
        }
        public int EncoderGroup1
        {
            get
            {
                return this._EncoderGroup1;
            }
            set
            {
                this._ChangedButton = BCF2000Buttons.EncoderGroup1;
                this._EncoderGroup1 = value;
                this.outDevice.Send(new ChannelMessage(ChannelCommand.NoteOn, 0, 57, _EncoderGroup1));
            }
        }
        public int EncoderGroup2
        {
            get
            {
                return this._EncoderGroup2;
            }
            set
            {
                this._ChangedButton = BCF2000Buttons.EncoderGroup2;
                this._EncoderGroup2 = value;
                this.outDevice.Send(new ChannelMessage(ChannelCommand.NoteOn, 0, 58, _EncoderGroup2));
            }
        }
        public int EncoderGroup3
        {
            get
            {
                return this._EncoderGroup3;
            }
            set
            {
                this._ChangedButton = BCF2000Buttons.EncoderGroup3;
                this._EncoderGroup3 = value;
                this.outDevice.Send(new ChannelMessage(ChannelCommand.NoteOn, 0, 59, _EncoderGroup1));
            }
        }
        public int EncoderGroup4
        {
            get
            {
                return this._EncoderGroup4;
            }
            set
            {
                this._ChangedButton = BCF2000Buttons.EncoderGroup4;
                this._EncoderGroup4 = value;
                this.outDevice.Send(new ChannelMessage(ChannelCommand.NoteOn, 0, 60, _EncoderGroup1));
            }
        }
        public int PresetDecr
        {
            get
            {
                return this._PresetDecr;
            }
            set
            {
                this._ChangedButton = BCF2000Buttons.PresetDecr;
                this._PresetDecr = value;
                this.outDevice.Send(new ChannelMessage(ChannelCommand.NoteOn, 0, 63, _PresetDecr));
            }
        }
        public int PresetIncr
        {
            get
            {
                return this._PresetIncr;
            }
            set
            {
                this._ChangedButton = BCF2000Buttons.PresetIncr;
                this._PresetIncr = value;
                this.outDevice.Send(new ChannelMessage(ChannelCommand.NoteOn, 0, 64, _PresetIncr));
            }
        }
        public int UserKey1
        {
            get
            {
                return this._UserKey1;
            }
            set
            {
                this._ChangedButton = BCF2000Buttons.UserKey1;
                this._UserKey1 = value;
                this.outDevice.Send(new ChannelMessage(ChannelCommand.NoteOn, 0, 49, _UserKey1));
            }
        }
        public int UserKey2
        {
            get
            {
                return this._UserKey2;
            }
            set
            {
                this._ChangedButton = BCF2000Buttons.UserKey2;
                this._UserKey2 = value;
                this.outDevice.Send(new ChannelMessage(ChannelCommand.NoteOn, 0, 50, _UserKey2));
            }
        }
        public int UserKey3
        {
            get
            {
                return this._UserKey3;
            }
            set
            {
                this._ChangedButton = BCF2000Buttons.UserKey3;
                this._UserKey3 = value;
                this.outDevice.Send(new ChannelMessage(ChannelCommand.NoteOn, 0, 51, _UserKey3));
            }
        }
        public int UserKey4
        {
            get
            {
                return this._UserKey4;
            }
            set
            {
                this._ChangedButton = BCF2000Buttons.UserKey4;
                this._UserKey4 = value;
                this.outDevice.Send(new ChannelMessage(ChannelCommand.NoteOn, 0, 52, _UserKey4));
            }
        }
        public int LowerRowKey1
        {
            get
            {
                return this._LowerRowKey1;
            }
            set
            {
                this._ChangedButton = BCF2000Buttons.LowerRowKey1;
                this._LowerRowKey1 = value;
                this.outDevice.Send(new ChannelMessage(ChannelCommand.NoteOn, 0, 41, _LowerRowKey1));
            }
        }
        public int LowerRowKey2
        {
            get
            {
                return this._LowerRowKey2;
            }
            set
            {
                this._ChangedButton = BCF2000Buttons.LowerRowKey2;
                this._LowerRowKey2 = value;
                this.outDevice.Send(new ChannelMessage(ChannelCommand.NoteOn, 0, 42, _LowerRowKey2));
            }
        }
        public int LowerRowKey3
        {
            get
            {
                return this._LowerRowKey3;
            }
            set
            {
                this._ChangedButton = BCF2000Buttons.LowerRowKey3;
                this._LowerRowKey3 = value;
                this.outDevice.Send(new ChannelMessage(ChannelCommand.NoteOn, 0, 43, _LowerRowKey3));
            }
        }
        public int LowerRowKey4
        {
            get
            {
                return this._LowerRowKey4;
            }
            set
            {
                this._ChangedButton = BCF2000Buttons.LowerRowKey4;
                this._LowerRowKey4 = value;
                this.outDevice.Send(new ChannelMessage(ChannelCommand.NoteOn, 0, 44, _LowerRowKey4));
            }
        }
        public int LowerRowKey5
        {
            get
            {
                return this._LowerRowKey5;
            }
            set
            {
                this._ChangedButton = BCF2000Buttons.LowerRowKey5;
                this._LowerRowKey5 = value;
                this.outDevice.Send(new ChannelMessage(ChannelCommand.NoteOn, 0, 45, _LowerRowKey5));
            }
        }
        public int LowerRowKey6
        {
            get
            {
                return this._LowerRowKey6;
            }
            set
            {
                this._ChangedButton = BCF2000Buttons.LowerRowKey6;
                this._LowerRowKey6 = value;
                this.outDevice.Send(new ChannelMessage(ChannelCommand.NoteOn, 0, 46, _LowerRowKey6));
            }
        }
        public int LowerRowKey7
        {
            get
            {
                return this._LowerRowKey7;
            }
            set
            {
                this._ChangedButton = BCF2000Buttons.LowerRowKey7;
                this._LowerRowKey7 = value;
                this.outDevice.Send(new ChannelMessage(ChannelCommand.NoteOn, 0, 47, _LowerRowKey7));
            }
        }
        public int LowerRowKey8
        {
            get
            {
                return this._LowerRowKey8;
            }
            set
            {
                this._ChangedButton = BCF2000Buttons.LowerRowKey8;
                this._LowerRowKey8 = value;
                this.outDevice.Send(new ChannelMessage(ChannelCommand.NoteOn, 0, 48, _LowerRowKey8));
            }
        }
        public int UpperRowKey1
        {
            get
            {
                return this._UpperRowKey1;
            }
            set
            {
                this._ChangedButton = BCF2000Buttons.UpperRowKey1;
                this._UpperRowKey1 = value;
                this.outDevice.Send(new ChannelMessage(ChannelCommand.NoteOn, 0, 33, _UpperRowKey1));
            }
        }
        public int UpperRowKey2
        {
            get
            {
                return this._UpperRowKey2;
            }
            set
            {
                this._ChangedButton = BCF2000Buttons.UpperRowKey2;
                this._UpperRowKey2 = value;
                this.outDevice.Send(new ChannelMessage(ChannelCommand.NoteOn, 0, 34, _UpperRowKey2));
            }
        }
        public int UpperRowKey3
        {
            get
            {
                return this._UpperRowKey3;
            }
            set
            {
                this._ChangedButton = BCF2000Buttons.UpperRowKey3;
                this._UpperRowKey3 = value;
                this.outDevice.Send(new ChannelMessage(ChannelCommand.NoteOn, 0, 35, _UpperRowKey3));
            }
        }
        public int UpperRowKey4
        {
            get
            {
                return this._UpperRowKey4;
            }
            set
            {
                this._ChangedButton = BCF2000Buttons.UpperRowKey4;
                this._UpperRowKey4 = value;
                this.outDevice.Send(new ChannelMessage(ChannelCommand.NoteOn, 0, 36, _UpperRowKey4));
            }
        }
        public int UpperRowKey5
        {
            get
            {
                return this._UpperRowKey5;
            }
            set
            {
                this._ChangedButton = BCF2000Buttons.UpperRowKey5;
                this._UpperRowKey5 = value;
                this.outDevice.Send(new ChannelMessage(ChannelCommand.NoteOn, 0, 37, _UpperRowKey5));
            }
        }
        public int UpperRowKey6
        {
            get
            {
                return this._UpperRowKey6;
            }
            set
            {
                this._ChangedButton = BCF2000Buttons.UpperRowKey6;
                this._UpperRowKey6 = value;
                this.outDevice.Send(new ChannelMessage(ChannelCommand.NoteOn, 0, 38, _UpperRowKey6));
            }
        }
        public int UpperRowKey7
        {
            get
            {
                return this._UpperRowKey7;
            }
            set
            {
                this._ChangedButton = BCF2000Buttons.UpperRowKey7;
                this._UpperRowKey7 = value;
                this.outDevice.Send(new ChannelMessage(ChannelCommand.NoteOn, 0, 39, _UpperRowKey7));
            }
        }
        public int UpperRowKey8
        {
            get
            {
                return this._UpperRowKey8;
            }
            set
            {
                this._ChangedButton = BCF2000Buttons.UpperRowKey8;
                this._UpperRowKey8 = value;
                this.outDevice.Send(new ChannelMessage(ChannelCommand.NoteOn, 0, 40, _UpperRowKey8));
            }
        }
        public int Fader1
        {
            get
            {
                return this._Fader1;
            }
            set
            {
                this._ChangedButton = BCF2000Buttons.Fader1;
                this._Fader1 = value;
                this.outDevice.Send(new ChannelMessage(ChannelCommand.Controller, 0, 7, _Fader1));
            }
        }
        public int Fader2
        {
            get
            {
                return this._Fader2;
            }
            set
            {
                this._ChangedButton = BCF2000Buttons.Fader2;
                this._Fader2 = value;
                this.outDevice.Send(new ChannelMessage(ChannelCommand.Controller, 1, 7, _Fader2));
            }
        }
        public int Fader3
        {
            get
            {
                return this._Fader3;
            }
            set
            {
                this._ChangedButton = BCF2000Buttons.Fader3;
                this._Fader3 = value;
                this.outDevice.Send(new ChannelMessage(ChannelCommand.Controller, 2, 7, _Fader3));
            }
        }
        public int Fader4
        {
            get
            {
                return this._Fader4;
            }
            set
            {
                this._ChangedButton = BCF2000Buttons.Fader4;
                this._Fader4 = value;
                this.outDevice.Send(new ChannelMessage(ChannelCommand.Controller, 3, 7, _Fader4));
            }
        }
        public int Fader5
        {
            get
            {
                return this._Fader5;
            }
            set
            {
                this._ChangedButton = BCF2000Buttons.Fader5;
                this._Fader5 = value;
                this.outDevice.Send(new ChannelMessage(ChannelCommand.Controller, 4, 7, _Fader5));
            }
        }
        public int Fader6
        {
            get
            {
                return this._Fader6;
            }
            set
            {
                this._ChangedButton = BCF2000Buttons.Fader6;
                this._Fader6 = value;
                this.outDevice.Send(new ChannelMessage(ChannelCommand.Controller, 5, 7, _Fader6));
            }
        }
        public int Fader7
        {
            get
            {
                return this._Fader7;
            }
            set
            {
                this._ChangedButton = BCF2000Buttons.Fader7;
                this._Fader7 = value;
                this.outDevice.Send(new ChannelMessage(ChannelCommand.Controller, 6, 7, _Fader7));
            }
        }
        public int Fader8
        {
            get
            {
                return this._Fader8;
            }
            set
            {
                this._ChangedButton = BCF2000Buttons.Fader8;
                this._Fader8 = value;
                this.outDevice.Send(new ChannelMessage(ChannelCommand.Controller, 7, 7, _Fader8));
            }
        }
        public int Encoder1
        {
            get
            {
                return this._Encoder1;
            }
            set
            {
                this._ChangedButton = BCF2000Buttons.Encoder1;
                this._Encoder1 = value;
                this.outDevice.Send(new ChannelMessage(ChannelCommand.Controller, 0, 8, _Encoder1));
            }
        }
        public int Encoder2
        {
            get
            {
                return this._Encoder2;
            }
            set
            {
                this._ChangedButton = BCF2000Buttons.Encoder2;
                this._Encoder2 = value;
                this.outDevice.Send(new ChannelMessage(ChannelCommand.Controller, 1, 8, _Encoder2));
            }
        }
        public int Encoder3
        {
            get
            {
                return this._Encoder3;
            }
            set
            {
                this._ChangedButton = BCF2000Buttons.Encoder3;
                this._Encoder3 = value;
                this.outDevice.Send(new ChannelMessage(ChannelCommand.Controller, 2, 8, _Encoder3));
            }
        }
        public int Encoder4
        {
            get
            {
                return this._Encoder4;
            }
            set
            {
                this._ChangedButton = BCF2000Buttons.Encoder4;
                this._Encoder4 = value;
                this.outDevice.Send(new ChannelMessage(ChannelCommand.Controller, 3, 8, _Encoder4));
            }
        }
        public int Encoder5
        {
            get
            {
                return this._Encoder5;
            }
            set
            {
                this._ChangedButton = BCF2000Buttons.Encoder5;
                this._Encoder5 = value;
                this.outDevice.Send(new ChannelMessage(ChannelCommand.Controller, 4, 8, _Encoder5));
            }
        }
        public int Encoder6
        {
            get
            {
                return this._Encoder6;
            }
            set
            {
                this._ChangedButton = BCF2000Buttons.Encoder6;
                this._Encoder6 = value;
                this.outDevice.Send(new ChannelMessage(ChannelCommand.Controller, 5, 8, _Encoder6));
            }
        }
        public int Encoder7
        {
            get
            {
                return this._Encoder7;
            }
            set
            {
                this._ChangedButton = BCF2000Buttons.Encoder7;
                this._Encoder7 = value;
                this.outDevice.Send(new ChannelMessage(ChannelCommand.Controller, 6, 8, _Encoder7));
            }
        }
        public int Encoder8
        {
            get
            {
                return this._Encoder8;
            }
            set
            {
                this._ChangedButton = BCF2000Buttons.Encoder8;
                this._Encoder8 = value;
                this.outDevice.Send(new ChannelMessage(ChannelCommand.Controller, 7, 8, _Encoder8));
            }
        }
        public int Encoder9
        {
            get
            {
                return this._Encoder9;
            }
            set
            {
                this._ChangedButton = BCF2000Buttons.Encoder9;
                this._Encoder9 = value;
                this.outDevice.Send(new ChannelMessage(ChannelCommand.Controller, 8, 8, _Encoder9));
            }
        }
        public int Encoder10
        {
            get
            {
                return this._Encoder10;
            }
            set
            {
                this._ChangedButton = BCF2000Buttons.Encoder10;
                this._Encoder10 = value;
                this.outDevice.Send(new ChannelMessage(ChannelCommand.Controller, 9, 8, _Encoder10));
            }
        }
        public int Encoder11
        {
            get
            {
                return this._Encoder11;
            }
            set
            {
                this._ChangedButton = BCF2000Buttons.Encoder11;
                this._Encoder11 = value;
                this.outDevice.Send(new ChannelMessage(ChannelCommand.Controller, 10, 8, _Encoder11));
            }
        }
        public int Encoder12
        {
            get
            {
                return this._Encoder12;
            }
            set
            {
                this._ChangedButton = BCF2000Buttons.Encoder12;
                this._Encoder12 = value;
                this.outDevice.Send(new ChannelMessage(ChannelCommand.Controller, 11, 8, _Encoder12));
            }
        }
        public int Encoder13
        {
            get
            {
                return this._Encoder13;
            }
            set
            {
                this._ChangedButton = BCF2000Buttons.Encoder13;
                this._Encoder13 = value;
                this.outDevice.Send(new ChannelMessage(ChannelCommand.Controller, 12, 8, _Encoder13));
            }
        }
        public int Encoder14
        {
            get
            {
                return this._Encoder14;
            }
            set
            {
                this._ChangedButton = BCF2000Buttons.Encoder14;
                this._Encoder14 = value;
                this.outDevice.Send(new ChannelMessage(ChannelCommand.Controller, 13, 8, _Encoder14));
            }
        }
        public int Encoder15
        {
            get
            {
                return this._Encoder15;
            }
            set
            {
                this._ChangedButton = BCF2000Buttons.Encoder15;
                this._Encoder15 = value;
                this.outDevice.Send(new ChannelMessage(ChannelCommand.Controller, 14, 8, _Encoder15));
            }
        }
        public int Encoder16
        {
            get
            {
                return this._Encoder16;
            }
            set
            {
                this._ChangedButton = BCF2000Buttons.Encoder16;
                this._Encoder16 = value;
                this.outDevice.Send(new ChannelMessage(ChannelCommand.Controller, 15, 8, _Encoder16));
            }
        }
        #endregion
        #region BCF2000 Routines
        public void BCF2000_Init()
        {
            this.IsInitActivated = true;
            for (int i = 1; i <= 64; i++)
            {
                this.outDevice.Send(new ChannelMessage(ChannelCommand.NoteOn, 0, i, 0));
            }

            this.Fader1 = 0;
            this.Fader2 = 0;
            this.Fader3 = 0;
            this.Fader4 = 0;
            this.Fader5 = 0;
            this.Fader6 = 0;
            this.Fader7 = 0;
            this.Fader8 = 0;
            this.UpperRowKey1 = 0;
            this.UpperRowKey2 = 0;
            this.UpperRowKey3 = 0;
            this.UpperRowKey4 = 0;
            this.UpperRowKey5 = 0;
            this.UpperRowKey6 = 0;
            this.UpperRowKey7 = 0;
            this.UpperRowKey8 = 0;
            this.LowerRowKey1 = 0;
            this.LowerRowKey2 = 0;
            this.LowerRowKey3 = 0;
            this.LowerRowKey4 = 0;
            this.LowerRowKey5 = 0;
            this.LowerRowKey6 = 0;
            this.LowerRowKey7 = 0;
            this.LowerRowKey8 = 0;
            this.FunctionKeyStore = 0;
            this.FunctionKeyLearn = 0;
            this.FunctionKeyEdit = 0;
            this.FunctionKeyExit = 0;
            this.PresetDecr = 0;
            this.PresetIncr = 0;
            this.UserKey1 = 0;
            this.UserKey2 = 0;
            this.UserKey3 = 0;
            this.UserKey4 = 0;
            this.IsInitActivated = false;
        }
        #endregion

        public BCF2000_MIDI_Controller(System.Windows.Forms.Form frm)
        {
            this._thisForm = frm;
        }

        /// <summary>
        /// Get Devices
        /// </summary>
        /// <returns>
        /// True, they are exist
        /// </returns>
        public bool GetDevices()
        {
            return !(InputDevice.DeviceCount == 0);
        }
        public void Connect()
        {
            if (InputDevice.DeviceCount == 0)
            {
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
            #region BCF2000
            switch (e.Message.Command)
            {
                #region Controller
                case ChannelCommand.Controller:
                    switch (e.Message.Data1)
                    {
                        #region Faders
                        case 7: // Faders
                            switch (e.Message.MidiChannel)
                            {
                                case 0:
                                    this._ChangedButton = BCF2000Buttons.Fader1;
                                    this._Fader1 = e.Message.Data2;
                                    this.OnFader1?.Invoke(this, e);
                                    break;

                                case 1:
                                    this._ChangedButton = BCF2000Buttons.Fader2;
                                    this._Fader2 = e.Message.Data2;
                                    this.OnFader2?.Invoke(this, e);
                                    break;

                                case 2:
                                    this._ChangedButton = BCF2000Buttons.Fader3;
                                    this._Fader3 = e.Message.Data2;
                                    this.OnFader3?.Invoke(this, e);
                                    break;

                                case 3:
                                    this._ChangedButton = BCF2000Buttons.Fader4;
                                    this._Fader4 = e.Message.Data2;
                                    this.OnFader4?.Invoke(this, e);
                                    break;

                                case 4:
                                    this._ChangedButton = BCF2000Buttons.Fader5;
                                    this._Fader5 = e.Message.Data2;
                                    this.OnFader5?.Invoke(this, e);
                                    break;

                                case 5:
                                    this._ChangedButton = BCF2000Buttons.Fader6;
                                    this._Fader6 = e.Message.Data2;
                                    this.OnFader6?.Invoke(this, e);
                                    break;

                                case 6:
                                    this._ChangedButton = BCF2000Buttons.Fader7;
                                    this._Fader7 = e.Message.Data2;
                                    this.OnFader7?.Invoke(this, e);
                                    break;

                                case 7:
                                    this._ChangedButton = BCF2000Buttons.Fader8;
                                    this._Fader8 = e.Message.Data2;
                                    this.OnFader8?.Invoke(this, e);
                                    break;

                                default:
                                    break;
                            }
                            this.OnFaders?.Invoke(this, e);
                            break;
                        #endregion
                        #region Encoders
                        case 8: // Encoders
                            switch (e.Message.MidiChannel)
                            {
                                case 0:
                                    this._ChangedButton = BCF2000Buttons.Encoder1;
                                    this._Encoder1 = e.Message.Data2;
                                    this.OnEncoder1?.Invoke(this, e);
                                    break;

                                case 1:
                                    this._ChangedButton = BCF2000Buttons.Encoder2;
                                    this._Encoder2 = e.Message.Data2;
                                    this.OnEncoder2?.Invoke(this, e);
                                    break;

                                case 2:
                                    this._ChangedButton = BCF2000Buttons.Encoder3;
                                    this._Encoder3 = e.Message.Data2;
                                    this.OnEncoder3?.Invoke(this, e);
                                    break;

                                case 3:
                                    this._ChangedButton = BCF2000Buttons.Encoder4;
                                    this._Encoder4 = e.Message.Data2;
                                    this.OnEncoder4?.Invoke(this, e);
                                    break;

                                case 4:
                                    this._ChangedButton = BCF2000Buttons.Encoder5;
                                    this._Encoder5 = e.Message.Data2;
                                    this.OnEncoder5?.Invoke(this, e);
                                    break;

                                case 5:
                                    this._ChangedButton = BCF2000Buttons.Encoder6;
                                    this._Encoder6 = e.Message.Data2;
                                    this.OnEncoder6?.Invoke(this, e);
                                    break;

                                case 6:
                                    this._ChangedButton = BCF2000Buttons.Encoder7;
                                    this._Encoder7 = e.Message.Data2;
                                    this.OnEncoder7?.Invoke(this, e);
                                    break;

                                case 7:
                                    this._ChangedButton = BCF2000Buttons.Encoder8;
                                    this._Encoder8 = e.Message.Data2;
                                    this.OnEncoder8?.Invoke(this, e);
                                    break;

                                case 8:
                                    this._ChangedButton = BCF2000Buttons.Encoder9;
                                    this._Encoder9 = e.Message.Data2;
                                    this.OnEncoder9?.Invoke(this, e);
                                    break;

                                case 9:
                                    this._ChangedButton = BCF2000Buttons.Encoder10;
                                    this._Encoder10 = e.Message.Data2;
                                    this.OnEncoder10?.Invoke(this, e);
                                    break;

                                case 10:
                                    this._ChangedButton = BCF2000Buttons.Encoder11;
                                    this._Encoder11 = e.Message.Data2;
                                    this.OnEncoder11?.Invoke(this, e);
                                    break;

                                case 11:
                                    this._ChangedButton = BCF2000Buttons.Encoder12;
                                    this._Encoder12 = e.Message.Data2;
                                    this.OnEncoder12?.Invoke(this, e);
                                    break;

                                case 12:
                                    this._ChangedButton = BCF2000Buttons.Encoder13;
                                    this._Encoder13 = e.Message.Data2;
                                    this.OnEncoder13?.Invoke(this, e);
                                    break;

                                case 13:
                                    this._ChangedButton = BCF2000Buttons.Encoder14;
                                    this._Encoder14 = e.Message.Data2;
                                    this.OnEncoder14?.Invoke(this, e);
                                    break;

                                case 14:
                                    this._ChangedButton = BCF2000Buttons.Encoder15;
                                    this._Encoder15 = e.Message.Data2;
                                    this.OnEncoder15?.Invoke(this, e);
                                    break;

                                case 15:
                                    this._ChangedButton = BCF2000Buttons.Encoder16;
                                    this._Encoder16 = e.Message.Data2;
                                    this.OnEncoder16?.Invoke(this, e);
                                    break;

                                default:
                                    break;
                            }
                            this.OnEncoders?.Invoke(this, e);
                            break;
                        #endregion

                        default:
                            break;
                    }
                    break;
                #endregion
                #region NoteOn (Keys)
                case ChannelCommand.NoteOn:
                    switch (e.Message.Data1)
                    {
                        #region Functionkeys
                        case 53:        // Functionkey Store
                            this._ChangedButton = BCF2000Buttons.FunctionKeyStore;
                            this._FunctionKeyStore = e.Message.Data2;
                            this.OnFunctionKeyStore?.Invoke(this, e);
                            this.OnFunctionKeys?.Invoke(this, e);
                            break;

                        case 54:        // Functionkey Learn
                            this._ChangedButton = BCF2000Buttons.FunctionKeyLearn;
                            this._FunctionKeyLearn = e.Message.Data2;
                            this.OnFunctionKeyLearn?.Invoke(this, e);
                            this.OnFunctionKeys?.Invoke(this, e);
                            break;

                        case 55:        // Functionkey Edit
                            this._ChangedButton = BCF2000Buttons.FunctionKeyEdit;
                            this._FunctionKeyEdit = e.Message.Data2;
                            this.OnFunctionKeyEdit?.Invoke(this, e);
                            this.OnFunctionKeys?.Invoke(this, e);
                            break;

                        case 56:        // Functionkey Exit
                            this._ChangedButton = BCF2000Buttons.FunctionKeyExit;
                            this._FunctionKeyExit = e.Message.Data2;
                            this.OnFunctionKeyExit?.Invoke(this, e);
                            this.OnFunctionKeys?.Invoke(this, e);
                            break;
                        #endregion
                        #region Encodergroups
                        case 57:        // Encodergroup 1
                            this._ChangedButton = BCF2000Buttons.EncoderGroup1;
                            this._EncoderGroup1 = e.Message.Data2;
                            this.OnEncodergroup1?.Invoke(this, e);
                            this.OnEncodergroups?.Invoke(this, e);
                            break;

                        case 58:        // Encodergroup 2
                            this._ChangedButton = BCF2000Buttons.EncoderGroup2;
                            this._EncoderGroup2 = e.Message.Data2;
                            this.OnEncodergroup2?.Invoke(this, e);
                            this.OnEncodergroups?.Invoke(this, e);
                            break;

                        case 59:        // Encodergroup 3
                            this._ChangedButton = BCF2000Buttons.EncoderGroup3;
                            this._EncoderGroup3 = e.Message.Data2;
                            this.OnEncodergroup3?.Invoke(this, e);
                            this.OnEncodergroups?.Invoke(this, e);
                            break;

                        case 60:        // Encodergroup 4
                            this._ChangedButton = BCF2000Buttons.EncoderGroup4;
                            this._EncoderGroup4 = e.Message.Data2;
                            this.OnEncodergroup4?.Invoke(this, e);
                            this.OnEncodergroups?.Invoke(this, e);
                            break;
                        #endregion
                        #region Foot switch and DYSFUNCTIONAL
                        case 61:        // Foot Switch
                                        // TODO
                            break;

                        case 62:        // DYSFUNCTIONAL
                                        // TODO
                            break;
                        #endregion
                        #region Preset
                        case 63:        // Preset <
                            this._ChangedButton = BCF2000Buttons.PresetDecr;
                            this._PresetDecr = e.Message.Data2;
                            this.OnPresetDecr?.Invoke(this, e);
                            this.OnPreset?.Invoke(this, e);
                            break;

                        case 64:        // Preset >
                            this._ChangedButton = BCF2000Buttons.PresetIncr;
                            this._PresetIncr = e.Message.Data2;
                            this.OnPresetIncr?.Invoke(this, e);
                            this.OnPreset?.Invoke(this, e);
                            break;
                        #endregion

                        default:
                            switch (e.Message.Data1)
                            {
                                #region Push encoder group 1
                                case 1:
                                    this._ChangedButton = BCF2000Buttons.EncoderKey1Group1;
                                    this._EncoderKey1Group1 = e.Message.Data2;
                                    this.OnEncoders?.Invoke(this, e);
                                    break;
                                case 2:
                                    this._ChangedButton = BCF2000Buttons.EncoderKey2Group1;
                                    this._EncoderKey2Group1 = e.Message.Data2;
                                    this.OnEncoders?.Invoke(this, e);
                                    break;
                                case 3:
                                    this._ChangedButton = BCF2000Buttons.EncoderKey3Group1;
                                    this._EncoderKey3Group1 = e.Message.Data2;
                                    this.OnEncoders?.Invoke(this, e);
                                    break;
                                case 4:
                                    this._ChangedButton = BCF2000Buttons.EncoderKey4Group1;
                                    this._EncoderKey4Group1 = e.Message.Data2;
                                    this.OnEncoders?.Invoke(this, e);
                                    break;
                                case 5:
                                    this._ChangedButton = BCF2000Buttons.EncoderKey5Group1;
                                    this._EncoderKey5Group1 = e.Message.Data2;
                                    this.OnEncoders?.Invoke(this, e);
                                    break;
                                case 6:
                                    this._ChangedButton = BCF2000Buttons.EncoderKey6Group1;
                                    this._EncoderKey6Group1 = e.Message.Data2;
                                    this.OnEncoders?.Invoke(this, e);
                                    break;
                                case 7:
                                    this._ChangedButton = BCF2000Buttons.EncoderKey7Group1;
                                    this._EncoderKey7Group1 = e.Message.Data2;
                                    this.OnEncoders?.Invoke(this, e);
                                    break;
                                case 8:
                                    this._ChangedButton = BCF2000Buttons.EncoderKey8Group1;
                                    this._EncoderKey8Group1 = e.Message.Data2;
                                    this.OnEncoders?.Invoke(this, e);
                                    break;
                                #endregion
                                #region Push encoder group 2
                                case 9:
                                    this._ChangedButton = BCF2000Buttons.EncoderKey1Group2;
                                    this._EncoderKey1Group2 = e.Message.Data2;
                                    this.OnEncoders?.Invoke(this, e);
                                    break;
                                case 10:
                                    this._ChangedButton = BCF2000Buttons.EncoderKey2Group2;
                                    this._EncoderKey2Group2 = e.Message.Data2;
                                    this.OnEncoders?.Invoke(this, e);
                                    break;
                                case 11:
                                    this._ChangedButton = BCF2000Buttons.EncoderKey3Group2;
                                    this._EncoderKey3Group2 = e.Message.Data2;
                                    this.OnEncoders?.Invoke(this, e);
                                    break;
                                case 12:
                                    this._ChangedButton = BCF2000Buttons.EncoderKey4Group2;
                                    this._EncoderKey4Group2 = e.Message.Data2;
                                    this.OnEncoders?.Invoke(this, e);
                                    break;
                                case 13:
                                    this._ChangedButton = BCF2000Buttons.EncoderKey5Group2;
                                    this._EncoderKey5Group2 = e.Message.Data2;
                                    this.OnEncoders?.Invoke(this, e);
                                    break;
                                case 14:
                                    this._ChangedButton = BCF2000Buttons.EncoderKey6Group2;
                                    this._EncoderKey6Group2 = e.Message.Data2;
                                    this.OnEncoders?.Invoke(this, e);
                                    break;
                                case 15:
                                    this._ChangedButton = BCF2000Buttons.EncoderKey7Group2;
                                    this._EncoderKey7Group2 = e.Message.Data2;
                                    this.OnEncoders?.Invoke(this, e);
                                    break;
                                case 16:
                                    this._ChangedButton = BCF2000Buttons.EncoderKey8Group2;
                                    this._EncoderKey8Group2 = e.Message.Data2;
                                    this.OnEncoders?.Invoke(this, e);
                                    break;
                                #endregion
                                #region Push encoder group 3
                                case 17:
                                    this._ChangedButton = BCF2000Buttons.EncoderKey1Group3;
                                    this._EncoderKey1Group3 = e.Message.Data2;
                                    this.OnEncoders?.Invoke(this, e);
                                    break;
                                case 18:
                                    this._ChangedButton = BCF2000Buttons.EncoderKey2Group3;
                                    this._EncoderKey2Group3 = e.Message.Data2;
                                    this.OnEncoders?.Invoke(this, e);
                                    break;
                                case 19:
                                    this._ChangedButton = BCF2000Buttons.EncoderKey3Group3;
                                    this._EncoderKey3Group3 = e.Message.Data2;
                                    this.OnEncoders?.Invoke(this, e);
                                    break;
                                case 20:
                                    this._ChangedButton = BCF2000Buttons.EncoderKey4Group3;
                                    this._EncoderKey4Group3 = e.Message.Data2;
                                    this.OnEncoders?.Invoke(this, e);
                                    break;
                                case 21:
                                    this._ChangedButton = BCF2000Buttons.EncoderKey5Group3;
                                    this._EncoderKey5Group3 = e.Message.Data2;
                                    this.OnEncoders?.Invoke(this, e);
                                    break;
                                case 22:
                                    this._ChangedButton = BCF2000Buttons.EncoderKey6Group3;
                                    this._EncoderKey6Group3 = e.Message.Data2;
                                    this.OnEncoders?.Invoke(this, e);
                                    break;
                                case 23:
                                    this._ChangedButton = BCF2000Buttons.EncoderKey7Group3;
                                    this._EncoderKey7Group3 = e.Message.Data2;
                                    this.OnEncoders?.Invoke(this, e);
                                    break;
                                case 24:
                                    this._ChangedButton = BCF2000Buttons.EncoderKey8Group3;
                                    this._EncoderKey8Group3 = e.Message.Data2;
                                    this.OnEncoders?.Invoke(this, e);
                                    break;
                                #endregion
                                #region Push encoder group 4
                                case 25:
                                    this._ChangedButton = BCF2000Buttons.EncoderKey1Group4;
                                    this._EncoderKey1Group4 = e.Message.Data2;
                                    this.OnEncoders?.Invoke(this, e);
                                    break;
                                case 26:
                                    this._ChangedButton = BCF2000Buttons.EncoderKey2Group4;
                                    this._EncoderKey2Group4 = e.Message.Data2;
                                    this.OnEncoders?.Invoke(this, e);
                                    break;
                                case 27:
                                    this._ChangedButton = BCF2000Buttons.EncoderKey3Group4;
                                    this._EncoderKey3Group4 = e.Message.Data2;
                                    this.OnEncoders?.Invoke(this, e);
                                    break;
                                case 28:
                                    this._ChangedButton = BCF2000Buttons.EncoderKey4Group4;
                                    this._EncoderKey4Group4 = e.Message.Data2;
                                    this.OnEncoders?.Invoke(this, e);
                                    break;
                                case 29:
                                    this._ChangedButton = BCF2000Buttons.EncoderKey5Group4;
                                    this._EncoderKey5Group4 = e.Message.Data2;
                                    this.OnEncoders?.Invoke(this, e);
                                    break;
                                case 30:
                                    this._ChangedButton = BCF2000Buttons.EncoderKey6Group4;
                                    this._EncoderKey6Group4 = e.Message.Data2;
                                    this.OnEncoders?.Invoke(this, e);
                                    break;
                                case 31:
                                    this._ChangedButton = BCF2000Buttons.EncoderKey7Group4;
                                    this._EncoderKey7Group4 = e.Message.Data2;
                                    this.OnEncoders?.Invoke(this, e);
                                    break;
                                case 32:
                                    this._ChangedButton = BCF2000Buttons.EncoderKey8Group4;
                                    this._EncoderKey8Group4 = e.Message.Data2;
                                    this.OnEncoders?.Invoke(this, e);
                                    break;
                                #endregion
                                #region Upper row keys
                                case 33:
                                    this._ChangedButton = BCF2000Buttons.UpperRowKey1;
                                    this._UpperRowKey1 = e.Message.Data2;
                                    this.OnUpperRowKey1?.Invoke(this, e);
                                    this.OnUpperRowKeys?.Invoke(this, e);
                                    break;
                                case 34:
                                    this._ChangedButton = BCF2000Buttons.UpperRowKey2;
                                    this._UpperRowKey2 = e.Message.Data2;
                                    this.OnUpperRowKey2?.Invoke(this, e);
                                    this.OnUpperRowKeys?.Invoke(this, e);
                                    break;
                                case 35:
                                    this._ChangedButton = BCF2000Buttons.UpperRowKey3;
                                    this._UpperRowKey3 = e.Message.Data2;
                                    this.OnUpperRowKey3?.Invoke(this, e);
                                    this.OnUpperRowKeys?.Invoke(this, e);
                                    break;
                                case 36:
                                    this._ChangedButton = BCF2000Buttons.UpperRowKey4;
                                    this._UpperRowKey4 = e.Message.Data2;
                                    this.OnUpperRowKey4?.Invoke(this, e);
                                    this.OnUpperRowKeys?.Invoke(this, e);
                                    break;
                                case 37:
                                    this._ChangedButton = BCF2000Buttons.UpperRowKey5;
                                    this._UpperRowKey5 = e.Message.Data2;
                                    this.OnUpperRowKey5?.Invoke(this, e);
                                    this.OnUpperRowKeys?.Invoke(this, e);
                                    break;
                                case 38:
                                    this._ChangedButton = BCF2000Buttons.UpperRowKey6;
                                    this._UpperRowKey6 = e.Message.Data2;
                                    this.OnUpperRowKey6?.Invoke(this, e);
                                    this.OnUpperRowKeys?.Invoke(this, e);
                                    break;
                                case 39:
                                    this._ChangedButton = BCF2000Buttons.UpperRowKey7;
                                    this._UpperRowKey7 = e.Message.Data2;
                                    this.OnUpperRowKey7?.Invoke(this, e);
                                    this.OnUpperRowKeys?.Invoke(this, e);
                                    break;
                                case 40:
                                    this._ChangedButton = BCF2000Buttons.UpperRowKey8;
                                    this._UpperRowKey8 = e.Message.Data2;
                                    this.OnUpperRowKey8?.Invoke(this, e);
                                    this.OnUpperRowKeys?.Invoke(this, e);
                                    break;
                                #endregion
                                #region Lower row keys
                                case 41:
                                    this._ChangedButton = BCF2000Buttons.LowerRowKey1;
                                    this._LowerRowKey1 = e.Message.Data2;
                                    this.OnLowerRowKey1?.Invoke(this, e);
                                    this.OnLowerRowKeys?.Invoke(this, e);
                                    break;
                                case 42:
                                    this._ChangedButton = BCF2000Buttons.LowerRowKey2;
                                    this._LowerRowKey2 = e.Message.Data2;
                                    this.OnLowerRowKey2?.Invoke(this, e);
                                    this.OnLowerRowKeys?.Invoke(this, e);
                                    break;
                                case 43:
                                    this._ChangedButton = BCF2000Buttons.LowerRowKey3;
                                    this._LowerRowKey3 = e.Message.Data2;
                                    this.OnLowerRowKey3?.Invoke(this, e);
                                    this.OnLowerRowKeys?.Invoke(this, e);
                                    break;
                                case 44:
                                    this._ChangedButton = BCF2000Buttons.LowerRowKey4;
                                    this._LowerRowKey4 = e.Message.Data2;
                                    this.OnLowerRowKey4?.Invoke(this, e);
                                    this.OnLowerRowKeys?.Invoke(this, e);
                                    break;
                                case 45:
                                    this._ChangedButton = BCF2000Buttons.LowerRowKey5;
                                    this._LowerRowKey5 = e.Message.Data2;
                                    this.OnLowerRowKey5?.Invoke(this, e);
                                    this.OnLowerRowKeys?.Invoke(this, e);
                                    break;
                                case 46:
                                    this._ChangedButton = BCF2000Buttons.LowerRowKey6;
                                    this._LowerRowKey6 = e.Message.Data2;
                                    this.OnLowerRowKey6?.Invoke(this, e);
                                    this.OnLowerRowKeys?.Invoke(this, e);
                                    break;
                                case 47:
                                    this._ChangedButton = BCF2000Buttons.LowerRowKey7;
                                    this._LowerRowKey7 = e.Message.Data2;
                                    this.OnLowerRowKey7?.Invoke(this, e);
                                    this.OnLowerRowKeys?.Invoke(this, e);
                                    break;
                                case 48:
                                    this._ChangedButton = BCF2000Buttons.LowerRowKey8;
                                    this._LowerRowKey8 = e.Message.Data2;
                                    this.OnLowerRowKey8?.Invoke(this, e);
                                    this.OnLowerRowKeys?.Invoke(this, e);
                                    break;
                                #endregion
                                #region User keys
                                case 49:
                                    this._ChangedButton = BCF2000Buttons.UserKey1;
                                    this._UserKey1 = e.Message.Data2;
                                    this.OnUserKey1?.Invoke(this, e);
                                    this.OnUserKeys?.Invoke(this, e);
                                    break;
                                case 50:
                                    this._ChangedButton = BCF2000Buttons.UserKey2;
                                    this._UserKey2 = e.Message.Data2;
                                    this.OnUserKey2?.Invoke(this, e);
                                    this.OnUserKeys?.Invoke(this, e);
                                    break;
                                case 51:
                                    this._ChangedButton = BCF2000Buttons.UserKey3;
                                    this._UserKey3 = e.Message.Data2;
                                    this.OnUserKey3?.Invoke(this, e);
                                    this.OnUserKeys?.Invoke(this, e);
                                    break;
                                case 52:
                                    this._ChangedButton = BCF2000Buttons.UserKey4;
                                    this._UserKey4 = e.Message.Data2;
                                    this.OnUserKey4?.Invoke(this, e);
                                    this.OnUserKeys?.Invoke(this, e);
                                    break;
                                    #endregion
                            }
                            break;
                    }
                    break;
                #endregion
                default:
                    break;
            }
            this.OnChannelMessageReceived?.Invoke(this, e);
            #endregion
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
