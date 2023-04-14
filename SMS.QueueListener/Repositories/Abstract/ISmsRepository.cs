using SMS.Shared.Models;

namespace SMS.QueueListener.Repositories.Abstract
{
    public interface ISmsRepository
    {
        Task AddAsync(Sms sms);
    }
}
