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

using BMDSwitcherAPI;
using System;

namespace BMDSwitcherLib
{
    public class SwitcherAudioMonitorOutputEventArgs : EventArgs
    {
    }
    public class SwitcherAudioMonitorOutputLevelNotificationEventArgs : EventArgs
    {
        internal double _left;
        internal double _right;
        internal double _peakLeft;
        internal double _peakRight;
        public double Left
        {
            get
            {
                return this._left;
            }
        }
        public double Right
        {
            get
            {
                return this._right;
            }
        }
        public double PeakLeft
        {
            get
            {
                return this._peakLeft;
            }
        }
        public double PeakRight
        {
            get
            {
                return this._peakRight;
            }
        }
    }
    public delegate void SwitcherAudioMonitorOutputEventHandler(SwitcherAudioMonitorOutputCallback s, SwitcherAudioMonitorOutputEventArgs a);
    public delegate void SwitcherAudioMonitorOutputLevelNotificationEventHandler(SwitcherAudioMonitorOutputCallback s, SwitcherAudioMonitorOutputLevelNotificationEventArgs a);

    public class SwitcherAudioMonitorOutputCallback : IBMDSwitcherAudioMonitorOutputCallback
    {
        // Events:
        public event SwitcherAudioMonitorOutputLevelNotificationEventHandler LevelNotification;
        public event SwitcherAudioMonitorOutputEventHandler SwitcherAudioMonitorOutputEventTypeDimChanged;
        public event SwitcherAudioMonitorOutputEventHandler SwitcherAudioMonitorOutputEventTypeDimLevelChanged;
        public event SwitcherAudioMonitorOutputEventHandler SwitcherAudioMonitorOutputEventTypeGainChanged;
        public event SwitcherAudioMonitorOutputEventHandler SwitcherAudioMonitorOutputEventTypeMonitorEnableChanged;
        public event SwitcherAudioMonitorOutputEventHandler SwitcherAudioMonitorOutputEventTypeMuteChanged;
        public event SwitcherAudioMonitorOutputEventHandler SwitcherAudioMonitorOutputEventTypeSoloChanged;
        public event SwitcherAudioMonitorOutputEventHandler SwitcherAudioMonitorOutputEventTypeSoloInputChanged;

        private SwitcherAudioMonitorOutputEventArgs _switcherAudioMonitorOutputEventArgs;
        private SwitcherAudioMonitorOutputLevelNotificationEventArgs _switcherAudioMonitorOutputLevelNotificationEventArgs;

        private int _indexnr;
        internal IBMDSwitcherAudioMonitorOutput AudioMonitorOutput;
        internal SwitcherAudioMonitorOutputCallback(IBMDSwitcherAudioMonitorOutput output, int index)
        {
            this._indexnr = index;
            this.AudioMonitorOutput = output;
        }

