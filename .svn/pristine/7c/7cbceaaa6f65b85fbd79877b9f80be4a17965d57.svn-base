using System;
using System.Collections.Generic;
using System.Text;
using EAM.Data.Domain;
using EAM.Data.Repositories;

namespace EAM.Data.Services
{
    public class GeneralService
    {
        private readonly IGeneralRepository _assetsGeneralRep;

        public GeneralService(IGeneralRepository assetsGeneralRep)
        {
            _assetsGeneralRep = assetsGeneralRep;
        }

        public void AddAssetsGeneral(GeneralAttribute item)
        {
            _assetsGeneralRep.Add(item);
        }


    }
}