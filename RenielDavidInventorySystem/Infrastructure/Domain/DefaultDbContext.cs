using Microsoft.EntityFrameworkCore;
using RenielDavidInventorySystem.Infrastructure.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RenielDavidInventorySystem.Infrastructure.Domain
{
    public class DefaultDbContext : DbContext
    {
        public DefaultDbContext(DbContextOptions<DefaultDbContext> options)
            : base(options)
        { }

        public DbSet<Models.Product> Products { get; set; }
        public DbSet<Models.Tag> Tags{ get; set; }
        public DbSet<Models.Category> Categories { get; set; }
        public DbSet<Models.ProductTag> ProductTags { get; set; }
        public DbSet<Models.User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            base.OnModelCreating(modelBuilder);
            List<Tag> Tags = new List<Tag>()
            {
                new Tag()
                {
                    ID=Guid.Parse("810203f8-e1c2-45f1-b411-f6f97d00ce01"),
                    Name = "Tag 1"
                },
                 new Tag()
                {
                    ID=Guid.Parse("810203f8-e1c2-45f1-b411-f6f97d00ce02"),
                    Name = "Tag 2"
                },
                  new Tag()
                {
                    ID=Guid.Parse("810203f8-e1c2-45f1-b411-f6f97d00ce03"),
                    Name = "Tag 3"
                }
                  
            };
            modelBuilder.Entity<Tag>()
                       .HasData(Tags);
            List<Product> products = new List<Product>()
            {
                new Product()
                {
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    Brand =  "Emperador",
                    CategoryId = Guid.NewGuid(),
                    Description = "Lights",
                    ID = Guid.NewGuid(),
                    Name = "Brandy",
                    Price = 0,
                    Tagline = "PALASING"    
                }



            };
            modelBuilder.Entity<Product>()
                .HasData(products);
                
        }
    }
}
