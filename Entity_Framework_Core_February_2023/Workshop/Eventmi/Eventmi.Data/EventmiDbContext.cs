using Microsoft.EntityFrameworkCore;

using Eventmi.Core.Models;

namespace Eventmi.Core
{
    public class EventmiDbContext : DbContext
    {
        public EventmiDbContext()
        {
        }

        public EventmiDbContext(DbContextOptions options)
           : base(options)
        {
            
        }

        public DbSet<Event> Events { get; set; } = null!;
    }
}
