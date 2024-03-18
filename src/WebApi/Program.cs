using Webapi.ErrorListeners;
using Webapi.Middleware;
using Webapi.Utils;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

var configuredDisabledValidators = configuration.GetSection("DisabledValidators").Get<string[]>();

builder.Services.AddValidators(configuredDisabledValidators ?? []);
builder.Services.AddErrorListener(new SimpleListener());

builder.Services.AddSingleton<ValidatorMiddleware>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ValidatorMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthorization();

app.MapControllers();

app.Run();
