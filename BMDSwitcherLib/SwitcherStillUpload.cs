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
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;

namespace BMDSwitcherLib
{
    public class SwitcherStillUpload
    {
        public Switcher switcher;
        public string filename;
        public int slot;
        public IBMDSwitcherStills stills;
        public IBMDSwitcherFrame frame;
        public byte[] numArray;
        public IBMDSwitcherLockCallback lockCallback;

        public SwitcherStillUpload(Switcher sw)
        {
            this.switcher = sw;
        }

        public void Upload(string fn, int s)
        {
            this.filename = fn;
            this.slot = s;
            this.stills = this.switcher.BMDSwitcherMediaPool.Stills;
        }
        public void Start()
        {
            frame = GetFrame();
        }
        public IBMDSwitcherFrame GetFrame()
        {
            IBMDSwitcherFrame variable;
            variable = switcher.BMDSwitcherMediaPool.CreateFrame(_BMDSwitcherPixelFormat.bmdSwitcherPixelFormat8BitARGB, (uint)1920, (uint)1080);
            variable.GetBytes(out IntPtr intPtr);
            string lower = Path.GetExtension(this.filename).ToLower();

            try
            {
                Bitmap bitmap = new Bitmap(this.filename);
                if ((bitmap.Width != (uint)1920 ? true : bitmap.Height != (uint)1080))
                {
                    string str = bitmap.Width.ToString();
                    int height = bitmap.Height;
                }
                byte[] b = new byte[bitmap.Width * bitmap.Height * 4];
                for (int i = 0; i < bitmap.Width * bitmap.Height; i++)
                {
                    Color pixel = this.GetPixel(bitmap, i);
                    int num = i * 4;
                    b[num] = pixel.B;
                    b[num + 1] = pixel.G;
                    b[num + 2] = pixel.R;
                    b[num + 3] = pixel.A;
                }
                numArray = b;
            }
            catch (Exception exception1)
            {
                Exception exception = exception1;
            }

            Marshal.Copy(numArray, 0, intPtr, (int)numArray.Length);
            return variable;
        }
        public Color GetPixel(Bitmap image, int index)
        {
            int num = index % image.Width;
            int num1 = (index - num) / image.Width;
            return image.GetPixel(num, num1);
        }
    }
}
