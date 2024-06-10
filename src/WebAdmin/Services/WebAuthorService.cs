using BlogApp.WebAdmin.Config;
using BlogApp.WebAdmin.Models;
using Microsoft.Extensions.Options;
using RestSharp;

namespace BlogApp.WebAdmin.Services;

public class WebAuthorService(IOptions<ClientConfig> config) : WebApiModelService<AuthorModel>(config, "authors")
{
}
