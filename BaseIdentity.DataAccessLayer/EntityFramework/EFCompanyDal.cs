using System;
using BaseIdentity.DataAccessLayer.Abstract;
using BaseIdentity.DataAccessLayer.Repository;
using BaseIdentity.EntityLayer.Concrete;

namespace BaseIdentity.DataAccessLayer.EntityFramework
{
	public class EFCompanyDal : GenericRepository<Company>, ICompanyDal
    {
		public EFCompanyDal()
		{
		}
	}
}

