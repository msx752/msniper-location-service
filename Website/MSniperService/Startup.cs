using System;
using Owin;
using Microsoft.Owin;
[assembly: OwinStartup(typeof(RMSniper1.Startup))]
namespace RMSniper1
{
    public class Startup
    {

        public void Configuration(IAppBuilder app)
        {
            // Any connection or hub wire up and configuration should go here
            app.MapSignalR();
        }
    }
}