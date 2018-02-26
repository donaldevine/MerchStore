using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MerchStore.Services
{
    public class NullMailService : IMailService
    {
        private readonly ILogger<NullMailService> logger;

        public void SendMessage(string to, string subject, string body)
        {
            this.logger.LogInformation($"To: {to}. Subject: {subject}. body: {body}.");
        }

        public NullMailService(ILogger<NullMailService> logger)
        {
            this.logger = logger;
        }
    }
}


