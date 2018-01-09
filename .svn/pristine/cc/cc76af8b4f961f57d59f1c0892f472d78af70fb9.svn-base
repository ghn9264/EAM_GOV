using EAM.Data.Comm;
using NPoco;

namespace EAM.Data.Domain
{
    [TableName("ASSETS_CLASS_CODE")]
    [PrimaryKey("ID")]
    public class ClassCode : IEntity<string>
    {
        /// <summary>
        /// 分类编号
        /// </summary>
        [Column("ID")]
        public string EntityId { get; set; }

        /// <summary>
        /// 父级分类编码
        /// </summary>
        [Column("PARENT_ID")]
        public string ParentId { get; set; }

        /// <summary>
        /// 分类名称
        /// </summary>
        [Column("CLASS_NAME")]
        public string ClassName { get; set; }

        /// <summary>
        /// 计量单位
        /// </summary>
        [Column("UNIT")]
        public string Unit { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Column("MEMO")]
        public string Memo { get; set; }

        /// <summary>
        /// 下一个资产号
        /// </summary>
        [Column("NEXTNUM")]
        public int NextNum { get; set; }
        /// <summary>
        /// 报废年限
        /// </summary>
        [Column("SCRAP_YEAR")]
        public int SCRAPYEAR { get; set; }
    }
}