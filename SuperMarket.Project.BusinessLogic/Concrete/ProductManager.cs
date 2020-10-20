using Microsoft.EntityFrameworkCore;
using SuperMarket.Project.DataAccess;
using SuperMarket.Project.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuperMarket.Project.BusinessLogic.Concrete
{
    public class ProductManager : IProductService
    {
        IUnitOfWork _unitOfWork;
        public ProductManager()
        {
            _unitOfWork = new UnitOfWork();
        }

        public List<Product> GetAll(int page = 1, int pageSize = 0)
        {
            List<Product> res = new List<Product>();
            res = _unitOfWork.efProductDal.GetList(null, (qry) => qry.OrderByDescending(x => x.Id), page, pageSize, i => i.Include(d => d.Category));
            return res;
        }

        public Product GetById(int Id)
        {
            Product res = new Product();
            res = _unitOfWork.efProductDal.Get(c => c.Id == Id);
            return res;
        }

        public string Add(Product entity)
        {
            _unitOfWork.BeginTransaction();
            _unitOfWork.efProductDal.Add(entity);
            return _unitOfWork.CommitSaveChanges();
        }

        public string Update(Product entity)
        {
            _unitOfWork.BeginTransaction();
            _unitOfWork.efProductDal.Update(entity);
            return _unitOfWork.CommitSaveChanges();
        }

        public string Delete(Product entity)
        {
            _unitOfWork.BeginTransaction();
            _unitOfWork.efProductDal.Delete(entity);
            return _unitOfWork.CommitSaveChanges();
        }
    }
}
