using Microsoft.EntityFrameworkCore;
using SMS.Shared.Contexts;
using SMS.Shared.Models;
using SMS.WebService.Repositories.Abstract;

namespace SMS.WebService.Repositories.Implementation
{
    public class SmsRepository : ISmsRepository
    {
        private readonly SmsDbContext context;
        public SmsRepository(SmsDbContext context)
        {
            this.context = context;
        }

        public async Task<IReadOnlyList<Sms>> GetAllSmsMessagesAsync()
        {
            return await context.Smses.AsNoTracking().ToListAsync();
        }

        public async Task<Sms> GetSmsMessageByIdAsync(Guid id)
        {
            return await context.Smses.FindAsync(id);
        }
    }
}
