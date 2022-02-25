using Confluent.Kafka;
using ConsoleApp_consumer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kafka
{
    public class KafkaConsumer : IKafkaConsumer
    {
        private bool disposeHasBeenCalled = false;
        private readonly object disposeHasBeenCalledLockObj = new object();

        private readonly IConsumer<string, string> _consumer;

        /// <summary>
        /// 建構函式，初始化配置
        /// </summary>
        /// <param name="config">配置引數</param>
        /// <param name="topic">主題名稱</param>
        public KafkaConsumer(ConsumerConfig config, string topic)
        {
            _consumer = new ConsumerBuilder<string, string>(config).Build();

            _consumer.Subscribe(topic);
        }

        /// <summary>
        /// 消費
        /// </summary>
        /// <returns></returns>
        public T Consume<T>() where T : class
        {
            try
            {
                var result = _consumer.Consume(TimeSpan.FromSeconds(1));
                if (result != null)
                {
                    if (typeof(T) == typeof(string))
                        return (T)Convert.ChangeType(result.Value, typeof(T));

                    return JsonConvert.DeserializeObject<T>(result.Value);
                }
            }
            catch (ConsumeException e)
            {
                Console.WriteLine($"consume error: {e.Error.Reason}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"consume error: {e.Message}");
            }

            return default;
        }

        /// <summary>
        /// 釋放
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            lock (disposeHasBeenCalledLockObj)
            {
                if (disposeHasBeenCalled) { return; }
                disposeHasBeenCalled = true;
            }

            if (disposing)
            {
                _consumer?.Close();
            }
        }
    }


}