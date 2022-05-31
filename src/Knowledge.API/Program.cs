using Knowledge.API;
using Knowledge.API.Grpc;
using Microsoft.EntityFrameworkCore;
using Demo.Common;

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
builder.Services.AddDbContext<KnowledgeDbContext>((ctx, opt) =>
{
    var configuration = ctx.GetRequiredService<IConfiguration>();
    var connStr = configuration.GetConnectionString("DefaultConnection");
    opt.UseSqlServer(connStr);
});
builder.Host.UseCustomLogging();

var app = builder.Build();

app.UseHeaderPropagation();
app.MapGrpcService<CertificateGrpcServiceImpl>();
app.MapGrpcService<SkillGrpcServiceImpl>();
if (app.Environment.IsDevelopment())
{
    app.MapGrpcReflectionService();
}

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<KnowledgeDbContext>();
    dbContext.Database.Migrate();
}

app.Run();
