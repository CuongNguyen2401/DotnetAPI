using Microsoft.EntityFrameworkCore;
using MyApiNetCore8.Data;
using MyApiNetCore8.Repository;
using MyApiNetCore8.Repository.impl;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options => options.AddDefaultPolicy(policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

builder.Services.AddDbContext<MyContext>(options =>
{
    options.UseMySql(builder.Configuration.GetConnectionString("dotnetStore"), new MySqlServerVersion(new Version(8, 0, 26)));
});
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<IProductRepository,ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
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
