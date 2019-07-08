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
    public class SwitcherInputSuperSourceEventArgs : EventArgs
    {
    }
    public delegate void SwitcherInputSuperSourceEventHandler(SwitcherInputSuperSourceCallback s, SwitcherInputSuperSourceEventArgs a);

    public class SwitcherInputSuperSourceCallback : IBMDSwitcherInputSuperSourceCallback
    {
        // Events:
        public event SwitcherInputSuperSourceEventHandler SwitcherInputSuperSourceEventTypeArtOptionChanged;
        public event SwitcherInputSuperSourceEventHandler SwitcherInputSuperSourceEventTypeBorderBevelChanged;
        public event SwitcherInputSuperSourceEventHandler SwitcherInputSuperSourceEventTypeBorderBevelPositionChanged;
        public event SwitcherInputSuperSourceEventHandler SwitcherInputSuperSourceEventTypeBorderBevelSoftnessChanged;
        public event SwitcherInputSuperSourceEventHandler SwitcherInputSuperSourceEventTypeBorderEnabledChanged;
        public event SwitcherInputSuperSourceEventHandler SwitcherInputSuperSourceEventTypeBorderHueChanged;
        public event SwitcherInputSuperSourceEventHandler SwitcherInputSuperSourceEventTypeBorderLightSourceAltitudeChanged;
        public event SwitcherInputSuperSourceEventHandler SwitcherInputSuperSourceEventTypeBorderLightSourceDirectionChanged;
        public event SwitcherInputSuperSourceEventHandler SwitcherInputSuperSourceEventTypeBorderLumaChanged;
        public event SwitcherInputSuperSourceEventHandler SwitcherInputSuperSourceEventTypeBorderSaturationChanged;
        public event SwitcherInputSuperSourceEventHandler SwitcherInputSuperSourceEventTypeBorderSoftnessInChanged;
        public event SwitcherInputSuperSourceEventHandler SwitcherInputSuperSourceEventTypeBorderSoftnessOutChanged;
        public event SwitcherInputSuperSourceEventHandler SwitcherInputSuperSourceEventTypeBorderWidthInChanged;
        public event SwitcherInputSuperSourceEventHandler SwitcherInputSuperSourceEventTypeBorderWidthOutChanged;
        public event SwitcherInputSuperSourceEventHandler SwitcherInputSuperSourceEventTypeClipChanged;
        public event SwitcherInputSuperSourceEventHandler SwitcherInputSuperSourceEventTypeGainChanged;
        public event SwitcherInputSuperSourceEventHandler SwitcherInputSuperSourceEventTypeInputCutChanged;
        public event SwitcherInputSuperSourceEventHandler SwitcherInputSuperSourceEventTypeInputFillChanged;
        public event SwitcherInputSuperSourceEventHandler SwitcherInputSuperSourceEventTypeInverseChanged;
        public event SwitcherInputSuperSourceEventHandler SwitcherInputSuperSourceEventTypePreMultipliedChanged;

        private SwitcherInputSuperSourceEventArgs _switcherInputSuperSourceEventArgs;

        private int _indexnr;
        internal IBMDSwitcherInputSuperSource InputSuperSource;
        internal SwitcherInputSuperSourceCallback(IBMDSwitcherInputSuperSource inputSuperSource, int index)
        {
            this._indexnr = index;
            this.InputSuperSource = inputSuperSource;
        }

        void IBMDSwitcherInputSuperSourceCallback.Notify(_BMDSwitcherInputSuperSourceEventType eventType)
        {
            this._switcherInputSuperSourceEventArgs = new SwitcherInputSuperSourceEventArgs();
            switch (eventType)
            {
                case _BMDSwitcherInputSuperSourceEventType.bmdSwitcherInputSuperSourceEventTypeArtOptionChanged:
                    this.SwitcherInputSuperSourceEventTypeArtOptionChanged?.Invoke(this, this._switcherInputSuperSourceEventArgs);
                    break;
                case _BMDSwitcherInputSuperSourceEventType.bmdSwitcherInputSuperSourceEventTypeBorderBevelChanged:
                    this.SwitcherInputSuperSourceEventTypeBorderBevelChanged?.Invoke(this, this._switcherInputSuperSourceEventArgs);
                    break;
                case _BMDSwitcherInputSuperSourceEventType.bmdSwitcherInputSuperSourceEventTypeBorderBevelPositionChanged:
                    this.SwitcherInputSuperSourceEventTypeBorderBevelPositionChanged?.Invoke(this, this._switcherInputSuperSourceEventArgs);
                    break;
                case _BMDSwitcherInputSuperSourceEventType.bmdSwitcherInputSuperSourceEventTypeBorderBevelSoftnessChanged:
                    this.SwitcherInputSuperSourceEventTypeBorderBevelSoftnessChanged?.Invoke(this, this._switcherInputSuperSourceEventArgs);
                    break;
                case _BMDSwitcherInputSuperSourceEventType.bmdSwitcherInputSuperSourceEventTypeBorderEnabledChanged:
                    this.SwitcherInputSuperSourceEventTypeBorderEnabledChanged?.Invoke(this, this._switcherInputSuperSourceEventArgs);
                    break;
                case _BMDSwitcherInputSuperSourceEventType.bmdSwitcherInputSuperSourceEventTypeBorderHueChanged:
                    this.SwitcherInputSuperSourceEventTypeBorderHueChanged?.Invoke(this, this._switcherInputSuperSourceEventArgs);
                    break;
                case _BMDSwitcherInputSuperSourceEventType.bmdSwitcherInputSuperSourceEventTypeBorderLightSourceAltitudeChanged:
                    this.SwitcherInputSuperSourceEventTypeBorderLightSourceAltitudeChanged?.Invoke(this, this._switcherInputSuperSourceEventArgs);
                    break;
                case _BMDSwitcherInputSuperSourceEventType.bmdSwitcherInputSuperSourceEventTypeBorderLightSourceDirectionChanged:
                    this.SwitcherInputSuperSourceEventTypeBorderLightSourceDirectionChanged?.Invoke(this, this._switcherInputSuperSourceEventArgs);
                    break;
                case _BMDSwitcherInputSuperSourceEventType.bmdSwitcherInputSuperSourceEventTypeBorderLumaChanged:
                    this.SwitcherInputSuperSourceEventTypeBorderLumaChanged?.Invoke(this, this._switcherInputSuperSourceEventArgs);
                    break;
                case _BMDSwitcherInputSuperSourceEventType.bmdSwitcherInputSuperSourceEventTypeBorderSaturationChanged:
                    this.SwitcherInputSuperSourceEventTypeBorderSaturationChanged?.Invoke(this, this._switcherInputSuperSourceEventArgs);
                    break;
                case _BMDSwitcherInputSuperSourceEventType.bmdSwitcherInputSuperSourceEventTypeBorderSoftnessInChanged:
                    this.SwitcherInputSuperSourceEventTypeBorderSoftnessInChanged?.Invoke(this, this._switcherInputSuperSourceEventArgs);
                    break;
                case _BMDSwitcherInputSuperSourceEventType.bmdSwitcherInputSuperSourceEventTypeBorderSoftnessOutChanged:
                    this.SwitcherInputSuperSourceEventTypeBorderSoftnessOutChanged?.Invoke(this, this._switcherInputSuperSourceEventArgs);
                    break;
                case _BMDSwitcherInputSuperSourceEventType.bmdSwitcherInputSuperSourceEventTypeBorderWidthInChanged:
                    this.SwitcherInputSuperSourceEventTypeBorderWidthInChanged?.Invoke(this, this._switcherInputSuperSourceEventArgs);
                    break;
                case _BMDSwitcherInputSuperSourceEventType.bmdSwitcherInputSuperSourceEventTypeBorderWidthOutChanged:
                    this.SwitcherInputSuperSourceEventTypeBorderWidthOutChanged?.Invoke(this, this._switcherInputSuperSourceEventArgs);
                    break;
                case _BMDSwitcherInputSuperSourceEventType.bmdSwitcherInputSuperSourceEventTypeClipChanged:
                    this.SwitcherInputSuperSourceEventTypeClipChanged?.Invoke(this, this._switcherInputSuperSourceEventArgs);
                    break;
                case _BMDSwitcherInputSuperSourceEventType.bmdSwitcherInputSuperSourceEventTypeGainChanged:
                    this.SwitcherInputSuperSourceEventTypeGainChanged?.Invoke(this, this._switcherInputSuperSourceEventArgs);
                    break;
                case _BMDSwitcherInputSuperSourceEventType.bmdSwitcherInputSuperSourceEventTypeInputCutChanged:
                    this.SwitcherInputSuperSourceEventTypeInputCutChanged?.Invoke(this, this._switcherInputSuperSourceEventArgs);
                    break;
                case _BMDSwitcherInputSuperSourceEventType.bmdSwitcherInputSuperSourceEventTypeInputFillChanged:
                    this.SwitcherInputSuperSourceEventTypeInputFillChanged?.Invoke(this, this._switcherInputSuperSourceEventArgs);
                    break;
                case _BMDSwitcherInputSuperSourceEventType.bmdSwitcherInputSuperSourceEventTypeInverseChanged:
                    this.SwitcherInputSuperSourceEventTypeInverseChanged?.Invoke(this, this._switcherInputSuperSourceEventArgs);
                    break;
                case _BMDSwitcherInputSuperSourceEventType.bmdSwitcherInputSuperSourceEventTypePreMultipliedChanged:
                    this.SwitcherInputSuperSourceEventTypePreMultipliedChanged?.Invoke(this, this._switcherInputSuperSourceEventArgs);
                    break;
            }
        }

        private _BMDSwitcherSuperSourceArtOption _artOption;
        private _BMDSwitcherBorderBevelOption _bevelOption;
        private double _bevelPosition;
        private double _bevelSoftness;
        private int _enabled;
        private double _hue;
        private double _altitude;
        private double _degrees;
        private double _luma;
        private double _sat;
        private double _softnessIn;
        private double _softnessOut;
        private double _widthIn;
        private double _widthOut;
        private double _clip;
        private _BMDSwitcherInputAvailability _cutInputAvailabilityMask;
        private _BMDSwitcherInputAvailability _fillInputAvailabilityMask;
        private double _gain;
        private long _inputCut;
        private long _inputFill;
        private int _inverse;
        private int _preMultiplied;

        public _BMDSwitcherSuperSourceArtOption ArtOption
        {
            get
            {
                this.InputSuperSource.GetArtOption(out this._artOption);
                return this._artOption;
            }
            set
            {
                this.InputSuperSource.SetArtOption(value);
            }
        }
        public _BMDSwitcherBorderBevelOption BorderBevel
        {
            get
            {
                this.InputSuperSource.GetBorderBevel(out this._bevelOption);
                return this._bevelOption;
            }
            set
            {
                this.InputSuperSource.SetBorderBevel(value);
            }
        }
        public double BorderBevelPosition
        {
            get
            {
                this.InputSuperSource.GetBorderBevelPosition(out this._bevelPosition);
                return this._bevelPosition;
            }
            set
            {
                this.InputSuperSource.SetBorderBevelPosition(value);
            }
        }
        public double BorderBevelSoftness
        {
            get
            {
                this.InputSuperSource.GetBorderBevelSoftness(out this._bevelSoftness);
                return this._bevelSoftness;
            }
            set
            {
                this.InputSuperSource.SetBorderBevelSoftness(value);
            }
        }
        public int BorderEnabled
        {
            get
            {
                this.InputSuperSource.GetBorderEnabled(out this._enabled);
                return this._enabled;
            }
            set
            {
                this.InputSuperSource.SetBorderEnabled(value);
            }
        }
        public double BorderHue
        {
            get
            {
                this.InputSuperSource.GetBorderHue(out this._hue);
                return this._hue;
            }
            set
            {
                this.InputSuperSource.SetBorderHue(value);
            }
        }
        public double BorderLightSourceAltitude
        {
            get
            {
                this.InputSuperSource.GetBorderLightSourceAltitude(out this._altitude);
                return this._altitude;
            }
            set
            {
                this.InputSuperSource.SetBorderLightSourceAltitude(value);
            }
        }
        public double BorderLightSourceDirection
        {
            get
            {
                this.InputSuperSource.GetBorderLightSourceDirection(out this._degrees);
                return this._degrees;
            }
            set
            {
                this.InputSuperSource.SetBorderLightSourceDirection(value);
            }
        }
        public double BorderLuma
        {
            get
            {
                this.InputSuperSource.GetBorderLuma(out this._luma);
                return this._luma;
            }
            set
            {
                this.InputSuperSource.SetBorderLuma(value);
            }
        }
        public double BorderSaturation
        {
            get
            {
                this.InputSuperSource.GetBorderSaturation(out this._sat);
                return this._sat;
            }
            set
            {
                this.InputSuperSource.SetBorderSaturation(value);
            }
        }
        public double BorderSoftnessIn
        {
            get
            {
                this.InputSuperSource.GetBorderSoftnessIn(out this._softnessIn);
                return this._softnessIn;
            }
            set
            {
                this.InputSuperSource.SetBorderSoftnessIn(value);
            }
        }
        public double BorderSoftnessOut
        {
            get
            {
                this.InputSuperSource.GetBorderSoftnessOut(out this._softnessOut);
                return this._softnessOut;
            }
            set
            {
                this.InputSuperSource.SetBorderSoftnessOut(value);
            }
        }
        public double BorderWidthIn
        {
            get
            {
                this.InputSuperSource.GetBorderWidthIn(out this._widthIn);
                return this._widthIn;
            }
            set
            {
                this.InputSuperSource.SetBorderWidthIn(value);
            }
        }
        public double BorderWidthOut
        {
            get
            {
                this.InputSuperSource.GetBorderWidthOut(out this._widthOut);
                return this._widthOut;
            }
            set
            {
                this.InputSuperSource.SetBorderWidthOut(value);
            }
        }
        public double Clip
        {
            get
            {
                this.InputSuperSource.GetClip(out this._clip);
                return this._clip;
            }
            set
            {
                this.InputSuperSource.SetClip(value);
            }
        }
        public _BMDSwitcherInputAvailability CutInputAvailabilityMask
        {
            get
            {
                this.InputSuperSource.GetCutInputAvailabilityMask(out this._cutInputAvailabilityMask);
                return this._cutInputAvailabilityMask;
            }
        }
        public _BMDSwitcherInputAvailability FillInputAvailabilityMask
        {
            get
            {
                this.InputSuperSource.GetFillInputAvailabilityMask(out this._fillInputAvailabilityMask);
                return this._fillInputAvailabilityMask;
            }
        }
        public double Gain
        {
            get
            {
                this.InputSuperSource.GetGain(out this._gain);
                return this._gain;
            }
            set
            {
                this.InputSuperSource.SetGain(value);
            }
        }
        public long InputCut
        {
            get
            {
                this.InputSuperSource.GetInputCut(out this._inputCut);
                return this._inputCut;
            }
            set
            {
                this.InputSuperSource.SetInputCut(value);
            }
        }
        public long InputFill
        {
            get
            {
                this.InputSuperSource.GetInputFill(out this._inputFill);
                return this._inputFill;
            }
            set
            {
                this.InputSuperSource.SetInputFill(value);
            }
        }
        public int Inverse
        {
            get
            {
                this.InputSuperSource.GetInverse(out this._inverse);
                return this._inverse;
            }
            set
            {
                this.InputSuperSource.SetInverse(value);
            }
        }
        public int PreMultiplied
        {
            get
            {
                this.InputSuperSource.GetPreMultiplied(out this._preMultiplied);
                return this._preMultiplied;
            }
            set
            {
                this.InputSuperSource.SetPreMultiplied(value);
            }
        }
    }
}
