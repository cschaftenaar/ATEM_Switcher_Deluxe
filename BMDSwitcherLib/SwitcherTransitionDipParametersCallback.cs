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
    public class SwitcherTransitionDipParametersEventArgs : EventArgs
    {
    }
    public delegate void SwitcherTransitionDipParametersEventHandler(SwitcherTransitionDipParametersCallback s, SwitcherTransitionDipParametersEventArgs a);

    public class SwitcherTransitionDipParametersCallback : IBMDSwitcherTransitionDipParametersCallback
    {
        // Events:
        public event SwitcherTransitionDipParametersEventHandler SwitcherTransitionDipParametersEventTypeInputDipChanged;
        public event SwitcherTransitionDipParametersEventHandler SwitcherTransitionDipParametersEventTypeRateChanged;

        private SwitcherTransitionDipParametersEventArgs _switcherTransitionDipParametersEventArgs;

        internal IBMDSwitcherTransitionDipParameters TransitionDipParameters;
        internal SwitcherTransitionDipParametersCallback(IBMDSwitcherTransitionDipParameters transitionDipParameters)
        {
            this.TransitionDipParameters = transitionDipParameters;
        }

        void IBMDSwitcherTransitionDipParametersCallback.Notify(_BMDSwitcherTransitionDipParametersEventType eventType)
        {
            this._switcherTransitionDipParametersEventArgs = new SwitcherTransitionDipParametersEventArgs();
            switch (eventType)
            {
                case _BMDSwitcherTransitionDipParametersEventType.bmdSwitcherTransitionDipParametersEventTypeInputDipChanged:
                    SwitcherTransitionDipParametersEventTypeInputDipChanged?.Invoke(this, this._switcherTransitionDipParametersEventArgs);
                    break;
                case _BMDSwitcherTransitionDipParametersEventType.bmdSwitcherTransitionDipParametersEventTypeRateChanged:
                    SwitcherTransitionDipParametersEventTypeRateChanged?.Invoke(this, this._switcherTransitionDipParametersEventArgs);
                    break;
            }
        }

        private long _input;
        private uint _frames;

        public long InputDip
        {
            get
            {
                this.TransitionDipParameters.GetInputDip(out this._input);
                return this._input;
            }
            set
            {
                this.TransitionDipParameters.SetInputDip(value);
            }
        }
        public uint Rate
        {
            get
            {
                this.TransitionDipParameters.GetRate(out this._frames);
                return this._frames;
            }
            set
            {
                this.TransitionDipParameters.SetRate(value);
            }
        }
    }
}
