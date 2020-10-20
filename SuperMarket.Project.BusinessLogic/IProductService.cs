using SuperMarket.Project.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperMarket.Project.BusinessLogic
{
    public interface IProductService
    {
        Product GetById(int Id);
        List<Product> GetAll(int page = 1, int pageSize = 0);
        string Add(Product entity);
        string Update(Product entity);
        string Delete(Product entity);
    }
}
