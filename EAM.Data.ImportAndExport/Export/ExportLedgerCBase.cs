using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using EAM.Data.Domain;
using EAM.Data.Services;
using NPOI.HSSF.Record;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using EAM.Data.ImportAndExport.Import;

namespace EAM.Data.ImportAndExport.Export
{
    //导出功能 基类
    public abstract class ExportLedgerCBase
    {
        public IWorkbook Workbook;
        protected ISheet Sheet;
        protected IRow TempRow;
        protected ICell TempCell;
        protected int NextRowIndex = 0;

        /// <summary>
        /// 文件名称设定
        /// </summary>
        public string SaveFileName = "";

        /// <summary>
        /// 设置Sheet名称
        /// </summary>
        public abstract string SheetName { get; }

        /// <summary>
        /// 导出文件前准备
        /// </summary>
        /// <param name="type"></param>
        public ExportLedgerCBase(string type)
        {
            //
            // 获取当前根目录路径
            //
            string strBasePath = AppDomain.CurrentDomain.BaseDirectory;                          //Web程序目录

            //
            // 设置模板所在路径
            //
            string excelTempatePath = strBasePath + "Temp\\";                    //模板目录

            //
            // 设置临时文件路径
            //
            string strTempFile = "";

            SaveFileName = "表格一 固定资产明细账.xls"; //临时文件路径名
            strTempFile = excelTempatePath + "表格一 固定资产明细账.xls"; //临时文件路径名

            //
            // 设置文件保存全路径名称
            //
            string excelTemplateFile = strBasePath + "ExportExcelTemplate\\" + this.SaveFileName;

            //
            // 将模板文件复制到临时文件
            //
            File.Copy(excelTemplateFile, strTempFile, true);   //模板复制到临时文件

            //
            // 打开临时文件，获取文件读写流
            //
            FileStream fs = new FileStream(strTempFile, FileMode.Open, FileAccess.Read); //打开临时模板

            //
            // 获取临时文件句柄,来实现对文件的操作
            //
            Workbook = new HSSFWorkbook(fs);
            Sheet = Workbook.GetSheetAt(0);
            fs.Close();
            fs.Dispose();

        }

        /// <summary>
        /// 创建表Title
        /// </summary>
        protected abstract void BuildTitle();

        /// <summary>
        /// 查询数据
        /// </summary>
        protected virtual void QueryData()
        {

        }

        /// <summary>
        /// 创建表头
        /// </summary>
        protected abstract void BuildHeader();

        /// <summary>
        /// 填充数据
        /// </summary>
        protected abstract void FillBody();

        /// <summary>
        /// 填充数据并给出填充进度
        /// </summary>
        /// <param name="progressinfo">进度</param>
        protected abstract void FillBody(ref ProgressInfo progressinfo);

        /// <summary>
        /// 创建表底部
        /// </summary>
        protected abstract void BuildFooter();

        /// <summary>
        /// 导出，并给出进度
        /// </summary>
        /// <param name="progressinfo"></param>
        public void DoExport(ref ProgressInfo progressinfo)
        {
            //
            // 设置默认指向行数
            //
            NextRowIndex = 1;

            //
            // 查询数据
            //
            QueryData();

            //
            // 填写数据到表格
            //
            FillBody(ref progressinfo);     //填充

            //
            // 写表底部
            //
            BuildFooter();
        }

        /// <summary>
        /// 导出数据
        /// </summary>
        public void DoExport()
        {
            //
            // 设置默认指向行数
            //
            NextRowIndex =1;

            //
            // 查询数据
            //
            QueryData();

            //
            // 填写数据到表格
            //
            FillBody();     //填充

            //
            // 写表底部
            //
            //BuildFooter();
        }

        /// <summary>
        /// 将写好的文件保存到指定路径
        /// </summary>
        /// <param name="filePath"></param>
        public virtual void Save(string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                Workbook.Write(fs);
            }
        }
    }
}