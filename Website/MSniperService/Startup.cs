using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using MSniperService.Models;
using Owin;
using System;

[assembly: OwinStartup(typeof(MSniperService.Startup))]

namespace MSniperService
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            GlobalHost.Configuration.ConnectionTimeout = TimeSpan.FromSeconds(100);
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