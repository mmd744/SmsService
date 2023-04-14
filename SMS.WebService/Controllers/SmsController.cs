using Microsoft.AspNetCore.Mvc;
using SMS.WebService.Dtos.Request;
using SMS.WebService.Services.Abstract;
using System.Net;

namespace SMS.WebService.Controllers
{
    [ApiController]
    [Route("api/v1/sms")]
    public class SmsController : ControllerBase
    {
        private readonly ISmsService smsMessageService;
        public SmsController(ISmsService smsMessageService)
        {
            this.smsMessageService = smsMessageService;
        }


        [HttpPost]
        public async Task<IActionResult> SendMessageAsync(SendSmsRequest request)
        {
            try
            {
                await smsMessageService.SendSmsAsync(request);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
            return Ok();
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllMessagesAsync()
        {
            try
            {
                return Ok(await smsMessageService.GetAllSmsesAsync());
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSmsMessageByIdAsync(Guid id)
        {
            try
            {
                return Ok(await smsMessageService.GetSmsByIdAsync(id));
            }
            catch (KeyNotFoundException)
            {
                return NotFound(id);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}