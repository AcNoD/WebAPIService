using System;
using System.Web.Http.ExceptionHandling;
using NLog;

namespace WebAPIService.WebAPI
{
    /// <summary>
    /// Global unhndled exception logger. Catches unhandled exceptions and logs it
    /// </summary>
    public class UnhandledExceptionLogger : ExceptionLogger
    {
        /// <summary>
        /// Logger
        /// </summary>
        private readonly Logger _logger;

        /// <summary>
        /// Ctro
        /// </summary>
        /// <param name="logger">Logger</param>
        public UnhandledExceptionLogger(Logger logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Logs the exceptions
        /// </summary>
        /// <param name="context">Exception logger context</param>
        public override void Log(ExceptionLoggerContext context)
        {
            _logger.Trace("Method: {0}, URI: {1}, Exception: {2}{3}Stack Trace:{4}{5}", 
                context.Request.Method, 
                context.Request.RequestUri, 
                context.Exception.Message,
                Environment.NewLine, Environment.NewLine, context.Exception.StackTrace);
        }
    }
}
