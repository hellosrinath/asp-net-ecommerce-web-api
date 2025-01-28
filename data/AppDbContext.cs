using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using asp_net_ecommerce_web_api.models;
using Microsoft.EntityFrameworkCore;

namespace asp_net_ecommerce_web_api.data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) :
        base(options)
        { }

        public DbSet<Category> Categories { get; set; }
    }
}