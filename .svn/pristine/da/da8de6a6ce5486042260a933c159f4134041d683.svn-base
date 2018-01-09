using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EAM.Data.Comm;
using EAM.Data.Domain;
using EAM.Data.Services.Dto;
using EAM.Data.Services.Query;

namespace EAM.Data.Services
{
    /// <summary>
    /// 提供所有系统信息新增,修改,删除,查询操作
    /// </summary>
    public interface IScrapService
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="item"></param>
        void AddAssetsScrap(AssetsScrapApply item);

        /// <summary>
        /// 新增报废批复资产
        /// </summary>
        /// <param name="item"></param>
        void AddAssetsScrapReply(AssetsScrapReply item);

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        PagedList<AssetsScrapApply> QueryPage(AllRecordQuery query);

        /// <summary>
        /// 查询报废批复资产
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        PagedList<AssetsScrapReply> QueryPageReply(AllRecordQuery query);

        /// <summary>
        /// 查询报废批复表总的资产
        /// </summary>
        /// <param name="ScrapFormNo"></param>
        /// <returns></returns>
        AssetsScrapReply GetScrapRelpy(int ScrapFormNo);

        /// <summary>
        /// 根据资产编号查询资产
        /// </summary>
        /// <param name="ScrapFormNo"></param>
        /// <returns></returns>
        ScrapMerge GetRelpyTable(string AssetsNum);

        /// <summary>
        /// 根据分类编号查询图片路径
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        List<AssetsScrapPhoto> GetPhotoSrc(string type);

        /// <summary>
        /// 查询报废详细资产编号
        /// </summary>
        /// <param name="entityID"></param>
        /// <returns></returns>
        List<string> GetAssetsScrapList(int entityID);

        /// <summary>
        /// 根据图片路径查询整条资产
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        AssetsScrapPhoto GetAssetsScrapPhoto(string src);

        /// <summary>
        /// 新增绑定图片参数
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        void AddAssetsScrapPhoto(AssetsScrapPhoto photo);

        /// <summary>
        /// 添加分类合并数据
        /// </summary>
        /// <param name="scrapMerge"></param>
        void AddScrapMerge(ScrapMerge scrapMerge);

        /// <summary>
        /// 批量添加分类合并数据
        /// </summary>
        /// <param name="scrapMerge"></param>

        void AddScrapMerge(List<ScrapMerge> scrapMergeList);


        /// <summary>
        /// 删除对同一条报废申请添加的分类合并记录
        /// 因为每次点击分类合并就会产生一次分类合并结果，这个结果是写到数据库中的
        /// 所以当对这同一条报废申请点击多次分类合并按钮，就会生成多个分类合并结果
        /// 故，每次执行分类合并前，先将该报废条目对应的分类合并记录先删除，然后再添加
        /// </summary>
        /// <param name="scrapFormNo">报废申请记录号</param>
        void RemoveLastScrapMerge(int scrapFormNo);

        /// <summary>
        /// 查询分类合并结果
        /// </summary>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        MergePage QueryMergePage(int PageIndex, int PageSize, int FormNum);

        /// <summary>
        /// 查询批复导出列表数据
        /// </summary>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="FormNum"></param>
        /// <returns></returns>
        PagedList<ScrapMerge> QueryGetOut(int PageIndex, int PageSize);
        /// <summary>
        /// 根据entityId查询SCRAPMERGE
        /// </summary>
        /// <param name="EntityId"></param>
        /// <returns></returns>
        ScrapMerge FindMergePage(int entityId);

        /// <summary>
        /// 根据EntityId获取一条记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        ScrapMerge GetScrapMerge(int entity);
    }
}