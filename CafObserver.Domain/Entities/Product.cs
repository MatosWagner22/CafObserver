using CafObserver.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafObserver.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public ProductCategory Category { get; set; }
        public bool IsAvailable { get; set; } = true;
        public int PreparationTimeMinutes { get; set; }
    }
}
