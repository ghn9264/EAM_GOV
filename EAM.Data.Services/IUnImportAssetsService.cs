using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EAM.Data.Comm;
using EAM.Data.Domain;
using EAM.Data.Services.Query;
using EAM.Data.Domain;

namespace EAM.Data.Services
{
    public interface IUnImportAssetsService
    {
       
        //未导入资产
        PagedList<UnImportAssets> QueryPage(UnImportAssetsQuery query);
        UnImportAssets Add(UnImportAssets item);
        //int UnImportAssetsNum { get; }
        void Update(UnImportAssets item);
        UnImportAssets QueryAllUnImport();
    }
    
     
}