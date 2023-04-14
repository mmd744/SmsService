using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SMS.Shared.Models;

namespace SMS.Shared.Contexts
{
    public class SmsDbContext : DbContext
    {
        public SmsDbContext(DbContextOptions<SmsDbContext> options) : base(options) { }

        public DbSet<Sms> Smses { get; set; }
    }
}
