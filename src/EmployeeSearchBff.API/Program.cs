using EmployeeSearchBff.API.Grpc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();

var app = builder.Build();

app.MapGrpcService<SearchGrpcServiceImpl>();

app.Run();
