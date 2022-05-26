using EmployeeSearchBff.API.Grpc;
using Demo.Common;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();
builder.Services.AddGrpcReflection();
builder.Services.AddHeaderPropagation();

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