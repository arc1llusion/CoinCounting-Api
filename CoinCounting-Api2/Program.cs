using CoinCounting.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(opts => opts.JsonSerializerOptions.PropertyNamingPolicy = null);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CoinContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"), x =>
    {
        x.MigrationsAssembly("CoinCounting-Data");
    });
});

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddCors(options =>
{
    options.AddPolicy("ControllerPolicy", policy =>
    {
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
        //policy.AllowAnyOrigin();
        policy.SetIsOriginAllowed(origin =>
        {
            if (origin.ToLower().StartsWith("http://localhost"))
                return true;

            if (origin.ToLower().StartsWith("http://coincounting-app.azurewebsites.net") || origin.ToLower().StartsWith("https://coincounting-app.azurewebsites.net"))
                return true;

            return false;
        });
    });

    options.DefaultPolicyName = "ControllerPolicy";
});

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
