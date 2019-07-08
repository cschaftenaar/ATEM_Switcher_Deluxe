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

namespace BMDSwitcherLib
{
    public enum SwitcherBusInputsValues
    {
        Black = 0,
        Camera1 = 1,
        Camera2 = 2,
        Camera3 = 3,
        Camera4 = 4,
        Camera5 = 5,
        Camera6 = 6,
        Camera7 = 7,
        Camera8 = 8,
        Camera9 = 9,
        Camera10 = 10,

        ColorBars = 1000,

        Color1 = 2001,
        Color2 = 2002,

        MediaPlayer1 = 3010,
        MediaPlayer1Key = 3011,
        MediaPlayer2 = 3020,
        MediaPlayer2Key = 3021,

        CleanFeed1 = 7001,
        CleanFeed2 = 7002,

        Auxiliary1 = 8001,
        Auxiliary2 = 8002,
        Auxiliary3 = 8003,

        Program = 10010,
        Preview = 10011
    }

    public enum SwitcherAudioInputsValues
    {
        Black = 0,
        Camera1 = 1,
        Camera2 = 2,
        Camera3 = 3,
        Camera4 = 4,
        Camera5 = 5,
        Camera6 = 6,
        Camera7 = 7,
        Camera8 = 8,
        Camera9 = 9,
        Camera10 = 10,

        ExternalXLR = 1001,

        MediaPlayer1 = 2001,
        MediaPlayer2 = 2002,
    }
}