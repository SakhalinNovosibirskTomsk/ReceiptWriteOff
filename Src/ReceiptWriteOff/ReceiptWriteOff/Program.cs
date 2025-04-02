using Microsoft.EntityFrameworkCore;
using ReceiptWriteOff.Infrastructure.Repositories.Implementation;
using ReceiptWriteOff.Application.Implementations;
using ReceiptWriteOff.Infrastructure.EntityFramework.Implementation;
using ReceiptWriteOff.Mapping;
using ReceiptWriteOff.Settings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var applicationSettings = builder.Configuration.Get<ApplicationSettings>();
builder.Services.AddDatabaseContext(applicationSettings!.ConnectionString);
builder.Services.AddMapping();
builder.Services.AddRepositories();
builder.Services.AddServices();
builder.Services.AddControllers(options => options.SuppressAsyncSuffixInActionNames = false);

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

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
    await db.Database.MigrateAsync();
}

app.Run();