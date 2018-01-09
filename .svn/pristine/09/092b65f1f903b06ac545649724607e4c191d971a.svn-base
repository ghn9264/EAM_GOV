using System.Drawing;
using System.Text;
using ThoughtWorks.QRCode.Codec;
using ThoughtWorks.QRCode.Codec.Data;

namespace Eam.QrCode
{
    public class QrCode
    {
        /// <summary>
        /// 生成二维码
        /// </summary>
        /// <param name="enCodeString">二维码所要写入的信息</param>
        /// <param name="path">二维码图片的存储路径</param>
        public static void Create(string enCodeString, string path)
        {
            Bitmap bt = Create(enCodeString);
            bt.Save(path);
        }

        public static Bitmap Create(string enCodeString)
        { 
            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder { QRCodeScale = 20 };
            Bitmap bt = qrCodeEncoder.Encode(enCodeString, Encoding.UTF8);
            return bt;
        }

        /// <summary>
        /// 获取二维码的信息
        /// </summary>
        /// <param name="path">二维码图片的存储路径</param>
        /// <returns>二维码信息</returns>
        public static string DecodeMessage(string path)
        {
            QRCodeDecoder decoder = new QRCodeDecoder();
            Bitmap bm = new Bitmap(path);
            return decoder.decode(new QRCodeBitmapImage(bm), Encoding.UTF8);
        }
    }

}