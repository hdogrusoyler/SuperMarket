using Microsoft.EntityFrameworkCore;
using SuperMarket.Project.DataAccess;
using SuperMarket.Project.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuperMarket.Project.BusinessLogic.Concrete
{
    public class CartProductManager : ICartProductService
    {
        IUnitOfWork _unitOfWork;
        public CartProductManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<CartProduct> GetAll(int page = 1, int pageSize = 0)
        {
            List<CartProduct> res = new List<CartProduct>();
            res = _unitOfWork.efCartProductDal.GetList(null, (qry) => qry.OrderByDescending(x => x.Id), page, pageSize);
            return res;
        }

        public CartProduct GetById(int Id)
        {
            CartProduct res = new CartProduct();
            res = _unitOfWork.efCartProductDal.Get(c => c.Id == Id,cp=>cp.Include(p=>p.Product).ThenInclude(c=>c.Category));
            return res;
        }

        public CartProduct GetByProductId(int Id, int CartId, int page = 1, int pageSize = 0)
        {
            CartProduct res = new CartProduct();
            res = _unitOfWork.efCartProductDal.GetList(f => f.ProductId == Id && f.Cart.Id == CartId, (qry) => qry.OrderByDescending(x => x.Id), page, pageSize, cp=>cp.Include(p => p.Product).ThenInclude(c => c.Category)).FirstOrDefault();
            return res;
        }

        public string Add(CartProduct entity)
        {
            _unitOfWork.BeginTransaction();
            _unitOfWork.efCartProductDal.Add(entity);
            return _unitOfWork.CommitSaveChanges();
        }

        public string Update(CartProduct entity)
        {
            _unitOfWork.BeginTransaction();
            _unitOfWork.efCartProductDal.Update(entity);
            return _unitOfWork.CommitSaveChanges();
        }

        public string Delete(CartProduct entity)
        {
            _unitOfWork.BeginTransaction();
            _unitOfWork.efCartProductDal.Delete(entity);
            return _unitOfWork.CommitSaveChanges();
        }
    }
}
