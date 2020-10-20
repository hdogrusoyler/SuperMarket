using System;
using System.Collections.Generic;
using System.Text;

namespace SuperMarket.Project.DataAccess
{
    public interface IUnitOfWork
    {
        public IUserDal efUserDal { get; set; }
        public IProductDal efProductDal { get; set; }
        public ICategoryDal efCategoryDal { get; set; }
        public ICartDal efCartDal { get; set; }
        public ICartProductDal efCartProductDal { get; set; }
        public ISalesInformationDal efSalesInformationDal { get; set; }
        public IPaymentTypeDal efPaymentTypeDal { get; set; }
        void BeginTransaction();
        string CommitSaveChanges();
        //void Save();
        //void Dispose();
    }
}
