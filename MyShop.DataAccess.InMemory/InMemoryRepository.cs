using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using MyShop.Core;
using MyShop.Core.Models;
namespace MyShop.DataAccess.InMemory
{
    public class InMemoryRepository<P> where P : BaseEntity
    {
        ObjectCache cache = MemoryCache.Default;
        List<P> items;
        string className;
        public InMemoryRepository()
        {
            className = typeof(P).Name;
            items = cache[className] as List<P>;
            if (items == null)
            {
                items = new List<P>();

            }
        }
        public void Commit()
        {
            cache[className] = items;

        }
        public void Insert(P p)
        {
            items.Add(p);
        }
        public void Update(P p)
        {
            P ptoUpdate = items.Find(i => i.Id == p.Id);
            if (ptoUpdate != null)
            {
                ptoUpdate = p;
            }
            else
            {
                throw new Exception(className + "Not found");
            }
        }
        public P Find(string Id)
        {
            P p = items.Find(i => i.Id == Id);
            if (p != null)
            {
                return p;
            }
            else
            {
                throw new Exception(className + "Not found");
            }

        }
        public IQueryable<P> Collection()
        {
            return items.AsQueryable();

        }
        public void Delete(string Id)
        {
            P ptodelete = items.Find(i => i.Id == Id);//class doesn't know what the ID is 
            if (ptodelete != null)
            {
                items.Remove(ptodelete);
            }
            else
            {
                throw new Exception(className + "Not found");
            }
        }
    }
}
