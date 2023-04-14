using MassTransit;
using SMS.QueueListener.Repositories.Abstract;
using SMS.Shared.Enums;
using SMS.Shared.Models;

namespace SMS.QueueListener
{
    public class SmsSentConsumer : IConsumer<Sms>
    {
        private readonly ISmsRepository repo;
        public SmsSentConsumer(ISmsRepository repo)
        {
            this.repo = repo;
        }

        public async Task Consume(ConsumeContext<Sms> context)
        {
            var sms = context.Message;

            sms.Status = IsValidPhoneNumber(sms.To) 
                ? SmsStatus.Delivered 
                : SmsStatus.Failed;

            await repo.AddAsync(sms);

            string messageToPrint = sms.Status == SmsStatus.Delivered
                ? $"{sms.To} {sms.Status}"
                : $"{sms.Status}";

            Console.WriteLine(messageToPrint);
        }

        /// <summary>
        /// Checks whether a phone number is considered valid in Azerbaijan.
        /// Rules: 1. Must start with 994[50/55/70/77]xxx-xx-xx; 2. Must contain 12 characters; 3. All characters must be numbers;
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        private static bool IsValidPhoneNumber(string phoneNumber) =>
            !string.IsNullOrEmpty(phoneNumber) &&
            phoneNumber.Length == 12 &&
            phoneNumber.All(c => char.IsNumber(c)) &&
            phoneNumber.StartsWith("994") &&
            IsValidOperatorPrefix(phoneNumber.Substring(3, 2));

        private static bool IsValidOperatorPrefix(string prefix) =>
            prefix == "50" || prefix == "51" || prefix == "55" || prefix == "70" || prefix == "77";
    }
}
