using System;
using Microsoft.AspNet.SignalR;
using Owin;
using Microsoft.Owin;
using MSniperService.Models;

[assembly: OwinStartup(typeof(MSniperService.Startup))]
namespace MSniperService
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            GlobalHost.Configuration.ConnectionTimeout = TimeSpan.FromSeconds(110);
            GlobalHost.Configuration.DisconnectTimeout = TimeSpan.FromSeconds(30);
            GlobalHost.Configuration.KeepAlive = TimeSpan.FromSeconds(10);

            GlobalHost.HubPipeline.AddModule(new ErrorHubPipelineModule());
            GlobalHost.HubPipeline.AddModule(new LoggingPipelineModule());
            var hubConfiguration = new HubConfiguration
            {
                EnableDetailedErrors = true,
                EnableJSONP = true
            };
            app.MapSignalR(hubConfiguration);
        }
    }
}