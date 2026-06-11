using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using WargearTracker.Core;

namespace WargearTracker.Data
{
    public class WargearDbContext : DbContext
    {
        public WargearDbContext(DbContextOptions<WargearDbContext> options) : base(options)
        {
        }
        public DbSet<Army> Armies { get; set; }
        public DbSet<Miniature> Miniatures { get; set; }
    }
}
