using System;
using System.Collections.Generic;
using System.Threading;
using EAM.Data.Domain;
using EAM.Data.ImportAndExport.Import;
using EAM.Data.Services;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.HSSF.UserModel;
using NPOI.SS.Formula.Eval;
using NPOI.SS.Formula.Functions;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.POIFS.FileSystem;
using NPOI.HPSF;
using System.IO;
using NPOI.SS.Util;
//using System.Drawing;
using NPOI.HSSF.Util;
using System.Web;
using System.Net;
using System.Net.Mail;  

namespace EAM.Data.ImportAndExport.Export.ExportAssets
{
    public class ExportScrapMerge : ExportScrapMergeBase
    {
        public ExportScrapMerge(IScrapService scrapService, ScrapMerge scrapMerge)
            : base(scrapService, scrapMerge)
        {

        }

        public override AssetsTypes AssetsType
        {
            get { return AssetsTypes.Land; }
        }

        /// <summary>
        /// 创建表Title
        /// </summary>
        protected override void BuildTitle()
        {
            NextRowIndex++;
            NextRowIndex++;

        }

        /// <summary>
        /// 创建表头
        /// </summary>
        protected override void BuildHeader()
        {
            NextRowIndex++;
            NextRowIndex++;
        }

        /// <summary>
        /// 填充数据到表格
        /// </summary>
        protected override void FillBody()
        {
            if (null == _ScrapMerge)
                return;

            //
            // 填写填表人
            //
            TempRow = Sheet.GetRow(2);// CreateRow(2);
            TempRow.GetCell(5).SetCellValue(_ScrapMerge.FillPerson);

            //
            // 填写学校名称
            //
            TempRow = Sheet.GetRow(3);
            TempRow.GetCell(1).SetCellValue("测试学校");

            //
            // 填写信息化负责人和电话
            //
            TempRow = Sheet.GetRow(4);
            TempRow.GetCell(1).SetCellValue(_ScrapMerge.People);
            TempRow.GetCell(4).SetCellValue(_ScrapMerge.Tel);

            //
            // 填写报表详情
            //
            TempRow = Sheet.GetRow(5);
            var aaa = (DateTime.Now.Year.ToString() + "年度学校信息化设备报废明细表").ToString();
            TempRow.GetCell(0).SetCellValue(aaa);

            //
            // 填写名称及品牌行
            //
            TempRow = Sheet.GetRow(7);
            TempRow.GetCell(1).SetCellValue(_ScrapMerge.AssetsName + _ScrapMerge.ModelSpecification);
            TempRow.GetCell(2).SetCellValue(_ScrapMerge.Counts);
            TempRow.GetCell(3).SetCellValue(_ScrapMerge.GetsDate.ToShortDateString());
            TempRow.GetCell(4).SetCellValue(_ScrapMerge.UsedYears);
            TempRow.GetCell(5).SetCellValue(_ScrapMerge.DeadYears);

            //
            // 填写图片上方规格及品牌型号
            //
            TempRow = Sheet.GetRow(24);
            TempRow.GetCell(2).SetCellValue(_ScrapMerge.AssetsName + _ScrapMerge.ModelSpecification);

            //
            // 分辨四大类
            //
            switch (_ScrapMerge.type)
            {
                case 1:// 计算机
                {
                    //
                    // 填写CPU行
                    //
                    TempRow = Sheet.GetRow(9);
                    TempRow.GetCell(1).SetCellValue(_ScrapMerge.dcpu);
                    TempRow.GetCell(2).SetCellValue(_ScrapMerge.dzhuban);
                    TempRow.GetCell(3).SetCellValue(_ScrapMerge.dyingpan);
                    TempRow.GetCell(4).SetCellValue(_ScrapMerge.dneicun);
                    TempRow.GetCell(5).SetCellValue(_ScrapMerge.dxianka);
                    TempRow.GetCell(6).SetCellValue(_ScrapMerge.dguangqu);

                    //
                    // 填写CPU行
                    //
                    TempRow = Sheet.GetRow(11);
                    TempRow.GetCell(1).SetCellValue(_ScrapMerge.dxianshiqi);
                    TempRow.GetCell(2).SetCellValue(_ScrapMerge.dxianshiqichicun);
                    TempRow.GetCell(3).SetCellValue(_ScrapMerge.dxianshiqileixing);
                    TempRow.GetCell(4).SetCellValue(_ScrapMerge.djianpan);
                    TempRow.GetCell(5).SetCellValue(_ScrapMerge.dshubiao);
                    
                    //
                    // 时间行
                    //
                    TempRow = Sheet.GetRow(14);
                    var i = ("学校（公章）" + DateTime.Now.ToLongDateString().ToString()).ToString();
                    TempRow.GetCell(4).SetCellValue(i);

                    //插入计算机类图片2
                    //第一步：读取图片到byte数组   (方法二)
                    byte[] bytes1 = System.IO.File.ReadAllBytes(@_ScrapMerge.PhotoSrcD);


                    //第二步：将图片添加到workbook中  指定图片格式 返回图片所在workbook->Picture数组中的索引地址（从1开始）  
                    int pictureIdx1 = Workbook.AddPicture(bytes1, PictureType.JPEG);

                    //第三步：在sheet中创建画部  
                    IDrawing patriarch1 = Sheet.CreateDrawingPatriarch();

                    //第四步：设置锚点 （在起始单元格的X坐标0-1023，Y的坐标0-255，在终止单元格的X坐标0-1023，Y的坐标0-255，起始单元格行数，列数，终止单元格行数，列数）  
                    IClientAnchor anchor1 = patriarch1.CreateAnchor(0, 0, 0, 0, 1, 42, 6, 57);


                    //第五步：创建图片  
                    IPicture pict1 = patriarch1.CreatePicture(anchor1, pictureIdx1);
                    break;
                }
                case 2://服务器
                {
                    //
                    // 填写CPU行
                    //
                    TempRow = Sheet.GetRow(9);
                    TempRow.GetCell(1).SetCellValue(_ScrapMerge.fcpu);
                    TempRow.GetCell(2).SetCellValue(_ScrapMerge.fzhuban);
                    TempRow.GetCell(3).SetCellValue(_ScrapMerge.fyingpan);
                    TempRow.GetCell(4).SetCellValue(_ScrapMerge.fneicun);
                    TempRow.GetCell(5).SetCellValue(_ScrapMerge.fxianka);
                    TempRow.GetCell(6).SetCellValue(_ScrapMerge.fguangqu);

                    //
                    // 时间行
                    //
                    TempRow = Sheet.GetRow(12);
                    var i = ("学校（公章）" + DateTime.Now.ToLongDateString().ToString()).ToString();
                    TempRow.GetCell(4).SetCellValue(i);
                    break;
                }
                case 3:// 投影仪
                {
                    //
                    // 填写显示技术行
                    //
                    TempRow = Sheet.GetRow(9);
                    TempRow.GetCell(1).SetCellValue(_ScrapMerge.txianshijishu);
                    TempRow.GetCell(2).SetCellValue(_ScrapMerge.tzuigaoxianshifenbianlv);
                    TempRow.GetCell(3).SetCellValue(_ScrapMerge.tbiaochengguangliangdu);
                    TempRow.GetCell(4).SetCellValue(_ScrapMerge.tbiaochengduibidu);

                    //
                    // 时间行
                    //
                    TempRow = Sheet.GetRow(12);
                    var i = ("学校（公章）" + DateTime.Now.ToLongDateString().ToString()).ToString();
                    TempRow.GetCell(4).SetCellValue(i);
                    break;
                }
                case 4:// 其他
                {
                    //
                    // 填写主要参数行
                    //
                    TempRow = Sheet.GetRow(9);
                    TempRow.GetCell(1).SetCellValue(_ScrapMerge.qzhuyaocanshu1);
                    TempRow.GetCell(2).SetCellValue(_ScrapMerge.qzhuyaocanshu2);
                    TempRow.GetCell(3).SetCellValue(_ScrapMerge.qzhuyaocanshu3);
                    TempRow.GetCell(4).SetCellValue(_ScrapMerge.qzhuyaocanshu4);
                    TempRow.GetCell(5).SetCellValue(_ScrapMerge.qzhuyaocanshu5);
                    TempRow.GetCell(6).SetCellValue(_ScrapMerge.qzhuyaocanshu6);

                    //
                    // 时间行
                    //
                    TempRow = Sheet.GetRow(12);
                    var i = ("学校（公章）" + DateTime.Now.ToLongDateString().ToString()).ToString();
                    TempRow.GetCell(4).SetCellValue(i);
                    break;
                }
                default:
                    break;
            }

            //*****************************************************************
            //说明：插入图片  

            //1.创建EXCEL中的Workbook           
            //IWorkbook myworkbook = new HSSFWorkbook();                    //已有（Workbook）

            //2.创建Workbook中的Sheet          
            //ISheet mysheet = myworkbook.CreateSheet("sheet1");            //已有（Sheet）

            //第一步：读取图片到byte数组   (方法一)
            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(_ScrapMerge.PhotoSrc);     // 有异常，图片路径格式不对

            //byte[] bytes;
            //using (Stream stream = request.GetResponse().GetResponseStream())
            //{
            //    using (MemoryStream mstream = new MemoryStream())
            //    {
            //        int count = 0;
            //        byte[] buffer = new byte[1024];
            //        int readNum = 0;
            //        while ((readNum = stream.Read(buffer, 0, 1024)) > 0)
            //        {
            //            count = count + readNum;
            //            mstream.Write(buffer, 0, 1024);
            //        }
            //        mstream.Position = 0;
            //        using (BinaryReader br = new BinaryReader(mstream))
            //        {

            //            bytes = br.ReadBytes(count);
            //        }
            //    }
            //}

            //第一步：读取图片到byte数组   (方法二)
            byte[] bytes = System.IO.File.ReadAllBytes(@_ScrapMerge.PhotoSrc);


            //第二步：将图片添加到workbook中  指定图片格式 返回图片所在workbook->Picture数组中的索引地址（从1开始）  
            int pictureIdx = Workbook.AddPicture(bytes, PictureType.JPEG);

            //第三步：在sheet中创建画部  
            IDrawing patriarch = Sheet.CreateDrawingPatriarch();

            //第四步：设置锚点 （在起始单元格的X坐标0-1023，Y的坐标0-255，在终止单元格的X坐标0-1023，Y的坐标0-255，起始单元格行数，列数，终止单元格行数，列数）  
            IClientAnchor anchor = patriarch.CreateAnchor(0, 0, 0, 0, 1, 26, 6, 42);


            //第五步：创建图片  
            IPicture pict = patriarch.CreatePicture(anchor, pictureIdx);

            //6.保存         
            //FileStream file = new FileStream(@"E:\myworkbook11.xls", FileMode.Create);
            //Workbook.Write(file);

            //using (FileStream fs = new FileStream(savePath, FileMode.OpenOrCreate))
            //{
            //    Workbook.Write(fs);
            //    fs.Close();                     // 释放
            //}

            //
            // 将临时文件保存到指定路径
            //
            //scrapMergeExport.Save(savePath);
            //*****************************************************************

            
            Thread.Sleep(10);
        }

        /// <summary>
        /// 填充数据到表格并给出进度
        /// </summary>
        /// <param name="progressinfo"></param>
        protected override void FillBody(ref ProgressInfo progressinfo)
        {

            if (null == _ScrapMerge)
                return;

            TempRow = Sheet.CreateRow(NextRowIndex);

            // 使用属性0
            TempRow.CreateCell(0).SetCellValue(_ScrapMerge.type);

            //
            // 换行
            //
            NextRowIndex++;
            Thread.Sleep(10);
        }
    }
}