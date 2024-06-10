using BlogApp.WebAdmin.Config;
using BlogApp.WebAdmin.Models;
using Microsoft.Extensions.Options;

namespace BlogApp.WebAdmin.Services;

public class WebArticleService(IOptions<ClientConfig> config) : WebApiModelService<ArticleModel>(config, "articles")
{
}
