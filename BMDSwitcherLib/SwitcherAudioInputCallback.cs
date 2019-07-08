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
    public class SwitcherAudioInputEventArgs : EventArgs
    {
    }
    public class SwitcherAudioInputLevelNotificationEventArgs : EventArgs
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
    public delegate void SwitcherAudioInputEventHandler(SwitcherAudioInputCallback s, SwitcherAudioInputEventArgs a);
    public delegate void SwitcherAudioInputLevelNotificationEventHandler(SwitcherAudioInputCallback s, SwitcherAudioInputLevelNotificationEventArgs a);

    public class SwitcherAudioInputCallback : IBMDSwitcherAudioInputCallback
    {
        // Events:
        public event SwitcherAudioInputLevelNotificationEventHandler LevelNotificationChanged;
        public event SwitcherAudioInputEventHandler SwitcherAudioInputEventTypeBalanceChanged;
        public event SwitcherAudioInputEventHandler SwitcherAudioInputEventTypeCurrentExternalPortTypeChanged;
        public event SwitcherAudioInputEventHandler SwitcherAudioInputEventTypeGainChanged;
        public event SwitcherAudioInputEventHandler SwitcherAudioInputEventTypeIsMixedInChanged;
        public event SwitcherAudioInputEventHandler SwitcherAudioInputEventTypeMixOptionChanged;

        private SwitcherAudioInputEventArgs _switcherAudioInputEventArgs;
        private SwitcherAudioInputLevelNotificationEventArgs _switcherAudioInputLevelNotificationEventArgs;

        private int _indexnr;
        internal IBMDSwitcherAudioInput AudioInput;
        internal SwitcherAudioInputCallback(IBMDSwitcherAudioInput audioInput, int index)
        {
            this._indexnr = index;
            this.AudioInput = audioInput;
        }

        void IBMDSwitcherAudioInputCallback.LevelNotification(double left, double right, double peakLeft, double peakRight)
        {
            this._switcherAudioInputLevelNotificationEventArgs = new SwitcherAudioInputLevelNotificationEventArgs { _left = left, _right = right, _peakLeft = peakLeft, _peakRight = peakRight };
            this.LevelNotificationChanged?.Invoke(this, this._switcherAudioInputLevelNotificationEventArgs);
        }
        void IBMDSwitcherAudioInputCallback.Notify(_BMDSwitcherAudioInputEventType eventType)
        {
            this._switcherAudioInputEventArgs = new SwitcherAudioInputEventArgs();
            switch (eventType)
            {
                case _BMDSwitcherAudioInputEventType.bmdSwitcherAudioInputEventTypeBalanceChanged:
                    this.SwitcherAudioInputEventTypeBalanceChanged?.Invoke(this, this._switcherAudioInputEventArgs);
                    break;
                case _BMDSwitcherAudioInputEventType.bmdSwitcherAudioInputEventTypeCurrentExternalPortTypeChanged:
                    this.SwitcherAudioInputEventTypeCurrentExternalPortTypeChanged?.Invoke(this, this._switcherAudioInputEventArgs);
                    break;
                case _BMDSwitcherAudioInputEventType.bmdSwitcherAudioInputEventTypeGainChanged:
                    this.SwitcherAudioInputEventTypeGainChanged?.Invoke(this, this._switcherAudioInputEventArgs);
                    break;
                case _BMDSwitcherAudioInputEventType.bmdSwitcherAudioInputEventTypeIsMixedInChanged:
                    this.SwitcherAudioInputEventTypeIsMixedInChanged?.Invoke(this, this._switcherAudioInputEventArgs);
                    break;
                case _BMDSwitcherAudioInputEventType.bmdSwitcherAudioInputEventTypeMixOptionChanged:
                    this.SwitcherAudioInputEventTypeMixOptionChanged?.Invoke(this, this._switcherAudioInputEventArgs);
                    break;
            }
        }

        private long _audioInputId;
        private double _balance;
        private _BMDSwitcherExternalPortType _externalPortType;
        private double _gain;
        private _BMDSwitcherAudioMixOption _mixOption;
        private _BMDSwitcherAudioInputType _audioInputType;
        private int _mixedIn;

        public int IndexNr
        {
            get
            {
                return this._indexnr;
            }
        }
        public long AudioInputId
        {
            get
            {
                this.AudioInput.GetAudioInputId(out this._audioInputId);
                return this._audioInputId;
            }
        }
        public double Balance
        {
            get
            {
                this.AudioInput.GetBalance(out this._balance);
                return this._balance;
            }
            set
            {
                this.AudioInput.SetBalance(value);
            }
        }
        public _BMDSwitcherExternalPortType CurrentExternalPortType
        {
            get
            {
                this.AudioInput.GetCurrentExternalPortType(out this._externalPortType);
                return this._externalPortType;
            }
        }
        public double Gain
        {
            get
            {
                this.AudioInput.GetGain(out this._gain);
                return this._gain;
            }
            set
            {
                this.AudioInput.SetGain(value);
            }
        }
        public _BMDSwitcherAudioMixOption MixOption
        {
            get
            {
                this.AudioInput.GetMixOption(out this._mixOption);
                return this._mixOption;
            }
            set
            {
                this.AudioInput.SetMixOption(value);
            }
        }
        public _BMDSwitcherAudioInputType Type
        {
            get
            {
                this.AudioInput.GetType(out this._audioInputType);
                return this._audioInputType;
            }
        }
        public int IsMixedIn
        {
            get
            {
                this.AudioInput.IsMixedIn(out this._mixedIn);
                return this._mixedIn;
            }
        }
        public void ResetLevelNotificationPeaks()
        {
            this.AudioInput.ResetLevelNotificationPeaks();
        }
    }
}