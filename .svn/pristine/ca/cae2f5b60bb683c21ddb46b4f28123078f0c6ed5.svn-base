using EAM.Data.Comm;
using EAM.Data.Domain;
using EAM.Data.Repositories;
using EAM.Data.Services.Query;
using NPoco;

namespace EAM.Data.Services.Impl
{
    public class UnImportHistoryService : IUnImportHistoryService
    {
        private readonly IUnImportHistoryRepository _importHistoryRep;

        public UnImportHistoryService(IUnImportHistoryRepository importHistoryRep)
        {
            _importHistoryRep = importHistoryRep;

        }

        public int UploadRateOfProgress { get; set; }
        public UnImportHistory Add(UnImportHistory item)
        {
            _importHistoryRep.Add(item);
            return item;
        }

        public void Update(UnImportHistory item)
        {
            _importHistoryRep.Update(item);
        }

        public UnImportHistory LastHistory(int importType)
        {
            var sql = @"SELECT * FROM assets_import_history WHERE IMPORT_TYPE=@0 ORDER BY ID DESC LIMIT 1";
            return _importHistoryRep.FirstOrDefault(sql, importType);
        }
        public UnImportHistory LastHistory()
        {
            var sql = @"SELECT * FROM assets_import_history  ORDER BY ID DESC LIMIT 1";
            return _importHistoryRep.FirstOrDefault(sql);
        }

        public PagedList<UnImportHistory> QueryPage(UnImportHistoryQuery query)
        {
            return _importHistoryRep.PagedList(query.PageIndex, query.PageSize, query.QuerySql);
        }
        public PagedList<UnImportHistory> QueryPage(AllRecordQuery query)
        {
            return _importHistoryRep.PagedList(query.PageIndex, query.PageSize, query.QuerySql);
        }

    }
}