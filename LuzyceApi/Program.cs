using System.Text;
using LuzyceApi;
using LuzyceApi.Db.AppDb.Data;
using LuzyceApi.Db.Subiekt.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using QuestPDF.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddEnvironmentVariables();
var jwtkey = builder.Configuration["Jwt:SigningKey"] ?? throw new KeyNotFoundException("Jwt:SigningKey");

builder.Services.AddLogging(l => l.AddConsole());

var logger = LoggerFactory.Create(config =>
{
    config.AddConsole();
    config.AddConfiguration(builder.Configuration.GetSection("Logging"));
}).CreateLogger("Program");

logger.LogInformation("Starting application");
logger.LogInformation("Signing key: {jwtkey}", jwtkey);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtkey))
        };
    });

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

builder.Services.AddScoped<LuzyceApi.Repositories.UsersRepository>();
builder.Services.AddScoped<LuzyceApi.Repositories.KwitRepository>();
builder.Services.AddScoped<LuzyceApi.Repositories.OrderRepository>();
builder.Services.AddScoped<LuzyceApi.Repositories.ProductionOrderRepository>();
builder.Services.AddScoped<LuzyceApi.Repositories.LampshadeRepository>();
builder.Services.AddScoped<LuzyceApi.Repositories.ProductionPriorityRepository>();
builder.Services.AddScoped<LuzyceApi.Repositories.ProductionPlanRepository>();
builder.Services.AddScoped<LuzyceApi.Repositories.LogRepository>();

builder.Services.AddDbContext<ApplicationDbContext>();
builder.Services.AddDbContext<SubiektDbContext>();

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyHeader()
              .AllowAnyMethod()
              .AllowAnyOrigin();
    });
});

QuestPDF.Settings.License = LicenseType.Community;

var app = builder.Build();

// Apply CORS middleware before other middlewares
app.UseCors();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.UseMiddleware<CorsMiddleware>();

app.Run();
