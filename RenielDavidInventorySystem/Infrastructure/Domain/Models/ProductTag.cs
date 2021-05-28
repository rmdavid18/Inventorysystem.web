using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RenielDavidInventorySystem.Infrastructure.Domain.Models
{
    public class ProductTag : BasedModel
    {
        public Guid? ProductId { get; set; }

        public Category Category { get; set; }
        public Guid? TagId { get; set; }
        public Tag Tag { get; set; } 

    }
}
