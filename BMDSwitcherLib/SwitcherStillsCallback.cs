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
    public class SwitcherStillsEventArgs : EventArgs
    {
        internal IBMDSwitcherFrame _frame;
        internal int _index;
        public IBMDSwitcherFrame Frame
        {
            get
            {
                return this._frame;
            }
        }
        public int Index
        {
            get
            {
                return this._index;
            }
        }
    }
    public delegate void SwitcherStillsEventHandler(SwitcherStillsCallback s, SwitcherStillsEventArgs a);

    public class SwitcherStillsCallback : IBMDSwitcherStillsCallback
    {
        // Events:
        public event SwitcherStillsEventHandler SwitcherMediaPoolEventTypeAudioHashChanged;
        public event SwitcherStillsEventHandler SwitcherMediaPoolEventTypeAudioNameChanged;
        public event SwitcherStillsEventHandler SwitcherMediaPoolEventTypeAudioValidChanged;
        public event SwitcherStillsEventHandler SwitcherMediaPoolEventTypeHashChanged;
        public event SwitcherStillsEventHandler SwitcherMediaPoolEventTypeLockBusy;
        public event SwitcherStillsEventHandler SwitcherMediaPoolEventTypeLockIdle;
        public event SwitcherStillsEventHandler SwitcherMediaPoolEventTypeNameChanged;
        public event SwitcherStillsEventHandler SwitcherMediaPoolEventTypeTransferCancelled;
        public event SwitcherStillsEventHandler SwitcherMediaPoolEventTypeTransferCompleted;
        public event SwitcherStillsEventHandler SwitcherMediaPoolEventTypeTransferFailed;
        public event SwitcherStillsEventHandler SwitcherMediaPoolEventTypeValidChanged;

        private SwitcherStillsEventArgs _switcherStillsEventArgs;

        internal IBMDSwitcherStills Stills;
        internal SwitcherStillsCallback(IBMDSwitcherStills stills)
        {
            this.Stills = stills;
        }

        void IBMDSwitcherStillsCallback.Notify(_BMDSwitcherMediaPoolEventType eventType, IBMDSwitcherFrame frame, int index)
        {
            this._switcherStillsEventArgs = new SwitcherStillsEventArgs { _frame = frame, _index = index };
            switch (eventType)
            {
                case _BMDSwitcherMediaPoolEventType.bmdSwitcherMediaPoolEventTypeAudioHashChanged:
                    this.SwitcherMediaPoolEventTypeAudioHashChanged?.Invoke(this, this._switcherStillsEventArgs);
                    break;
                case _BMDSwitcherMediaPoolEventType.bmdSwitcherMediaPoolEventTypeAudioNameChanged:
                    this.SwitcherMediaPoolEventTypeAudioNameChanged?.Invoke(this, this._switcherStillsEventArgs);
                    break;
                case _BMDSwitcherMediaPoolEventType.bmdSwitcherMediaPoolEventTypeAudioValidChanged:
                    this.SwitcherMediaPoolEventTypeAudioValidChanged?.Invoke(this, this._switcherStillsEventArgs);
                    break;
                case _BMDSwitcherMediaPoolEventType.bmdSwitcherMediaPoolEventTypeHashChanged:
                    this.SwitcherMediaPoolEventTypeHashChanged?.Invoke(this, this._switcherStillsEventArgs);
                    break;
                case _BMDSwitcherMediaPoolEventType.bmdSwitcherMediaPoolEventTypeLockBusy:
                    this.SwitcherMediaPoolEventTypeLockBusy?.Invoke(this, this._switcherStillsEventArgs);
                    break;
                case _BMDSwitcherMediaPoolEventType.bmdSwitcherMediaPoolEventTypeLockIdle:
                    this.SwitcherMediaPoolEventTypeLockIdle?.Invoke(this, this._switcherStillsEventArgs);
                    break;
                case _BMDSwitcherMediaPoolEventType.bmdSwitcherMediaPoolEventTypeNameChanged:
                    this.SwitcherMediaPoolEventTypeNameChanged?.Invoke(this, this._switcherStillsEventArgs);
                    break;
                case _BMDSwitcherMediaPoolEventType.bmdSwitcherMediaPoolEventTypeTransferCancelled:
                    this.SwitcherMediaPoolEventTypeTransferCancelled?.Invoke(this, this._switcherStillsEventArgs);
                    break;
                case _BMDSwitcherMediaPoolEventType.bmdSwitcherMediaPoolEventTypeTransferCompleted:
                    this.SwitcherMediaPoolEventTypeTransferCompleted?.Invoke(this, this._switcherStillsEventArgs);
                    break;
                case _BMDSwitcherMediaPoolEventType.bmdSwitcherMediaPoolEventTypeTransferFailed:
                    this.SwitcherMediaPoolEventTypeTransferFailed?.Invoke(this, this._switcherStillsEventArgs);
                    break;
                case _BMDSwitcherMediaPoolEventType.bmdSwitcherMediaPoolEventTypeValidChanged:
                    this.SwitcherMediaPoolEventTypeValidChanged?.Invoke(this, this._switcherStillsEventArgs);
                    break;
            }
        }

        private uint _count;
        private BMDSwitcherHash _hash;
        private string _name;
        private double _progress;
        private int _valid;

        public void CancelTransfer()
        {
            this.Stills.CancelTransfer();
        }
        public void Download(uint index)
        {
            this.Stills.Download(index);
        }
        public uint Count
        {
            get
            {
                this.Stills.GetCount(out this._count);
                return this._count;
            }
        }
        public BMDSwitcherHash Hash(uint index)
        {
            this.Stills.GetHash(index, out this._hash);
            return this._hash;
        }
        public string GetName(uint index)
        {
            this.Stills.GetName(index, out this._name);
            return this._name;
        }
        public double Progress
        {
            get
            {
                this.Stills.GetProgress(out this._progress);
                return this._progress;
            }
        }
        public int IsValid(uint index)
        {
            this.Stills.IsValid(index, out this._valid);
            return this._valid;
        }
        public void Lock(IBMDSwitcherLockCallback lockCallback)
        {
            this.Stills.Lock(lockCallback);
        }
        public void SetInvalid(uint index)
        {
            this.Stills.SetInvalid(index);
        }
        public void SetName(uint index, string name)
        {
            this.Stills.SetName(index, name);
        }
        public void Unlock(IBMDSwitcherLockCallback lockCallback)
        {
            this.Stills.Unlock(lockCallback);
        }
        public void Upload(uint index, string name, IBMDSwitcherFrame frame)
        {
            this.Stills.Upload(index, name, frame);
        }
    }
}
