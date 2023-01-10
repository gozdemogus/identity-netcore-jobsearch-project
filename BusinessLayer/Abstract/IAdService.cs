using System;
using BaseIdentity.EntityLayer.Concrete;

namespace BaseIdentity.BusinessLayer.Abstract
{
	public interface IAdService : IGenericService<Ad>
    {
        Ad SearchJobWithDetail(int id);
        List<Ad> ListJobsWithDetail();
    }
}

