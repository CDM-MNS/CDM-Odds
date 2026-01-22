using System.Text;
using System.Text.Json;
using CDM.Match;
using CDM.Match.Consumer;
using CDM.Match.Repository;
using CMD.Odds.Messaging;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOpenApi();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Configuration Dbcontext
builder.Services.AddDbContext<OddDbContext>(options =>
    options.UseNpgsql(connectionString));

// Injection de dépendances des repositories
builder.Services.AddScoped<IOddsRepository, OddsRepository>();
builder.Services.AddScoped<IRabbitMQService, RabbitMQService>();

//builder.Services.AddHostedService<RabbitMqConsumerService>();


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

// MassTransit configuration
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<GetOddsConsumer>();
    x.AddConsumer<PongConsumer>();

    x.SetDefaultEndpointNameFormatter();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("rabbitmq", "/", h =>
        {
            h.Username("user");
            h.Password("password");
        });

        cfg.ConfigureJsonSerializerOptions(options =>
        {
            options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            return options;
        });
        
        cfg.ReceiveEndpoint("get-odds-in", e =>
        {
            e.UseRawJsonDeserializer();
            e.ConfigureConsumer<GetOddsConsumer>(context);
        });
        
        cfg.ReceiveEndpoint("pong-queue", e =>
        {
            e.UseRawJsonDeserializer();
            e.ConfigureConsumer<PongConsumer>(context);
        });
    });
});

var app = builder.Build();


// Update de la ddb à partir de la dernière migration
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<OddDbContext>();
        
        context.Database.Migrate();
        
        Console.WriteLine("Database migrations OK.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Database migration NOT OK: {ex.Message}");
    }
}

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    
    app.MapScalarApiReference(options =>
    {
        options.Title = "Odds API";
        options.Theme = ScalarTheme.Moon;
    });
}





app.UseHttpsRedirection();  
app.UseAuthorization();
app.MapControllers();

/*
IRabbitMQService rmq = new RabbitMqConsumerService();
await rmq.ConsumeAsync("get-odds");
*/


app.Run();