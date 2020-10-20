using SuperMarket.Project.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperMarket.Project.BusinessLogic
{
    public interface IPaymentTypeService
    {
        PaymentType GetById(int Id);
        List<PaymentType> GetAll(int page = 1, int pageSize = 0);
        string Add(PaymentType entity);
        string Update(PaymentType entity);
        string Delete(PaymentType entity);
    }
}
