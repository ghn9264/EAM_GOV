using System;
using System.Collections.Generic;
using System.Text;
using EAM.Data.Domain;
using EAM.Data.Repositories;

namespace EAM.Data.Services
{
    public class CulturalrelicService
    {

        private readonly ICulturalrelicRepository _assetsCulturalrelicRep;

        public CulturalrelicService(ICulturalrelicRepository assetsCulturalrelicRep)
        {
            _assetsCulturalrelicRep = assetsCulturalrelicRep;
        }

        public void AddAssetsCulturalrelic(CulturalrelicAttribute item)
        {
            _assetsCulturalrelicRep.Add(item);
        }


    }
}