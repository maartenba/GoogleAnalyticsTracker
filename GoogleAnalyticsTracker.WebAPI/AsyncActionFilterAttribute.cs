using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace GoogleAnalyticsTracker.WebApi
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class AsyncActionFilterAttribute 
        : FilterAttribute, IActionFilter
    {
        public async Task<HttpResponseMessage> ExecuteActionFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
        {
            cancellationToken.ThrowIfCancellationRequested();

            await OnActionExecutingAsync(actionContext, cancellationToken);

            if (actionContext.Response != null)
            {
                return actionContext.Response;
            }

            HttpActionExecutedContext executedContext;

            try
            {
                var response = await continuation();
                executedContext = new HttpActionExecutedContext(actionContext, null)
                {
                    Response = response
                };
            }
            catch (Exception exception)
            {
                executedContext = new HttpActionExecutedContext(actionContext, exception);
            }

            await OnActionExecutedAsync(executedContext, cancellationToken);

            if (executedContext.Response != null)
            {
                return executedContext.Response;
            }
            if (executedContext.Exception != null)
            {
                throw executedContext.Exception;
            }

            throw new InvalidOperationException(string.Format("ActionFilterAttribute of type {0} must supply response or exception.", GetType().Name));
        }

        public virtual void OnActionExecuting(HttpActionContext actionContext)
        {
        }

        public virtual void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
        }

        public virtual Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            this.OnActionExecuting(actionContext);
            return Task.FromResult(new object());
        }

        public virtual Task OnActionExecutedAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            this.OnActionExecuted(actionExecutedContext);
            return Task.FromResult(new object());
        }
    }
}