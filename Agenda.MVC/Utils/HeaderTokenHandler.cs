namespace Agenda.MVC.Utils
{
    public class HeaderTokenHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HeaderTokenHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer",
                _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Bearer")?.Value);

            return base.SendAsync(request, cancellationToken);
        }
    }
}
