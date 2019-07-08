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
    public class SwitcherTransitionWipeParametersEventArgs : EventArgs
    {
    }
    public delegate void SwitcherTransitionWipeParametersEventHandler(SwitcherTransitionWipeParametersCallback s, SwitcherTransitionWipeParametersEventArgs a);

    public class SwitcherTransitionWipeParametersCallback : IBMDSwitcherTransitionWipeParametersCallback
    {
        // Events:
        public event SwitcherTransitionWipeParametersEventHandler SwitcherTransitionWipeParametersEventTypeBorderSizeChanged;
        public event SwitcherTransitionWipeParametersEventHandler SwitcherTransitionWipeParametersEventTypeFlipFlopChanged;
        public event SwitcherTransitionWipeParametersEventHandler SwitcherTransitionWipeParametersEventTypeHorizontalOffsetChanged;
        public event SwitcherTransitionWipeParametersEventHandler SwitcherTransitionWipeParametersEventTypeInputBorderChanged;
        public event SwitcherTransitionWipeParametersEventHandler SwitcherTransitionWipeParametersEventTypePatternChanged;
        public event SwitcherTransitionWipeParametersEventHandler SwitcherTransitionWipeParametersEventTypeRateChanged;
        public event SwitcherTransitionWipeParametersEventHandler SwitcherTransitionWipeParametersEventTypeReverseChanged;
        public event SwitcherTransitionWipeParametersEventHandler SwitcherTransitionWipeParametersEventTypeSoftnessChanged;
        public event SwitcherTransitionWipeParametersEventHandler SwitcherTransitionWipeParametersEventTypeSymmetryChanged;
        public event SwitcherTransitionWipeParametersEventHandler SwitcherTransitionWipeParametersEventTypeVerticalOffsetChanged;

        private SwitcherTransitionWipeParametersEventArgs _switcherTransitionWipeParametersEventArgs;

        void IBMDSwitcherTransitionWipeParametersCallback.Notify(_BMDSwitcherTransitionWipeParametersEventType eventType)
        {
            this._switcherTransitionWipeParametersEventArgs = new SwitcherTransitionWipeParametersEventArgs();
            switch (eventType)
            {
                case _BMDSwitcherTransitionWipeParametersEventType.bmdSwitcherTransitionWipeParametersEventTypeBorderSizeChanged:
                    SwitcherTransitionWipeParametersEventTypeBorderSizeChanged?.Invoke(this, this._switcherTransitionWipeParametersEventArgs);
                    break;
                case _BMDSwitcherTransitionWipeParametersEventType.bmdSwitcherTransitionWipeParametersEventTypeFlipFlopChanged:
                    SwitcherTransitionWipeParametersEventTypeFlipFlopChanged?.Invoke(this, this._switcherTransitionWipeParametersEventArgs);
                    break;
                case _BMDSwitcherTransitionWipeParametersEventType.bmdSwitcherTransitionWipeParametersEventTypeHorizontalOffsetChanged:
                    SwitcherTransitionWipeParametersEventTypeHorizontalOffsetChanged?.Invoke(this, this._switcherTransitionWipeParametersEventArgs);
                    break;
                case _BMDSwitcherTransitionWipeParametersEventType.bmdSwitcherTransitionWipeParametersEventTypeInputBorderChanged:
                    SwitcherTransitionWipeParametersEventTypeInputBorderChanged?.Invoke(this, this._switcherTransitionWipeParametersEventArgs);
                    break;
                case _BMDSwitcherTransitionWipeParametersEventType.bmdSwitcherTransitionWipeParametersEventTypePatternChanged:
                    SwitcherTransitionWipeParametersEventTypePatternChanged?.Invoke(this, this._switcherTransitionWipeParametersEventArgs);
                    break;
                case _BMDSwitcherTransitionWipeParametersEventType.bmdSwitcherTransitionWipeParametersEventTypeRateChanged:
                    SwitcherTransitionWipeParametersEventTypeRateChanged?.Invoke(this, this._switcherTransitionWipeParametersEventArgs);
                    break;
                case _BMDSwitcherTransitionWipeParametersEventType.bmdSwitcherTransitionWipeParametersEventTypeReverseChanged:
                    SwitcherTransitionWipeParametersEventTypeReverseChanged?.Invoke(this, this._switcherTransitionWipeParametersEventArgs);
                    break;
                case _BMDSwitcherTransitionWipeParametersEventType.bmdSwitcherTransitionWipeParametersEventTypeSoftnessChanged:
                    SwitcherTransitionWipeParametersEventTypeSoftnessChanged?.Invoke(this, this._switcherTransitionWipeParametersEventArgs);
                    break;
                case _BMDSwitcherTransitionWipeParametersEventType.bmdSwitcherTransitionWipeParametersEventTypeSymmetryChanged:
                    SwitcherTransitionWipeParametersEventTypeSymmetryChanged?.Invoke(this, this._switcherTransitionWipeParametersEventArgs);
                    break;
                case _BMDSwitcherTransitionWipeParametersEventType.bmdSwitcherTransitionWipeParametersEventTypeVerticalOffsetChanged:
                    SwitcherTransitionWipeParametersEventTypeVerticalOffsetChanged?.Invoke(this, this._switcherTransitionWipeParametersEventArgs);
                    break;
            }
        }

        internal IBMDSwitcherTransitionWipeParameters TransitionWipeParameters;
        internal SwitcherTransitionWipeParametersCallback(IBMDSwitcherTransitionWipeParameters transitionWipeParameters)
        {
            this.TransitionWipeParameters = transitionWipeParameters;
        }

        private double _size;
        private int _flipflop;
        private double _hOffset;
        private long _input;
        private _BMDSwitcherPatternStyle _pattern;
        private uint _frames;
        private int _reverse;
        private double _soft;
        private double _symmetry;
        private double _vOffset;

        public double BorderSize
        {
            get
            {
                this.TransitionWipeParameters.GetBorderSize(out this._size);
                return this._size;
            }
            set
            {
                this.TransitionWipeParameters.SetBorderSize(value);
            }
        }
        public int FlipFlop
        {
            get
            {
                this.TransitionWipeParameters.GetFlipFlop(out this._flipflop);
                return this._flipflop;
            }
            set
            {
                this.TransitionWipeParameters.SetFlipFlop(value);
            }
        }
        public double HorizontalOffset
        {
            get
            {
                this.TransitionWipeParameters.GetHorizontalOffset(out this._hOffset);
                return this._hOffset;
            }
            set
            {
                this.TransitionWipeParameters.SetHorizontalOffset(value);
            }
        }
        public long InputBorder
        {
            get
            {
                this.TransitionWipeParameters.GetInputBorder(out this._input);
                return this._input;
            }
            set
            {
                this.TransitionWipeParameters.SetInputBorder(value);
            }
        }
        public _BMDSwitcherPatternStyle Pattern
        {
            get
            {
                this.TransitionWipeParameters.GetPattern(out this._pattern);
                return this._pattern;
            }
            set
            {
                this.TransitionWipeParameters.SetPattern(value);
            }
        }
        public uint Rate
        {
            get
            {
                this.TransitionWipeParameters.GetRate(out this._frames);
                return this._frames;
            }
            set
            {
                this.TransitionWipeParameters.SetRate(value);
            }
        }
        public int Reverse
        {
            get
            {
                this.TransitionWipeParameters.GetReverse(out this._reverse);
                return this._reverse;
            }
            set
            {
                this.TransitionWipeParameters.SetReverse(value);
            }
        }
        public double Softness
        {
            get
            {
                this.TransitionWipeParameters.GetSoftness(out this._soft);
                return this._soft;
            }
            set
            {
                this.TransitionWipeParameters.SetSoftness(value);
            }
        }
        public double Symmetry
        {
            get
            {
                this.TransitionWipeParameters.GetSymmetry(out this._symmetry);
                return this._symmetry;
            }
            set
            {
                this.TransitionWipeParameters.SetSymmetry(value);
            }
        }
        public double VerticalOffset
        {
            get
            {
                this.TransitionWipeParameters.GetVerticalOffset(out this._vOffset);
                return this._vOffset;
            }
            set
            {
                this.TransitionWipeParameters.SetVerticalOffset(value);
            }
        }
    }
}
