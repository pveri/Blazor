using BlazorMovies.Client.Helpers;
using BlazorMovies.Client.Helpers.Interfaces;
using BlazorMovies.Shared.Entities;
using BlazorMovies.Shared.IRepositories;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlazorMovies.Client.Auth
{
    public class JWTAuthProvider: AuthenticationStateProvider, ILoginService
    {
        private readonly IJSRuntime js;
        private readonly IAccountsRepository userRepo;
        private AuthenticationState anonymous =>
            new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        private readonly HttpClient client;
        public JWTAuthProvider(IJSRuntime _js, HttpClient _client, IAccountsRepository userRepo)
        {
            this.js = _js;
            this.client = _client;
            this.userRepo = userRepo;
        }

        public async Task TryRenew()
        {
            var expiry = await js.GetAsync<string>("expiry");

            if (DateTime.TryParse(expiry, out var expiryDate))
            {
                if (isExpired(expiryDate))
                {
                    await Logout();
                }

                if (ShouldRenew(expiryDate))
                {
                    var token = await js.GetAsync<string>("token");
                    token = await RenewToken(token);
                    var authState = BuildAuthState(token);
                    NotifyAuthenticationStateChanged(Task.FromResult(authState));
                }
            }
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await js.GetAsync<string>("token");
            var expiry = await js.GetAsync<string>("expiry");

            if  (DateTime.TryParse(expiry, out var expiryDate))
            {
                if (isExpired(expiryDate))
                {
                    await this.Cleanup();
                    return anonymous;
                }

                if (ShouldRenew(expiryDate))
                {
                    token = await RenewToken(token);
                }
            }

            if (string.IsNullOrEmpty(token))
            {
                return anonymous;
            }

            return BuildAuthState(token);
        }

        private async Task<string> RenewToken (string token)
        {
            this.client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue
                ("bearer", token);
            var newToken = await userRepo.RenewToken();
            await js.SetAsync("token", newToken.Token);
            await js.SetAsync("expiry", newToken.Expires.ToString());
            return newToken.Token;
        }
        private bool ShouldRenew(DateTime expiryDate)
        {
            return expiryDate.Subtract(DateTime.UtcNow) < TimeSpan.FromMinutes(5);
        }

        private bool isExpired (DateTime date)
        {
            return date <= DateTime.UtcNow;
        }
        public AuthenticationState BuildAuthState (string token)
        {
            this.client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue
                ("bearer", token);
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt")));
        }

         private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var claims = new List<Claim>();
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

            keyValuePairs.TryGetValue(ClaimTypes.Role, out object roles);

            if (roles != null)
            {
                if (roles.ToString().Trim().StartsWith("["))
                {
                    var parsedRoles = JsonSerializer.Deserialize<string[]>(roles.ToString());

                    foreach (var parsedRole in parsedRoles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, parsedRole));
                    }
                }
                else
                {
                    claims.Add(new Claim(ClaimTypes.Role, roles.ToString()));
                }

                keyValuePairs.Remove(ClaimTypes.Role);
            }

            claims.AddRange(keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString())));
            return claims;
        }

        private byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }

        public async Task Login(UserToken userToken)
        {
            await js.SetAsync("token", userToken.Token);
            await js.SetAsync("expiry", userToken.Expires.ToString());
            // await js.SetAsync(EXPIRATION"token", userToken.Expiration.ToString());
            var authState = BuildAuthState(userToken.Token);
            NotifyAuthenticationStateChanged(Task.FromResult(authState));
        }

        public async Task Logout()
        {
            await Cleanup();
            NotifyAuthenticationStateChanged(Task.FromResult(anonymous));
        }

        private async Task Cleanup()
        {
            await js.DeleteAsync("token");
            await js.DeleteAsync("expiry");
            client.DefaultRequestHeaders.Authorization = null;
        }
        //private async Task CleanUp()
        //{
        //    await js.DeleteAsync("token");
        //    // await js.DeleteAsync(EXPIRATION"token");
        //    client.DefaultRequestHeaders.Authorization = null;
        //}
    }
}
