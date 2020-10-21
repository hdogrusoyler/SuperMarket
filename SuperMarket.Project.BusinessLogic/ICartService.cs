using SuperMarket.Project.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperMarket.Project.BusinessLogic
{
    public interface ICartService
    {
        Cart GetById(int Id);
        Cart GetLastCartIsNotPaid(int userId, int page = 1, int pageSize = 0);
        List<Cart> GetAll(int page = 1, int pageSize = 0);
        Cart AddAndGetLastCartIsNotPaid(Cart cart, int page = 1, int pageSize = 0);
        void CheckCartAndAddProduct(int ProductId, int UserId, int page = 1, int pageSize = 0);
        string UpdateAndSalesInformationAdd(Cart cart, SalesInformation salesInfo);
        string Add(Cart entity);
        string Update(Cart entity);
        string Delete(Cart entity);
    }
}
