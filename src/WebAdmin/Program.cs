using BlogApp.WebAdmin.Config;
using BlogApp.WebAdmin.Middlewares;
using BlogApp.WebAdmin.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddScoped<WebAuthService>();
builder.Services.AddScoped<WebArticleService>();
builder.Services.AddScoped<WebAuthorService>();
builder.Services.AddScoped<AuthMiddleware>();

builder.Services.Configure<ClientConfig>(builder.Configuration.GetSection("Client"));

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStaticFiles();

app.UseMiddleware<AuthMiddleware>();
app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action}/{id?}");


app.Run();
