namespace SMS.WebService.Dtos.Request
{
    public class SendSmsRequest
    {
        public string From { get; set; }
        public IList<string> To { get; set; }
        public string Content { get; set; }
    }
}
