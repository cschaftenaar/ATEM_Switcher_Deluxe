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
    public class SwitcherDownstreamKeyEventArgs : EventArgs
    {
    }
    public delegate void SwitcherDownstreamKeyEventHandler(SwitcherDownstreamKeyCallback s, SwitcherDownstreamKeyEventArgs a);

    public class SwitcherDownstreamKeyCallback : IBMDSwitcherDownstreamKeyCallback
    {
        // Events:
        public event SwitcherDownstreamKeyEventHandler SwitcherDownstreamKeyEventTypeClipChanged;
        public event SwitcherDownstreamKeyEventHandler SwitcherDownstreamKeyEventTypeFramesRemainingChanged;
        public event SwitcherDownstreamKeyEventHandler SwitcherDownstreamKeyEventTypeGainChanged;
        public event SwitcherDownstreamKeyEventHandler SwitcherDownstreamKeyEventTypeInputCutChanged;
        public event SwitcherDownstreamKeyEventHandler SwitcherDownstreamKeyEventTypeInputFillChanged;
        public event SwitcherDownstreamKeyEventHandler SwitcherDownstreamKeyEventTypeInverseChanged;
        public event SwitcherDownstreamKeyEventHandler SwitcherDownstreamKeyEventTypeIsAutoTransitioningChanged;
        public event SwitcherDownstreamKeyEventHandler SwitcherDownstreamKeyEventTypeIsTransitioningChanged;
        public event SwitcherDownstreamKeyEventHandler SwitcherDownstreamKeyEventTypeMaskBottomChanged;
        public event SwitcherDownstreamKeyEventHandler SwitcherDownstreamKeyEventTypeMaskedChanged;
        public event SwitcherDownstreamKeyEventHandler SwitcherDownstreamKeyEventTypeMaskLeftChanged;
        public event SwitcherDownstreamKeyEventHandler SwitcherDownstreamKeyEventTypeMaskRightChanged;
        public event SwitcherDownstreamKeyEventHandler SwitcherDownstreamKeyEventTypeMaskTopChanged;
        public event SwitcherDownstreamKeyEventHandler SwitcherDownstreamKeyEventTypeOnAirChanged;
        public event SwitcherDownstreamKeyEventHandler SwitcherDownstreamKeyEventTypePreMultipliedChanged;
        public event SwitcherDownstreamKeyEventHandler SwitcherDownstreamKeyEventTypeRateChanged;
        public event SwitcherDownstreamKeyEventHandler SwitcherDownstreamKeyEventTypeTieChanged;

        private SwitcherDownstreamKeyEventArgs _switcherDownstreamKeyEventArgs;

        private int _indexnr;
        internal IBMDSwitcherDownstreamKey DownstreamKey;
        internal SwitcherDownstreamKeyCallback(IBMDSwitcherDownstreamKey downstreamKey, int index)
        {
            this._indexnr = index;
            this.DownstreamKey = downstreamKey;
        }

        void IBMDSwitcherDownstreamKeyCallback.Notify(_BMDSwitcherDownstreamKeyEventType eventType)
        {
            this._switcherDownstreamKeyEventArgs = new SwitcherDownstreamKeyEventArgs();
            switch (eventType)
            {
                case _BMDSwitcherDownstreamKeyEventType.bmdSwitcherDownstreamKeyEventTypeClipChanged:
                    this.SwitcherDownstreamKeyEventTypeClipChanged?.Invoke(this, this._switcherDownstreamKeyEventArgs);
                    break;
                case _BMDSwitcherDownstreamKeyEventType.bmdSwitcherDownstreamKeyEventTypeFramesRemainingChanged:
                    this.SwitcherDownstreamKeyEventTypeFramesRemainingChanged?.Invoke(this, this._switcherDownstreamKeyEventArgs);
                    break;
                case _BMDSwitcherDownstreamKeyEventType.bmdSwitcherDownstreamKeyEventTypeGainChanged:
                    this.SwitcherDownstreamKeyEventTypeGainChanged?.Invoke(this, this._switcherDownstreamKeyEventArgs);
                    break;
                case _BMDSwitcherDownstreamKeyEventType.bmdSwitcherDownstreamKeyEventTypeInputCutChanged:
                    this.SwitcherDownstreamKeyEventTypeInputCutChanged?.Invoke(this, this._switcherDownstreamKeyEventArgs);
                    break;
                case _BMDSwitcherDownstreamKeyEventType.bmdSwitcherDownstreamKeyEventTypeInputFillChanged:
                    this.SwitcherDownstreamKeyEventTypeInputFillChanged?.Invoke(this, this._switcherDownstreamKeyEventArgs);
                    break;
                case _BMDSwitcherDownstreamKeyEventType.bmdSwitcherDownstreamKeyEventTypeInverseChanged:
                    this.SwitcherDownstreamKeyEventTypeInverseChanged?.Invoke(this, this._switcherDownstreamKeyEventArgs);
                    break;
                case _BMDSwitcherDownstreamKeyEventType.bmdSwitcherDownstreamKeyEventTypeIsAutoTransitioningChanged:
                    this.SwitcherDownstreamKeyEventTypeIsAutoTransitioningChanged?.Invoke(this, this._switcherDownstreamKeyEventArgs);
                    break;
                case _BMDSwitcherDownstreamKeyEventType.bmdSwitcherDownstreamKeyEventTypeIsTransitioningChanged:
                    this.SwitcherDownstreamKeyEventTypeIsTransitioningChanged?.Invoke(this, this._switcherDownstreamKeyEventArgs);
                    break;
                case _BMDSwitcherDownstreamKeyEventType.bmdSwitcherDownstreamKeyEventTypeMaskBottomChanged:
                    this.SwitcherDownstreamKeyEventTypeMaskBottomChanged?.Invoke(this, this._switcherDownstreamKeyEventArgs);
                    break;
                case _BMDSwitcherDownstreamKeyEventType.bmdSwitcherDownstreamKeyEventTypeMaskedChanged:
                    this.SwitcherDownstreamKeyEventTypeMaskedChanged?.Invoke(this, this._switcherDownstreamKeyEventArgs);
                    break;
                case _BMDSwitcherDownstreamKeyEventType.bmdSwitcherDownstreamKeyEventTypeMaskLeftChanged:
                    this.SwitcherDownstreamKeyEventTypeMaskLeftChanged?.Invoke(this, this._switcherDownstreamKeyEventArgs);
                    break;
                case _BMDSwitcherDownstreamKeyEventType.bmdSwitcherDownstreamKeyEventTypeMaskRightChanged:
                    this.SwitcherDownstreamKeyEventTypeMaskRightChanged?.Invoke(this, this._switcherDownstreamKeyEventArgs);
                    break;
                case _BMDSwitcherDownstreamKeyEventType.bmdSwitcherDownstreamKeyEventTypeMaskTopChanged:
                    this.SwitcherDownstreamKeyEventTypeMaskTopChanged?.Invoke(this, this._switcherDownstreamKeyEventArgs);
                    break;
                case _BMDSwitcherDownstreamKeyEventType.bmdSwitcherDownstreamKeyEventTypeOnAirChanged:
                    this.SwitcherDownstreamKeyEventTypeOnAirChanged?.Invoke(this, this._switcherDownstreamKeyEventArgs);
                    break;
                case _BMDSwitcherDownstreamKeyEventType.bmdSwitcherDownstreamKeyEventTypePreMultipliedChanged:
                    this.SwitcherDownstreamKeyEventTypePreMultipliedChanged?.Invoke(this, this._switcherDownstreamKeyEventArgs);
                    break;
                case _BMDSwitcherDownstreamKeyEventType.bmdSwitcherDownstreamKeyEventTypeRateChanged:
                    this.SwitcherDownstreamKeyEventTypeRateChanged?.Invoke(this, this._switcherDownstreamKeyEventArgs);
                    break;
                case _BMDSwitcherDownstreamKeyEventType.bmdSwitcherDownstreamKeyEventTypeTieChanged:
                    this.SwitcherDownstreamKeyEventTypeTieChanged?.Invoke(this, this._switcherDownstreamKeyEventArgs);
                    break;
            }
        }

        private double _clip;
        private _BMDSwitcherInputAvailability _availabilityMask;
        private _BMDSwitcherInputAvailability _fillInputAvailabilityMask;
        private uint _framesRemaining;
        private double _gain;
        private long _inputCut;
        private long _inputFill;
        private int _inverse;
        private double _bottom;
        private int _maskedEnabled;
        private double _left;
        private double _right;
        private double _top;
        private int _onAir;
        private int _preMultiplied;
        private uint _frames;
        private int _tie;
        private int _isAutoTransitioning;
        private int _isTransitioning;

        public int IndexNr
        {
            get
            {
                return this._indexnr;
            }
        }
        public double Clip
        {
            get
            {
                this.DownstreamKey.GetClip(out this._clip);
                return this._clip;
            }
            set
            {
                this.DownstreamKey.SetClip(value);
            }
        }
        public _BMDSwitcherInputAvailability CutInputAvailabilityMask
        {
            get
            {
                this.DownstreamKey.GetCutInputAvailabilityMask(out this._availabilityMask);
                return this._availabilityMask;
            }
        }
        public _BMDSwitcherInputAvailability FillInputAvailabilityMask
        {
            get
            {
                this.DownstreamKey.GetFillInputAvailabilityMask(out this._fillInputAvailabilityMask);
                return this._fillInputAvailabilityMask;
            }
        }
        public uint FramesRemaining
        {
            get
            {
                this.DownstreamKey.GetFramesRemaining(out this._framesRemaining);
                return this._framesRemaining;
            }
        }
        public double Gain
        {
            get
            {
                this.DownstreamKey.GetGain(out this._gain);
                return this._gain;
            }
            set
            {
                this.DownstreamKey.SetGain(value);
            }
        }
        public long InputCut
        {
            get
            {
                this.DownstreamKey.GetInputCut(out this._inputCut);
                return this._inputCut;
            }
            set
            {
                this.DownstreamKey.SetInputCut(value);
            }
        }
        public long InputFill
        {
            get
            {
                this.DownstreamKey.GetInputFill(out this._inputFill);
                return this._inputFill;
            }
            set
            {
                this.DownstreamKey.SetInputFill(value);
            }
        }
        public int Inverse
        {
            get
            {
                this.DownstreamKey.GetInverse(out this._inverse);
                return this._inverse;
            }
            set
            {
                this.DownstreamKey.SetInverse(value);
            }
        }
        public double MaskBottom
        {
            get
            {
                this.DownstreamKey.GetMaskBottom(out this._bottom);
                return this._bottom;
            }
            set
            {
                this.DownstreamKey.SetMaskBottom(value);
            }
        }
        public int Masked
        {
            get
            {
                this.DownstreamKey.GetMasked(out this._maskedEnabled);
                return this._maskedEnabled;
            }
            set
            {
                this.DownstreamKey.SetMasked(value);
            }
        }
        public double MaskLeft
        {
            get
            {
                this.DownstreamKey.GetMaskLeft(out this._left);
                return this._left;
            }
            set
            {
                this.DownstreamKey.SetMaskLeft(value);
            }
        }
        public double MaskRight
        {
            get
            {
                this.DownstreamKey.GetMaskRight(out this._right);
                return this._right;
            }
            set
            {
                this.DownstreamKey.SetMaskRight(value);
            }
        }
        public double MaskTop
        {
            get
            {
                this.DownstreamKey.GetMaskTop(out this._top);
                return this._top;
            }
            set
            {
                this.DownstreamKey.SetMaskTop(value);
            }
        }
        public int OnAir
        {
            get
            {
                this.DownstreamKey.GetOnAir(out this._onAir);
                return this._onAir;
            }
            set
            {
                this.DownstreamKey.SetOnAir(value);
            }
        }
        public int PreMultiplied
        {
            get
            {
                this.DownstreamKey.GetPreMultiplied(out this._preMultiplied);
                return this._preMultiplied;
            }
            set
            {
                this.DownstreamKey.SetPreMultiplied(value);
            }
        }
        public uint Rate
        {
            get
            {
                this.DownstreamKey.GetRate(out this._frames);
                return this._frames;
            }
            set
            {
                this.DownstreamKey.SetRate(value);
            }
        }
        public int Tie
        {
            get
            {
                this.DownstreamKey.GetTie(out this._tie);
                return this._tie;
            }
            set
            {
                this.DownstreamKey.SetTie(value);
            }
        }
        public int IsAutoTransitioning
        {
            get
            {
                this.DownstreamKey.IsAutoTransitioning(out this._isAutoTransitioning);
                return this._isAutoTransitioning;
            }
        }
        public int IsTransitioning
        {
            get
            {
                this.DownstreamKey.IsTransitioning(out this._isTransitioning);
                return this._isTransitioning;
            }
        }
        public void PerformAutoTransition()
        {
            this.DownstreamKey.PerformAutoTransition();
        }
        public void ResetMask()
        {
            this.DownstreamKey.ResetMask();
        }
    }
}
