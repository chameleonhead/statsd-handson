using StatsN;
using System;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args) => MainAsync(args).Wait();

        static async Task MainAsync(string[] args)
        {
            var statsd = Statsd.New<Udp>(a =>
            {
                a.HostOrIp = "localhost";
                a.Port = 8125;
                a.Prefix = "MyMicroserviceName";
            });

            Console.WriteLine("Hello World!");
            while (true)
            {
                Console.ReadKey();
                await statsd.CountAsync("myapp.counterstat");
            }
        }
    }
}
