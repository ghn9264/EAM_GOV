using EAM.Data.Comm;
using EAM.Data.Domain;
using EAM.Data.Repositories;
using EAM.Data.Services.Query;
using NPoco;

namespace EAM.Data.Services.Impl
{
    public class ImportHistoryService : IImportHistoryService
    {
        private readonly IImportHistoryRepository _importHistoryRep;

        public ImportHistoryService(IImportHistoryRepository importHistoryRep)
        {
            _importHistoryRep = importHistoryRep;
        }

        public int UploadRateOfProgress { get; set; }
        public ImportHistory Add(ImportHistory item)
        {
            _importHistoryRep.Add(item);
            return item;
        }

        public void Update(ImportHistory item)
        {
            _importHistoryRep.Update(item);
        }

        public ImportHistory LastHistory(int importType)
        {
            var sql = @"SELECT * FROM assets_import_history WHERE IMPORT_TYPE=@0 ORDER BY ID DESC LIMIT 1";
            return _importHistoryRep.FirstOrDefault(sql, importType);
        }
        public ImportHistory LastHistory()
        {
            var sql = @"SELECT * FROM assets_import_history  ORDER BY ID DESC LIMIT 1";
            return _importHistoryRep.FirstOrDefault(sql);
        }

        public PagedList<ImportHistory> QueryPage(ImportHistoryQuery query)
        {
            return _importHistoryRep.PagedList(query.PageIndex, query.PageSize, query.QuerySql);
        }
        public PagedList<ImportHistory> QueryPage(AllRecordQuery query)
        {
            return _importHistoryRep.PagedList(query.PageIndex, query.PageSize, query.QuerySql);
        }

    }
}