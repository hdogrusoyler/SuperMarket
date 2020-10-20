using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SuperMarket.Project.BusinessLogic;
using SuperMarket.Project.BusinessLogic.Concrete;
using SuperMarket.Project.DataAccess.EntityFramework;
using SuperMarket.Project.Entity;

namespace SuperMarket.Project.Presentation.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        //private readonly DataContext _context;
        private IProductService productService;
        private ICategoryService categoryService;
        private ICartService cartService;
        private ICartProductService cartProductService;

        public ProductsController()
        {
            //_context = context;
            productService = new ProductManager();
            categoryService = new CategoryManager();
            cartService = new CartManager();
            cartProductService = new CartProductManager();
        }

        // GET: Products
        public IActionResult Index()
        {
            List<Product> list = productService.GetAll();
            return View(list);
        }

        // GET: Products/Details/5
        public IActionResult Details(int id)
        {
            var product = productService.GetById(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(categoryService.GetAll(), "Id", "CategoryName");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,ProductName,CategoryId,StockAmount,Price")] Product product)
        {
            if (ModelState.IsValid)
            {
                productService.Add(product);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(categoryService.GetAll(), "Id", "CategoryName", product.CategoryId);
            return View(product);
        }

        // GET: Products/Edit/5
        public IActionResult Edit(int id)
        {
            var product = productService.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(categoryService.GetAll(), "Id", "CategoryName", product.CategoryId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,ProductName,CategoryId,StockAmount,Price")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    productService.Update(product);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(categoryService.GetAll(), "Id", "CategoryName", product.CategoryId);
            return View(product);
        }

        // GET: Products/Delete/5
        public IActionResult Delete(int id)
        {
            var product = productService.GetById(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var product = productService.GetById(id);
            productService.Delete(product);
            return RedirectToAction(nameof(Index));
        }

        // POST: Products/AddToCart/5
        [HttpPost, ActionName("AddToCart")]
        [ValidateAntiForgeryToken]
        public IActionResult AddToCart(int id)
        {
            var idf = User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier)
               .Select(c => c.Value).SingleOrDefault();
            Cart cart = cartService.GetLastCartIsNotPaid(Convert.ToInt32(idf));
            if (cart == null)
            {
                cartService.Add(new Cart() { UserId = Convert.ToInt32(idf) });
                cartService = new CartManager();
                cart = cartService.GetLastCartIsNotPaid(Convert.ToInt32(idf));
            }
            CartProduct cartProduct = cartProductService.GetByProductId(id, cart.Id);
            if (cartProduct != null)
            {
                cartProduct.ProductAmount += 1;
                cartProductService.Update(cartProduct);
            }
            else
            {
                cartProductService.Add(new CartProduct() { CartId = cart.Id, ProductId = Convert.ToInt32(id), ProductAmount = 1 });
            }
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            bool result = false;
            Product product = productService.GetById(id);
            if (product != null)
            {
                result = true;
            }
            return result;
        }
    }
}
