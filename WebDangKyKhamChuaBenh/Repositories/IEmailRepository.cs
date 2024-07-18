using Microsoft.AspNetCore.Mvc;
using WebDangKyKhamChuaBenh.Models;

namespace WebDangKyKhamChuaBenh.Repositories
{
    public interface IEmailRepository
    {
        Task<IActionResult> SendEmailAsync(Email model);
    }
}
