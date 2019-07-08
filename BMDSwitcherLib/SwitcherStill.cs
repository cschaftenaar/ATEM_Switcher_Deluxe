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
    public class SwitcherStill
    {
        private int _indexnr;
        internal IBMDSwitcherStills Stills;
        internal SwitcherStill(IBMDSwitcherStills stills, int index)
        {
            this._indexnr = index;
            this.Stills = stills;
        }

        private uint _count;
        private BMDSwitcherHash _hash;
        private string _name;
        private double _progress;
        private int _valid;

        public int IndexNr
        {
            get
            {
                return this._indexnr;
            }
        }
        public void CancelTransfer()
        {
            this.Stills.CancelTransfer();
        }
        public void Download()
        {
            this.Stills.Download((uint)this._indexnr);
        }
        public uint Count
        {
            get
            {
                this.Stills.GetCount(out this._count);
                return this._count;
            }
        }
        public BMDSwitcherHash Hash()
        {
            this.Stills.GetHash((uint)this._indexnr, out this._hash);
            return this._hash;
        }
        public string Name
        {
            get
            {
                this.Stills.GetName((uint)this._indexnr, out this._name);
                return this._name;
            }
            set
            {
                this.Stills.SetName((uint)this._indexnr, value);
            }
        }
        public double Progress
        {
            get
            {
                this.Stills.GetProgress(out this._progress);
                return this._progress;
            }
        }
        public int IsValid()
        {
            this.Stills.IsValid((uint)this._indexnr, out this._valid);
            return this._valid;
        }
        public void Lock(IBMDSwitcherLockCallback lockCallback)
        {
            this.Stills.Lock(lockCallback);
        }
        public void SetInvalid()
        {
            this.Stills.SetInvalid((uint)this._indexnr);
        }
        public void Unlock(IBMDSwitcherLockCallback lockCallback)
        {
            this.Stills.Unlock(lockCallback);
        }
        public void Upload(string name, IBMDSwitcherFrame frame)
        {
            this.Stills.Upload((uint)this._indexnr, name, frame);
        }
    }
}
