using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_consumer
{
    interface IKafkaConsumer : IDisposable
    {
        /// <summary>
        /// 消費資料
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T Consume<T>() where T : class;
    }
}
