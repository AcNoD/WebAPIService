﻿using System.Globalization;
using System.Messaging;
using System.Web.Configuration;

namespace WebAPIService
{
    /// <summary>
    /// MSMQ Helper
    /// </summary>
    public class Msmq
    {
        private static long _queueId;

        /// <summary>
        /// Gets or creates queue
        /// </summary>
        /// <returns>Queue object</returns>
        private static MessageQueue GetQueue()
        {
            var queueName = WebConfigurationManager.AppSettings["queueName"];
            return MessageQueue.Exists(queueName) ? new MessageQueue(queueName) : MessageQueue.Create(queueName);
        }

        /// <summary>
        /// Send new message to a queue
        /// </summary>
        /// <param name="body">Message body</param>
        public static void SendMessage(string body)
        {
            var message = new Message {Body = body, Label = _queueId++.ToString(CultureInfo.InvariantCulture)};
            var queue = GetQueue();
            queue.Send(message);
        }

        /// <summary>
        /// Receives message from a queue and returns it body
        /// </summary>
        /// <returns></returns>
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
