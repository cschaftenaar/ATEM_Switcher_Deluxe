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
    public class SwitcherMacroPoolEventArgs : EventArgs
    {
        internal uint _index;
        internal IBMDSwitcherTransferMacro _macroTransfer;
        public uint Index
        {
            get
            {
                return this._index;
            }
        }
        public IBMDSwitcherTransferMacro MacroTransfer
        {
            get
            {
                return this._macroTransfer;
            }
        }
    }
    public delegate void SwitcherMacroPoolEventHandler(SwitcherMacroPoolCallback s, SwitcherMacroPoolEventArgs a);

    public class SwitcherMacroPoolCallback : IBMDSwitcherMacroPoolCallback
    {
        // Events:
        public event SwitcherMacroPoolEventHandler SwitcherMacroPoolEventTypeDescriptionChanged;
        public event SwitcherMacroPoolEventHandler SwitcherMacroPoolEventTypeHasUnsupportedOpsChanged;
        public event SwitcherMacroPoolEventHandler SwitcherMacroPoolEventTypeNameChanged;
        public event SwitcherMacroPoolEventHandler SwitcherMacroPoolEventTypeTransferCancelled;
        public event SwitcherMacroPoolEventHandler SwitcherMacroPoolEventTypeTransferCompleted;
        public event SwitcherMacroPoolEventHandler SwitcherMacroPoolEventTypeTransferFailed;
        public event SwitcherMacroPoolEventHandler SwitcherMacroPoolEventTypeValidChanged;

        private SwitcherMacroPoolEventArgs _switcherMacroPoolEventArgs;

        internal IBMDSwitcherMacroPool MacroPool;
        internal SwitcherMacroPoolCallback(IBMDSwitcherMacroPool macroPool)
        {
            this.MacroPool = macroPool;
        }

        void IBMDSwitcherMacroPoolCallback.Notify(_BMDSwitcherMacroPoolEventType eventType, uint index, IBMDSwitcherTransferMacro macroTransfer)
        {
            this._switcherMacroPoolEventArgs = new SwitcherMacroPoolEventArgs { _index = index, _macroTransfer = macroTransfer };
            switch(eventType)
            {
                case _BMDSwitcherMacroPoolEventType.bmdSwitcherMacroPoolEventTypeDescriptionChanged:
                    this.SwitcherMacroPoolEventTypeDescriptionChanged?.Invoke(this, this._switcherMacroPoolEventArgs);
                    break;
                case _BMDSwitcherMacroPoolEventType.bmdSwitcherMacroPoolEventTypeHasUnsupportedOpsChanged:
                    this.SwitcherMacroPoolEventTypeHasUnsupportedOpsChanged?.Invoke(this, this._switcherMacroPoolEventArgs);
                    break;
                case _BMDSwitcherMacroPoolEventType.bmdSwitcherMacroPoolEventTypeNameChanged:
                    this.SwitcherMacroPoolEventTypeNameChanged?.Invoke(this, this._switcherMacroPoolEventArgs);
                    break;
                case _BMDSwitcherMacroPoolEventType.bmdSwitcherMacroPoolEventTypeTransferCancelled:
                    this.SwitcherMacroPoolEventTypeTransferCancelled?.Invoke(this, this._switcherMacroPoolEventArgs);
                    break;
                case _BMDSwitcherMacroPoolEventType.bmdSwitcherMacroPoolEventTypeTransferCompleted:
                    this.SwitcherMacroPoolEventTypeTransferCompleted?.Invoke(this, this._switcherMacroPoolEventArgs);
                    break;
                case _BMDSwitcherMacroPoolEventType.bmdSwitcherMacroPoolEventTypeTransferFailed:
                    this.SwitcherMacroPoolEventTypeTransferFailed?.Invoke(this, this._switcherMacroPoolEventArgs);
                    break;
                case _BMDSwitcherMacroPoolEventType.bmdSwitcherMacroPoolEventTypeValidChanged:
                    this.SwitcherMacroPoolEventTypeValidChanged?.Invoke(this, this._switcherMacroPoolEventArgs);
                    break;
            }
        }

        private IBMDSwitcherMacro _macro;
        private IBMDSwitcherTransferMacro _macroTransfer;
        private string _description;
        private uint _maxCount;
        private string _name;
        private int _hasUnsupportedOps;
        private int _valid;

        public IBMDSwitcherMacro CreateMacro(uint sizeBytes)
        {
            this.MacroPool.CreateMacro(sizeBytes, out _macro);
            return this._macro;
        }
        public void Delete(uint index)
        {
            this.MacroPool.Delete(index);
        }
        public IBMDSwitcherTransferMacro Download(uint index)
        {
            this.MacroPool.Download(index, out this._macroTransfer);
            return this._macroTransfer;
        }
        public string GetDescription(uint index)
        {
            this.MacroPool.GetDescription(index, out this._description);
            return this._description;
        }
        public void SetDescription(uint index, string description)
        {
            this.MacroPool.SetDescription(index, description);
        }
        public uint MaxCount
        {
            get
            {
                this.MacroPool.GetMaxCount(out this._maxCount);
                return this._maxCount;
            }
        }
        public string GetName(uint index)
        {
            this.MacroPool.GetName(index, out this._name);
            return this._name;
        }
        public void SetName(uint index, string name)
        {
            this.MacroPool.SetName(index, name);
        }
        public int HasUnsupportedOps(uint index)
        {
            this.MacroPool.HasUnsupportedOps(index, out this._hasUnsupportedOps);
            return this._hasUnsupportedOps;
        }
        public int IsValid(uint index)
        {
            this.MacroPool.IsValid(index, out this._valid);
            return this._valid;
        }
        public IBMDSwitcherTransferMacro Upload(uint index, string name, string description, IBMDSwitcherMacro macro)
        {
            this.MacroPool.Upload(index, name, description, macro, out this._macroTransfer);
            return this._macroTransfer;
        }
    }
}
