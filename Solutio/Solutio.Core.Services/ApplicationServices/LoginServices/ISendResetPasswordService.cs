﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Solutio.Core.Services.ApplicationServices.LoginServices
{
    public interface ISendResetPasswordService
    {
        Task Send(int userId, string email, string ConfirmationToken);
    }
}
