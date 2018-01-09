using System;
using System.Collections.Generic;
using System.Text;
using EAM.Data.Comm;
using NPoco;

namespace EAM.Data.Domain
{
    [TableName("ASSETS_ANIMAL")]
    [PrimaryKey("ID")]
    public class AnimalandplantAttribute : IEntity<int>
    {
        [Column("ID")]
        public int EntityId { get; set; }

        /// <summary>
        /// 资产编号
        /// </summary>
        [Column("ASSETSNUM")]
        public string AssetsNum { get; set; }

        /// <summary>
        /// 出生/栽种年份
        /// </summary>
        [Column("BIRTHDAY")]
        public DateTime Birthday { get; set; }

        /// <summary>
        /// 预计寿命/种龄
        /// </summary>
        [Column("AGE")]
        public string Age { get; set; }

        /// <summary>
        /// 纲属科
        /// </summary>
        [Column("CLASS")]
        public string Class { get; set; }

        /// <summary>
        /// 产地
        /// </summary>
        [Column("PALCE")]
        public string Palce { get; set; }

        [Ignore]
        public int AttrId { get; set; }
    }
}