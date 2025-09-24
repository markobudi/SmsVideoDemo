using Microsoft.AspNetCore.Mvc;
using SmsVideoDemo.Interfaces;

namespace SmsVideoDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SmsController : ControllerBase
    {

        private readonly ISmsService _smsService;

        public SmsController(ISmsService smsService)
        {
            _smsService = smsService;
        }

        [HttpGet(Name = "SendSms")]
        public async Task<IActionResult> Get()
        {
            var result = await _smsService.SendSmsMessageAsync("+543264259482", "Hola desde mi casa!");
            return Ok(result);
        }
    }
}
