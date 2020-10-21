using SuperMarket.Project.DataAccess;
using SuperMarket.Project.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuperMarket.Project.BusinessLogic.Concrete
{
    public class PaymentTypeManager : IPaymentTypeService
    {
        IUnitOfWork _unitOfWork;
        public PaymentTypeManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<PaymentType> GetAll(int page = 1, int pageSize = 0)
        {
            List<PaymentType> res = new List<PaymentType>();
            res = _unitOfWork.efPaymentTypeDal.GetList(null, (qry) => qry.OrderByDescending(x => x.Id), page, pageSize);
            return res;
        }

        public PaymentType GetById(int Id)
        {
            PaymentType res = new PaymentType();
            res = _unitOfWork.efPaymentTypeDal.Get(c => c.Id == Id);
            return res;
        }

        public string Add(PaymentType entity)
        {
            _unitOfWork.BeginTransaction();
            _unitOfWork.efPaymentTypeDal.Add(entity);
            return _unitOfWork.CommitSaveChanges();
        }

        public string Update(PaymentType entity)
        {
            _unitOfWork.BeginTransaction();
            _unitOfWork.efPaymentTypeDal.Update(entity);
            return _unitOfWork.CommitSaveChanges();
        }

        public string Delete(PaymentType entity)
        {
            _unitOfWork.BeginTransaction();
            _unitOfWork.efPaymentTypeDal.Delete(entity);
            return _unitOfWork.CommitSaveChanges();
        }
    }
}
