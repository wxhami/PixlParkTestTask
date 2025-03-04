using MassTransit;
using Microsoft.AspNetCore.Localization;
using WebClient;
using WebClient.Components;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddEnvironmentVariables().AddUserSecrets<Program>(true);

builder.Services.AddSettings(builder.Configuration).AddQueue(builder.Configuration).AddLocal()
    .AddServerServiceClient(builder.Configuration);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();


var app = builder.Build();


app.UseRequestLocalization();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();


app.MapGet(
    "/culture/set",
    (string culture, string redirectUri, HttpContext context) =>
    {
        if (culture != null)
        {
            context.Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(
                    new RequestCulture(culture, culture)));
        }

        return Results.LocalRedirect(redirectUri);
    });

app.Run();