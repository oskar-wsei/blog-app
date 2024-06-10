using BlogApp.WebAdmin.Config;
using Microsoft.Extensions.Options;
using RestSharp;

namespace BlogApp.WebAdmin.Services;

public abstract class WebApiService(IOptions<ClientConfig> config)
{
    protected readonly RestClient _client = new (config.Value.WebApiUrl);
    
}
