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
    public class SwitcherClipEventArgs : EventArgs
    {
        internal IBMDSwitcherFrame _frame;
        internal int _frameIndex;
        internal IBMDSwitcherAudio _audio;
        internal int _clipIndex;
        public IBMDSwitcherFrame Frame
        {
            get
            {
                return this._frame;
            }
        }
        public int FrameIndex
        {
            get
            {
                return this._frameIndex;
            }
        }
        public IBMDSwitcherAudio Audio
        {
            get
            {
                return this._audio;
            }
        }
        internal int ClipIndex
        {
            get
            {
                return this._clipIndex;
            }
        }
    }
    public delegate void SwitcherClipEventHandler(SwitcherClipCallback s, SwitcherClipEventArgs a);

    public class SwitcherClipCallback : IBMDSwitcherClipCallback
    {
        // Events:
        public event SwitcherClipEventHandler SwitcherMediaPoolEventTypeAudioHashChanged;
        public event SwitcherClipEventHandler SwitcherMediaPoolEventTypeAudioNameChanged;
        public event SwitcherClipEventHandler SwitcherMediaPoolEventTypeAudioValidChanged;
        public event SwitcherClipEventHandler SwitcherMediaPoolEventTypeHashChanged;
        public event SwitcherClipEventHandler SwitcherMediaPoolEventTypeLockBusy;
        public event SwitcherClipEventHandler SwitcherMediaPoolEventTypeLockIdle;
        public event SwitcherClipEventHandler SwitcherMediaPoolEventTypeNameChanged;
        public event SwitcherClipEventHandler SwitcherMediaPoolEventTypeTransferCancelled;
        public event SwitcherClipEventHandler SwitcherMediaPoolEventTypeTransferCompleted;
        public event SwitcherClipEventHandler SwitcherMediaPoolEventTypeTransferFailed;
        public event SwitcherClipEventHandler SwitcherMediaPoolEventTypeValidChanged;

        private SwitcherClipEventArgs _switcherClipEventArgs;

        private int _indexnr;
        internal IBMDSwitcherClip Clip;
        internal SwitcherClipCallback(IBMDSwitcherClip clip, int index)
        {
            this._indexnr = index;
            this.Clip = clip;
        }

        void IBMDSwitcherClipCallback.Notify(_BMDSwitcherMediaPoolEventType eventType, IBMDSwitcherFrame frame, int frameIndex, IBMDSwitcherAudio audio, int clipIndex)
        {
            this._switcherClipEventArgs = new SwitcherClipEventArgs { _frame = frame, _frameIndex = frameIndex, _audio = audio, _clipIndex = clipIndex };
            switch(eventType)
            {
                case _BMDSwitcherMediaPoolEventType.bmdSwitcherMediaPoolEventTypeAudioHashChanged:
                    this.SwitcherMediaPoolEventTypeAudioHashChanged?.Invoke(this, this._switcherClipEventArgs);
                    break;
                case _BMDSwitcherMediaPoolEventType.bmdSwitcherMediaPoolEventTypeAudioNameChanged:
                    this.SwitcherMediaPoolEventTypeAudioNameChanged?.Invoke(this, this._switcherClipEventArgs);
                    break;
                case _BMDSwitcherMediaPoolEventType.bmdSwitcherMediaPoolEventTypeAudioValidChanged:
                    this.SwitcherMediaPoolEventTypeAudioValidChanged?.Invoke(this, this._switcherClipEventArgs);
                    break;
                case _BMDSwitcherMediaPoolEventType.bmdSwitcherMediaPoolEventTypeHashChanged:
                    this.SwitcherMediaPoolEventTypeHashChanged?.Invoke(this, this._switcherClipEventArgs);
                    break;
                case _BMDSwitcherMediaPoolEventType.bmdSwitcherMediaPoolEventTypeLockBusy:
                    this.SwitcherMediaPoolEventTypeLockBusy?.Invoke(this, this._switcherClipEventArgs);
                    break;
                case _BMDSwitcherMediaPoolEventType.bmdSwitcherMediaPoolEventTypeLockIdle:
                    this.SwitcherMediaPoolEventTypeLockIdle?.Invoke(this, this._switcherClipEventArgs);
                    break;
                case _BMDSwitcherMediaPoolEventType.bmdSwitcherMediaPoolEventTypeNameChanged:
                    this.SwitcherMediaPoolEventTypeNameChanged?.Invoke(this, this._switcherClipEventArgs);
                    break;
                case _BMDSwitcherMediaPoolEventType.bmdSwitcherMediaPoolEventTypeTransferCancelled:
                    this.SwitcherMediaPoolEventTypeTransferCancelled?.Invoke(this, this._switcherClipEventArgs);
                    break;
                case _BMDSwitcherMediaPoolEventType.bmdSwitcherMediaPoolEventTypeTransferCompleted:
                    this.SwitcherMediaPoolEventTypeTransferCompleted?.Invoke(this, this._switcherClipEventArgs);
                    break;
                case _BMDSwitcherMediaPoolEventType.bmdSwitcherMediaPoolEventTypeTransferFailed:
                    this.SwitcherMediaPoolEventTypeTransferFailed?.Invoke(this, this._switcherClipEventArgs);
                    break;
                case _BMDSwitcherMediaPoolEventType.bmdSwitcherMediaPoolEventTypeValidChanged:
                    this.SwitcherMediaPoolEventTypeValidChanged?.Invoke(this, this._switcherClipEventArgs);
                    break;
            }
        }

        private BMDSwitcherHash _audiohash;
        private BMDSwitcherHash _hash2;
        private string _audioname;
        private uint _frameCount;
        private uint _index;
        private uint _maxFrameCount;
        private string _name;
        private double _progress;
        private int _audioValid;
        private int _frameValid;
        private int _valid;
        //private IBMDSwitcherLockCallback _lockCallback;

        public int IndexNr
        {
            get
            {
                return this._indexnr;
            }
        }
        public void CancelTransfer()
        {
            this.Clip.CancelTransfer();
        }
        public void DownloadAudio()
        {
            this.Clip.DownloadAudio();
        }
        public void DownloadFrame(uint frameIndex)
        {
            this.Clip.DownloadFrame(frameIndex);
        }
        public BMDSwitcherHash AudioHash
        {
            get
            {
                this.Clip.GetAudioHash(out this._audiohash);
                return this._audiohash;
            }
        }
        public string AudioName
        {
            get
            {
                this.Clip.GetAudioName(out this._audioname);
                return this._audioname;
            }
            set
            {
                this.Clip.SetAudioName(value);
            }
        }
        public uint FrameCount
        {
            get
            {
                this.Clip.GetFrameCount(out this._frameCount);
                return this._frameCount;
            }
        }
        public BMDSwitcherHash FrameHash(uint frameIndex)
        {
            this.Clip.GetFrameHash(frameIndex, out this._hash2);
            return this._hash2;
        }
        public uint Index
        {
            get
            {
                this.Clip.GetIndex(out this._index);
                return this._index;
            }
        }
        public uint MaxFrameCount
        {
            get
            {
                this.Clip.GetMaxFrameCount(out this._maxFrameCount);
                return this._maxFrameCount;
            }
        }
        public string Name
        {
            get
            {
                this.Clip.GetName(out this._name);
                return this._name;
            }
            set
            {
                this.Clip.SetName(value);
            }
        }
        public double Progress
        {
            get
            {
                this.Clip.GetProgress(out this._progress);
                return this._progress;
            }
        }
        public int IsAudioValid
        {
            get
            {
                this.Clip.IsAudioValid(out this._audioValid);
                return this._audioValid;
            }
        }
        public int IsFrameValid(uint frameIndex)
        {
            this.Clip.IsFrameValid(frameIndex, out this._frameValid);
            return this._frameValid;
        }
        public int IsValid
        {
            get
            {
                this.Clip.IsValid(out this._valid);
                return this._valid;
            }
        }
        public void Lock(IBMDSwitcherLockCallback lockCallback)
        {
            this.Clip.Lock(lockCallback);
        }
        public int SetAudioValid
        {
            set
            {
                this.Clip.SetAudioInvalid();
            }
        }
        public int SetInvalid
        {
            set
            {
                this.Clip.SetInvalid();
            }
        }
        public void SetValid(string name, uint frameCount)
        {
            this.Clip.SetValid(name, frameCount);
        }
        public void UnLock(IBMDSwitcherLockCallback lockCallback)
        {
            this.Clip.Unlock(lockCallback);
        }
        public void UploadAudio(string name, IBMDSwitcherAudio audio)
        {
            this.Clip.UploadAudio(name, audio);
        }
        public void UploadFrame(uint frameIndex, IBMDSwitcherFrame frame)
        {
            this.Clip.UploadFrame(frameIndex, frame);
        }
    }
}
