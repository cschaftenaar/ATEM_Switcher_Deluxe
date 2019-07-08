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
    public class SwitcherKeyLumaParametersEventArgs : EventArgs
    {
    }
    public delegate void SwitcherKeyLumaParametersEventHandler(SwitcherKeyLumaParametersCallback s, SwitcherKeyLumaParametersEventArgs a);

    public class SwitcherKeyLumaParametersCallback : IBMDSwitcherKeyLumaParametersCallback
    {
        // Events:
        public event SwitcherKeyLumaParametersEventHandler SwitcherKeyLumaParametersEventTypeClipChanged;
        public event SwitcherKeyLumaParametersEventHandler SwitcherKeyLumaParametersEventTypeGainChanged;
        public event SwitcherKeyLumaParametersEventHandler SwitcherKeyLumaParametersEventTypeInverseChanged;
        public event SwitcherKeyLumaParametersEventHandler SwitcherKeyLumaParametersEventTypePreMultipliedChanged;

        private SwitcherKeyLumaParametersEventArgs _switcherKeyLumaParametersEventArgs;

        private int _indexnr;
        internal IBMDSwitcherKeyLumaParameters KeyLumaParameters;
        internal SwitcherKeyLumaParametersCallback(IBMDSwitcherKeyLumaParameters keyLumaParameters, int index)
        {
            this._indexnr = index;
            this.KeyLumaParameters = keyLumaParameters;
        }

        void IBMDSwitcherKeyLumaParametersCallback.Notify(_BMDSwitcherKeyLumaParametersEventType eventType)
        {
            this._switcherKeyLumaParametersEventArgs = new SwitcherKeyLumaParametersEventArgs();
            switch (eventType)
            {
                case _BMDSwitcherKeyLumaParametersEventType.bmdSwitcherKeyLumaParametersEventTypeClipChanged:
                    this.SwitcherKeyLumaParametersEventTypeClipChanged?.Invoke(this, this._switcherKeyLumaParametersEventArgs);
                    break;
                case _BMDSwitcherKeyLumaParametersEventType.bmdSwitcherKeyLumaParametersEventTypeGainChanged:
                    this.SwitcherKeyLumaParametersEventTypeGainChanged?.Invoke(this, this._switcherKeyLumaParametersEventArgs);
                    break;
                case _BMDSwitcherKeyLumaParametersEventType.bmdSwitcherKeyLumaParametersEventTypeInverseChanged:
                    this.SwitcherKeyLumaParametersEventTypeInverseChanged?.Invoke(this, this._switcherKeyLumaParametersEventArgs);
                    break;
                case _BMDSwitcherKeyLumaParametersEventType.bmdSwitcherKeyLumaParametersEventTypePreMultipliedChanged:
                    this.SwitcherKeyLumaParametersEventTypePreMultipliedChanged?.Invoke(this, this._switcherKeyLumaParametersEventArgs);
                    break;
            }
        }

        private double _clip;
        private double _gain;
        private int _inverse;
        private int _preMultiplied;

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
                this.KeyLumaParameters.GetClip(out this._clip);
                return this._clip;
            }
            set
            {
                this.KeyLumaParameters.SetClip(value);
            }
        }
        public double Gain
        {
            get
            {
                this.KeyLumaParameters.GetGain(out this._gain);
                return this._gain;
            }
            set
            {
                this.KeyLumaParameters.SetGain(value);
            }
        }
        public int Inverse
        {
            get
            {
                this.KeyLumaParameters.GetInverse(out this._inverse);
                return this._inverse;
            }
            set
            {
                this.KeyLumaParameters.SetInverse(value);
            }
        }
        public int PreMultiplied
        {
            get
            {
                this.KeyLumaParameters.GetPreMultiplied(out this._preMultiplied);
                return this._preMultiplied;
            }
            set
            {
                this.KeyLumaParameters.SetPreMultiplied(value);
            }
        }
    }
}
