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
    public class SwitcherKeyPatternParametersEventArgs : EventArgs
    {
    }
    public delegate void SwitcherKeyPatternParametersEventHandler(SwitcherKeyPatternParametersCallback s, SwitcherKeyPatternParametersEventArgs a);

    public class SwitcherKeyPatternParametersCallback : IBMDSwitcherKeyPatternParametersCallback
    {
        // Events:
        public event SwitcherKeyPatternParametersEventHandler SwitcherKeyPatternParametersEventTypeHorizontalOffsetChanged;
        public event SwitcherKeyPatternParametersEventHandler SwitcherKeyPatternParametersEventTypeInverseChanged;
        public event SwitcherKeyPatternParametersEventHandler SwitcherKeyPatternParametersEventTypePatternChanged;
        public event SwitcherKeyPatternParametersEventHandler SwitcherKeyPatternParametersEventTypeSizeChanged;
        public event SwitcherKeyPatternParametersEventHandler SwitcherKeyPatternParametersEventTypeSoftnessChanged;
        public event SwitcherKeyPatternParametersEventHandler SwitcherKeyPatternParametersEventTypeSymmetryChanged;
        public event SwitcherKeyPatternParametersEventHandler SwitcherKeyPatternParametersEventTypeVerticalOffsetChanged;

        private SwitcherKeyPatternParametersEventArgs _switcherKeyPatternParametersEventArgs;

        private int _indexnr;
        internal IBMDSwitcherKeyPatternParameters KeyPatternParameters;
        internal SwitcherKeyPatternParametersCallback(IBMDSwitcherKeyPatternParameters keyPatternParameters, int index)
        {
            this._indexnr = index;
            this.KeyPatternParameters = keyPatternParameters;
        }
        
        void IBMDSwitcherKeyPatternParametersCallback.Notify(_BMDSwitcherKeyPatternParametersEventType eventType)
        {
            this._switcherKeyPatternParametersEventArgs = new SwitcherKeyPatternParametersEventArgs();
            switch (eventType)
            {
                case _BMDSwitcherKeyPatternParametersEventType.bmdSwitcherKeyPatternParametersEventTypeHorizontalOffsetChanged:
                    this.SwitcherKeyPatternParametersEventTypeHorizontalOffsetChanged?.Invoke(this, this._switcherKeyPatternParametersEventArgs);
                    break;
                case _BMDSwitcherKeyPatternParametersEventType.bmdSwitcherKeyPatternParametersEventTypeInverseChanged:
                    this.SwitcherKeyPatternParametersEventTypeInverseChanged?.Invoke(this, this._switcherKeyPatternParametersEventArgs);
                    break;
                case _BMDSwitcherKeyPatternParametersEventType.bmdSwitcherKeyPatternParametersEventTypePatternChanged:
                    this.SwitcherKeyPatternParametersEventTypePatternChanged?.Invoke(this, this._switcherKeyPatternParametersEventArgs);
                    break;
                case _BMDSwitcherKeyPatternParametersEventType.bmdSwitcherKeyPatternParametersEventTypeSizeChanged:
                    this.SwitcherKeyPatternParametersEventTypeSizeChanged?.Invoke(this, this._switcherKeyPatternParametersEventArgs);
                    break;
                case _BMDSwitcherKeyPatternParametersEventType.bmdSwitcherKeyPatternParametersEventTypeSoftnessChanged:
                    this.SwitcherKeyPatternParametersEventTypeSoftnessChanged?.Invoke(this, this._switcherKeyPatternParametersEventArgs);
                    break;
                case _BMDSwitcherKeyPatternParametersEventType.bmdSwitcherKeyPatternParametersEventTypeSymmetryChanged:
                    this.SwitcherKeyPatternParametersEventTypeSymmetryChanged?.Invoke(this, this._switcherKeyPatternParametersEventArgs);
                    break;
                case _BMDSwitcherKeyPatternParametersEventType.bmdSwitcherKeyPatternParametersEventTypeVerticalOffsetChanged:
                    this.SwitcherKeyPatternParametersEventTypeVerticalOffsetChanged?.Invoke(this, this._switcherKeyPatternParametersEventArgs);
                    break;
            }
        }

        private double _hOffset;
        private int _inverse;
        private _BMDSwitcherPatternStyle _pattern;
        private double _size;
        private double _softness;
        private double _symmetry;
        private double _vOffset;

        public int IndexNr
        {
            get
            {
                return this._indexnr;
            }
        }
        public double HorizontalOffset
        {
            get
            {
                this.KeyPatternParameters.GetHorizontalOffset(out this._hOffset);
                return this._hOffset;
            }
            set
            {
                this.KeyPatternParameters.SetHorizontalOffset(value);
            }
        }
        public int Inverse
        {
            get
            {
                this.KeyPatternParameters.GetInverse(out this._inverse);
                return this._inverse;
            }
            set
            {
                this.KeyPatternParameters.SetInverse(value);
            }
        }
        public _BMDSwitcherPatternStyle Pattern
        {
            get
            {
                this.KeyPatternParameters.GetPattern(out this._pattern);
                return this._pattern;
            }
            set
            {
                this.KeyPatternParameters.SetPattern(value);
            }
        }
        public double Size
        {
            get
            {
                this.KeyPatternParameters.GetSize(out this._size);
                return this._size;
            }
            set
            {
                this.KeyPatternParameters.SetSize(value);
            }
        }
        public double Softness
        {
            get
            {
                this.KeyPatternParameters.GetSoftness(out this._softness);
                return this._softness;
            }
            set
            {
                this.KeyPatternParameters.SetSoftness(value);
            }
        }
        public double Symmetry
        {
            get
            {
                this.KeyPatternParameters.GetSymmetry(out this._symmetry);
                return this._symmetry;
            }
            set
            {
                this.KeyPatternParameters.SetSymmetry(value);
            }
        }
        public double VerticalOffset
        {
            get
            {
                this.KeyPatternParameters.GetVerticalOffset(out this._vOffset);
                return this._vOffset;
            }
            set
            {
                this.KeyPatternParameters.SetVerticalOffset(value);
            }
        }
    }
}
