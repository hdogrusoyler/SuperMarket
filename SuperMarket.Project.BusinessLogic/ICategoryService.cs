using SuperMarket.Project.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperMarket.Project.BusinessLogic
{
    public interface ICategoryService
    {
        Category GetById(int Id);
        List<Category> GetAll(int page = 1, int pageSize = 0);
        string Add(Category entity);
        string Update(Category entity);
        string Delete(Category entity);
    }
}
