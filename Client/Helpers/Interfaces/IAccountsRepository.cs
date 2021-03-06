﻿using BlazorMovies.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorMovies.Client.Helpers.Interfaces
{
    public interface IAccountsRepository
    {
        Task<UserToken> Register(UserInfo userInfo);
        Task<UserToken> Login(UserInfo userInfo);
        Task<UserToken> RenewToken();
    }
}
