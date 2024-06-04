using CloudinaryDotNet;
using Microsoft.EntityFrameworkCore;
using MyApiNetCore8.Data;
using MyApiNetCore8.Repositories;
using MyApiNetCore8.Repositories.impl;
using MyApiNetCore8.Repository;
using MyApiNetCore8.Repository.impl;
using dotenv.net;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options => options.AddDefaultPolicy(policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

builder.Services.AddDbContext<MyContext>(options =>
{
    options.UseMySql(builder.Configuration.GetConnectionString("dotnetStore"),
                   new MySqlServerVersion(new Version(8, 0, 26)),
                   mysqlOptions =>
                   {
                       mysqlOptions.EnableRetryOnFailure(
                           maxRetryCount: 5,
                           maxRetryDelay: TimeSpan.FromSeconds(5),
                           errorNumbersToAdd: null);
                   });
});

// Configuring AutoMapper
builder.Services.AddAutoMapper(typeof(Program));
// Configuring Repositories
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IPermissionRepository, PermissionRepository>();
builder.Services.AddScoped<ICouponRepository, CouponRepository>();
//Configuring Cloudinary
DotEnv.Load(new DotEnvOptions(probeForEnv: true));
Account account = new Account(
  "drsxhbpwp",
  "552366933472957",
  "JXjOqMvW5-IE6BAPH9sGAF5wc68");

Cloudinary cloudinary = new Cloudinary(account);

// Register the Singleton instance of Cloudinary
builder.Services.AddSingleton(cloudinary);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
