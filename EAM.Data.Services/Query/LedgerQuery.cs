using System;
using System.Text;
using NPoco;

namespace EAM.Data.Services.Query
{
    public class LedgerQuery : QueryBase
    {
        public DateTime? BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string GoodsName { get; set; }
        public string CatCode { get; set; }
        public string CatName { get; set; }
        
        public override Sql QuerySql
        {
            get
            {
                var where = "";
                //if (BeginDate.HasValue)
                //    where = string.Format(" AND GET_DATE>='{0}'", BeginDate.Value.ToString("yyyy-MM-dd 00:00:00"));
                //if (EndDate.HasValue)
                //    where = string.Format(" AND GET_DATE<='{0}'", EndDate.Value.ToString("yyyy-MM-dd 23:59:59"));
                //if (!string.IsNullOrEmpty(GoodsName))
                //    where = string.Format("{0} AND GOODS_NAME LIKE {1}", where, "%" + GoodsName + "%");
                //if (!string.IsNullOrEmpty(CatCode))
                //    where = string.Format("{0} AND CAT_CODE ='{1}'", where, CatCode);

                StringBuilder sb = new StringBuilder();
                sb.Append(
                    "SELECT TIN.*,IFNULL( TOUT.OUTCOUNT,0) AS OUTCOUNT,IFNULL( TOUT.OUTMONEY,0) AS OUTMONEY,IFNULL( TOUT.OUTDEPRECIATION,0) AS OUTDEPRECIATION,(TIN.INDEPRECIATION-IFNULL( TOUT.OUTDEPRECIATION,0)) AS DEPRECIATION,(TIN.INCOUNT-IFNULL( TOUT.OUTCOUNT,0)) AS `COUNT`,(TIN.INMONEY-IFNULL( TOUT.OUTMONEY,0)-(TIN.INDEPRECIATION-IFNULL( TOUT.OUTDEPRECIATION,0))) AS `MONEY`  FROM ")
                    .AppendFormat("(SELECT DATE_FORMAT(`GET_DATE`,'%Y') AS YEAR ,DATE_FORMAT(`GET_DATE`,'%Y-%m-%d') GETDATE ,`ASSETSNUM`,`BRAND`,`MODEL_SPECIFICATIONS`,`STORE_PLACE`,`USE_PEOPLE`,`ACCOUNTING_DOC_NUM`,`GOODS_NAME`,MAX(`PRICE`) AS INPRICE,COUNT(*) AS INCOUNT, SUM(`MONEY`) AS INMONEY,SUM(ACCUMULATED_DEPRECIATION) AS INDEPRECIATION FROM ledger_cach  WHERE 1=1 {0} GROUP BY DATE_FORMAT(`GET_DATE`,'%Y-%m-%d') ,`ACCOUNTING_DOC_NUM`,`GOODS_NAME`,`PRICE`) TIN", where)
                    .Append(" LEFT JOIN  ")
                    .AppendFormat("(SELECT DATE_FORMAT(`GET_DATE`,'%Y-%m-%d') GETDATE ,`ACCOUNTING_DOC_NUM`,`GOODS_NAME`,`PRICE` AS OUTPRICE,COUNT(*) AS OUTCOUNT, SUM(`MONEY`) OUTMONEY,SUM(ACCUMULATED_DEPRECIATION) AS OUTDEPRECIATION FROM ledger_cach WHERE (`IS_SCRAP`=1 OR `IS_TURNOUT`=1) {0} GROUP BY DATE_FORMAT(`GET_DATE`,'%Y-%m-%d') ,`ACCOUNTING_DOC_NUM`,`GOODS_NAME`,`PRICE`) TOUT", where)
                    .Append(" ON TIN.GETDATE=TOUT.GETDATE AND TIN.ACCOUNTING_DOC_NUM=TOUT.ACCOUNTING_DOC_NUM AND TIN.GOODS_NAME=TOUT.GOODS_NAME  ORDER BY YEAR");

                //"SELECT TIN.*,IFNULL( TOUT.OUTCOUNT,0) AS OUTCOUNT,IFNULL( TOUT.OUTMONEY,0) AS OUTMONEY,IFNULL( TOUT.OUTDEPRECIATION,0) AS OUTDEPRECIATION,(TIN.INDEPRECIATION-IFNULL( TOUT.OUTDEPRECIATION,0)) AS DEPRECIATION,(TIN.INCOUNT-IFNULL( TOUT.OUTCOUNT,0)) AS `COUNT`,(TIN.INMONEY-IFNULL( TOUT.OUTMONEY,0)-(TIN.INDEPRECIATION-IFNULL( TOUT.OUTDEPRECIATION,0))) AS `MONEY`  FROM ")
                //.AppendFormat("(SELECT DATE_FORMAT(`GET_DATE`,'%Y') YEAR ,DATE_FORMAT(`GET_DATE`,'%Y-%m-%d') GETDATE ,`GOODS_NAME`,`ASSETSNUM`,`BRAND`,`MODEL_SPECIFICATIONS`,COUNT(*) AS INCOUNT, MAX(`PRICE`) AS INPRICE,SUM(`MONEY`) AS INMONEY,`STORE_PLACE`,`USE_PEOPLE`,`ACCOUNTING_DOC_NUM`,SUM(ACCUMULATED_DEPRECIATION) AS INDEPRECIATION ,FROM ASSETS_MAIN  WHERE 1=1 {0} GROUP BY DATE_FORMAT(`GET_DATE`,'%Y-%m-%d') ,`ACCOUNTING_DOC_NUM`,`GOODS_NAME`,`PRICE`) TIN", where)    
                //.Append(" LEFT JOIN  ")
                //.AppendFormat("(SELECT DATE_FORMAT(`GET_DATE`,'%Y') YEAR ,DATE_FORMAT(`GET_DATE`,'%Y-%m-%d') GETDATE ,`GOODS_NAME`,COUNT(*) AS OUTCOUNT, MAX(`PRICE`) AS OUTPRICE,SUM(`MONEY`) AS OUTMONEY,`ACCOUNTING_DOC_NUM`,SUM(ACCUMULATED_DEPRECIATION) AS INDEPRECIATION , FROM ASSETS_MAIN WHERE (`IS_SCRAP`=1 OR `IS_TURNOUT`=1) {0} GROUP BY DATE_FORMAT(`GET_DATE`,'%Y-%m-%d') ,`ACCOUNTING_DOC_NUM`,`GOODS_NAME`,`PRICE`) TOUT", where)
                //.Append(" ON TIN.GETDATE=TOUT.GETDATE AND TIN.ACCOUNTING_DOC_NUM=TOUT.ACCOUNTING_DOC_NUM AND TIN.GOODS_NAME=TOUT.GOODS_NAME  ORDER BY YEAR");
                var sql = Sql.Builder.Append(sb.ToString());
                //StringBuilder sb = new StringBuilder();
                //var sql = Sql.Builder.Append("select * from ledger_cach where(1=1)");
                return sql;
            }
        }
    }
}