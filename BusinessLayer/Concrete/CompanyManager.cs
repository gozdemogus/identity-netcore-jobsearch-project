using System;
using BaseIdentity.BusinessLayer.Abstract;
using BaseIdentity.DataAccessLayer.Abstract;
using BaseIdentity.EntityLayer.Concrete;

namespace BaseIdentity.BusinessLayer.Concrete
{
	public class CompanyManager:ICompanyService
	{
        private readonly ICompanyDal _companyDal;

        public CompanyManager(ICompanyDal companyDal)
        {
            _companyDal = companyDal;
        }

        

        public void TDelete(Company t)
        {
            throw new NotImplementedException();
        }

        public Company TGetByID(int id)
        {
            return _companyDal.GetByID(id);
        }

        public List<Company> TGetList()
        {
            return _companyDal.GetList();
        }

        public void TInsert(Company t)
        {
            throw new NotImplementedException();
        }

        public void TUpdate(Company t)
        {
            throw new NotImplementedException();
        }
    }
}

