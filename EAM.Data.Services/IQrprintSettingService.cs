using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EAM.Data.Comm;
using EAM.Data.Domain;
using EAM.Data.Services.Query;

namespace EAM.Data.Services
{
    public interface IQrprintSettingService
    {
        void add(QrprintSetting qrset);

        void add(QrprintSettinghistory qrsethistory);

        QrprintSetting getqrset();
    }
}
