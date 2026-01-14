using Azure.Messaging.ServiceBus;
using System;
using System.Collections.Generic;
using System.Text;

namespace Auditfunction
{
    public class AuditServiceBusProcessor(ServiceBusClient client) :IAuditServiceBusProcessor
    {
        //private const string queueName = "auditdataqueue";
        private readonly ServiceBusClient _client = client;
        public async Task SendDataToAuditQueue(string messageBody)
        {
            string? queueName = Environment.GetEnvironmentVariable("ServiceBusConnection__queueName");
            ServiceBusSender sender = _client.CreateSender(queueName);

                // 4. Create a ServiceBusMessage from the JSON string
                // The message body is internally handled as a byte array
                ServiceBusMessage message = new ServiceBusMessage(messageBody)
                {
                    ContentType = "application/json" // Optional, but good practice
                };

                try
                {
                   await sender.SendMessageAsync(message);
                    
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
    }
}
