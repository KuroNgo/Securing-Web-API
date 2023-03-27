using Azure.Core;
using Bogus.DataSets;
using Microsoft.FSharp.Data.UnitSystems.SI.UnitNames;
using NBomber;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace QuanLiTuyenXeBusDalat.Models
{
    public class RateLimitingSettings
    {
        public const string UserBasedTokenBucket = "RateLimitUserBasedTokenBucket";
        public const string UserBasedSlidingWindow = "RateLimitUserBasedSlidingWindow";
        public const string RateLimitGlobalFixedWindow = "RateLimitGlobalFixedWindow";
        public const string TokenBucket = "RateLimitTokenBucket";
        public const string Concurrency = "RateLimitConcurrency";
        public const string SlidingWindow = "RateLimitSlidingWindow";
        public const string FixedWindow = "RateLimitFixedWindow";
        //The PermitLimit property sets the maximum number of requests that can be made in the specified period(30 seconds in this case)
        public int PermitLimit { get; set; } = 100;
        //The Window property sets the length of the time window for which the rate limit applies.
        public int Window { get; set; } = 10;

        public int ReplenishmentPeriod { get; set; } = 2;
        //The QueueLimit property sets the maximum number of requests that can be queued when the limit has been reached
        public int QueueLimit { get; set; } = 2;
        public int SegmentsPerWindow { get; set; } = 8;
        public int TokenLimit { get; set; } = 10;
        public int TokensPerPeriod { get; set; } = 4;
        public bool AutoReplenishment { get; set; } = false;
    }
}
