using OpenTelemetry;
using OpenTelemetry.Trace;
using Prometheus;
using System;
using System.Diagnostics;
using System.Threading;

namespace ConsoleApp1
{
    class Program
    {
        private static readonly Counter TickTock =
            Metrics.CreateCounter("sampleapp_ticks_total", "Just keeps on ticking");

        private static readonly ActivitySource MyActivitySource = new ActivitySource(
            "MyCompany.MyProduct.MyLibrary");

        static void Main()
        {
            using var tracerProvider = Sdk.CreateTracerProviderBuilder()
                .SetSampler(new AlwaysOnSampler())
                .AddSource("MyCompany.MyProduct.MyLibrary")
                .AddZipkinExporter(options => { options.Endpoint = new Uri("http://zipkin:9411/api/v2/spans"); })
                .Build();

            var server = new MetricServer(hostname: "*", port: 1234);
            server.Start();

            while (true)
            {

                using (var activity = MyActivitySource.StartActivity("SayHello"))
                {
                    activity?.SetTag("foo", 1);
                    activity?.SetTag("bar", "Hello, World!");
                    activity?.SetTag("baz", new int[] { 1, 2, 3 });
                }

                TickTock.Inc();
                Thread.Sleep(TimeSpan.FromSeconds(1));
            }
        }
    }
}
