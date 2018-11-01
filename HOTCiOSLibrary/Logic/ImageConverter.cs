using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

using Foundation;
using UIKit;

namespace HOTCLibrary.Logic
{
    public class ImageConverter
    {
        public byte[] ConvertImageToBytes(UIImage userImage)
        {
            NSData imagedata = userImage.AsPNG();
            byte[] ByteArray = new byte[imagedata.Length];
            Marshal.Copy(imagedata.Bytes, ByteArray, 0, Convert.ToInt32(imagedata.Length));
            return ByteArray;
        }
    }
}