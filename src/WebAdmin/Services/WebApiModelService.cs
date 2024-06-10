using BlogApp.WebAdmin.Config;
using Microsoft.Extensions.Options;
using RestSharp;

namespace BlogApp.WebAdmin.Services;

public abstract class WebApiModelService<TModel>(IOptions<ClientConfig> config, string basePath)
    : WebApiService(config) where TModel : class
{
    public async Task<List<TModel>> GetAll(string token)
    {
        var response = await _client.ExecuteAsync<List<TModel>>(_authorizedRequest(token, basePath));
        return (!response.IsSuccessful || response.Data == null) ? [] : response.Data;
    }

    public async Task<TModel?> Get(string token, int id)
    {
        var response = await _client.ExecuteAsync<TModel>(_authorizedRequest(token, $"{basePath}/{id}"));
        return (!response.IsSuccessful || response.Data == null) ? null : response.Data;
    }

    public async Task Post(string token, TModel model)
    {
        await _client.ExecuteAsync(_authorizedRequest(token, basePath, Method.Post, model));
    }

    public async Task Put(string token, int id, TModel model)
    {
        await _client.ExecuteAsync(_authorizedRequest(token, $"{basePath}/{id}", Method.Put, model));
    }

    public async Task Delete(string token, int id)
    {
        await _client.ExecuteAsync(_authorizedRequest(token, $"{basePath}/{id}", Method.Delete));
    }

    private static RestRequest _authorizedRequest(string token, string? resource, Method method = Method.Get, TModel? model = null)
    {
        var request = new RestRequest(resource, method);
        request.AddHeader("Authorization", $"Bearer {token}");
        if (model is not null) request.AddJsonBody(model);
        return request;
    }
}
