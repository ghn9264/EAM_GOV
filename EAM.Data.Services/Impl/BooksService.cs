using System;
using System.Collections.Generic;
using System.Text;
using EAM.Data.Domain;
using EAM.Data.Repositories;

namespace EAM.Data.Services
{
    public class BooksService
    {

        private readonly IBooksRepository _assetsBooksRep;

        public BooksService(IBooksRepository assetsBooksRep)
        {
            _assetsBooksRep = assetsBooksRep;
        }

        public void AddAssetsBooks(BooksAttribute item)
        {
            _assetsBooksRep.Add(item);
        }
    }
}