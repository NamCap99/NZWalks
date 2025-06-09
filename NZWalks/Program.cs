using Microsoft.EntityFrameworkCore;
using NZWalks.Data;
using NZWalks.Mappings;
using NZWalks.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

// 1. Add DbContext
builder.Services.AddDbContext<NZWalksDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("NZWalksConnectionString")));

// 2. Register repository
builder.Services.AddScoped<IRegionRepository, SQLRegionRepository>();

// 3. Add services to the container.
builder.Services.AddControllers();

// 4. Configure Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder = WebApplication.CreateBuilder(args);

// This line will discover all Profile classes in the assembly
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));


// … other services


var app = builder.Build();

// Configure the HTTP request pipeline.
// 5. Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
