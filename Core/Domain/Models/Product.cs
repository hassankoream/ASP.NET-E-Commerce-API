using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Product:BaseEntity<int>
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string PictureUrl { get; set; } = null!;
        public decimal Price { get; set; }

        public ProductBrand ProductBrand { get; set; }

        public int BrandId { get; set; } //FK

        public ProductType ProductType { get; set; }

        public int TypeId { get; set; } //FK





















        //// Domain events storage (for notifications)
        //private readonly List<object> _domainEvents = new();
        //public IReadOnlyCollection<object> DomainEvents => _domainEvents.AsReadOnly();

        //// Constructor enforces business rules
        //public Product(string name, decimal price)
        //{
        //    if (string.IsNullOrWhiteSpace(name))
        //        throw new ArgumentException("Product name is required.", nameof(name));
        //    if (price <= 0)
        //        throw new ArgumentException("Price must be greater than zero.", nameof(price));

        //    //Id = Guid.NewGuid();
        //    Name = name;
        //    Price = price;

        //    // Raise domain event for creation
        //    _domainEvents.Add(new Domain.Events);
        //}
    }
}
