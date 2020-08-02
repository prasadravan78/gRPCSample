using Greet;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gRPC_Server
{
    class Program
    {
        const int portNumber = 50051;
        static void Main(string[] args)
        {
            Server server = null;
            try
            {
                server = new Server
                {
                    Services = 
                    { 
                        GreetingService.BindService(new GreetingServiceImplementation()) 
                    },
                    Ports = 
                    { 
                        new ServerPort("localhost", portNumber, ServerCredentials.Insecure) 
                    }
                };

                server.Start();
                Console.WriteLine("Server started on port number: " + portNumber);

                Console.ReadKey();
            }
            catch (IOException exception)
            {
                Console.WriteLine("The server failed to start." + exception.Message);
                throw;
            }
            finally
            {
                if (server != null)
                {
                    server.ShutdownAsync().Wait();
                }
            }
        }
    }
}
