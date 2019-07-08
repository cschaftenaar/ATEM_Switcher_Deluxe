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
    public class SwitcherKeyDVEParametersEventArgs : EventArgs
    {
    }
    public delegate void SwitcherKeyDVEParametersEventHandler(SwitcherKeyDVEParametersCallback s, SwitcherKeyDVEParametersEventArgs a);

    public class SwitcherKeyDVEParametersCallback : IBMDSwitcherKeyDVEParametersCallback
    {
        // Events:
        public event SwitcherKeyDVEParametersEventHandler SwitcherKeyDVEParametersEventTypeBorderBevelChanged;
        public event SwitcherKeyDVEParametersEventHandler SwitcherKeyDVEParametersEventTypeBorderBevelPositionChanged;
        public event SwitcherKeyDVEParametersEventHandler SwitcherKeyDVEParametersEventTypeBorderBevelSoftnessChanged;
        public event SwitcherKeyDVEParametersEventHandler SwitcherKeyDVEParametersEventTypeBorderEnabledChanged;
        public event SwitcherKeyDVEParametersEventHandler SwitcherKeyDVEParametersEventTypeBorderHueChanged;
        public event SwitcherKeyDVEParametersEventHandler SwitcherKeyDVEParametersEventTypeBorderLumaChanged;
        public event SwitcherKeyDVEParametersEventHandler SwitcherKeyDVEParametersEventTypeBorderOpacityChanged;
        public event SwitcherKeyDVEParametersEventHandler SwitcherKeyDVEParametersEventTypeBorderSaturationChanged;
        public event SwitcherKeyDVEParametersEventHandler SwitcherKeyDVEParametersEventTypeBorderSoftnessInChanged;
        public event SwitcherKeyDVEParametersEventHandler SwitcherKeyDVEParametersEventTypeBorderSoftnessOutChanged;
        public event SwitcherKeyDVEParametersEventHandler SwitcherKeyDVEParametersEventTypeBorderWidthInChanged;
        public event SwitcherKeyDVEParametersEventHandler SwitcherKeyDVEParametersEventTypeBorderWidthOutChanged;
        public event SwitcherKeyDVEParametersEventHandler SwitcherKeyDVEParametersEventTypeLightSourceAltitudeChanged;
        public event SwitcherKeyDVEParametersEventHandler SwitcherKeyDVEParametersEventTypeLightSourceDirectionChanged;
        public event SwitcherKeyDVEParametersEventHandler SwitcherKeyDVEParametersEventTypeMaskBottomChanged;
        public event SwitcherKeyDVEParametersEventHandler SwitcherKeyDVEParametersEventTypeMaskedChanged;
        public event SwitcherKeyDVEParametersEventHandler SwitcherKeyDVEParametersEventTypeMaskLeftChanged;
        public event SwitcherKeyDVEParametersEventHandler SwitcherKeyDVEParametersEventTypeMaskRightChanged;
        public event SwitcherKeyDVEParametersEventHandler SwitcherKeyDVEParametersEventTypeMaskTopChanged;
        public event SwitcherKeyDVEParametersEventHandler SwitcherKeyDVEParametersEventTypeShadowChanged;

        private SwitcherKeyDVEParametersEventArgs _switcherKeyDVEParametersEventArgs;

        private int _indexnr;
        internal IBMDSwitcherKeyDVEParameters KeyDVEParameters;
        internal SwitcherKeyDVEParametersCallback(IBMDSwitcherKeyDVEParameters keyDVEParameters, int index)
        {
            this._indexnr = index;
            this.KeyDVEParameters = keyDVEParameters;
        }

        void IBMDSwitcherKeyDVEParametersCallback.Notify(_BMDSwitcherKeyDVEParametersEventType eventType)
        {
            this._switcherKeyDVEParametersEventArgs = new SwitcherKeyDVEParametersEventArgs();
            switch (eventType)
            {
                case _BMDSwitcherKeyDVEParametersEventType.bmdSwitcherKeyDVEParametersEventTypeBorderBevelChanged:
                    this.SwitcherKeyDVEParametersEventTypeBorderBevelChanged?.Invoke(this, this._switcherKeyDVEParametersEventArgs);
                    break;
                case _BMDSwitcherKeyDVEParametersEventType.bmdSwitcherKeyDVEParametersEventTypeBorderBevelPositionChanged:
                    this.SwitcherKeyDVEParametersEventTypeBorderBevelPositionChanged?.Invoke(this, this._switcherKeyDVEParametersEventArgs);
                    break;
                case _BMDSwitcherKeyDVEParametersEventType.bmdSwitcherKeyDVEParametersEventTypeBorderBevelSoftnessChanged:
                    this.SwitcherKeyDVEParametersEventTypeBorderBevelSoftnessChanged?.Invoke(this, this._switcherKeyDVEParametersEventArgs);
                    break;
                case _BMDSwitcherKeyDVEParametersEventType.bmdSwitcherKeyDVEParametersEventTypeBorderEnabledChanged:
                    this.SwitcherKeyDVEParametersEventTypeBorderEnabledChanged?.Invoke(this, this._switcherKeyDVEParametersEventArgs);
                    break;
                case _BMDSwitcherKeyDVEParametersEventType.bmdSwitcherKeyDVEParametersEventTypeBorderHueChanged:
                    this.SwitcherKeyDVEParametersEventTypeBorderHueChanged?.Invoke(this, this._switcherKeyDVEParametersEventArgs);
                    break;
                case _BMDSwitcherKeyDVEParametersEventType.bmdSwitcherKeyDVEParametersEventTypeBorderLumaChanged:
                    this.SwitcherKeyDVEParametersEventTypeBorderLumaChanged?.Invoke(this, this._switcherKeyDVEParametersEventArgs);
                    break;
                case _BMDSwitcherKeyDVEParametersEventType.bmdSwitcherKeyDVEParametersEventTypeBorderOpacityChanged:
                    this.SwitcherKeyDVEParametersEventTypeBorderOpacityChanged?.Invoke(this, this._switcherKeyDVEParametersEventArgs);
                    break;
                case _BMDSwitcherKeyDVEParametersEventType.bmdSwitcherKeyDVEParametersEventTypeBorderSaturationChanged:
                    this.SwitcherKeyDVEParametersEventTypeBorderSaturationChanged?.Invoke(this, this._switcherKeyDVEParametersEventArgs);
                    break;
                case _BMDSwitcherKeyDVEParametersEventType.bmdSwitcherKeyDVEParametersEventTypeBorderSoftnessInChanged:
                    this.SwitcherKeyDVEParametersEventTypeBorderSoftnessInChanged?.Invoke(this, this._switcherKeyDVEParametersEventArgs);
                    break;
                case _BMDSwitcherKeyDVEParametersEventType.bmdSwitcherKeyDVEParametersEventTypeBorderSoftnessOutChanged:
                    this.SwitcherKeyDVEParametersEventTypeBorderSoftnessOutChanged?.Invoke(this, this._switcherKeyDVEParametersEventArgs);
                    break;
                case _BMDSwitcherKeyDVEParametersEventType.bmdSwitcherKeyDVEParametersEventTypeBorderWidthInChanged:
                    this.SwitcherKeyDVEParametersEventTypeBorderWidthInChanged?.Invoke(this, this._switcherKeyDVEParametersEventArgs);
                    break;
                case _BMDSwitcherKeyDVEParametersEventType.bmdSwitcherKeyDVEParametersEventTypeBorderWidthOutChanged:
                    this.SwitcherKeyDVEParametersEventTypeBorderWidthOutChanged?.Invoke(this, this._switcherKeyDVEParametersEventArgs);
                    break;
                case _BMDSwitcherKeyDVEParametersEventType.bmdSwitcherKeyDVEParametersEventTypeLightSourceAltitudeChanged:
                    this.SwitcherKeyDVEParametersEventTypeLightSourceAltitudeChanged?.Invoke(this, this._switcherKeyDVEParametersEventArgs);
                    break;
                case _BMDSwitcherKeyDVEParametersEventType.bmdSwitcherKeyDVEParametersEventTypeLightSourceDirectionChanged:
                    this.SwitcherKeyDVEParametersEventTypeLightSourceDirectionChanged?.Invoke(this, this._switcherKeyDVEParametersEventArgs);
                    break;
                case _BMDSwitcherKeyDVEParametersEventType.bmdSwitcherKeyDVEParametersEventTypeMaskBottomChanged:
                    this.SwitcherKeyDVEParametersEventTypeMaskBottomChanged?.Invoke(this, this._switcherKeyDVEParametersEventArgs);
                    break;
                case _BMDSwitcherKeyDVEParametersEventType.bmdSwitcherKeyDVEParametersEventTypeMaskedChanged:
                    this.SwitcherKeyDVEParametersEventTypeMaskedChanged?.Invoke(this, this._switcherKeyDVEParametersEventArgs);
                    break;
                case _BMDSwitcherKeyDVEParametersEventType.bmdSwitcherKeyDVEParametersEventTypeMaskLeftChanged:
                    this.SwitcherKeyDVEParametersEventTypeMaskLeftChanged?.Invoke(this, this._switcherKeyDVEParametersEventArgs);
                    break;
                case _BMDSwitcherKeyDVEParametersEventType.bmdSwitcherKeyDVEParametersEventTypeMaskRightChanged:
                    this.SwitcherKeyDVEParametersEventTypeMaskRightChanged?.Invoke(this, this._switcherKeyDVEParametersEventArgs);
                    break;
                case _BMDSwitcherKeyDVEParametersEventType.bmdSwitcherKeyDVEParametersEventTypeMaskTopChanged:
                    this.SwitcherKeyDVEParametersEventTypeMaskTopChanged?.Invoke(this, this._switcherKeyDVEParametersEventArgs);
                    break;
                case _BMDSwitcherKeyDVEParametersEventType.bmdSwitcherKeyDVEParametersEventTypeShadowChanged:
                    this.SwitcherKeyDVEParametersEventTypeShadowChanged?.Invoke(this, this._switcherKeyDVEParametersEventArgs);
                    break;
            }
        }

        private _BMDSwitcherBorderBevelOption _bevelOption;
        private double _bevelPosition;
        private double _bevelSoft;
        private int _on;
        private double _hue;
        private double _luma;
        private double _opacity;
        private double _sat;
        private double _softIn;
        private double _softOut;
        private double _widthIn;
        private double _widthOut;
        private double _altitude;
        private double _degrees;
        private double _bottom;
        private int _maskEnabled;
        private double _left;
        private double _right;
        private double _top;
        private int _shadowOn;

        public int IndexNr
        {
            get
            {
                return this._indexnr;
            }
        }
        public _BMDSwitcherBorderBevelOption BorderBevel
        {
            get
            {
                this.KeyDVEParameters.GetBorderBevel(out this._bevelOption);
                return this._bevelOption;
            }
            set
            {
                this.KeyDVEParameters.SetBorderBevel(value);
            }
        }
        public double BorderBevelPosition
        {
            get
            {
                this.KeyDVEParameters.GetBorderBevelPosition(out this._bevelPosition);
                return this._bevelPosition;
            }
            set
            {
                this.KeyDVEParameters.SetBorderBevelPosition(value);
            }
        }
        public double BorderBevelSoftness
        {
            get
            {
                this.KeyDVEParameters.GetBorderBevelSoftness(out this._bevelSoft);
                return this._bevelSoft;
            }
            set
            {
                this.KeyDVEParameters.SetBorderBevelSoftness(value);
            }
        }
        public int BorderEnabled
        {
            get
            {
                this.KeyDVEParameters.GetBorderEnabled(out this._on);
                return this._on;
            }
            set
            {
                this.KeyDVEParameters.SetBorderEnabled(value);
            }
        }
        public double BorderHue
        {
            get
            {
                this.KeyDVEParameters.GetBorderHue(out this._hue);
                return this._hue;
            }
            set
            {
                this.KeyDVEParameters.SetBorderHue(value);
            }
        }
        public double BorderLuma
        {
            get
            {
                this.KeyDVEParameters.GetBorderLuma(out this._luma);
                return this._luma;
            }
            set
            {
                this.KeyDVEParameters.SetBorderLuma(value);
            }
        }
        public double BorderOpacity
        {
            get
            {
                this.KeyDVEParameters.GetBorderOpacity(out this._opacity);
                return this._opacity;
            }
            set
            {
                this.KeyDVEParameters.SetBorderOpacity(value);
            }
        }
        public double BorderSaturation
        {
            get
            {
                this.KeyDVEParameters.GetBorderSaturation(out this._sat);
                return this._sat;
            }
            set
            {
                this.KeyDVEParameters.SetBorderSaturation(value);
            }
        }
        public double BorderSoftnessIn
        {
            get
            {
                this.KeyDVEParameters.GetBorderSoftnessIn(out this._softIn);
                return this._softIn;
            }
            set
            {
                this.KeyDVEParameters.SetBorderSoftnessIn(value);
            }
        }
        public double BorderSoftnessOut
        {
            get
            {
                this.KeyDVEParameters.GetBorderSoftnessOut(out this._softOut);
                return this._softOut;
            }
            set
            {
                this.KeyDVEParameters.SetBorderSoftnessOut(value);
            }
        }
        public double BorderWidthIn
        {
            get
            {
                this.KeyDVEParameters.GetBorderWidthIn(out this._widthIn);
                return this._widthIn;
            }
            set
            {
                this.KeyDVEParameters.SetBorderWidthIn(value);
            }
        }
        public double BorderWidthOut
        {
            get
            {
                this.KeyDVEParameters.GetBorderWidthOut(out this._widthOut);
                return this._widthOut;
            }
            set
            {
                this.KeyDVEParameters.SetBorderWidthOut(value);
            }
        }
        public double LightSourceAltitude
        {
            get
            {
                this.KeyDVEParameters.GetLightSourceAltitude(out this._altitude);
                return this._altitude;
            }
            set
            {
                this.KeyDVEParameters.SetLightSourceAltitude(value);
            }
        }
        public double LightSourceDirection
        {
            get
            {
                this.KeyDVEParameters.GetLightSourceDirection(out this._degrees);
                return this._degrees;
            }
            set
            {
                this.KeyDVEParameters.SetLightSourceDirection(value);
            }
        }
        public double MaskBottom
        {
            get
            {
                this.KeyDVEParameters.GetMaskBottom(out this._bottom);
                return this._bottom;
            }
            set
            {
                this.KeyDVEParameters.SetMaskBottom(value);
            }
        }
        public int Masked
        {
            get
            {
                this.KeyDVEParameters.GetMasked(out this._maskEnabled);
                return this._maskEnabled;
            }
            set
            {
                this.KeyDVEParameters.SetMasked(value);
            }
        }
        public double MaskLeft
        {
            get
            {
                this.KeyDVEParameters.GetMaskLeft(out this._left);
                return this._left;
            }
            set
            {
                this.KeyDVEParameters.SetMaskLeft(value);
            }
        }
        public double MaskRight
        {
            get
            {
                this.KeyDVEParameters.GetMaskRight(out this._right);
                return this._right;
            }
            set
            {
                this.KeyDVEParameters.SetMaskRight(value);
            }
        }
        public double MaskTop
        {
            get
            {
                this.KeyDVEParameters.GetMaskTop(out this._top);
                return this._top;
            }
            set
            {
                this.KeyDVEParameters.SetMaskTop(value);
            }
        }
        public int Shadow
        {
            get
            {
                this.KeyDVEParameters.GetShadow(out this._shadowOn);
                return this._shadowOn;
            }
            set
            {
                this.KeyDVEParameters.SetShadow(value);
            }
        }
        public void ResetMask()
        {
            this.KeyDVEParameters.ResetMask();
        }
    }
}
