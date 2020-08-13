using System;
using System.Runtime.Caching;

namespace ExchangeRateApi.DataAccess.Repository
{
    public abstract class CachedRepository
    {
        protected ObjectCache Cache { get; set; }
        protected CacheItemPolicy Policy { get; set; }

        public CachedRepository()
        {
            Cache = MemoryCache.Default;
            Policy = new CacheItemPolicy
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(30.0)
            };
        }
    }
}