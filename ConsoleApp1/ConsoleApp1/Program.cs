using StatsN;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var statsd = Statsd.New<Udp>(a =>
            {
                a.HostOrIp = "graphiteapp";
                a.Port = 8125;
                a.Prefix = "MyMicroserviceName";
            });

            Console.WriteLine("Hello World!");
            while (true)
            {
                await statsd.CountAsync("myapp.counterstat");
                Thread.Sleep(TimeSpan.FromSeconds(1));
            }
        }
    }
}
