using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EAM.Data.Comm;
using EAM.Data.Domain;
using EAM.Data.Services.Query;

// 2017-05-27 wnn
namespace EAM.Data.Services
{
    /// <summary>
    /// 提供角色管理相关操作
    /// </summary>
    public interface IRoleService
    {
        List<Role> GetAll();
        Role GetRole(string Role);


        Role GetRole(int id);
        void AddRole(Role item);
        void UpdateRole(Role item);
        void DeleteRole(int id);
        decimal GetIDByRole(string roles);

        int ifexitRole(int id);
    }
}