using System;
using EAM.Data.Comm;
using EAM.Data.Domain;
using EAM.Data.Repositories;
using EAM.Data.Services.Query;
using NPoco;

namespace EAM.Data.Services.Impl
{
    public class QrprintSettingService:IQrprintSettingService
    {
        private readonly IQrprintSettingRepository _qrprintSettingRep;
        private readonly IQrprintSettinghistoryRepository _qrprintSettinghistoryRep;
        public QrprintSettingService(IQrprintSettingRepository qrprintSettingRep, IQrprintSettinghistoryRepository qrprintSettinghistoryRep)
        {
            _qrprintSettingRep = qrprintSettingRep;
            _qrprintSettinghistoryRep = qrprintSettinghistoryRep;
        }
        public void add(QrprintSetting qrset)
        {
            _qrprintSettingRep.Update(qrset);
        }
        public void add(QrprintSettinghistory qrsethistory)
        {
            _qrprintSettinghistoryRep.Add(qrsethistory);
        }
        public QrprintSetting getqrset() 
        {
            return _qrprintSettingRep.FirstOrDefault(new NPoco.Sql("select * from qrprintsetting"));
        }
    }
}
