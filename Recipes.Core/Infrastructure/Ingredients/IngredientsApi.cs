using System.Net.NetworkInformation;
using Microsoft.Extensions.Options;
using Recipes.Core.Application.Contracts;

namespace Recipes.Core.Infrastructure.Ingredients;

public class IngredientsApi : IIngredientsApi
{
    private readonly IOptions<IngredientsApiSettings> _options;
    private readonly HttpClient _httpClient;

    public IngredientsApi(IOptions<IngredientsApiSettings> options, HttpClient httpClient)
    {
        _options = options;
        _httpClient = httpClient;
    }

    public async Task<bool> IsHealthyAsync(CancellationToken cancellationToken)
    {
        var host = new Uri(_options.Value.BaseUrl).Host;
        
        using var ping = new Ping();
        
        var replyTask = await ping.SendPingAsync(host);

        return replyTask.Status == IPStatus.Success;
    }
}