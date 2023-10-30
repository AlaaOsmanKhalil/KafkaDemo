using Confluent.Kafka;
using Confluent.Kafka.SyncOverAsync;
using Confluent.SchemaRegistry;
using Confluent.SchemaRegistry.Serdes;
using System;
using System.Collections.Generic;

namespace KafkaTest
{
    public class Consumer
    {
        public void ConsumeTransactions(string bootstrapServers, string schemaRegistryUrl, string groupId, string topic)
        {
            var schemaRegistryConfig = new SchemaRegistryConfig
            {
                Url = schemaRegistryUrl
            };

            var schemaRegistry = new CachedSchemaRegistryClient(schemaRegistryConfig);

            var config = new ProducerConfig
            {
                BootstrapServers = "pkc-ldvmy.centralus.azure.confluent.cloud:9092",
                SaslMechanism = SaslMechanism.Plain,
                SecurityProtocol = SecurityProtocol.SaslSsl,
                SaslUsername = "P5BBCQB4OZUDXZP5",
                SaslPassword = "ccfmmmX3YvBg/i6wWcye4vEgMPCXNSwLYp8B2WXjd8dh/SeNps6gs0VRAmrzoDB+"
            };

            using (var consumer = new ConsumerBuilder<string, Transactions>(config)
                .SetValueDeserializer(new AvroDeserializer<Transactions>(schemaRegistry).AsSyncOverAsync())
                .Build())
            {
                consumer.Subscribe(topic);

                Console.WriteLine("Consuming transactions. Press Ctrl+C to exit.");

                while (true)
                {
                    try
                    {
                        var consumeResult = consumer.Consume();
                        var transaction = consumeResult.Message.Value;

                        Console.WriteLine($"Received transaction: ID: {transaction.Id}, Amount: {transaction.Amount}, Merchant: {transaction.MerchantName}");
                    }
                    catch (ConsumeException e)
                    {
                        Console.WriteLine($"Error consuming message: {e.Error}");
                    }
                }
            }
        }
    }
}
