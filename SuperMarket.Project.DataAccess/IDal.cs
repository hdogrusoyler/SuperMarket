using Project.Core.DataAccess;
using SuperMarket.Project.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperMarket.Project.DataAccess
{
    public interface IUserDal : IRepository<User>
    {
    }
    public interface IProductDal : IRepository<Product>
    {
    }
    public interface ICategoryDal : IRepository<Category>
    {
    }
    public interface ICartDal : IRepository<Cart>
    {
    }
    public interface ICartProductDal : IRepository<CartProduct>
    {
    }
    public interface ISalesInformationDal : IRepository<SalesInformation>
    {
    }
    public interface IPaymentTypeDal : IRepository<PaymentType>
    {
    }
}
