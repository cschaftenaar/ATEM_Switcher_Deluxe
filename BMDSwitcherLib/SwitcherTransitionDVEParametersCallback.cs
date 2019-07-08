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
    public class SwitcherTransitionDVEParametersEventArgs : EventArgs
    {
    }
    public delegate void SwitcherTransitionDVEParametersEventHandler(SwitcherTransitionDVEParametersCallback s, SwitcherTransitionDVEParametersEventArgs a);

    public class SwitcherTransitionDVEParametersCallback : IBMDSwitcherTransitionDVEParametersCallback
    {
        // Events:
        public event SwitcherTransitionDVEParametersEventHandler SwitcherTransitionDVEParametersEventTypeClipChanged;
        public event SwitcherTransitionDVEParametersEventHandler SwitcherTransitionDVEParametersEventTypeEnableKeyChanged;
        public event SwitcherTransitionDVEParametersEventHandler SwitcherTransitionDVEParametersEventTypeFlipFlopChanged;
        public event SwitcherTransitionDVEParametersEventHandler SwitcherTransitionDVEParametersEventTypeGainChanged;
        public event SwitcherTransitionDVEParametersEventHandler SwitcherTransitionDVEParametersEventTypeInputCutChanged;
        public event SwitcherTransitionDVEParametersEventHandler SwitcherTransitionDVEParametersEventTypeInputFillChanged;
        public event SwitcherTransitionDVEParametersEventHandler SwitcherTransitionDVEParametersEventTypeInverseChanged;
        public event SwitcherTransitionDVEParametersEventHandler SwitcherTransitionDVEParametersEventTypeLogoRateChanged;
        public event SwitcherTransitionDVEParametersEventHandler SwitcherTransitionDVEParametersEventTypePreMultipliedChanged;
        public event SwitcherTransitionDVEParametersEventHandler SwitcherTransitionDVEParametersEventTypeRateChanged;
        public event SwitcherTransitionDVEParametersEventHandler SwitcherTransitionDVEParametersEventTypeReverseChanged;
        public event SwitcherTransitionDVEParametersEventHandler SwitcherTransitionDVEParametersEventTypeStyleChanged;

        private SwitcherTransitionDVEParametersEventArgs _switcherTransitionDVEParametersEventArgs;

        internal IBMDSwitcherTransitionDVEParameters TransitionDVEParameters;
        internal SwitcherTransitionDVEParametersCallback(IBMDSwitcherTransitionDVEParameters transitionDVEParameters)
        {
            this.TransitionDVEParameters = transitionDVEParameters;
        }

        void IBMDSwitcherTransitionDVEParametersCallback.Notify(_BMDSwitcherTransitionDVEParametersEventType eventType)
        {
            this._switcherTransitionDVEParametersEventArgs = new SwitcherTransitionDVEParametersEventArgs();
            switch (eventType)
            {
                case _BMDSwitcherTransitionDVEParametersEventType.bmdSwitcherTransitionDVEParametersEventTypeClipChanged:
                    SwitcherTransitionDVEParametersEventTypeClipChanged?.Invoke(this, this._switcherTransitionDVEParametersEventArgs);
                    break;
                case _BMDSwitcherTransitionDVEParametersEventType.bmdSwitcherTransitionDVEParametersEventTypeEnableKeyChanged:
                    SwitcherTransitionDVEParametersEventTypeEnableKeyChanged?.Invoke(this, this._switcherTransitionDVEParametersEventArgs);
                    break;
                case _BMDSwitcherTransitionDVEParametersEventType.bmdSwitcherTransitionDVEParametersEventTypeFlipFlopChanged:
                    SwitcherTransitionDVEParametersEventTypeFlipFlopChanged?.Invoke(this, this._switcherTransitionDVEParametersEventArgs);
                    break;
                case _BMDSwitcherTransitionDVEParametersEventType.bmdSwitcherTransitionDVEParametersEventTypeGainChanged:
                    SwitcherTransitionDVEParametersEventTypeGainChanged?.Invoke(this, this._switcherTransitionDVEParametersEventArgs);
                    break;
                case _BMDSwitcherTransitionDVEParametersEventType.bmdSwitcherTransitionDVEParametersEventTypeInputCutChanged:
                    SwitcherTransitionDVEParametersEventTypeInputCutChanged?.Invoke(this, this._switcherTransitionDVEParametersEventArgs);
                    break;
                case _BMDSwitcherTransitionDVEParametersEventType.bmdSwitcherTransitionDVEParametersEventTypeInputFillChanged:
                    SwitcherTransitionDVEParametersEventTypeInputFillChanged?.Invoke(this, this._switcherTransitionDVEParametersEventArgs);
                    break;
                case _BMDSwitcherTransitionDVEParametersEventType.bmdSwitcherTransitionDVEParametersEventTypeInverseChanged:
                    SwitcherTransitionDVEParametersEventTypeInverseChanged?.Invoke(this, this._switcherTransitionDVEParametersEventArgs);
                    break;
                case _BMDSwitcherTransitionDVEParametersEventType.bmdSwitcherTransitionDVEParametersEventTypeLogoRateChanged:
                    SwitcherTransitionDVEParametersEventTypeLogoRateChanged?.Invoke(this, this._switcherTransitionDVEParametersEventArgs);
                    break;
                case _BMDSwitcherTransitionDVEParametersEventType.bmdSwitcherTransitionDVEParametersEventTypePreMultipliedChanged:
                    SwitcherTransitionDVEParametersEventTypePreMultipliedChanged?.Invoke(this, this._switcherTransitionDVEParametersEventArgs);
                    break;
                case _BMDSwitcherTransitionDVEParametersEventType.bmdSwitcherTransitionDVEParametersEventTypeRateChanged:
                    SwitcherTransitionDVEParametersEventTypeRateChanged?.Invoke(this, this._switcherTransitionDVEParametersEventArgs);
                    break;
                case _BMDSwitcherTransitionDVEParametersEventType.bmdSwitcherTransitionDVEParametersEventTypeReverseChanged:
                    SwitcherTransitionDVEParametersEventTypeReverseChanged?.Invoke(this, this._switcherTransitionDVEParametersEventArgs);
                    break;
                case _BMDSwitcherTransitionDVEParametersEventType.bmdSwitcherTransitionDVEParametersEventTypeStyleChanged:
                    SwitcherTransitionDVEParametersEventTypeStyleChanged?.Invoke(this, this._switcherTransitionDVEParametersEventArgs);
                    break;
            }
        }

        private int _supported;
        private double _clip;
        private _BMDSwitcherInputAvailability _cutInputAvailabilityMask;
        private int _enableKey;
        private _BMDSwitcherInputAvailability _fillInputAvailabilityMask;
        private int _flipflop;
        private double _gain;
        private long _inputCut;
        private long _inputFill;
        private int _inverse;
        private uint _logoRateFrames;
        private uint _numSupportedStyles;
        private int _preMultiplied;
        private uint _rateFrames;
        private int _reverse;
        private _BMDSwitcherDVETransitionStyle _style;
        private _BMDSwitcherDVETransitionStyle _supportedStyles;

        public int DoesSupportStyle(_BMDSwitcherDVETransitionStyle style)
        {
            this.TransitionDVEParameters.DoesSupportStyle(style, out this._supported);
            return this._supported;
        }
        public double Clip
        {
            get
            {
                this.TransitionDVEParameters.GetClip(out this._clip);
                return this._clip;
            }
            set
            {
                this.TransitionDVEParameters.SetClip(value);
            }
        }
        public _BMDSwitcherInputAvailability CutInputAvailabilityMask
        {
            get
            {
                this.TransitionDVEParameters.GetCutInputAvailabilityMask(out this._cutInputAvailabilityMask);
                return this._cutInputAvailabilityMask;
            }
        }
        public int EnableKey
        {
            get
            {
                this.TransitionDVEParameters.GetEnableKey(out this._enableKey);
                return this._enableKey;
            }
            set
            {
                this.TransitionDVEParameters.SetEnableKey(value);
            }
        }
        public _BMDSwitcherInputAvailability FillInputAvailabilityMask
        {
            get
            {
                this.TransitionDVEParameters.GetFillInputAvailabilityMask(out this._fillInputAvailabilityMask);
                return this._fillInputAvailabilityMask;
            }
        }
        public int FlipFlop
        {
            get
            {
                this.TransitionDVEParameters.GetFlipFlop(out this._flipflop);
                return this._flipflop;
            }
            set
            {
                this.TransitionDVEParameters.SetFlipFlop(value);
            }
        }
        public double Gain
        {
            get
            {
                this.TransitionDVEParameters.GetGain(out this._gain);
                return this._gain;
            }
            set
            {
                this.TransitionDVEParameters.SetGain(value);
            }
        }
        public long InputCut
        {
            get
            {
                this.TransitionDVEParameters.GetInputCut(out this._inputCut);
                return this._inputCut;
            }
            set
            {
                this.TransitionDVEParameters.SetInputCut(value);
            }
        }
        public long InputFill
        {
            get
            {
                this.TransitionDVEParameters.GetInputFill(out this._inputFill);
                return this._inputFill;
            }
            set
            {
                this.TransitionDVEParameters.SetInputFill(value);
            }
        }
        public int InputInverse
        {
            get
            {
                this.TransitionDVEParameters.GetInverse(out this._inverse);
                return this._inverse;
            }
            set
            {
                this.TransitionDVEParameters.SetInverse(value);
            }
        }
        public uint LogoRate
        {
            get
            {
                this.TransitionDVEParameters.GetLogoRate(out this._logoRateFrames);
                return this._logoRateFrames;
            }
            set
            {
                this.TransitionDVEParameters.SetLogoRate(value);
            }
        }
        public uint NumSupportedStyles
        {
            get
            {
                this.TransitionDVEParameters.GetNumSupportedStyles(out _numSupportedStyles);
                return this._numSupportedStyles;
            }
        }
        public int PreMultiplied
        {
            get
            {
                this.TransitionDVEParameters.GetPreMultiplied(out this._preMultiplied);
                return this._preMultiplied;
            }
            set
            {
                this.TransitionDVEParameters.SetPreMultiplied(value);
            }
        }
        public uint Rate
        {
            get
            {
                this.TransitionDVEParameters.GetRate(out this._rateFrames);
                return this._rateFrames;
            }
            set
            {
                this.TransitionDVEParameters.SetRate(value);
            }
        }
        public int Reverse
        {
            get
            {
                this.TransitionDVEParameters.GetReverse(out this._reverse);
                return this._reverse;
            }
            set
            {
                this.TransitionDVEParameters.SetReverse(value);
            }
        }
        public _BMDSwitcherDVETransitionStyle Style
        {
            get
            {
                this.TransitionDVEParameters.GetStyle(out this._style);
                return this._style;
            }
            set
            {
                this.TransitionDVEParameters.SetStyle(value);
            }
        }
        public _BMDSwitcherDVETransitionStyle SupportedStyles(uint supportedStylesMaxCount)
        {
            this.TransitionDVEParameters.GetSupportedStyles(out this._supportedStyles, supportedStylesMaxCount);
            return this._supportedStyles;
        }
    }
}
