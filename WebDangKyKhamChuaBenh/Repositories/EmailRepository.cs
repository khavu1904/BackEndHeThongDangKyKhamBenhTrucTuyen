using Microsoft.AspNetCore.Mvc;
using MimeKit.Text;
using MimeKit;
using WebDangKyKhamChuaBenh.Models;
using MailKit.Net.Smtp;

namespace WebDangKyKhamChuaBenh.Repositories
{
    public class EmailRepository : IEmailRepository
    {
        public async Task<IActionResult> SendEmailAsync(Email model)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("travukha190402@gmail.com"));
            email.To.Add(MailboxAddress.Parse(model.To));
            email.Subject = model.Subject;
            email.Body = new TextPart(TextFormat.Html) { Text = model.Body };

            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect("smtp.gmail.com", 587, false);
                    client.Authenticate("travukha190402@gmail.com", "u m k x a l a h b k e r m s f h");

                    await client.SendAsync(email);
                    client.Disconnect(true);
                    return new OkResult(); // Trả về OkResult khi gửi email thành công
                }
                catch (Exception ex)
                {
                    return new StatusCodeResult(500); // Trả về StatusCodeResult với mã lỗi 500 khi gửi email thất bại
                }
            }
        }
    }
}
