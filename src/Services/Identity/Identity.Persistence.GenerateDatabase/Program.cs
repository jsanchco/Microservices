using Identity.Persistence.Database;
using Identity.Persistence.Database.Configuration;
using Identity.Persistence.Database.Utils;
using System;

namespace Identity.Persistence.GenerateDatabase
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("*********************************************");
            Console.WriteLine("*         Process Generate Database         *");
            Console.WriteLine("*********************************************");

            Console.WriteLine("Press any key to start ...");
            Console.ReadLine();           

            Console.WriteLine("");
            Console.WriteLine("Start Process ...");
            var context = new ApplicationDbContext(new DatabaseSettings {
                ConnectionString = "mongodb://localhost:27017",
                DatabaseName = "IdentityDB"
            });

            RemoveCollections(context);

            if (!context.Clients.CollectionExists())
            {
                foreach (var client in Config.GetClients())
                {
                    context.Clients.InsertOne(client);
                }
            }

            if (!context.IdentityResources.CollectionExists())
            {
                foreach (var identityResource in Config.GetIdentityResources())
                {
                    context.IdentityResources.InsertOne(identityResource);
                }
            }

            if (!context.ApiResources.CollectionExists())
            {
                foreach (var apiResource in Config.GetApiResources())
                {
                    context.ApiResources.InsertOne(apiResource);
                }
            }

            Console.WriteLine("");
            Console.WriteLine("End Process");

            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("Press any key to exit ...");

            Console.ReadLine();
        }

        private static void RemoveCollections(ApplicationDbContext context)
        {
            Console.WriteLine("");
            Console.WriteLine("Do you want remove all documents in your collections?");
            var readKey = Console.ReadKey();
            Console.WriteLine("");
            if (readKey.KeyChar == 'Y' || readKey.KeyChar == 'y')
            {
                context.Database.DropCollection(nameof(context.Clients));
                context.Database.DropCollection(nameof(context.IdentityResources));
                context.Database.DropCollection(nameof(context.ApiResources));

                Console.WriteLine("Remove all collections");
            }
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            ClearCurrentConsoleLine();
        }

        private static void ClearCurrentConsoleLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }
    }
}
