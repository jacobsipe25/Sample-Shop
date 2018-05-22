using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Models;
using MyShop.DataAccess.InMemory;


namespace MyShop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        ProductRepository context;//creates an object instance of the Repo 
        public ProductManagerController() //constructor method
        {
            context = new ProductRepository(); //full out basically a list of all of Products
        }

        // GET: ProductManager
        public ActionResult Index()
        {
            List<Product> products = context.Collection().ToList();
            return View(products);
        }
        public ActionResult Create()
        {
            Product product = new Product();
            return View(product);
        }
        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            else
            {
                context.Insert(product);
                context.Commit();
                return RedirectToAction("Index");
            }
            }
        public ActionResult Edit(string Id)
        {
            Product product = context.Find(Id);// this uses our find method in the Repo
            if (product == null)
            {
                return HttpNotFound();

            }
            else
            {
                return View(product);
            }

        }
        [HttpPost]
        public ActionResult Edit(Product product, string ID)
        {
            Product productToEdit = context.Find(ID);
            if (productToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(product);
                }
                productToEdit.Category = product.Category;
                productToEdit.Description = product.Description;
                productToEdit.Image = product.Image;
                productToEdit.Name = product.Name;
                productToEdit.Price = product.Price;

                context.Commit();
                return RedirectToAction("Index");
            }
            
        }
        public ActionResult Delete(string ID)
        {
            Product producttoremove = context.Find(ID);
            if (producttoremove == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(producttoremove);
            }
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string ID)
        {
            Product producttoremove = context.Find(ID);
            if (producttoremove == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Delete(ID);
                context.Commit();
                return RedirectToAction("Index");
            }
        }

        }
    
    }

