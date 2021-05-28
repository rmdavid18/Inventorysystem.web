using RenielDavidInventorySystem.Infrastructure.Domain.Models;
using RenielDavidInventorySystem.Infrastructure.Paged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RenielDavidInventorySystem.Models
{
    public class ProductViewModel : Product
    {
            
    }
    public class ProductSearchViewModel 
    {
        public Paged<ProductViewModel> Products { get; set; }
    }
}
