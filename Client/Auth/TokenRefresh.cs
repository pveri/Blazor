using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;

namespace BlazorMovies.Client.Auth
{
    public class TokenRefresh: IDisposable
    {
        Timer timer;
        private readonly ILoginService loginService;

        public TokenRefresh(ILoginService loginService)
        {
            this.loginService = loginService;
            timer = new Timer();
        }

        public void Initialize()
        {
            timer.Interval = 60*1000;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        private void Timer_Elapsed (object sender, ElapsedEventArgs e)
        {
            loginService.TryRenew();
        }

        public void Dispose()
        {
            Console.WriteLine("timer disposed");
        }
    }
}
