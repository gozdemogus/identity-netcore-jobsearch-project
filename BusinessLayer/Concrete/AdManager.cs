using System;
using BaseIdentity.BusinessLayer.Abstract;
using BaseIdentity.DataAccessLayer.Abstract;
using BaseIdentity.EntityLayer.Concrete;

namespace BaseIdentity.BusinessLayer.Concrete
{
	public class AdManager:IAdService
	{
        private readonly IAdDal _AdDal;


        public AdManager(IAdDal AdDal)
		{
            _AdDal = AdDal;
		}

        public List<Ad> ListJobsWithDetail()
        {
            return _AdDal.ListJobsWithDetail();
        }

        public Ad SearchJobWithDetail(int id)
        {
            return _AdDal.SearchJobWithDetail(id);
        }

        public void TDelete(Ad t)
        {
            throw new NotImplementedException();
        }

        public Ad TGetByID(int id)
        {
            throw new NotImplementedException();
        }

        public List<Ad> TGetList()
        {
            return _AdDal.GetList();
        }

        public void TInsert(Ad t)
        {
            throw new NotImplementedException();
        }

        public void TUpdate(Ad t)
        {
            throw new NotImplementedException();
        }
    }
}

