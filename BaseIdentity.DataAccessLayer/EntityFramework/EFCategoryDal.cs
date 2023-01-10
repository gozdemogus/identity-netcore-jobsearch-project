using System;
using BaseIdentity.DataAccessLayer.Abstract;
using BaseIdentity.DataAccessLayer.Repository;
using BaseIdentity.EntityLayer.Concrete;

namespace BaseIdentity.DataAccessLayer.EntityFramework
{
	public class EFCategoryDal : GenericRepository<Category>, ICategoryDal
    {
		public EFCategoryDal()
		{
		}
	}
}

