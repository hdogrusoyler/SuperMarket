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
        public CartManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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

        public Cart AddAndGetLastCartIsNotPaid(Cart entity, int page = 1, int pageSize = 0)
        {
            Cart cart = new Cart();
            _unitOfWork.BeginTransaction();
            _unitOfWork.efCartDal.Add(entity);
            _unitOfWork.Save();
            cart = _unitOfWork.efCartDal.GetList(c => c.isPaymentCompleted == false && c.UserId == entity.UserId, (qry) => qry.OrderByDescending(x => x.Id), page, pageSize, i => i.Include(p => p.CartProducts).ThenInclude(t => t.Product).ThenInclude(c => c.Category).Include(u => u.User)).LastOrDefault();
            //_unitOfWork.CommitSaveChanges();
            return cart;
        }

        public string UpdateAndSalesInformationAdd(Cart cart, SalesInformation salesInfo)
        {

            _unitOfWork.BeginTransaction();
            _unitOfWork.efSalesInformationDal.Add(salesInfo);
            cart.isPaymentCompleted = true;
            _unitOfWork.efCartDal.Update(cart);
            return _unitOfWork.CommitSaveChanges();
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

        public void CheckCartAndAddProduct(int ProductId, int UserId, int page = 1, int pageSize = 0)
        {
            _unitOfWork.BeginTransaction();
            Cart cart = new Cart();
            cart = _unitOfWork.efCartDal.GetList(c => c.isPaymentCompleted == false && c.UserId == UserId, (qry) => qry.OrderByDescending(x => x.Id), page, pageSize, i => i.Include(p => p.CartProducts).ThenInclude(t => t.Product).ThenInclude(c => c.Category).Include(u => u.User)).LastOrDefault();
            CartProduct cartProduct = new CartProduct();
            if (cart == null)
            {
                cart = new Cart() { UserId = Convert.ToInt32(UserId) };
                //_unitOfWork.BeginTransaction();
                _unitOfWork.efCartDal.Add(cart);
                _unitOfWork.Save();
                cart = _unitOfWork.efCartDal.GetList(c => c.isPaymentCompleted == false && c.UserId == cart.UserId, (qry) => qry.OrderByDescending(x => x.Id), page, pageSize, i => i.Include(p => p.CartProducts).ThenInclude(t => t.Product).ThenInclude(c => c.Category).Include(u => u.User)).LastOrDefault();

                
                //_unitOfWork.CommitSaveChanges();
                //cart = cartService.GetLastCartIsNotPaid(ent.UserId);
            }
            cartProduct = _unitOfWork.efCartProductDal.GetList(f => f.ProductId == ProductId && f.Cart.Id == cart.Id, (qry) => qry.OrderByDescending(x => x.Id), page, pageSize, cp => cp.Include(p => p.Product).ThenInclude(c => c.Category)).FirstOrDefault();
            if (cartProduct != null)
            {
                cartProduct.ProductAmount += 1;
                _unitOfWork.efCartProductDal.Update(cartProduct);
            }
            else
            {
                //_unitOfWork.BeginTransaction();
                cartProduct = new CartProduct();
                cartProduct.CartId = cart.Id;
                cartProduct.ProductId = ProductId;
                cartProduct.ProductAmount = 1;
                _unitOfWork.efCartProductDal.Add(cartProduct);
            }
            _unitOfWork.CommitSaveChanges();
        }
    }
}
