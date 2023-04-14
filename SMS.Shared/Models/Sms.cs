using SMS.Shared.Enums;

namespace SMS.Shared.Models
{
    public class Sms
    {
        public Guid Id { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Content { get; set; }
        public SmsStatus Status { get; set; }
    }
}
