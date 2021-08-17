using StatsN;
using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var statsd = Statsd.New<Udp>(a => { 
                a.HostOrIp = "localhost"; 
                a.Port = 8125;
                a.Prefix = "MyMicroserviceName"; 
            });
            statsd.CountAsync("myapp.counterstat");
            Console.WriteLine("Hello World!");
        }
    }
}
