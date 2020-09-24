using CandidateTesting.BrunoSenaeSilvaCorreia.AgoraLog.Integration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CandidateTesting.BrunoSenaeSilvaCorreia.AgoraLog
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
            .AddSingleton<ILogger, CDN>()
            .BuildServiceProvider();

            Uri httpOriginLog = null;
            var cdn = serviceProvider.GetService<ILogger>();

            try
            {
                if (args.Length < 2 || !Uri.TryCreate(args[0], UriKind.Absolute, out httpOriginLog) || string.IsNullOrWhiteSpace(args[1]))
                {
                    throw new ArgumentException("Wrong arguments!");
                }

                var agoraLog = new AgoraLog(cdn);
                agoraLog.WriteFromCDN(httpOriginLog, args[1]);
                Console.WriteLine("The log was converted with success!!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("######## Error: " + ex.Message);
            }
        }
    }
}
