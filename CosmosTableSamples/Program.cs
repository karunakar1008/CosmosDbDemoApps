using System;

namespace CosmosTableSamples
{
    //Reference: https://docs.microsoft.com/en-gb/azure/cosmos-db/tutorial-develop-table-dotnet
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Azure Cosmos Table Samples");
            BasicSamples basicSamples = new BasicSamples();
            basicSamples.RunSamples().Wait();

            //AdvancedSamples advancedSamples = new AdvancedSamples();
            //advancedSamples.RunSamples().Wait();

            Console.WriteLine();
            Console.WriteLine("Press any key to exit");
            Console.Read();
        }
    }
}
