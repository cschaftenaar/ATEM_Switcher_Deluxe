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
    public class SwitcherTransitionParametersEventArgs : EventArgs
    {
    }
    public delegate void SwitcherTransitionParametersEventHandler(SwitcherTransitionParametersCallback s, SwitcherTransitionParametersEventArgs a);

    public class SwitcherTransitionParametersCallback : IBMDSwitcherTransitionParametersCallback
    {
        // Events:
        public event SwitcherTransitionParametersEventHandler SwitcherTransitionParametersEventTypeNextTransitionSelectionChanged;
        public event SwitcherTransitionParametersEventHandler SwitcherTransitionParametersEventTypeNextTransitionStyleChanged;
        public event SwitcherTransitionParametersEventHandler SwitcherTransitionParametersEventTypeTransitionSelectionChanged;
        public event SwitcherTransitionParametersEventHandler SwitcherTransitionParametersEventTypeTransitionStyleChanged;

        private SwitcherTransitionParametersEventArgs _switcherTransitionParametersEventArgs;

        internal IBMDSwitcherTransitionParameters TransitionParameters;
        internal SwitcherTransitionParametersCallback(IBMDSwitcherTransitionParameters transitionParameters)
        {
            this.TransitionParameters = transitionParameters;
        }
        
        void IBMDSwitcherTransitionParametersCallback.Notify(_BMDSwitcherTransitionParametersEventType eventType)
        {
            this._switcherTransitionParametersEventArgs = new SwitcherTransitionParametersEventArgs();
            switch (eventType)
            {
                case _BMDSwitcherTransitionParametersEventType.bmdSwitcherTransitionParametersEventTypeNextTransitionSelectionChanged:
                    SwitcherTransitionParametersEventTypeNextTransitionSelectionChanged?.Invoke(this, this._switcherTransitionParametersEventArgs);
                    break;
                case _BMDSwitcherTransitionParametersEventType.bmdSwitcherTransitionParametersEventTypeNextTransitionStyleChanged:
                    SwitcherTransitionParametersEventTypeNextTransitionStyleChanged?.Invoke(this, this._switcherTransitionParametersEventArgs);
                    break;
                case _BMDSwitcherTransitionParametersEventType.bmdSwitcherTransitionParametersEventTypeTransitionSelectionChanged:
                    SwitcherTransitionParametersEventTypeTransitionSelectionChanged?.Invoke(this, this._switcherTransitionParametersEventArgs);
                    break;
                case _BMDSwitcherTransitionParametersEventType.bmdSwitcherTransitionParametersEventTypeTransitionStyleChanged:
                    SwitcherTransitionParametersEventTypeTransitionStyleChanged?.Invoke(this, this._switcherTransitionParametersEventArgs);
                    break;
            }
        }

        private _BMDSwitcherTransitionSelection _nextTransitionSelection;
        private _BMDSwitcherTransitionSelection _transitionSelection;
        private _BMDSwitcherTransitionStyle _nextTransitionStyle;
        private _BMDSwitcherTransitionStyle _transitionStyle;

        public _BMDSwitcherTransitionSelection NextTransitionSelection
        {
            get
            {
                this.TransitionParameters.GetNextTransitionSelection(out this._nextTransitionSelection);
                return this._nextTransitionSelection;
            }
            set
            {
                if(value>0)
                this.TransitionParameters.SetNextTransitionSelection(value);
            }
        }
        public _BMDSwitcherTransitionStyle NextTransitionStyle
        {
            get
            {
                this.TransitionParameters.GetNextTransitionStyle(out this._nextTransitionStyle);
                return this._nextTransitionStyle;
            }
            set
            {
                this.TransitionParameters.SetNextTransitionStyle(value);
            }
        }
        public _BMDSwitcherTransitionSelection TransitionSelection
        {
            get
            {
                this.TransitionParameters.GetTransitionSelection(out this._transitionSelection);
                return this._transitionSelection;
            }
        }
        public _BMDSwitcherTransitionStyle TransitionStyle
        {
            get
            {
                this.TransitionParameters.GetTransitionStyle(out this._transitionStyle);
                return this._transitionStyle;
            }
        }
    }
}
