using AutoMapper;
using eGTS.Bussiness.ExcerciseService;
using eGTS.Bussiness.AccountService;
using eGTS.Bussiness.FoodAndSupplimentService;
using eGTS.Bussiness.LoginService;
using eGTS.Bussiness.PackageService;
using eGTS_Backend.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<EGtsContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("EGTSDbConnection"));
});


builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IPackageService, PackageService>();
builder.Services.AddScoped<IFoodAndSupplimentService, FoodAndSupplimentService>();
builder.Services.AddScoped<IExcerciseService, ExcerciseService>();

//swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

//JWT
builder.Services.AddAuthentication().AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateAudience = false,
        ValidateIssuer = false,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Appsettings:Token").Value!))
    };
});
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

//swagger
app.UseSwaggerUI();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//fix cors error
app.UseCors(builder => builder.WithOrigins("*")
                               .AllowAnyMethod()
                               .AllowAnyHeader());

//swagger
app.UseSwagger(x => x.SerializeAsV2 = true);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
