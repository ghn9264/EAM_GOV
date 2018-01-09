using EAM.Data.Comm;
using NPoco;
using System;

namespace EAM.Data.Domain
{
    [TableName("DIFF_RESULT")]
    [PrimaryKey("ID")]
    [NPoco.ExplicitColumns]
    public class DiffResult : IEntity<int>
    {
        [Column("ID")]
        public int EntityId { get; set; }

        [Column("INDEX")]
        public int Index { get; set; }

        /// <summary>
        /// 比对状态
        /// </summary>
        [Column("DIFF_STATE")]
        public string DiffState { get; set; }

        /// <summary>
        /// 本库编码
        /// </summary>
        [Column("LOCAL_STATE")]
        public string LocalState { get; set; }

        /// <summary>
        /// 本库编码
        /// </summary>
        [Column("LOCAL_CODE")]
        public string LocalCode { get; set; }

        /// <summary>
        /// 本库名称
        /// </summary>
        [Column("LOCAL_NAME")]
        public string LocalName { get; set; }

        /// <summary>
        /// 本库价值
        /// </summary>
        [Column("LOCAL_VALUE")]
        public decimal LocalValue { get; set; }

        /// <summary>
        /// 本库规格
        /// </summary>
        [Column("LOCAL_MODE")]
        public string LocalMode { get; set; }

        /// <summary>
        /// 办学编码
        /// </summary>
        [Column("BX_STATE")]
        public string BxState { get; set; }

        /// <summary>
        /// 办学编码
        /// </summary>
        [Column("BX_CODE")]
        public string BxCode { get; set; }

        /// <summary>
        /// 办学名称
        /// </summary>
        [Column("BX_NAME")]
        public string BxName { get; set; }

        /// <summary>
        /// 办学价值
        /// </summary>
        [Column("BX_VALUE")]
        public decimal BxValue { get; set; }

        /// <summary>
        /// 办学规格
        /// </summary>
        [Column("BX_MODE")]
        public string BxMode { get; set; }

        /// <summary>
        /// 动态编码
        /// </summary>
        [Column("DT_STATE")]
        public string DtState { get; set; }

        /// <summary>
        /// 动态编码
        /// </summary>
        [Column("DT_CODE")]
        public string DtCode { get; set; }

        /// <summary>
        /// 动态名称
        /// </summary>
        [Column("DT_NAME")]
        public string DtName { get; set; }

        /// <summary>
        /// 动态价值
        /// </summary>
        [Column("DT_VALUE")]
        public decimal DtValue { get; set; }

        /// <summary>
        /// 动态规格
        /// </summary>
        [Column("DT_MODE")]
        public string DtMode { get; set; }


    }
}
