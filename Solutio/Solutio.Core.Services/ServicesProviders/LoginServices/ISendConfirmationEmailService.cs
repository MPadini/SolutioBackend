﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Solutio.Core.Services.ServicesProviders.LoginServices
{
    public interface ISendConfirmationEmailService
    {
        Task Send(int userId, string email, string ConfirmationToken);
    }
}
