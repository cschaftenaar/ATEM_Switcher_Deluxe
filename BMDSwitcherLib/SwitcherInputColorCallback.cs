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
    public class SwitcherInputColorEventArgs : EventArgs
    {
    }
    public delegate void SwitcherInputColorEventHandler(SwitcherInputColorCallback s, SwitcherInputColorEventArgs a);

    public class SwitcherInputColorCallback : IBMDSwitcherInputColorCallback
    {
        // Events:
        public event SwitcherInputColorEventHandler SwitcherInputColorEventTypeHueChanged;
        public event SwitcherInputColorEventHandler SwitcherInputColorEventTypeLumaChanged;
        public event SwitcherInputColorEventHandler SwitcherInputColorEventTypeSaturationChanged;

        private SwitcherInputColorEventArgs _switcherInputColorEventArgs;

        private int _indexnr;
        internal IBMDSwitcherInputColor InputColor;
        internal SwitcherInputColorCallback(IBMDSwitcherInputColor inputColor, int index)
        {
            this._indexnr = index;
            this.InputColor = inputColor;
        }
        
        void IBMDSwitcherInputColorCallback.Notify(_BMDSwitcherInputColorEventType eventType)
        {
            this._switcherInputColorEventArgs = new SwitcherInputColorEventArgs();
            switch (eventType)
            {
                case _BMDSwitcherInputColorEventType.bmdSwitcherInputColorEventTypeHueChanged:
                    this.SwitcherInputColorEventTypeHueChanged?.Invoke(this, this._switcherInputColorEventArgs);
                    break;
                case _BMDSwitcherInputColorEventType.bmdSwitcherInputColorEventTypeLumaChanged:
                    this.SwitcherInputColorEventTypeLumaChanged?.Invoke(this, this._switcherInputColorEventArgs);
                    break;
                case _BMDSwitcherInputColorEventType.bmdSwitcherInputColorEventTypeSaturationChanged:
                    this.SwitcherInputColorEventTypeSaturationChanged?.Invoke(this, this._switcherInputColorEventArgs);
                    break;
            }
        }

        private double _hue;
        private double _luma;
        private double _sat;

        public int IndexNr
        {
            get
            {
                return this._indexnr;
            }
        }
        public double Hue
        {
            get
            {
                this.InputColor.GetHue(out this._hue);
                return this._hue;
            }
            set
            {
                this.InputColor.SetHue(value);
            }
        }
        public double Luma
        {
            get
            {
                this.InputColor.GetLuma(out this._luma);
                return this._luma;
            }
            set
            {
                this.InputColor.SetLuma(value);
            }
        }
        public double Saturation
        {
            get
            {
                this.InputColor.GetSaturation(out this._sat);
                return this._sat;
            }
            set
            {
                this.InputColor.SetSaturation(value);
            }
        }
    }
}
