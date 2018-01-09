﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using EAM.Data.Domain;
using EAM.Data.Services;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace EAM.Data.ImportAndExport.Export
{
    //导出功能 基类
    public abstract class ExportDynamicBase
    {
        protected IWorkbook Workbook;
        protected ISheet Sheet;
        protected IRow TempRow;
        protected ICell TempCell;
        protected int NextRowIndex = 0;
        

        public abstract string SaveFileName { get; }
        public abstract string SheetName { get; }
        public ExportDynamicBase()
        {
            //ryb test 2016-06-24
            //复制模板文件到临时文件
       
            string strBasePath = AppDomain.CurrentDomain.BaseDirectory;                          //Web程序目录
            string excelTempatePath = strBasePath + "ExportExcelTemplate\\";                    //模板目录
           // string excelTemplateFile = excelTempatePath + "ZC01_土地_导入模板.xls";            //模板文件
            string excelTemplateFile = excelTempatePath + this.SaveFileName;
           // string strNewTempFile = Path.Combine(excelTempatePath, string.Format("{0}.xls", DateTime.Now.ToString("yyMMddHHmmss")));
            string strTempFile = excelTempatePath + "temp.xls"; //临时文件路径名
            File.Copy(excelTemplateFile, strTempFile, true);   //模板复制到临时文件
            FileStream fs = new FileStream(strTempFile, FileMode.Open, FileAccess.Read); //打开临时模板
          
            //将临时模板文件构造为 HSSFWorkbook
            Workbook = new HSSFWorkbook(fs);
            Sheet = Workbook.GetSheetAt(0);
            fs.Close();
            fs.Dispose();
           
        }
        protected abstract void BuildTitle();

        protected virtual void QueryData()
        {
             
        }

        protected abstract void BuildHeader();
        protected abstract void FillBody();
        protected abstract void BuildFooter();

        public void DoExport()
        {
            NextRowIndex = 4;
           //    BuildTitle(); // cmt by ryb 2016-06-24
            QueryData();
         //   BuildHeader();  //字段
            FillBody();     //填充
            BuildFooter();
        }

        public virtual void Save(string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                Workbook.Write(fs);
            }
        }
    }
}