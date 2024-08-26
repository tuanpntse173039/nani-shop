using System.Text;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Helpers;

public class CacheAttribute : Attribute, IAsyncActionFilter
{
    private readonly int _timeToLiveSecond;

    public CacheAttribute(int timeToLiveSecond)
    {
        _timeToLiveSecond = timeToLiveSecond;
    }

    public async Task OnActionExecutionAsync(
        ActionExecutingContext context,
        ActionExecutionDelegate next
    )
    {
        var cacheService =
            context.HttpContext.RequestServices.GetRequiredService<IResponseCacheService>();

        var cacheKey = GenerateCacheKeyFromRequest(context.HttpContext.Request);
        var cacheRespone = await cacheService.GetCachedResponseAsync(cacheKey);

        if (!string.IsNullOrEmpty(cacheRespone))
        {
            var contentResult = new ContentResult
            {
                Content = cacheRespone,
                ContentType = "application/json",
                StatusCode = 200,
            };

            context.Result = contentResult;
            return;
        }

        var executedContext = await next();

        if (executedContext.Result is OkObjectResult okObjectResult)
        {
            await cacheService.CacheResponseAsync(
                cacheKey,
                okObjectResult.Value,
                TimeSpan.FromSeconds(_timeToLiveSecond)
            );
        }
    }

    private string GenerateCacheKeyFromRequest(HttpRequest request)
    {
        var cacheKey = new StringBuilder();
        cacheKey.Append($"{request.Path}");

        foreach (var (key, value) in request.Query.OrderBy(x => x.Key))
        {
            cacheKey.Append($"|{key}-{value}");
        }

        return cacheKey.ToString();
    }
}
