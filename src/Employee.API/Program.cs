using Employee.API;
using Employee.API.Grpc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();
builder.Services.AddDbContext<EmployeeDbContext>((ctx, opt) =>
{
    var configuration = ctx.GetRequiredService<IConfiguration>();
    var connStr = configuration.GetConnectionString("DefaultConnection");
    opt.UseSqlServer(connStr);
});

var app = builder.Build();

app.MapGrpcService<EmployeeGrpcServiceImpl>();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<EmployeeDbContext>();
    dbContext.Database.Migrate();
}

app.Run();
