﻿using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;
using System.Text;
using ThoughtWorks.QRCode.Codec;

namespace Eam.QrCode
{
    public class QrCodeBuilder
    {
        /// <summary>
        /// 生成二维码
        /// </summary>
        /// <param name="info"></param>
        /// <param name="savePath"></param>
        /// <param name="settings">是由配置assetname assetcode brandtype storeplace user datetime 这几个字符串由-连接而成</param>
        public static void Build(QrCodeInfo info, string savePath,string settings,int titleX,int titleY,int codeX,int codeY,int fontSize,string LableName, string LableContact)
        {
            string _encode = info.ofsNum + "_" + info.Num;  // 系统码加动态码
            string _encode1 = info.Num;                     // 动态码
            string _encode2 = info.ofsNum + "_";            // 系统码
          
            //
            // 生成二维码
            //
            var qrCodeImg = Create(_encode,9);
            
            //
            // 生成整个条码
            //
            int scale = 2;

            // 创建一个bitmap对象
            using( Bitmap bmPhoto = new Bitmap(28 * 28* scale, 16 * 28 * scale))
	{
		 
	
           
            bmPhoto.SetResolution(qrCodeImg.HorizontalResolution, qrCodeImg.VerticalResolution);
            using(Graphics g = Graphics.FromImage(bmPhoto))
	{
		 
	
            // 创建一个画布
            

            // 设置画布参数提高画布的显示质量
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.TextRenderingHint = TextRenderingHint.SingleBitPerPixelGridFit;

            //插入的像素位置由偏移量控制。
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

            //像素偏移方式，像素在水平和垂直距离上均偏移若干个单位，以进行高速锯齿消除。
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;

            //也可以通过设置Graphics对不平平滑处理方式解决，代码如下： 
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            // 创建一个画笔
            Brush b = new SolidBrush(Color.Black);
         
            

            // 创建字体对象
            var titleFont = new Font("黑体", fontSize*2, FontStyle.Regular);      // 标题字体
            var detailFont = new Font("微软雅黑", 24*2, FontStyle.Regular); // 内容字体
            var logoFont = new Font("微软雅黑", 21*2, FontStyle.Regular); // 提示字体

            //
            // 资产编码字体
            //
            float _fontSize = (float)(24 * 1.65);
            var codeFont = new Font("微软雅黑", _fontSize, FontStyle.Regular);

            // 
            // 设置内容的位置
            // 
            int titlewidthX = 210 * scale;
            int startY = 70 * scale;
            int stepY = 44 * scale;
            int curY = startY;

            
             //在标签上写标题

            if (titleX > 0 || titleY > 0)
            {
                g.DrawString(LableName, titleFont, b, new Point(titleX, titleY));

            }
            else
            {
                g.DrawString(LableName, titleFont, b, new Point(200 * scale, 10 * scale));
            }
            //g.DrawString(info.Title, titleFont, b, new Point(titleX * scale, titleY * scale));
            
             //写资产名称

            if (settings.IndexOf("assetname", System.StringComparison.Ordinal) >= 0)
            {
                g.DrawString(string.Format("资产名称:{0}", info.Name), detailFont, b, new Point(titlewidthX, curY));
                curY += stepY;
            }
            if (settings.IndexOf("assetcode", System.StringComparison.Ordinal) >= 0)
            {
                g.DrawString(string.Format("资产编码:"), detailFont, b, new Point(titlewidthX, curY));
                g.DrawString(string.Format("{0}", info.Num), codeFont, b, new Point(titlewidthX + 280, curY));
                curY += stepY;
            }
            if (settings.IndexOf("brandtype", System.StringComparison.Ordinal) > 0)
            {
                g.DrawString(string.Format("品牌型号:{0}", info.ModelSpecification), detailFont, b, new Point(titlewidthX, curY));
                curY += stepY;
            }
            if (settings.IndexOf("storeplace", System.StringComparison.Ordinal) > 0)
            {
                g.DrawString(string.Format("存放地点:{0}", info.StorePlace), detailFont, b, new Point(titlewidthX, curY));
                curY += stepY;
            }
            if (settings.IndexOf("user", System.StringComparison.Ordinal) > 0)
            {
                g.DrawString(string.Format("使用人:{0}", info.UsePeople), detailFont, b, new Point(titlewidthX, curY));
                curY += stepY;
            }
            if (settings.IndexOf("department", System.StringComparison.Ordinal) > 0)
            {
                g.DrawString(string.Format("部门:{0}", info.de), detailFont, b, new Point(titlewidthX, curY));
                curY += stepY;
            }
            if (settings.IndexOf("datetime", System.StringComparison.Ordinal) > 0)
            {
                g.DrawString(string.Format("取得日期:{0}", info.DateTime), detailFont, b, new Point(titlewidthX, curY));
                curY += stepY;
            }

            //写标签上的提示内容
            g.DrawString(LableContact, logoFont, b, new Point(titlewidthX - 200, 668));
            
             //在标签画二维码
            //if (codex>0||codey>0)
            //{
            //    g.drawimage(qrcodeimg, new rectangle(codex, codey, qrcodeimg.width * scale, qrcodeimg.height * scale), 0, 0, qrcodeimg.width * scale, qrcodeimg.height * scale, graphicsunit.pixel);
            //}
            //else
            //{
            //    g.drawimage(qrcodeimg, new rectangle(5 * scale, 130 * scale, qrcodeimg.width * scale, qrcodeimg.height * scale), 0, 0, qrcodeimg.width * scale, qrcodeimg.height * scale, graphicsunit.pixel);
            //}

            g.DrawImage(qrCodeImg, new Rectangle(codeX *  scale, codeY * scale, qrCodeImg.Width * scale, qrCodeImg.Height * scale), 0, 0, qrCodeImg.Width * scale, qrCodeImg.Height * scale, GraphicsUnit.Pixel);
            
           
            // 释放画图资源


            // 将整个标签图片保存到指定位置
            if (File.Exists(savePath))
            {
                File.Delete(savePath);
                
           }
            bmPhoto.Save(savePath);    
                
            
           
           }
            }
        }

        private static Bitmap Create(string enCodeString,int size)
        {
            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder { QRCodeScale = size };
            Bitmap bt = qrCodeEncoder.Encode(enCodeString, Encoding.UTF8);
            return bt;
        }

        public sealed class QrCodeInfo
        {
            /// <summary>
            /// 标签标题
            /// </summary>
            public string Title { get; set; }
 

            /// <summary>
            /// 资产代码
            /// </summary>
            public string Num { get; set; }

            /// <summary>
            /// 动态编码
            /// </summary>
            public string ofsNum { get; set; }

            /// <summary>
            /// 资产名称
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// 存放地点
            /// </summary>
            public string StorePlace { get; set; }

            /// <summary>
            /// 型号规格
            /// </summary>
            public string ModelSpecification { get; set; }

            /// <summary>
            /// 分类代码
            /// </summary>
            public string CatCode { get; set; }

            /// <summary>
            /// 使用人
            /// </summary>
            public string UsePeople { get; set; }

            /// <summary>
            /// 使用部门
            /// </summary>
            public string de { get; set; }

            /// <summary>
            /// 取得日期
            /// </summary>
            public DateTime DateTime { get; set; }

        }

    }
}