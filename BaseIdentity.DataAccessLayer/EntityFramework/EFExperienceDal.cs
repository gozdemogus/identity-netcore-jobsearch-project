using System;
using BaseIdentity.DataAccessLayer.Abstract;
using BaseIdentity.DataAccessLayer.Repository;
using BaseIdentity.EntityLayer.Concrete;

namespace BaseIdentity.DataAccessLayer.EntityFramework
{
	public class EFExperienceDal : GenericRepository<Experience>, IExperienceDal
    {
		public EFExperienceDal()
		{
		}
	}
}

