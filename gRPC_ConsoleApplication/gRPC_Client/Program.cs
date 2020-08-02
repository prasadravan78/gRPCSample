using Dummy;
using Greet;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gRPC_Client
{
    class Program
    {
        const string serverUrl = "127.0.0.1:50051";
        static void Main(string[] args)
        {
            Channel channel = new Channel(serverUrl, ChannelCredentials.Insecure);

            channel.ConnectAsync().ContinueWith((task) =>
            {
                if (task.Status == TaskStatus.RanToCompletion)
                {
                    Console.WriteLine("The client connected Successfully.");
                }
            });

            var greetClient = new GreetingService.GreetingServiceClient(channel);

            var greeting = new Greeting()
            {
                FirstName = "User First Name",
                LastName = "User Last Name"
            };
            var request = new GreetingRequest()
            {
                Greeting = greeting
            };

            var response = greetClient.Greet(request);

            Console.WriteLine("Response from server \n" + response.Result);
            channel.ShutdownAsync().Wait();

            Console.ReadKey();
        }
    }
}
