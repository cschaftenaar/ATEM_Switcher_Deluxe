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
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;

namespace BMDSwitcherLib
{
	public class Switcher
	{
		private IBMDSwitcher m_switcher;
		private IBMDSwitcherAudioInput m_switcherAudioInput;
		private IBMDSwitcherAudioMixer m_switcherAudioMixer;
        private IBMDSwitcherAudioMonitorOutput m_switcherAudioMonitorOutput;
		private IBMDSwitcherClip m_switcherClip;
		private IBMDSwitcherDiscovery m_switcherDiscovery;
		private IBMDSwitcherDownstreamKey m_switcherDownstreamKey;
		private IBMDSwitcherInput m_switcherInput;
		private IBMDSwitcherInputAux m_switcherInputAux;
		private IBMDSwitcherInputColor m_switcherInputColor;
		private IBMDSwitcherInputSuperSource m_switcherInputSuperSource;
		private IBMDSwitcherKey m_switcherKey;
		private IBMDSwitcherKeyChromaParameters m_switcherKeyChromaParameters;
		private IBMDSwitcherKeyDVEParameters m_switcherKeyDVEParameters;
		private IBMDSwitcherKeyFlyKeyFrameParameters m_switcherKeyFlyKeyFrameParameters;
		private IBMDSwitcherKeyFlyParameters m_switcherKeyFlyParameters;
		private IBMDSwitcherKeyLumaParameters m_switcherKeyLumaParameters;
		private IBMDSwitcherKeyPatternParameters m_switcherKeyPatternParameters;
		private IBMDSwitcherMacroPool m_switcherMacroPool;
		private IBMDSwitcherMediaPlayer m_switcherMediaPlayer;
		private IBMDSwitcherMediaPool m_switcherMediaPool;
		private IBMDSwitcherMixEffectBlock m_switcherMixEffectBlock; 
		private IBMDSwitcherMixMinusOutput m_switcherMixMinusOutput;
		private IBMDSwitcherMultiView m_switcherMultiView;
		private IBMDSwitcherStills m_switcherStills;
		private IBMDSwitcherTransitionDipParameters m_switcherTransitionDipParameters;
		private IBMDSwitcherTransitionDVEParameters m_switcherTransitionDVEParameters;
		private IBMDSwitcherTransitionMixParameters m_switcherTransitionMixParameters;
		private IBMDSwitcherTransitionParameters m_switcherTransitionParameters;
		private IBMDSwitcherTransitionStingerParameters m_switcherTransitionStingerParameters;
		private IBMDSwitcherTransitionWipeParameters m_switcherTransitionWipeParameters;
		//private IBMDSwitcherFrame m_switcherFrame;
		//private IBMDSwitcherAudio m_switcherAudio;
		//private IBMDSwitcherAudioHeadphoneOutput m_switcherAudioHeadphoneOutput;
        //private IBMDSwitcherCameraControl m_switcherCameraControl;
		//private IBMDSwitcherHyperDeck m_switcherHyperDeck;
		//private IBMDSwitcherHyperDeckClip m_switcherHyperDeckClip;
		//private IBMDSwitcherMacro m_switcherMacro;
		//private IBMDSwitcherMacroControl m_switcherMacroControl;
		//private IBMDSwitcherSerialPort m_switcherSerialPort;
		//private IBMDSwitcherSuperSourceBox m_switcherSuperSourceBox;
		//private IBMDSwitcherTalkback m_switcherTalkback;
		//private IBMDSwitcherTransferMacro m_switcherTransferMacro;

        public SwitcherInfo BMDSwitcherInfo;
		public SwitcherCallback BMDSwitcher;
		public SwitcherAudioMixerCallback BMDSwitcherAudioMixer;
        public SwitcherMixMinusOutputCallback BMDSwitcherMixMinusOutput;
        public SwitcherTransitionDipParametersCallback BMDSwitcherTransitionDipParameters;
		public SwitcherTransitionDVEParametersCallback BMDSwitcherTransitionDVEParameters;
		public SwitcherTransitionMixParametersCallback BMDSwitcherTransitionMixParameters;
		public SwitcherTransitionParametersCallback BMDSwitcherTransitionParameters;
		public SwitcherTransitionStingerParametersCallback BMDSwitcherTransitionStingerParameters;
		public SwitcherTransitionWipeParametersCallback BMDSwitcherTransitionWipeParameters;
        public SwitcherMediaPoolCallback BMDSwitcherMediaPool;
        public SwitcherMacroPoolCallback BMDSwitcherMacroPool;
        public List<SwitcherMacro> BMDSwitcherMacro = new List<SwitcherMacro>();
		public List<SwitcherAudioInputCallback> BMDSwitcherAudioInput = new List<SwitcherAudioInputCallback>();
		public List<SwitcherAudioMonitorOutputCallback> BMDSwitcherAudioMonitorOutput = new List<SwitcherAudioMonitorOutputCallback>();
		public List<SwitcherClipCallback> BMDSwitcherClip = new List<SwitcherClipCallback>();
		public List<SwitcherDownstreamKeyCallback> BMDSwitcherDownstreamKey = new List<SwitcherDownstreamKeyCallback>();
		public List<SwitcherInputAuxCallback> BMDSwitcherInputAux = new List<SwitcherInputAuxCallback>();
		public List<SwitcherInputCallback> BMDSwitcherInput = new List<SwitcherInputCallback>();
		public List<SwitcherKeyCallback> BMDSwitcherKey = new List<SwitcherKeyCallback>();
		public List<SwitcherMixEffectBlockCallback> BMDSwitcherMixEffectBlock = new List<SwitcherMixEffectBlockCallback>();
        public List<SwitcherMediaPlayerCallback> BMDSwitcherMediaPlayer = new List<SwitcherMediaPlayerCallback>();
        public SwitcherStillsCallback BMDSwitcherStills;
        public List<SwitcherStill> BMDSwitcherStill = new List<SwitcherStill>();
        public List<SwitcherMultiViewCallback> BMDSwitcherMultiView = new List<SwitcherMultiViewCallback>();
        public List<SwitcherInputColorCallback> BMDSwitcherInputColor = new List<SwitcherInputColorCallback>();
        public List<SwitcherInputSuperSourceCallback> BMDSwitcherInputSuperSource = new List<SwitcherInputSuperSourceCallback>();
        public List<SwitcherKeyChromaParametersCallback> BMDSwitcherKeyChromaParameters = new List<SwitcherKeyChromaParametersCallback>();
        public List<SwitcherKeyLumaParametersCallback> BMDSwitcherKeyLumaParameters = new List<SwitcherKeyLumaParametersCallback>();
        public List<SwitcherKeyPatternParametersCallback> BMDSwitcherKeyPatternParameters = new List<SwitcherKeyPatternParametersCallback>();
        public List<SwitcherKeyDVEParametersCallback> BMDSwitcherKeyDVEParameters = new List<SwitcherKeyDVEParametersCallback>();
        public List<SwitcherKeyFlyParametersCallback> BMDSwitcherKeyFlyParameters = new List<SwitcherKeyFlyParametersCallback>();
        public List<SwitcherKeyFlyKeyFrameParametersCallback> BMDSwitcherKeyFlyKeyFrameParameters = new List<SwitcherKeyFlyKeyFrameParametersCallback>();

