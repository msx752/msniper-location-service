using Microsoft.AspNet.SignalR.Hubs;

namespace MSniperService.Models
{
    public class ErrorHubPipelineModule : HubPipelineModule
    {
        protected override void OnIncomingError(ExceptionContext exceptionContext,
            IHubIncomingInvokerContext invokerContext)
        {
            dynamic caller = invokerContext.Hub.Clients.Caller;
            caller.Exceptions(exceptionContext.Error.Message);
        }
    }
}