﻿using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.DataAccess.InMemory;
using MyShop.Core.Contracts;

namespace MyShop.WebUI.Controllers
{
    public class ProductCategoryManagerController : Controller
    {
        // GET: ProductCategoryManager

       IInMemoryRepository<ProductCategory> context;//creates an object instance of the Repo 
       public ProductCategoryManagerController(IInMemoryRepository<ProductCategory> context) //constructor method
         {
           this.context=context; //full out basically a list of all of Products
         }
                  // GET: ProductManager
       public ActionResult Index()
       {
       List<ProductCategory> productcats = context.Collection().ToList();
       return View(productcats);
       }
       public ActionResult Create()
       {
       ProductCategory productcat = new ProductCategory();
       return View(productcat);
       }
       [HttpPost]

       public ActionResult Create(ProductCategory productcat)
       {
            if (!ModelState.IsValid)
            {
                return View(productcat);
            }
            else
            {
                context.Insert(productcat);
                context.Commit();
                return RedirectToAction("Index");
            }
       }
       public ActionResult Edit(string Id)
       {
          ProductCategory productcat = context.Find(Id);// this uses our find method in the Repo
          if (productcat == null)
           {
              return HttpNotFound();

           }
          else
           {
              return View(productcat);
           }

        }
        [HttpPost]
       public ActionResult Edit(ProductCategory productcat, string ID)
       {
         ProductCategory productcatToEdit = context.Find(ID);
         if (productcatToEdit == null)
           {
             return HttpNotFound();
           }
         else
            {
              if (!ModelState.IsValid)
              {
                    return View(productcat);
              }
                productcatToEdit.Category = productcat.Category;
                this.context.Commit();
              return RedirectToAction("Index");
            }

        }
        public ActionResult Delete(string ID)
        {
            ProductCategory productcattoremove = this.context.Find(ID);
            if (productcattoremove == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productcattoremove);
            }
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string ID)
        {
            ProductCategory productcattoremove = context.Find(ID);
            if (productcattoremove == null)
            {
                return HttpNotFound();
            }
            else
            {
                this.context.Delete(ID);
                this.context.Commit();
                return RedirectToAction("Index");
            }
        }
         
        
    }
}