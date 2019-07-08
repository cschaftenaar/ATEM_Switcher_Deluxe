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
    public class SwitcherMediaPoolEventArgs : EventArgs
    {
    }
    public delegate void SwitcherMediaPoolEventHandler(SwitcherMediaPoolCallback s, SwitcherMediaPoolEventArgs a);

    public class SwitcherMediaPoolCallback : IBMDSwitcherMediaPoolCallback
    {
        // Events:
        public event SwitcherMediaPoolEventHandler SwitcherMediaPoolEventClipFrameMaxCountsChanged;
        public event SwitcherMediaPoolEventHandler SwitcherMediaPoolEventFrameTotalForClipsChanged;

        private SwitcherMediaPoolEventArgs _switcherMediaPoolEventArgs;

        internal IBMDSwitcherMediaPool MediaPool;
        internal SwitcherMediaPoolCallback(IBMDSwitcherMediaPool mediaPool)
        {
            this.MediaPool = mediaPool;
        }

        void IBMDSwitcherMediaPoolCallback.ClipFrameMaxCountsChanged()
        {
            this._switcherMediaPoolEventArgs = new SwitcherMediaPoolEventArgs();
            this.SwitcherMediaPoolEventClipFrameMaxCountsChanged?.Invoke(this, this._switcherMediaPoolEventArgs);
        }
        void IBMDSwitcherMediaPoolCallback.FrameTotalForClipsChanged()
        {
            this._switcherMediaPoolEventArgs = new SwitcherMediaPoolEventArgs();
            this.SwitcherMediaPoolEventFrameTotalForClipsChanged?.Invoke(this, this._switcherMediaPoolEventArgs);
        }

        private IBMDSwitcherAudio _audio;
        private IBMDSwitcherFrame _frame;
        private IBMDSwitcherClip _clip;
        private uint _clipCount;
        private uint _clipMaxFrameCounts;
        private uint _total;
        private IBMDSwitcherStills _stills;

        public void Clear()
        {
            this.MediaPool.Clear();
        }
        public IBMDSwitcherAudio CreateAudio(uint sizeBytes)
        {
            this.MediaPool.CreateAudio(sizeBytes, out this._audio);
            return this._audio;
        }
        public IBMDSwitcherFrame CreateFrame(_BMDSwitcherPixelFormat pixelFormat, uint width, uint height)
        {
            this.MediaPool.CreateFrame(pixelFormat, width, height, out this._frame);
            return this._frame;
        }
        public IBMDSwitcherClip Clip(uint clipIndex)
        {
            this.MediaPool.GetClip(clipIndex, out this._clip);
            return this._clip;
        }
        public uint ClipCount
        {
            get
            {
                this.MediaPool.GetClipCount(out this._clipCount);
                return this._clipCount;
            }
        }
        public uint ClipMaxFrameCounts(uint clipCount)
        {
            this.MediaPool.GetClipMaxFrameCounts(clipCount, out _clipMaxFrameCounts);
            return this._clipMaxFrameCounts;
        }
        public uint FrameTotalForClips
        {
            get
            {
                this.MediaPool.GetFrameTotalForClips(out this._total);
                return this._total;
            }
        }
        public IBMDSwitcherStills Stills
        {
            get
            {
                this.MediaPool.GetStills(out this._stills);
                return this._stills;
            }
        }
        public void SetClipMaxFrameCounts(uint clipCount, ref uint clipMaxFrameCounts)
        {
            this.MediaPool.SetClipMaxFrameCounts(clipCount, clipMaxFrameCounts);
        }
    }
}