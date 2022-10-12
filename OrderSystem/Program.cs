using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OrderSystem.Contracts;
using OrderSystem.Data;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//In Memory DB Context (SQL also below if needed)
builder.Services.AddDbContext<OrderDBContext>(options =>
            options.UseInMemoryDatabase("OrderDBContext"));

//builder.Services.AddDbContext<OrderDBContext>(options =>
//     options.UseSqlServer(builder.Configuration.GetConnectionString("TestDB")));

//Add dataseeder
builder.Services.AddTransient<DataSeeder>();

builder.Services.AddScoped<IOrderRepo, SqlOrder>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //SEED SOME DATA
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();
    using (var scope = scopedFactory.CreateScope())
    {
        var service = scope.ServiceProvider.GetService<DataSeeder>();
        service.Seed();
    }

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


