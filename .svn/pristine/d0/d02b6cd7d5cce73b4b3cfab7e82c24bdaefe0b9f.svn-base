using EAM.Data.Domain;
using EAM.Data.Repositories;

namespace EAM.Data.Services.Impl
{
    public class BorrowService
    {

        private readonly IBorrowRepository _assetsBorrowRep;

        public BorrowService(IBorrowRepository assetsBorrowRep)
        {
            _assetsBorrowRep = assetsBorrowRep;
        }

        public void AddAssetsBorrow(BorrowAttribute item)
        {
            _assetsBorrowRep.Add(item);
        }


    }
}