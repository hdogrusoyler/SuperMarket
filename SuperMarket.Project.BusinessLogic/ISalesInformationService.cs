using SuperMarket.Project.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperMarket.Project.BusinessLogic
{
    public interface ISalesInformationService
    {
        SalesInformation GetById(int Id);
        List<SalesInformation> GetAll(int page = 1, int pageSize = 0);
        string Add(SalesInformation entity);
        string Update(SalesInformation entity);
        string Delete(SalesInformation entity);
    }
}
