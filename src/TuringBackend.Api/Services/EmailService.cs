using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using TuringBackend.Api.Core;
using TuringBackend.Models;

namespace TuringBackend.Api.Services
{
    public class EmailService : IEmailService
    {
        private readonly IOptions<EmailSettings> _emailOptions;
        private readonly IOptions<AppOptions> _appOptions;

        public EmailService(IOptions<EmailSettings> emailOptions, IOptions<AppOptions> appOptions)
        {
            _emailOptions = emailOptions;
            _appOptions = appOptions;
        }

        public async Task<Response> SendEmail(string email, string subject, string message)
        {
            var client = new SendGridClient(_emailOptions.Value.ApiKey);
            var from = new EmailAddress(_emailOptions.Value.Email, _appOptions.Value.Name);
            var to = new EmailAddress(email);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, message, message);
            return await client.SendEmailAsync(msg);
        }
    }
}
