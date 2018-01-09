using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EAM.Data.Comm;
using NPoco;

namespace EAM.Data.Domain
{
    [TableName("ASSETS_SCRAP_PHOTO")]
    [PrimaryKey("ID")]
    public class AssetsScrapPhoto : IEntity<int>
    {
        [Column("ID")]
        public int EntityId { get; set; }

        /// <summary>
        /// 分类编号
        /// </summary>
        [Column("TYPE")]
        public int type { get; set; }

        /// <summary>
        /// 图片路径
        /// </summary>
        [Column("SRC")]
        public string src { get; set; }

        /// <summary>
        /// 计算机类图片2路径
        /// </summary>
        [Column("SRCD")]
        public string srcd { get; set; }

        /// <summary>
        /// 学校名称
        /// </summary>
        [Column("XUEXIAOMINGCHENG")]
        public string xuexiaomingcheng { get; set; }

        /// <summary>
        /// 填表人
        /// </summary>
        [Column("TIANBIAOREN")]
        public string tianbiaoren { get; set; }

        /// <summary>
        /// 信息化负责人
        /// </summary>
        [Column("XINXIHUAFUZEREN")]
        public string xinxihuafuzeren { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        [Column("DIANHUA")]
        public string dianhua { get; set; }

        /// <summary>
        /// 规定使用年限
        /// </summary>
        [Column("GUIDINGSHIYONGNIANXIAN")]
        public string guidingshiyongnianxian { get; set; }

        /// <summary>
        /// 显示技术
        /// </summary>
        [Column("T_XIANSHIJISHU")]
        public string txianshijishu { get; set; }

        /// <summary>
        /// 最高显示分辨率
        /// </summary>
        [Column("T_ZUIGAOXIANSHIFENBIANLV")]
        public string tzuigaoxianshifenbianlv { get; set; }

        /// <summary>
        /// 标称光亮度
        /// </summary>
        [Column("T_BIAOCHENGGUANGLIANGDU")]
        public string tbiaochengguangliangdu { get; set; }

        /// <summary>
        /// 标称对比度
        /// </summary>
        [Column("T_BIAOCHENGDUIBIDU")]
        public string tbiaochengduibidu { get; set; }

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
        /// 主要参数1
        /// </summary>
        [Column("Q_ZHUYAOCANSHU1")]
        public string qzhuyaocanshu1 { get; set; }

        /// <summary>
        /// 主要参数2
        /// </summary>
        [Column("Q_ZHUYAOCANSHU2")]
        public string qzhuyaocanshu2 { get; set; }

        /// <summary>
        /// 主要参数3
        /// </summary>
        [Column("Q_ZHUYAOCANSHU3")]
        public string qzhuyaocanshu3 { get; set; }

        /// <summary>
        /// 主要参数4
        /// </summary>
        [Column("Q_ZHUYAOCANSHU4")]
        public string qzhuyaocanshu4 { get; set; }

        /// <summary>
        /// 主要参数5
        /// </summary>
        [Column("Q_ZHUYAOCANSHU5")]
        public string qzhuyaocanshu5 { get; set; }

        /// <summary>
        /// 主要参数6
        /// </summary>
        [Column("Q_ZHUYAOCANSHU6")]
        public string qzhuyaocanshu6 { get; set; }

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
        [Column("D_JIANPAN")]
        public string djianpan { get; set; }

        /// <summary>
        /// D_鼠标
        /// </summary>
        [Column("D_SHUBIAO")]
        public string dshubiao { get; set; }

        [ResultColumn]
        public List<string> AssetsNums { get; set; }
    }
}