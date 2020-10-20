using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuperMarket.Project.BusinessLogic;
using SuperMarket.Project.BusinessLogic.Concrete;
using SuperMarket.Project.Entity;

namespace SuperMarket.Project.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IProductService productService;
        public ProductsController()
        {
            productService = new ProductManager();
        }

        [HttpGet]
        public ActionResult GetList()
        {
            return Ok(productService.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult Get(int Id)
        {
            return Ok(productService.GetById(Id));
        }

        [Route("Add")]
        [HttpPost]
        public ActionResult Add([FromBody] Product product)
        {
            return Ok(productService.Add(product));
        }

        [Route("Update")]
        [HttpPost]
        public ActionResult Update([FromBody] Product product)
        {
            return Ok(productService.Update(product));
        }

        [Route("Delete")]
        [HttpPost]
        public ActionResult Delete([FromBody] Product product)
        {
            return Ok(productService.Delete(product));
        }

        [Route("Delete/{id}")]
        [HttpPost]
        public ActionResult Delete(int Id)
        {
            return Ok(productService.Delete(new Product { Id = Id }));
        }
    }
}
