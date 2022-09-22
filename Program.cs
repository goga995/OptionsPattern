using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OptionsPattern.Data;
using OptionsPattern.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.ConfigureOptions<DatabaseOptionsSetup>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CommandDbContext>(
    (serviceProvider,o) =>
    {
        var databaseOptions = serviceProvider.GetService<IOptions<DatabaseOptions>>()!.Value;

        
        o.UseSqlite(databaseOptions.ConnectionString, sqliteActions =>
        {
            // sqliteActions.EnableRetryOnFailur(3);

            sqliteActions.CommandTimeout(databaseOptions.CommandTimeout);
        });

        o.EnableDetailedErrors(databaseOptions.EnableDetailedErrors);

        o.EnableSensitiveDataLogging(databaseOptions.EnableSensitiveDataLogging);
    }
);


var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<CommandDbContext>();

    DbInitializer.Initialize(context);
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
