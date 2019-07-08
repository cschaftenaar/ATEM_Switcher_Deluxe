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
    public class SwitcherTransitionMixParametersEventArgs : EventArgs
    {
    }
    public delegate void SwitcherTransitionMixParametersEventHandler(SwitcherTransitionMixParametersCallback s, SwitcherTransitionMixParametersEventArgs a);

    public class SwitcherTransitionMixParametersCallback : IBMDSwitcherTransitionMixParametersCallback
    {
        // Events:
        public event SwitcherTransitionMixParametersEventHandler SwitcherTransitionMixParametersEventTypeRateChanged;

        private SwitcherTransitionMixParametersEventArgs _switcherTransitionMixParametersEventArgs;

        internal IBMDSwitcherTransitionMixParameters TransitionMixParameters;
        internal SwitcherTransitionMixParametersCallback(IBMDSwitcherTransitionMixParameters transitionMixParameters)
        {
            this.TransitionMixParameters = transitionMixParameters;
        }

        void IBMDSwitcherTransitionMixParametersCallback.Notify(_BMDSwitcherTransitionMixParametersEventType eventType)
        {
            this._switcherTransitionMixParametersEventArgs = new SwitcherTransitionMixParametersEventArgs();
            switch (eventType)
            {
                case _BMDSwitcherTransitionMixParametersEventType.bmdSwitcherTransitionMixParametersEventTypeRateChanged:
                    SwitcherTransitionMixParametersEventTypeRateChanged?.Invoke(this, this._switcherTransitionMixParametersEventArgs);
                    break;
            }
        }

        private uint _frames;

        public uint Rate
        {
            get
            {
                TransitionMixParameters.GetRate(out this._frames);
                return this._frames;
            }
            set
            {
                TransitionMixParameters.SetRate(value);
            }
        }
    }
}
