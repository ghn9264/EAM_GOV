using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace EAM.Inventory
{
    public class InventoryHelper
    {
        private static string CreateTableSql =
            @"DROP TABLE IF EXISTS inventory;CREATE TABLE [inventory] ([id] integer NOT NULL PRIMARY KEY AUTOINCREMENT,[goods_no] text,[goods_fiscalNO] text, [goods_name] text, [goods_model] text, [goods_status] integer, [goods_statusName] text,[goods_num] integer DEFAULT 0, [detail_id] integer DEFAULT 0, [remark] text);";

        private string _connString;

        public InventoryHelper(string sqliteFilePath)
        {
            _connString = string.Format("data source={0}", sqliteFilePath);
        }

        public bool TestConnection()
        {
            using (SQLiteConnection conn = new SQLiteConnection(_connString))
            {
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText = CreateTableSql;
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            return true;
        }

        public bool InsertInventory(List<InventoryInfo> items)
        {
            const string insertSqlTmp = "INSERT INTO [inventory]([goods_no],[goods_fiscalNO],[goods_name],[goods_model],[goods_status],[goods_statusName],[goods_num],[detail_id],[remark])" +
                                        "VALUES(@goods_no,@goods_fiscalNO,@goods_name,@goods_model,@goods_status, @goods_statusName,@goods_num,@detail_id,@remark);";
            using (SQLiteConnection conn = new SQLiteConnection(_connString))
            {
                foreach (var item in items)
                {
                    SqLiteHelper.ExecuteNonQuery(conn, insertSqlTmp, item.GoodsNo, item.GoodsFiscalNo, item.GoodsName, item.GoodsModel, item.GoodsStatus, item.GoodsStatusName, item.GoodsNum, item.DetailId, item.Remark);
                }
            }
            return true;
        }

        public List<InventoryInfo> GetInventoryList()
        {
            var result = new List<InventoryInfo>();
            using (SQLiteConnection conn = new SQLiteConnection(_connString))
            {
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"SELECT * FROM [inventory]";
                var dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    var item = new InventoryInfo();
                    item.Id = dataReader.GetInt32(0);
                    item.GoodsNo = dataReader.GetString(1);
                    item.GoodsFiscalNo = dataReader.GetString(2);
                    item.GoodsName = dataReader.GetString(3);
                    item.GoodsModel = dataReader.GetString(4);
                    item.GoodsStatus = dataReader.GetInt32(5);
                    item.GoodsStatusName = dataReader.GetString(6);
                    item.GoodsNum = dataReader.GetInt32(7);
                    item.DetailId = dataReader.GetInt32(8);
                    item.Remark = dataReader.GetString(9);
                    result.Add(item);
                }
            }
            return result;
        }

    }
}