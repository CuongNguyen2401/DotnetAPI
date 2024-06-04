using CloudinaryDotNet;
using Microsoft.EntityFrameworkCore;
using MyApiNetCore8.Data;
using MyApiNetCore8.Repositories;
using MyApiNetCore8.Repositories.impl;
using MyApiNetCore8.Repository;
using MyApiNetCore8.Repository.impl;
using dotenv.net;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MyApiNetCore8.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options => options.AddDefaultPolicy(policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<MyContext>().AddDefaultTokenProviders();

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
builder.Services.AddScoped<ICouponRepository, CouponRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();

//Configuring Cloudinary
DotEnv.Load(new DotEnvOptions(probeForEnv: true));
Account account = new Account(
  "drsxhbpwp",
  "552366933472957",
  "JXjOqMvW5-IE6BAPH9sGAF5wc68");
Cloudinary cloudinary = new Cloudinary(account);

// Register the Singleton instance of Cloudinary
builder.Services.AddSingleton(cloudinary);

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new
        Microsoft.IdentityModel.Tokens.TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes
        (builder.Configuration["JWT:Secret"]))
    };
});


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
