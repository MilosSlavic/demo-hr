namespace EmployeeSearchBff.API.Grpc
{
    public static class GrpcClientExtensions
    {
        public static IHttpClientBuilder AddGrpcClient<TClient>(this IServiceCollection services, string configurationKey) where TClient : class
        {
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            return services.AddGrpcClient<TClient>((ctx, cfg) =>
            {
                using var scope = ctx.CreateScope();
                var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<TClient>>();
                var value = configuration.GetValue<string>(configurationKey);
                logger.LogInformation(message: $"Registering client of type {typeof(TClient).Name} using configurationKey: {configurationKey} with value: '{value}'");
                cfg.Address = new Uri(value);
            }).AddHeaderPropagation();
        }
    }
}
