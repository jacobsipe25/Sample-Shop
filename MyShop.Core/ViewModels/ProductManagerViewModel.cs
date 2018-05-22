using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.ViewModels
{
   public class ProductManagerViewModel
    {
        public Product Product { get; set; } //This takes everything from the product model
        public IEnumerable<ProductCategory> ProductCategories { get; set; } //queryset for the Product Categories


    }
}
