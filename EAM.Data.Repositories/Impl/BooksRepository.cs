using EAM.Data.Comm;
using EAM.Data.Domain;

namespace EAM.Data.Repositories.Impl
{
    public class BooksRepository : Repository<BooksAttribute, int>, IBooksRepository
    {
        public BooksRepository(EamDatabase database)
            : base(database)
        {

        }

        public void Save(BooksAttribute attr)
        {
            Db.Save<BooksAttribute>(attr);
        }
    }
     
}
