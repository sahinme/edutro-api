﻿using System.Threading.Tasks;

namespace Microsoft.EgitimAPI.ApplicationCore.Interfaces
{

    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
