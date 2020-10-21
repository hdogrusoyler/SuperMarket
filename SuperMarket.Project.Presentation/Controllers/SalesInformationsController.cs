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
    public class SalesInformationsController : Controller
    {
        private ISalesInformationService salesInformationService;

        public SalesInformationsController(ISalesInformationService _salesInformationService)
        {
            salesInformationService = _salesInformationService;
        }

        // GET: SalesInformations
        public IActionResult Index()
        {
            return View(salesInformationService.GetAll());
        }

        // GET: SalesInformations/Details/5
        public IActionResult Details(int id)
        {
            var salesInformation = salesInformationService.GetById(id);
            if (salesInformation == null)
            {
                return NotFound();
            }

            return View(salesInformation);
        }

        private bool SalesInformationExists(int id)
        {
            bool result = false;
            SalesInformation product = salesInformationService.GetById(id);
            if (product != null)
            {
                result = true;
            }
            return result;
        }
    }
}
