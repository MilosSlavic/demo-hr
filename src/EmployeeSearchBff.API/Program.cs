using EmployeeSearchBff.API.Grpc;
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
builder.Services.AddGrpcClient<Employee.API.EmployeeGrpcService.EmployeeGrpcServiceClient>(configurationKey: "EMPLOYEE_GRPC_API");
builder.Services.AddGrpcClient<Knowledge.API.Certificate.CertificateGrpcService.CertificateGrpcServiceClient>(configurationKey: "KNOWLEDGE_GRPC_API");
builder.Services.AddGrpcClient<Knowledge.API.Skill.SkillGrpcService.SkillGrpcServiceClient>(configurationKey: "KNOWLEDGE_GRPC_API");
builder.Host.UseCustomLogging();

var app = builder.Build();

app.UseHeaderPropagation();
app.MapGrpcService<SearchGrpcServiceImpl>();
if (app.Environment.IsDevelopment())
{
    app.MapGrpcReflectionService();
}
app.Run();