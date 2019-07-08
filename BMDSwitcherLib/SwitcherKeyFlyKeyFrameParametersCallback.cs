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
    public class SwitcherKeyFlyKeyFrameParametersEventArgs : EventArgs
    {
    }
    public delegate void SwitcherKeyFlyKeyFrameParametersEventHandler(SwitcherKeyFlyKeyFrameParametersCallback s, SwitcherKeyFlyKeyFrameParametersEventArgs a);

    public class SwitcherKeyFlyKeyFrameParametersCallback : IBMDSwitcherKeyFlyKeyFrameParametersCallback
    {
        // Events:
        public event SwitcherKeyFlyKeyFrameParametersEventHandler SwitcherKeyFlyKeyFrameParametersEventTypeBorderBevelPositionChanged;
        public event SwitcherKeyFlyKeyFrameParametersEventHandler SwitcherKeyFlyKeyFrameParametersEventTypeBorderBevelSoftnessChanged;
        public event SwitcherKeyFlyKeyFrameParametersEventHandler SwitcherKeyFlyKeyFrameParametersEventTypeBorderHueChanged;
        public event SwitcherKeyFlyKeyFrameParametersEventHandler SwitcherKeyFlyKeyFrameParametersEventTypeBorderLightSourceAltitudeChanged;
        public event SwitcherKeyFlyKeyFrameParametersEventHandler SwitcherKeyFlyKeyFrameParametersEventTypeBorderLightSourceDirectionChanged;
        public event SwitcherKeyFlyKeyFrameParametersEventHandler SwitcherKeyFlyKeyFrameParametersEventTypeBorderLumaChanged;
        public event SwitcherKeyFlyKeyFrameParametersEventHandler SwitcherKeyFlyKeyFrameParametersEventTypeBorderSaturationChanged;
        public event SwitcherKeyFlyKeyFrameParametersEventHandler SwitcherKeyFlyKeyFrameParametersEventTypeBorderSoftnessInChanged;
        public event SwitcherKeyFlyKeyFrameParametersEventHandler SwitcherKeyFlyKeyFrameParametersEventTypeBorderSoftnessOutChanged;
        public event SwitcherKeyFlyKeyFrameParametersEventHandler SwitcherKeyFlyKeyFrameParametersEventTypeBorderWidthInChanged;
        public event SwitcherKeyFlyKeyFrameParametersEventHandler SwitcherKeyFlyKeyFrameParametersEventTypeBorderWidthOutChanged;
        public event SwitcherKeyFlyKeyFrameParametersEventHandler SwitcherKeyFlyKeyFrameParametersEventTypePositionXChanged;
        public event SwitcherKeyFlyKeyFrameParametersEventHandler SwitcherKeyFlyKeyFrameParametersEventTypePositionYChanged;
        public event SwitcherKeyFlyKeyFrameParametersEventHandler SwitcherKeyFlyKeyFrameParametersEventTypeRotationChanged;
        public event SwitcherKeyFlyKeyFrameParametersEventHandler SwitcherKeyFlyKeyFrameParametersEventTypeSizeXChanged;
        public event SwitcherKeyFlyKeyFrameParametersEventHandler SwitcherKeyFlyKeyFrameParametersEventTypeSizeYChanged;

        private SwitcherKeyFlyKeyFrameParametersEventArgs _switcherKeyFlyKeyFrameParametersEventArgs;

        private int _indexnr;
        internal IBMDSwitcherKeyFlyKeyFrameParameters KeyFlyKeyFrameParameters;
        internal SwitcherKeyFlyKeyFrameParametersCallback(IBMDSwitcherKeyFlyKeyFrameParameters keyFlyKeyFrameParameters, int index)
        {
            this._indexnr = index;
            this.KeyFlyKeyFrameParameters = keyFlyKeyFrameParameters;
        }

        void IBMDSwitcherKeyFlyKeyFrameParametersCallback.Notify(_BMDSwitcherKeyFlyKeyFrameParametersEventType eventType)
        {
            this._switcherKeyFlyKeyFrameParametersEventArgs = new SwitcherKeyFlyKeyFrameParametersEventArgs();
            switch (eventType)
            {
                case _BMDSwitcherKeyFlyKeyFrameParametersEventType.bmdSwitcherKeyFlyKeyFrameParametersEventTypeBorderBevelPositionChanged:
                    this.SwitcherKeyFlyKeyFrameParametersEventTypeBorderBevelPositionChanged?.Invoke(this, this._switcherKeyFlyKeyFrameParametersEventArgs);
                    break;
                case _BMDSwitcherKeyFlyKeyFrameParametersEventType.bmdSwitcherKeyFlyKeyFrameParametersEventTypeBorderBevelSoftnessChanged:
                    this.SwitcherKeyFlyKeyFrameParametersEventTypeBorderBevelSoftnessChanged?.Invoke(this, this._switcherKeyFlyKeyFrameParametersEventArgs);
                    break;
                case _BMDSwitcherKeyFlyKeyFrameParametersEventType.bmdSwitcherKeyFlyKeyFrameParametersEventTypeBorderHueChanged:
                    this.SwitcherKeyFlyKeyFrameParametersEventTypeBorderHueChanged?.Invoke(this, this._switcherKeyFlyKeyFrameParametersEventArgs);
                    break;
                case _BMDSwitcherKeyFlyKeyFrameParametersEventType.bmdSwitcherKeyFlyKeyFrameParametersEventTypeBorderLightSourceAltitudeChanged:
                    this.SwitcherKeyFlyKeyFrameParametersEventTypeBorderLightSourceAltitudeChanged?.Invoke(this, this._switcherKeyFlyKeyFrameParametersEventArgs);
                    break;
                case _BMDSwitcherKeyFlyKeyFrameParametersEventType.bmdSwitcherKeyFlyKeyFrameParametersEventTypeBorderLightSourceDirectionChanged:
                    this.SwitcherKeyFlyKeyFrameParametersEventTypeBorderLightSourceDirectionChanged?.Invoke(this, this._switcherKeyFlyKeyFrameParametersEventArgs);
                    break;
                case _BMDSwitcherKeyFlyKeyFrameParametersEventType.bmdSwitcherKeyFlyKeyFrameParametersEventTypeBorderLumaChanged:
                    this.SwitcherKeyFlyKeyFrameParametersEventTypeBorderLumaChanged?.Invoke(this, this._switcherKeyFlyKeyFrameParametersEventArgs);
                    break;
                case _BMDSwitcherKeyFlyKeyFrameParametersEventType.bmdSwitcherKeyFlyKeyFrameParametersEventTypeBorderSaturationChanged:
                    this.SwitcherKeyFlyKeyFrameParametersEventTypeBorderSaturationChanged?.Invoke(this, this._switcherKeyFlyKeyFrameParametersEventArgs);
                    break;
                case _BMDSwitcherKeyFlyKeyFrameParametersEventType.bmdSwitcherKeyFlyKeyFrameParametersEventTypeBorderSoftnessInChanged:
                    this.SwitcherKeyFlyKeyFrameParametersEventTypeBorderSoftnessInChanged?.Invoke(this, this._switcherKeyFlyKeyFrameParametersEventArgs);
                    break;
                case _BMDSwitcherKeyFlyKeyFrameParametersEventType.bmdSwitcherKeyFlyKeyFrameParametersEventTypeBorderSoftnessOutChanged:
                    this.SwitcherKeyFlyKeyFrameParametersEventTypeBorderSoftnessOutChanged?.Invoke(this, this._switcherKeyFlyKeyFrameParametersEventArgs);
                    break;
                case _BMDSwitcherKeyFlyKeyFrameParametersEventType.bmdSwitcherKeyFlyKeyFrameParametersEventTypeBorderWidthInChanged:
                    this.SwitcherKeyFlyKeyFrameParametersEventTypeBorderWidthInChanged?.Invoke(this, this._switcherKeyFlyKeyFrameParametersEventArgs);
                    break;
                case _BMDSwitcherKeyFlyKeyFrameParametersEventType.bmdSwitcherKeyFlyKeyFrameParametersEventTypeBorderWidthOutChanged:
                    this.SwitcherKeyFlyKeyFrameParametersEventTypeBorderWidthOutChanged?.Invoke(this, this._switcherKeyFlyKeyFrameParametersEventArgs);
                    break;
                case _BMDSwitcherKeyFlyKeyFrameParametersEventType.bmdSwitcherKeyFlyKeyFrameParametersEventTypePositionXChanged:
                    this.SwitcherKeyFlyKeyFrameParametersEventTypePositionXChanged?.Invoke(this, this._switcherKeyFlyKeyFrameParametersEventArgs);
                    break;
                case _BMDSwitcherKeyFlyKeyFrameParametersEventType.bmdSwitcherKeyFlyKeyFrameParametersEventTypePositionYChanged:
                    this.SwitcherKeyFlyKeyFrameParametersEventTypePositionYChanged?.Invoke(this, this._switcherKeyFlyKeyFrameParametersEventArgs);
                    break;
                case _BMDSwitcherKeyFlyKeyFrameParametersEventType.bmdSwitcherKeyFlyKeyFrameParametersEventTypeRotationChanged:
                    this.SwitcherKeyFlyKeyFrameParametersEventTypeRotationChanged?.Invoke(this, this._switcherKeyFlyKeyFrameParametersEventArgs);
                    break;
                case _BMDSwitcherKeyFlyKeyFrameParametersEventType.bmdSwitcherKeyFlyKeyFrameParametersEventTypeSizeXChanged:
                    this.SwitcherKeyFlyKeyFrameParametersEventTypeSizeXChanged?.Invoke(this, this._switcherKeyFlyKeyFrameParametersEventArgs);
                    break;
                case _BMDSwitcherKeyFlyKeyFrameParametersEventType.bmdSwitcherKeyFlyKeyFrameParametersEventTypeSizeYChanged:
                    this.SwitcherKeyFlyKeyFrameParametersEventTypeSizeYChanged?.Invoke(this, this._switcherKeyFlyKeyFrameParametersEventArgs);
                    break;
            }
        }

        private double _bevelPosition;
        private double _bevelSoft;
        private double _hue;
        private double _altitude;
        private double _degrees;
        private double _luma;
        private double _opacity;
        private double _sat;
        private double _softIn;
        private double _softOut;
        private double _widthIn;
        private double _widthOut;
        private int _canRotate;
        private int _canScaleUp;
        private double _bottom;
        private double _left;
        private double _right;
        private double _top;
        private double _offsetX;
        private double _offsetY;
        private double _degreesRotation;
        private double _multiplierX;
        private double _multiplierY;

        public int IndexNr
        {
            get
            {
                return this._indexnr;
            }
        }
        public double BorderBevelPosition
        {
            get
            {
                this.KeyFlyKeyFrameParameters.GetBorderBevelPosition(out this._bevelPosition);
                return this._bevelPosition;
            }
            set
            {
                this.KeyFlyKeyFrameParameters.SetBorderBevelPosition(value);
            }
        }
        public double BorderBevelSoftness
        {
            get
            {
                this.KeyFlyKeyFrameParameters.GetBorderBevelSoftness(out this._bevelSoft);
                return this._bevelSoft;
            }
            set
            {
                this.KeyFlyKeyFrameParameters.SetBorderBevelSoftness(value);
            }
        }
        public double BorderHue
        {
            get
            {
                this.KeyFlyKeyFrameParameters.GetBorderHue(out this._hue);
                return this._hue;
            }
            set
            {
                this.KeyFlyKeyFrameParameters.SetBorderHue(value);
            }
        }
        public double BorderLightSourceAltitude
        {
            get
            {
                this.KeyFlyKeyFrameParameters.GetBorderLightSourceAltitude(out this._altitude);
                return this._altitude;
            }
            set
            {
                this.KeyFlyKeyFrameParameters.SetBorderLightSourceAltitude(value);
            }
        }
        public double BorderLightSourceDirection
        {
            get
            {
                this.KeyFlyKeyFrameParameters.GetBorderLightSourceDirection(out this._degrees);
                return this._degrees;
            }
            set
            {
                this.KeyFlyKeyFrameParameters.SetBorderLightSourceDirection(value);
            }
        }
        public double BorderLuma
        {
            get
            {
                this.KeyFlyKeyFrameParameters.GetBorderLuma(out this._luma);
                return this._luma;
            }
            set
            {
                this.KeyFlyKeyFrameParameters.SetBorderLuma(value);
            }
        }
        public double BorderOpacity
        {
            get
            {
                this.KeyFlyKeyFrameParameters.GetBorderOpacity(out this._opacity);
                return this._opacity;
            }
            set
            {
                this.KeyFlyKeyFrameParameters.SetBorderOpacity(value);
            }
        }
        public double BorderSaturation
        {
            get
            {
                this.KeyFlyKeyFrameParameters.GetBorderSaturation(out this._sat);
                return this._sat;
            }
            set
            {
                this.KeyFlyKeyFrameParameters.SetBorderSaturation(value);
            }
        }
        public double BorderSoftnessIn
        {
            get
            {
                this.KeyFlyKeyFrameParameters.GetBorderSoftnessIn(out this._softIn);
                return this._softIn;
            }
            set
            {
                this.KeyFlyKeyFrameParameters.SetBorderSoftnessIn(value);
            }
        }
        public double BorderSoftnessOut
        {
            get
            {
                this.KeyFlyKeyFrameParameters.GetBorderSoftnessOut(out this._softOut);
                return this._softOut;
            }
            set
            {
                this.KeyFlyKeyFrameParameters.SetBorderSoftnessOut(value);
            }
        }
        public double BorderWidthIn
        {
            get
            {
                this.KeyFlyKeyFrameParameters.GetBorderWidthIn(out this._widthIn);
                return this._widthIn;
            }
            set
            {
                this.KeyFlyKeyFrameParameters.SetBorderWidthIn(value);
            }
        }
        public double BorderWidthOut
        {
            get
            {
                this.KeyFlyKeyFrameParameters.GetBorderWidthOut(out this._widthOut);
                return this._widthOut;
            }
            set
            {
                this.KeyFlyKeyFrameParameters.SetBorderWidthOut(value);
            }
        }
        public double CanRotate
        {
            get
            {
                this.KeyFlyKeyFrameParameters.GetCanRotate(out this._canRotate);
                return this._canRotate;
            }
        }
        public double CanScaleUp
        {
            get
            {
                this.KeyFlyKeyFrameParameters.GetCanScaleUp(out this._canScaleUp);
                return this._canScaleUp;
            }
        }
        public double MaskBottom
        {
            get
            {
                this.KeyFlyKeyFrameParameters.GetMaskBottom(out this._bottom);
                return this._bottom;
            }
            set
            {
                this.KeyFlyKeyFrameParameters.SetMaskBottom(value);
            }
        }
        public double MaskLeft
        {
            get
            {
                this.KeyFlyKeyFrameParameters.GetMaskLeft(out this._left);
                return this._left;
            }
            set
            {
                this.KeyFlyKeyFrameParameters.SetMaskLeft(value);
            }
        }
        public double MaskRight
        {
            get
            {
                this.KeyFlyKeyFrameParameters.GetMaskRight(out this._right);
                return this._right;
            }
            set
            {
                this.KeyFlyKeyFrameParameters.SetMaskRight(value);
            }
        }
        public double MaskTop
        {
            get
            {
                this.KeyFlyKeyFrameParameters.GetMaskTop(out this._top);
                return this._top;
            }
            set
            {
                this.KeyFlyKeyFrameParameters.SetMaskTop(value);
            }
        }
        public double PositionX
        {
            get
            {
                this.KeyFlyKeyFrameParameters.GetPositionX(out this._offsetX);
                return this._offsetX;
            }
            set
            {
                this.KeyFlyKeyFrameParameters.SetPositionX(value);
            }
        }
        public double PositionY
        {
            get
            {
                this.KeyFlyKeyFrameParameters.GetPositionY(out this._offsetY);
                return this._offsetY;
            }
            set
            {
                this.KeyFlyKeyFrameParameters.SetPositionY(value);
            }
        }
        public double Rotation
        {
            get
            {
                this.KeyFlyKeyFrameParameters.GetRotation(out this._degreesRotation);
                return this._degreesRotation;
            }
            set
            {
                this.KeyFlyKeyFrameParameters.SetRotation(value);
            }
        }
        public double SizeX
        {
            get
            {
                this.KeyFlyKeyFrameParameters.GetSizeX(out this._multiplierX);
                return this._multiplierX;
            }
            set
            {
                this.KeyFlyKeyFrameParameters.SetSizeX(value);
            }
        }
        public double SizeY
        {
            get
            {
                this.KeyFlyKeyFrameParameters.GetSizeY(out this._multiplierY);
                return this._multiplierY;
            }
            set
            {
                this.KeyFlyKeyFrameParameters.SetSizeY(value);
            }
        }
    }
}
