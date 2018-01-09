using EAM.Data.Domain;
using EAM.Data.Repositories;

namespace EAM.Data.Services
{
    public class SpecialService
    {
        private readonly ISpecialRepository _specialRep;

        public SpecialService(ISpecialRepository specialRep)
        {
            _specialRep = specialRep;
        }

        public void AddAssetsSpecial(SpecialAttribute item)
        {
            _specialRep.Add(item);
        }

    }
}