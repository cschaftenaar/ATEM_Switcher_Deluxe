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
    public class SwitcherMixEffectBlockEventArgs : EventArgs
    {
        internal _BMDSwitcherMixEffectBlockPropertyId _propId;
        public _BMDSwitcherMixEffectBlockPropertyId PropId
        {
            get
            {
                return this._propId;
            }
        }
    }
    public delegate void SwitcherMixEffectBlockEventHandler(SwitcherMixEffectBlockCallback s, SwitcherMixEffectBlockEventArgs a);

    public class SwitcherMixEffectBlockCallback : IBMDSwitcherMixEffectBlockCallback
    {
        // Events:
        public event SwitcherMixEffectBlockEventHandler SwitcherMixEffectBlockPropertyIdFadeToBlackFramesRemaining;
        public event SwitcherMixEffectBlockEventHandler SwitcherMixEffectBlockPropertyIdFadeToBlackFullyBlack;
        public event SwitcherMixEffectBlockEventHandler SwitcherMixEffectBlockPropertyIdFadeToBlackInTransition;
        public event SwitcherMixEffectBlockEventHandler SwitcherMixEffectBlockPropertyIdFadeToBlackRate;
        public event SwitcherMixEffectBlockEventHandler SwitcherMixEffectBlockPropertyIdInFadeToBlack;
        public event SwitcherMixEffectBlockEventHandler SwitcherMixEffectBlockPropertyIdInputAvailabilityMask;
        public event SwitcherMixEffectBlockEventHandler SwitcherMixEffectBlockPropertyIdProgramInput;
        public event SwitcherMixEffectBlockEventHandler SwitcherMixEffectBlockPropertyIdPreviewLive;
        public event SwitcherMixEffectBlockEventHandler SwitcherMixEffectBlockPropertyIdPreviewTransition;
        public event SwitcherMixEffectBlockEventHandler SwitcherMixEffectBlockPropertyIdPreviewInput;
        public event SwitcherMixEffectBlockEventHandler SwitcherMixEffectBlockPropertyIdTransitionFramesRemaining;
        public event SwitcherMixEffectBlockEventHandler SwitcherMixEffectBlockPropertyIdTransitionPosition;
        public event SwitcherMixEffectBlockEventHandler SwitcherMixEffectBlockPropertyIdInTransition;

        private SwitcherMixEffectBlockEventArgs _switcherMixEffectBlockEventArgs;

        private int _indexnr;
        internal IBMDSwitcherMixEffectBlock MixEffectBlock;
        internal SwitcherMixEffectBlockCallback(IBMDSwitcherMixEffectBlock mixEffectBlock, int index)
        {
            this._indexnr = index;
            this.MixEffectBlock = mixEffectBlock;
        }

        void IBMDSwitcherMixEffectBlockCallback.PropertyChanged(_BMDSwitcherMixEffectBlockPropertyId propId)
        {
            this._switcherMixEffectBlockEventArgs = new SwitcherMixEffectBlockEventArgs { _propId = propId };
            switch (propId)
            {
                case _BMDSwitcherMixEffectBlockPropertyId.bmdSwitcherMixEffectBlockPropertyIdFadeToBlackFramesRemaining:
                    this.SwitcherMixEffectBlockPropertyIdFadeToBlackFramesRemaining?.Invoke(this, this._switcherMixEffectBlockEventArgs);
                    break;
                case _BMDSwitcherMixEffectBlockPropertyId.bmdSwitcherMixEffectBlockPropertyIdFadeToBlackFullyBlack:
                    this.SwitcherMixEffectBlockPropertyIdFadeToBlackFullyBlack?.Invoke(this, this._switcherMixEffectBlockEventArgs);
                    break;
                case _BMDSwitcherMixEffectBlockPropertyId.bmdSwitcherMixEffectBlockPropertyIdFadeToBlackInTransition:
                    this.SwitcherMixEffectBlockPropertyIdFadeToBlackInTransition?.Invoke(this, this._switcherMixEffectBlockEventArgs);
                    break;
                case _BMDSwitcherMixEffectBlockPropertyId.bmdSwitcherMixEffectBlockPropertyIdFadeToBlackRate:
                    this.SwitcherMixEffectBlockPropertyIdFadeToBlackRate?.Invoke(this, this._switcherMixEffectBlockEventArgs);
                    break;
                case _BMDSwitcherMixEffectBlockPropertyId.bmdSwitcherMixEffectBlockPropertyIdInFadeToBlack:
                    this.SwitcherMixEffectBlockPropertyIdInFadeToBlack?.Invoke(this, this._switcherMixEffectBlockEventArgs);
                    break;
                case _BMDSwitcherMixEffectBlockPropertyId.bmdSwitcherMixEffectBlockPropertyIdInputAvailabilityMask:
                    this.SwitcherMixEffectBlockPropertyIdInputAvailabilityMask?.Invoke(this, this._switcherMixEffectBlockEventArgs);
                    break;
                case _BMDSwitcherMixEffectBlockPropertyId.bmdSwitcherMixEffectBlockPropertyIdInTransition:
                    this.SwitcherMixEffectBlockPropertyIdInTransition?.Invoke(this, this._switcherMixEffectBlockEventArgs);
                    break;
                case _BMDSwitcherMixEffectBlockPropertyId.bmdSwitcherMixEffectBlockPropertyIdPreviewInput:
                    this.SwitcherMixEffectBlockPropertyIdPreviewInput?.Invoke(this, this._switcherMixEffectBlockEventArgs);
                    break;
                case _BMDSwitcherMixEffectBlockPropertyId.bmdSwitcherMixEffectBlockPropertyIdPreviewLive:
                    this.SwitcherMixEffectBlockPropertyIdPreviewLive?.Invoke(this, this._switcherMixEffectBlockEventArgs);
                    break;
                case _BMDSwitcherMixEffectBlockPropertyId.bmdSwitcherMixEffectBlockPropertyIdPreviewTransition:
                    this.SwitcherMixEffectBlockPropertyIdPreviewTransition?.Invoke(this, this._switcherMixEffectBlockEventArgs);
                    break;
                case _BMDSwitcherMixEffectBlockPropertyId.bmdSwitcherMixEffectBlockPropertyIdProgramInput:
                    this.SwitcherMixEffectBlockPropertyIdProgramInput?.Invoke(this, this._switcherMixEffectBlockEventArgs);
                    break;              
                case _BMDSwitcherMixEffectBlockPropertyId.bmdSwitcherMixEffectBlockPropertyIdTransitionFramesRemaining:
                    this.SwitcherMixEffectBlockPropertyIdTransitionFramesRemaining?.Invoke(this, this._switcherMixEffectBlockEventArgs);
                    break;
                case _BMDSwitcherMixEffectBlockPropertyId.bmdSwitcherMixEffectBlockPropertyIdTransitionPosition:
                    this.SwitcherMixEffectBlockPropertyIdTransitionPosition?.Invoke(this, this._switcherMixEffectBlockEventArgs);
                    break;
            }
        }

        private long _intvalue;
        private string _stringvalue;
        private int _flagvalue;
        private double _floatvalue;

        public int IndexNr
        {
            get
            {
                return this._indexnr;
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
        public void SetInt(_BMDSwitcherMixEffectBlockPropertyId propertyId, long value)
        {
            this.MixEffectBlock.SetInt(propertyId, value);
        }
        public long GetInt(_BMDSwitcherMixEffectBlockPropertyId propertyId)
        {
            this.MixEffectBlock.GetInt(propertyId, out this._intvalue);
            return this._intvalue;
        }
        public void SetString(_BMDSwitcherMixEffectBlockPropertyId propertyId, string strongvalue)
        {
            this.MixEffectBlock.SetString(propertyId, strongvalue);
        }
        public string GetString(_BMDSwitcherMixEffectBlockPropertyId propertyId)
        {
            this.MixEffectBlock.GetString(propertyId, out this._stringvalue);
            return this._stringvalue;
        }
        public void SetFlag(_BMDSwitcherMixEffectBlockPropertyId propertryId, int _intvalue)
        {
            this.MixEffectBlock.SetFlag(propertryId, _intvalue);
        }
        public int GetFlag(_BMDSwitcherMixEffectBlockPropertyId propertryId)
        {
            this.MixEffectBlock.GetFlag(propertryId, out this._flagvalue);
            return this._flagvalue;
        }
        public void SetFloat(_BMDSwitcherMixEffectBlockPropertyId propertryId, double _floatvalue)
        {
            this.MixEffectBlock.SetFloat(propertryId, _floatvalue);
        }
        public double GetFloat(_BMDSwitcherMixEffectBlockPropertyId propertryId)
        {
            this.MixEffectBlock.GetFloat(propertryId, out this._floatvalue);
            return this._floatvalue;
        }
    }
}