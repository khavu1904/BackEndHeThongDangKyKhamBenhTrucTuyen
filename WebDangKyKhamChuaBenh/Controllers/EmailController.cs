using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebDangKyKhamChuaBenh.Models;
using WebDangKyKhamChuaBenh.Repositories;

namespace WebDangKyKhamChuaBenh.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailRepository _emailRepository;

        public EmailController(IEmailRepository emailRepository)
        {
            _emailRepository = emailRepository;
        }
        [HttpPost]
        public async Task<IActionResult> SendEmail([FromBody] Email model)
        {
            return await _emailRepository.SendEmailAsync(model);
        }
    }
}
