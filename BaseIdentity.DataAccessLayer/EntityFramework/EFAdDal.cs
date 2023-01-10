using System;
using BaseIdentity.DataAccessLayer.Abstract;
using BaseIdentity.DataAccessLayer.Concrete;
using BaseIdentity.DataAccessLayer.Repository;
using BaseIdentity.EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;

namespace BaseIdentity.DataAccessLayer.EntityFramework
{
    public class EFAdDal : GenericRepository<Ad>, IAdDal
    {
        public List<Ad> ListJobsWithDetail()
        {
            using (var context = new Context())
            {
                return context.Ads.Include(a => a.Company).Include(x => x.Category).Include(x=>x.Experience).ToList();
            }
        }

        public Ad SearchJobWithDetail(int id)
        {
            using (var context = new Context())
            {
               
               return context.Ads.Include(a => a.Company).Include(x => x.Category).Include(x=>x.Experience).Where(x => x.AdID == id).FirstOrDefault();
            }
        }

     
    }
}