        void IBMDSwitcherAudioMonitorOutputCallback.LevelNotification(double left, double right, double peakLeft, double peakRight)
        {
            this._switcherAudioMonitorOutputLevelNotificationEventArgs = new SwitcherAudioMonitorOutputLevelNotificationEventArgs{ _left = left, _right = right, _peakLeft = peakLeft, _peakRight = peakRight };
            this.LevelNotification?.Invoke(this, this._switcherAudioMonitorOutputLevelNotificationEventArgs);
        }
        void IBMDSwitcherAudioMonitorOutputCallback.Notify(_BMDSwitcherAudioMonitorOutputEventType eventType)
        {
            this._switcherAudioMonitorOutputEventArgs = new SwitcherAudioMonitorOutputEventArgs();
            switch(eventType)
            {
                case _BMDSwitcherAudioMonitorOutputEventType.bmdSwitcherAudioMonitorOutputEventTypeDimChanged:
                    this.SwitcherAudioMonitorOutputEventTypeDimChanged?.Invoke(this, this._switcherAudioMonitorOutputEventArgs);
                    break;
                case _BMDSwitcherAudioMonitorOutputEventType.bmdSwitcherAudioMonitorOutputEventTypeDimLevelChanged:
                    this.SwitcherAudioMonitorOutputEventTypeDimLevelChanged?.Invoke(this, this._switcherAudioMonitorOutputEventArgs);
                    break;
                case _BMDSwitcherAudioMonitorOutputEventType.bmdSwitcherAudioMonitorOutputEventTypeGainChanged:
                    this.SwitcherAudioMonitorOutputEventTypeGainChanged?.Invoke(this, this._switcherAudioMonitorOutputEventArgs);
                    break;
                case _BMDSwitcherAudioMonitorOutputEventType.bmdSwitcherAudioMonitorOutputEventTypeMonitorEnableChanged:
                    this.SwitcherAudioMonitorOutputEventTypeMonitorEnableChanged?.Invoke(this, this._switcherAudioMonitorOutputEventArgs);
                    break;
                case _BMDSwitcherAudioMonitorOutputEventType.bmdSwitcherAudioMonitorOutputEventTypeMuteChanged:
                    this.SwitcherAudioMonitorOutputEventTypeMuteChanged?.Invoke(this, this._switcherAudioMonitorOutputEventArgs);
                    break;
                case _BMDSwitcherAudioMonitorOutputEventType.bmdSwitcherAudioMonitorOutputEventTypeSoloChanged:
                    this.SwitcherAudioMonitorOutputEventTypeSoloChanged?.Invoke(this, this._switcherAudioMonitorOutputEventArgs);
                    break;
                case _BMDSwitcherAudioMonitorOutputEventType.bmdSwitcherAudioMonitorOutputEventTypeSoloInputChanged:
                    this.SwitcherAudioMonitorOutputEventTypeSoloInputChanged?.Invoke(this, this._switcherAudioMonitorOutputEventArgs);
                    break;
            }
        }

        private int _dim;
        private double _dimGain;
        private double _gain;
        private int _enable;
        private int _mute;
        private int _solo;
        private long _audioInput;

        public int IndexNr
        {
            get
            {
                return this._indexnr;
            }
        }
        public int Dim
        {
            get
            {
                this.AudioMonitorOutput.GetDim(out this._dim);
                return this._dim;
            }
            set
            {
                this.AudioMonitorOutput.SetDim(value);
            }
        }
        public double DimLevel
        {
            get
            {
                this.AudioMonitorOutput.GetDimLevel(out this._dimGain);
                return this._dimGain;
            }
            set
            {
                this.AudioMonitorOutput.SetDimLevel(value);
            }
        }
        public double Gain
        {
            get
            {
                this.AudioMonitorOutput.GetGain(out this._gain);
                return this._gain;
            }
            set
            {
                this.AudioMonitorOutput.SetGain(value);
            }
        }
        public int MonitorEnable
        {
            get
            {
                this.AudioMonitorOutput.GetMonitorEnable(out this._enable);
                return this._enable;
            }
            set
            {
                this.AudioMonitorOutput.SetMonitorEnable(value);
            }
        }
        public int Mute
        {
            get
            {
                this.AudioMonitorOutput.GetMute(out this._mute);
                return this._mute;
            }
            set
            {
                this.AudioMonitorOutput.SetMute(value);
            }
        }
        public int Solo
        {
            get
            {
                this.AudioMonitorOutput.GetSolo(out this._solo);
                return this._solo;
            }
            set
            {
                this.AudioMonitorOutput.SetSolo(value);
            }
        }
        public long SoloInput
        {
            get
            {
                this.AudioMonitorOutput.GetSoloInput(out this._audioInput);
                return this._audioInput;
            }
            set
            {
                this.AudioMonitorOutput.SetSoloInput(value);
            }
        }
        public void ResetLevelNotificationPeaks()
        {
            this.AudioMonitorOutput.ResetLevelNotificationPeaks();
        }
    }
}
