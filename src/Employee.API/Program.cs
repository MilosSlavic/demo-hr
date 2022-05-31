using Employee.API;
using Employee.API.Grpc;
using Microsoft.EntityFrameworkCore;
using Demo.Common;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();
builder.Services.AddGrpcReflection();
builder.Services.AddHeaderPropagation(opt =>
{
    opt.Headers.Add("x-request-id");
    opt.Headers.Add("x-b3-traceid");
    opt.Headers.Add("x-b3-spanid");
    opt.Headers.Add("x-b3-parentspanid");
    opt.Headers.Add("x-b3-sampled");
    opt.Headers.Add("xx-b3-flags");
    opt.Headers.Add("b3");
});
builder.Services.AddDbContext<EmployeeDbContext>((ctx, opt) =>
{
    var configuration = ctx.GetRequiredService<IConfiguration>();
    var connStr = configuration.GetConnectionString("DefaultConnection");
    opt.UseSqlServer(connStr);
});
builder.Host.UseCustomLogging();

var app = builder.Build();

app.UseHeaderPropagation();
app.MapGrpcService<EmployeeGrpcServiceImpl>();
if (app.Environment.IsDevelopment())
{
    app.MapGrpcReflectionService();
}
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<EmployeeDbContext>();
    dbContext.Database.Migrate();
}

app.Run();