		private string BMDDeviceAddress;
        public bool IsConnected { get; private set; }

        public Switcher(string deviceAddress)
		{
			this.BMDDeviceAddress = deviceAddress;
		}

		public void Discover()
		{
            this.m_switcherDiscovery = new CBMDSwitcherDiscovery();
            _BMDSwitcherConnectToFailure failReason = 0;
			try
			{
				this.m_switcherDiscovery.ConnectTo(this.BMDDeviceAddress, out this.m_switcher, out failReason);
				this.IsConnected = true;
			}
			catch (COMException exception1)
			{
                switch(failReason)
                {
                    case _BMDSwitcherConnectToFailure.bmdSwitcherConnectToFailureIncompatibleFirmware:
                            throw new BMDSwitcherLibExeption("Incompatible firmware");
                    case _BMDSwitcherConnectToFailure.bmdSwitcherConnectToFailureNoResponse:
                            throw new BMDSwitcherLibExeption(string.Format("No response from {0}", this.BMDDeviceAddress));
                    default:
                        throw new BMDSwitcherLibExeption(string.Format("Unknown Error: {0}", exception1.Message));
                }
			}
			catch (Exception exception2)
			{
				throw new BMDSwitcherLibExeption(string.Format("Unable to connect to switcher: {0}", exception2.Message));
			}
		}
        public void Connect()
        {
            this.BMDSwitcherInfo = new SwitcherInfo(this);

            #region Switcher
            this.BMDSwitcher = new SwitcherCallback(this.m_switcher);
            this.m_switcher.AddCallback(this.BMDSwitcher);
            #endregion
                        
            #region SwitcherMediaPool
            this.m_switcherMediaPool = (IBMDSwitcherMediaPool)this.m_switcher;
            this.BMDSwitcherMediaPool = new SwitcherMediaPoolCallback(this.m_switcherMediaPool);
            this.m_switcherMediaPool.AddCallback(this.BMDSwitcherMediaPool);
            #endregion

            #region SwitcherStills
            this.m_switcherMediaPool.GetStills(out this.m_switcherStills);
            this.BMDSwitcherStills = new SwitcherStillsCallback(this.m_switcherStills);
            this.m_switcherStills.GetCount(out uint totalStills);
            for (this.BMDSwitcherInfo.TotalSwitcherStills = 0; this.BMDSwitcherInfo.TotalSwitcherStills < totalStills; this.BMDSwitcherInfo.TotalSwitcherStills++)
            {
                this.BMDSwitcherStill.Add(new SwitcherStill(this.m_switcherStills, (int)this.BMDSwitcherInfo.TotalSwitcherStills));
            }
            #endregion
            
            #region SwitcherClips
            this.m_switcherMediaPool.GetClipCount(out uint totalClips);
            for (this.BMDSwitcherInfo.TotalSwitcherClip = 0; this.BMDSwitcherInfo.TotalSwitcherClip < totalClips; this.BMDSwitcherInfo.TotalSwitcherClip++)
            {
                this.m_switcherMediaPool.GetClip(this.BMDSwitcherInfo.TotalSwitcherClip, out this.m_switcherClip);
                this.BMDSwitcherClip.Add(new SwitcherClipCallback(this.m_switcherClip, (int)this.BMDSwitcherInfo.TotalSwitcherClip));
            }
            #endregion

            #region SwitcherMacroPool
            this.m_switcherMacroPool = (IBMDSwitcherMacroPool)this.m_switcher;
            this.BMDSwitcherMacroPool = new SwitcherMacroPoolCallback(this.m_switcherMacroPool);
            this.m_switcherMacroPool.AddCallback(this.BMDSwitcherMacroPool);
            this.m_switcherMacroPool.GetMaxCount(out uint totalMacros);
            for(this.BMDSwitcherInfo.TotalSwitcherMacros=0; this.BMDSwitcherInfo.TotalSwitcherMacros<totalMacros; this.BMDSwitcherInfo.TotalSwitcherMacros++)
            {
                this.BMDSwitcherMacro.Add(new SwitcherMacro(this.m_switcherMacroPool, (int)this.BMDSwitcherInfo.TotalSwitcherMacros));
            }
            #endregion

            #region SwitcherAudioMixer
            this.m_switcherAudioMixer = (IBMDSwitcherAudioMixer)this.m_switcher;
            this.BMDSwitcherAudioMixer = new SwitcherAudioMixerCallback(this.m_switcherAudioMixer);
            this.m_switcherAudioMixer.AddCallback(this.BMDSwitcherAudioMixer);
            #endregion

            #region SwitcherMixMinusOutput
            try
            {
                this.m_switcherMixMinusOutput = (IBMDSwitcherMixMinusOutput)this.m_switcher;
                this.BMDSwitcherMixMinusOutput = new SwitcherMixMinusOutputCallback(this.m_switcherMixMinusOutput);
                this.m_switcherMixMinusOutput.AddCallback(this.BMDSwitcherMixMinusOutput);
            }
            catch
            {
                // Not Supported bij switcher
            }
            #endregion

            #region SwitcherInput
            IBMDSwitcherInputIterator SwitcherInputIterator = null;
            Guid SwitcherInputIteratorIID = typeof(IBMDSwitcherInputIterator).GUID;
            this.m_switcher.CreateIterator(ref SwitcherInputIteratorIID, out IntPtr SwitcherInputIteratorintPtr);
            SwitcherInputIterator = (IBMDSwitcherInputIterator)Marshal.GetObjectForIUnknown(SwitcherInputIteratorintPtr);

            if (SwitcherInputIterator != null)
            {
                SwitcherInputIterator.Next(out this.m_switcherInput);
                while (this.m_switcherInput != null)
                {
                    SwitcherInputCallback switcherInputCallback = new SwitcherInputCallback(this.m_switcherInput, this.BMDSwitcherInfo.TotalSwitcherInput);
                    this.m_switcherInput.AddCallback(switcherInputCallback);
                    this.BMDSwitcherInput.Add(switcherInputCallback);

                    switch (switcherInputCallback.PortType)
                    {
                        #region bmdSwitcherPortTypeBlack
                        case _BMDSwitcherPortType.bmdSwitcherPortTypeBlack:
                            this.BMDSwitcherInfo.TotalSwitcherPortTypeBlack++;
                            break;
                        #endregion

                        #region SwitcherPortTypeColorBars
                        case _BMDSwitcherPortType.bmdSwitcherPortTypeColorBars:
                            this.BMDSwitcherInfo.TotalSwitcherPortTypeColorBars++;
                            break;
                        #endregion

                        #region SwitcherPortTypeExternal
                        case _BMDSwitcherPortType.bmdSwitcherPortTypeExternal:
                            this.BMDSwitcherInfo.TotalSwitcherPortTypeExternal++;
                            break;
                        #endregion

                        #region SwitcherPortTypeKeyCutOutput
                        case _BMDSwitcherPortType.bmdSwitcherPortTypeKeyCutOutput:
                            this.BMDSwitcherInfo.TotalSwitcherPortTypeKeyCutOutput++;
                            break;
                        #endregion

                        #region SwitcherPortTypeMediaPlayerCut
                        case _BMDSwitcherPortType.bmdSwitcherPortTypeMediaPlayerCut:
                            this.BMDSwitcherInfo.TotalSwitcherPortTypeMediaPlayerCut++;
                            break;
                        #endregion

                        #region SwitcherPortTypeMediaPlayerFill
                        case _BMDSwitcherPortType.bmdSwitcherPortTypeMediaPlayerFill:
                            this.BMDSwitcherInfo.TotalSwitcherPortTypeMediaPlayerFill++;
                            break;
                        #endregion

                        #region SwitcherPortTypeMixEffectBlockOutput
                        case _BMDSwitcherPortType.bmdSwitcherPortTypeMixEffectBlockOutput:
                            this.BMDSwitcherInfo.TotalSwitcherPortTypeMixEffectBlockOutput++;
                            break;
                        #endregion

                        #region SwitcherPortTypeAuxOutput
                        case _BMDSwitcherPortType.bmdSwitcherPortTypeAuxOutput:
                            this.m_switcherInputAux = (IBMDSwitcherInputAux)this.m_switcherInput;
                            SwitcherInputAuxCallback switcherInputAuxCallback = new SwitcherInputAuxCallback(this.m_switcherInputAux, this.BMDSwitcherInfo.TotalSwitcherPortTypeAuxOutput);
                            this.BMDSwitcherInputAux.Add(switcherInputAuxCallback);
                            this.BMDSwitcherInfo.TotalSwitcherPortTypeAuxOutput++;
                            break;
                        #endregion

                        #region SwitcherPortTypeColorGenerator
                        case _BMDSwitcherPortType.bmdSwitcherPortTypeColorGenerator:
                            this.m_switcherInputColor = (IBMDSwitcherInputColor)this.m_switcherInput;
                            SwitcherInputColorCallback switcherInputColorCallback = new SwitcherInputColorCallback(this.m_switcherInputColor, this.BMDSwitcherInfo.TotalSwitcherPortTypeColorGenerator);
                            this.BMDSwitcherInputColor.Add(switcherInputColorCallback);
                            this.BMDSwitcherInfo.TotalSwitcherPortTypeColorGenerator++;
                            break;
                        #endregion

                        #region SwitcherPortTypeSuperSource
                        case _BMDSwitcherPortType.bmdSwitcherPortTypeSuperSource:
                            this.m_switcherInputSuperSource = (IBMDSwitcherInputSuperSource)this.m_switcherInput;
                            SwitcherInputSuperSourceCallback switcherInputSuperSourceCallback = new SwitcherInputSuperSourceCallback(this.m_switcherInputSuperSource, this.BMDSwitcherInfo.TotalSwitcherPortTypeSuperSource);
                            this.BMDSwitcherInputSuperSource.Add(switcherInputSuperSourceCallback);
                            this.BMDSwitcherInfo.TotalSwitcherPortTypeSuperSource++;
                            break;
                       #endregion
                    }
                    SwitcherInputIterator.Next(out this.m_switcherInput);
                    this.BMDSwitcherInfo.TotalSwitcherInput++;
                }
            }
            #endregion
            
            #region SwitcherMixEffectBlock
            IBMDSwitcherMixEffectBlockIterator SwitcherMixEffectBlockIterator = null;
            Guid SwitcherMixEffectBlockIteratorIID = typeof(IBMDSwitcherMixEffectBlockIterator).GUID;

            IBMDSwitcherKeyIterator SwitcherKeyIterator = null;
            Guid SwitcherKeyIteratorIID = typeof(IBMDSwitcherKeyIterator).GUID;

            this.m_switcher.CreateIterator(ref SwitcherMixEffectBlockIteratorIID, out IntPtr SwitcherMixEffectBlockIteratorintPtr);
            SwitcherMixEffectBlockIterator = (IBMDSwitcherMixEffectBlockIterator)Marshal.GetObjectForIUnknown(SwitcherMixEffectBlockIteratorintPtr);

            if (SwitcherMixEffectBlockIterator != null)
            {
                SwitcherMixEffectBlockIterator.Next(out this.m_switcherMixEffectBlock);

                while (this.m_switcherMixEffectBlock != null)
                {
                    #region SwitcherTransitionParameters
                    this.m_switcherTransitionParameters = (IBMDSwitcherTransitionParameters)this.m_switcherMixEffectBlock;
                    this.BMDSwitcherTransitionParameters = new SwitcherTransitionParametersCallback(this.m_switcherTransitionParameters);
                    this.m_switcherTransitionParameters.AddCallback(this.BMDSwitcherTransitionParameters);
                    #endregion

                    #region SwitcherTransitionDipParameters
                    this.m_switcherTransitionDipParameters = (IBMDSwitcherTransitionDipParameters)this.m_switcherMixEffectBlock;
                    this.BMDSwitcherTransitionDipParameters = new SwitcherTransitionDipParametersCallback(this.m_switcherTransitionDipParameters);
                    this.m_switcherTransitionDipParameters.AddCallback(this.BMDSwitcherTransitionDipParameters);
                    #endregion

                    #region SwitcherTransitionDVEParameters
                    try
                    {
                        this.m_switcherTransitionDVEParameters = (IBMDSwitcherTransitionDVEParameters)this.m_switcherMixEffectBlock;
                        this.BMDSwitcherTransitionDVEParameters = new SwitcherTransitionDVEParametersCallback(this.m_switcherTransitionDVEParameters);
                        this.m_switcherTransitionDVEParameters.AddCallback(this.BMDSwitcherTransitionDVEParameters);
                    }
                    catch { }
                    #endregion

                    #region SwitcherTransitionMixParameters
                    this.m_switcherTransitionMixParameters = (IBMDSwitcherTransitionMixParameters)this.m_switcherMixEffectBlock;
                    this.BMDSwitcherTransitionMixParameters = new SwitcherTransitionMixParametersCallback(this.m_switcherTransitionMixParameters);
                    this.m_switcherTransitionMixParameters.AddCallback(this.BMDSwitcherTransitionMixParameters);
                    #endregion

                    #region SwitcherTransitionStingerParameters
                    try
                    {
                        this.m_switcherTransitionStingerParameters = (IBMDSwitcherTransitionStingerParameters)this.m_switcherMixEffectBlock;
                        this.BMDSwitcherTransitionStingerParameters = new SwitcherTransitionStingerParametersCallback(this.m_switcherTransitionStingerParameters);
                        this.m_switcherTransitionStingerParameters.AddCallback(this.BMDSwitcherTransitionStingerParameters);
                    }
                    catch { }
                    #endregion

                    #region SwitcherTransitionWipeParameters
                    this.m_switcherTransitionWipeParameters = (IBMDSwitcherTransitionWipeParameters)this.m_switcherMixEffectBlock;
                    this.BMDSwitcherTransitionWipeParameters = new SwitcherTransitionWipeParametersCallback(this.m_switcherTransitionWipeParameters);
                    this.m_switcherTransitionWipeParameters.AddCallback(this.BMDSwitcherTransitionWipeParameters);
                    #endregion

                    SwitcherMixEffectBlockCallback switcherMixEffectBlockCallback = new SwitcherMixEffectBlockCallback(this.m_switcherMixEffectBlock, this.BMDSwitcherInfo.TotalSwitcherMixEffectBlock);
                    this.m_switcherMixEffectBlock.AddCallback(switcherMixEffectBlockCallback);
                    this.BMDSwitcherMixEffectBlock.Add(switcherMixEffectBlockCallback);

                    this.m_switcherMixEffectBlock.CreateIterator(ref SwitcherKeyIteratorIID, out IntPtr SwitcherKeyIteratorintPtr);
                    SwitcherKeyIterator = (IBMDSwitcherKeyIterator)Marshal.GetObjectForIUnknown(SwitcherKeyIteratorintPtr);
                    if (SwitcherKeyIterator != null)
                    {
                        SwitcherKeyIterator.Next(out this.m_switcherKey);

                        while (this.m_switcherKey != null)
                        {
                            #region SwitcherKey
                            SwitcherKeyCallback switcherKeyCallback = new SwitcherKeyCallback(this.m_switcherKey, this.BMDSwitcherInfo.TotalSwitcherKey);
                            this.m_switcherKey.AddCallback(switcherKeyCallback);
                            this.BMDSwitcherKey.Add(switcherKeyCallback);
                            #endregion

                            #region SwitcherKeyChromaParameters
                            this.m_switcherKeyChromaParameters = (IBMDSwitcherKeyChromaParameters)this.m_switcherKey;
                            SwitcherKeyChromaParametersCallback switcherKeyChromaParametersCallback = new SwitcherKeyChromaParametersCallback(this.m_switcherKeyChromaParameters, this.BMDSwitcherInfo.TotalSwitcherKey);
                            this.m_switcherKeyChromaParameters.AddCallback(switcherKeyChromaParametersCallback);
                            this.BMDSwitcherKeyChromaParameters.Add(switcherKeyChromaParametersCallback);
                            #endregion

                            #region SwitcherKeyLumaParameters
                            this.m_switcherKeyLumaParameters = (IBMDSwitcherKeyLumaParameters)this.m_switcherKey;
                            SwitcherKeyLumaParametersCallback switcherKeyLumaParametersCallback = new SwitcherKeyLumaParametersCallback(this.m_switcherKeyLumaParameters, this.BMDSwitcherInfo.TotalSwitcherKey);
                            this.m_switcherKeyLumaParameters.AddCallback(switcherKeyLumaParametersCallback);
                            this.BMDSwitcherKeyLumaParameters.Add(switcherKeyLumaParametersCallback);
                            #endregion

                            #region SwitcherKeyPatternParameters
                            this.m_switcherKeyPatternParameters = (IBMDSwitcherKeyPatternParameters)this.m_switcherKey;
                            SwitcherKeyPatternParametersCallback switcherKeyPatternParametersCallback = new SwitcherKeyPatternParametersCallback(this.m_switcherKeyPatternParameters, this.BMDSwitcherInfo.TotalSwitcherKey);
                            this.m_switcherKeyPatternParameters.AddCallback(switcherKeyPatternParametersCallback);
                            this.BMDSwitcherKeyPatternParameters.Add(switcherKeyPatternParametersCallback);
                            #endregion

                            #region SwitcherKeyDVEParameters
                            try
                            {
                                this.m_switcherKeyDVEParameters = (IBMDSwitcherKeyDVEParameters)this.m_switcherKey;
                                SwitcherKeyDVEParametersCallback switcherKeyDVEParametersCallback = new SwitcherKeyDVEParametersCallback(this.m_switcherKeyDVEParameters, this.BMDSwitcherInfo.TotalSwitcherKey);
                                this.m_switcherKeyDVEParameters.AddCallback(switcherKeyDVEParametersCallback);
                                this.BMDSwitcherKeyDVEParameters.Add(switcherKeyDVEParametersCallback);
                            }
                            catch { }
                            #endregion

                            #region SwitcherKeyFlyParameters
                                this.m_switcherKeyFlyParameters = (IBMDSwitcherKeyFlyParameters)this.m_switcherKey;
                                SwitcherKeyFlyParametersCallback switcherKeyFlyParametersCallback = new SwitcherKeyFlyParametersCallback(this.m_switcherKeyFlyParameters, this.BMDSwitcherInfo.TotalSwitcherKey);
                                this.m_switcherKeyFlyParameters.AddCallback(switcherKeyFlyParametersCallback);
                                this.BMDSwitcherKeyFlyParameters.Add(switcherKeyFlyParametersCallback);
                            #endregion

                            #region SwitcherKeyFlyKeyFrameParameters (Not sure if i handle it the right way.
                            this.m_switcherKeyFlyKeyFrameParameters = (IBMDSwitcherKeyFlyKeyFrameParameters)switcherKeyFlyParametersCallback.KeyFrameParameters(_BMDSwitcherFlyKeyFrame.bmdSwitcherFlyKeyFrameA);
                                SwitcherKeyFlyKeyFrameParametersCallback switcherKeyFlyKeyFrameParametersCallback = new SwitcherKeyFlyKeyFrameParametersCallback(this.m_switcherKeyFlyKeyFrameParameters, this.BMDSwitcherInfo.TotalSwitcherKey);
                                this.m_switcherKeyFlyKeyFrameParameters.AddCallback(switcherKeyFlyKeyFrameParametersCallback);
                                this.BMDSwitcherKeyFlyKeyFrameParameters.Add(switcherKeyFlyKeyFrameParametersCallback);
                            #endregion

                            SwitcherKeyIterator.Next(out this.m_switcherKey);
                            this.BMDSwitcherInfo.TotalSwitcherKey++;
                        }
                    }
                    SwitcherMixEffectBlockIterator.Next(out this.m_switcherMixEffectBlock);
                    this.BMDSwitcherInfo.TotalSwitcherMixEffectBlock++;
                }
            }
            #endregion

            #region SwitcherDownstreamKey
            IBMDSwitcherDownstreamKeyIterator SwitcherDownstreamKeyIterator = null;
            Guid SwitcherDownstreamKeyIteratorIID = typeof(IBMDSwitcherDownstreamKeyIterator).GUID;
            this.m_switcher.CreateIterator(ref SwitcherDownstreamKeyIteratorIID, out IntPtr SwitcherDownstreamKeyIteratorintPtr);
            SwitcherDownstreamKeyIterator = (IBMDSwitcherDownstreamKeyIterator)Marshal.GetObjectForIUnknown(SwitcherDownstreamKeyIteratorintPtr);

            if (SwitcherDownstreamKeyIterator != null)
            {
                SwitcherDownstreamKeyIterator.Next(out this.m_switcherDownstreamKey);
                while (this.m_switcherDownstreamKey != null)
                {
                    SwitcherDownstreamKeyCallback switcherDownstreamKeyCallback = new SwitcherDownstreamKeyCallback(this.m_switcherDownstreamKey, this.BMDSwitcherInfo.TotalSwitcherDownstreamKey);
                    this.m_switcherDownstreamKey.AddCallback(switcherDownstreamKeyCallback);
                    this.BMDSwitcherDownstreamKey.Add(switcherDownstreamKeyCallback);
                    SwitcherDownstreamKeyIterator.Next(out this.m_switcherDownstreamKey);
                    this.BMDSwitcherInfo.TotalSwitcherDownstreamKey++;
                }
            }
            #endregion

            #region SwitcherAudioInput
            IBMDSwitcherAudioInputIterator SwitcherAudioInputIterator = null;
            Guid SwitcherAudioInputIteratorIID = typeof(IBMDSwitcherAudioInputIterator).GUID;
            this.m_switcherAudioMixer.CreateIterator(ref SwitcherAudioInputIteratorIID, out IntPtr SwitcherAudioInputIteratorintPtr);
            SwitcherAudioInputIterator = (IBMDSwitcherAudioInputIterator)Marshal.GetObjectForIUnknown(SwitcherAudioInputIteratorintPtr);

            if (SwitcherAudioInputIterator != null)
            {
                SwitcherAudioInputIterator.Next(out this.m_switcherAudioInput);
                while (this.m_switcherAudioInput != null)
                {
                    SwitcherAudioInputCallback switcherAudioInputCallback = new SwitcherAudioInputCallback(this.m_switcherAudioInput, this.BMDSwitcherInfo.TotalSwitcherAudioInput);
                    this.m_switcherAudioInput.AddCallback(switcherAudioInputCallback);
                    this.BMDSwitcherAudioInput.Add(switcherAudioInputCallback);
                    SwitcherAudioInputIterator.Next(out this.m_switcherAudioInput);
                    this.BMDSwitcherInfo.TotalSwitcherAudioInput++;
                }
            }
            #endregion

            #region SwitcherAudioMonitorOutput
            IBMDSwitcherAudioMonitorOutputIterator SwitcherAudioMonitorOutputIterator = null;
            Guid SwitcherAudioMonitorOutputIteratorIID = typeof(IBMDSwitcherAudioMonitorOutputIterator).GUID;
            this.m_switcherAudioMixer.CreateIterator(ref SwitcherAudioMonitorOutputIteratorIID, out IntPtr SwitcherAudioMonitorOutputIteratorintPtr);
            SwitcherAudioMonitorOutputIterator = (IBMDSwitcherAudioMonitorOutputIterator)Marshal.GetObjectForIUnknown(SwitcherAudioMonitorOutputIteratorintPtr);

            if (SwitcherAudioMonitorOutputIterator != null)
            {
                SwitcherAudioMonitorOutputIterator.Next(out this.m_switcherAudioMonitorOutput);
                while (this.m_switcherAudioMonitorOutput != null)
                {
                    SwitcherAudioMonitorOutputCallback switcherAudioMonitorOutputCallback = new SwitcherAudioMonitorOutputCallback(this.m_switcherAudioMonitorOutput, this.BMDSwitcherInfo.TotalSwitcherAudioMonitorOutput);
                    this.m_switcherAudioMonitorOutput.AddCallback(switcherAudioMonitorOutputCallback);
                    this.BMDSwitcherAudioMonitorOutput.Add(switcherAudioMonitorOutputCallback);
                    SwitcherAudioMonitorOutputIterator.Next(out this.m_switcherAudioMonitorOutput);
                    this.BMDSwitcherInfo.TotalSwitcherAudioMonitorOutput++;
                }
            }
            #endregion

            #region SwitcherMediaPlayer
            IBMDSwitcherMediaPlayerIterator SwitcherMediaPlayerIterator = null;
            Guid SwitcherMediaPlayerIteratorIID = typeof(IBMDSwitcherMediaPlayerIterator).GUID;
            this.m_switcher.CreateIterator(ref SwitcherMediaPlayerIteratorIID, out IntPtr SwitcherMediaPlayerIteratorintPtr);
            SwitcherMediaPlayerIterator = (IBMDSwitcherMediaPlayerIterator)Marshal.GetObjectForIUnknown(SwitcherMediaPlayerIteratorintPtr);

            if (SwitcherMediaPlayerIterator != null)
            {
                SwitcherMediaPlayerIterator.Next(out m_switcherMediaPlayer);
                while (this.m_switcherMediaPlayer != null)
                {
                    SwitcherMediaPlayerCallback switcherMediaPlayerCallback = new SwitcherMediaPlayerCallback(this.m_switcherMediaPlayer, this.BMDSwitcherInfo.TotalSwitcherMediaPlayer);
                    this.m_switcherMediaPlayer.AddCallback(switcherMediaPlayerCallback);
                    this.BMDSwitcherMediaPlayer.Add(switcherMediaPlayerCallback);
                    SwitcherMediaPlayerIterator.Next(out m_switcherMediaPlayer);
                    this.BMDSwitcherInfo.TotalSwitcherMediaPlayer++;
                }
            }
            #endregion

            #region SwitcherMultiView
            IBMDSwitcherMultiViewIterator SwitcherMultiViewIterator = null;
            Guid SwitcherMultiViewIteratorIID = typeof(IBMDSwitcherMultiViewIterator).GUID;
            this.m_switcher.CreateIterator(ref SwitcherMultiViewIteratorIID, out IntPtr SwitcherMultiViewIteratorintPtr);
            SwitcherMultiViewIterator = (IBMDSwitcherMultiViewIterator)Marshal.GetObjectForIUnknown(SwitcherMultiViewIteratorintPtr);

            if (SwitcherMultiViewIterator != null)
            {
                SwitcherMultiViewIterator.Next(out m_switcherMultiView);
                while (this.m_switcherMultiView != null)
                {
                    SwitcherMultiViewCallback switcherMultiViewCallback = new SwitcherMultiViewCallback(this.m_switcherMultiView, this.BMDSwitcherInfo.TotalSwitcherMultiView);
                    this.m_switcherMultiView.AddCallback(switcherMultiViewCallback);
                    this.BMDSwitcherMultiView.Add(switcherMultiViewCallback);
                    SwitcherMultiViewIterator.Next(out m_switcherMultiView);
                    this.BMDSwitcherInfo.TotalSwitcherMultiView++;
                }
            }
            #endregion
        }
        public void Disconnect()
		{

        }
        public void Command(string command)
        {
            string[] lines;
            string[] args;

            lines = command.Split(new char[] { ';' });

            foreach (string l in lines)
            {
                args = l.Split(new char[] { '|' });

                switch (args[0].Trim().ToLower())
                {

                    #region Wait | <miliseconds>
                    case "wait":
                        Thread.Sleep(Int32.Parse(args[1]));
                        break;
                    #endregion

                    #region InputAux | <nr 1-.. > | <source>
                    case "inputaux":
                        this.BMDSwitcherInputAux[Int32.Parse(args[1]) - 1].InputSource = Int32.Parse(args[2]);
                        break;
                    #endregion

                    #region Perform | <Cut> or <autotransition> or <fadetoblack>
                    case "perform":
                        switch (args[1].ToLower().Trim())
                        {
                            case "cut":
                                this.BMDSwitcherMixEffectBlock[0].PerformCut();
                                break;
                            case "autotransition":
                                this.BMDSwitcherMixEffectBlock[0].PerformAutoTransition();
                                break;
                            case "fadetoblack":
                                this.BMDSwitcherMixEffectBlock[0].PerformFadeToBlack();
                                break;
                        }
                        break;
                    #endregion

                    #region ProgramInput | <nr>
                    case "programinput":
                        this.BMDSwitcherMixEffectBlock[0].SetInt(BMDSwitcherAPI._BMDSwitcherMixEffectBlockPropertyId.bmdSwitcherMixEffectBlockPropertyIdProgramInput, Int32.Parse(args[1]));
                        break;
                    #endregion

                    #region PreviewInput | <nr>
                    case "previewinput":
                        this.BMDSwitcherMixEffectBlock[0].SetInt(BMDSwitcherAPI._BMDSwitcherMixEffectBlockPropertyId.bmdSwitcherMixEffectBlockPropertyIdPreviewInput, Int32.Parse(args[1]));
                        break;
                    #endregion

                    #region NextTransitionStyle | <Mix> or <Dip> or <Wipe> or <Stinger> or <DVE>
                    case "nexttransitionstyle":
                        switch (args[1].ToLower().Trim())
                        {
                            case "mix":
                                this.BMDSwitcherTransitionParameters.NextTransitionStyle = BMDSwitcherAPI._BMDSwitcherTransitionStyle.bmdSwitcherTransitionStyleMix;
                                break;
                            case "dip":
                                this.BMDSwitcherTransitionParameters.NextTransitionStyle = BMDSwitcherAPI._BMDSwitcherTransitionStyle.bmdSwitcherTransitionStyleDip;
                                break;
                            case "wipe":
                                this.BMDSwitcherTransitionParameters.NextTransitionStyle = BMDSwitcherAPI._BMDSwitcherTransitionStyle.bmdSwitcherTransitionStyleWipe;
                                break;
                            case "stinger":
                                this.BMDSwitcherTransitionParameters.NextTransitionStyle = BMDSwitcherAPI._BMDSwitcherTransitionStyle.bmdSwitcherTransitionStyleStinger;
                                break;
                            case "dve":
                                this.BMDSwitcherTransitionParameters.NextTransitionStyle = BMDSwitcherAPI._BMDSwitcherTransitionStyle.bmdSwitcherTransitionStyleDVE;
                                break;
                        }
                        break;
                    #endregion

                    #region KeyOnAir | <nr>
                    case "keyonair":
                        this.BMDSwitcherKey[Int32.Parse(args[1]) - 1].OnAir = this.BMDSwitcherKey[Int32.Parse(args[1]) - 1].OnAir == 0 ? 1 : 0;
                        break;
                    #endregion
                    #region KeyOnAirOn | <nr>
                    case "keyonairon":
                        this.BMDSwitcherKey[Int32.Parse(args[1]) - 1].OnAir = 1;
                        break;
                    #endregion
                    #region KeyOnAirOff | <nr>
                    case "keyonairoff":
                        this.BMDSwitcherKey[Int32.Parse(args[1]) - 1].OnAir = 0;
                        break;
                    #endregion

                    #region NextTransitionSelection | <BKGD> or <1> or <2> or <3> or <4>
                    case "nexttransitionselection":
                        switch (args[1].ToLower().Trim())
                        {
                            case "bkgd":
                                this.BMDSwitcherTransitionParameters.NextTransitionSelection = this.BMDSwitcherTransitionParameters.NextTransitionSelection ^ BMDSwitcherAPI._BMDSwitcherTransitionSelection.bmdSwitcherTransitionSelectionBackground;
                                break;
                            case "1":
                                this.BMDSwitcherTransitionParameters.NextTransitionSelection = this.BMDSwitcherTransitionParameters.NextTransitionSelection ^ BMDSwitcherAPI._BMDSwitcherTransitionSelection.bmdSwitcherTransitionSelectionKey1;
                                break;
                            case "2":
                                this.BMDSwitcherTransitionParameters.NextTransitionSelection = this.BMDSwitcherTransitionParameters.NextTransitionSelection ^ BMDSwitcherAPI._BMDSwitcherTransitionSelection.bmdSwitcherTransitionSelectionKey2;
                                break;
                            case "3":
                                this.BMDSwitcherTransitionParameters.NextTransitionSelection = this.BMDSwitcherTransitionParameters.NextTransitionSelection ^ BMDSwitcherAPI._BMDSwitcherTransitionSelection.bmdSwitcherTransitionSelectionKey3;
                                break;
                            case "4":
                                this.BMDSwitcherTransitionParameters.NextTransitionSelection = this.BMDSwitcherTransitionParameters.NextTransitionSelection ^ BMDSwitcherAPI._BMDSwitcherTransitionSelection.bmdSwitcherTransitionSelectionKey4;
                                break;
                            default:
                                break;
                        }
                        break;
                    #endregion
                    #region NextTransitionSelectionOn | <BKGD> or <1> or <2> or <3> or <4>
                    case "nexttransitionselectionon":
                        switch (args[1].ToLower().Trim())
                        {
                            case "bkgd":
                                this.BMDSwitcherTransitionParameters.NextTransitionSelection = this.BMDSwitcherTransitionParameters.NextTransitionSelection | BMDSwitcherAPI._BMDSwitcherTransitionSelection.bmdSwitcherTransitionSelectionBackground;
                                break;
                            case "1":
                                this.BMDSwitcherTransitionParameters.NextTransitionSelection = this.BMDSwitcherTransitionParameters.NextTransitionSelection | BMDSwitcherAPI._BMDSwitcherTransitionSelection.bmdSwitcherTransitionSelectionKey1;
                                break;
                            case "2":
                                this.BMDSwitcherTransitionParameters.NextTransitionSelection = this.BMDSwitcherTransitionParameters.NextTransitionSelection | BMDSwitcherAPI._BMDSwitcherTransitionSelection.bmdSwitcherTransitionSelectionKey2;
                                break;
                            case "3":
                                this.BMDSwitcherTransitionParameters.NextTransitionSelection = this.BMDSwitcherTransitionParameters.NextTransitionSelection | BMDSwitcherAPI._BMDSwitcherTransitionSelection.bmdSwitcherTransitionSelectionKey3;
                                break;
                            case "4":
                                this.BMDSwitcherTransitionParameters.NextTransitionSelection = this.BMDSwitcherTransitionParameters.NextTransitionSelection | BMDSwitcherAPI._BMDSwitcherTransitionSelection.bmdSwitcherTransitionSelectionKey4;
                                break;
                            default:
                                break;
                        }
                        break;
                    #endregion
                    #region NextTransitionSelectionOff | <BKGD> or <1> or <2> or <3> or <4>
                    case "nexttransitionselectionoff":
                        switch (args[1].ToLower().Trim())
                        {
                            case "bkgd":
                                this.BMDSwitcherTransitionParameters.NextTransitionSelection = this.BMDSwitcherTransitionParameters.NextTransitionSelection & (0x20 - BMDSwitcherAPI._BMDSwitcherTransitionSelection.bmdSwitcherTransitionSelectionBackground);
                                break;
                            case "1":
                                this.BMDSwitcherTransitionParameters.NextTransitionSelection = this.BMDSwitcherTransitionParameters.NextTransitionSelection & (0x20 - BMDSwitcherAPI._BMDSwitcherTransitionSelection.bmdSwitcherTransitionSelectionKey1);
                                break;
                            case "2":
                                this.BMDSwitcherTransitionParameters.NextTransitionSelection = this.BMDSwitcherTransitionParameters.NextTransitionSelection & (0x20 - BMDSwitcherAPI._BMDSwitcherTransitionSelection.bmdSwitcherTransitionSelectionKey2);
                                break;
                            case "3":
                                this.BMDSwitcherTransitionParameters.NextTransitionSelection = this.BMDSwitcherTransitionParameters.NextTransitionSelection & (0x20 - BMDSwitcherAPI._BMDSwitcherTransitionSelection.bmdSwitcherTransitionSelectionKey3);
                                break;
                            case "4":
                                this.BMDSwitcherTransitionParameters.NextTransitionSelection = this.BMDSwitcherTransitionParameters.NextTransitionSelection & (0x20 - BMDSwitcherAPI._BMDSwitcherTransitionSelection.bmdSwitcherTransitionSelectionKey4);
                                break;
                            default:
                                break;
                        }
                        break;
                    #endregion

                    #region PreviewTransition
                    case "previewtransition":
                        this.BMDSwitcherMixEffectBlock[0].SetFlag(BMDSwitcherAPI._BMDSwitcherMixEffectBlockPropertyId.bmdSwitcherMixEffectBlockPropertyIdPreviewTransition, this.BMDSwitcherMixEffectBlock[0].GetFlag(BMDSwitcherAPI._BMDSwitcherMixEffectBlockPropertyId.bmdSwitcherMixEffectBlockPropertyIdPreviewTransition) == 0 ? 1 : 0);
                        break;
                    #endregion
                    #region PreviewTransitionOn
                    case "previewtransitionon":
                        this.BMDSwitcherMixEffectBlock[0].SetFlag(BMDSwitcherAPI._BMDSwitcherMixEffectBlockPropertyId.bmdSwitcherMixEffectBlockPropertyIdPreviewTransition, 1);
                        break;
                    #endregion
                    #region PreviewTransitionOff
                    case "previewtransitionoff":
                        this.BMDSwitcherMixEffectBlock[0].SetFlag(BMDSwitcherAPI._BMDSwitcherMixEffectBlockPropertyId.bmdSwitcherMixEffectBlockPropertyIdPreviewTransition, 0);
                        break;
                    #endregion

                    #region DownstreamKey | <nr> | <Tie> or <OnAir> or <PerformAutoTransition>
                    case "downstreamkey":
                        switch (args[2].ToLower().Trim())
                        {
                            case "tie":
                                this.BMDSwitcherDownstreamKey[Int32.Parse(args[1]) - 1].Tie = this.BMDSwitcherDownstreamKey[Int32.Parse(args[1]) - 1].Tie == 0 ? 1 : 0;
                                break;
                            case "onair":
                                this.BMDSwitcherDownstreamKey[Int32.Parse(args[1]) - 1].OnAir = this.BMDSwitcherDownstreamKey[Int32.Parse(args[1]) - 1].OnAir == 0 ? 1 : 0;
                                break;
                            case "performautotransition":
                                this.BMDSwitcherDownstreamKey[Int32.Parse(args[1]) - 1].PerformAutoTransition();
                                break;
                        }
                        break;
                    #endregion
                    #region DownstreamKeyOn | <nr> | <Tie> or <OnAir> or <PerformAutoTransition>
                    case "downstreamkeyon":
                        switch (args[2].ToLower().Trim())
                        {
                            case "tie":
                                this.BMDSwitcherDownstreamKey[Int32.Parse(args[1]) - 1].Tie = 1;
                                break;
                            case "onair":
                                this.BMDSwitcherDownstreamKey[Int32.Parse(args[1]) - 1].OnAir = 1;
                                break;
                        }
                        break;
                    #endregion
                    #region DownstreamKeyOff | <nr> | <Tie> or <OnAir> or <PerformAutoTransition>
                    case "downstreamkeyoff":
                        switch (args[2].ToLower().Trim())
                        {
                            case "tie":
                                this.BMDSwitcherDownstreamKey[Int32.Parse(args[1]) - 1].Tie = 0;
                                break;
                            case "onair":
                                this.BMDSwitcherDownstreamKey[Int32.Parse(args[1]) - 1].OnAir = 0;
                                break;
                        }
                        break;
                    #endregion

                    default:
                        break;
                }
            }
        }
    }
}