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

namespace ATEM_Switcher_Deluxe
{
    public enum SwitcherInputBus
    {
        Cam1 = 1101,
        Cam2 = 1102,
        Cam3 = 1103,
        Cam4 = 1104,
        Cam5 = 1105,
        Cam6 = 1106,
        Cam7 = 1107,
        Cam8 = 1108
    }

    public enum SwitcherOutputBus
    {
        Blk = 2101,
        Bars = 2201,
        Col1 = 2301,
        Col2 = 2302,
        Cfd1 = 2401,
        Cfd2 = 2402,
        Auxiliary1 = 2501,
        Auxiliary2 = 2502,
        Auxiliary3 = 2503,
        Program = 2601,
        Preview = 2602
    }

    public enum SwitcherMediaBus
    {
        MP1 = 3101,
        MP2 = 3102,
        MP1K = 3201,
        MP2K = 3202
    }

    public enum SwitcherPanelButtons
    {
        Cam1 = 1101,
        Cam2 = 1102,
        Cam3 = 1103,
        Cam4 = 1104,
        Cam5 = 1105,
        Cam6 = 1106,
        Cam7 = 1107,
        Cam8 = 1108,

        Blk = 2101,
        Bars = 2201,
        Col1 = 2301,
        Col2 = 2302,

        MP1 = 3101,
        MP2 = 3102,

        NextTransBKGD = 4101,
        NextTransOnAirKey1 = 4201,
        NextTransOnAirKey2 = 4202,
        NextTransOnAirKey3 = 4203,
        NextTransOnAirKey4 = 4204,
        NextTransKey1 = 4301,
        NextTransKey2 = 4302,
        NextTransKey3 = 4303,
        NextTransKey4 = 4304,

        TransStyleMIX = 5101,
        TransStyleDIP = 5102,
        TransStyleWIPE = 5103,
        TransStyleSTING = 5104,
        TransStyleDVE = 5105,
        TransStylePrevTrans = 5201,
        TransStyleCUT = 5301,
        TransStyleAUTO = 5302,

        DSK1 = 6101,
        DSK2 = 6102,
        DSK1OnAir = 6201,
        DSK2OnAir = 6201,
        DSK1Auto = 6301,
        DSK2Auto = 6302,

        FTB = 7101,

        TBAR = 8101,

        AudioMixON1 = 9101,
        AudioMixON2 = 9102,
        AudioMixON3 = 9103,
        AudioMixON4 = 9104,
        AudioMixON5 = 9105,
        AudioMixON6 = 9106,
        AudioMixON7 = 9107,
        AudioMixON8 = 9108,
        AudioMixONMP1 = 9201,
        AudioMixONMP2 = 9202,
        AudioMixONEXT = 9301,
        AudioMixONMaster = 9401,

        AudioMixMute1 = 9501,
        AudioMixMute2 = 9502,
        AudioMixMute3 = 9503,
        AudioMixMute4 = 9504,
        AudioMixMute5 = 9505,
        AudioMixMute6 = 9506,
        AudioMixMute7 = 9507,
        AudioMixMute8 = 9508,
        AudioMixMuteMP1 = 9601,
        AudioMixMuteMP2 = 9602,
        AudioMixMuteEXT = 9701,
        AudioMixMuteMaster = 9801,

        AudioMixSelect1 = 9901,
        AudioMixSelect2 = 9902,
        AudioMixSelect3 = 9903,
        AudioMixSelect4 = 9904,
        AudioMixSelect5 = 9905,
        AudioMixSelect6 = 9906,
        AudioMixSelect7 = 9907,
        AudioMixSelect8 = 9908,
        AudioMixSelectMP1 = 91001,
        AudioMixSelectMP2 = 91002,
        AudioMixSelectEXT = 91101,
        AudioMixSelectMaster = 91201,

        AudioMixGainTrackBar1 = 91301,
        AudioMixGainTrackBar2 = 91302,
        AudioMixGainTrackBar3 = 91303,
        AudioMixGainTrackBar4 = 91304,
        AudioMixGainTrackBar5 = 91305,
        AudioMixGainTrackBar6 = 91306,
        AudioMixGainTrackBar7 = 91307,
        AudioMixGainTrackBar8 = 91308,
        AudioMixGainTrackBarMP1 = 91401,
        AudioMixGainTrackBarMP2 = 91402,
        AudioMixGainTrackBarEXT = 91501,
        AudioMixGainTrackBarMaster = 91601,

        AudioMixBalance1 = 91701,
        AudioMixBalance2 = 91702,
        AudioMixBalance3 = 91703,
        AudioMixBalance4 = 91704,
        AudioMixBalance5 = 91705,
        AudioMixBalance6 = 91706,
        AudioMixBalance7 = 91707,
        AudioMixBalance8 = 91708,
        AudioMixBalanceMP1 = 91801,
        AudioMixBalanceMP2 = 91802,
        AudioMixBalanceEXT = 91901,
        AudioMixBalanceMaster = 92001
    }

    public enum SwitcherXKeysButtons
    {
        Button0 = 1101,
        Button1 = 1201,
        Button2 = 1202,
        Button3 = 1203,
        Button4 = 1204,
        Button5 = 1205,
        Button6 = 1206,
        Button7 = 1207,
        Button8 = 1208,
        Button9 = 1209,
        Button10 = 1210,
        Button11 = 1211,
        Button12 = 1212,
        Button13 = 1213,
        Button14 = 1214,
        Button15 = 1215,
        Button16 = 1216
    }
}