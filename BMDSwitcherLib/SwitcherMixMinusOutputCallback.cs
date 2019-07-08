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
    public class SwitcherMixMinusOutputEventArgs : EventArgs
    {
    }
    public delegate void SwitcherMixMinusOutputEventHandler(SwitcherMixMinusOutputCallback s, SwitcherMixMinusOutputEventArgs a);

    public class SwitcherMixMinusOutputCallback : IBMDSwitcherMixMinusOutputCallback
    {
        // Events:
        public event SwitcherMixMinusOutputEventHandler SwitcherMixMinusOutputEventTypeAudioModeChanged;

        private SwitcherMixMinusOutputEventArgs _switcherMixMinusOutputEventArgs;

        internal IBMDSwitcherMixMinusOutput MixMinusOutput;
        internal SwitcherMixMinusOutputCallback(IBMDSwitcherMixMinusOutput mixMinusOutput)
        {
            this.MixMinusOutput = mixMinusOutput;
        }

        void IBMDSwitcherMixMinusOutputCallback.Notify(_BMDSwitcherMixMinusOutputEventType eventType)
        {
            this._switcherMixMinusOutputEventArgs = new SwitcherMixMinusOutputEventArgs();
            switch (eventType)
            {
                case _BMDSwitcherMixMinusOutputEventType.bmdSwitcherMixMinusOutputEventTypeAudioModeChanged: 
                    this.SwitcherMixMinusOutputEventTypeAudioModeChanged?.Invoke(this, this._switcherMixMinusOutputEventArgs);
                    break;
            }
        }

        private _BMDSwitcherMixMinusOutputAudioMode _audioMode;
        private long _audioInputId;

        public _BMDSwitcherMixMinusOutputAudioMode AudioMode
        {
            get
            {
                this.MixMinusOutput.GetAudioMode(out this._audioMode);
                return this._audioMode;
            }
            set
            {
                this.MixMinusOutput.SetAudioMode(value);
            }
        }
        public long MinusAudioInputId
        {
            get
            {
                this.MixMinusOutput.GetMinusAudioInputId(out this._audioInputId);
                return this._audioInputId;
            }
        }
    }
}
