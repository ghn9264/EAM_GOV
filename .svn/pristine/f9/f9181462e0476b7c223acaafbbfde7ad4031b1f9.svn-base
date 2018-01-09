using System;
using EAM.Data.Comm;
using NPoco;

namespace EAM.Data.Domain
{
    [TableName("ASSETS_UNIMPORT")]
    [PrimaryKey("ID")]
    public class UnImportAssets : IEntity<int>
    {
        [Column("ID")]
        public int EntityId { get; set; }

        /// <summary>
        /// 资产名称
        /// </summary>
        [Column("GOODS_NAME")]
        public string GoodsName { get; set; }

        /// <summary>
        /// 动态资产编号
        /// </summary>
        [Column("DYNAMIC_NUM")]
        public string DynamicNum { get; set; }

        /// <summary>
        /// 办学库存号
        /// </summary>
        [Column("STORE_NUM")]
        public string StoreNum { get; set; }

        /// <summary>
        /// 导入表名字
        /// </summary>
        [Column("TABLE_NAME")]
        public string TableName { get; set; }

        /// <summary>
        /// 在导入表中第几行
        /// </summary>
        [Column("TABLE_ROW")]
        public int TableRow { get; set; }

        /// <summary>
        /// 导入记录ID
        /// </summary>
        [Column("IMPORT_ID")]
        public int ImportId { get; set; }

        /// <summary>
        /// 导入类型 1:办学,2:动态
        /// </summary>
        [Column("IMPORT_TYPE")]
        public int ImportType { get; set; }

        /// <summary>
        /// 导入日期
        /// </summary>
        [Column("IMPORT_TIME")]
        public DateTime ImportTime { get; set; }

        /// <summary>
        /// 导入用户
        /// </summary>
        [Column("IMPORT_PERSON")]
        public string ImportPerson { get; set; }

        /// <summary>
        /// 导入异常
        /// </summary>
        [Column("EXCEPTION")]
        public string Exception { get; set; }

        

        


    }
     
}