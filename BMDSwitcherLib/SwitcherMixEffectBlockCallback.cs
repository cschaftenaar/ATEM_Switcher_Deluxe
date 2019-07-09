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
        internal _BMDSwitcherMixEffectBlockPropertyId_v7_5 _propId;
        public _BMDSwitcherMixEffectBlockPropertyId_v7_5 PropId
        {
            get
            {
                return this._propId;
            }
        }
    }
    public delegate void SwitcherMixEffectBlockEventHandler(SwitcherMixEffectBlockCallback s, SwitcherMixEffectBlockEventArgs a);

    public class SwitcherMixEffectBlockCallback : IBMDSwitcherMixEffectBlockCallback_v7_5
    {
        // Events:
        public event SwitcherMixEffectBlockEventHandler SwitcherMixEffectBlockPropertyIdFadeToBlackFramesRemaining_v7_5;
        public event SwitcherMixEffectBlockEventHandler SwitcherMixEffectBlockPropertyIdFadeToBlackFullyBlack_v7_5;
        public event SwitcherMixEffectBlockEventHandler SwitcherMixEffectBlockPropertyIdFadeToBlackInTransition_v7_5;
        public event SwitcherMixEffectBlockEventHandler SwitcherMixEffectBlockPropertyIdFadeToBlackRate_v7_5;
        public event SwitcherMixEffectBlockEventHandler SwitcherMixEffectBlockPropertyIdInFadeToBlack_v7_5;
        public event SwitcherMixEffectBlockEventHandler SwitcherMixEffectBlockPropertyIdInputAvailabilityMask_v7_5;
        public event SwitcherMixEffectBlockEventHandler SwitcherMixEffectBlockPropertyIdInTransition_v7_5;
        public event SwitcherMixEffectBlockEventHandler SwitcherMixEffectBlockPropertyIdPreviewInput_v7_5;
        public event SwitcherMixEffectBlockEventHandler SwitcherMixEffectBlockPropertyIdPreviewLive_v7_5;
        public event SwitcherMixEffectBlockEventHandler SwitcherMixEffectBlockPropertyIdPreviewTransition_v7_5;
        public event SwitcherMixEffectBlockEventHandler SwitcherMixEffectBlockPropertyIdProgramInput_v7_5;
        public event SwitcherMixEffectBlockEventHandler SwitcherMixEffectBlockPropertyIdTransitionFramesRemaining_v7_5;
        public event SwitcherMixEffectBlockEventHandler SwitcherMixEffectBlockPropertyIdTransitionPosition_v7_5;

        private SwitcherMixEffectBlockEventArgs _switcherMixEffectBlockEventArgs;

        private int _indexnr;
        internal IBMDSwitcherMixEffectBlock_v7_5 MixEffectBlock;
        internal SwitcherMixEffectBlockCallback(IBMDSwitcherMixEffectBlock_v7_5 mixEffectBlock, int index)
        {
            this._indexnr = index;
            this.MixEffectBlock = mixEffectBlock;
        }

        void IBMDSwitcherMixEffectBlockCallback_v7_5.PropertyChanged(_BMDSwitcherMixEffectBlockPropertyId_v7_5 propId)
        {
            this._switcherMixEffectBlockEventArgs = new SwitcherMixEffectBlockEventArgs { _propId = propId };
            switch (propId)
            {
                case _BMDSwitcherMixEffectBlockPropertyId_v7_5.bmdSwitcherMixEffectBlockPropertyIdFadeToBlackFramesRemaining_v7_5:
                    this.SwitcherMixEffectBlockPropertyIdFadeToBlackFramesRemaining_v7_5?.Invoke(this, this._switcherMixEffectBlockEventArgs);
                    break;
                case _BMDSwitcherMixEffectBlockPropertyId_v7_5.bmdSwitcherMixEffectBlockPropertyIdFadeToBlackFullyBlack_v7_5:
                    this.SwitcherMixEffectBlockPropertyIdFadeToBlackFullyBlack_v7_5?.Invoke(this, this._switcherMixEffectBlockEventArgs);
                    break;
                case _BMDSwitcherMixEffectBlockPropertyId_v7_5.bmdSwitcherMixEffectBlockPropertyIdFadeToBlackInTransition_v7_5:
                    this.SwitcherMixEffectBlockPropertyIdFadeToBlackInTransition_v7_5?.Invoke(this, this._switcherMixEffectBlockEventArgs);
                    break;
                case _BMDSwitcherMixEffectBlockPropertyId_v7_5.bmdSwitcherMixEffectBlockPropertyIdFadeToBlackRate_v7_5:
                    this.SwitcherMixEffectBlockPropertyIdFadeToBlackRate_v7_5?.Invoke(this, this._switcherMixEffectBlockEventArgs);
                    break;
                case _BMDSwitcherMixEffectBlockPropertyId_v7_5.bmdSwitcherMixEffectBlockPropertyIdInFadeToBlack_v7_5:
                    this.SwitcherMixEffectBlockPropertyIdInFadeToBlack_v7_5?.Invoke(this, this._switcherMixEffectBlockEventArgs);
                    break;
                case _BMDSwitcherMixEffectBlockPropertyId_v7_5.bmdSwitcherMixEffectBlockPropertyIdInputAvailabilityMask_v7_5:
                    this.SwitcherMixEffectBlockPropertyIdInputAvailabilityMask_v7_5?.Invoke(this, this._switcherMixEffectBlockEventArgs);
                    break;
                case _BMDSwitcherMixEffectBlockPropertyId_v7_5.bmdSwitcherMixEffectBlockPropertyIdInTransition_v7_5:
                    this.SwitcherMixEffectBlockPropertyIdInTransition_v7_5?.Invoke(this, this._switcherMixEffectBlockEventArgs);
                    break;
                case _BMDSwitcherMixEffectBlockPropertyId_v7_5.bmdSwitcherMixEffectBlockPropertyIdPreviewInput_v7_5:
                    this.SwitcherMixEffectBlockPropertyIdPreviewInput_v7_5?.Invoke(this, this._switcherMixEffectBlockEventArgs);
                    break;
                case _BMDSwitcherMixEffectBlockPropertyId_v7_5.bmdSwitcherMixEffectBlockPropertyIdPreviewLive_v7_5:
                    this.SwitcherMixEffectBlockPropertyIdPreviewLive_v7_5?.Invoke(this, this._switcherMixEffectBlockEventArgs);
                    break;
                case _BMDSwitcherMixEffectBlockPropertyId_v7_5.bmdSwitcherMixEffectBlockPropertyIdPreviewTransition_v7_5:
                    this.SwitcherMixEffectBlockPropertyIdPreviewTransition_v7_5?.Invoke(this, this._switcherMixEffectBlockEventArgs);
                    break;
                case _BMDSwitcherMixEffectBlockPropertyId_v7_5.bmdSwitcherMixEffectBlockPropertyIdProgramInput_v7_5:
                    this.SwitcherMixEffectBlockPropertyIdProgramInput_v7_5?.Invoke(this, this._switcherMixEffectBlockEventArgs);
                    break;              
                case _BMDSwitcherMixEffectBlockPropertyId_v7_5.bmdSwitcherMixEffectBlockPropertyIdTransitionFramesRemaining_v7_5:
                    this.SwitcherMixEffectBlockPropertyIdTransitionFramesRemaining_v7_5?.Invoke(this, this._switcherMixEffectBlockEventArgs);
                    break;
                case _BMDSwitcherMixEffectBlockPropertyId_v7_5.bmdSwitcherMixEffectBlockPropertyIdTransitionPosition_v7_5:
                    this.SwitcherMixEffectBlockPropertyIdTransitionPosition_v7_5?.Invoke(this, this._switcherMixEffectBlockEventArgs);
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
        public void SetInt(_BMDSwitcherMixEffectBlockPropertyId_v7_5 propertyId, long value)
        {
            this.MixEffectBlock.SetInt(propertyId, value);
        }
        public long GetInt(_BMDSwitcherMixEffectBlockPropertyId_v7_5 propertyId)
        {
            this.MixEffectBlock.GetInt(propertyId, out this._intvalue);
            return this._intvalue;
        }
        public void SetString(_BMDSwitcherMixEffectBlockPropertyId_v7_5 propertyId, string strongvalue)
        {
            this.MixEffectBlock.SetString(propertyId, strongvalue);
        }
        public string GetString(_BMDSwitcherMixEffectBlockPropertyId_v7_5 propertyId)
        {
            this.MixEffectBlock.GetString(propertyId, out this._stringvalue);
            return this._stringvalue;
        }
        public void SetFlag(_BMDSwitcherMixEffectBlockPropertyId_v7_5 propertryId, int _intvalue)
        {
            this.MixEffectBlock.SetFlag(propertryId, _intvalue);
        }
        public int GetFlag(_BMDSwitcherMixEffectBlockPropertyId_v7_5 propertryId)
        {
            this.MixEffectBlock.GetFlag(propertryId, out this._flagvalue);
            return this._flagvalue;
        }
        public void SetFloat(_BMDSwitcherMixEffectBlockPropertyId_v7_5 propertryId, double _floatvalue)
        {
            this.MixEffectBlock.SetFloat(propertryId, _floatvalue);
        }
        public double GetFloat(_BMDSwitcherMixEffectBlockPropertyId_v7_5 propertryId)
        {
            this.MixEffectBlock.GetFloat(propertryId, out this._floatvalue);
            return this._floatvalue;
        }
    }
}