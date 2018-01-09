using System.Dynamic;
using EAM.Data.Comm;
using EAM.Data.Domain;
using EAM.Data.Repositories;
using EAM.Data.Services.Query;
using NPoco;

namespace EAM.Data.Services.Impl
{
    public class UnImportAssetsService : IUnImportAssetsService
    {
        private readonly IUnImportAssetsRepository _unImportAssetsRep;

        public UnImportAssetsService(IUnImportAssetsRepository unImportAssetsRep)
        {
            _unImportAssetsRep = unImportAssetsRep;
         
        }
        public UnImportAssets Add(UnImportAssets item)
        {
            _unImportAssetsRep.Add(item);
            return item;
        }

        public void Update(UnImportAssets item)
        {
            _unImportAssetsRep.Update(item);
        }

        //public int UnImportAssetsNum{
        //  get { return _unImportAssetsRep.Count(); }
        //  //  set { }
        //}
        public UnImportAssets QueryAllUnImport()
        {
            var sql = @"SELECT * FROM assets_unimport ORDER BY ID DESC LIMIT 1";
            return _unImportAssetsRep.FirstOrDefault(sql);
        }
        public PagedList<UnImportAssets> QueryPage(UnImportAssetsQuery query)
        {
           return _unImportAssetsRep.PagedList(query.PageIndex, query.PageSize, query.QuerySql);

          
        }
    

    }
}