using SuperMarket.Project.DataAccess;
using SuperMarket.Project.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuperMarket.Project.BusinessLogic.Concrete
{
    public class UserManager : IUserService
    {
        IUnitOfWork _unitOfWork;
        public UserManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<User> GetAll(int page = 1, int pageSize = 0)
        {
            List<User> res = new List<User>();
            res = _unitOfWork.efUserDal.GetList(null, (qry) => qry.OrderByDescending(x => x.Id), page, pageSize);
            return res;
        }

        public User GetById(int Id)
        {
            User res = new User();
            res = _unitOfWork.efUserDal.Get(c => c.Id == Id);
            return res;
        }

        public string Add(User entity)
        {
            _unitOfWork.BeginTransaction();
            _unitOfWork.efUserDal.Add(entity);
            return _unitOfWork.CommitSaveChanges();
        }

        public string Update(User entity)
        {
            _unitOfWork.BeginTransaction();
            _unitOfWork.efUserDal.Update(entity);
            return _unitOfWork.CommitSaveChanges();
        }

        public string Delete(User entity)
        {
            _unitOfWork.BeginTransaction();
            _unitOfWork.efUserDal.Delete(entity);
            return _unitOfWork.CommitSaveChanges();
        }

        public User GetByUserName(string userName, int page = 1, int pageSize = 0)
        {
            List<User> res = new List<User>();
            res = _unitOfWork.efUserDal.GetList(u => u.UserName == userName, (qry) => qry.OrderByDescending(x => x.Id), page, pageSize);
            if (res.Count > 0)
            {
                return res[0];
            }
            return null;
        }
    }
}
