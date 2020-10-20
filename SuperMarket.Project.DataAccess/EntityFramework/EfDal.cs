using Project.Core.DataAccess.EntityFramework;
using SuperMarket.Project.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperMarket.Project.DataAccess.EntityFramework
{
    public class EfUserDal : EfRepositoryBase<User, DataContext>, IUserDal
    {
        public EfUserDal(DataContext dbContext) : base(dbContext)
        {

        }
    }
    public class EfProductDal : EfRepositoryBase<Product, DataContext>, IProductDal
    {
        public EfProductDal(DataContext dbContext) : base(dbContext)
        {

        }
    }
    public class EfCategoryDal : EfRepositoryBase<Category, DataContext>, ICategoryDal
    {
        public EfCategoryDal(DataContext dbContext) : base(dbContext)
        {

        }
    }
    public class EfCartDal : EfRepositoryBase<Cart, DataContext>, ICartDal
    {
        public EfCartDal(DataContext dbContext) : base(dbContext)
        {

        }
    }
    public class EfCartProductDal : EfRepositoryBase<CartProduct, DataContext>, ICartProductDal
    {
        public EfCartProductDal(DataContext dbContext) : base(dbContext)
        {

        }
    }
    public class EfSalesInformationDal : EfRepositoryBase<SalesInformation, DataContext>, ISalesInformationDal
    {
        public EfSalesInformationDal(DataContext dbContext) : base(dbContext)
        {

        }
    }
    public class EfPaymentTypeDal : EfRepositoryBase<PaymentType, DataContext>, IPaymentTypeDal
    {
        public EfPaymentTypeDal(DataContext dbContext) : base(dbContext)
        {

        }
    }
}
