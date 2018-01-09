using System;
using System.Collections.Generic;
using System.Text;
using EAM.Data.Domain;
using EAM.Data.Repositories;

namespace EAM.Data.Services
{
    public class LandService
    {

        private readonly ILandRepository _assetsLandRep;

        public LandService(ILandRepository assetsLandRep)
        {
            _assetsLandRep = assetsLandRep;
        }

        public void AddAssetsLand(LandAttribute item)
        {
            _assetsLandRep.Add(item);
        }


    }
}