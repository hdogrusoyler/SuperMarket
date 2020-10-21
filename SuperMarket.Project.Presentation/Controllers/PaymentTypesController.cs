using System;
using System.Collections.Generic;
using System.Linq;
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
    public class PaymentTypesController : Controller
    {
        private IPaymentTypeService paymentTypeService;

        public PaymentTypesController(IPaymentTypeService _paymentTypeService)
        {
            paymentTypeService = _paymentTypeService;
        }

        // GET: PaymentTypes
        public IActionResult Index()
        {
            return View(paymentTypeService.GetAll());
        }

        // GET: PaymentTypes/Details/5
        public IActionResult Details(int id)
        {
            var paymentType = paymentTypeService.GetById(id);
            if (paymentType == null)
            {
                return NotFound();
            }

            return View(paymentType);
        }

        // GET: PaymentTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PaymentTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public IActionResult Create([Bind("Id,PaymentTypeName")] PaymentType paymentType)
        {
            if (ModelState.IsValid)
            {
                paymentTypeService.Add(paymentType);
                return RedirectToAction(nameof(Index));
            }
            return View(paymentType);
        }

        // GET: PaymentTypes/Edit/5
        public IActionResult Edit(int id)
        {
            var paymentType = paymentTypeService.GetById(id);
            if (paymentType == null)
            {
                return NotFound();
            }
            return View(paymentType);
        }

        // POST: PaymentTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public IActionResult Edit(int id, [Bind("Id,PaymentTypeName")] PaymentType paymentType)
        {
            if (id != paymentType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    paymentTypeService.Update(paymentType);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaymentTypeExists(paymentType.Id))
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
            return View(paymentType);
        }

        // GET: PaymentTypes/Delete/5
        public IActionResult Delete(int id)
        {
            var paymentType = paymentTypeService.GetById(id);
            if (paymentType == null)
            {
                return NotFound();
            }

            return View(paymentType);
        }

        // POST: PaymentTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var paymentType = paymentTypeService.GetById(id);
            paymentTypeService.Delete(paymentType);
            return RedirectToAction(nameof(Index));
        }

        private bool PaymentTypeExists(int id)
        {
            bool result = false;
            PaymentType product = paymentTypeService.GetById(id);
            if (product != null)
            {
                result = true;
            }
            return result;
        }
    }
}
