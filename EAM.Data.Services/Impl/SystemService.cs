using System.Collections.Generic;
using EAM.Data.Domain;
using EAM.Data.Repositories;

namespace EAM.Data.Services.Impl
{
    public class SystemService : ISystemService
    {
        private readonly IDepartmentRepository _deptRep;
        private readonly IPlaceRepository _placeRep;

        public SystemService(IDepartmentRepository deptRep,
            IPlaceRepository placeRep)
        {
            _deptRep = deptRep;
            _placeRep = placeRep;
        }

        #region 部门管理
        public void AddDept(Department dept)
        {
            _deptRep.Add(dept);
        }

        public void Update(Department dept)
        {
            _deptRep.Update(dept);
        }

        public void Delete(int deptId)
        {
            _deptRep.Remove(deptId);
        }

        public List<Department> GetAll()
        {
            return _deptRep.Query("SELECT d.*,p.DEPT_NAME as PARENT_NAME from department d left JOIN department p on d.PARENT_ID=p.ID");
        }

        #endregion

        #region Place
        public void AddPlace(Place item)
        {
            _placeRep.Add(item);
        }

        public void UpdatePlace(Place item)
        {
            _placeRep.Update(item);

        }

        public void DeletePlace(int itemId)
        {
            _placeRep.Remove(itemId);
        }

        public Place GetPlaceOne(string parent)
        {
            var sql = "select * from place where PLACE_NAME='" + parent + "'";
            return _placeRep.FirstOrDefault(sql);
        }

        public List<Place> GetAllPlace(string placeType)
        {
            var sql = "SELECT p.*,pp.`PLACE_CODE` AS PARENT_PLACE_CODE,pp.`PLACE_NAME` AS PARENT_PLACE_NAME FROM `place` p LEFT JOIN `place` pp ON p.PARENT_ID=pp.ID WHERE p.PLACE_TYPE=@0";
            return _placeRep.Query(sql, placeType);
        }
        #endregion

    }
}