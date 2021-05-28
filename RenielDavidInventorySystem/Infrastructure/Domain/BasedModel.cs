using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RenielDavidInventorySystem.Infrastructure.Domain
{
    public class BasedModel
    {
		public BasedModel()

		{

			this.CreatedAt = DateTime.UtcNow;
			this.UpdatedAt = DateTime.UtcNow;


		}

		public Guid? ID { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }
	}
}
