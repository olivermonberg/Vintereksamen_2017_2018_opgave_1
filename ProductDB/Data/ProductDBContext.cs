using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ProductDB.Models
{
    public class ProductDBContext : DbContext
    {
        public ProductDBContext (DbContextOptions<ProductDBContext> options)
            : base(options)
        {
        }

        public DbSet<ProductDB.Models.Product> Product { get; set; }
    }
}
