using AutoMapper;
using eGTS.Bussiness.AccountService;
using eGTS.Bussiness.LoginService;
using eGTS_Backend.Data.Models;
using Microsoft.EntityFrameworkCore;

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

//swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//swagger
app.UseSwaggerUI();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//swagger
app.UseSwagger(x => x.SerializeAsV2 = true);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
