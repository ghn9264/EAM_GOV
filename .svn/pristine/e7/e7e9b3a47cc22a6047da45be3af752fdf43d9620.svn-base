 using System.Collections.Generic;
using EAM.Data.Domain;

namespace EAM.Data.Services
{
    public interface ISystemService
    {
        #region 部门管理

        void AddDept(Department dept);
        void Update(Department dept);
        void Delete(int deptId);
        List<Department> GetAll();

        #endregion

        #region 位置管理

        void AddPlace(Place item);
        void UpdatePlace(Place item);
        void DeletePlace(int itemId);

        Place GetPlaceOne(string parent);
        List<Place> GetAllPlace(string placeType);

        #endregion
    }
}