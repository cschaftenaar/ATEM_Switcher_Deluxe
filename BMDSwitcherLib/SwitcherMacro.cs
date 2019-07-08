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
    public class SwitcherMacro
    {
        private int _indexnr;
        internal IBMDSwitcherMacroPool MacroPool;
        internal SwitcherMacro(IBMDSwitcherMacroPool switcherMacroPool, int index)
        {
            this._indexnr = index;
            this.MacroPool = switcherMacroPool;
        }

        private uint _maxCount;
        private int _valid;
        private int _HasUnsupportedOps;
        private string _name;
        private string _description;
        private IBMDSwitcherMacro _macro;
        private IBMDSwitcherTransferMacro _macroTransfer;

        public int IndexNr
        {
            get
            {
                return this._indexnr;
            }
        }
        public uint MaxCount
        {
            get
            {
                this.MacroPool.GetMaxCount(out this._maxCount);
                return this._maxCount;
            }
        }
        public void Delete()
        {
            this.MacroPool.Delete((uint)this._indexnr);
        }
        public int IsValid
        {
            get
            {
                this.MacroPool.IsValid((uint)this._indexnr, out this._valid);
                return this._valid;
            }
        }
        public int HasUnsupportedOps
        {
            get
            {
                this.MacroPool.HasUnsupportedOps((uint)this._indexnr, out this._HasUnsupportedOps);
                return this._HasUnsupportedOps;
            }
        }
        public string Name
        {
            get
            {
                this.MacroPool.GetName((uint)this._indexnr, out this._name);
                return this._name;
            }
            set
            {
                this.MacroPool.SetName((uint)this._indexnr, value);
            }
        }
        public string Description
        {
            get
            {
                this.MacroPool.GetDescription((uint)this._indexnr, out this._description);
                return this._description;
            }
            set
            {
                this.MacroPool.SetDescription((uint)this._indexnr, value);
            }
        }
        public IBMDSwitcherMacro CreateMacro(uint sizeBytes)
        {
            this.MacroPool.CreateMacro(sizeBytes, out _macro);
            return this._macro;
        }
        public IBMDSwitcherTransferMacro Upload(string name, string description, IBMDSwitcherMacro macro)
        {
            this.MacroPool.Upload((uint)this._indexnr, name, description, macro, out this._macroTransfer);
            return this._macroTransfer;
        }
        public IBMDSwitcherTransferMacro Download()
        {
            this.MacroPool.Download((uint)this._indexnr, out this._macroTransfer);
            return this._macroTransfer;
        }
    }
}
