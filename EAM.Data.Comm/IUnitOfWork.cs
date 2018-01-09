using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EAM.Data.Comm
{
    /// <summary>
    /// 工作单元
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// 启动
        /// </summary>
        void Start();

        /// <summary>
        /// 提交更新
        /// </summary>
        void Commit();
    }
}