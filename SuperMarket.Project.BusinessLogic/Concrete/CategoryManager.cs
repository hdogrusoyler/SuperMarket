using SuperMarket.Project.DataAccess;
using SuperMarket.Project.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuperMarket.Project.BusinessLogic.Concrete
{
    public class CategoryManager : ICategoryService
    {
        IUnitOfWork _unitOfWork;
        public CategoryManager()
        {
            _unitOfWork = new UnitOfWork();
        }

        public List<Category> GetAll(int page = 1, int pageSize = 0)
        {
            List<Category> res = new List<Category>();
            res = _unitOfWork.efCategoryDal.GetList(null, (qry) => qry.OrderByDescending(x => x.Id), page, pageSize);
            return res;
        }

        public Category GetById(int Id)
        {
            Category res = new Category();
            res = _unitOfWork.efCategoryDal.Get(c => c.Id == Id);
            return res;
        }

        public string Add(Category entity)
        {
            _unitOfWork.BeginTransaction();
            _unitOfWork.efCategoryDal.Add(entity);
            return _unitOfWork.CommitSaveChanges();
        }

        public string Update(Category entity)
        {
            _unitOfWork.BeginTransaction();
            _unitOfWork.efCategoryDal.Update(entity);
            return _unitOfWork.CommitSaveChanges();
        }

        public string Delete(Category entity)
        {
            _unitOfWork.BeginTransaction();
            _unitOfWork.efCategoryDal.Delete(entity);
            return _unitOfWork.CommitSaveChanges();
        }
    }
}
