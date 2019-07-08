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
    public class SwitcherInputEventArgs : EventArgs
    {
    }
    public delegate void SwitcherInputEventHandler(SwitcherInputCallback s, SwitcherInputEventArgs a);

    public class SwitcherInputCallback : IBMDSwitcherInputCallback
    {
        // Events:
        public event SwitcherInputEventHandler SwitcherInputEventTypeAreNamesDefaultChanged;
        public event SwitcherInputEventHandler SwitcherInputEventTypeAvailableExternalPortTypesChanged;
        public event SwitcherInputEventHandler SwitcherInputEventTypeCurrentExternalPortTypeChanged;
        public event SwitcherInputEventHandler SwitcherInputEventTypeIsPreviewTalliedChanged;
        public event SwitcherInputEventHandler SwitcherInputEventTypeIsProgramTalliedChanged;
        public event SwitcherInputEventHandler SwitcherInputEventTypeLongNameChanged;
        public event SwitcherInputEventHandler SwitcherInputEventTypeShortNameChanged;

        private SwitcherInputEventArgs _switcherInputEventArgs;

        private int _indexnr;
        internal IBMDSwitcherInput Input;
        internal SwitcherInputCallback(IBMDSwitcherInput input, int index)
        {
            this._indexnr = index;
            this.Input = input;
        }

        void IBMDSwitcherInputCallback.Notify(_BMDSwitcherInputEventType eventType)
        {
            this._switcherInputEventArgs = new SwitcherInputEventArgs();
            switch (eventType)
            {
                case _BMDSwitcherInputEventType.bmdSwitcherInputEventTypeAreNamesDefaultChanged:
                    this.SwitcherInputEventTypeAreNamesDefaultChanged?.Invoke(this, this._switcherInputEventArgs);
                    break;
                case _BMDSwitcherInputEventType.bmdSwitcherInputEventTypeAvailableExternalPortTypesChanged:
                    this.SwitcherInputEventTypeAvailableExternalPortTypesChanged?.Invoke(this, this._switcherInputEventArgs);
                    break;
                case _BMDSwitcherInputEventType.bmdSwitcherInputEventTypeCurrentExternalPortTypeChanged:
                    this.SwitcherInputEventTypeCurrentExternalPortTypeChanged?.Invoke(this, this._switcherInputEventArgs);
                    break;
                case _BMDSwitcherInputEventType.bmdSwitcherInputEventTypeIsPreviewTalliedChanged:
                    this.SwitcherInputEventTypeIsPreviewTalliedChanged?.Invoke(this, this._switcherInputEventArgs);
                    break;
                case _BMDSwitcherInputEventType.bmdSwitcherInputEventTypeIsProgramTalliedChanged:
                    this.SwitcherInputEventTypeIsProgramTalliedChanged?.Invoke(this, this._switcherInputEventArgs);
                    break;
                case _BMDSwitcherInputEventType.bmdSwitcherInputEventTypeLongNameChanged:
                    this.SwitcherInputEventTypeLongNameChanged?.Invoke(this, this._switcherInputEventArgs);
                    break;
                case _BMDSwitcherInputEventType.bmdSwitcherInputEventTypeShortNameChanged:
                    this.SwitcherInputEventTypeShortNameChanged?.Invoke(this, this._switcherInputEventArgs);
                    break;
            }
        }

        private int _isDefault;
        private _BMDSwitcherExternalPortType _externalPortTypes;
        private _BMDSwitcherExternalPortType _CurrentExternalPort;
        private _BMDSwitcherInputAvailability _availability;
        private long _inputId;
        private string _longName;
        private string _shortName;
        private _BMDSwitcherPortType _portType;
        private int _isPreviewTallied;
        private int _isProgramTallied;

        public int IndexNr
        {
            get
            {
                return this._indexnr;
            }
        }
        public int AreNamesDefault
        {
            get
            {
                this.Input.AreNamesDefault(ref this._isDefault);
                return this._isDefault;
            }
            set
            {
                this.Input.AreNamesDefault(value);
            }
        }
        public _BMDSwitcherExternalPortType AvailableExternalPortTypes
        {
            get
            {
                this.Input.GetAvailableExternalPortTypes(out this._externalPortTypes);
                return this._externalPortTypes;
            }
        }
        public _BMDSwitcherExternalPortType CurrentExternalPort
        {
            get
            {
                this.Input.GetCurrentExternalPortType(out _CurrentExternalPort);
                return this._CurrentExternalPort;
            }
            set
            {
                this.Input.SetCurrentExternalPortType(value);
            }
        }
        public _BMDSwitcherInputAvailability InputAvailability
        {
            get
            {
                this.Input.GetInputAvailability(out this._availability);
                return this._availability;
            }
        }
        public long InputId
        {
            get
            {
                this.Input.GetInputId(out this._inputId);
                return this._inputId;
            }
        }
        public string LongName
        {
            get
            {
                this.Input.GetLongName(out this._longName);
                return this._longName;
            }
            set
            {
                this.Input.SetLongName(value);
            }
        }
        public _BMDSwitcherPortType PortType
        {
            get
            {
                this.Input.GetPortType(out this._portType);
                return this._portType;
            }
        }
        public string ShortName
        {
            get
            {
                this.Input.GetShortName(out this._shortName);
                return this._shortName;
            }
            set
            {
                this.Input.SetShortName(value);
            }
        }
        public int IsPreviewTallied
        {
            get
            {
                this.Input.IsPreviewTallied(out this._isPreviewTallied);
                return this._isPreviewTallied;
            }
        }
        public int IsProgramTallied
        {
            get
            {
                this.Input.IsProgramTallied(out this._isProgramTallied);
                return this._isProgramTallied;
            }
        }
        public void ResetNames()
        {
            this.Input.ResetNames();
        }
    }
}
