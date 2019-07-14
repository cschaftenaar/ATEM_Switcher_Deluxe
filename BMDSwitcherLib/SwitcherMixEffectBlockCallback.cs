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
    /// <summary>
    /// Version 8.x
    /// </summary>
    public class SwitcherMixEffectBlockEventArgs : EventArgs
    {
    }
    public delegate void SwitcherMixEffectBlockEventHandler(SwitcherMixEffectBlockCallback s, SwitcherMixEffectBlockEventArgs a);

    public class SwitcherMixEffectBlockCallback : IBMDSwitcherMixEffectBlockCallback
    {
        // Events:
        public event SwitcherMixEffectBlockEventHandler SwitcherMixEffectBlockEventTypeFadeToBlackFramesRemainingChanged;
        public event SwitcherMixEffectBlockEventHandler SwitcherMixEffectBlockEventTypeFadeToBlackFullyBlackChanged;
        public event SwitcherMixEffectBlockEventHandler SwitcherMixEffectBlockEventTypeFadeToBlackInTransitionChanged;
        public event SwitcherMixEffectBlockEventHandler SwitcherMixEffectBlockEventTypeFadeToBlackRateChanged;
        public event SwitcherMixEffectBlockEventHandler SwitcherMixEffectBlockEventTypeInFadeToBlackChanged;
        public event SwitcherMixEffectBlockEventHandler SwitcherMixEffectBlockEventTypeInputAvailabilityMaskChanged;
        public event SwitcherMixEffectBlockEventHandler SwitcherMixEffectBlockEventTypeInTransitionChanged;
        public event SwitcherMixEffectBlockEventHandler SwitcherMixEffectBlockEventTypePreviewInputChanged;
        public event SwitcherMixEffectBlockEventHandler SwitcherMixEffectBlockEventTypePreviewLiveChanged;
        public event SwitcherMixEffectBlockEventHandler SwitcherMixEffectBlockEventTypePreviewTransitionChanged;
        public event SwitcherMixEffectBlockEventHandler SwitcherMixEffectBlockEventTypeProgramInputChanged;
        public event SwitcherMixEffectBlockEventHandler SwitcherMixEffectBlockEventTypeTransitionFramesRemainingChanged;
        public event SwitcherMixEffectBlockEventHandler SwitcherMixEffectBlockEventTypeTransitionPositionChanged;

        private SwitcherMixEffectBlockEventArgs _switcherMixEffectBlockEventArgs;

        private int _indexnr;
        internal IBMDSwitcherMixEffectBlock MixEffectBlock;
        internal SwitcherMixEffectBlockCallback(IBMDSwitcherMixEffectBlock mixEffectBlock, int index)
        {
            this._indexnr = index;
            this.MixEffectBlock = mixEffectBlock;
        }

        public void Notify(_BMDSwitcherMixEffectBlockEventType eventType)
        {
            this._switcherMixEffectBlockEventArgs = new SwitcherMixEffectBlockEventArgs();
            switch (eventType)
            {
                case _BMDSwitcherMixEffectBlockEventType.bmdSwitcherMixEffectBlockEventTypeFadeToBlackFramesRemainingChanged:
                    this.SwitcherMixEffectBlockEventTypeFadeToBlackFramesRemainingChanged?.Invoke(this, this._switcherMixEffectBlockEventArgs);
                    break;
                case _BMDSwitcherMixEffectBlockEventType.bmdSwitcherMixEffectBlockEventTypeFadeToBlackFullyBlackChanged:
                    this.SwitcherMixEffectBlockEventTypeFadeToBlackFullyBlackChanged?.Invoke(this, this._switcherMixEffectBlockEventArgs);
                    break;
                case _BMDSwitcherMixEffectBlockEventType.bmdSwitcherMixEffectBlockEventTypeFadeToBlackInTransitionChanged:
                    this.SwitcherMixEffectBlockEventTypeFadeToBlackInTransitionChanged?.Invoke(this, this._switcherMixEffectBlockEventArgs);
                    break;
                case _BMDSwitcherMixEffectBlockEventType.bmdSwitcherMixEffectBlockEventTypeFadeToBlackRateChanged:
                    this.SwitcherMixEffectBlockEventTypeFadeToBlackRateChanged?.Invoke(this, this._switcherMixEffectBlockEventArgs);
                    break;
                case _BMDSwitcherMixEffectBlockEventType.bmdSwitcherMixEffectBlockEventTypeInFadeToBlackChanged:
                    this.SwitcherMixEffectBlockEventTypeInFadeToBlackChanged?.Invoke(this, this._switcherMixEffectBlockEventArgs);
                    break;
                case _BMDSwitcherMixEffectBlockEventType.bmdSwitcherMixEffectBlockEventTypeInputAvailabilityMaskChanged:
                    this.SwitcherMixEffectBlockEventTypeInputAvailabilityMaskChanged?.Invoke(this, this._switcherMixEffectBlockEventArgs);
                    break;
                case _BMDSwitcherMixEffectBlockEventType.bmdSwitcherMixEffectBlockEventTypeInTransitionChanged:
                    this.SwitcherMixEffectBlockEventTypeInTransitionChanged?.Invoke(this, this._switcherMixEffectBlockEventArgs);
                    break;
                case _BMDSwitcherMixEffectBlockEventType.bmdSwitcherMixEffectBlockEventTypePreviewInputChanged:
                    this.SwitcherMixEffectBlockEventTypePreviewInputChanged?.Invoke(this, this._switcherMixEffectBlockEventArgs);
                    break;
                case _BMDSwitcherMixEffectBlockEventType.bmdSwitcherMixEffectBlockEventTypePreviewLiveChanged:
                    this.SwitcherMixEffectBlockEventTypePreviewLiveChanged?.Invoke(this, this._switcherMixEffectBlockEventArgs);
                    break;
                case _BMDSwitcherMixEffectBlockEventType.bmdSwitcherMixEffectBlockEventTypePreviewTransitionChanged:
                    this.SwitcherMixEffectBlockEventTypePreviewTransitionChanged?.Invoke(this, this._switcherMixEffectBlockEventArgs);
                    break;
                case _BMDSwitcherMixEffectBlockEventType.bmdSwitcherMixEffectBlockEventTypeProgramInputChanged:
                    this.SwitcherMixEffectBlockEventTypeProgramInputChanged?.Invoke(this, this._switcherMixEffectBlockEventArgs);
                    break;
                case _BMDSwitcherMixEffectBlockEventType.bmdSwitcherMixEffectBlockEventTypeTransitionFramesRemainingChanged:
                    this.SwitcherMixEffectBlockEventTypeTransitionFramesRemainingChanged?.Invoke(this, this._switcherMixEffectBlockEventArgs);
                    break;
                case _BMDSwitcherMixEffectBlockEventType.bmdSwitcherMixEffectBlockEventTypeTransitionPositionChanged:
                    this.SwitcherMixEffectBlockEventTypeTransitionPositionChanged?.Invoke(this, this._switcherMixEffectBlockEventArgs);
                    break;
            }
        }

        private uint _FadeToBlackFramesRemaining;
        private int _FadeToBlackFullyBlack;
        private int _FadeToBlackInTransition;
        private uint _FadeToBlackRate;
        private int _InFadeToBlack;
        private _BMDSwitcherInputAvailability _BMDSwitcherInputAvailability;
        private int _InTransition;
        private long _PreviewInput;
        private int _PreviewLive;
        private int _PreviewTransition;
        private long _ProgramInput;
        private uint _TransitionFramesRemaining;
        private double _TransitionPosition;

        public int IndexNr
        {
            get
            {
                return this._indexnr;
            }
        }

        public uint FadeToBlackFramesRemaining
        {
            get
            {
                this.MixEffectBlock.GetFadeToBlackFramesRemaining(out this._FadeToBlackFramesRemaining);
                return this._FadeToBlackFramesRemaining;
            }
        }
        public int FadeToBlackFullyBlack
        {
            get
            {
                this.MixEffectBlock.GetFadeToBlackFullyBlack(out this._FadeToBlackFullyBlack);
                return this._FadeToBlackFullyBlack;
            }
            set
            {
                this.MixEffectBlock.SetFadeToBlackFullyBlack(value);
            }
        }
        public int FadeToBlackInTransition
        {
            get
            {
                this.MixEffectBlock.GetFadeToBlackInTransition(out this._FadeToBlackInTransition);
                return this._FadeToBlackInTransition;
            }
        }
        public uint FadeToBlackRate
        {
            get
            {
                this.MixEffectBlock.GetFadeToBlackRate(out this._FadeToBlackRate);
                return this._FadeToBlackRate;
            }
            set
            {
                this.MixEffectBlock.SetFadeToBlackRate(value);
            }
        }
        public int InFadeToBlack
        {
            get
            {
                this.MixEffectBlock.GetInFadeToBlack(out this._InFadeToBlack);
                return this._InFadeToBlack;
            }
        }
        public _BMDSwitcherInputAvailability InputAvailabilityMask
        {
            get
            {
                this.MixEffectBlock.GetInputAvailabilityMask(out this._BMDSwitcherInputAvailability);
                return this._BMDSwitcherInputAvailability;
            }
        }
        public int InTransition
        {
            get
            {
                this.MixEffectBlock.GetInTransition(out this._InTransition);
                return this._InTransition;
            }
        }
        public long PreviewInput
        {
            get
            {
                this.MixEffectBlock.GetPreviewInput(out this._PreviewInput);
                return this._PreviewInput;
            }
            set
            {
                this.MixEffectBlock.SetPreviewInput(value);
            }
        }
        public long PreviewLive
        {
            get
            {
                this.MixEffectBlock.GetPreviewLive(out this._PreviewLive);
                return this._PreviewLive;
            }
        }
        public int PreviewTransition
        {
            get
            {
                this.MixEffectBlock.GetPreviewTransition(out this._PreviewTransition);
                return this._PreviewTransition;
            }
            set
            {
                this.MixEffectBlock.SetPreviewTransition(value);
            }
        }
        public long ProgramInput
        {
            get
            {
                this.MixEffectBlock.GetProgramInput(out this._ProgramInput);
                return this._ProgramInput;
            }
            set
            {
                this.MixEffectBlock.SetProgramInput(value);
            }
        }
        public uint TransitionFramesRemaining
        {
            get
            {
                this.MixEffectBlock.GetTransitionFramesRemaining(out this._TransitionFramesRemaining);
                return this._TransitionFramesRemaining;
            }
        }
        public double TransitionPosition
        {
            get
            {
                this.MixEffectBlock.GetTransitionPosition(out this._TransitionPosition);
                return this._TransitionPosition;
            }
            set
            {
                this.MixEffectBlock.SetTransitionPosition(value);
            }
        }
        public void PerformAutoTransition()
        {
            this.MixEffectBlock.PerformAutoTransition();
        }
        public void PerformCut()
        {
            this.MixEffectBlock.PerformCut();
        }
        public void PerformFadeToBlack()
        {
            this.MixEffectBlock.PerformFadeToBlack();
        }
    }
}