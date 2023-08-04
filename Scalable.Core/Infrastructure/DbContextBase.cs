using Microsoft.EntityFrameworkCore;
using Scalable.Core.Domain;

namespace Scalable.Core.Infrastructure
{
    public abstract class DbContextBase : DbContext
    {
        public DbSet<IntegrationEvent> IntegrationEvents { get; set; }

        public DbContextBase(DbContextOptions options) : base(options)
        {            

        }
    }
}
