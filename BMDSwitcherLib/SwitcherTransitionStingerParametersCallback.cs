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
    public class SwitcherTransitionStingerParametersEventArgs : EventArgs
    {
    }
    public delegate void SwitcherTransitionStingerParametersEventHandler(SwitcherTransitionStingerParametersCallback s, SwitcherTransitionStingerParametersEventArgs a);

    public class SwitcherTransitionStingerParametersCallback : IBMDSwitcherTransitionStingerParametersCallback
    {
        // Events:
        public event SwitcherTransitionStingerParametersEventHandler SwitcherTransitionStingerParametersEventTypeClipChanged;
        public event SwitcherTransitionStingerParametersEventHandler SwitcherTransitionStingerParametersEventTypeClipDurationChanged;
        public event SwitcherTransitionStingerParametersEventHandler SwitcherTransitionStingerParametersEventTypeGainChanged;
        public event SwitcherTransitionStingerParametersEventHandler SwitcherTransitionStingerParametersEventTypeInverseChanged;
        public event SwitcherTransitionStingerParametersEventHandler SwitcherTransitionStingerParametersEventTypeMixRateChanged;
        public event SwitcherTransitionStingerParametersEventHandler SwitcherTransitionStingerParametersEventTypePreMultipliedChanged;
        public event SwitcherTransitionStingerParametersEventHandler SwitcherTransitionStingerParametersEventTypePrerollChanged;
        public event SwitcherTransitionStingerParametersEventHandler SwitcherTransitionStingerParametersEventTypeSourceChanged;
        public event SwitcherTransitionStingerParametersEventHandler SwitcherTransitionStingerParametersEventTypeTriggerPointChanged;

        private SwitcherTransitionStingerParametersEventArgs _switcherTransitionStingerParametersEventArgs;

        internal IBMDSwitcherTransitionStingerParameters TransitionStingerParameters;
        internal SwitcherTransitionStingerParametersCallback(IBMDSwitcherTransitionStingerParameters transitionStingerParameters)
        {
            this.TransitionStingerParameters = transitionStingerParameters;
        }

        void IBMDSwitcherTransitionStingerParametersCallback.Notify(_BMDSwitcherTransitionStingerParametersEventType eventType)
        {
            this._switcherTransitionStingerParametersEventArgs = new SwitcherTransitionStingerParametersEventArgs();
            switch (eventType)
            {
                case _BMDSwitcherTransitionStingerParametersEventType.bmdSwitcherTransitionStingerParametersEventTypeClipChanged:
                    SwitcherTransitionStingerParametersEventTypeClipChanged?.Invoke(this, this._switcherTransitionStingerParametersEventArgs);
                    break;
                case _BMDSwitcherTransitionStingerParametersEventType.bmdSwitcherTransitionStingerParametersEventTypeClipDurationChanged:
                    SwitcherTransitionStingerParametersEventTypeClipDurationChanged?.Invoke(this, this._switcherTransitionStingerParametersEventArgs);
                    break;
                case _BMDSwitcherTransitionStingerParametersEventType.bmdSwitcherTransitionStingerParametersEventTypeGainChanged:
                    SwitcherTransitionStingerParametersEventTypeGainChanged?.Invoke(this, this._switcherTransitionStingerParametersEventArgs);
                    break;
                case _BMDSwitcherTransitionStingerParametersEventType.bmdSwitcherTransitionStingerParametersEventTypeInverseChanged:
                    SwitcherTransitionStingerParametersEventTypeInverseChanged?.Invoke(this, this._switcherTransitionStingerParametersEventArgs);
                    break;
                case _BMDSwitcherTransitionStingerParametersEventType.bmdSwitcherTransitionStingerParametersEventTypeMixRateChanged:
                    SwitcherTransitionStingerParametersEventTypeMixRateChanged?.Invoke(this, this._switcherTransitionStingerParametersEventArgs);
                    break;
                case _BMDSwitcherTransitionStingerParametersEventType.bmdSwitcherTransitionStingerParametersEventTypePreMultipliedChanged:
                    SwitcherTransitionStingerParametersEventTypePreMultipliedChanged?.Invoke(this, this._switcherTransitionStingerParametersEventArgs);
                    break;
                case _BMDSwitcherTransitionStingerParametersEventType.bmdSwitcherTransitionStingerParametersEventTypePrerollChanged:
                    SwitcherTransitionStingerParametersEventTypePrerollChanged?.Invoke(this, this._switcherTransitionStingerParametersEventArgs);
                    break;
                case _BMDSwitcherTransitionStingerParametersEventType.bmdSwitcherTransitionStingerParametersEventTypeSourceChanged:
                    SwitcherTransitionStingerParametersEventTypeSourceChanged?.Invoke(this, this._switcherTransitionStingerParametersEventArgs);
                    break;
                case _BMDSwitcherTransitionStingerParametersEventType.bmdSwitcherTransitionStingerParametersEventTypeTriggerPointChanged:
                    SwitcherTransitionStingerParametersEventTypeTriggerPointChanged?.Invoke(this, this._switcherTransitionStingerParametersEventArgs);
                    break;
            }
        }

        private double _clip;
        private uint _clipDurationFrames;
        private double _gain;
        private int _inverse;
        private uint _mixRateFrames;
        private int _preMultiplied;
        private uint _prerollFrames;
        private _BMDSwitcherStingerTransitionSource _src;
        private uint _triggerPointFrames;

        public double Clip
        {
            get
            {
                this.TransitionStingerParameters.GetClip(out this._clip);
                return this._clip;
            }
            set
            {
                this.TransitionStingerParameters.SetClip(value);
            }
        }
        public uint ClipDuration
        {
            get
            {
                this.TransitionStingerParameters.GetClipDuration(out this._clipDurationFrames);
                return this._clipDurationFrames;
            }
            set
            {
                this.TransitionStingerParameters.SetClipDuration(value);
            }
        }
        public double Gain
        {
            get
            {
                this.TransitionStingerParameters.GetGain(out this._gain);
                return this._gain;
            }
            set
            {
                this.TransitionStingerParameters.SetGain(value);
            }
        }
        public int Inverse
        {
            get
            {
                this.TransitionStingerParameters.GetInverse(out this._inverse);
                return this._inverse;
            }
            set
            {
                this.TransitionStingerParameters.SetInverse(value);
            }
        }
        public uint MixRate
        {
            get
            {
                this.TransitionStingerParameters.GetMixRate(out this._mixRateFrames);
                return this._mixRateFrames;
            }
            set
            {
                this.TransitionStingerParameters.SetMixRate(value);
            }
        }
        public int PreMultiplied
        {
            get
            {
                this.TransitionStingerParameters.GetPreMultiplied(out this._preMultiplied);
                return this._preMultiplied;
            }
            set
            {
                this.TransitionStingerParameters.SetPreMultiplied(value);
            }
        }
        public uint Preroll
        {
            get
            {
                this.TransitionStingerParameters.GetPreroll(out this._prerollFrames);
                return this._prerollFrames;
            }
            set
            {
                this.TransitionStingerParameters.SetPreroll(value);
            }
        }
        public _BMDSwitcherStingerTransitionSource Source
        {
            get
            {
                this.TransitionStingerParameters.GetSource(out this._src);
                return this._src;
            }
            set
            {
                this.TransitionStingerParameters.SetSource(value);
            }
        }
        public uint TriggerPoint
        {
            get
            {
                this.TransitionStingerParameters.GetTriggerPoint(out this._triggerPointFrames);
                return this._triggerPointFrames;
            }
            set
            {
                this.TransitionStingerParameters.SetTriggerPoint(value);
            }
        }
    }
}
