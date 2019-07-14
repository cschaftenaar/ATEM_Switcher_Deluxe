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
    public class SwitcherMultiViewEventArgs_v7_5_2 : EventArgs
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
    public delegate void SwitcherMultiViewEventHandler_v7_5_2(SwitcherMultiViewCallback s, SwitcherMultiViewEventArgs_v7_5_2 a);

    public class SwitcherMultiViewCallback : IBMDSwitcherMultiViewCallback
    {
        // Events:
        public event SwitcherMultiViewEventHandler_v7_5_2 SwitcherMultiViewEventTypeCurrentInputSupportsSafeAreaChanged;
        public event SwitcherMultiViewEventHandler_v7_5_2 SwitcherMultiViewEventTypeCurrentInputSupportsVuMeterChanged;
        public event SwitcherMultiViewEventHandler_v7_5_2 SwitcherMultiViewEventTypeLayoutChanged;
        public event SwitcherMultiViewEventHandler_v7_5_2 SwitcherMultiViewEventTypeProgramPreviewSwappedChanged;
        public event SwitcherMultiViewEventHandler_v7_5_2 SwitcherMultiViewEventTypeSafeAreaEnabledChanged;
        public event SwitcherMultiViewEventHandler_v7_5_2 SwitcherMultiViewEventTypeVuMeterEnabledChanged;
        public event SwitcherMultiViewEventHandler_v7_5_2 SwitcherMultiViewEventTypeVuMeterOpacityChanged;
        public event SwitcherMultiViewEventHandler_v7_5_2 SwitcherMultiViewEventTypeWindowChanged;

        private SwitcherMultiViewEventArgs_v7_5_2 _switcherMultiViewEventArgs_v7_5_2;

        private int _indexnr;
        internal IBMDSwitcherMultiView_v7_5_2 MultiView;
        internal SwitcherMultiViewCallback(IBMDSwitcherMultiView_v7_5_2 multiView, int index)
        {
            this._indexnr = index;
            this.MultiView = multiView;
        }

        void IBMDSwitcherMultiViewCallback.Notify(_BMDSwitcherMultiViewEventType eventType, int window)
        {
            this._switcherMultiViewEventArgs_v7_5_2 = new SwitcherMultiViewEventArgs_v7_5_2 { _window = window };
            switch (eventType)
            {
                case _BMDSwitcherMultiViewEventType.bmdSwitcherMultiViewEventTypeCurrentInputSupportsSafeAreaChanged:
                    SwitcherMultiViewEventTypeCurrentInputSupportsSafeAreaChanged?.Invoke(this, this._switcherMultiViewEventArgs_v7_5_2);
                    break;
                case _BMDSwitcherMultiViewEventType.bmdSwitcherMultiViewEventTypeCurrentInputSupportsVuMeterChanged:
                    SwitcherMultiViewEventTypeCurrentInputSupportsVuMeterChanged?.Invoke(this, this._switcherMultiViewEventArgs_v7_5_2);
                    break;
                case _BMDSwitcherMultiViewEventType.bmdSwitcherMultiViewEventTypeLayoutChanged:
                    SwitcherMultiViewEventTypeLayoutChanged?.Invoke(this, this._switcherMultiViewEventArgs_v7_5_2);
                    break;
                case _BMDSwitcherMultiViewEventType.bmdSwitcherMultiViewEventTypeProgramPreviewSwappedChanged:
                    SwitcherMultiViewEventTypeProgramPreviewSwappedChanged?.Invoke(this, this._switcherMultiViewEventArgs_v7_5_2);
                    break;
                case _BMDSwitcherMultiViewEventType.bmdSwitcherMultiViewEventTypeSafeAreaEnabledChanged:
                    SwitcherMultiViewEventTypeSafeAreaEnabledChanged?.Invoke(this, this._switcherMultiViewEventArgs_v7_5_2);
                    break;
                case _BMDSwitcherMultiViewEventType.bmdSwitcherMultiViewEventTypeVuMeterEnabledChanged:
                    SwitcherMultiViewEventTypeVuMeterEnabledChanged?.Invoke(this, this._switcherMultiViewEventArgs_v7_5_2);
                    break;
                case _BMDSwitcherMultiViewEventType.bmdSwitcherMultiViewEventTypeVuMeterOpacityChanged:
                    SwitcherMultiViewEventTypeVuMeterOpacityChanged?.Invoke(this, this._switcherMultiViewEventArgs_v7_5_2);
                    break;
                case _BMDSwitcherMultiViewEventType.bmdSwitcherMultiViewEventTypeWindowChanged:
                    SwitcherMultiViewEventTypeWindowChanged?.Invoke(this, this._switcherMultiViewEventArgs_v7_5_2);
                    break;
            }
        }

        private int _canRoute;
        private int _canToggleSafeAreaEnabled;
        private int _currentInputSupportsVuMeter;
        private _BMDSwitcherInputAvailability _availabilityMask;
        private _BMDSwitcherMultiViewLayout_v7_5_2 _layout;
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
        public _BMDSwitcherMultiViewLayout_v7_5_2 Layout
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
