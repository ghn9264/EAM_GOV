using System;
using EAM.Data.Comm;
using EAM.Data.Domain;
using EAM.Data.Repositories;
using EAM.Data.Services.Query;

namespace EAM.Data.Services.Impl
{
    public class AssetsAcquireServices : IAssetsAcquireServices
    {

        private readonly IAquairRepository _aquairRepository;

        public AssetsAcquireServices(IAquairRepository aquairRepository)
        {
            _aquairRepository = aquairRepository;
        }

        #region 查询

        public PagedList<AquairAttribute> QueryPage(AllRecordQuery query)
        {
            return _aquairRepository.PagedList(query.PageIndex, query.PageSize, query.QuerySql);
        }

        #endregion


    }
}