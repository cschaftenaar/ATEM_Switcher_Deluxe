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
    public class SwitcherAudioMixerEventArgs : EventArgs
    {
    }
    public class SwitcherAudioMixerProgramOutLevelNotificationEventArgs : EventArgs
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
    public delegate void SwitcherAudioMixerEventHandler(SwitcherAudioMixerCallback s, SwitcherAudioMixerEventArgs a);
    public delegate void SwitcherAudioMixerProgramOutLevelNotificationEventHandler(SwitcherAudioMixerCallback s, SwitcherAudioMixerProgramOutLevelNotificationEventArgs a);

    public class SwitcherAudioMixerCallback : IBMDSwitcherAudioMixerCallback
    {
        // Events:
        public event SwitcherAudioMixerProgramOutLevelNotificationEventHandler ProgramOutLevelNotification;
        public event SwitcherAudioMixerEventHandler SwitcherAudioMixerEventTypeProgramOutBalanceChanged;
        public event SwitcherAudioMixerEventHandler SwitcherAudioMixerEventTypeProgramOutFollowFadeToBlackChanged;
        public event SwitcherAudioMixerEventHandler SwitcherAudioMixerEventTypeProgramOutGainChanged;

        private SwitcherAudioMixerEventArgs _switcherAudioMixerEventArgs;
        private SwitcherAudioMixerProgramOutLevelNotificationEventArgs _switcherAudioMixerProgramOutLevelNotificationEventArgs;

        internal IBMDSwitcherAudioMixer AudioMixer;
        internal SwitcherAudioMixerCallback(IBMDSwitcherAudioMixer mixer)
        {
            this.AudioMixer = mixer;
        }

        void IBMDSwitcherAudioMixerCallback.ProgramOutLevelNotification(double left, double right, double peakLeft, double peakRight)
        {
            this._switcherAudioMixerProgramOutLevelNotificationEventArgs = new SwitcherAudioMixerProgramOutLevelNotificationEventArgs{ _left = left, _right = right, _peakLeft = peakLeft, _peakRight = peakRight };
            this.ProgramOutLevelNotification?.Invoke(this, this._switcherAudioMixerProgramOutLevelNotificationEventArgs);
        }
        void IBMDSwitcherAudioMixerCallback.Notify(_BMDSwitcherAudioMixerEventType eventType)
        {
            this._switcherAudioMixerEventArgs = new SwitcherAudioMixerEventArgs();
            switch(eventType)
            {
                case _BMDSwitcherAudioMixerEventType.bmdSwitcherAudioMixerEventTypeProgramOutBalanceChanged:
                    this.SwitcherAudioMixerEventTypeProgramOutBalanceChanged?.Invoke(this, this._switcherAudioMixerEventArgs);
                    break;
                case _BMDSwitcherAudioMixerEventType.bmdSwitcherAudioMixerEventTypeProgramOutFollowFadeToBlackChanged:
                    this.SwitcherAudioMixerEventTypeProgramOutFollowFadeToBlackChanged?.Invoke(this, this._switcherAudioMixerEventArgs);
                    break;
                case _BMDSwitcherAudioMixerEventType.bmdSwitcherAudioMixerEventTypeProgramOutGainChanged:
                    this.SwitcherAudioMixerEventTypeProgramOutGainChanged?.Invoke(this, this._switcherAudioMixerEventArgs);
                    break;
            }

        }

        private double _balance;
        private int _follow;
        private double _gain;

        public double ProgramOutBalance
        {
            get
            {
                this.AudioMixer.GetProgramOutBalance(out this._balance);
                return this._balance;
            }
            set
            {
                this.AudioMixer.SetProgramOutBalance(value);
            }
        }
        public int ProgramOutFollowFadeToBlack
        {
            get
            {
                this.AudioMixer.GetProgramOutFollowFadeToBlack(out this._follow);
                return this._follow;
            }
            set
            {
                this.AudioMixer.SetProgramOutFollowFadeToBlack(value);
            }
        }
        public double ProgramOutGain
        {
            get
            {
                this.AudioMixer.GetProgramOutGain(out this._gain);
                return this._gain;
            }
            set
            {
                this.AudioMixer.SetProgramOutGain(value);
            }
        }
        public void ResetAllLevelNotificationPeaks()
        {
            this.AudioMixer.ResetAllLevelNotificationPeaks();
        }
        public void ResetProgramOutLevelNotificationPeaks()
        {
            this.AudioMixer.ResetProgramOutLevelNotificationPeaks();
        }
        public int AllLevelNotificationsEnable
        {
            set
            {
                this.AudioMixer.SetAllLevelNotificationsEnable(value);
            }
        }
    }
}
