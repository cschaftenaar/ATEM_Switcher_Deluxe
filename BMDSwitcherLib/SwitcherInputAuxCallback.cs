﻿#region License
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
    public class SwitcherInputAuxEventArgs : EventArgs
    {
    }
    public delegate void SwitcherInputAuxEventHandler(SwitcherInputAuxCallback s, SwitcherInputAuxEventArgs a);

    public class SwitcherInputAuxCallback : IBMDSwitcherInputAuxCallback
    {
        // Events:
        public event SwitcherInputAuxEventHandler SwitcherInputAuxEventTypeInputSourceChanged;

        private SwitcherInputAuxEventArgs _switcherInputAuxEventArgs;

        private int _indexnr;
        internal IBMDSwitcherInputAux InputAux;
        internal SwitcherInputAuxCallback(IBMDSwitcherInputAux input, int index)
        {
            this._indexnr = index;
            this.InputAux = input;
        }

        void IBMDSwitcherInputAuxCallback.Notify(_BMDSwitcherInputAuxEventType eventType)
        {
            this._switcherInputAuxEventArgs = new SwitcherInputAuxEventArgs();
            switch (eventType)
            {
                case _BMDSwitcherInputAuxEventType.bmdSwitcherInputAuxEventTypeInputSourceChanged:
                    this.SwitcherInputAuxEventTypeInputSourceChanged?.Invoke(this, this._switcherInputAuxEventArgs);
                    break;
            }
        }

        private long _input;
        private _BMDSwitcherInputAvailability _availabilityMask;

        public int IndexNr
        {
            get
            {
                return this._indexnr;
            }
        }
        public _BMDSwitcherInputAvailability InputAvailabilityMask
        {
            get
            {
                this.InputAux.GetInputAvailabilityMask(out this._availabilityMask);
                return this._availabilityMask;
            }
        }
        public long InputSource
        {
            get
            {
                this.InputAux.GetInputSource(out this._input);
                return this._input;
            }
            set
            {
                this.InputAux.SetInputSource(value);
            }
        }
    }
}
