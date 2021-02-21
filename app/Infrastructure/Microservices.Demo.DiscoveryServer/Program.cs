using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Microservices.Demo.DiscoveryServer
{
    class Program
    {
        static void Main(string[] args)
        {
            StartEureka();
        }

        public static void StartEureka()
        {
            var psi = new ProcessStartInfo
            {
                FileName = @"D:\maven\bin\mvn.cmd",
                Arguments = @"-f discovery-server spring-boot:run",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };

            var proc = new Process
            {
                StartInfo = psi
            };

            proc.Start();

            Task.WaitAll(Task.Run(() =>
            {
                while (!proc.StandardOutput.EndOfStream)
                {
                    var line = proc.StandardOutput.ReadLine();
                    Console.WriteLine(line);
                }
            }), Task.Run(() =>
            {
                while (!proc.StandardError.EndOfStream)
                {
                    var line = proc.StandardError.ReadLine();
                    Console.WriteLine(line);
                }
            }));


            proc.WaitForExit();
            Console.WriteLine(proc.ExitCode);
        }
    }
}
