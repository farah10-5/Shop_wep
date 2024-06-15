using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShopDomain.Entities;

namespace ShopPersistance
{
    public class ShopContext :DbContext
    {
        public ShopContext(DbContextOptions<ShopContext> options) :
            base(options)
        { 
        
        }

        public DbSet<Gift>Gifts { get; set; }
        public DbSet<Flower> Flowers { get; set; }
    }
}
