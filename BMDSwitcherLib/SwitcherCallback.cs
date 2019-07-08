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
    public class SwitcherEventArgs : EventArgs
    {
        internal _BMDSwitcherVideoMode _coreVideoMode;
        public _BMDSwitcherVideoMode CoreVideoMode
        {
            get
            {
                return this._coreVideoMode;
            }
        }
    }
    public delegate void SwitcherEventHandler(SwitcherCallback s, SwitcherEventArgs a);

    public class SwitcherCallback : IBMDSwitcherCallback
    {
        // Events:
        public event SwitcherEventHandler Switcher3GSDIOutputLevelChanged;
        public event SwitcherEventHandler SwitcherDisconnected;
        public event SwitcherEventHandler SwitcherDownConvertedHDVideoModeChanged;
        public event SwitcherEventHandler SwitcherMethodForDownConvertedSDChanged;
        public event SwitcherEventHandler SwitcherMultiViewVideoModeChanged;
        public event SwitcherEventHandler SwitcherPowerStatusChanged;
        public event SwitcherEventHandler SwitcherVideoModeChanged;

        private SwitcherEventArgs _switcherEventArgs;

        internal IBMDSwitcher Switcher;
        internal SwitcherCallback(IBMDSwitcher sw)
        {
            this.Switcher = sw;
        }

        void IBMDSwitcherCallback.Notify(_BMDSwitcherEventType eventType, _BMDSwitcherVideoMode coreVideoMode)
        {
            this._switcherEventArgs = new SwitcherEventArgs();
            switch (eventType)
            {
                case _BMDSwitcherEventType.bmdSwitcherEventType3GSDIOutputLevelChanged:
                    this.Switcher3GSDIOutputLevelChanged?.Invoke(this, this._switcherEventArgs);
                    break;
                case _BMDSwitcherEventType.bmdSwitcherEventTypeDisconnected:
                    this._switcherEventArgs = new SwitcherEventArgs { _coreVideoMode = coreVideoMode };
                    this.SwitcherDisconnected?.Invoke(this, this._switcherEventArgs);
                    break;
                case _BMDSwitcherEventType.bmdSwitcherEventTypeDownConvertedHDVideoModeChanged:
                    this.SwitcherDownConvertedHDVideoModeChanged?.Invoke(this, this._switcherEventArgs);
                    break;
                case _BMDSwitcherEventType.bmdSwitcherEventTypeMethodForDownConvertedSDChanged:
                    this.SwitcherMethodForDownConvertedSDChanged?.Invoke(this, this._switcherEventArgs);
                    break;
                case _BMDSwitcherEventType.bmdSwitcherEventTypeMultiViewVideoModeChanged:
                    this.SwitcherMultiViewVideoModeChanged?.Invoke(this, this._switcherEventArgs);
                    break;
                case _BMDSwitcherEventType.bmdSwitcherEventTypePowerStatusChanged:
                    this.SwitcherPowerStatusChanged?.Invoke(this, this._switcherEventArgs);
                    break;
                case _BMDSwitcherEventType.bmdSwitcherEventTypeVideoModeChanged:
                    this.SwitcherVideoModeChanged?.Invoke(this, this._switcherEventArgs);
                    break;
            }
        }

        private int _supported1;
        private int _supported2;
        private int _supported3;
        private _BMDSwitcher3GSDIOutputLevel _outputLevel;
        private _BMDSwitcherVideoMode _downConvertedHDVideoMode;
        private _BMDSwitcherDownConversionMethod _methode;
        private _BMDSwitcherVideoMode _multiviewVideoMode;
        private _BMDSwitcherPowerStatus _powerStatus;
        private string _productName;
        private _BMDSwitcherVideoMode _videoMode;

        public int DoesSupportDownConvertedHDVideoMode(_BMDSwitcherVideoMode coreVideoMode, _BMDSwitcherVideoMode downConvertedHDVideoMode)
        {
            this.Switcher.DoesSupportDownConvertedHDVideoMode(coreVideoMode, downConvertedHDVideoMode, out this._supported1);
            return this._supported1;
        }
        public int DoesSupportMultiViewVideoMode(_BMDSwitcherVideoMode coreVideoMode, _BMDSwitcherVideoMode multiviewVideoMode)
        {
            this.Switcher.DoesSupportMultiViewVideoMode(coreVideoMode, multiviewVideoMode, out this._supported2);
            return this._supported2;
        }
        public int DoesSupportVideoMode(_BMDSwitcherVideoMode videoMode)
        {
            this.Switcher.DoesSupportVideoMode(videoMode, out this._supported3);
            return this._supported3;
        }
        public _BMDSwitcher3GSDIOutputLevel OutputLevel3GSDI
        {
            get
            {
                this.Switcher.Get3GSDIOutputLevel(out this._outputLevel);
                return this._outputLevel;
            }
            set
            {
                this.Switcher.Set3GSDIOutputLevel(value);
            }
        }
        public _BMDSwitcherVideoMode DownConvertedHDVideoMode(_BMDSwitcherVideoMode coreVideoMode)
        {
            this.Switcher.GetDownConvertedHDVideoMode(coreVideoMode, out this._downConvertedHDVideoMode);
            return this._downConvertedHDVideoMode;
        }
        public void DownConvertedHDVideoMode(_BMDSwitcherVideoMode coreVideoMode, _BMDSwitcherVideoMode downConvertedHDVideoMode)
        {
            this.Switcher.SetDownConvertedHDVideoMode(coreVideoMode, downConvertedHDVideoMode);
        }
        public _BMDSwitcherDownConversionMethod MethodForDownConvertedSD
        {
            get
            {
                this.Switcher.GetMethodForDownConvertedSD(out this._methode);
                return this._methode;
            }
            set
            {
                this.Switcher.SetMethodForDownConvertedSD(value);
            }
        }
        public _BMDSwitcherVideoMode GetMultiViewVideoMode(_BMDSwitcherVideoMode coreVideMode)
        {
            this.Switcher.GetMultiViewVideoMode(coreVideMode, out _multiviewVideoMode);
            return this._multiviewVideoMode;
        }
        public void SetMultiViewVideoMode(_BMDSwitcherVideoMode coreVideoMode, _BMDSwitcherVideoMode multiviewVideoMode)
        {
            this.Switcher.SetMultiViewVideoMode(coreVideoMode, multiviewVideoMode);
        }
        public _BMDSwitcherPowerStatus PowerStatus
        {
            get
            {
                this.Switcher.GetPowerStatus(out this._powerStatus);
                return this._powerStatus;
            }
        }
        public string ProductName
        {
            get
            {
                this.Switcher.GetProductName(out this._productName);
                return this._productName;
            }
        }
        public _BMDSwitcherVideoMode VideoMode
        {
            get
            {
                this.Switcher.GetVideoMode(out this._videoMode);
                return this._videoMode;
            }
            set
            {
                this.Switcher.SetVideoMode(value);
            }
        }
    }
}
