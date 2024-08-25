using irs.API.Shared.Domain.Repositories;
using irs.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using irs.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using irs.API.Shared.Interfaces.ASP.Configuration;
using irs.API.DueDiligence.Application.Internal.CommandServices;
using irs.API.DueDiligence.Application.Internal.QueryServices;
using irs.API.DueDiligence.Domain.Repositories;
using irs.API.DueDiligence.Domain.Services;
using irs.API.DueDiligence.Infrastructure.Persistence.EFC.Repositories;
using irs.API.IAM.Application.Internal.CommandServices;
using irs.API.IAM.Application.Internal.OutboundServices;
using irs.API.IAM.Application.Internal.QueryServices;
using irs.API.IAM.Domain.Model.Repositories;
using irs.API.IAM.Domain.Model.Services;
using irs.API.IAM.Infrastructure.Hashing.BCrypt.Services;
using irs.API.IAM.Infrastructure.Persistence.EFC.Repositories;
using irs.API.IAM.Infrastructure.Pipeline.Middleware.Extensions;
using irs.API.IAM.Infrastructure.Tokens.JWT.Configuration;
using irs.API.IAM.Infrastructure.Tokens.JWT.Services;
using irs.API.IAM.Interfaces.ACL;
using irs.API.IAM.Interfaces.ACL.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options => options.Conventions.Add(new KebabCaseRouteNamingConvention()));

// Add Database Connection
var connectionString = builder.Configuration.GetConnectionString("ProdConnection");


// Configure Database Context and Logging Levels

builder.Services.AddDbContext<AppDbContext>(
    options =>
    {
        if (connectionString != null)
            if (builder.Environment.IsDevelopment())
                options.UseSqlServer(connectionString)
                    .LogTo(Console.WriteLine, LogLevel.Information)
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors();
            else if (builder.Environment.IsProduction())
                options.UseSqlServer(connectionString)
                    .LogTo(Console.WriteLine, LogLevel.Error)
                    .EnableDetailedErrors();
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    c =>
    {
        c.SwaggerDoc("v1",
            new OpenApiInfo
            {
                Title = "IRS.API",
                Version = "v1",
                Description = "IRS API",
                License = new OpenApiLicense
                {
                    Name = "Apache 2.0",
                    Url = new Uri("https://www.apache.org/licenses/LICENSE-2.0.html")
                }
            });
        c.EnableAnnotations();
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Please enter token",
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT",
            Scheme = "bearer"
        });
        c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Id = "Bearer",
                        Type = ReferenceType.SecurityScheme
                    }
                },
                Array.Empty<string>()
            } 
        });
    });
// Configure Lowercase URLs
builder.Services.AddRouting(options => options.LowercaseUrls = true);

// Add CORS Policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowedAllPolicy",
        policy => policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

// Shared Bounded Context Injection Configuration
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IVendorRepository, VendorRepository>();
builder.Services.AddScoped<IVendorQueryService, VendorQueryService>();
builder.Services.AddScoped<IVendorCommandService, VendorCommandService>();


// IAM Bounded Context Injection Configuration
builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection("TokenSettings"));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserCommandService, UserCommandService>();
builder.Services.AddScoped<IUserQueryService, UserQueryService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IHashingService, HashingService>();
builder.Services.AddScoped<IIamContextFacade, IamContextFacade>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()|| app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Apply CORS Policy
app.UseCors("AllowedAllPolicy");

// Add Authorization Middleware to the Request Pipeline


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();