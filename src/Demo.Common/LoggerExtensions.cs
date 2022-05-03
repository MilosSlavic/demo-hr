using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Exceptions;
using Serilog.Exceptions.Core;
using Serilog.Formatting.Elasticsearch;

namespace Demo.Common
{
    public static class LoggerExtensions
    {
        public static IHostBuilder UseCustomLogging(this IHostBuilder builder)
        {
            return builder.UseSerilog((ctx, cfg) =>
            {
                cfg
                    .Enrich.FromLogContext()
                    .Enrich.WithExceptionDetails(
                            new DestructuringOptionsBuilder()
                            .WithDefaultDestructurers()
                            .WithDestructuringDepth(3))
                    .WriteTo.Debug()
                    .ReadFrom.Configuration(ctx.Configuration)
                    .WriteTo.Console(new ElasticsearchJsonFormatter());
            });
        }
    }
}