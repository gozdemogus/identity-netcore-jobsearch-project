using System;
using BaseIdentity.EntityLayer.Concrete;

namespace BaseIdentity.PresentationLayer.Models
{
	public class SearchViewModel
	{
		public SearchViewModel()
		{
		}

		public string experience { get; set; }
		public string category { get; set; }
		public string keyword { get; set; }
	}
}

