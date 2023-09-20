using Core.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.Contexts
{
    public class EfContext : DbContext
    {
        IConfiguration _configuration;
        public EfContext(DbContextOptions<EfContext> options) : base(options)
        {
        }

        public EfContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public EfContext()
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetSection("ConnectionStrings").GetConnectionString("DefaultConnection"));
        }


        public DbSet<User> Users { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
    }
}
