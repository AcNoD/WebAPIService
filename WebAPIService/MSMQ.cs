using System;
using System.Globalization;
using System.Messaging;
using System.Runtime.Serialization;
using System.Web.Configuration;

namespace WebAPIService
{
    public class Msmq
    {
        private static long _queueId;

        private static MessageQueue GetQueue()
        {
            var queueName = WebConfigurationManager.AppSettings["queueName"];
            return MessageQueue.Exists(queueName) ? new MessageQueue(queueName) : MessageQueue.Create(queueName);
        }

        public static void SendMessage(string body)
        {
            var message = new Message {Body = body, Label = _queueId++.ToString(CultureInfo.InvariantCulture)};
            var queue = GetQueue();
            queue.Send(message);
        }

        public static string ReceiveMessage()
        {
            var queue = GetQueue();
            var message = queue.Receive();
            if (message == null) return string.Empty;
            message.Formatter = new XmlMessageFormatter(new[] { "System.String,mscorlib" });
            return message.Body.ToString();
        }
    }
}
