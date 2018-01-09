using System;
using System.Activities.Statements;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using EAM.Data.Comm;
using EAM.Data.Domain;
using EAM.Data.Repositories;
using EAM.Data.Services.Dto;
using EAM.Data.Services.Query;
using NPoco;

namespace EAM.Data.Services.Impl
{
    public class ScrapService : IScrapService
    {
        //
        // 接口引用
        //
        private readonly IScrapApllyRepository _scrapApllyRepository;
        private readonly IScrapApplyDetailRepository _scrapApplyDetailRepository;
        private readonly IAssetsScrapReplyRepository _assetsScrapReplyRepository;
        private readonly IAssetsScrapPhotoRepository _assetsScrapPhotoRepository;
        private readonly IScrapMergeRepository _scrapMergeRepository;

        public ScrapService(IScrapApllyRepository scrapApllyRepository,
            IScrapApplyDetailRepository scrapApplyDetailRepository,
            IAssetsScrapReplyRepository assetsScrapReplyRepository,
            IAssetsScrapPhotoRepository assetsScrapPhotoRepository,
            IScrapMergeRepository scrapMergeRepository)
        {
            //
            // 接口实例化
            //
            _scrapApllyRepository = scrapApllyRepository;
            _scrapApplyDetailRepository = scrapApplyDetailRepository;
            _assetsScrapReplyRepository = assetsScrapReplyRepository;
            _assetsScrapPhotoRepository = assetsScrapPhotoRepository;
            _scrapMergeRepository = scrapMergeRepository;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="item"></param>
        public void AddAssetsScrap(AssetsScrapApply item)
        {
            _scrapApllyRepository.Add(item);
        }

        /// <summary>
        /// 新增报废批复资产
        /// </summary>
        /// <param name="item"></param>
        public void AddAssetsScrapReply(AssetsScrapReply item)
        {
            _assetsScrapReplyRepository.Add(item);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public PagedList<AssetsScrapApply> QueryPage(AllRecordQuery query)
        {
            return _scrapApllyRepository.PagedList(query.PageIndex, query.PageSize, query.QuerySql);
        }

        /// <summary>
        /// 查询报废批复资产
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public PagedList<AssetsScrapReply> QueryPageReply(AllRecordQuery query)
        {
            return _assetsScrapReplyRepository.PagedList(query.PageIndex, query.PageSize, query.QuerySql);
        }

        /// <summary>
        /// 查询报废批复表总的资产
        /// </summary>
        /// <param name="ScrapFormNo"></param>
        /// <returns></returns>
        public AssetsScrapReply GetScrapRelpy(int ScrapFormNo)
        {
            var result = _assetsScrapReplyRepository.FirstOrDefault(x => x.ScrapFormNo == ScrapFormNo);
            return result;
        }

        /// <summary>
        /// 根据资产编号查询资产
        /// </summary>
        /// <param name="ScrapFormNo"></param>
        /// <returns></returns>
        public ScrapMerge GetRelpyTable(string AssetsNum)
        {
            var result = _scrapMergeRepository.FirstOrDefault(x => x.AssetsNum == AssetsNum);
            return result;
        }

        /// <summary>
        /// 根据分类编号查询图片路径
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public List<AssetsScrapPhoto> GetPhotoSrc(string type)
        {
            //
            // 创建查询SQL
            // 查询符合分类的所有条目
            //
            var sql = Sql.Builder.Where("TYPE = @0", type);

            //
            // 查询所有记录
            //
            var scrapPhotoList = _assetsScrapPhotoRepository.Query(sql);

            //
            // 从查询结果中提取资产编号列表
            //
            //var result = scrapPhotoList.Select(X => X.src).ToList();

            return scrapPhotoList;
        }



        /// <summary>
        /// 查询报废详细资产编号
        /// </summary>
        /// <param name="entityID"></param>
        /// <returns></returns>
        public List<string> GetAssetsScrapList(int entityID)
        {
            //
            // 创建查询SQL
            // 查询所有要报废的条目
            //
            var sql = Sql.Builder.Where("SCRAP_FORM_NO = @0", entityID);

            //
            // 查询所有记录
            //
            var scrapReplyList = _scrapApplyDetailRepository.Query(sql);

            //
            // 从查询结果中提取资产编号列表
            //
            var scrapReplyAssetsNumList = scrapReplyList.Select(X => X.AssetsNo).ToList();


            return scrapReplyAssetsNumList;
        }

        /// <summary>
        /// 根据图片路径查询整条资产
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public AssetsScrapPhoto GetAssetsScrapPhoto(string src)
        {
            var result = _assetsScrapPhotoRepository.FirstOrDefault(x => x.src == src);
            return result;
        }

        /// <summary>
        /// 新增绑定图片参数
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public void AddAssetsScrapPhoto(AssetsScrapPhoto photo)
        {
            try
            {
                _assetsScrapPhotoRepository.Save(photo);
            }
            catch (Exception)
            {
                throw new ArgumentOutOfRangeException("绑定图片参数失败！");
            }

        }

        /// <summary>
        /// 添加分类合并数据
        /// </summary>
        /// <param name="scrapMerge"></param>

        public void AddScrapMerge(ScrapMerge scrapMerge)
        {
            try
            {
                _scrapMergeRepository.Save(scrapMerge);
            }
            catch (Exception)
            {
                throw new ArgumentOutOfRangeException("添加分类合并失败！");
            }
        }

        /// <summary>
        /// 根据EntityId获取一条记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ScrapMerge GetScrapMerge(int entity)
        {
            return _scrapMergeRepository.FirstOrDefault(X => X.EntityId == entity);
        }

        /// <summary>
        /// 批量添加分类合并数据
        /// </summary>
        /// <param name="scrapMerge"></param>

        public void AddScrapMerge(List<ScrapMerge> scrapMergeList)
        {
            try
            {
                _scrapMergeRepository.Add(scrapMergeList);
            }
            catch (Exception)
            {
                throw new ArgumentOutOfRangeException("批量添加分类合并失败！");
            }
        }

        /// <summary>
        /// 删除对同一条报废申请添加的分类合并记录
        /// 因为每次点击分类合并就会产生一次分类合并结果，这个结果是写到数据库中的
        /// 所以当对这同一条报废申请点击多次分类合并按钮，就会生成多个分类合并结果
        /// 故，每次执行分类合并前，先将该报废条目对应的分类合并记录先删除，然后再添加
        /// </summary>
        /// <param name="scrapFormNo">报废申请记录号</param>

        public void RemoveLastScrapMerge(int scrapFormNo)
        {
            try
            {
                var deleteScrapList = _scrapMergeRepository.Query(X => X.ScrapFormNo == scrapFormNo).Select(Y => Y.EntityId);
                _scrapMergeRepository.Remove(deleteScrapList);
            }
            catch (Exception)
            {
                throw new ArgumentOutOfRangeException("批量删除分类合并失败！");
            }
        }

        /// <summary>
        /// 查询分类合并数据
        /// </summary>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public MergePage QueryMergePage(int PageIndex, int PageSize, int FormNum)
        {
            var merGePage = new MergePage();
            try
            {
                //
                // 查询计算机类型
                //
                string sql = string.Format("SELECT * FROM SCRAPMERGE WHERE TYPE = 1 AND SCRAPFORM = {0}", FormNum);
                merGePage.ComputerPagedList = _scrapMergeRepository.PagedList(PageIndex, PageSize, sql);

                //
                // 查询服务器类型
                //
                sql = string.Format("SELECT * FROM SCRAPMERGE WHERE TYPE = 2 AND SCRAPFORM = {0}", FormNum);
                merGePage.ServicePagedList = _scrapMergeRepository.PagedList(PageIndex, PageSize, sql);

                //
                // 查询投影仪类型
                //
                sql = string.Format("SELECT * FROM SCRAPMERGE WHERE TYPE = 3 AND SCRAPFORM = {0}", FormNum);
                merGePage.TouYingPagedList = _scrapMergeRepository.PagedList(PageIndex, PageSize, sql);

                //
                // 查询其它类型
                //
                sql = string.Format("SELECT * FROM SCRAPMERGE WHERE TYPE = 4 AND SCRAPFORM = {0}", FormNum);
                merGePage.OtherPagedList = _scrapMergeRepository.PagedList(PageIndex, PageSize, sql);

                return merGePage;
            }
            catch (Exception)
            {
                throw new ArgumentOutOfRangeException("查询分类合并数据失败！");
            }
        }

        /// <summary>
        /// 查询批复导出列表数据
        /// </summary>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="FormNum"></param>
        /// <returns></returns>
        public PagedList<ScrapMerge> QueryGetOut(int PageIndex, int PageSize)
        {
            try
            {
                //
                // 查询导出列表
                //
                string sql = string.Format("SELECT * FROM SCRAPMERGE WHERE IS_DELET = 0 AND GETOUT <> 0");
                return _scrapMergeRepository.PagedList(PageIndex, PageSize, sql);
            }
            catch (Exception)
            {
                throw new ArgumentOutOfRangeException("查询查询批复导出列表数据失败！");
            }
        }

        /// <summary>
        /// 根据entityId查询SCRAPMERGE
        /// </summary>
        /// <param name="EntityId"></param>
        /// <returns></returns>
        public ScrapMerge FindMergePage(int entityId)
        {
            return _scrapMergeRepository.FirstOrDefault(X => X.EntityId == entityId);
        }


        public void ExportScrapMerge()
        {

        }

    }
}