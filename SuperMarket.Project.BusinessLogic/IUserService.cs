using SuperMarket.Project.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperMarket.Project.BusinessLogic
{
    public interface IUserService
    {
        User GetById(int Id);
        User GetByUserName(string userName, int page = 1, int pageSize = 0);
        List<User> GetAll(int page = 1, int pageSize = 0);
        string Add(User entity);
        string Update(User entity);
        string Delete(User entity);
    }
}
