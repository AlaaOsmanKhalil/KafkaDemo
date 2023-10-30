using KafkaTest;
using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        string bootstrapServers = "pkc-ldvmy.centralus.azure.confluent.cloud:9092";
        string apiKey = "P5BBCQB4OZUDXZP5";
        string apiSecret = "ccfmmmX3YvBg/i6wWcye4vEgMPCXNSwLYp8B2WXjd8dh/SeNps6gs0VRAmrzoDB+";
        string topic = "transactions";


        var producer = new KafkaProducer(bootstrapServers, apiKey, apiSecret, topic);

        var transactions = new Transactions
        {
            Id = Guid.NewGuid(), // Replace with your desired transaction ID
            Amount = 100.0m,   // Replace with your desired transaction amount
            MerchantName = "Acme Corp"
        };
        
       await producer.ProduceAsync(transactions);
        

        Console.WriteLine("Press Enter to exit...");
        Console.ReadLine();
    }
}
