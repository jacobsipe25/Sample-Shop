using System.Linq;
using MyShop.Core;

namespace MyShop.Core.Contracts
{  //this is simply a way to pull out methods from the base class so nothing screws when we add methods, we can't delete them though
    public interface IInMemoryRepository<P> where P : BaseEntity
    {
        IQueryable<P> Collection();
        void Commit();
        void Delete(string Id);
        P Find(string Id);
        void Insert(P p);
        void Update(P p);
    }
}