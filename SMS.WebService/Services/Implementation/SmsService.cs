using AutoMapper;
using MassTransit;
using SMS.Shared.Enums;
using SMS.Shared.Models;
using SMS.WebService.Dtos.Request;
using SMS.WebService.Dtos.Response;
using SMS.WebService.Repositories.Abstract;
using SMS.WebService.Services.Abstract;

namespace SMS.WebService.Services.Implementation
{
    public class SmsService : ISmsService
    {
        private readonly IPublishEndpoint publishEndpoint;
        private readonly IMapper mapper;
        private readonly ISmsRepository repo;
        public SmsService(IPublishEndpoint publishEndPoint, IMapper mapper, ISmsRepository repo)
        {
            this.publishEndpoint = publishEndPoint;
            this.mapper = mapper;
            this.repo = repo;
        }

        public async Task SendSmsAsync(SendSmsRequest request)
        {
            var tasks = request.To.Select(number =>
            {
                var newSmsMessage = new Sms
                {
                    Id = Guid.NewGuid(),
                    Content = request.Content,
                    From = request.From,
                    Status = SmsStatus.Processing,
                    To = number
                };

                return publishEndpoint.Publish(newSmsMessage);
            });

            await Task.WhenAll(tasks);
        }

        public async Task<IReadOnlyList<GetAllSmsesResponse>> GetAllSmsesAsync()
        {
            var smss = await repo.GetAllSmsMessagesAsync();

            return smss.Select(sms =>
                mapper.Map<GetAllSmsesResponse>(sms)).ToList();
        }

        public async Task<GetSmsByIdResponse> GetSmsByIdAsync(Guid id)
        {
            var sms = await repo.GetSmsMessageByIdAsync(id);

            return sms is null
                ? throw new KeyNotFoundException(nameof(id))
                : mapper.Map<GetSmsByIdResponse>(sms);
        }
    }
}
