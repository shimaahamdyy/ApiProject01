using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Services.Services.CacheService
{
    public interface ICacheService
    {
        Task SetCacheResponseAsync(string Key, object response, TimeSpan timeForLive);

        Task<string> GetCacheResponseAsync(string Key);

    }
}
