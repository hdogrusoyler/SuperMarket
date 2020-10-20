using SuperMarket.Project.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperMarket.Project.BusinessLogic
{
    public interface ICartProductService
    {
        CartProduct GetById(int Id);
        CartProduct GetByProductId(int Id, int CartId, int page = 1, int pageSize = 0);
        List<CartProduct> GetAll(int page = 1, int pageSize = 0);
        string Add(CartProduct entity);
        string Update(CartProduct entity);
        string Delete(CartProduct entity);
    }
}
