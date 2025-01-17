﻿using NTierApplication.DataAccess.Models;
using NTierApplication.Service.ViewModels;

namespace NTierApplication.Service;

public interface IUserService
{
    bool Register(RegisterUserViewModel rUserModel);
    ResponseModelLogin Login(LogInViewModel lUserModel);
}
