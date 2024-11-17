using System.Net.Http.Headers;

namespace MVC.Handlers
{
    public class BearerTokenMessageHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BearerTokenMessageHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancelToken)
        {
            if (request?.Headers?.Authorization != null)
            {
                var token = _httpContextAccessor.HttpContext.User.FindFirst("token").Value;
                request.Headers.Authorization = new AuthenticationHeaderValue(request.Headers.Authorization.Scheme, token);
            }

            return await base.SendAsync(request, cancelToken);
        }
    }
}