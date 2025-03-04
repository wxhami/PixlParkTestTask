using ServerService;
using ServerService.Redis;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddEnvironmentVariables().AddUserSecrets<Program>(true);

builder.Services.AddRedis().AddSettings(builder.Configuration)
    .AddQueue(builder.Configuration)
    .AddServices().AddEndpointsApiExplorer().AddSwaggerGen();


var app = builder.Build();

app.MapGet("/code-info/{email}", async (string email, ICashService cashService) =>
{
    var codeInformation = await cashService.GetAsync(email);
    return codeInformation == null ? Results.NotFound() : Results.Ok(codeInformation);
});

app.Run();