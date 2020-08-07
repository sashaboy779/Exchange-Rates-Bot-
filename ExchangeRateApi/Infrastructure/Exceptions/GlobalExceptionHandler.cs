using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using ExchangeRateApi.Resources;

namespace ExchangeRateApi.Infrastructure.Exceptions
{
    class GlobalExceptionHandler : ExceptionHandler
    {
        public override Task HandleAsync(ExceptionHandlerContext context, CancellationToken cancellationToken)
        {
            if (ShouldHandle(context))
            {
                context.Result = new BotErrorResult
                {
                    Request = context.ExceptionContext.Request,
                    Content = Messages.ErrorOcurred
                };
            }

            return Task.FromResult(0);
        }

        private class BotErrorResult : IHttpActionResult
        {
            public HttpRequestMessage Request { get; set; }
            public string Content { get; set; }

            public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
            {
                var response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(Content),
                    RequestMessage = Request
                };

                return Task.FromResult(response);
            }
        }
    }
}