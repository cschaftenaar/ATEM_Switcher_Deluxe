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
    public class SwitcherKeyFlyParametersEventArgs : EventArgs
    {
        internal _BMDSwitcherFlyKeyFrame _keyFrame;
        public _BMDSwitcherFlyKeyFrame KeyFrame
        {
            get
            {
                return this._keyFrame;
            }
        }
    }
    public delegate void SwitcherKeyFlyParametersEventHandler(SwitcherKeyFlyParametersCallback s, SwitcherKeyFlyParametersEventArgs a);

    public class SwitcherKeyFlyParametersCallback : IBMDSwitcherKeyFlyParametersCallback
    {
        // Events:
        public event SwitcherKeyFlyParametersEventHandler SwitcherKeyFlyParametersEventTypeCanFlyChanged;
        public event SwitcherKeyFlyParametersEventHandler SwitcherKeyFlyParametersEventTypeFlyChanged;
        public event SwitcherKeyFlyParametersEventHandler SwitcherKeyFlyParametersEventTypeIsAtKeyFramesChanged;
        public event SwitcherKeyFlyParametersEventHandler SwitcherKeyFlyParametersEventTypeIsKeyFrameStoredChanged;
        public event SwitcherKeyFlyParametersEventHandler SwitcherKeyFlyParametersEventTypeIsRunningChanged;
        public event SwitcherKeyFlyParametersEventHandler SwitcherKeyFlyParametersEventTypePositionXChanged;
        public event SwitcherKeyFlyParametersEventHandler SwitcherKeyFlyParametersEventTypePositionYChanged;
        public event SwitcherKeyFlyParametersEventHandler SwitcherKeyFlyParametersEventTypeRateChanged;
        public event SwitcherKeyFlyParametersEventHandler SwitcherKeyFlyParametersEventTypeRotationChanged;
        public event SwitcherKeyFlyParametersEventHandler SwitcherKeyFlyParametersEventTypeSizeXChanged;
        public event SwitcherKeyFlyParametersEventHandler SwitcherKeyFlyParametersEventTypeSizeYChanged;

        private SwitcherKeyFlyParametersEventArgs _switcherKeyFlyParametersEventArgs;

        private int _indexnr;
        internal IBMDSwitcherKeyFlyParameters KeyFlyParameters;
        internal SwitcherKeyFlyParametersCallback(IBMDSwitcherKeyFlyParameters keyFlyParam, int index)
        {
            this._indexnr = index;
            this.KeyFlyParameters = keyFlyParam;
        }

        void IBMDSwitcherKeyFlyParametersCallback.Notify(_BMDSwitcherKeyFlyParametersEventType eventType, _BMDSwitcherFlyKeyFrame keyFrame)
        {
            this._switcherKeyFlyParametersEventArgs = new SwitcherKeyFlyParametersEventArgs { _keyFrame = keyFrame };
            switch (eventType)
            {
                case _BMDSwitcherKeyFlyParametersEventType.bmdSwitcherKeyFlyParametersEventTypeCanFlyChanged:
                    this.SwitcherKeyFlyParametersEventTypeCanFlyChanged?.Invoke(this, this._switcherKeyFlyParametersEventArgs);
                    break;
                case _BMDSwitcherKeyFlyParametersEventType.bmdSwitcherKeyFlyParametersEventTypeFlyChanged:
                    this.SwitcherKeyFlyParametersEventTypeFlyChanged?.Invoke(this, this._switcherKeyFlyParametersEventArgs);
                    break;
                case _BMDSwitcherKeyFlyParametersEventType.bmdSwitcherKeyFlyParametersEventTypeIsAtKeyFramesChanged:
                    this.SwitcherKeyFlyParametersEventTypeIsAtKeyFramesChanged?.Invoke(this, this._switcherKeyFlyParametersEventArgs);
                    break;
                case _BMDSwitcherKeyFlyParametersEventType.bmdSwitcherKeyFlyParametersEventTypeIsKeyFrameStoredChanged:
                    this.SwitcherKeyFlyParametersEventTypeIsKeyFrameStoredChanged?.Invoke(this, this._switcherKeyFlyParametersEventArgs);
                    break;
                case _BMDSwitcherKeyFlyParametersEventType.bmdSwitcherKeyFlyParametersEventTypeIsRunningChanged:
                    this.SwitcherKeyFlyParametersEventTypeIsRunningChanged?.Invoke(this, this._switcherKeyFlyParametersEventArgs);
                    break;
                case _BMDSwitcherKeyFlyParametersEventType.bmdSwitcherKeyFlyParametersEventTypePositionXChanged:
                    this.SwitcherKeyFlyParametersEventTypePositionXChanged?.Invoke(this, this._switcherKeyFlyParametersEventArgs);
                    break;
                case _BMDSwitcherKeyFlyParametersEventType.bmdSwitcherKeyFlyParametersEventTypePositionYChanged:
                    this.SwitcherKeyFlyParametersEventTypePositionYChanged?.Invoke(this, this._switcherKeyFlyParametersEventArgs);
                    break;
                case _BMDSwitcherKeyFlyParametersEventType.bmdSwitcherKeyFlyParametersEventTypeRateChanged:
                    this.SwitcherKeyFlyParametersEventTypeRateChanged?.Invoke(this, this._switcherKeyFlyParametersEventArgs);
                    break;
                case _BMDSwitcherKeyFlyParametersEventType.bmdSwitcherKeyFlyParametersEventTypeRotationChanged:
                    this.SwitcherKeyFlyParametersEventTypeRotationChanged?.Invoke(this, this._switcherKeyFlyParametersEventArgs);
                    break;
                case _BMDSwitcherKeyFlyParametersEventType.bmdSwitcherKeyFlyParametersEventTypeSizeXChanged:
                    this.SwitcherKeyFlyParametersEventTypeSizeXChanged?.Invoke(this, this._switcherKeyFlyParametersEventArgs);
                    break;
                case _BMDSwitcherKeyFlyParametersEventType.bmdSwitcherKeyFlyParametersEventTypeSizeYChanged:
                    this.SwitcherKeyFlyParametersEventTypeSizeYChanged?.Invoke(this, this._switcherKeyFlyParametersEventArgs);
                    break;
            }
        }

        private int _canFly;
        private int _canRotate;
        private int _canScaleUp;
        private int _isFlyKey;
        private IBMDSwitcherKeyFlyKeyFrameParameters _keyFrameParameters;
        private double _offsetX;
        private double _offsetY;
        private uint _frames;
        private double _degrees;
        private double _multiplierX;
        private double _multiplierY;
        private _BMDSwitcherFlyKeyFrame _keyFrames;
        private int _stored;
        private int _isRunning;
        private _BMDSwitcherFlyKeyFrame _destination;

        public int IndexNr
        {
            get
            {
                return this._indexnr;
            }
        }
        public void ClearKeyFrame(_BMDSwitcherFlyKeyFrame _keyFrame)
        {
            this.KeyFlyParameters.ClearKeyFrame(_keyFrame);
        }
        public int CanFly
        {
            get
            {
                this.KeyFlyParameters.GetCanFly(out this._canFly);
                return this._canFly;
            }
        }
        public int CanRotate
        {
            get
            {
                this.KeyFlyParameters.GetCanRotate(out this._canRotate);
                return this._canRotate;
            }
        }
        public int CanScaleUp
        {
            get
            {
                this.KeyFlyParameters.GetCanScaleUp(out this._canScaleUp);
                return this._canScaleUp;
            }
        }
        public int Fly
        {
            get
            {
                this.KeyFlyParameters.GetFly(out this._isFlyKey);
                return this._isFlyKey;
            }
            set
            {
                this.KeyFlyParameters.SetFly(value);
            }
        }
        public IBMDSwitcherKeyFlyKeyFrameParameters KeyFrameParameters(_BMDSwitcherFlyKeyFrame keyFrame)
        {
            this.KeyFlyParameters.GetKeyFrameParameters(keyFrame, out this._keyFrameParameters);
            return this._keyFrameParameters;
        }
        public double PositionX
        {
            get
            {
                this.KeyFlyParameters.GetPositionX(out this._offsetX);
                return this._offsetX;
            }
            set
            {
                this.KeyFlyParameters.SetPositionX(value);
            }
        }
        public double PositionY
        {
            get
            {
                this.KeyFlyParameters.GetPositionY(out this._offsetY);
                return this._offsetY;
            }
            set
            {
                this.KeyFlyParameters.SetPositionY(value);
            }
        }
        public uint Rate
        {
            get
            {
                this.KeyFlyParameters.GetRate(out this._frames);
                return this._frames;
            }
            set
            {
                this.KeyFlyParameters.SetRate(value);
            }
        }
        public double Rotation
        {
            get
            {
                this.KeyFlyParameters.GetRotation(out this._degrees);
                return this._degrees;
            }
            set
            {
                this.KeyFlyParameters.SetRotation(value);
            }
        }
        public double SizeX
        {
            get
            {
                this.KeyFlyParameters.GetSizeX(out this._multiplierX);
                return this._multiplierX;
            }
            set
            {
                this.KeyFlyParameters.SetSizeX(value);
            }
        }
        public double SizeY
        {
            get
            {
                this.KeyFlyParameters.GetSizeY(out this._multiplierY);
                return this._multiplierY;
            }
            set
            {
                this.KeyFlyParameters.SetSizeY(value);
            }
        }
        public _BMDSwitcherFlyKeyFrame IsAtKeyFrames
        {
            get
            {
                this.KeyFlyParameters.IsAtKeyFrames(out this._keyFrames);
                return this._keyFrames;
            }
        }
        public int IsKeyFrameStored(_BMDSwitcherFlyKeyFrame keyFrame)
        {
            this.KeyFlyParameters.IsKeyFrameStored(keyFrame, out this._stored);
            return this._stored;
        }
        public int IsRunning  // Bug in API ????  void IsRunning(out int IsRunning, out _BMDSwitcherFlyKeyFrame destination)
        {
            get
            {
                this.KeyFlyParameters.IsRunning(out this._isRunning, out this._destination);
                return this._isRunning;
            }
        }
        public _BMDSwitcherFlyKeyFrame Destination  // Bug in API ????  void IsRunning(out int IsRunning, out _BMDSwitcherFlyKeyFrame destination)
        {
            get
            {
                this.KeyFlyParameters.IsRunning(out this._isRunning, out this._destination);
                return this._destination;
            }
        }
        public void ResetDVE()
        {
            this.KeyFlyParameters.ResetDVE();
        }
        public void ResetDVEFull()
        {
            this.KeyFlyParameters.ResetDVEFull();
        }
        public void ResetRotation()
        {
            this.KeyFlyParameters.ResetRotation();
        }
        public _BMDSwitcherFlyKeyFrame RunToKeyFrame
        {
            set
            {
                this.KeyFlyParameters.RunToKeyFrame(value);
            }
        }
        public _BMDSwitcherFlyKeyFrame StoreAsKeyFrame
        {
            set
            {
                this.KeyFlyParameters.StoreAsKeyFrame(value);
            }
        }
    }
}
