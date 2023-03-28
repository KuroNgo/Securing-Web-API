using AspNetCoreRateLimit;

namespace QuanLiTuyenXeBusDalat.RateLimiting
{
    public class CustomClientResolveContributor : IClientResolveContributor
    {
        public Task<string> ResolveClientAsync(HttpContext httpContext)
        {
            var nickHeaderValue = string.Empty;
            if(httpContext.Request.Headers.TryGetValue("Kuro-Header",out var values))
            {
                nickHeaderValue = values.ToString();
            }    
            return Task.FromResult(nickHeaderValue);
        }
    }
}
