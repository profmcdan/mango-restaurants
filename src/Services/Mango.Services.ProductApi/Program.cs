using Amazon.S3;
using AutoMapper;
using Mango.Services.ProductApi.Config;
using Mango.Services.ProductApi.DbContexts;
using Mango.Services.ProductApi.Helpers;
using Mango.Services.ProductApi.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();

// Add services to the container.
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddDbContext<ProductApiDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ProductDbConnection"));
});
builder.Services.AddScoped<IAWSS3Service, AWSS3Service>();
builder.Services.AddAWSService<IAmazonS3>(builder.Configuration.GetAWSOptions());

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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