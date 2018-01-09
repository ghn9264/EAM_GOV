using System;
using System.Collections.Generic;
using System.Text;
using EAM.Data.Domain;
using EAM.Data.Repositories;

namespace EAM.Data.Services
{
    public class CarService
    {
        private readonly ICarRepository _assetsCarRep;

        public CarService(ICarRepository assetsCarRep)
        {
            _assetsCarRep = assetsCarRep;
        }

        public void AddAssetsCar(CarAttribute item)
        {
            _assetsCarRep.Add(item);
        }


    }
}