using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EAM.Data.Comm;
using EAM.Data.Domain;
using EAM.Data.Services.Query;

namespace EAM.Data.Services
{
    public interface IImportHistoryService
    {
        ImportHistory Add(ImportHistory item);
        void Update(ImportHistory item);
        int Remove(int id);

        /// <summary>
        /// 根据记录号去删除动态导入记录
        /// 并且删除该条记录导入的所有资产
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int DeleteRecordAndData(int id);
        int RemoveAll();
        /// <summary>
        /// 获取最新导入记录 导入类型 1:办学,2:动态
        /// </summary>
        /// <param name="importType"></param>
        /// <returns></returns>
        /// 
        //查询最新上办学或动态资产上传历史纪录，
        ImportHistory LastHistory(int importType);
       //查询最新上传历史纪录
        ImportHistory LastHistory();
       
        //最新上传进度
        int UploadRateOfProgress { get; set; }
        
        //上传历史查询
        PagedList<ImportHistory> QueryPage(ImportHistoryQuery query);
      
      
    }
}