using Knowledge.API.Grpc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();

var app = builder.Build();

app.MapGrpcService<CertificateGrpcServiceImpl>();
app.MapGrpcService<SkillGrpcServiceImpl>();

app.Run();
