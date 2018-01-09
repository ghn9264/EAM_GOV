using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EAM.Data.Comm;
using EAM.Data.Domain;
using EAM.Data.Services.Query;

namespace EAM.Data.Services
{
    public interface IUnImportHistoryService
    {
        UnImportHistory Add(UnImportHistory item);
        void Update(UnImportHistory item);

        /// <summary>
        /// 获取最新导入记录 导入类型 1:办学,2:动态
        /// </summary>
        /// <param name="importType"></param>
        /// <returns></returns>
        /// 
        //查询最新上办学或动态资产上传历史纪录，
        UnImportHistory LastHistory(int importType);
       //查询最新上传历史纪录
        UnImportHistory LastHistory();
       
        //最新上传进度
        int UploadRateOfProgress { get; set; }
        
        //上传历史查询
        PagedList<UnImportHistory> QueryPage(UnImportHistoryQuery query);
    }
}