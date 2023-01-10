using System;
using BaseIdentity.EntityLayer.Concrete;

namespace BaseIdentity.DataAccessLayer.Abstract
{
	public interface IAdDal : IGenericDal<Ad>
    {
        Ad SearchJobWithDetail(int id);
        List<Ad> ListJobsWithDetail();
    }
}

