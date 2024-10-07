using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Store.Services.Services.CacheService;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace Store.Web.Helper
{
    public class CacheAttribute : Attribute , IAsyncActionFilter
    {
        private readonly int _timeToLiveSeconds;

        public CacheAttribute(int TimeToLiveSeconds)
        {
            _timeToLiveSeconds = TimeToLiveSeconds;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var _CacheService = context.HttpContext.RequestServices.GetRequiredService<ICacheService>();

            var CacheKey = GenerateCacheKeyFromRequest(context.HttpContext.Request);

            var CacheResponse = await _CacheService.GetCacheResponseAsync(CacheKey);

            if (!string.IsNullOrEmpty(CacheResponse))
            {
                var contentResult = new ContentResult
                {
                    Content = CacheResponse,
                    ContentType = "application/json" ,
                    StatusCode = 200
                };
                context.Result = contentResult;

                return;
            }

            var excutedContext = await next();

            if (excutedContext.Result is OkObjectResult response)
                await _CacheService.SetCacheResponseAsync(CacheKey , response.Value , TimeSpan.FromSeconds(_timeToLiveSeconds));

        }

        private string GenerateCacheKeyFromRequest(HttpRequest request)
        {
            StringBuilder CacheKey = new StringBuilder();

            CacheKey.Append($"{request.Path}");

            foreach (var (Key , Value) in request.Query.OrderBy(x => x.Key))
                CacheKey.Append($"|{Key}-{Value}");

            return CacheKey.ToString();
        }
    }
}
