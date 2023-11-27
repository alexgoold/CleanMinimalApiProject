using Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;
using Server.DependencyInjection;
using Server.Extensions.OrderEndpoints;
using Server.Extensions.ProductEndpoints;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ShopContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("UsersDb");
    options.UseSqlServer(connectionString, b => b.MigrationsAssembly("Server"));
    
});

DependencyInjection.AddDependencyInjection(builder.Services, builder);


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
app.MapProductEndpoints();
app.MapOrderEndpoints();

app.Run();
