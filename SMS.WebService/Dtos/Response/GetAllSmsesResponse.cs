using SMS.Shared.Enums;

namespace SMS.WebService.Dtos.Response
{
    public class GetAllSmsesResponse
    {
        public Guid Id { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Status { get; set; }
    }
}
