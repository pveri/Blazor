using BlazorMovies.Client.Helpers.Interfaces;
using BlazorMovies.Shared.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorMovies.Client.Helpers.Repository
{
    public class AccountsRepository: IAccountsRepository
    {
        private readonly IHttpService httpService;
        private string url = "api/account";
        public AccountsRepository(IHttpService httpService)
        {
            this.httpService = httpService;
        }

        public async Task<UserToken> Register(UserInfo userInfo)
        {
            var response = await httpService.Post<UserInfo, UserToken>($"{url}/create", userInfo);
            if (response.Success)
            {
                return response.Response;
            }
            throw new Exception(await response.GetBody());
        }
    }
}
