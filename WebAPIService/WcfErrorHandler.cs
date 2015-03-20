using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using NLog;

namespace WebAPIService
{
    public class WcfErrorHandler : IErrorHandler
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public void ProvideFault(Exception error, MessageVersion version, ref Message fault)
        {
            var ex = new FaultException();
            var msgFault = ex.CreateMessageFault();
            fault = Message.CreateMessage(version, msgFault, ex.Action);
        }

        public bool HandleError(Exception error)
        {
            Logger.Error(error.Message);
            return true;
        }
    }
}
