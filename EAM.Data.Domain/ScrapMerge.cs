using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EAM.Data.Comm;
using NPoco;

namespace EAM.Data.Domain
{
    [TableName("SCRAPMERGE")]
    [PrimaryKey("ID")]
    public class ScrapMerge : IEntity<int>
    {
        [Column("ID")]
        public int EntityId { get; set; }

        /// <summary>
        /// 资产编号
        /// </summary>
        [Column("ASSETSNUM")]
        public string AssetsNum { get; set; }

        /// <summary>
        /// 品牌型号
        /// </summary>
        [Column("BRAND")]
        public string Brand { get; set; }

        /// <summary>
        /// 资产名称
        /// </summary>
        [Column("ASSETSNAME")]
        public string AssetsName { get; set; }

        /// <summary>
        /// 资产数量
        /// </summary>
        [Column("COUNTS")]
        public int Counts { get; set; }

        /// <summary>
        /// 资产单价
        /// </summary>
        [Column("PRICE")]
        public decimal Price { get; set; }

        /// <summary>
        /// 取得日期 
        /// </summary>
        [Column("GET_DATA")]
        public DateTime GetsDate { get; set; }

        /// <summary>
        /// 已经使用年限 
        /// </summary>
        [Column("USED_YEAS")]
        public string UsedYears { get; set; }

        /// <summary>
        /// 规定使用年限 
        /// </summary>
        [Column("DEAD_YEAS")]
        public string DeadYears { get; set; }

        /// <summary>
        /// 信息化负责人 
        /// </summary>
        [Column("PEOPLE")]
        public string People { get; set; }

        /// <summary>
        /// 信息化负责人电话 
        /// </summary>
        [Column("TEL")]
        public string Tel { get; set; }

        /// <summary>
        /// 主要参数1
        /// </summary>
        [Column("MAIN_PARA1")]
        public string qzhuyaocanshu1 { get; set; }

        /// <summary>
        /// 主要参数2
        /// </summary>
        [Column("MAIN_PARA2")]
        public string qzhuyaocanshu2 { get; set; }

        /// <summary>
        /// 主要参数3
        /// </summary>
        [Column("MAIN_PARA3")]
        public string qzhuyaocanshu3 { get; set; }

        /// <summary>
        /// 主要参数4
        /// </summary>
        [Column("MAIN_PARA4")]
        public string qzhuyaocanshu4 { get; set; }

        /// <summary>
        /// 主要参数5
        /// </summary>
        [Column("MAIN_PARA5")]
        public string qzhuyaocanshu5 { get; set; }

        /// <summary>
        /// 主要参数6
        /// </summary>
        [Column("MAIN_PARA6")]
        public string qzhuyaocanshu6 { get; set; }

        /// <summary>
        /// 报废理由
        /// </summary>
        [Column("REASON")]
        public string Reason { get; set; }


        /// <summary>
        /// 填表人
        /// </summary>
        [Column("FILL_PERSON")]
        public string FillPerson { get; set; }

        /// <summary>
        /// 显示技术
        /// </summary>
        [Column("XIANSHIJISHU")]
        public string txianshijishu { get; set; }

        /// <summary>
        /// 最高显示分辨率
        /// </summary>
        [Column("FENBIANLV")]
        public string tzuigaoxianshifenbianlv { get; set; }

        /// <summary>
        /// 标称光亮度
        /// </summary>
        [Column("LIANGDU")]
        public string tbiaochengguangliangdu { get; set; }

        /// <summary>
        /// 标称对比度
        /// </summary>
        [Column("DUIBIDU")]
        public string tbiaochengduibidu { get; set; }

        /// <summary>
        /// D_CPU
        /// </summary>
        [Column("D_CPU")]
        public string dcpu { get; set; }

        /// <summary>
        /// D_主板
        /// </summary>
        [Column("D_ZHUBAN")]
        public string dzhuban { get; set; }

        /// <summary>
        /// D_硬盘
        /// </summary>
        [Column("D_YINGPAN")]
        public string dyingpan { get; set; }

        /// <summary>
        /// D_内存
        /// </summary>
        [Column("D_NEICUN")]
        public string dneicun { get; set; }

        /// <summary>
        /// D_显卡
        /// </summary>
        [Column("D_XIANKA")]
        public string dxianka { get; set; }

        /// <summary>
        /// D_光驱
        /// </summary>
        [Column("D_GUANGQU")]
        public string dguangqu { get; set; }

        /// <summary>
        /// D_显示器
        /// </summary>
        [Column("D_XIANSHIQI")]
        public string dxianshiqi { get; set; }

        /// <summary>
        /// D_显示器尺寸
        /// </summary>
        [Column("D_XIANSHIQICHICUN")]
        public string dxianshiqichicun { get; set; }

        /// <summary>
        /// D_显示器类型
        /// </summary>
        [Column("D_XIANSHIQILEIXING")]
        public string dxianshiqileixing { get; set; }

        /// <summary>
        /// D_键盘
        /// </summary>
        [Column("D_KEYBORD")]
        public string djianpan { get; set; }

        /// <summary>
        /// D_鼠标
        /// </summary>
        [Column("D_MOUSE")]
        public string dshubiao { get; set; }

        /// <summary>
        /// F_CPU
        /// </summary>
        [Column("F_CPU")]
        public string fcpu { get; set; }

        /// <summary>
        /// F_主板
        /// </summary>
        [Column("F_ZHUBAN")]
        public string fzhuban { get; set; }

        /// <summary>
        /// F_硬盘
        /// </summary>
        [Column("F_YINGPAN")]
        public string fyingpan { get; set; }

        /// <summary>
        /// F_内存
        /// </summary>
        [Column("F_NEICUN")]
        public string fneicun { get; set; }

        /// <summary>
        /// F_显卡
        /// </summary>
        [Column("F_XIANKA")]
        public string fxianka { get; set; }

        /// <summary>
        /// F_光驱
        /// </summary>
        [Column("F_GUANGQU")]
        public string fguangqu { get; set; }

        /// <summary>
        /// 分类编号
        /// </summary>
        [Column("TYPE")]
        public int type { get; set; }

        /// <summary>
        /// 图片MD5
        /// </summary>
        [Column("PIC")]
        public string src { get; set; }

        /// <summary>
        /// 报废单号
        /// </summary>
        [Column("SCRAPFORM")]

        public int ScrapFormNo { get; set; }

        [Column("MODEL")]
        public string ModelSpecification { get; set; }

        /// <summary>
        /// 总金额
        /// </summary>
        [Column("MONEY")]
        public decimal Money { get; set; }

        /// <summary>
        /// 导出状态(0、未批复 1、已导出 2、已批复，未导出）
        /// </summary>
        [Column("GETOUT")]
        public int HasGetOut { get; set; }

        /// <summary>
        /// 是否删除（不显示在导出列表 0、未删除）
        /// </summary>
        [Column("IS_DELET")]
        public int Isdelet { get; set; }

        /// <summary>
        /// 图片在服务器上的路径
        /// </summary>
        [Column("PHOTO_SRC")]
        public string PhotoSrc { get; set; }

        /// <summary>
        /// 计算机类图片2在服务器上的路径
        /// </summary>
        [Column("PHOTO_SRCD")]
        public string PhotoSrcD { get; set; }

    }
}