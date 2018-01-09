using System;
using EAM.Data.Comm;
using EAM.Data.Domain;
using EAM.Data.Repositories;
using EAM.Data.Services.Query;

namespace EAM.Data.Services.Impl
{
    public class AssetsBorrrowServices : IAssetsBorrrowServices
    {

        private readonly IAssetsBorrrowRepository _assetsBorrrowRepository;

        public AssetsBorrrowServices(IAssetsBorrrowRepository assetsBorrrowRepository)
        {
            _assetsBorrrowRepository = assetsBorrrowRepository;
        }

        #region 新增

        public void AddBorrowRecord(BorrowAttribute item)
        {
            _assetsBorrrowRepository.Add(item);
        }

        #endregion

        #region 编辑

        public void UpdateBorrowRecord(BorrowAttribute assetsBorrrow)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region 删除

        #endregion

        #region 查询

        public PagedList<BorrowAttribute> QueryPage(AllRecordQuery query)
        {
            return _assetsBorrrowRepository.PagedList(query.PageIndex, query.PageSize, query.QuerySql);
        }

        #endregion

        #region 导入


        #endregion

    }
}