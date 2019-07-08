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
    public class SwitcherKeyChromaParametersEventArgs : EventArgs
    {
    }
    public delegate void SwitcherKeyChromaParametersEventHandler(SwitcherKeyChromaParametersCallback s, SwitcherKeyChromaParametersEventArgs a);

    public class SwitcherKeyChromaParametersCallback : IBMDSwitcherKeyChromaParametersCallback
    {
        // Events:
        public event SwitcherKeyChromaParametersEventHandler SwitcherKeyChromaParametersEventTypeGainChanged;
        public event SwitcherKeyChromaParametersEventHandler SwitcherKeyChromaParametersEventTypeHueChanged;
        public event SwitcherKeyChromaParametersEventHandler SwitcherKeyChromaParametersEventTypeLiftChanged;
        public event SwitcherKeyChromaParametersEventHandler SwitcherKeyChromaParametersEventTypeNarrowChanged;
        public event SwitcherKeyChromaParametersEventHandler SwitcherKeyChromaParametersEventTypeYSuppressChanged;

        private SwitcherKeyChromaParametersEventArgs _switcherKeyChromaParametersEventArgs;

        private int _indexnr;
        internal IBMDSwitcherKeyChromaParameters KeyChromaParameters;
        internal SwitcherKeyChromaParametersCallback(IBMDSwitcherKeyChromaParameters keyChromaParameters, int index)
        {
            this._indexnr = index;
            this.KeyChromaParameters = keyChromaParameters;
        }

        void IBMDSwitcherKeyChromaParametersCallback.Notify(_BMDSwitcherKeyChromaParametersEventType eventType)
        {
            this._switcherKeyChromaParametersEventArgs = new SwitcherKeyChromaParametersEventArgs();
            switch (eventType)
            {
                case _BMDSwitcherKeyChromaParametersEventType.bmdSwitcherKeyChromaParametersEventTypeGainChanged:
                    this.SwitcherKeyChromaParametersEventTypeGainChanged?.Invoke(this, this._switcherKeyChromaParametersEventArgs);
                    break;
                case _BMDSwitcherKeyChromaParametersEventType.bmdSwitcherKeyChromaParametersEventTypeHueChanged:
                    this.SwitcherKeyChromaParametersEventTypeHueChanged?.Invoke(this, this._switcherKeyChromaParametersEventArgs);
                    break;
                case _BMDSwitcherKeyChromaParametersEventType.bmdSwitcherKeyChromaParametersEventTypeLiftChanged:
                    this.SwitcherKeyChromaParametersEventTypeLiftChanged?.Invoke(this, this._switcherKeyChromaParametersEventArgs);
                    break;
                case _BMDSwitcherKeyChromaParametersEventType.bmdSwitcherKeyChromaParametersEventTypeNarrowChanged:
                    this.SwitcherKeyChromaParametersEventTypeNarrowChanged?.Invoke(this, this._switcherKeyChromaParametersEventArgs);
                    break;
                case _BMDSwitcherKeyChromaParametersEventType.bmdSwitcherKeyChromaParametersEventTypeYSuppressChanged:
                    this.SwitcherKeyChromaParametersEventTypeYSuppressChanged?.Invoke(this, this._switcherKeyChromaParametersEventArgs);
                    break;
            }
        }

        private double _gain;
        private double _hue;
        private double _lift;
        private int _narrow;
        private double _ySuppress;

        public int IndexNr
        {
            get
            {
                return this._indexnr;
            }
        }
        public double Gain
        {
            get
            {
                this.KeyChromaParameters.GetGain(out this._gain);
                return this._gain;
            }
            set
            {
                this.KeyChromaParameters.SetGain(value);
            }
        }
        public double Hue
        {
            get
            {
                this.KeyChromaParameters.GetHue(out this._hue);
                return this._hue;
            }
            set
            {
                this.KeyChromaParameters.SetHue(value);
            }
        }
        public double Lift
        {
            get
            {
                this.KeyChromaParameters.GetLift(out this._lift);
                return this._lift;
            }
            set
            {
                this.KeyChromaParameters.SetLift(value);
            }
        }
        public int Narrow
        {
            get
            {
                this.KeyChromaParameters.GetNarrow(out this._narrow);
                return this._narrow;
            }
            set
            {
                this.KeyChromaParameters.SetNarrow(value);
            }
        }
        public double YSuppress
        {
            get
            {
                this.KeyChromaParameters.GetYSuppress(out this._ySuppress);
                return this._ySuppress;
            }
            set
            {
                this.KeyChromaParameters.SetYSuppress(value);
            }
        }
    }
}
