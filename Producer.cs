using Confluent.Kafka;
using KafkaTest;
using System;
using System.Threading.Tasks;

public class KafkaProducer
{
    private readonly IProducer<string, string> producer;
    private readonly string topic;

    public KafkaProducer(string bootstrapServers, string apiKey, string apiSecret, string topic)
    {
        this.topic = topic;
        var config = new ProducerConfig
        {
            BootstrapServers = "pkc-ldvmy.centralus.azure.confluent.cloud:9092",
            SaslMechanism = SaslMechanism.Plain,
            SaslUsername = "P5BBCQB4OZUDXZP5",
            SaslPassword = "ccfmmmX3YvBg/i6wWcye4vEgMPCXNSwLYp8B2WXjd8dh/SeNps6gs0VRAmrzoDB+",
            SecurityProtocol = SecurityProtocol.SaslSsl,
            Debug = "security,broker,protocol"  // Enable for debugging
        };

        producer = new ProducerBuilder<string, string>(config).Build();
    }

    public async Task ProduceAsync(Transactions transaction)
    {
        try
        {
            var message = new Message<string, string>
            {
                Key = transaction.Id.ToString(),
                Value = Newtonsoft.Json.JsonConvert.SerializeObject(transaction) // Serialize the transaction object to JSON
            };

            DeliveryResult<string, string> result = await producer.ProduceAsync(topic, message);

            Console.WriteLine($"Produced message to: {result.Topic} [Key: {result.Message.Key}, Value: {result.Message.Value}]");
        }
        catch (ProduceException<string, string> e)
        {
            Console.WriteLine($"Delivery failed: {e.Error.Reason}");
        }
    }

    public void Dispose()
    {
        producer.Dispose();
    }
}
