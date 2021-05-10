using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemberShipApp.Extensions
{
    public static class ActionCache
    {
        public static void StateRegionCountryRemoval(IMemoryCache _cache)
        {
            _cache.Remove(CacheKeys.Countries);
            _cache.Remove(CacheKeys.States);
            _cache.Remove(CacheKeys.Regions);
            _cache.Remove(CacheKeys.CountryTuplates);
        }
        public static void PositionRemoval(IMemoryCache _cache)
        {
            _cache.Remove(CacheKeys.Positions);
        }
    }
}
