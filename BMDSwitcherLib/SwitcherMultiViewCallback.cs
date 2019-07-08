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
using System.Runtime.InteropServices;

namespace BMDSwitcherLib
{
    public class SwitcherMultiViewEventArgs : EventArgs
    {
        internal int _window;
        public int Window
        {
            get
            {
                return this._window;
            }
        }
    }
    public delegate void SwitcherMultiViewEventHandler(SwitcherMultiViewCallback s, SwitcherMultiViewEventArgs a);

    public class SwitcherMultiViewCallback : IBMDSwitcherMultiViewCallback
    {
        // Events:
        public event SwitcherMultiViewEventHandler SwitcherMultiViewEventTypeCurrentInputSupportsVuMeterChanged;
        public event SwitcherMultiViewEventHandler SwitcherMultiViewEventTypeLayoutChanged;
        public event SwitcherMultiViewEventHandler SwitcherMultiViewEventTypeProgramPreviewSwappedChanged;
        public event SwitcherMultiViewEventHandler SwitcherMultiViewEventTypeSafeAreaEnabledChanged;
        public event SwitcherMultiViewEventHandler SwitcherMultiViewEventTypeVuMeterEnabledChanged;
        public event SwitcherMultiViewEventHandler SwitcherMultiViewEventTypeVuMeterOpacityChanged;
        public event SwitcherMultiViewEventHandler SwitcherMultiViewEventTypeWindowChanged;

        private SwitcherMultiViewEventArgs _switcherMultiViewEventArgs;

        private int _indexnr;
        internal IBMDSwitcherMultiView MultiView;
        internal SwitcherMultiViewCallback(IBMDSwitcherMultiView multiView, int index)
        {
            this._indexnr = index;
            this.MultiView = multiView;
        }

        void IBMDSwitcherMultiViewCallback.Notify(_BMDSwitcherMultiViewEventType eventType, int window)
        {
            this._switcherMultiViewEventArgs = new SwitcherMultiViewEventArgs { _window = window };
            switch (eventType)
            {
                case _BMDSwitcherMultiViewEventType.bmdSwitcherMultiViewEventTypeCurrentInputSupportsVuMeterChanged:
                    SwitcherMultiViewEventTypeCurrentInputSupportsVuMeterChanged?.Invoke(this, this._switcherMultiViewEventArgs);
                    break;
                case _BMDSwitcherMultiViewEventType.bmdSwitcherMultiViewEventTypeLayoutChanged:
                    SwitcherMultiViewEventTypeLayoutChanged?.Invoke(this, this._switcherMultiViewEventArgs);
                    break;
                case _BMDSwitcherMultiViewEventType.bmdSwitcherMultiViewEventTypeProgramPreviewSwappedChanged:
                    SwitcherMultiViewEventTypeProgramPreviewSwappedChanged?.Invoke(this, this._switcherMultiViewEventArgs);
                    break;
                case _BMDSwitcherMultiViewEventType.bmdSwitcherMultiViewEventTypeSafeAreaEnabledChanged:
                    SwitcherMultiViewEventTypeSafeAreaEnabledChanged?.Invoke(this, this._switcherMultiViewEventArgs);
                    break;
                case _BMDSwitcherMultiViewEventType.bmdSwitcherMultiViewEventTypeVuMeterEnabledChanged:
                    SwitcherMultiViewEventTypeVuMeterEnabledChanged?.Invoke(this, this._switcherMultiViewEventArgs);
                    break;
                case _BMDSwitcherMultiViewEventType.bmdSwitcherMultiViewEventTypeVuMeterOpacityChanged:
                    SwitcherMultiViewEventTypeVuMeterOpacityChanged?.Invoke(this, this._switcherMultiViewEventArgs);
                    break;
                case _BMDSwitcherMultiViewEventType.bmdSwitcherMultiViewEventTypeWindowChanged:
                    SwitcherMultiViewEventTypeWindowChanged?.Invoke(this, this._switcherMultiViewEventArgs);
                    break;
            }
        }

        private int _canRoute;
        private int _canToggleSafeAreaEnabled;
        private int _currentInputSupportsVuMeter;
        private _BMDSwitcherInputAvailability _availabilityMask;
        private _BMDSwitcherMultiViewLayout _layout;
        private int _swapped;
        private int _safeAreaEnabled;
        private int _vuMeterEnabled;
        private double _opacity;
        private uint _windowCount;
        private long _input;
        private int _supportsProgramPreviewSwap;
        private int _supportsVuMeters;

        public int IndexNr
        {
            get
            {
                return this._indexnr;
            }
        }
        public int CanRouteInputs
        {
            get
            {
                this.MultiView.CanRouteInputs(out this._canRoute);
                return this._canRoute;
            }
        }
        private int CanToggleSafeAreaEnabled
        {
            get
            {
                this.MultiView.CanToggleSafeAreaEnabled(out this._canToggleSafeAreaEnabled);
                return this._canToggleSafeAreaEnabled;
            }
        }
        public int CurrentInputSupportsVuMeter(uint window)
        {
            this.MultiView.CurrentInputSupportsVuMeter(window, out this._currentInputSupportsVuMeter);
            return this._currentInputSupportsVuMeter;
        }
        public _BMDSwitcherInputAvailability InputAvailabilityMask
        {
            get
            {
                this.MultiView.GetInputAvailabilityMask(out this._availabilityMask);
                return this._availabilityMask;
            }
        }
        public _BMDSwitcherMultiViewLayout Layout
        {
            get
            {
                this.MultiView.GetLayout(out this._layout);
                return this._layout;
            }
            set
            {
                this.MultiView.SetLayout(value);
            }
        }
        public int ProgramPreviewSwapped
        {
            get
            {
                this.MultiView.GetProgramPreviewSwapped(out this._swapped);
                return this._swapped;
            }
            set
            {
                this.MultiView.SetProgramPreviewSwapped(value);
            }
        }
        public int SafeAreaEnabled
        {
            get
            {
                this.MultiView.GetSafeAreaEnabled(out this._safeAreaEnabled);
                return this._safeAreaEnabled;
            }
            set
            {
                this.MultiView.SetSafeAreaEnabled(value);
            }
        }
        public int GetVuMeterEnabled(uint window)
        {
            this.MultiView.GetVuMeterEnabled(window, out this._vuMeterEnabled);
            return this._vuMeterEnabled;
        }
        public void SetVuMeterEnabled(uint window, int enable)
        {
            this.MultiView.SetVuMeterEnabled(window, enable);
        }
        public double VuMeterOpacity
        {
            get
            {
                try
                {
                    this.MultiView.GetVuMeterOpacity(out this._opacity);
                    return this._opacity;
                }
                catch
                {
                   return 0;
                }
            }
            set
            {
                this.MultiView.SetVuMeterOpacity(value);
            }
        }
        public uint WindowCount
        {
            get
            {
                this.MultiView.GetWindowCount(out this._windowCount);
                return this._windowCount;
            }
        }
        public long GetWindowInput(uint window)
        {
            this.MultiView.GetWindowInput(window, out this._input);
            return this._input;
        }
        public void SetWindowInput(uint window, long input)
        {
            this.MultiView.SetWindowInput(window, input);
        }
        public int SupportsProgramPreviewSwap
        {
            get
            {
                this.MultiView.SupportsProgramPreviewSwap(out this._supportsProgramPreviewSwap);
                return this._supportsProgramPreviewSwap;
            }
        }
        public int SupportsVuMeters
        {
            get
            {
                this.MultiView.SupportsVuMeters(out this._supportsVuMeters);
                return this._supportsVuMeters;
            }
        }
    }
}
