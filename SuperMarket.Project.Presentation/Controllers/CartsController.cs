using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SuperMarket.Project.BusinessLogic;
using SuperMarket.Project.BusinessLogic.Concrete;
using SuperMarket.Project.DataAccess.EntityFramework;
using SuperMarket.Project.Entity;

namespace SuperMarket.Project.Presentation.Controllers
{
    public class CartsController : Controller
    {
        private ICartService cartService;
        private ICartProductService cartProductService;
        private ISalesInformationService salesInformationService;
        private IPaymentTypeService paymentTypeService;
        private IUserService userService;

        public CartsController()
        {
            cartService = new CartManager();
            cartProductService = new CartProductManager();
            salesInformationService = new SalesInformationManager();
            paymentTypeService = new PaymentTypeManager();
            userService = new UserManager();
        }

        // GET: Carts
        public IActionResult Index()
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
            //List<Cart> list = cartService.GetAll();
            return View(cart);
        }

        // GET: Carts/Delete/5
        public IActionResult Delete(int id)
        {
            CartProduct cart = cartProductService.GetById(id);
            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }

        // POST: Carts/Delete/5
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var cart = cartProductService.GetById(id);
            cartProductService.Delete(cart);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult CompleteCart(int id)
        {
            var list = new SelectList(paymentTypeService.GetAll(), "Id", "PaymentTypeName");
            ViewData["PaymentTypeId"] = new SelectList(paymentTypeService.GetAll(), "Id", "PaymentTypeName");
            Cart cart = cartService.GetById(id);
            if (cart == null)
            {
                return NotFound();
            }
            return View(cart);
        }

        [HttpPost, ActionName("CompleteCart")]
        public IActionResult CompleteCartSales(int id, int paymentTypeId)
        {
            var cart = cartService.GetById(id);
            int total = 0;
            foreach (var item in cart.CartProducts)
            {
                total += item.ProductAmount * item.Product.Price;
            }
            salesInformationService.Add(new SalesInformation() { CartId = cart.Id, PaymentTypeId = paymentTypeId, TotalPrice= total });
            cart.isPaymentCompleted = true;
            cartService.Update(cart);
            return RedirectToAction(nameof(Index), "Products");
        }

        private bool CartExists(int id)
        {
            bool result = false;
            Cart cart = cartService.GetById(id);
            if (cart != null)
            {
                result = true;
            }
            return result;
        }
    }
}
