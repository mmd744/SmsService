using SMS.QueueListener.Repositories.Abstract;
using SMS.Shared.Contexts;
using SMS.Shared.Models;

namespace SMS.QueueListener.Repositories.Implementation
{
    public class SmsRepository : ISmsRepository
    {
        private readonly SmsDbContext smsDbContext;
        public SmsRepository(SmsDbContext smsDbContext)
        {
            this.smsDbContext = smsDbContext;
        }

        public async Task AddAsync(Sms sms)
        {
            await smsDbContext.AddAsync(sms);
            await smsDbContext.SaveChangesAsync();
        }
    }
}
