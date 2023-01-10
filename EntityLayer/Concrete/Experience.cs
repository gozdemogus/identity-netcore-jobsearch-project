using System;
namespace BaseIdentity.EntityLayer.Concrete
{
	

        public class Experience
        {
            public int Id { get; set; }
            public string? Name { get; set; }
            public virtual ICollection<Ad>? Ads { get; set; }
        }

   
}

