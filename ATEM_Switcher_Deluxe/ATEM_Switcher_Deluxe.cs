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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Sanford.Multimedia.Midi;
using PIEHid64Net;
using BMDSwitcherLib;
using BMDMidiLib;
using BMDXKeysLib;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Net.Sockets;
using BMDSwitcherAPI;
using System.Threading;

namespace ATEM_Switcher_Deluxe
{
    public partial class ATEM_Switcher_Form : Form
    {
        public int ActivePreset = 1;
        public bool existMIDI;
        public bool existXKEYS;

        public Switcher sw;
        public XKeys_Controller XKeys;

        public BCF2000_MIDI_Controller midi_BCF2000;
        public iCON_MIDI_iPad_Controller midi_iCON_iPAD;

        //ChannelMessageEventArgs BCF2000_Messages;
               
        protected override void OnLoad(EventArgs e)
        {
            this.Invoke((Action)(() =>
            {
                #region X-Keys
                XKeys = new XKeys_Controller(ref CboDevices);
                existXKEYS = XKeys.GetDevices();

                if (existXKEYS)
                {
                    XKeys.Connect();
                    XKeys.OnButton += new XKeys_EventHandler((s, a) => this.Invoke((Action)(() => OnXKeysButton(s, a))));
                    XKeys.OnSwitch += new XKeys_EventHandler((s, a) => this.Invoke((Action)(() => OnXKeysSwitch(s, a))));
                }
                else
                {
                    XKeys = null;
                    MessageBox.Show("Error : No Devices Found!", "X-Keys ERROR", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }

                #endregion
                
                #region MIDI iCON
                midi_iCON_iPAD = new iCON_MIDI_iPad_Controller(this);
                midi_iCON_iPAD.OnButton += new MIDI_iCON_iPad_EventHandler((s, a) => this.Invoke((Action)(() => OnButton(s, a))));
                midi_iCON_iPAD.OnFaderF1 += new MIDI_iCON_iPad_EventHandler((s, a) => this.Invoke((Action)(() => OnFaderF1(s, a))));
                midi_iCON_iPAD.OnTouchpad += new MIDI_iCON_iPad_EventHandler((s, a) => this.Invoke((Action)(() => OnTouchpad(s, a))));

                midi_iCON_iPAD.Connect();
                midi_iCON_iPAD.StartRecording();
                #endregion
                #region MIDI BCF2000
                midi_BCF2000 = new BCF2000_MIDI_Controller(this);
                existMIDI = midi_BCF2000.GetDevices();

                if (existMIDI)
                {
                    midi_BCF2000.OnFader1 += new MIDI_BCF2000_EventHandler((s, a) => this.Invoke((Action)(() => OnFader1(s, a))));
                    midi_BCF2000.OnFader2 += new MIDI_BCF2000_EventHandler((s, a) => this.Invoke((Action)(() => OnFader2(s, a))));
                    midi_BCF2000.OnFader3 += new MIDI_BCF2000_EventHandler((s, a) => this.Invoke((Action)(() => OnFader3(s, a))));
                    midi_BCF2000.OnFader4 += new MIDI_BCF2000_EventHandler((s, a) => this.Invoke((Action)(() => OnFader4(s, a))));
                    midi_BCF2000.OnFader5 += new MIDI_BCF2000_EventHandler((s, a) => this.Invoke((Action)(() => OnFader5(s, a))));
                    midi_BCF2000.OnFader6 += new MIDI_BCF2000_EventHandler((s, a) => this.Invoke((Action)(() => OnFader6(s, a))));
                    midi_BCF2000.OnFader7 += new MIDI_BCF2000_EventHandler((s, a) => this.Invoke((Action)(() => OnFader7(s, a))));
                    midi_BCF2000.OnFader8 += new MIDI_BCF2000_EventHandler((s, a) => this.Invoke((Action)(() => OnFader8(s, a))));

                    midi_BCF2000.OnEncoder1 += new MIDI_BCF2000_EventHandler((s, a) => this.Invoke((Action)(() => OnEncoder1(s, a))));
                    midi_BCF2000.OnEncoder2 += new MIDI_BCF2000_EventHandler((s, a) => this.Invoke((Action)(() => OnEncoder2(s, a))));
                    midi_BCF2000.OnEncoder3 += new MIDI_BCF2000_EventHandler((s, a) => this.Invoke((Action)(() => OnEncoder3(s, a))));
                    midi_BCF2000.OnEncoder4 += new MIDI_BCF2000_EventHandler((s, a) => this.Invoke((Action)(() => OnEncoder4(s, a))));
                    midi_BCF2000.OnEncoder5 += new MIDI_BCF2000_EventHandler((s, a) => this.Invoke((Action)(() => OnEncoder5(s, a))));
                    midi_BCF2000.OnEncoder6 += new MIDI_BCF2000_EventHandler((s, a) => this.Invoke((Action)(() => OnEncoder6(s, a))));
                    midi_BCF2000.OnEncoder7 += new MIDI_BCF2000_EventHandler((s, a) => this.Invoke((Action)(() => OnEncoder7(s, a))));
                    midi_BCF2000.OnEncoder8 += new MIDI_BCF2000_EventHandler((s, a) => this.Invoke((Action)(() => OnEncoder8(s, a))));

                    midi_BCF2000.OnUpperRowKeys += new MIDI_BCF2000_EventHandler((s, a) => this.Invoke((Action)(() => OnUpperRowKeys(s, a))));
                    midi_BCF2000.OnLowerRowKeys += new MIDI_BCF2000_EventHandler((s, a) => this.Invoke((Action)(() => OnLowerRowKeys(s, a))));
                    midi_BCF2000.OnPresetDecr += new MIDI_BCF2000_EventHandler((s, a) => this.Invoke((Action)(() => GroupBoxPreset1_Enter(s, a))));
                    midi_BCF2000.OnPresetIncr += new MIDI_BCF2000_EventHandler((s, a) => this.Invoke((Action)(() => GroupBoxPreset2_Enter(s, a))));

                    midi_BCF2000.OnUserKey1 += new MIDI_BCF2000_EventHandler((s, a) => this.Invoke((Action)(() => OnUserKey1(s, a))));

                    midi_BCF2000.Connect();
                    midi_BCF2000.StartRecording();

                    // Reset Buttons
                    for (int i = 1; i <= 64; i++)
                    {
                        midi_BCF2000.MIDI_Send(ChannelCommand.NoteOn, 0, 0, i, 0);
                    }
                }
                else
                {
                    midi_BCF2000 = null;
                    MessageBox.Show("No MIDI input devices available.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                #endregion

                #region BMD Switcher
                BMDSwitcherLib.DeviceForm dev = new BMDSwitcherLib.DeviceForm()
                {
                    DeviceAddress = "192.168.1.200"
                };
                dev.ShowDialog();
                sw = new Switcher(dev.DeviceAddress);
                sw.Discover();
                sw.Connect();

                for (int i = 0; i < sw.BMDSwitcherAudioInput.Count; i++)
                {
                    sw.BMDSwitcherAudioInput[i].SwitcherAudioInputEventTypeGainChanged += new SwitcherAudioInputEventHandler((s, a) => this.Invoke((Action)(() => OnSwitcherAudioInputEventTypeGainChanged(s, a))));//
                    sw.BMDSwitcherAudioInput[i].SwitcherAudioInputEventTypeBalanceChanged += new SwitcherAudioInputEventHandler((s, a) => this.Invoke((Action)(() => OnSwitcherAudioInputEventTypeBalanceChanged(s, a))));//
                    sw.BMDSwitcherAudioInput[i].SwitcherAudioInputEventTypeMixOptionChanged += new SwitcherAudioInputEventHandler((s, a) => this.Invoke((Action)(() => OnSwitcherAudioInputEventTypeMixOptionChanged(s, a))));//
                }
                sw.BMDSwitcherAudioMixer.SwitcherAudioMixerEventTypeProgramOutGainChanged += new SwitcherAudioMixerEventHandler((s, a) => this.Invoke((Action)(() => OnSwitcherAudioMixerEventTypeProgramOutGainChanged(s, a))));//
                sw.BMDSwitcherAudioMixer.SwitcherAudioMixerEventTypeProgramOutBalanceChanged += new SwitcherAudioMixerEventHandler((s, a) => this.Invoke((Action)(() => OnSwitcherAudioMixerEventTypeProgramOutBalanceChanged(s, a))));//

                sw.BMDSwitcherMixEffectBlock_7_5[0].SwitcherMixEffectBlockPropertyIdPreviewInput_v7_5 += new SwitcherMixEffectBlockEventHandler_v7_5((s, a) => this.Invoke((Action)(() => OnSwitcherMixEffectBlockPropertyIdPreviewInput(s, a))));//
                sw.BMDSwitcherMixEffectBlock_7_5[0].SwitcherMixEffectBlockPropertyIdProgramInput_v7_5 += new SwitcherMixEffectBlockEventHandler_v7_5((s, a) => this.Invoke((Action)(() => OnSwitcherMixEffectBlockPropertyIdProgramInput(s, a))));//
                sw.BMDSwitcherMixEffectBlock_7_5[0].SwitcherMixEffectBlockPropertyIdInTransition_v7_5 += new SwitcherMixEffectBlockEventHandler_v7_5((s, a) => this.Invoke((Action)(() => OnSwitcherMixEffectBlockPropertyIdInTransition(s, a))));//
                sw.BMDSwitcherMixEffectBlock_7_5[0].SwitcherMixEffectBlockPropertyIdTransitionPosition_v7_5 += new SwitcherMixEffectBlockEventHandler_v7_5((s, a) => this.Invoke((Action)(() => OnSwitcherMixEffectBlockPropertyIdTransitionPosition(s, a))));//
                sw.BMDSwitcherMixEffectBlock_7_5[0].SwitcherMixEffectBlockPropertyIdTransitionFramesRemaining_v7_5 += new SwitcherMixEffectBlockEventHandler_v7_5((s, a) => this.Invoke((Action)(() => OnSwitcherMixEffectBlockPropertyIdTransitionFramesRemaining(s, a))));//
                sw.BMDSwitcherMixEffectBlock_7_5[0].SwitcherMixEffectBlockPropertyIdPreviewTransition_v7_5 += new SwitcherMixEffectBlockEventHandler_v7_5((s, a) => this.Invoke((Action)(() => OnSwitcherMixEffectBlockPropertyIdPreviewTransition(s, a))));//
                sw.BMDSwitcherMixEffectBlock_7_5[0].SwitcherMixEffectBlockPropertyIdFadeToBlackInTransition_v7_5 += new SwitcherMixEffectBlockEventHandler_v7_5((s, a) => this.Invoke((Action)(() => OnSwitcherMixEffectBlockPropertyIdFadeToBlackInTransition(s, a))));//
                sw.BMDSwitcherMixEffectBlock_7_5[0].SwitcherMixEffectBlockPropertyIdFadeToBlackFullyBlack_v7_5 += new SwitcherMixEffectBlockEventHandler_v7_5((s, a) => this.Invoke((Action)(() => OnSwitcherMixEffectBlockPropertyIdFadeToBlackFullyBlack(s, a))));//
                sw.BMDSwitcherMixEffectBlock_7_5[0].SwitcherMixEffectBlockPropertyIdFadeToBlackFramesRemaining_v7_5 += new SwitcherMixEffectBlockEventHandler_v7_5((s, a) => this.Invoke((Action)(() => OnSwitcherMixEffectBlockPropertyIdFadeToBlackFramesRemaining(s, a))));//

                for (int i = 0; i < sw.BMDSwitcherKey.Count; i++)
                {
                    sw.BMDSwitcherKey[i].SwitcherKeyEventTypeOnAirChanged += new SwitcherKeyEventHandler((s, a) => this.Invoke((Action)(() => OnSwitcherKeyEventTypeOnAirChanged(s, a))));//
                }

                for (int i = 0; i < sw.BMDSwitcherDownstreamKey.Count; i++)
                {
                    sw.BMDSwitcherDownstreamKey[i].SwitcherDownstreamKeyEventTypeFramesRemainingChanged += new SwitcherDownstreamKeyEventHandler((s, a) => this.Invoke((Action)(() => OnSwitcherDownstreamKeyEventTypeFramesRemainingChanged(s, a))));//
                    sw.BMDSwitcherDownstreamKey[i].SwitcherDownstreamKeyEventTypeIsAutoTransitioningChanged += new SwitcherDownstreamKeyEventHandler((s, a) => this.Invoke((Action)(() => OnSwitcherDownstreamKeyEventTypeIsAutoTransitioningChanged(s, a))));//
                    sw.BMDSwitcherDownstreamKey[i].SwitcherDownstreamKeyEventTypeOnAirChanged += new SwitcherDownstreamKeyEventHandler((s, a) => this.Invoke((Action)(() => OnSwitcherDownstreamKeyEventTypeOnAirChanged(s, a))));//
                    sw.BMDSwitcherDownstreamKey[i].SwitcherDownstreamKeyEventTypeTieChanged += new SwitcherDownstreamKeyEventHandler((s, a) => this.Invoke((Action)(() => OnSwitcherDownstreamKeyEventTypeTieChanged(s, a))));//
                }

                sw.BMDSwitcherTransitionParameters.SwitcherTransitionParametersEventTypeTransitionStyleChanged += new SwitcherTransitionParametersEventHandler((s, a) => this.Invoke((Action)(() => OnSwitcherTransitionParametersEventTypeTransitionStyleChanged(s, a))));//
                sw.BMDSwitcherTransitionParameters.SwitcherTransitionParametersEventTypeNextTransitionStyleChanged += new SwitcherTransitionParametersEventHandler((s, a) => this.Invoke((Action)(() => OnSwitcherTransitionParametersEventTypeNextTransitionStyleChanged(s, a))));//

                OnSwitcherMixEffectBlockPropertyIdPreviewInput(sw.BMDSwitcherMixEffectBlock_7_5[0], null);
                OnSwitcherMixEffectBlockPropertyIdProgramInput(sw.BMDSwitcherMixEffectBlock_7_5[0], null);
                OnSwitcherMixEffectBlockPropertyIdInTransition(sw.BMDSwitcherMixEffectBlock_7_5[0], null);
                OnSwitcherMixEffectBlockPropertyIdTransitionPosition(sw.BMDSwitcherMixEffectBlock_7_5[0], null);

                for (int i = 0; i < sw.BMDSwitcherKey.Count; i++)
                {
                    OnSwitcherKeyEventTypeOnAirChanged(sw.BMDSwitcherKey[i], null);
                }

                for (int i = 0; i < sw.BMDSwitcherDownstreamKey.Count; i++)
                {
                    OnSwitcherDownstreamKeyEventTypeTieChanged(sw.BMDSwitcherDownstreamKey[i], null);
                    OnSwitcherDownstreamKeyEventTypeOnAirChanged(sw.BMDSwitcherDownstreamKey[i], null);
                    OnSwitcherDownstreamKeyEventTypeIsAutoTransitioningChanged(sw.BMDSwitcherDownstreamKey[i], null);
                    OnSwitcherDownstreamKeyEventTypeFramesRemainingChanged(sw.BMDSwitcherDownstreamKey[i], null);
                }

                OnSwitcherMixEffectBlockPropertyIdPreviewTransition(sw.BMDSwitcherMixEffectBlock_7_5[0], null);
                OnSwitcherMixEffectBlockPropertyIdFadeToBlackInTransition(sw.BMDSwitcherMixEffectBlock_7_5[0], null);
                OnSwitcherMixEffectBlockPropertyIdFadeToBlackFullyBlack(sw.BMDSwitcherMixEffectBlock_7_5[0], null);
                OnSwitcherMixEffectBlockPropertyIdFadeToBlackFramesRemaining(sw.BMDSwitcherMixEffectBlock_7_5[0], null);
                OnSwitcherTransitionParametersEventTypeTransitionStyleChanged(sw.BMDSwitcherTransitionParameters, null);
                OnSwitcherTransitionParametersEventTypeNextTransitionStyleChanged(sw.BMDSwitcherTransitionParameters, null);

                OnSwitcherAudioInputEventTypeBalanceChanged(sw.BMDSwitcherAudioInput[sw.BMDSwitcherAudioInput.Count - 1], null);
                OnSwitcherAudioInputEventTypeGainChanged(sw.BMDSwitcherAudioInput[sw.BMDSwitcherAudioInput.Count - 1], null);
                OnSwitcherAudioMixerEventTypeProgramOutBalanceChanged(sw.BMDSwitcherAudioMixer, null);
                OnSwitcherAudioMixerEventTypeProgramOutGainChanged(sw.BMDSwitcherAudioMixer, null);
                GroupBoxPreset2_Enter(this, null);
                GroupBoxPreset1_Enter(this, null);
                #endregion
            }));
            base.OnLoad(e);
        }
        protected override void OnClosed(EventArgs e)
        {
            if (midi_BCF2000 != null)
            {
                midi_BCF2000.Disconnect();
                midi_BCF2000 = null;
            }

            if (XKeys != null)
            {
                XKeys.Disconnect();
                XKeys = null;
            }

            if (sw != null)
            {
                sw.Disconnect();
                sw = null;
            }

            base.OnClosed(e);
        }

        private void OnUpperRowKeys(object s, ChannelMessageEventArgs a)
        {
            switch (midi_BCF2000.UserKey1)
            {
                case 0:
                    switch (midi_BCF2000.ChangedButton)
                    {
                        case BCF2000Buttons.UpperRowKey1:
                            ButtonProgramCam1_Click(this, null);
                            break;
                        case BCF2000Buttons.UpperRowKey2:
                            ButtonProgramCam2_Click(this, null);
                            break;
                        case BCF2000Buttons.UpperRowKey3:
                            ButtonProgramCam3_Click(this, null);
                            break;
                        case BCF2000Buttons.UpperRowKey4:
                            ButtonProgramCam4_Click(this, null);
                            break;
                        case BCF2000Buttons.UpperRowKey5:
                            ButtonProgramCam5_Click(this, null);
                            break;
                        case BCF2000Buttons.UpperRowKey6:
                            ButtonProgramCam6_Click(this, null);
                            break;
                        case BCF2000Buttons.UpperRowKey7:
                            ButtonProgramCam7_Click(this, null);
                            break;
                        case BCF2000Buttons.UpperRowKey8:
                            ButtonProgramCam8_Click(this, null);
                            break;
                    }
                    break;

                case 1:
                    break;
            }
        }
        private void OnLowerRowKeys(object s, ChannelMessageEventArgs a)
        {
            switch (midi_BCF2000.UserKey1)
            {
                case 0:
                    switch (midi_BCF2000.ChangedButton)
                    {
                        case BCF2000Buttons.LowerRowKey1:
                            ButtonPreviewCam1_Click(this, null);
                            break;
                        case BCF2000Buttons.LowerRowKey2:
                            ButtonPreviewCam2_Click(this, null);
                            break;
                        case BCF2000Buttons.LowerRowKey3:
                            ButtonPreviewCam3_Click(this, null);
                            break;
                        case BCF2000Buttons.LowerRowKey4:
                            ButtonPreviewCam4_Click(this, null);
                            break;
                        case BCF2000Buttons.LowerRowKey5:
                            ButtonPreviewCam5_Click(this, null);
                            break;
                        case BCF2000Buttons.LowerRowKey6:
                            ButtonPreviewCam6_Click(this, null);
                            break;
                        case BCF2000Buttons.LowerRowKey7:
                            ButtonPreviewCam7_Click(this, null);
                            break;
                        case BCF2000Buttons.LowerRowKey8:
                            ButtonPreviewCam8_Click(this, null);
                            break;
                    }
                    break;

                case 1:
                    break;
            }
        }
        private void OnUserKey1(object s, ChannelMessageEventArgs a)
        {
            checkBoxUser1.Checked = (midi_BCF2000.UserKey1 == 1);
            for (int i = 33; i <= 48; i++)
            {
                this.midi_BCF2000.MIDI_Send(ChannelCommand.NoteOn, 0, 0, i, 0);
            }

            switch (midi_BCF2000.UserKey1)
            {
                // Leave Audio modes
                case 0:
                    this.midi_BCF2000.MIDI_Send(ChannelCommand.NoteOn, 0, 0, 40 + (int)sw.BMDSwitcherMixEffectBlock_7_5[0].GetInt(BMDSwitcherAPI._BMDSwitcherMixEffectBlockPropertyId_v7_5.bmdSwitcherMixEffectBlockPropertyIdPreviewInput_v7_5), 1);
                    this.midi_BCF2000.MIDI_Send(ChannelCommand.NoteOn, 0, 0, 32 + (int)sw.BMDSwitcherMixEffectBlock_7_5[0].GetInt(BMDSwitcherAPI._BMDSwitcherMixEffectBlockPropertyId_v7_5.bmdSwitcherMixEffectBlockPropertyIdProgramInput_v7_5), 1);
                    break;

                case 1:
                    switch (ActivePreset)
                    {
                        case 1:
                            for (int i = 1; i <= 6; i++)
                            {
                                this.midi_BCF2000.MIDI_Send(ChannelCommand.NoteOn, 0, 0, 32 + i, sw.BMDSwitcherAudioInput[i].MixOption == _BMDSwitcherAudioMixOption.bmdSwitcherAudioMixOptionOn ? 1 : 0);
                                this.midi_BCF2000.MIDI_Send(ChannelCommand.NoteOn, 0, 0, 40 + i, sw.BMDSwitcherAudioInput[i].MixOption == _BMDSwitcherAudioMixOption.bmdSwitcherAudioMixOptionAudioFollowVideo ? 1 : 0);
                            }
                            break;

                        case 2:
                            for (int i = 1; i <= 2; i++)
                            {
                                this.midi_BCF2000.MIDI_Send(ChannelCommand.NoteOn, 0, 0, 32 + i, sw.BMDSwitcherAudioInput[i + 6].MixOption == _BMDSwitcherAudioMixOption.bmdSwitcherAudioMixOptionOn ? 1 : 0);
                                this.midi_BCF2000.MIDI_Send(ChannelCommand.NoteOn, 0, 0, 40 + i, sw.BMDSwitcherAudioInput[i + 6].MixOption == _BMDSwitcherAudioMixOption.bmdSwitcherAudioMixOptionAudioFollowVideo ? 1 : 0);
                            }
                            break;
                    }
                    break;
            }
        }

        private void OnTouchpad(object s, iCON_MIDI_iPad_EventArgs a)
        {
            textBoxX.Text = a.TouchPadX.ToString();
            textBoxY.Text = a.TouchPadY.ToString();
        }
        private void OnButton(object s, iCON_MIDI_iPad_EventArgs a)
        {
            buttoniCON1.ImageIndex = (long)(a.ButtonNr & (long)iCONButtons.iCONButtonP1) > 0 ? 2 : 0;
            buttoniCON2.ImageIndex = (long)(a.ButtonNr & (long)iCONButtons.iCONButtonP2) > 0 ? 2 : 0;
            buttoniCON3.ImageIndex = (long)(a.ButtonNr & (long)iCONButtons.iCONButtonP3) > 0 ? 2 : 0;
            buttoniCON4.ImageIndex = (long)(a.ButtonNr & (long)iCONButtons.iCONButtonP4) > 0 ? 2 : 0;
            buttoniCON5.ImageIndex = (long)(a.ButtonNr & (long)iCONButtons.iCONButtonP5) > 0 ? 2 : 0;
            buttoniCON6.ImageIndex = (long)(a.ButtonNr & (long)iCONButtons.iCONButtonP6) > 0 ? 2 : 0;
            buttoniCON7.ImageIndex = (long)(a.ButtonNr & (long)iCONButtons.iCONButtonP7) > 0 ? 2 : 0;
            buttoniCON8.ImageIndex = (long)(a.ButtonNr & (long)iCONButtons.iCONButtonP8) > 0 ? 2 : 0;
            buttoniCON9.ImageIndex = (long)(a.ButtonNr & (long)iCONButtons.iCONButtonP9) > 0 ? 2 : 0;
            buttoniCON10.ImageIndex = (long)(a.ButtonNr & (long)iCONButtons.iCONButtonP10) > 0 ? 2 : 0;
            buttoniCON11.ImageIndex = (long)(a.ButtonNr & (long)iCONButtons.iCONButtonP11) > 0 ? 2 : 0;
            buttoniCON12.ImageIndex = (long)(a.ButtonNr & (long)iCONButtons.iCONButtonP12) > 0 ? 2 : 0;

            switch (a.Preset)
            {
                case 1:
                    switch (a.ButtonNr)
                    {
                        case (long)BMDMidiLib.iCONButtons.iCONButtonP1:
                            //MXLightSocket.StreamOn();
                            break;
                        case (long)BMDMidiLib.iCONButtons.iCONButtonP7:
                            //MXLightSocket.StreamOff();
                            break;

                        case (long)BMDMidiLib.iCONButtons.iCONButtonP2:
                            //MXLightSocket.PreviewOn();
                            break;
                        case (long)BMDMidiLib.iCONButtons.iCONButtonP8:
                            //MXLightSocket.PreviewOff();
                            break;

                        case (long)BMDMidiLib.iCONButtons.iCONButtonP3:
                            //MXLightSocket.RecordOn();
                            break;
                        case (long)BMDMidiLib.iCONButtons.iCONButtonP9:
                            //MXLightSocket.RecordOff();
                            break;

                        case (long)BMDMidiLib.iCONButtons.iCONButtonP12:
                            //MXLightSocket.Disconnect();
                            //MXLightSocket.Restart();
                            //MXLightSocket.Connect();
                            break;
                    }
                    break;

                case 2:
                    switch (a.ButtonNr)
                    {
                        case (long)BMDMidiLib.iCONButtons.iCONButtonP1:
                            //MXLightSocket.ReplayAddCue("test");
                            break;
                        case (long)BMDMidiLib.iCONButtons.iCONButtonP2:
                            //MXLightSocket.ReplayPlayLastCue();
                            break;

                        case (long)BMDMidiLib.iCONButtons.iCONButtonP5:
                            //MXLightSocket.ReplayStop();
                            break;

                        case (long)BMDMidiLib.iCONButtons.iCONButtonP6:
                            //MXLightSocket.ReplaySpeed(0.5);
                            break;
                        case (long)BMDMidiLib.iCONButtons.iCONButtonP12:
                            //MXLightSocket.ReplaySpeed(1);
                            break;
                    }
                    break;

                case 3:
                    break;

                case 4:
                    break;
            }
        }
        private void OnFaderF1(object s, iCON_MIDI_iPad_EventArgs a)
        {
            double position = ((float)(a.FaderF1) / 127 * 100) / 100.0;
            if (m_moveSliderDownwards)
                position = (100 - ((float)(a.FaderF1) / 127 * 100)) / 100.0;
            sw.BMDSwitcherMixEffectBlock_7_5[0].SetFloat(BMDSwitcherAPI._BMDSwitcherMixEffectBlockPropertyId_v7_5.bmdSwitcherMixEffectBlockPropertyIdTransitionPosition_v7_5, position);
        }

        #region Switcher Events
        private void OnSwitcherAudioInputEventTypeBalanceChanged(SwitcherAudioInputCallback s, SwitcherAudioInputEventArgs a)
        {
            switch (ActivePreset)
            {
                case 1:
                    switch ((SwitcherAudioInputsValues)s.AudioInputId)
                    {
                        case SwitcherAudioInputsValues.Camera1:
                            trackBarAudioBalance1.Value = (int)(s.Balance * 127 / 2 + 63.5);
                            if (existMIDI) midi_BCF2000.Encoder1 = trackBarAudioBalance1.Value;
                            textBoxAudioBalance1.Text = ((int)(s.Balance * 50)).ToString();
                            break;

                        case SwitcherAudioInputsValues.Camera2:
                            trackBarAudioBalance2.Value = (int)(s.Balance * 127 / 2 + 63.5);
                            if (existMIDI) midi_BCF2000.Encoder2 = trackBarAudioBalance2.Value;
                            textBoxAudioBalance2.Text = ((int)(s.Balance * 50)).ToString();
                            break;

                        case SwitcherAudioInputsValues.Camera3:
                            trackBarAudioBalance3.Value = (int)(s.Balance * 127 / 2 + 63.5);
                            if (existMIDI) midi_BCF2000.Encoder3 = trackBarAudioBalance3.Value;
                            textBoxAudioBalance3.Text = ((int)(s.Balance * 50)).ToString();
                            break;

                        case SwitcherAudioInputsValues.Camera4:
                            trackBarAudioBalance4.Value = (int)(s.Balance * 127 / 2 + 63.5);
                            if (existMIDI) midi_BCF2000.Encoder4 = trackBarAudioBalance4.Value;
                            textBoxAudioBalance4.Text = ((int)(s.Balance * 50)).ToString();
                            break;

                        case SwitcherAudioInputsValues.Camera5:
                            trackBarAudioBalance5.Value = (int)(s.Balance * 127 / 2 + 63.5);
                            if (existMIDI) midi_BCF2000.Encoder5 = trackBarAudioBalance5.Value;
                            textBoxAudioBalance5.Text = ((int)(s.Balance * 50)).ToString();
                            break;

                        case SwitcherAudioInputsValues.Camera6:
                            trackBarAudioBalance6.Value = (int)(s.Balance * 127 / 2 + 63.5);
                            if (existMIDI) midi_BCF2000.Encoder6 = trackBarAudioBalance6.Value;
                            textBoxAudioBalance6.Text = ((int)(s.Balance * 50)).ToString();
                            break;
                    }
                    break;

                case 2:
                    switch ((SwitcherAudioInputsValues)s.AudioInputId)
                    {
                        case SwitcherAudioInputsValues.Camera7:
                            trackBarAudioBalance7.Value = (int)(s.Balance * 127 / 2 + 63.5);
                            if (existMIDI) midi_BCF2000.Encoder1 = trackBarAudioBalance7.Value;
                            textBoxAudioBalance7.Text = ((int)(s.Balance * 50)).ToString();
                            break;

                        case SwitcherAudioInputsValues.Camera8:
                            trackBarAudioBalance8.Value = (int)(s.Balance * 127 / 2 + 63.5);
                            if (existMIDI) midi_BCF2000.Encoder2 = trackBarAudioBalance8.Value;
                            textBoxAudioBalance8.Text = ((int)(s.Balance * 50)).ToString();
                            break;

                        case SwitcherAudioInputsValues.MediaPlayer1:
                            trackBarAudioBalanceMP1.Value = (int)(s.Balance * 127 / 2 + 63.5);
                            if (existMIDI) midi_BCF2000.Encoder3 = trackBarAudioBalanceMP1.Value;
                            textBoxAudioBalanceMP1.Text = ((int)(s.Balance * 50)).ToString();
                            break;

                        case SwitcherAudioInputsValues.MediaPlayer2:
                            trackBarAudioBalanceMP2.Value = (int)(s.Balance * 127 / 2 + 63.5);
                            if (existMIDI) midi_BCF2000.Encoder4 = trackBarAudioBalanceMP2.Value;
                            textBoxAudioBalanceMP2.Text = ((int)(s.Balance * 50)).ToString();
                            break;
                    }
                    if (existMIDI)
                    {
                        midi_BCF2000.Encoder5 = 0;
                        midi_BCF2000.Encoder6 = 0;
                    }
                    break;
            }

            if ((SwitcherAudioInputsValues)s.AudioInputId == SwitcherAudioInputsValues.ExternalXLR)
            {
                trackBarAudioBalanceEXT.Value = (int)(s.Balance * 127 / 2 + 63.5);
                if(existMIDI) midi_BCF2000.Encoder7 = trackBarAudioBalanceEXT.Value;
                textBoxAudioBalanceEXT.Text = ((int)(s.Balance * 50)).ToString();
            }

        }
        private void OnSwitcherAudioInputEventTypeGainChanged(SwitcherAudioInputCallback s, SwitcherAudioInputEventArgs a)
        {
            int trackBarAudioGainValue = (s.Gain.ToString() != "-Infinity") ? (int)((s.Gain + 60.4817648225089) * (127.0 / 66.48179729958136)) : 0;
         
            switch (ActivePreset)
            {
                case 1:
                    switch ((SwitcherAudioInputsValues)s.AudioInputId)
                    {
                        case SwitcherAudioInputsValues.Camera1:
                            trackBarAudioGain1.Value = trackBarAudioGainValue;
                            if (existMIDI) midi_BCF2000.Fader1 = trackBarAudioGain1.Value;
                            textBoxAudioChannel1.Text = Math.Round(s.Gain, 2).ToString();
                            break;

                        case SwitcherAudioInputsValues.Camera2:
                            trackBarAudioGain2.Value = trackBarAudioGainValue;
                            if (existMIDI) midi_BCF2000.Fader2 = trackBarAudioGain2.Value;
                            textBoxAudioChannel2.Text = Math.Round(s.Gain, 2).ToString();
                            break;

                        case SwitcherAudioInputsValues.Camera3:
                            trackBarAudioGain3.Value = trackBarAudioGainValue;
                            if (existMIDI) midi_BCF2000.Fader3 = trackBarAudioGain3.Value;
                            textBoxAudioChannel3.Text = Math.Round(s.Gain, 2).ToString();
                            break;

                        case SwitcherAudioInputsValues.Camera4:
                            trackBarAudioGain4.Value = trackBarAudioGainValue;
                            if (existMIDI) midi_BCF2000.Fader4 = trackBarAudioGain4.Value;
                            textBoxAudioChannel4.Text = Math.Round(s.Gain, 2).ToString();
                            break;

                        case SwitcherAudioInputsValues.Camera5:
                            trackBarAudioGain5.Value = trackBarAudioGainValue;
                            if (existMIDI) midi_BCF2000.Fader5 = trackBarAudioGain5.Value;
                            textBoxAudioChannel5.Text = Math.Round(s.Gain, 2).ToString();
                            break;

                        case SwitcherAudioInputsValues.Camera6:
                            trackBarAudioGain6.Value = trackBarAudioGainValue;
                            if (existMIDI) midi_BCF2000.Fader6 = trackBarAudioGain6.Value;
                            textBoxAudioChannel6.Text = Math.Round(s.Gain, 2).ToString();
                            break;
                    }
                    break;

                case 2:
                    switch ((SwitcherAudioInputsValues)s.AudioInputId)
                    {
                        case SwitcherAudioInputsValues.Camera7:
                            trackBarAudioGain7.Value = trackBarAudioGainValue;
                            if (existMIDI) midi_BCF2000.Fader1 = trackBarAudioGain7.Value;
                            textBoxAudioChannel7.Text = Math.Round(s.Gain, 2).ToString();
                            break;

                        case SwitcherAudioInputsValues.Camera8:
                            trackBarAudioGain8.Value = trackBarAudioGainValue;
                            if (existMIDI) midi_BCF2000.Fader2 = trackBarAudioGain8.Value;
                            textBoxAudioChannel8.Text = Math.Round(s.Gain, 2).ToString();
                            break;

                        case SwitcherAudioInputsValues.Camera9:
                            if (sw.BMDSwitcherInfo.TotalSwitcherPortTypeExternal> 8)
                            {
                                trackBarAudioGain9.Value = trackBarAudioGainValue;
                                if (existMIDI) midi_BCF2000.Fader3 = trackBarAudioGain9.Value;
                                textBoxAudioChannel9.Text = Math.Round(s.Gain, 2).ToString();
                            }
                            break;

                        case SwitcherAudioInputsValues.Camera10:
                            if (sw.BMDSwitcherInfo.TotalSwitcherPortTypeExternal > 8)
                            {
                                trackBarAudioGain10.Value = trackBarAudioGainValue;
                                if (existMIDI) midi_BCF2000.Fader4 = trackBarAudioGain9.Value;
                                textBoxAudioChannel10.Text = Math.Round(s.Gain, 2).ToString();
                            }
                            break;

                        case SwitcherAudioInputsValues.MediaPlayer1:
                            trackBarAudioGainMP1.Value = trackBarAudioGainValue;
                            if (existMIDI) midi_BCF2000.Fader5 = trackBarAudioGainMP1.Value;
                            textBoxAudioChannelMP1.Text = Math.Round(s.Gain, 2).ToString();
                            break;

                        case SwitcherAudioInputsValues.MediaPlayer2:
                            trackBarAudioGainMP2.Value = trackBarAudioGainValue;
                            if (existMIDI) midi_BCF2000.Fader6 = trackBarAudioGainMP2.Value;
                            textBoxAudioChannelMP2.Text = Math.Round(s.Gain, 2).ToString();
                            break;
                    }
                    if (sw.BMDSwitcherInfo.TotalSwitcherPortTypeExternal <= 8)
                    {
                        if (existMIDI)
                        {
                            midi_BCF2000.Fader3 = 0;
                            midi_BCF2000.Fader4 = 0;
                        }
                    }
                    break;
            }

            if ((SwitcherAudioInputsValues)s.AudioInputId == SwitcherAudioInputsValues.ExternalXLR)
            {
                trackBarAudioGainEXT.Value = trackBarAudioGainValue;
                if (existMIDI) midi_BCF2000.Fader7 = trackBarAudioGainEXT.Value;
                textBoxAudioChannelEXT.Text = Math.Round(s.Gain, 2).ToString();
            }

        }
        private void OnSwitcherAudioMixerEventTypeProgramOutBalanceChanged(SwitcherAudioMixerCallback s, SwitcherAudioMixerEventArgs a)
        {
            trackBarAudioBalanceMaster.Value = (int)(s.ProgramOutBalance * 127 / 2 + 63.5);
            if (existMIDI) midi_BCF2000.Encoder1 = trackBarAudioBalanceMaster.Value;
            textBoxAudioBalanceMaster.Text = ((int)(s.ProgramOutBalance * 50)).ToString();
        }
        private void OnSwitcherAudioMixerEventTypeProgramOutGainChanged(SwitcherAudioMixerCallback s, SwitcherAudioMixerEventArgs a)
        {
            trackBarAudioGainMaster.Value = (s.ProgramOutGain.ToString() != "-Infinity") ? (int)((s.ProgramOutGain + 60.4817648225089) * (127.0 / 66.48179729958136)) : 0;
            if (existMIDI) midi_BCF2000.Fader8 = trackBarAudioGainMaster.Value;
            textBoxAudioChannelMaster.Text = Math.Round(s.ProgramOutGain, 2).ToString();
        }

        private void OnSwitcherAudioInputEventTypeMixOptionChanged(SwitcherAudioInputCallback s, SwitcherAudioInputEventArgs a)
        {
            //MessageBox.Show(s.AudioInputId.ToString());

            switch(s.MixOption)
            {
                case _BMDSwitcherAudioMixOption.bmdSwitcherAudioMixOptionOn:
                    break;
                case _BMDSwitcherAudioMixOption.bmdSwitcherAudioMixOptionOff:
                    break;
                case _BMDSwitcherAudioMixOption.bmdSwitcherAudioMixOptionAudioFollowVideo:
                    break;
            }

            if (existMIDI)
            {
                switch (ActivePreset)
                {
                    case 1:
                        switch (s.AudioInputId)
                        {
                            case 1:
                                if (existMIDI)
                                    midi_BCF2000.UpperRowKey1 = (s.MixOption == _BMDSwitcherAudioMixOption.bmdSwitcherAudioMixOptionOn ? 1 : 0);
                                midi_BCF2000.LowerRowKey1 = (s.MixOption == _BMDSwitcherAudioMixOption.bmdSwitcherAudioMixOptionAudioFollowVideo ? 1 : 0);
                                break;
                            case 2:
                                midi_BCF2000.UpperRowKey2 = (s.MixOption == _BMDSwitcherAudioMixOption.bmdSwitcherAudioMixOptionOn ? 1 : 0);
                                midi_BCF2000.LowerRowKey2 = (s.MixOption == _BMDSwitcherAudioMixOption.bmdSwitcherAudioMixOptionAudioFollowVideo ? 1 : 0);
                                break;
                            case 3:
                                midi_BCF2000.UpperRowKey3 = (s.MixOption == _BMDSwitcherAudioMixOption.bmdSwitcherAudioMixOptionOn ? 1 : 0);
                                midi_BCF2000.LowerRowKey3 = (s.MixOption == _BMDSwitcherAudioMixOption.bmdSwitcherAudioMixOptionAudioFollowVideo ? 1 : 0);
                                break;
                            case 4:
                                midi_BCF2000.UpperRowKey4 = (s.MixOption == _BMDSwitcherAudioMixOption.bmdSwitcherAudioMixOptionOn ? 1 : 0);
                                midi_BCF2000.LowerRowKey4 = (s.MixOption == _BMDSwitcherAudioMixOption.bmdSwitcherAudioMixOptionAudioFollowVideo ? 1 : 0);
                                break;
                            case 5:
                                midi_BCF2000.UpperRowKey5 = (s.MixOption == _BMDSwitcherAudioMixOption.bmdSwitcherAudioMixOptionOn ? 1 : 0);
                                midi_BCF2000.LowerRowKey5 = (s.MixOption == _BMDSwitcherAudioMixOption.bmdSwitcherAudioMixOptionAudioFollowVideo ? 1 : 0);
                                break;
                            case 6:
                                midi_BCF2000.UpperRowKey6 = (s.MixOption == _BMDSwitcherAudioMixOption.bmdSwitcherAudioMixOptionOn ? 1 : 0);
                                midi_BCF2000.LowerRowKey6 = (s.MixOption == _BMDSwitcherAudioMixOption.bmdSwitcherAudioMixOptionAudioFollowVideo ? 1 : 0);
                                break;
                        }
                        break;

                    case 2:
                        midi_BCF2000.UpperRowKey3 = 0;
                        midi_BCF2000.LowerRowKey3 = 0;
                        midi_BCF2000.UpperRowKey4 = 0;
                        midi_BCF2000.LowerRowKey4 = 0;
                        switch (s.AudioInputId)
                        {
                            case 7:
                                midi_BCF2000.UpperRowKey1 = (s.MixOption == _BMDSwitcherAudioMixOption.bmdSwitcherAudioMixOptionOn ? 1 : 0);
                                midi_BCF2000.LowerRowKey1 = (s.MixOption == _BMDSwitcherAudioMixOption.bmdSwitcherAudioMixOptionAudioFollowVideo ? 1 : 0);
                                break;
                            case 8:
                                midi_BCF2000.UpperRowKey2 = (s.MixOption == _BMDSwitcherAudioMixOption.bmdSwitcherAudioMixOptionOn ? 1 : 0);
                                midi_BCF2000.LowerRowKey2 = (s.MixOption == _BMDSwitcherAudioMixOption.bmdSwitcherAudioMixOptionAudioFollowVideo ? 1 : 0);
                                break;
                        }
                        break;
                }
            }
        }

        private void OnSwitcherTransitionParametersEventTypeNextTransitionStyleChanged(SwitcherTransitionParametersCallback s, SwitcherTransitionParametersEventArgs a)
        {
            buttonNextTransitionBKGD.ImageIndex = (s.NextTransitionSelection & BMDSwitcherAPI._BMDSwitcherTransitionSelection.bmdSwitcherTransitionSelectionBackground) == BMDSwitcherAPI._BMDSwitcherTransitionSelection.bmdSwitcherTransitionSelectionBackground ? 3 : 0;
            buttonNextTransitionKEY1.ImageIndex = (s.NextTransitionSelection & BMDSwitcherAPI._BMDSwitcherTransitionSelection.bmdSwitcherTransitionSelectionKey1) == BMDSwitcherAPI._BMDSwitcherTransitionSelection.bmdSwitcherTransitionSelectionKey1 ? 3 : 0;
            buttonNextTransitionKEY2.ImageIndex = (s.NextTransitionSelection & BMDSwitcherAPI._BMDSwitcherTransitionSelection.bmdSwitcherTransitionSelectionKey2) == BMDSwitcherAPI._BMDSwitcherTransitionSelection.bmdSwitcherTransitionSelectionKey2 ? 3 : 0;
            buttonNextTransitionKEY3.ImageIndex = (s.NextTransitionSelection & BMDSwitcherAPI._BMDSwitcherTransitionSelection.bmdSwitcherTransitionSelectionKey3) == BMDSwitcherAPI._BMDSwitcherTransitionSelection.bmdSwitcherTransitionSelectionKey3 ? 3 : 0;
            buttonNextTransitionKEY4.ImageIndex = (s.NextTransitionSelection & BMDSwitcherAPI._BMDSwitcherTransitionSelection.bmdSwitcherTransitionSelectionKey4) == BMDSwitcherAPI._BMDSwitcherTransitionSelection.bmdSwitcherTransitionSelectionKey4 ? 3 : 0;
        }
        private void OnSwitcherTransitionParametersEventTypeTransitionStyleChanged(SwitcherTransitionParametersCallback s, SwitcherTransitionParametersEventArgs a)
        {
            buttonTransitionStyleMix.ImageIndex = s.TransitionStyle == BMDSwitcherAPI._BMDSwitcherTransitionStyle.bmdSwitcherTransitionStyleMix ? 3 : 0;
            buttonTransitionStyleDip.ImageIndex = s.TransitionStyle == BMDSwitcherAPI._BMDSwitcherTransitionStyle.bmdSwitcherTransitionStyleDip ? 3 : 0;
            buttonTransitionStyleWipe.ImageIndex = s.TransitionStyle == BMDSwitcherAPI._BMDSwitcherTransitionStyle.bmdSwitcherTransitionStyleWipe ? 3 : 0;
            buttonTransitionStyleStinger.ImageIndex = s.TransitionStyle == BMDSwitcherAPI._BMDSwitcherTransitionStyle.bmdSwitcherTransitionStyleStinger ? 3 : 0;
            buttonTransitionStyleDVE.ImageIndex = s.TransitionStyle == BMDSwitcherAPI._BMDSwitcherTransitionStyle.bmdSwitcherTransitionStyleDVE ? 3 : 0;
        }
        private void OnSwitcherMixEffectBlockPropertyIdFadeToBlackFramesRemaining(SwitcherMixEffectBlockCallback_v7_5 s, SwitcherMixEffectBlockEventArgs_v7_5 a)
        {
            textBoxFadeToBlackRate.Text = s.GetInt(BMDSwitcherAPI._BMDSwitcherMixEffectBlockPropertyId_v7_5.bmdSwitcherMixEffectBlockPropertyIdFadeToBlackFramesRemaining_v7_5).ToString();
        }
        private void OnSwitcherMixEffectBlockPropertyIdFadeToBlackFullyBlack(SwitcherMixEffectBlockCallback_v7_5 s, SwitcherMixEffectBlockEventArgs_v7_5 a)
        {
            buttonFadeToBlack.ImageIndex = s.GetFlag(BMDSwitcherAPI._BMDSwitcherMixEffectBlockPropertyId_v7_5.bmdSwitcherMixEffectBlockPropertyIdFadeToBlackFullyBlack_v7_5) == 0 ? 0 : 2;
        }
        private void OnSwitcherMixEffectBlockPropertyIdFadeToBlackInTransition(SwitcherMixEffectBlockCallback_v7_5 s, SwitcherMixEffectBlockEventArgs_v7_5 a)
        {
            buttonFadeToBlack.ImageIndex = s.GetFlag(BMDSwitcherAPI._BMDSwitcherMixEffectBlockPropertyId_v7_5.bmdSwitcherMixEffectBlockPropertyIdFadeToBlackInTransition_v7_5) == 0 ? 0 : 2;
        }
        private void OnSwitcherMixEffectBlockPropertyIdPreviewTransition(SwitcherMixEffectBlockCallback_v7_5 s, SwitcherMixEffectBlockEventArgs_v7_5 a)
        {
            buttonTransitionStylePrevTrans.ImageIndex = s.GetFlag(BMDSwitcherAPI._BMDSwitcherMixEffectBlockPropertyId_v7_5.bmdSwitcherMixEffectBlockPropertyIdPreviewTransition_v7_5) == 1 ? 2 : 0;
        }
        private void OnSwitcherMixEffectBlockPropertyIdTransitionFramesRemaining(SwitcherMixEffectBlockCallback_v7_5 s, SwitcherMixEffectBlockEventArgs_v7_5 a)
        {
            textBoxNextTransitionRate.Text = s.GetInt(BMDSwitcherAPI._BMDSwitcherMixEffectBlockPropertyId_v7_5.bmdSwitcherMixEffectBlockPropertyIdTransitionFramesRemaining_v7_5).ToString();
        }
        private void OnSwitcherMixEffectBlockPropertyIdPreviewInput(SwitcherMixEffectBlockCallback_v7_5 s, SwitcherMixEffectBlockEventArgs_v7_5 a)
        {
            this.buttonPreviewCam1.ImageIndex = 0;
            this.buttonPreviewCam2.ImageIndex = 0;
            this.buttonPreviewCam3.ImageIndex = 0;
            this.buttonPreviewCam4.ImageIndex = 0;
            this.buttonPreviewCam5.ImageIndex = 0;
            this.buttonPreviewCam6.ImageIndex = 0;
            this.buttonPreviewCam7.ImageIndex = 0;
            this.buttonPreviewCam8.ImageIndex = 0;
            this.buttonPreviewCam9.ImageIndex = 0;
            this.buttonPreviewCam10.ImageIndex = 0;

            if (existMIDI)
            {
                if (midi_BCF2000.UserKey1 == 0)
                {
                    for (int i = 41; i <= 48; i++)
                    {
                        this.midi_BCF2000.MIDI_Send(ChannelCommand.NoteOn, 0, 0, i, 0);
                    }
                    this.midi_BCF2000.MIDI_Send(ChannelCommand.NoteOn, 0, 0, 40 + (int)s.GetInt(BMDSwitcherAPI._BMDSwitcherMixEffectBlockPropertyId_v7_5.bmdSwitcherMixEffectBlockPropertyIdPreviewInput_v7_5), 1);
                }
            }
            switch (s.GetInt(BMDSwitcherAPI._BMDSwitcherMixEffectBlockPropertyId_v7_5.bmdSwitcherMixEffectBlockPropertyIdPreviewInput_v7_5))
            {
                case 1:
                    this.buttonPreviewCam1.ImageIndex = 1;
                    break;
                case 2:
                    this.buttonPreviewCam2.ImageIndex = 1;
                    break;
                case 3:
                    this.buttonPreviewCam3.ImageIndex = 1;
                    break;
                case 4:
                    this.buttonPreviewCam4.ImageIndex = 1;
                    break;
                case 5:
                    this.buttonPreviewCam5.ImageIndex = 1;
                    break;
                case 6:
                    this.buttonPreviewCam6.ImageIndex = 1;
                    break;
                case 7:
                    this.buttonPreviewCam7.ImageIndex = 1;
                    break;
                case 8:
                    this.buttonPreviewCam8.ImageIndex = 1;
                    break;
                case 9:
                    this.buttonPreviewCam9.ImageIndex = 1;
                    break;
                case 10:
                    this.buttonPreviewCam10.ImageIndex = 1;
                    break;
            }
        }
        private void OnSwitcherMixEffectBlockPropertyIdProgramInput(SwitcherMixEffectBlockCallback_v7_5 s, SwitcherMixEffectBlockEventArgs_v7_5 a)
        {
            this.buttonProgramCam1.ImageIndex = 0;
            this.buttonProgramCam2.ImageIndex = 0;
            this.buttonProgramCam3.ImageIndex = 0;
            this.buttonProgramCam4.ImageIndex = 0;
            this.buttonProgramCam5.ImageIndex = 0;
            this.buttonProgramCam6.ImageIndex = 0;
            this.buttonProgramCam7.ImageIndex = 0;
            this.buttonProgramCam8.ImageIndex = 0;
            this.buttonProgramCam9.ImageIndex = 0;
            this.buttonProgramCam10.ImageIndex = 0;

            if (existMIDI)
            {
                if (midi_BCF2000.UserKey1 == 0)
                {
                    for (int i = 33; i <= 40; i++)
                    {
                        this.midi_BCF2000.MIDI_Send(ChannelCommand.NoteOn, 0, 0, i, 0);
                    }
                    this.midi_BCF2000.MIDI_Send(ChannelCommand.NoteOn, 0, 0, 32 + (int)s.GetInt(BMDSwitcherAPI._BMDSwitcherMixEffectBlockPropertyId_v7_5.bmdSwitcherMixEffectBlockPropertyIdProgramInput_v7_5), 1);
                }
            }
            switch (s.GetInt(BMDSwitcherAPI._BMDSwitcherMixEffectBlockPropertyId_v7_5.bmdSwitcherMixEffectBlockPropertyIdProgramInput_v7_5))
            {
                case 1:
                    this.buttonProgramCam1.ImageIndex = 2;
                    break;
                case 2:
                    this.buttonProgramCam2.ImageIndex = 2;
                    break;
                case 3:
                    this.buttonProgramCam3.ImageIndex = 2;
                    break;
                case 4:
                    this.buttonProgramCam4.ImageIndex = 2;
                    break;
                case 5:
                    this.buttonProgramCam5.ImageIndex = 2;
                    break;
                case 6:
                    this.buttonProgramCam6.ImageIndex = 2;
                    break;
                case 7:
                    this.buttonProgramCam7.ImageIndex = 2;
                    break;
                case 8:
                    this.buttonProgramCam8.ImageIndex = 2;
                    break;
                case 9:
                    this.buttonProgramCam9.ImageIndex = 2;
                    break;
                case 10:
                    this.buttonProgramCam10.ImageIndex = 2;
                    break;
            }
        }
        private void OnSwitcherMixEffectBlockPropertyIdInTransition(SwitcherMixEffectBlockCallback_v7_5 s, SwitcherMixEffectBlockEventArgs_v7_5 a)
        {
            int inTransition = s.GetFlag(BMDSwitcherAPI._BMDSwitcherMixEffectBlockPropertyId_v7_5.bmdSwitcherMixEffectBlockPropertyIdInTransition_v7_5);

            switch (s.GetInt(BMDSwitcherAPI._BMDSwitcherMixEffectBlockPropertyId_v7_5.bmdSwitcherMixEffectBlockPropertyIdPreviewInput_v7_5))
            {
                case 1:
                    buttonPreviewCam1.ImageIndex = inTransition == 0 ? 1 : 2;
                    break;
                case 2:
                    buttonPreviewCam2.ImageIndex = inTransition == 0 ? 1 : 2;
                    break;
                case 3:
                    buttonPreviewCam3.ImageIndex = inTransition == 0 ? 1 : 2;
                    break;
                case 4:
                    buttonPreviewCam4.ImageIndex = inTransition == 0 ? 1 : 2;
                    break;
                case 5:
                    buttonPreviewCam5.ImageIndex = inTransition == 0 ? 1 : 2;
                    break;
                case 6:
                    buttonPreviewCam6.ImageIndex = inTransition == 0 ? 1 : 2;
                    break;
                case 7:
                    buttonPreviewCam7.ImageIndex = inTransition == 0 ? 1 : 2;
                    break;
                case 8:
                    buttonPreviewCam8.ImageIndex = inTransition == 0 ? 1 : 2;
                    break;
            }

            buttonTransitionStyleAuto.ImageIndex = inTransition == 0 ? 0 : 2;

            if (inTransition == 0)
            {
                if (m_currentTransitionReachedHalfway)
                {
                    m_moveSliderDownwards = !m_moveSliderDownwards;
                    OnSwitcherMixEffectBlockPropertyIdTransitionPosition(s, null);
                }
                m_currentTransitionReachedHalfway = false;
            }
        }
        private void OnSwitcherMixEffectBlockPropertyIdTransitionPosition(SwitcherMixEffectBlockCallback_v7_5 s, SwitcherMixEffectBlockEventArgs_v7_5 a)
        {
            double transitionPos = s.GetFloat(BMDSwitcherAPI._BMDSwitcherMixEffectBlockPropertyId_v7_5.bmdSwitcherMixEffectBlockPropertyIdTransitionPosition_v7_5);
            m_currentTransitionReachedHalfway = (transitionPos >= 0.50);
            if (m_moveSliderDownwards)
                trackBarTransitionPosTBar.Value = 100 - (int)(transitionPos * 100);
            else
                trackBarTransitionPosTBar.Value = (int)(transitionPos * 100);
        }

        private void OnSwitcherKeyEventTypeOnAirChanged(SwitcherKeyCallback s, SwitcherKeyEventArgs a)
        {
            if (sw.BMDSwitcherKey.Count > 0) buttonNextTransitionOnAirKEY1.ImageIndex = sw.BMDSwitcherKey[0].OnAir == 0 ? 0 : 2;
            if (sw.BMDSwitcherKey.Count > 1) buttonNextTransitionOnAirKEY2.ImageIndex = sw.BMDSwitcherKey[1].OnAir == 0 ? 0 : 2;
            if (sw.BMDSwitcherKey.Count > 2) buttonNextTransitionOnAirKEY3.ImageIndex = sw.BMDSwitcherKey[2].OnAir == 0 ? 0 : 2;
            if (sw.BMDSwitcherKey.Count > 3) buttonNextTransitionOnAirKEY4.ImageIndex = sw.BMDSwitcherKey[3].OnAir == 0 ? 0 : 2;
        }

        private void OnSwitcherDownstreamKeyEventTypeTieChanged(SwitcherDownstreamKeyCallback s, SwitcherDownstreamKeyEventArgs a)
        {
            buttonDSK1Tie.ImageIndex = sw.BMDSwitcherDownstreamKey[0].Tie == 0 ? 0 : 3;
            buttonDSK2Tie.ImageIndex = sw.BMDSwitcherDownstreamKey[1].Tie == 0 ? 0 : 3;
        }
        private void OnSwitcherDownstreamKeyEventTypeOnAirChanged(SwitcherDownstreamKeyCallback s, SwitcherDownstreamKeyEventArgs a)
        {
            buttonDSK1OnAir.ImageIndex = sw.BMDSwitcherDownstreamKey[0].OnAir == 0 ? 0 : 2;
            buttonDSK2OnAir.ImageIndex = sw.BMDSwitcherDownstreamKey[1].OnAir == 0 ? 0 : 2;
        }
        private void OnSwitcherDownstreamKeyEventTypeIsAutoTransitioningChanged(SwitcherDownstreamKeyCallback s, SwitcherDownstreamKeyEventArgs a)
        {
            buttonDSK1Auto.ImageIndex = sw.BMDSwitcherDownstreamKey[0].IsAutoTransitioning == 0 ? 0 : 2;
            buttonDSK2Auto.ImageIndex = sw.BMDSwitcherDownstreamKey[1].IsAutoTransitioning == 0 ? 0 : 2;
        }
        private void OnSwitcherDownstreamKeyEventTypeFramesRemainingChanged(SwitcherDownstreamKeyCallback s, SwitcherDownstreamKeyEventArgs a)
        {
            textBoxDSK1Rate.Text = String.Format("{0}", sw.BMDSwitcherDownstreamKey[0].FramesRemaining);
            textBoxDSK2Rate.Text = String.Format("{0}", sw.BMDSwitcherDownstreamKey[1].FramesRemaining);
        }
        #endregion

        public ATEM_Switcher_Form()
        {
            InitializeComponent();
        }
        private void ATEM_Switcher_Form_Load(object sender, EventArgs e)
        {

        }

        #region MIDI Controllers
        private void OnEncoder1(object s, EventArgs a)
        {
            if (ActivePreset == 1)
            {
                trackBarAudioBalance1.Value = midi_BCF2000.Encoder1;
                TrackBarAudioBalance1_Scroll(this, null);
            }
            else
            {
                trackBarAudioBalance7.Value = midi_BCF2000.Encoder1;
                TrackBarAudioBalance7_Scroll(this, null);
            }
        }
        private void OnEncoder2(object s, EventArgs a)
        {
            if (ActivePreset == 1)
            {
                trackBarAudioBalance2.Value = midi_BCF2000.Encoder2;
                TrackBarAudioBalance2_Scroll(this, null);
            }
            else
            {
                trackBarAudioBalance8.Value = midi_BCF2000.Encoder2;
                TrackBarAudioBalance8_Scroll(this, null);
            }
        }
        private void OnEncoder3(object s, EventArgs a)
        {
            if (ActivePreset == 1)
            {
                trackBarAudioBalance3.Value = midi_BCF2000.Encoder3;
                TrackBarAudioBalance3_Scroll(this, null);
            }
            else
            {
                trackBarAudioBalance9.Value = midi_BCF2000.Encoder3;
                TrackBarAudioBalance9_Scroll(this, null);
            }
        }
        private void OnEncoder4(object s, EventArgs a)
        {
            if (ActivePreset == 1)
            {
                trackBarAudioBalance4.Value = midi_BCF2000.Encoder4;
                TrackBarAudioBalance4_Scroll(this, null);
            }
            else
            {
                trackBarAudioBalance10.Value = midi_BCF2000.Encoder4;
                TrackBarAudioBalance10_Scroll(this, null);
            }
        }
        private void OnEncoder5(object s, EventArgs a)
        {
            if (ActivePreset == 1)
            {
                trackBarAudioBalance5.Value = midi_BCF2000.Encoder5;
                TrackBarAudioBalance5_Scroll(this, null);
            }
            else
            {
                trackBarAudioBalanceMP1.Value = midi_BCF2000.Encoder3;
                TrackBarAudioBalanceMP1_Scroll(this, null);
            }
        }
        private void OnEncoder6(object s, EventArgs a)
        {
            if (ActivePreset == 1)
            {
                trackBarAudioBalance6.Value = midi_BCF2000.Encoder6;
                TrackBarAudioBalance6_Scroll(this, null);
            }
            else
            {
                trackBarAudioBalanceMP2.Value = midi_BCF2000.Encoder4;
                TrackBarAudioBalanceMP2_Scroll(this, null);
            }
        }
        private void OnEncoder7(object s, EventArgs a)
        {
            trackBarAudioBalanceEXT.Value = midi_BCF2000.Encoder7;
            TrackBarAudioBalanceEXT_Scroll(this, null);
        }
        private void OnEncoder8(object s, EventArgs a)
        {
            trackBarAudioBalanceMaster.Value = midi_BCF2000.Encoder8;
            TrackBarAudioBalanceMaster_Scroll(this, null);
        }

        private void OnFader1(object s, EventArgs a)
        {
            if (ActivePreset == 1)
            {
                trackBarAudioGain1.Value = midi_BCF2000.Fader1;
                TrackBarAudioGain1_Scroll(this, null);
            }
            else
            {
                trackBarAudioGain7.Value = midi_BCF2000.Fader1;
                TrackBarAudioGain7_Scroll(this, null);
            }
        }
        private void OnFader2(object s, EventArgs a)
        {
            if (ActivePreset == 1)
            {
                trackBarAudioGain2.Value = midi_BCF2000.Fader2;
                TrackBarAudioGain2_Scroll(this, null);
            }
            else
            {
                trackBarAudioGain8.Value = midi_BCF2000.Fader2;
                TrackBarAudioGain8_Scroll(this, null);
            }
        }
        private void OnFader3(object s, EventArgs a)
        {
            if (ActivePreset == 1)
            {
                trackBarAudioGain3.Value = midi_BCF2000.Fader3;
                TrackBarAudioGain3_Scroll(this, null);
            }
            else
            {
                if (sw.BMDSwitcherInfo.TotalSwitcherPortTypeExternal > 8)
                {
                    trackBarAudioGain9.Value = midi_BCF2000.Fader3;
                    TrackBarAudioGain9_Scroll(this, null);
                }
            }
        }
        private void OnFader4(object s, EventArgs a)
        {
            if (ActivePreset == 1)
            {
                trackBarAudioGain4.Value = midi_BCF2000.Fader4;
                TrackBarAudioGain4_Scroll(this, null);
            }
            else
            {
                if (sw.BMDSwitcherInfo.TotalSwitcherPortTypeExternal > 8)
                {
                    trackBarAudioGain10.Value = midi_BCF2000.Fader4;
                    TrackBarAudioGain10_Scroll(this, null);
                }
            }
        }
        private void OnFader5(object s, EventArgs a)
        {
            if (ActivePreset == 1)
            {
                trackBarAudioGain5.Value = midi_BCF2000.Fader5;
                TrackBarAudioGain5_Scroll(this, null);
            }
            else
            {
                trackBarAudioGainMP1.Value = midi_BCF2000.Fader5;
                TrackBarAudioGainMP1_Scroll(this, null);
            }
        }
        private void OnFader6(object s, EventArgs a)
        {
            if (ActivePreset == 1)
            {
                trackBarAudioGain6.Value = midi_BCF2000.Fader6;
                TrackBarAudioGain6_Scroll(this, null);
            }
            else
            {
                trackBarAudioGainMP2.Value = midi_BCF2000.Fader6;
                TrackBarAudioGainMP2_Scroll(this, null);
            }
        }
        private void OnFader7(object s, EventArgs a)
        {
            trackBarAudioGainEXT.Value = midi_BCF2000.Fader7;
            TrackBarAudioGainEXT_Scroll(this, null);
        }
        private void OnFader8(object s, EventArgs a)
        {
            trackBarAudioGainMaster.Value = midi_BCF2000.Fader8;
            TrackBarAudioGainMaster_Scroll(this, null);
        }
        #endregion

        #region Switcher Buttons
        #region Program Buttons
        private void ButtonProgramCam1_Click(object sender, EventArgs e)
        {
            sw.Command("ProgramInput|1");
        }
        private void ButtonProgramCam2_Click(object sender, EventArgs e)
        {
            sw.Command("ProgramInput|2");
        }
        private void ButtonProgramCam3_Click(object sender, EventArgs e)
        {
            sw.Command("ProgramInput|3");
        }
        private void ButtonProgramCam4_Click(object sender, EventArgs e)
        {
            sw.Command("ProgramInput|4");
        }
        private void ButtonProgramCam5_Click(object sender, EventArgs e)
        {
            sw.Command("ProgramInput|5"); ;
        }
        private void ButtonProgramCam6_Click(object sender, EventArgs e)
        {
            sw.Command("ProgramInput|6");
        }
        private void ButtonProgramCam7_Click(object sender, EventArgs e)
        {
            sw.Command("ProgramInput|7");
        }
        private void ButtonProgramCam8_Click(object sender, EventArgs e)
        {
            sw.Command("ProgramInput|8");
        }
        private void ButtonProgramCam9_Click(object sender, EventArgs e)
        {
        }
        private void ButtonProgramCam10_Click(object sender, EventArgs e)
        {
        }
        private void ButtonProgramBlk_Click(object sender, EventArgs e)
        {
        }
        private void ButtonProgramBars_Click(object sender, EventArgs e)
        {

        }
        private void ButtonProgramCol1_Click(object sender, EventArgs e)
        {

        }
        private void ButtonProgramCol2_Click(object sender, EventArgs e)
        {

        }
        private void ButtonProgramMP1_Click(object sender, EventArgs e)
        {

        }
        private void ButtonProgramMP2_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region Preview Buttons
        private void ButtonPreviewCam1_Click(object sender, EventArgs e)
        {
            sw.Command("PreviewInput|1");
        }
        private void ButtonPreviewCam2_Click(object sender, EventArgs e)
        {
            sw.Command("PreviewInput|2");
        }
        private void ButtonPreviewCam3_Click(object sender, EventArgs e)
        {
            sw.Command("PreviewInput|3");
        }
        private void ButtonPreviewCam4_Click(object sender, EventArgs e)
        {
            sw.Command("PreviewInput|4");
        }
        private void ButtonPreviewCam5_Click(object sender, EventArgs e)
        {
            sw.Command("PreviewInput|5");
        }
        private void ButtonPreviewCam6_Click(object sender, EventArgs e)
        {
            sw.Command("PreviewInput|6");
        }
        private void ButtonPreviewCam7_Click(object sender, EventArgs e)
        {
            sw.Command("PreviewInput|7");
        }
        private void ButtonPreviewCam8_Click(object sender, EventArgs e)
        {
            sw.Command("PreviewInput|8");
        }
        private void ButtonPreviewCam9_Click(object sender, EventArgs e)
        {
        }
        private void ButtonPreviewCam10_Click(object sender, EventArgs e)
        {
        }
        private void ButtonPreviewBlk_Click(object sender, EventArgs e)
        {

        }
        private void ButtonPreviewBars_Click(object sender, EventArgs e)
        {

        }
        private void ButtonPreviewCol1_Click(object sender, EventArgs e)
        {

        }
        private void ButtonPreviewCol2_Click(object sender, EventArgs e)
        {

        }
        private void ButtonPreviewMP1_Click(object sender, EventArgs e)
        {

        }
        private void ButtonPreviewMP2_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region Next Transition Buttons
        private void ButtonNextTransitionBKGD_Click(object sender, EventArgs e)
        {
            sw.Command("NextTransitionSelection|BKGD");
        }
        private void ButtonNextTransitionOnAirKEY1_Click(object sender, EventArgs e)
        {
            sw.Command("KeyOnAir|1");
        }
        private void ButtonNextTransitionOnAirKEY2_Click(object sender, EventArgs e)
        {
            sw.Command("KeyOnAir|2");
        }
        private void ButtonNextTransitionOnAirKEY3_Click(object sender, EventArgs e)
        {
            sw.Command("KeyOnAir|3");
        }
        private void ButtonNextTransitionOnAirKEY4_Click(object sender, EventArgs e)
        {
            sw.Command("KeyOnAir|4");
        }
        private void ButtonNextTransitionKEY1_Click(object sender, EventArgs e)
        {
            sw.Command("NextTransitionSelection|1");
        }
        private void ButtonNextTransitionKEY2_Click(object sender, EventArgs e)
        {
            sw.Command("NextTransitionSelection|2");
        }
        private void ButtonNextTransitionKEY3_Click(object sender, EventArgs e)
        {
            sw.Command("NextTransitionSelection|3");
        }
        private void ButtonNextTransitionKEY4_Click(object sender, EventArgs e)
        {
            sw.Command("NextTransitionSelection|4");
        }
        #endregion

        #region Transition Style Buttons
        private void ButtonTransitionStyleMix_Click(object sender, EventArgs e)
        {
            sw.Command("NextTransitionStyle|Mix");
        }
        private void ButtonTransitionStyleDip_Click(object sender, EventArgs e)
        {
            sw.Command("NextTransitionStyle|Dip");
        }
        private void ButtonTransitionStyleWipe_Click(object sender, EventArgs e)
        {
            sw.Command("NextTransitionStyle|Wipe");
        }
        private void ButtonTransitionStyleStinger_Click(object sender, EventArgs e)
        {
            sw.Command("NextTransitionStyle|Stinger");
        }
        private void ButtonTransitionStyleDVE_Click(object sender, EventArgs e)
        {
            sw.Command("NextTransitionStyle|DVE");
        }
        private void ButtonTransitionStylePrevTrans_Click(object sender, EventArgs e)
        {
            sw.Command("PreviewTransition");
        }
        private void ButtonTransitionStyleCut_Click(object sender, EventArgs e)
        {
            sw.Command("Perform|Cut");
        }
        private void ButtonTransitionStyleAuto_Click(object sender, EventArgs e)
        {
            sw.Command("Perform|Autotransition");
        }
        #endregion

        #region DSK Buttons
        #region DSK1 Buttons
        private void ButtonDSK1Tie_Click(object sender, EventArgs e)
        {
            sw.Command("DownstreamKey|1|Tie");
        }
        private void ButtonDSK1OnAir_Click(object sender, EventArgs e)
        {
            sw.Command("DownstreamKey|1|OnAir");
        }
        private void ButtonDSK1Auto_Click(object sender, EventArgs e)
        {
            sw.Command("DownstreamKey|1|PerformAutoTransition");
        }
        #endregion

        #region DSK2 Buttons
        private void ButtonDSK2Tie_Click(object sender, EventArgs e)
        {
            sw.Command("DownstreamKey|2|Tie");
        }
        private void ButtonDSK2OnAir_Click(object sender, EventArgs e)
        {
            sw.Command("DownstreamKey|2|OnAir");
        }
        private void ButtonDSK2Auto_Click(object sender, EventArgs e)
        {
            sw.Command("DownstreamKey|2|PerformAutoTransition");
        }
        #endregion
        #endregion

        #region Fade To Black Button
        private void ButtonFadeToBlack_Click(object sender, EventArgs e)
        {
            sw.Command("Perform|FadeToBlack");
        }
        #endregion

        #region T-Bar Control
        private bool m_moveSliderDownwards = false;
        private bool m_currentTransitionReachedHalfway = false;
        private void TrackBarTransitionPosTBar_Scroll(object sender, EventArgs e)
        {
            double position = trackBarTransitionPosTBar.Value / 100.0;
            if (m_moveSliderDownwards)
                position = (100 - trackBarTransitionPosTBar.Value) / 100.0;
            sw.BMDSwitcherMixEffectBlock_7_5[0].SetFloat(BMDSwitcherAPI._BMDSwitcherMixEffectBlockPropertyId_v7_5.bmdSwitcherMixEffectBlockPropertyIdTransitionPosition_v7_5, position);
        }
        #endregion

        #region Audio Mixer
        #region ON Buttons
        private void ButtonOnAudio1_Click(object sender, EventArgs e)
        {

        }
        private void ButtonOnAudio2_Click(object sender, EventArgs e)
        {

        }
        private void ButtonOnAudio3_Click(object sender, EventArgs e)
        {

        }
        private void ButtonOnAudio4_Click(object sender, EventArgs e)
        {

        }
        private void ButtonOnAudio5_Click(object sender, EventArgs e)
        {

        }
        private void ButtonOnAudio6_Click(object sender, EventArgs e)
        {

        }
        private void ButtonOnAudio7_Click(object sender, EventArgs e)
        {

        }
        private void ButtonOnAudio8_Click(object sender, EventArgs e)
        {

        }
        private void ButtonOnAudioMP1_Click(object sender, EventArgs e)
        {

        }
        private void ButtonOnAudioMP2_Click(object sender, EventArgs e)
        {

        }
        private void ButtonOnAudioEXT_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region AFV Buttons
        private void ButtonAudioAFVMaster_Click(object sender, EventArgs e)
        {

        }
        private void ButtonAudioAFV1_Click(object sender, EventArgs e)
        {

        }
        private void ButtonAudioAFV2_Click(object sender, EventArgs e)
        {

        }
        private void ButtonAudioAFV3_Click(object sender, EventArgs e)
        {

        }
        private void ButtonAudioAFV4_Click(object sender, EventArgs e)
        {

        }
        private void ButtonAudioAFV5_Click(object sender, EventArgs e)
        {

        }
        private void ButtonAudioAFV6_Click(object sender, EventArgs e)
        {

        }
        private void ButtonAudioAFV7_Click(object sender, EventArgs e)
        {

        }
        private void ButtonAudioAFV8_Click(object sender, EventArgs e)
        {

        }
        private void ButtonAudioAFVMP1_Click(object sender, EventArgs e)
        {

        }
        private void ButtonAudioAFVMP2_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region Mute Buttons
        private void ButtonAudioMute1_Click(object sender, EventArgs e)
        {

        }
        private void ButtonAudioMute2_Click(object sender, EventArgs e)
        {

        }
        private void ButtonAudioMute3_Click(object sender, EventArgs e)
        {

        }
        private void ButtonAudioMute4_Click(object sender, EventArgs e)
        {

        }
        private void ButtonAudioMute5_Click(object sender, EventArgs e)
        {

        }
        private void ButtonAudioMute6_Click(object sender, EventArgs e)
        {

        }
        private void ButtonAudioMute7_Click(object sender, EventArgs e)
        {

        }
        private void ButtonAudioMute8_Click(object sender, EventArgs e)
        {

        }
        private void ButtonAudioMuteMP1_Click(object sender, EventArgs e)
        {

        }
        private void ButtonAudioMuteMP2_Click(object sender, EventArgs e)
        {

        }
        private void ButtonAudioMuteEXT_Click(object sender, EventArgs e)
        {

        }
        private void ButtonAudioMuteMaster_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region Audio Gain TrackBars
        private void TrackBarAudioGain1_Scroll(object sender, EventArgs e)
        {
            ActivePreset = 1;
            UpdateFaders();
            sw.BMDSwitcherAudioInput[0].Gain = trackBarAudioGain1.Value * (66.48179729958136 / 127) - 60.4817648225089;
        }
        private void TrackBarAudioGain2_Scroll(object sender, EventArgs e)
        {
            ActivePreset = 1;
            UpdateFaders();
            sw.BMDSwitcherAudioInput[1].Gain = trackBarAudioGain2.Value * (66.48179729958136 / 127) - 60.4817648225089;
        }
        private void TrackBarAudioGain3_Scroll(object sender, EventArgs e)
        {
            ActivePreset = 1;
            UpdateFaders();
            sw.BMDSwitcherAudioInput[2].Gain = trackBarAudioGain3.Value * (66.48179729958136 / 127) - 60.4817648225089;
        }
        private void TrackBarAudioGain4_Scroll(object sender, EventArgs e)
        {
            ActivePreset = 1;
            UpdateFaders();
            sw.BMDSwitcherAudioInput[3].Gain = trackBarAudioGain4.Value * (66.48179729958136 / 127) - 60.4817648225089;
        }
        private void TrackBarAudioGain5_Scroll(object sender, EventArgs e)
        {
            ActivePreset = 1;
            UpdateFaders();
            sw.BMDSwitcherAudioInput[4].Gain = trackBarAudioGain5.Value * (66.48179729958136 / 127) - 60.4817648225089;
        }
        private void TrackBarAudioGain6_Scroll(object sender, EventArgs e)
        {
            ActivePreset = 1;
            UpdateFaders();
            sw.BMDSwitcherAudioInput[5].Gain = trackBarAudioGain6.Value * (66.48179729958136 / 127) - 60.4817648225089;
        }
        private void TrackBarAudioGain7_Scroll(object sender, EventArgs e)
        {
            ActivePreset = 2;
            UpdateFaders();
            sw.BMDSwitcherAudioInput[6].Gain = trackBarAudioGain7.Value * (66.48179729958136 / 127) - 60.4817648225089;
        }
        private void TrackBarAudioGain8_Scroll(object sender, EventArgs e)
        {
            ActivePreset = 2;
            UpdateFaders();
            sw.BMDSwitcherAudioInput[7].Gain = trackBarAudioGain8.Value * (66.48179729958136 / 127) - 60.4817648225089;
        }
        private void TrackBarAudioGain9_Scroll(object sender, EventArgs e)
        {
            ActivePreset = 2;
            UpdateFaders();
            sw.BMDSwitcherAudioInput[8].Gain = trackBarAudioGain9.Value * (66.48179729958136 / 127) - 60.4817648225089;
        }
        private void TrackBarAudioGain10_Scroll(object sender, EventArgs e)
        {
            ActivePreset = 2;
            UpdateFaders();
            sw.BMDSwitcherAudioInput[9].Gain = trackBarAudioGain10.Value * (66.48179729958136 / 127) - 60.4817648225089;
        }
        private void TrackBarAudioGainMP1_Scroll(object sender, EventArgs e)
        {
            ActivePreset = 2;
            UpdateFaders();
            sw.BMDSwitcherAudioInput[8].Gain = trackBarAudioGainMP1.Value * (66.48179729958136 / 127) - 60.4817648225089;
        }
        private void TrackBarAudioGainMP2_Scroll(object sender, EventArgs e)
        {
            ActivePreset = 2;
            UpdateFaders();
            sw.BMDSwitcherAudioInput[9].Gain = trackBarAudioGainMP2.Value * (66.48179729958136 / 127) - 60.4817648225089;
        }
        private void TrackBarAudioGainEXT_Scroll(object sender, EventArgs e)
        {
            sw.BMDSwitcherAudioInput[sw.BMDSwitcherAudioInput.Count - 1].Gain = trackBarAudioGainEXT.Value * (66.48179729958136 / 127) - 60.4817648225089;
        }
        private void TrackBarAudioGainMaster_Scroll(object sender, EventArgs e)
        {
            sw.BMDSwitcherAudioMixer.ProgramOutGain = trackBarAudioGainMaster.Value * (66.48179729958136 / 127) - 60.4817648225089;
        }
        #endregion

        #region Audio Balance TrackBars
        private void TrackBarAudioBalance1_Scroll(object sender, EventArgs e)
        {
            ActivePreset = 1;
            UpdateFaders();
            sw.BMDSwitcherAudioInput[0].Balance = (100.0 / (127.0 / trackBarAudioBalance1.Value) - 50.0) / 50;
        }
        private void TrackBarAudioBalance2_Scroll(object sender, EventArgs e)
        {
            ActivePreset = 1;
            UpdateFaders();
            sw.BMDSwitcherAudioInput[1].Balance = (100.0 / (127.0 / trackBarAudioBalance2.Value) - 50.0) / 50;
        }
        private void TrackBarAudioBalance3_Scroll(object sender, EventArgs e)
        {
            ActivePreset = 1;
            UpdateFaders();
            sw.BMDSwitcherAudioInput[2].Balance = (100.0 / (127.0 / trackBarAudioBalance3.Value) - 50.0) / 50;
        }
        private void TrackBarAudioBalance4_Scroll(object sender, EventArgs e)
        {
            ActivePreset = 1;
            UpdateFaders();
            sw.BMDSwitcherAudioInput[3].Balance = (100.0 / (127.0 / trackBarAudioBalance4.Value) - 50.0) / 50;
        }
        private void TrackBarAudioBalance5_Scroll(object sender, EventArgs e)
        {
            ActivePreset = 1;
            UpdateFaders();
            sw.BMDSwitcherAudioInput[4].Balance = (100.0 / (127.0 / trackBarAudioBalance5.Value) - 50.0) / 50;
        }
        private void TrackBarAudioBalance6_Scroll(object sender, EventArgs e)
        {
            ActivePreset = 1;
            UpdateFaders();
            sw.BMDSwitcherAudioInput[5].Balance = (100.0 / (127.0 / trackBarAudioBalance6.Value) - 50.0) / 50;
        }
        private void TrackBarAudioBalance7_Scroll(object sender, EventArgs e)
        {
            ActivePreset = 2;
            UpdateFaders();
            sw.BMDSwitcherAudioInput[6].Balance = (100.0 / (127.0 / trackBarAudioBalance7.Value) - 50.0) / 50;
        }
        private void TrackBarAudioBalance8_Scroll(object sender, EventArgs e)
        {
            ActivePreset = 2;
            UpdateFaders();
            sw.BMDSwitcherAudioInput[7].Balance = (100.0 / (127.0 / trackBarAudioBalance8.Value) - 50.0) / 50;
        }
        private void TrackBarAudioBalance9_Scroll(object sender, EventArgs e)
        {
            ActivePreset = 2;
            UpdateFaders();
            sw.BMDSwitcherAudioInput[8].Balance = (100.0 / (127.0 / trackBarAudioBalance9.Value) - 50.0) / 50;
        }
        private void TrackBarAudioBalance10_Scroll(object sender, EventArgs e)
        {
            ActivePreset = 2;
            UpdateFaders();
            sw.BMDSwitcherAudioInput[9].Balance = (100.0 / (127.0 / trackBarAudioBalance10.Value) - 50.0) / 50;
        }
        private void TrackBarAudioBalanceMP1_Scroll(object sender, EventArgs e)
        {
            ActivePreset = 2;
            UpdateFaders();
            sw.BMDSwitcherAudioInput[8].Balance = (100.0 / (127.0 / trackBarAudioBalanceMP1.Value) - 50.0) / 50;
        }
        private void TrackBarAudioBalanceMP2_Scroll(object sender, EventArgs e)
        {
            ActivePreset = 2;
            UpdateFaders();
            sw.BMDSwitcherAudioInput[9].Balance = (100.0 / (127.0 / trackBarAudioBalanceMP2.Value) - 50.0) / 50;
        }
        private void TrackBarAudioBalanceEXT_Scroll(object sender, EventArgs e)
        {
            sw.BMDSwitcherAudioInput[sw.BMDSwitcherAudioInput.Count - 1].Balance = (100.0 / (127.0 / trackBarAudioBalanceEXT.Value) - 50.0) / 50;
        }
        private void TrackBarAudioBalanceMaster_Scroll(object sender, EventArgs e)
        {
            sw.BMDSwitcherAudioMixer.ProgramOutBalance = (100.0 / (127.0 / trackBarAudioBalanceMaster.Value) - 50.0) / 50;
        }
        #endregion
        #endregion

        #endregion

        #region X-Keys Buttons
        private void OnXKeysSwitch(object s, XKeys_EventArgs a)
        {
            XKeySwitch.Checked = a.Switch;
        }
        private void OnXKeysButton(object s, XKeys_EventArgs a)
        {
            buttonXkeys1.ImageIndex = (a.ButtonNr & (long)X16Buttons.X16Button1) > 0 ? 2 : 0;
            buttonXkeys2.ImageIndex = (a.ButtonNr & (long)X16Buttons.X16Button2) > 0 ? 2 : 0;
            buttonXkeys3.ImageIndex = (a.ButtonNr & (long)X16Buttons.X16Button3) > 0 ? 2 : 0;
            buttonXkeys4.ImageIndex = (a.ButtonNr & (long)X16Buttons.X16Button4) > 0 ? 2 : 0;
            buttonXkeys5.ImageIndex = (a.ButtonNr & (long)X16Buttons.X16Button5) > 0 ? 2 : 0;
            buttonXkeys6.ImageIndex = (a.ButtonNr & (long)X16Buttons.X16Button6) > 0 ? 2 : 0;
            buttonXkeys7.ImageIndex = (a.ButtonNr & (long)X16Buttons.X16Button7) > 0 ? 2 : 0;
            buttonXkeys8.ImageIndex = (a.ButtonNr & (long)X16Buttons.X16Button8) > 0 ? 2 : 0;
            buttonXkeys9.ImageIndex = (a.ButtonNr & (long)X16Buttons.X16Button9) > 0 ? 2 : 0;
            buttonXkeys10.ImageIndex = (a.ButtonNr & (long)X16Buttons.X16Button10) > 0 ? 2 : 0;
            buttonXkeys11.ImageIndex = (a.ButtonNr & (long)X16Buttons.X16Button11) > 0 ? 2 : 0;
            buttonXkeys12.ImageIndex = (a.ButtonNr & (long)X16Buttons.X16Button12) > 0 ? 2 : 0;
            buttonXkeys13.ImageIndex = (a.ButtonNr & (long)X16Buttons.X16Button13) > 0 ? 2 : 0;
            buttonXkeys14.ImageIndex = (a.ButtonNr & (long)X16Buttons.X16Button14) > 0 ? 2 : 0;
            buttonXkeys15.ImageIndex = (a.ButtonNr & (long)X16Buttons.X16Button15) > 0 ? 2 : 0;
            buttonXkeys16.ImageIndex = (a.ButtonNr & (long)X16Buttons.X16Button16) > 0 ? 2 : 0;

            switch (a.ButtonNr)
            {
                case (long)X16Buttons.X16Button1:
                    break;
                case (long)X16Buttons.X16Button2:
                    break;
                case (long)X16Buttons.X16Button3:
                    break;
                case (long)X16Buttons.X16Button4:
                    break;

                #region Aux 1
                case (long)X16Buttons.X16Button5 + (long)X16Buttons.X16Button2 + (long)X16Buttons.X16Button4:
                    sw.Command("InputAux|1|0");
                    break;
                case (long)X16Buttons.X16Button6 + (long)X16Buttons.X16Button2 + (long)X16Buttons.X16Button4:
                    sw.Command("InputAux|1|1");
                    break;
                case (long)X16Buttons.X16Button7 + (long)X16Buttons.X16Button2 + (long)X16Buttons.X16Button4:
                    sw.Command("InputAux|1|2");
                    break;
                case (long)X16Buttons.X16Button8 + (long)X16Buttons.X16Button2 + (long)X16Buttons.X16Button4:
                    sw.Command("InputAux|1|3");
                    break;
                case (long)X16Buttons.X16Button9 + (long)X16Buttons.X16Button2 + (long)X16Buttons.X16Button4:
                    sw.Command("InputAux|1|4");
                    break;
                case (long)X16Buttons.X16Button10 + (long)X16Buttons.X16Button2 + (long)X16Buttons.X16Button4:
                    sw.Command("InputAux|1|5");
                    break;
                case (long)X16Buttons.X16Button11 + (long)X16Buttons.X16Button2 + (long)X16Buttons.X16Button4:
                    sw.Command("InputAux|1|6");
                    break;
                case (long)X16Buttons.X16Button12 + (long)X16Buttons.X16Button2 + (long)X16Buttons.X16Button4:
                    sw.Command("InputAux|1|7");
                    break;
                case (long)X16Buttons.X16Button13 + (long)X16Buttons.X16Button2 + (long)X16Buttons.X16Button4:
                    sw.Command("InputAux|1|8");
                    break;
                case (long)X16Buttons.X16Button14 + (long)X16Buttons.X16Button2 + (long)X16Buttons.X16Button4:
                    sw.Command("InputAux|1|1000");
                    break;
                case (long)X16Buttons.X16Button15 + (long)X16Buttons.X16Button2 + (long)X16Buttons.X16Button4:
                    sw.Command("InputAux|1|10010");
                    break;
                case (long)X16Buttons.X16Button16 + (long)X16Buttons.X16Button2 + (long)X16Buttons.X16Button4:
                    sw.Command("InputAux|1|10011");
                    break;
                #endregion

                #region Aux 2
                case (long)X16Buttons.X16Button5 + (long)X16Buttons.X16Button2 + (long)X16Buttons.X16Button3:
                    sw.Command("InputAux|2|0");
                    break;
                case (long)X16Buttons.X16Button6 + (long)X16Buttons.X16Button2 + (long)X16Buttons.X16Button3:
                    sw.Command("InputAux|2|1");
                    break;
                case (long)X16Buttons.X16Button7 + (long)X16Buttons.X16Button2 + (long)X16Buttons.X16Button3:
                    sw.Command("InputAux|2|2");
                    break;
                case (long)X16Buttons.X16Button8 + (long)X16Buttons.X16Button2 + (long)X16Buttons.X16Button3:
                    sw.Command("InputAux|2|3");
                    break;
                case (long)X16Buttons.X16Button9 + (long)X16Buttons.X16Button2 + (long)X16Buttons.X16Button3:
                    sw.Command("InputAux|2|4");
                    break;
                case (long)X16Buttons.X16Button10 + (long)X16Buttons.X16Button2 + (long)X16Buttons.X16Button3:
                    sw.Command("InputAux|2|5");
                    break;
                case (long)X16Buttons.X16Button11 + (long)X16Buttons.X16Button2 + (long)X16Buttons.X16Button3:
                    sw.Command("InputAux|2|6");
                    break;
                case (long)X16Buttons.X16Button12 + (long)X16Buttons.X16Button2 + (long)X16Buttons.X16Button3:
                    sw.Command("InputAux|2|7");
                    break;
                case (long)X16Buttons.X16Button13 + (long)X16Buttons.X16Button2 + (long)X16Buttons.X16Button3:
                    sw.Command("InputAux|2|8");
                    break;
                case (long)X16Buttons.X16Button14 + (long)X16Buttons.X16Button2 + (long)X16Buttons.X16Button3:
                    sw.Command("InputAux|2|1000");
                    break;
                case (long)X16Buttons.X16Button15 + (long)X16Buttons.X16Button2 + (long)X16Buttons.X16Button3:
                    sw.Command("InputAux|2|10010");
                    break;
                case (long)X16Buttons.X16Button16 + (long)X16Buttons.X16Button2 + (long)X16Buttons.X16Button3:
                    sw.Command("InputAux|2|10011");
                    break;
                #endregion

                #region Aux 3
                case (long)X16Buttons.X16Button5 + (long)X16Buttons.X16Button2 + (long)X16Buttons.X16Button3 + (long)X16Buttons.X16Button4:
                    sw.Command("InputAux|3|0");
                    break;
                case (long)X16Buttons.X16Button6 + (long)X16Buttons.X16Button2 + (long)X16Buttons.X16Button3 + (long)X16Buttons.X16Button4:
                    sw.Command("InputAux|3|1");
                    break;
                case (long)X16Buttons.X16Button7 + (long)X16Buttons.X16Button2 + (long)X16Buttons.X16Button3 + (long)X16Buttons.X16Button4:
                    sw.Command("InputAux|3|2");
                    break;
                case (long)X16Buttons.X16Button8 + (long)X16Buttons.X16Button2 + (long)X16Buttons.X16Button3 + (long)X16Buttons.X16Button4:
                    sw.Command("InputAux|3|3");
                    break;
                case (long)X16Buttons.X16Button9 + (long)X16Buttons.X16Button2 + (long)X16Buttons.X16Button3 + (long)X16Buttons.X16Button4:
                    sw.Command("InputAux|3|4");
                    break;
                case (long)X16Buttons.X16Button10 + (long)X16Buttons.X16Button2 + (long)X16Buttons.X16Button3 + (long)X16Buttons.X16Button4:
                    sw.Command("InputAux|3|5");
                    break;
                case (long)X16Buttons.X16Button11 + (long)X16Buttons.X16Button2 + (long)X16Buttons.X16Button3 + (long)X16Buttons.X16Button4:
                    sw.Command("InputAux|3|6");
                    break;
                case (long)X16Buttons.X16Button12 + (long)X16Buttons.X16Button2 + (long)X16Buttons.X16Button3 + (long)X16Buttons.X16Button4:
                    sw.Command("InputAux|3|7");
                    break;
                case (long)X16Buttons.X16Button13 + (long)X16Buttons.X16Button2 + (long)X16Buttons.X16Button3 + (long)X16Buttons.X16Button4:
                    sw.Command("InputAux|3|8");
                    break;
                case (long)X16Buttons.X16Button14 + (long)X16Buttons.X16Button2 + (long)X16Buttons.X16Button3 + (long)X16Buttons.X16Button4:
                    sw.Command("InputAux|3|1000");
                    break;
                case (long)X16Buttons.X16Button15 + (long)X16Buttons.X16Button2 + (long)X16Buttons.X16Button3 + (long)X16Buttons.X16Button4:
                    sw.Command("InputAux|3|10010");
                    break;
                case (long)X16Buttons.X16Button16 + (long)X16Buttons.X16Button2 + (long)X16Buttons.X16Button3 + (long)X16Buttons.X16Button4:
                    sw.Command("InputAux|3|10011");
                    break;
                #endregion

                #region PreviewTransition
                case (long)X16Buttons.X16Button1 + (long)X16Buttons.X16Button16:
                    sw.Command("PreviewTransition");
                    break;
                #endregion

                #region DownstreamKey
                case (long)X16Buttons.X16Button6 + (long)X16Buttons.X16Button3:
                    sw.Command("DownstreamKey|1|Tie");
                    break;

                case (long)X16Buttons.X16Button7 + (long)X16Buttons.X16Button3:
                    sw.Command("DownstreamKey|2|Tie");
                    break;

                case (long)X16Buttons.X16Button6 + (long)X16Buttons.X16Button3 + (long)X16Buttons.X16Button1:
                    sw.Command("DownstreamKey|1|OnAir");
                    break;

                case (long)X16Buttons.X16Button7 + (long)X16Buttons.X16Button3 + (long)X16Buttons.X16Button1:
                    sw.Command("DownstreamKey|2|OnAir");
                    break;

                case (long)X16Buttons.X16Button6 + (long)X16Buttons.X16Button3 + (long)X16Buttons.X16Button1 + (long)X16Buttons.X16Button2:
                    sw.Command("DownstreamKey|1|performautotransition");
                    break;

                case (long)X16Buttons.X16Button7 + (long)X16Buttons.X16Button3 + (long)X16Buttons.X16Button1 + (long)X16Buttons.X16Button2:
                    sw.Command("DownstreamKey|2|performautotransition");
                    break;
                #endregion

                #region NextTransitionStyle
                case (long)X16Buttons.X16Button5 + (long)X16Buttons.X16Button4:
                    sw.Command("NextTransitionStyle|Mix");
                    break;

                case (long)X16Buttons.X16Button6 + (long)X16Buttons.X16Button4:
                    sw.Command("NextTransitionStyle|Dip");
                    break;

                case (long)X16Buttons.X16Button7 + (long)X16Buttons.X16Button4:
                    sw.Command("NextTransitionStyle|Wipe");
                    break;

                case (long)X16Buttons.X16Button8 + (long)X16Buttons.X16Button4:
                    sw.Command("NextTransitionStyle|Stinger");
                    break;

                case (long)X16Buttons.X16Button9 + (long)X16Buttons.X16Button4:
                    sw.Command("NextTransitionStyle|DVE");
                    break;
                #endregion

                #region NextTransitionSelection
                case (long)X16Buttons.X16Button5 + (long)X16Buttons.X16Button2:
                    sw.Command("NextTransitionSelection|BKGD");
                    break;

                case (long)X16Buttons.X16Button6 + (long)X16Buttons.X16Button2:
                    sw.Command("NextTransitionSelection|1");
                    break;

                case (long)X16Buttons.X16Button7 + (long)X16Buttons.X16Button2:
                    sw.Command("NextTransitionSelection|2");
                    break;

                case (long)X16Buttons.X16Button8 + (long)X16Buttons.X16Button2:
                    sw.Command("NextTransitionSelection|3");
                    break;

                case (long)X16Buttons.X16Button9 + (long)X16Buttons.X16Button2:
                    sw.Command("NextTransitionSelection|4");
                    break;
                #endregion

                #region KeyOnAir
                case (long)X16Buttons.X16Button6 + (long)X16Buttons.X16Button2 + (long)X16Buttons.X16Button1:
                    sw.Command("KeyOnAir|1");
                    break;

                case (long)X16Buttons.X16Button7 + (long)X16Buttons.X16Button2 + (long)X16Buttons.X16Button1:
                    sw.Command("KeyOnAir|2");
                    break;

                case (long)X16Buttons.X16Button8 + (long)X16Buttons.X16Button2 + (long)X16Buttons.X16Button1:
                    sw.Command("KeyOnAir|3");
                    break;

                case (long)X16Buttons.X16Button9 + (long)X16Buttons.X16Button2 + (long)X16Buttons.X16Button1:
                    sw.Command("KeyOnAir|4");
                    break;
                #endregion

                #region Preview Input
                case (long)X16Buttons.X16Button5:
                    sw.Command("PreviewInput|0");
                    break;
                case (long)X16Buttons.X16Button6:
                    sw.Command("PreviewInput|1");
                    break;
                case (long)X16Buttons.X16Button7:
                    sw.Command("PreviewInput|2");
                    break;
                case (long)X16Buttons.X16Button8:
                    sw.Command("PreviewInput|3");
                    break;
                case (long)X16Buttons.X16Button9:
                    sw.Command("PreviewInput|4");
                    break;
                case (long)X16Buttons.X16Button10:
                    sw.Command("PreviewInput|5");
                    break;
                case (long)X16Buttons.X16Button11:
                    sw.Command("PreviewInput|6");
                    break;
                case (long)X16Buttons.X16Button12:
                    sw.Command("PreviewInput|7");
                    break;
                case (long)X16Buttons.X16Button13:
                    sw.Command("PreviewInput|8");
                    break;
                #endregion

                #region Program Input
                case (long)X16Buttons.X16Button5 + (long)X16Buttons.X16Button1:
                    sw.Command("ProgramInput|0");
                    break;
                case (long)X16Buttons.X16Button6 + (long)X16Buttons.X16Button1:
                    sw.Command("ProgramInput|1");
                    break;
                case (long)X16Buttons.X16Button7 + (long)X16Buttons.X16Button1:
                    sw.Command("ProgramInput|2");
                    break;
                case (long)X16Buttons.X16Button8 + (long)X16Buttons.X16Button1:
                    sw.Command("ProgramInput|3");
                    break;
                case (long)X16Buttons.X16Button9 + (long)X16Buttons.X16Button1:
                    sw.Command("ProgramInput|4");
                    break;
                case (long)X16Buttons.X16Button10 + (long)X16Buttons.X16Button1:
                    sw.Command("ProgramInput|5");
                    break;
                case (long)X16Buttons.X16Button11 + (long)X16Buttons.X16Button1:
                    sw.Command("ProgramInput|6");
                    break;
                case (long)X16Buttons.X16Button12 + (long)X16Buttons.X16Button1:
                    sw.Command("ProgramInput|7");
                    break;
                case (long)X16Buttons.X16Button13 + (long)X16Buttons.X16Button1:
                    sw.Command("ProgramInput|8");
                    break;
                #endregion

                #region Preform Cut
                case (long)X16Buttons.X16Button14:
                    sw.Command("Perform|Cut");
                    break;
                #endregion

                #region Perform AutoTransition"
                case (long)X16Buttons.X16Button15:
                    sw.Command("Perform|AutoTransition");
                    break;
                #endregion

                #region Perform FadeToBlack
                case (long)X16Buttons.X16Button16:
                    sw.Command("perform|FadeToBlack");
                    break;
                    #endregion
            }
        }
        #endregion

        private void UpdateFaders()
        {
            if (ActivePreset == 1)
            {
                groupBoxPreset2.BackColor = Color.FromName("ControlDarkDark");
                groupBoxPreset1.BackColor = Color.DarkGray;
            }
            else
            {
                groupBoxPreset1.BackColor = Color.FromName("ControlDarkDark");
                groupBoxPreset2.BackColor = Color.DarkGray;
            }

        }
        private void GroupBoxPreset2_Enter(object sender, EventArgs e)
        {
            ActivePreset = 2;
            for (int i = 6; i < 10; i++)
            {
                OnSwitcherAudioInputEventTypeGainChanged(sw.BMDSwitcherAudioInput[i], null);
                OnSwitcherAudioInputEventTypeBalanceChanged(sw.BMDSwitcherAudioInput[i], null);
            }
            UpdateFaders();
        }
        private void GroupBoxPreset1_Enter(object sender, EventArgs e)
        {
            ActivePreset = 1;
            for (int i = 0; i < 6; i++)
            {
                OnSwitcherAudioInputEventTypeGainChanged(sw.BMDSwitcherAudioInput[i], null);
                OnSwitcherAudioInputEventTypeBalanceChanged(sw.BMDSwitcherAudioInput[i], null);
            }
            UpdateFaders();
        }

        private void CheckBoxUser1_CheckedChanged(object sender, EventArgs e)
        {
            midi_BCF2000.UserKey1 = checkBoxUser1.Checked == true ? 1 : 0;
            OnUserKey1(this, null);
        }
    }
}
