using Microsoft.EntityFrameworkCore;
using SuperMarket.Project.DataAccess;
using SuperMarket.Project.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuperMarket.Project.BusinessLogic.Concrete
{
    public class CartManager : ICartService
    {
        IUnitOfWork _unitOfWork;
        public CartManager()
        {
            _unitOfWork = new UnitOfWork();
        }

        public List<Cart> GetAll(int page = 1, int pageSize = 0)
        {
            List<Cart> res = new List<Cart>();
            res = _unitOfWork.efCartDal.GetList(null, (qry) => qry.OrderByDescending(x => x.Id), page, pageSize, i => i.Include(c => c.CartProducts));
            return res;
        }

        public Cart GetById(int Id)
        {
            Cart res = new Cart();
            res = _unitOfWork.efCartDal.Get(c => c.Id == Id, i => i.Include(c => c.CartProducts).ThenInclude(i => i.Product).ThenInclude(c => c.Category).Include(t => t.User));
            return res;
        }

        public Cart GetLastCartIsNotPaid(int userId, int page = 1, int pageSize = 0)
        {
            Cart res = new Cart();
            res = _unitOfWork.efCartDal.GetList(c => c.isPaymentCompleted == false && c.UserId == userId, (qry) => qry.OrderByDescending(x => x.Id), page, pageSize, i => i.Include(p => p.CartProducts).ThenInclude(t => t.Product).ThenInclude(c => c.Category).Include(u => u.User)).LastOrDefault();
            return res;
        }

        public string Add(Cart entity)
        {
            _unitOfWork.BeginTransaction();
            _unitOfWork.efCartDal.Add(entity);
            return _unitOfWork.CommitSaveChanges();
        }

        public string Update(Cart entity)
        {
            _unitOfWork.BeginTransaction();
            _unitOfWork.efCartDal.Update(entity);
            return _unitOfWork.CommitSaveChanges();
        }

        public string Delete(Cart entity)
        {
            _unitOfWork.BeginTransaction();
            _unitOfWork.efCartDal.Delete(entity);
            return _unitOfWork.CommitSaveChanges();
        }
    }
}
