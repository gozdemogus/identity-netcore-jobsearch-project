using System;
using Microsoft.AspNetCore.Identity;

namespace BaseIdentity.EntityLayer.Concrete
{
	public class AppRole:IdentityRole<int>
	{
		public AppRole()
		{
		}
	}
}

