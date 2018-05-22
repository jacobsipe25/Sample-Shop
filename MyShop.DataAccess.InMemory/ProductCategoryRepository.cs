using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using MyShop.Core.Models;


namespace MyShop.DataAccess.InMemory
{
    public class ProductCategoryRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<ProductCategory> productCat;
        public ProductCategoryRepository()
        {
            productCat = cache["productCat"] as List<ProductCategory>;
            if (productCat == null)
            {
                productCat = new List<ProductCategory>();
            }
        }
        public void Commit()
        {
            cache["productCat"] = productCat;
        }
        public void Insert(ProductCategory p)
        {
            productCat.Add(p);
        }
        public void Update(ProductCategory productcategory)
        {
            ProductCategory productcatToUpdate = productCat.Find(p => p.Id == productcategory.Id);//Product paramerter, bool
            if (productcatToUpdate != null)
            {
                productcatToUpdate = productcategory;
            }
            else
            {
                throw new Exception("Product not found");
            }
        }
        public ProductCategory Find(string ID)
        {
            ProductCategory productcatToUpdate = productCat.Find(p => p.Id == ID);
            if (productcatToUpdate != null)
            {
                return productcatToUpdate;
            }
            else
            {
                throw new Exception("Product not found");
            }

        }
        public IQueryable<ProductCategory> Collection()
        {
            return productCat.AsQueryable();
        }
        public void Delete(String Id)
        {
            ProductCategory productcatToDelete = productCat.Find(p => p.Id == Id);
            if (productcatToDelete != null)
            {
                productCat.Remove(productcatToDelete);
            }
            else
            {
                throw new Exception("Product not found");
            }

        }
    }
}
