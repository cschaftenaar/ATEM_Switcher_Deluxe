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
    /// Version 7.5
    /// </summary>
    public class SwitcherMixEffectBlockEventArgs_v7_5 : EventArgs
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
    public delegate void SwitcherMixEffectBlockEventHandler_v7_5(SwitcherMixEffectBlockCallback_v7_5 s, SwitcherMixEffectBlockEventArgs_v7_5 a);

    public class SwitcherMixEffectBlockCallback_v7_5 : IBMDSwitcherMixEffectBlockCallback_v7_5
    {
        // Events:
        public event SwitcherMixEffectBlockEventHandler_v7_5 SwitcherMixEffectBlockPropertyIdFadeToBlackFramesRemaining_v7_5;
        public event SwitcherMixEffectBlockEventHandler_v7_5 SwitcherMixEffectBlockPropertyIdFadeToBlackFullyBlack_v7_5;
        public event SwitcherMixEffectBlockEventHandler_v7_5 SwitcherMixEffectBlockPropertyIdFadeToBlackInTransition_v7_5;
        public event SwitcherMixEffectBlockEventHandler_v7_5 SwitcherMixEffectBlockPropertyIdFadeToBlackRate_v7_5;
        public event SwitcherMixEffectBlockEventHandler_v7_5 SwitcherMixEffectBlockPropertyIdInFadeToBlack_v7_5;
        public event SwitcherMixEffectBlockEventHandler_v7_5 SwitcherMixEffectBlockPropertyIdInputAvailabilityMask_v7_5;
        public event SwitcherMixEffectBlockEventHandler_v7_5 SwitcherMixEffectBlockPropertyIdInTransition_v7_5;
        public event SwitcherMixEffectBlockEventHandler_v7_5 SwitcherMixEffectBlockPropertyIdPreviewInput_v7_5;
        public event SwitcherMixEffectBlockEventHandler_v7_5 SwitcherMixEffectBlockPropertyIdPreviewLive_v7_5;
        public event SwitcherMixEffectBlockEventHandler_v7_5 SwitcherMixEffectBlockPropertyIdPreviewTransition_v7_5;
        public event SwitcherMixEffectBlockEventHandler_v7_5 SwitcherMixEffectBlockPropertyIdProgramInput_v7_5;
        public event SwitcherMixEffectBlockEventHandler_v7_5 SwitcherMixEffectBlockPropertyIdTransitionFramesRemaining_v7_5;
        public event SwitcherMixEffectBlockEventHandler_v7_5 SwitcherMixEffectBlockPropertyIdTransitionPosition_v7_5;

        private SwitcherMixEffectBlockEventArgs_v7_5 _switcherMixEffectBlockEventArgs_v7_5;

        private int _indexnr;
        internal IBMDSwitcherMixEffectBlock_v7_5 MixEffectBlock_v7_5;
        internal SwitcherMixEffectBlockCallback_v7_5(IBMDSwitcherMixEffectBlock_v7_5 mixEffectBlock_v7_5, int index)
        {
            this._indexnr = index;
            this.MixEffectBlock_v7_5 = mixEffectBlock_v7_5;
        }

        void IBMDSwitcherMixEffectBlockCallback_v7_5.PropertyChanged(_BMDSwitcherMixEffectBlockPropertyId_v7_5 propId)
        {
            this._switcherMixEffectBlockEventArgs_v7_5 = new SwitcherMixEffectBlockEventArgs_v7_5 { _propId = propId };
            switch (propId)
            {
                case _BMDSwitcherMixEffectBlockPropertyId_v7_5.bmdSwitcherMixEffectBlockPropertyIdFadeToBlackFramesRemaining_v7_5:
                    this.SwitcherMixEffectBlockPropertyIdFadeToBlackFramesRemaining_v7_5?.Invoke(this, this._switcherMixEffectBlockEventArgs_v7_5);
                    break;
                case _BMDSwitcherMixEffectBlockPropertyId_v7_5.bmdSwitcherMixEffectBlockPropertyIdFadeToBlackFullyBlack_v7_5:
                    this.SwitcherMixEffectBlockPropertyIdFadeToBlackFullyBlack_v7_5?.Invoke(this, this._switcherMixEffectBlockEventArgs_v7_5);
                    break;
                case _BMDSwitcherMixEffectBlockPropertyId_v7_5.bmdSwitcherMixEffectBlockPropertyIdFadeToBlackInTransition_v7_5:
                    this.SwitcherMixEffectBlockPropertyIdFadeToBlackInTransition_v7_5?.Invoke(this, this._switcherMixEffectBlockEventArgs_v7_5);
                    break;
                case _BMDSwitcherMixEffectBlockPropertyId_v7_5.bmdSwitcherMixEffectBlockPropertyIdFadeToBlackRate_v7_5:
                    this.SwitcherMixEffectBlockPropertyIdFadeToBlackRate_v7_5?.Invoke(this, this._switcherMixEffectBlockEventArgs_v7_5);
                    break;
                case _BMDSwitcherMixEffectBlockPropertyId_v7_5.bmdSwitcherMixEffectBlockPropertyIdInFadeToBlack_v7_5:
                    this.SwitcherMixEffectBlockPropertyIdInFadeToBlack_v7_5?.Invoke(this, this._switcherMixEffectBlockEventArgs_v7_5);
                    break;
                case _BMDSwitcherMixEffectBlockPropertyId_v7_5.bmdSwitcherMixEffectBlockPropertyIdInputAvailabilityMask_v7_5:
                    this.SwitcherMixEffectBlockPropertyIdInputAvailabilityMask_v7_5?.Invoke(this, this._switcherMixEffectBlockEventArgs_v7_5);
                    break;
                case _BMDSwitcherMixEffectBlockPropertyId_v7_5.bmdSwitcherMixEffectBlockPropertyIdInTransition_v7_5:
                    this.SwitcherMixEffectBlockPropertyIdInTransition_v7_5?.Invoke(this, this._switcherMixEffectBlockEventArgs_v7_5);
                    break;
                case _BMDSwitcherMixEffectBlockPropertyId_v7_5.bmdSwitcherMixEffectBlockPropertyIdPreviewInput_v7_5:
                    this.SwitcherMixEffectBlockPropertyIdPreviewInput_v7_5?.Invoke(this, this._switcherMixEffectBlockEventArgs_v7_5);
                    break;
                case _BMDSwitcherMixEffectBlockPropertyId_v7_5.bmdSwitcherMixEffectBlockPropertyIdPreviewLive_v7_5:
                    this.SwitcherMixEffectBlockPropertyIdPreviewLive_v7_5?.Invoke(this, this._switcherMixEffectBlockEventArgs_v7_5);
                    break;
                case _BMDSwitcherMixEffectBlockPropertyId_v7_5.bmdSwitcherMixEffectBlockPropertyIdPreviewTransition_v7_5:
                    this.SwitcherMixEffectBlockPropertyIdPreviewTransition_v7_5?.Invoke(this, this._switcherMixEffectBlockEventArgs_v7_5);
                    break;
                case _BMDSwitcherMixEffectBlockPropertyId_v7_5.bmdSwitcherMixEffectBlockPropertyIdProgramInput_v7_5:
                    this.SwitcherMixEffectBlockPropertyIdProgramInput_v7_5?.Invoke(this, this._switcherMixEffectBlockEventArgs_v7_5);
                    break;              
                case _BMDSwitcherMixEffectBlockPropertyId_v7_5.bmdSwitcherMixEffectBlockPropertyIdTransitionFramesRemaining_v7_5:
                    this.SwitcherMixEffectBlockPropertyIdTransitionFramesRemaining_v7_5?.Invoke(this, this._switcherMixEffectBlockEventArgs_v7_5);
                    break;
                case _BMDSwitcherMixEffectBlockPropertyId_v7_5.bmdSwitcherMixEffectBlockPropertyIdTransitionPosition_v7_5:
                    this.SwitcherMixEffectBlockPropertyIdTransitionPosition_v7_5?.Invoke(this, this._switcherMixEffectBlockEventArgs_v7_5);
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
            this.MixEffectBlock_v7_5.PerformAutoTransition();
        }
        public void PerformCut()
        {
            this.MixEffectBlock_v7_5.PerformCut();
        }
        public void PerformFadeToBlack()
        {
            this.MixEffectBlock_v7_5.PerformFadeToBlack();
        }
        public void SetInt(_BMDSwitcherMixEffectBlockPropertyId_v7_5 propertyId, long value)
        {
            this.MixEffectBlock_v7_5.SetInt(propertyId, value);
        }
        public long GetInt(_BMDSwitcherMixEffectBlockPropertyId_v7_5 propertyId)
        {
            this.MixEffectBlock_v7_5.GetInt(propertyId, out this._intvalue);
            return this._intvalue;
        }
        public void SetString(_BMDSwitcherMixEffectBlockPropertyId_v7_5 propertyId, string strongvalue)
        {
            this.MixEffectBlock_v7_5.SetString(propertyId, strongvalue);
        }
        public string GetString(_BMDSwitcherMixEffectBlockPropertyId_v7_5 propertyId)
        {
            this.MixEffectBlock_v7_5.GetString(propertyId, out this._stringvalue);
            return this._stringvalue;
        }
        public void SetFlag(_BMDSwitcherMixEffectBlockPropertyId_v7_5 propertryId, int _intvalue)
        {
            this.MixEffectBlock_v7_5.SetFlag(propertryId, _intvalue);
        }
        public int GetFlag(_BMDSwitcherMixEffectBlockPropertyId_v7_5 propertryId)
        {
            this.MixEffectBlock_v7_5.GetFlag(propertryId, out this._flagvalue);
            return this._flagvalue;
        }
        public void SetFloat(_BMDSwitcherMixEffectBlockPropertyId_v7_5 propertryId, double _floatvalue)
        {
            this.MixEffectBlock_v7_5.SetFloat(propertryId, _floatvalue);
        }
        public double GetFloat(_BMDSwitcherMixEffectBlockPropertyId_v7_5 propertryId)
        {
            this.MixEffectBlock_v7_5.GetFloat(propertryId, out this._floatvalue);
            return this._floatvalue;
        }
    }
}