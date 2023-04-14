using SMS.Shared.Models;

namespace SMS.WebService.Repositories.Abstract
{
    public interface ISmsRepository
    {
        Task<IReadOnlyList<Sms>> GetAllSmsMessagesAsync();
        Task<Sms> GetSmsMessageByIdAsync(Guid id);
    }
}
