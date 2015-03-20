using System;
using System.Web.Http.ExceptionHandling;
using NLog;

namespace WebAPIService.WebAPI
{
    public class UnhandledExceptionLogger : ExceptionLogger
    {
        private readonly Logger _logger;

        public UnhandledExceptionLogger(Logger logger)
        {
            _logger = logger;
        }

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
