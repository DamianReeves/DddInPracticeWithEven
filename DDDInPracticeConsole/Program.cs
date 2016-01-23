using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using DddInPractice.Logic.ActorModel;
using DddInPractice.Logic.DomainModel;
using Even;
using Even.Persistence;

namespace DDDInPracticeConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var actorSystem = ActorSystem.Create("SnackMachine");
            Task.Run(async () =>
            {
                var eventStore = new InMemoryStore();

                var gateway = await actorSystem
                    .SetupEven()
                    .UseStore(eventStore)
                    .Start("snacks");

                // Send some commands
                await Task.WhenAll(
                    gateway.SendAggregateCommand<SnackMachine>(1,
                        new SnackMachine.DeploySnackMachine(new Money(100,50,50,10,20,0))),
                    gateway.SendAggregateCommand<SnackMachine>(2,
                        new SnackMachine.DeploySnackMachine(new Money(100, 50, 50, 10, 20, 0)))
                    );

                // add some delay to make sure the data is flushed to the store 
                await Task.Delay(100);

                // print the contents of the event store
                Console.WriteLine();
                Console.WriteLine("Event Store Data");
                Console.WriteLine("================");
                Console.WriteLine();
                Console.WriteLine($"{"Seq",-6} {"Stream ID",-50} Event Name");

                foreach (var e in eventStore.GetEvents())
                    Console.WriteLine($"{e.GlobalSequence,-6} {e.Stream,-50} {e.EventType}");

            }).Wait();

            Console.WriteLine();
            Console.WriteLine("Scenario Completed");
            Console.ReadLine();
        }
    }
}
