using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Polly;
using Polly.Retry;
using FluentValidation;
using MovementHomeAssignment.DTOs;
using MovementHomeAssignment.InfrastructureLayer;
using MovementHomeAssignment.Interfaces;
using MovementHomeAssignment.Services;

namespace MovementHomeAssignment;

public class Program
{
    private static WebApplicationBuilder  builder;
    public static void Main(string[] args)
    {
        builder = WebApplication.CreateBuilder(args);
        builder.Services.AddControllers();

        AddSwaggerSupport();
        RegisterServices();

        builder.Services.AddDistributedMemoryCache();
        builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);
        builder.Services.AddFluentValidationAutoValidation().AddValidatorsFromAssembly(typeof(MappingProfile).Assembly);


        // Add Polly Resilience Pipeline for database calls
        builder.Services.AddResiliencePipeline("db-retry-policy", pipelineBuilder =>
        {
            pipelineBuilder.AddRetry(new RetryStrategyOptions
            {
                ShouldHandle = new PredicateBuilder().Handle<Exception>(), 
                MaxRetryAttempts = 3,
                Delay = TimeSpan.FromMilliseconds(200),
                BackoffType = DelayBackoffType.Constant
            });
        });


        var app = builder.Build();

        // --- Step 3: Configure the HTTP request pipeline ---
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "MultiLayeredDataApi v1");
                options.RoutePrefix = string.Empty; // Set Swagger UI at the app's root
            });
        }

        app.UseHttpsRedirection();
        app.MapControllers();
        app.Run();
    }

    private static void RegisterServices()
    {
        builder.Services.AddScoped<IDataService, DataService>();
        builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("MyInMemoryDb"));
        builder.Services.AddScoped<IDataRepository, DataRepository>();
        
        builder.Services.AddScoped<IDataSourceFactory, DataSourceFactory>();
        builder.Services.AddScoped<CacheDataSource>();
        builder.Services.AddScoped<FileDataSource>();
        builder.Services.AddScoped<DatabaseDataSource>();
    }

    private static void AddSwaggerSupport()
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new() { Title = "Multi-Layered Data API", Version = "v1" });
        });
    }
}

