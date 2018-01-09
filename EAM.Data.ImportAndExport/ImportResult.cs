using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EAM.Data.Domain;

namespace EAM.Data.ImportAndExport
{
    public class ImportResult
    {
        public List<AssetsMain> Imported { get; set; }
        public List<AssetsMainMsg> UnImported { get; set; }
        public List<UserInfo> UserImported { get; set; }//2017-05-26 wnn

        public Boolean ImportMsg { get; set; }

        public ImportResult()
        {
            Imported = new List<AssetsMain>();
            UnImported = new List<AssetsMainMsg>();
            UserImported = new List<UserInfo>();//2017-05-26 wnn
            ImportMsg = true;
        }
    }

    public class AssetsMainMsg
    {
        public AssetsMain AssetsMain { get; private set; }
        public string Message { get; set; }

        public AssetsMainMsg(AssetsMain assetsMain)
        {
            AssetsMain = assetsMain;
        }
    }
    /// <summary>
    /// 用户导入 2017-05-26 wnn
    /// </summary>
    public class UserMsg
    {
        public UserInfo UserInfo { get; private set; }
        public string Message { get; set; }
        public UserMsg(UserInfo userInfo)
        {
            UserInfo = userInfo;
        }
    }
}