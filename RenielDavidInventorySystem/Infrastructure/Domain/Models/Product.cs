using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RenielDavidInventorySystem.Infrastructure.Domain.Models
{
    public class Product : BasedModel
	{
		public string Name { get; set; }
		public string Brand { get; set; }
		public string Tagline { get; set; }
		public string Description { get; set; }
		public Decimal Price { get; set; }
		public Guid? CategoryId { get; set; }
		
	}
}
