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
    public class SwitcherInputSuperSourceEventArgs_v7_5_2 : EventArgs
    {
    }
    public delegate void SwitcherInputSuperSourceEventHandler_v7_5_2(SwitcherInputSuperSourceCallback_v7_5_2 s, SwitcherInputSuperSourceEventArgs_v7_5_2 a);

    public class SwitcherInputSuperSourceCallback_v7_5_2 : IBMDSwitcherInputSuperSourceCallback_v7_5_2
    {
        // Events:
        public event SwitcherInputSuperSourceEventHandler_v7_5_2 SwitcherInputSuperSourceEventTypeArtOptionChanged_v7_5_2;
        public event SwitcherInputSuperSourceEventHandler_v7_5_2 SwitcherInputSuperSourceEventTypeBorderBevelChanged_v7_5_2;
        public event SwitcherInputSuperSourceEventHandler_v7_5_2 SwitcherInputSuperSourceEventTypeBorderBevelPositionChanged_v7_5_2;
        public event SwitcherInputSuperSourceEventHandler_v7_5_2 SwitcherInputSuperSourceEventTypeBorderBevelSoftnessChanged_v7_5_2;
        public event SwitcherInputSuperSourceEventHandler_v7_5_2 SwitcherInputSuperSourceEventTypeBorderEnabledChanged_v7_5_2;
        public event SwitcherInputSuperSourceEventHandler_v7_5_2 SwitcherInputSuperSourceEventTypeBorderHueChanged_v7_5_2;
        public event SwitcherInputSuperSourceEventHandler_v7_5_2 SwitcherInputSuperSourceEventTypeBorderLightSourceAltitudeChanged_v7_5_2;
        public event SwitcherInputSuperSourceEventHandler_v7_5_2 SwitcherInputSuperSourceEventTypeBorderLightSourceDirectionChanged_v7_5_2;
        public event SwitcherInputSuperSourceEventHandler_v7_5_2 SwitcherInputSuperSourceEventTypeBorderLumaChanged_v7_5_2;
        public event SwitcherInputSuperSourceEventHandler_v7_5_2 SwitcherInputSuperSourceEventTypeBorderSaturationChanged_v7_5_2;
        public event SwitcherInputSuperSourceEventHandler_v7_5_2 SwitcherInputSuperSourceEventTypeBorderSoftnessInChanged_v7_5_2;
        public event SwitcherInputSuperSourceEventHandler_v7_5_2 SwitcherInputSuperSourceEventTypeBorderSoftnessOutChanged_v7_5_2;
        public event SwitcherInputSuperSourceEventHandler_v7_5_2 SwitcherInputSuperSourceEventTypeBorderWidthInChanged_v7_5_2;
        public event SwitcherInputSuperSourceEventHandler_v7_5_2 SwitcherInputSuperSourceEventTypeBorderWidthOutChanged_v7_5_2;
        public event SwitcherInputSuperSourceEventHandler_v7_5_2 SwitcherInputSuperSourceEventTypeClipChanged_v7_5_2;
        public event SwitcherInputSuperSourceEventHandler_v7_5_2 SwitcherInputSuperSourceEventTypeGainChanged_v7_5_2;
        public event SwitcherInputSuperSourceEventHandler_v7_5_2 SwitcherInputSuperSourceEventTypeInputCutChanged_v7_5_2;
        public event SwitcherInputSuperSourceEventHandler_v7_5_2 SwitcherInputSuperSourceEventTypeInputFillChanged_v7_5_2;
        public event SwitcherInputSuperSourceEventHandler_v7_5_2 SwitcherInputSuperSourceEventTypeInverseChanged_v7_5_2;
        public event SwitcherInputSuperSourceEventHandler_v7_5_2 SwitcherInputSuperSourceEventTypePreMultipliedChanged_v7_5_2;

        private SwitcherInputSuperSourceEventArgs_v7_5_2 _switcherInputSuperSourceEventArgs;

        private int _indexnr;
        internal IBMDSwitcherInputSuperSource_v7_5_2 InputSuperSource_v7_5_2;
        internal SwitcherInputSuperSourceCallback_v7_5_2(IBMDSwitcherInputSuperSource_v7_5_2 inputSuperSource, int index)
        {
            this._indexnr = index;
            this.InputSuperSource_v7_5_2 = inputSuperSource;
        }

        void IBMDSwitcherInputSuperSourceCallback_v7_5_2.Notify(_BMDSwitcherInputSuperSourceEventType_v7_5_2 eventType)
        {
            this._switcherInputSuperSourceEventArgs = new SwitcherInputSuperSourceEventArgs_v7_5_2();
            switch (eventType)
            {
                case _BMDSwitcherInputSuperSourceEventType_v7_5_2.bmdSwitcherInputSuperSourceEventTypeArtOptionChanged_v7_5_2:
                    this.SwitcherInputSuperSourceEventTypeArtOptionChanged_v7_5_2?.Invoke(this, this._switcherInputSuperSourceEventArgs);
                    break;
                case _BMDSwitcherInputSuperSourceEventType_v7_5_2.bmdSwitcherInputSuperSourceEventTypeBorderBevelChanged_v7_5_2:
                    this.SwitcherInputSuperSourceEventTypeBorderBevelChanged_v7_5_2?.Invoke(this, this._switcherInputSuperSourceEventArgs);
                    break;
                case _BMDSwitcherInputSuperSourceEventType_v7_5_2.bmdSwitcherInputSuperSourceEventTypeBorderBevelPositionChanged_v7_5_2:
                    this.SwitcherInputSuperSourceEventTypeBorderBevelPositionChanged_v7_5_2?.Invoke(this, this._switcherInputSuperSourceEventArgs);
                    break;
                case _BMDSwitcherInputSuperSourceEventType_v7_5_2.bmdSwitcherInputSuperSourceEventTypeBorderBevelSoftnessChanged_v7_5_2:
                    this.SwitcherInputSuperSourceEventTypeBorderBevelSoftnessChanged_v7_5_2?.Invoke(this, this._switcherInputSuperSourceEventArgs);
                    break;
                case _BMDSwitcherInputSuperSourceEventType_v7_5_2.bmdSwitcherInputSuperSourceEventTypeBorderEnabledChanged_v7_5_2:
                    this.SwitcherInputSuperSourceEventTypeBorderEnabledChanged_v7_5_2?.Invoke(this, this._switcherInputSuperSourceEventArgs);
                    break;
                case _BMDSwitcherInputSuperSourceEventType_v7_5_2.bmdSwitcherInputSuperSourceEventTypeBorderHueChanged_v7_5_2:
                    this.SwitcherInputSuperSourceEventTypeBorderHueChanged_v7_5_2?.Invoke(this, this._switcherInputSuperSourceEventArgs);
                    break;
                case _BMDSwitcherInputSuperSourceEventType_v7_5_2.bmdSwitcherInputSuperSourceEventTypeBorderLightSourceAltitudeChanged_v7_5_2:
                    this.SwitcherInputSuperSourceEventTypeBorderLightSourceAltitudeChanged_v7_5_2?.Invoke(this, this._switcherInputSuperSourceEventArgs);
                    break;
                case _BMDSwitcherInputSuperSourceEventType_v7_5_2.bmdSwitcherInputSuperSourceEventTypeBorderLightSourceDirectionChanged_v7_5_2:
                    this.SwitcherInputSuperSourceEventTypeBorderLightSourceDirectionChanged_v7_5_2?.Invoke(this, this._switcherInputSuperSourceEventArgs);
                    break;
                case _BMDSwitcherInputSuperSourceEventType_v7_5_2.bmdSwitcherInputSuperSourceEventTypeBorderLumaChanged_v7_5_2:
                    this.SwitcherInputSuperSourceEventTypeBorderLumaChanged_v7_5_2?.Invoke(this, this._switcherInputSuperSourceEventArgs);
                    break;
                case _BMDSwitcherInputSuperSourceEventType_v7_5_2.bmdSwitcherInputSuperSourceEventTypeBorderSaturationChanged_v7_5_2:
                    this.SwitcherInputSuperSourceEventTypeBorderSaturationChanged_v7_5_2?.Invoke(this, this._switcherInputSuperSourceEventArgs);
                    break;
                case _BMDSwitcherInputSuperSourceEventType_v7_5_2.bmdSwitcherInputSuperSourceEventTypeBorderSoftnessInChanged_v7_5_2:
                    this.SwitcherInputSuperSourceEventTypeBorderSoftnessInChanged_v7_5_2?.Invoke(this, this._switcherInputSuperSourceEventArgs);
                    break;
                case _BMDSwitcherInputSuperSourceEventType_v7_5_2.bmdSwitcherInputSuperSourceEventTypeBorderSoftnessOutChanged_v7_5_2:
                    this.SwitcherInputSuperSourceEventTypeBorderSoftnessOutChanged_v7_5_2?.Invoke(this, this._switcherInputSuperSourceEventArgs);
                    break;
                case _BMDSwitcherInputSuperSourceEventType_v7_5_2.bmdSwitcherInputSuperSourceEventTypeBorderWidthInChanged_v7_5_2:
                    this.SwitcherInputSuperSourceEventTypeBorderWidthInChanged_v7_5_2?.Invoke(this, this._switcherInputSuperSourceEventArgs);
                    break;
                case _BMDSwitcherInputSuperSourceEventType_v7_5_2.bmdSwitcherInputSuperSourceEventTypeBorderWidthOutChanged_v7_5_2:
                    this.SwitcherInputSuperSourceEventTypeBorderWidthOutChanged_v7_5_2?.Invoke(this, this._switcherInputSuperSourceEventArgs);
                    break;
                case _BMDSwitcherInputSuperSourceEventType_v7_5_2.bmdSwitcherInputSuperSourceEventTypeClipChanged_v7_5_2:
                    this.SwitcherInputSuperSourceEventTypeClipChanged_v7_5_2?.Invoke(this, this._switcherInputSuperSourceEventArgs);
                    break;
                case _BMDSwitcherInputSuperSourceEventType_v7_5_2.bmdSwitcherInputSuperSourceEventTypeGainChanged_v7_5_2:
                    this.SwitcherInputSuperSourceEventTypeGainChanged_v7_5_2?.Invoke(this, this._switcherInputSuperSourceEventArgs);
                    break;
                case _BMDSwitcherInputSuperSourceEventType_v7_5_2.bmdSwitcherInputSuperSourceEventTypeInputCutChanged_v7_5_2:
                    this.SwitcherInputSuperSourceEventTypeInputCutChanged_v7_5_2?.Invoke(this, this._switcherInputSuperSourceEventArgs);
                    break;
                case _BMDSwitcherInputSuperSourceEventType_v7_5_2.bmdSwitcherInputSuperSourceEventTypeInputFillChanged_v7_5_2:
                    this.SwitcherInputSuperSourceEventTypeInputFillChanged_v7_5_2?.Invoke(this, this._switcherInputSuperSourceEventArgs);
                    break;
                case _BMDSwitcherInputSuperSourceEventType_v7_5_2.bmdSwitcherInputSuperSourceEventTypeInverseChanged_v7_5_2:
                    this.SwitcherInputSuperSourceEventTypeInverseChanged_v7_5_2?.Invoke(this, this._switcherInputSuperSourceEventArgs);
                    break;
                case _BMDSwitcherInputSuperSourceEventType_v7_5_2.bmdSwitcherInputSuperSourceEventTypePreMultipliedChanged_v7_5_2:
                    this.SwitcherInputSuperSourceEventTypePreMultipliedChanged_v7_5_2?.Invoke(this, this._switcherInputSuperSourceEventArgs);
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
                this.InputSuperSource_v7_5_2.GetArtOption(out this._artOption);
                return this._artOption;
            }
            set
            {
                this.InputSuperSource_v7_5_2.SetArtOption(value);
            }
        }
        public _BMDSwitcherBorderBevelOption BorderBevel
        {
            get
            {
                this.InputSuperSource_v7_5_2.GetBorderBevel(out this._bevelOption);
                return this._bevelOption;
            }
            set
            {
                this.InputSuperSource_v7_5_2.SetBorderBevel(value);
            }
        }
        public double BorderBevelPosition
        {
            get
            {
                this.InputSuperSource_v7_5_2.GetBorderBevelPosition(out this._bevelPosition);
                return this._bevelPosition;
            }
            set
            {
                this.InputSuperSource_v7_5_2.SetBorderBevelPosition(value);
            }
        }
        public double BorderBevelSoftness
        {
            get
            {
                this.InputSuperSource_v7_5_2.GetBorderBevelSoftness(out this._bevelSoftness);
                return this._bevelSoftness;
            }
            set
            {
                this.InputSuperSource_v7_5_2.SetBorderBevelSoftness(value);
            }
        }
        public int BorderEnabled
        {
            get
            {
                this.InputSuperSource_v7_5_2.GetBorderEnabled(out this._enabled);
                return this._enabled;
            }
            set
            {
                this.InputSuperSource_v7_5_2.SetBorderEnabled(value);
            }
        }
        public double BorderHue
        {
            get
            {
                this.InputSuperSource_v7_5_2.GetBorderHue(out this._hue);
                return this._hue;
            }
            set
            {
                this.InputSuperSource_v7_5_2.SetBorderHue(value);
            }
        }
        public double BorderLightSourceAltitude
        {
            get
            {
                this.InputSuperSource_v7_5_2.GetBorderLightSourceAltitude(out this._altitude);
                return this._altitude;
            }
            set
            {
                this.InputSuperSource_v7_5_2.SetBorderLightSourceAltitude(value);
            }
        }
        public double BorderLightSourceDirection
        {
            get
            {
                this.InputSuperSource_v7_5_2.GetBorderLightSourceDirection(out this._degrees);
                return this._degrees;
            }
            set
            {
                this.InputSuperSource_v7_5_2.SetBorderLightSourceDirection(value);
            }
        }
        public double BorderLuma
        {
            get
            {
                this.InputSuperSource_v7_5_2.GetBorderLuma(out this._luma);
                return this._luma;
            }
            set
            {
                this.InputSuperSource_v7_5_2.SetBorderLuma(value);
            }
        }
        public double BorderSaturation
        {
            get
            {
                this.InputSuperSource_v7_5_2.GetBorderSaturation(out this._sat);
                return this._sat;
            }
            set
            {
                this.InputSuperSource_v7_5_2.SetBorderSaturation(value);
            }
        }
        public double BorderSoftnessIn
        {
            get
            {
                this.InputSuperSource_v7_5_2.GetBorderSoftnessIn(out this._softnessIn);
                return this._softnessIn;
            }
            set
            {
                this.InputSuperSource_v7_5_2.SetBorderSoftnessIn(value);
            }
        }
        public double BorderSoftnessOut
        {
            get
            {
                this.InputSuperSource_v7_5_2.GetBorderSoftnessOut(out this._softnessOut);
                return this._softnessOut;
            }
            set
            {
                this.InputSuperSource_v7_5_2.SetBorderSoftnessOut(value);
            }
        }
        public double BorderWidthIn
        {
            get
            {
                this.InputSuperSource_v7_5_2.GetBorderWidthIn(out this._widthIn);
                return this._widthIn;
            }
            set
            {
                this.InputSuperSource_v7_5_2.SetBorderWidthIn(value);
            }
        }
        public double BorderWidthOut
        {
            get
            {
                this.InputSuperSource_v7_5_2.GetBorderWidthOut(out this._widthOut);
                return this._widthOut;
            }
            set
            {
                this.InputSuperSource_v7_5_2.SetBorderWidthOut(value);
            }
        }
        public double Clip
        {
            get
            {
                this.InputSuperSource_v7_5_2.GetClip(out this._clip);
                return this._clip;
            }
            set
            {
                this.InputSuperSource_v7_5_2.SetClip(value);
            }
        }
        public _BMDSwitcherInputAvailability CutInputAvailabilityMask
        {
            get
            {
                this.InputSuperSource_v7_5_2.GetCutInputAvailabilityMask(out this._cutInputAvailabilityMask);
                return this._cutInputAvailabilityMask;
            }
        }
        public _BMDSwitcherInputAvailability FillInputAvailabilityMask
        {
            get
            {
                this.InputSuperSource_v7_5_2.GetFillInputAvailabilityMask(out this._fillInputAvailabilityMask);
                return this._fillInputAvailabilityMask;
            }
        }
        public double Gain
        {
            get
            {
                this.InputSuperSource_v7_5_2.GetGain(out this._gain);
                return this._gain;
            }
            set
            {
                this.InputSuperSource_v7_5_2.SetGain(value);
            }
        }
        public long InputCut
        {
            get
            {
                this.InputSuperSource_v7_5_2.GetInputCut(out this._inputCut);
                return this._inputCut;
            }
            set
            {
                this.InputSuperSource_v7_5_2.SetInputCut(value);
            }
        }
        public long InputFill
        {
            get
            {
                this.InputSuperSource_v7_5_2.GetInputFill(out this._inputFill);
                return this._inputFill;
            }
            set
            {
                this.InputSuperSource_v7_5_2.SetInputFill(value);
            }
        }
        public int Inverse
        {
            get
            {
                this.InputSuperSource_v7_5_2.GetInverse(out this._inverse);
                return this._inverse;
            }
            set
            {
                this.InputSuperSource_v7_5_2.SetInverse(value);
            }
        }
        public int PreMultiplied
        {
            get
            {
                this.InputSuperSource_v7_5_2.GetPreMultiplied(out this._preMultiplied);
                return this._preMultiplied;
            }
            set
            {
                this.InputSuperSource_v7_5_2.SetPreMultiplied(value);
            }
        }
    }
}
