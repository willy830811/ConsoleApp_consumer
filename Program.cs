using Confluent.Kafka;
using Kafka;
using System;

namespace ConsoleApp_consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            {
                var config = new ConsumerConfig
                {
                    BootstrapServers = "localhost:9092",
                    GroupId = "test",
                    AutoOffsetReset = AutoOffsetReset.Earliest
                };

                string text;
                Console.WriteLine("接受中......");
                while ((text = Console.ReadLine()) != "q")
                {
                    //接受訊息
                    using (var kafkaProducer = new KafkaConsumer(config, "topic-d"))
                    {
                        var result = kafkaProducer.Consume<object>();
                        if (result != null)
                        {
                            Console.WriteLine(result.ToString());
                        }

                    }
                }

            }
        }
    }
}
