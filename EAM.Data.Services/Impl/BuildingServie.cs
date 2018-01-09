using System;
using System.Collections.Generic;
using System.Text;
using EAM.Data.Domain;
using EAM.Data.Repositories;

namespace EAM.Data.Services
{
    public class BuildingServie
    {

        private readonly IBuildingRepository _assetsBuildingRep;

        public BuildingServie(IBuildingRepository assetsBuildingRep)
        {
            _assetsBuildingRep = assetsBuildingRep;
        }
        public void AddAssetsBuilding(BuildingAttribute item)
        {
            _assetsBuildingRep.Add(item);
        }


    }
}