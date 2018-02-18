using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MerchStore.Services
{
    public class NullMailService : IMailService
    {
        private readonly ILogger<NullMailService> _logger;

        public void SendMessage(string to, string subject, string body)
        {
            _logger.LogInformation($"To: {to}. Subject: {subject}. body: {body}.");
        }

        public NullMailService(ILogger<NullMailService> logger)
        {
            
        }
    }
}


