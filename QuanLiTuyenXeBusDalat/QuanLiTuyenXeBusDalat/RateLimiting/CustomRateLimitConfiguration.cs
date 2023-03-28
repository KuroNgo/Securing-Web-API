using AspNetCoreRateLimit;
using Microsoft.Extensions.Options;

namespace QuanLiTuyenXeBusDalat.RateLimiting
{
    public class CustomRateLimitConfiguration : RateLimitConfiguration
    {
        public CustomRateLimitConfiguration(IOptions<IpRateLimitOptions> ipOptions, 
            IOptions<ClientRateLimitOptions> clientOptions) : base(ipOptions, clientOptions)
        {
        }

        public override void RegisterResolvers()
        {
            base.RegisterResolvers();
            ClientResolvers.Add(new CustomClientResolveContributor());
        }
    }
}
