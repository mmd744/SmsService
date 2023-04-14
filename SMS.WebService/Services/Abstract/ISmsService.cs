using SMS.WebService.Dtos.Request;
using SMS.WebService.Dtos.Response;

namespace SMS.WebService.Services.Abstract
{
    public interface ISmsService
    {
        Task SendSmsAsync(SendSmsRequest request);
        Task<IReadOnlyList<GetAllSmsesResponse>> GetAllSmsesAsync();
        Task<GetSmsByIdResponse> GetSmsByIdAsync(Guid id);
    }
}
