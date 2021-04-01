using Elastic.Apm.SerilogEnricher;
using Elastic.CommonSchema.Serilog;

using Microsoft.Extensions.Configuration;

using Serilog;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;

using System;
using System.Reflection;

namespace Common.Infrastructure.Logging.ELK
{
    public static class ELKLog
    {
        //Configure ELK logging using serilog
        public static void ConfigureLogging(IConfigurationRoot configuration, string environment, string elasticUri, Assembly assembly)
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithExceptionDetails()
                .Enrich.WithMachineName()
                .WriteTo.Debug()
                .WriteTo.Console()
                .WriteTo.Elasticsearch(ConfigureElasticSink(elasticUri, environment, assembly))
                .Enrich.WithProperty("Environment", environment)
                .ReadFrom.Configuration(configuration)
                .Enrich.WithElasticApmCorrelationInfo()
                .CreateLogger();
        }

        /// <summary>
        /// Configure and provide a elastic search options
        /// </summary>
        /// <param name="elasticUri"></param>
        /// <param name="environment"></param>
        /// <param name="assembly">Executing Assembly</param>
        /// <returns></returns>
        private static ElasticsearchSinkOptions ConfigureElasticSink(string elasticUri, string environment, Assembly assembly)
        {
            return new ElasticsearchSinkOptions(new Uri(elasticUri))
            {
                CustomFormatter = new EcsTextFormatter(),
                AutoRegisterTemplate = true,
                IndexFormat = $"{assembly.GetName().Name.ToLower().Replace(".", "-")}-{environment?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}"
            };
        }
    }
}
