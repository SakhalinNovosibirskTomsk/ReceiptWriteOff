using ReceiptWriteOff.Infrastructure.EntityFramework;
using ReceiptWriteOff.Infrastructure.Repositories.Implementation;
using ReceiptWriteOff.Application.Implementations;
using ReceiptWriteOff.Settings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRepositories();
builder.Services.AddServices();

var applicationSettings = builder.Configuration.Get<ApplicationSettings>();
builder.Services.AddDatabaseContext(applicationSettings!.ConnectionString);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();

app.UseRouting();

app.MapControllers();

app.Run();