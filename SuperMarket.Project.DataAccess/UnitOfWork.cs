using Microsoft.EntityFrameworkCore.Storage;
using SuperMarket.Project.DataAccess.EntityFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperMarket.Project.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDbContextTransaction _transaction;
        private DataContext _context;
        public IUserDal efUserDal { get; set; }
        public IProductDal efProductDal { get; set; }
        public ICategoryDal efCategoryDal { get; set; }
        public ICartDal efCartDal { get; set; }
        public ICartProductDal efCartProductDal { get; set; }
        public ISalesInformationDal efSalesInformationDal { get; set; }
        public IPaymentTypeDal efPaymentTypeDal { get; set; }
        public UnitOfWork()
        {
            _context = new DataContext();
            efUserDal = new EfUserDal(_context);
            efProductDal = new EfProductDal(_context);
            efCategoryDal = new EfCategoryDal(_context);
            efCartDal = new EfCartDal(_context);
            efCartProductDal = new EfCartProductDal(_context);
            efSalesInformationDal = new EfSalesInformationDal(_context);
            efPaymentTypeDal = new EfPaymentTypeDal(_context);
        }
        public void BeginTransaction()
        {
            _transaction = _context.Database.BeginTransaction();
        }
        public string CommitSaveChanges()
        {
            string result = "";
            try
            {
                result = Save().ToString();
                _transaction.Commit();
            }
            catch (Exception e)
            {
                _transaction.Rollback();
                result = String.IsNullOrEmpty(e.InnerException?.Message) ? e.Message : e.InnerException.Message;
                //throw;
            }
            finally
            {
                _transaction.Dispose();
                Dispose();
            }
            return result;
        }
        public int Save()
        {
            //_context.EnsureAutoHistory();
            //_context.EnsureAutoHistory(() => new CustomAutoHistory()
            //{
            //    CustomField = "CustomValue"
            //});
            return _context.SaveChanges();
        }
        private bool disposedValue = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _context.Dispose();
                    //if (_context != null)
                    //{
                    //    _context.Dispose();
                    //    _context = null;
                    //}
                    //if (_transaction != null)
                    //{
                    //    _transaction.Dispose();
                    //    _transaction = null;
                    //}
                }
                disposedValue = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
