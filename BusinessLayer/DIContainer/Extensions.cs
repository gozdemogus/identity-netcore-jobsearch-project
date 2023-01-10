using System;
using BaseIdentity.BusinessLayer.Abstract;
using BaseIdentity.BusinessLayer.Concrete;
using BaseIdentity.DataAccessLayer.Abstract;
using BaseIdentity.DataAccessLayer.EntityFramework;
using Microsoft.Extensions.DependencyInjection;

namespace BaseIdentity.BusinessLayer.DIContainer
{
	public static class Extensions
	{
        public static void ContainerDependencies(this IServiceCollection services)
        {

            services.AddScoped<IMessageService, MessageManager>();
            services.AddScoped<IMessageDal, EFMessageDal>();

            services.AddScoped<IAdService, AdManager>();
            services.AddScoped<IAdDal, EFAdDal>();

            services.AddScoped<IFriendshipService, FriendShipManager>();
            services.AddScoped<IFriendshipDal, EFFriendshipDal>();

            services.AddScoped<ICompanyService, CompanyManager>();
            services.AddScoped<ICompanyDal, EFCompanyDal>();

            services.AddScoped<ICategoryService, CategoryManager>();
            services.AddScoped<ICategoryDal, EFCategoryDal>();

            services.AddScoped<IExperienceService, ExperienceManager>();
            services.AddScoped<IExperienceDal, EFExperienceDal>();
        }
    }
}

