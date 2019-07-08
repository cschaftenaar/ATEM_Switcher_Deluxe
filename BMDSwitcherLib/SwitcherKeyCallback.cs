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
    public class SwitcherKeyEventArgs : EventArgs
    {
    }
    public delegate void SwitcherKeyEventHandler(SwitcherKeyCallback s, SwitcherKeyEventArgs a);

    public class SwitcherKeyCallback : IBMDSwitcherKeyCallback
    {
        // Events:
        public event SwitcherKeyEventHandler SwitcherKeyEventTypeCanBeDVEKeyChanged;
        public event SwitcherKeyEventHandler SwitcherKeyEventTypeInputCutChanged;
        public event SwitcherKeyEventHandler SwitcherKeyEventTypeInputFillChanged;
        public event SwitcherKeyEventHandler SwitcherKeyEventTypeMaskBottomChanged;
        public event SwitcherKeyEventHandler SwitcherKeyEventTypeMaskedChanged;
        public event SwitcherKeyEventHandler SwitcherKeyEventTypeMaskLeftChanged;
        public event SwitcherKeyEventHandler SwitcherKeyEventTypeMaskRightChanged;
        public event SwitcherKeyEventHandler SwitcherKeyEventTypeMaskTopChanged;
        public event SwitcherKeyEventHandler SwitcherKeyEventTypeOnAirChanged;
        public event SwitcherKeyEventHandler SwitcherKeyEventTypeTypeChanged;

        private SwitcherKeyEventArgs _switcherKeyEventArgs;

        private int _indexnr;
        internal IBMDSwitcherKey Key;
        internal SwitcherKeyCallback(IBMDSwitcherKey key, int index)
        {
            this._indexnr = index;
            this.Key = key;
        }

        void IBMDSwitcherKeyCallback.Notify(_BMDSwitcherKeyEventType eventType)
        {
            this._switcherKeyEventArgs = new SwitcherKeyEventArgs();
            switch (eventType)
            {
                case _BMDSwitcherKeyEventType.bmdSwitcherKeyEventTypeCanBeDVEKeyChanged:
                    this.SwitcherKeyEventTypeCanBeDVEKeyChanged?.Invoke(this, this._switcherKeyEventArgs);
                    break;
                case _BMDSwitcherKeyEventType.bmdSwitcherKeyEventTypeInputCutChanged:
                    this.SwitcherKeyEventTypeInputCutChanged?.Invoke(this, this._switcherKeyEventArgs);
                    break;
                case _BMDSwitcherKeyEventType.bmdSwitcherKeyEventTypeInputFillChanged:
                    this.SwitcherKeyEventTypeInputFillChanged?.Invoke(this, this._switcherKeyEventArgs);
                    break;
                case _BMDSwitcherKeyEventType.bmdSwitcherKeyEventTypeMaskBottomChanged:
                    this.SwitcherKeyEventTypeMaskBottomChanged?.Invoke(this, this._switcherKeyEventArgs);
                    break;
                case _BMDSwitcherKeyEventType.bmdSwitcherKeyEventTypeMaskedChanged:
                    this.SwitcherKeyEventTypeMaskedChanged?.Invoke(this, this._switcherKeyEventArgs);
                    break;
                case _BMDSwitcherKeyEventType.bmdSwitcherKeyEventTypeMaskLeftChanged:
                    this.SwitcherKeyEventTypeMaskLeftChanged?.Invoke(this, this._switcherKeyEventArgs);
                    break;
                case _BMDSwitcherKeyEventType.bmdSwitcherKeyEventTypeMaskRightChanged:
                    this.SwitcherKeyEventTypeMaskRightChanged?.Invoke(this, this._switcherKeyEventArgs);
                    break;
                case _BMDSwitcherKeyEventType.bmdSwitcherKeyEventTypeMaskTopChanged:
                    this.SwitcherKeyEventTypeMaskTopChanged?.Invoke(this, this._switcherKeyEventArgs);
                    break;
                case _BMDSwitcherKeyEventType.bmdSwitcherKeyEventTypeOnAirChanged:
                    this.SwitcherKeyEventTypeOnAirChanged?.Invoke(this, this._switcherKeyEventArgs);
                    break;
                case _BMDSwitcherKeyEventType.bmdSwitcherKeyEventTypeTypeChanged:
                    this.SwitcherKeyEventTypeTypeChanged?.Invoke(this, this._switcherKeyEventArgs);
                    break;
            }
        }

        private int _candDVE;
        private _BMDSwitcherInputAvailability _cutInputAvailabilityMask;
        private _BMDSwitcherInputAvailability _inputAvailability;
        private long _inputCut;
        private long _inputFill;
        private double _bottom;
        private int _maskEnabled;
        private double _left;
        private double _right;
        private double _top;
        private int _onAir;
        private _BMDSwitcherTransitionSelection _transitionSelection;
        private _BMDSwitcherKeyType _type;

        public int IndexNr
        {
            get
            {
                return this._indexnr;
            }
        }
        public int CanBeDVEKey
        {
            get
            {
                this.Key.CanBeDVEKey(out this._candDVE);
                return this._candDVE;
            }
        }
        public _BMDSwitcherInputAvailability CutInputAvailabilityMask
        {
            get
            {
                this.Key.GetCutInputAvailabilityMask(out this._cutInputAvailabilityMask);
                return this._cutInputAvailabilityMask;
            }
        }
        public _BMDSwitcherInputAvailability FillInputAvailabilityMask
        {
            get
            {
                this.Key.GetFillInputAvailabilityMask(out this._inputAvailability);
                return this._inputAvailability;
            }
        }
        public long InputCut
        {
            get
            {
                this.Key.GetInputCut(out this._inputCut);
                return this._inputCut;
            }
            set
            {
                this.Key.SetInputCut(value);
            }
        }
        public long InputFill
        {
            get
            {
                this.Key.GetInputFill(out this._inputFill);
                return this._inputFill;
            }
            set
            {
                this.Key.SetInputFill(value);
            }
        }
        public double MaskBottom
        {
            get
            {
                this.Key.GetMaskBottom(out this._bottom);
                return this._bottom;
            }
            set
            {
                this.Key.SetMaskBottom(value);
            }
        }
        public int Masked
        {
            get
            {
                this.Key.GetMasked(out this._maskEnabled);
                return this._maskEnabled;
            }
            set
            {
                this.Key.SetMasked(value);
            }
        }
        public double MaskLeft
        {
            get
            {
                this.Key.GetMaskLeft(out this._left);
                return this._left;
            }
            set
            {
                this.Key.SetMaskLeft(value);
            }
        }
        public double MaskRight
        {
            get
            {
                this.Key.GetMaskRight(out this._right);
                return this._right;
            }
            set
            {
                this.Key.SetMaskRight(value);
            }
        }
        public double MaskTop
        {
            get
            {
                this.Key.GetMaskTop(out this._top);
                return this._top;
            }
            set
            {
                this.Key.SetMaskTop(value);
            }
        }
        public int OnAir
        {
            get
            {
                this.Key.GetOnAir(out this._onAir);
                return this._onAir;
            }
            set
            {
                this.Key.SetOnAir(value);
            }
        }
        public _BMDSwitcherTransitionSelection TransitionSelectionMask
        {
            get
            {
                this.Key.GetTransitionSelectionMask(out this._transitionSelection);
                return this._transitionSelection;
            }
        }
        public _BMDSwitcherKeyType Type
        {
            get
            {
                this.Key.GetType(out this._type);
                return this._type;
            }
            set
            {
                this.Key.SetType(value);
            }
        }
        public void ResetMask()
        {
            this.Key.ResetMask();
        }
    }
}