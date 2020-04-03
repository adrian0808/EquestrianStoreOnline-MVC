using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;

namespace Sklep_internetowy.Infrastructure
{
    public class DefaultCacheProvider : ICacheProvider
    {
        private Cache cache { get { return HttpContext.Current.Cache; } }
        public object Get(string key)
        {
            return cache[key];
        }
        public void Set(string key, object data, int cacheTime)
        {
            var expirationTime = DateTime.Now + TimeSpan.FromMinutes(cacheTime);
            cache.Insert(key, data, null, expirationTime, Cache.NoSlidingExpiration);
        }
        public bool isSet(string key)
        {
            return (cache[key] != null);
        }
        public void Invalidate(string key)
        {
            cache.Remove(key);
        }


    }
}