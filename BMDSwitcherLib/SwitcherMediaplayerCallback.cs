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
    public class SwitcherMediaPlayerEventArgs : EventArgs
    {
    }
    public delegate void SwitcherMediaPlayerEventHandler(SwitcherMediaPlayerCallback s, SwitcherMediaPlayerEventArgs a);

    public class MediaPlayerSource
    {
        public _BMDSwitcherMediaPlayerSourceType type;
        public uint index;
    }

    public class SwitcherMediaPlayerCallback : IBMDSwitcherMediaPlayerCallback
    {
        // Events:
        public event SwitcherMediaPlayerEventHandler SwitcherMediaPlayerEventAtBeginningChanged;
        public event SwitcherMediaPlayerEventHandler SwitcherMediaPlayerEventSourceChanged;
        public event SwitcherMediaPlayerEventHandler SwitcherMediaPlayerEventPlayingChanged;
        public event SwitcherMediaPlayerEventHandler SwitcherMediaPlayerEventLoopChanged;
        public event SwitcherMediaPlayerEventHandler SwitcherMediaPlayerEventClipFrameChanged;

        private SwitcherMediaPlayerEventArgs _switcherMediaPlayerEventArgs;

        private int _indexnr;
        internal IBMDSwitcherMediaPlayer MediaPlayer;
        internal SwitcherMediaPlayerCallback(IBMDSwitcherMediaPlayer mediaplayer, int index)
        {
            this._indexnr = index;
            this.MediaPlayer = mediaplayer;
        }

        void IBMDSwitcherMediaPlayerCallback.AtBeginningChanged()
        {
            this._switcherMediaPlayerEventArgs = new SwitcherMediaPlayerEventArgs();
            this.SwitcherMediaPlayerEventAtBeginningChanged?.Invoke(this, this._switcherMediaPlayerEventArgs);
        }
        void IBMDSwitcherMediaPlayerCallback.SourceChanged()
        {
            this._switcherMediaPlayerEventArgs = new SwitcherMediaPlayerEventArgs();
            this.SwitcherMediaPlayerEventSourceChanged?.Invoke(this, this._switcherMediaPlayerEventArgs);
        }
        void IBMDSwitcherMediaPlayerCallback.PlayingChanged()
        {
            this._switcherMediaPlayerEventArgs = new SwitcherMediaPlayerEventArgs();
            this.SwitcherMediaPlayerEventPlayingChanged?.Invoke(this, this._switcherMediaPlayerEventArgs);
        }
        void IBMDSwitcherMediaPlayerCallback.LoopChanged()
        {
            this._switcherMediaPlayerEventArgs = new SwitcherMediaPlayerEventArgs();
            this.SwitcherMediaPlayerEventLoopChanged?.Invoke(this, this._switcherMediaPlayerEventArgs);
        }
        void IBMDSwitcherMediaPlayerCallback.ClipFrameChanged()
        {
            this._switcherMediaPlayerEventArgs = new SwitcherMediaPlayerEventArgs();
            this.SwitcherMediaPlayerEventClipFrameChanged?.Invoke(this, this._switcherMediaPlayerEventArgs);
        }

        private int _atBeginning;
        private uint _clipFrameIndex;
        private int _loop;
        private int _playing;
        //private MediaPlayerSource _mdiaPlayerSource;

        public int IndexNr
        {
            get
            {
                return this._indexnr;
            }
        }
        public int AtBeginning
        {
            get
            {
                this.MediaPlayer.GetAtBeginning(out this._atBeginning);
                return this._atBeginning;
            }
            set
            {
                this.MediaPlayer.SetAtBeginning();
            }
        }
        public uint ClipFrame
        {
            get
            {
                this.MediaPlayer.GetClipFrame(out this._clipFrameIndex);
                return this._clipFrameIndex;
            }
            set
            {
                this.MediaPlayer.SetClipFrame(value);
            }
        }
        public int Loop
        {
            get
            {
                this.MediaPlayer.GetLoop(out this._loop);
                return this._loop;
            }
            set
            {
                this.MediaPlayer.SetLoop(value);
            }
        }
        public int Playing
        {
            get
            {
                this.MediaPlayer.GetPlaying(out this._playing);
                return this._playing;
            }
            set
            {
                this.MediaPlayer.SetPlaying(value);
            }
        }
        public MediaPlayerSource Source(_BMDSwitcherMediaPlayerSourceType type, uint index)
        {
            _BMDSwitcherMediaPlayerSourceType _type;
            uint _index;
            this.MediaPlayer.GetSource(out _type, out _index);
            return new MediaPlayerSource() { type = _type, index = _index };
        }
        public void Source(MediaPlayerSource mediasource)
        {
            this.MediaPlayer.SetSource(mediasource.type, mediasource.index);
        }
    }
}
