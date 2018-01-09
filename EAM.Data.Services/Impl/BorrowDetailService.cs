using EAM.Data.Domain;
using EAM.Data.Repositories;

namespace EAM.Data.Services.Impl
{
    public class BorrowDetailService
    {

        private readonly IBorrowDetailRepository _assetsBorrowDetailRep;

        public BorrowDetailService(IBorrowDetailRepository assetsBorrowDetailRep)
        {
            _assetsBorrowDetailRep = assetsBorrowDetailRep;
        }

        public void AddAssetsBorrowDetail(BorrowDetailAttribute item)
        {
            _assetsBorrowDetailRep.Add(item);
        }


    }
}