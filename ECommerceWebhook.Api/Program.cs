using ECommerceWebhook.Application.Services;
using ECommerceWebhook.Domain.Ports;
using ECommerceWebhook.Infrastructure.DbContexts;
using ECommerceWebhook.Infrastructure.Repositories;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

#region Dependency Injection

builder.Services.AddControllers();

builder.Services.AddScoped<IEventsService, EventsService>();
builder.Services.AddScoped<IEventsRepository, EventsRepository>();

builder.Services.AddScoped<IWebhooksService, WebhooksService>();
builder.Services.AddScoped<IWebhooksRepository, WebhooksRepository>();

#region Configure SQLite

var dbConn = new SqliteConnection("Filename=:memory:");
await dbConn.OpenAsync();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlite(dbConn, b => b.MigrationsAssembly("ECommerceWebhook.Api"));
});

#endregion

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

#endregion

var app = builder.Build();

#region Migrate the database

// Because of using In-Memory database, it has to be migrated every time the app starts
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();
}

#endregion

#region Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

#endregion