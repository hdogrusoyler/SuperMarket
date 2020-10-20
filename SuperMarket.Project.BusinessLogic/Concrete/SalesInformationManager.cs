using Microsoft.EntityFrameworkCore;
using SuperMarket.Project.DataAccess;
using SuperMarket.Project.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuperMarket.Project.BusinessLogic.Concrete
{
    public class SalesInformationManager : ISalesInformationService
    {
        IUnitOfWork _unitOfWork;
        public SalesInformationManager()
        {
            _unitOfWork = new UnitOfWork();
        }

        public List<SalesInformation> GetAll(int page = 1, int pageSize = 0)
        {
            List<SalesInformation> res = new List<SalesInformation>();
            res = _unitOfWork.efSalesInformationDal.GetList(null, (qry) => qry.OrderByDescending(x => x.Id), page, pageSize, i => i.Include(s => s.Cart).Include(s => s.PaymentType));
            return res;
        }

        public SalesInformation GetById(int Id)
        {
            SalesInformation res = new SalesInformation();
            res = _unitOfWork.efSalesInformationDal.Get(c => c.Id == Id, i=>i.Include(cp => cp.Cart).ThenInclude(c => c.CartProducts).ThenInclude(p => p.Product).ThenInclude(c => c.Category).Include(pt => pt.PaymentType));
            return res;
        }

        public string Add(SalesInformation entity)
        {
            _unitOfWork.BeginTransaction();
            _unitOfWork.efSalesInformationDal.Add(entity);
            return _unitOfWork.CommitSaveChanges();
        }

        public string Update(SalesInformation entity)
        {
            _unitOfWork.BeginTransaction();
            _unitOfWork.efSalesInformationDal.Update(entity);
            return _unitOfWork.CommitSaveChanges();
        }

        public string Delete(SalesInformation entity)
        {
            _unitOfWork.BeginTransaction();
            _unitOfWork.efSalesInformationDal.Delete(entity);
            return _unitOfWork.CommitSaveChanges();
        }
    }
}
