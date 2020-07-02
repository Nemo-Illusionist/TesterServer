using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Tester.Web.Core
{
    public abstract class BaseStartup
    {
        protected ILoggerFactory LoggerFactory { get; }
        protected IConfiguration Configuration { get; }
        protected IWebHostEnvironment Env { get; }

        protected BaseStartup([NotNull] ILoggerFactory loggerFactory,
            [NotNull] IConfiguration configuration,
            IWebHostEnvironment env)
        {
            LoggerFactory = loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory));
            Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            Env = env ?? throw new ArgumentNullException(nameof(env));
        }
    }
}