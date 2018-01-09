using EAM.Data.Comm;
using NPoco;

namespace EAM.Data.Domain
{
    [TableName("PLACE")]
    [PrimaryKey("ID")]

    public class Place : IEntity<int>
    {
        [Column("ID")]
        public int EntityId { get; set; }

        /// <summary>
        /// 父级ID
        /// </summary>
        [Column("PARENT_ID")]
        public int ParentId { get; set; }

        /// <summary>
        /// 父级名称 ResultColumn
        /// </summary>
        [ResultColumn("PARENT_PLACE_NAME")]
        public string ParentName { get; set; }

        /// <summary>
        /// 父级编码 ResultColumn
        /// </summary>
        [ResultColumn("PARENT_PLACE_CODE")]
        public string ParentCode { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Column("PLACE_NAME")]
        public string PlaceName { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Column("PLACE_CODE")]
        public string PlaceCode { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Column("PLACE_TYPE")]
        public string PlaceType { get; set; }
    }
}