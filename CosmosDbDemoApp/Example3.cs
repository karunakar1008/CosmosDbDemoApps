using System;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Newtonsoft.Json;

namespace CosmosDbDemoApp
{

  

   public class Example3
    {

       public  static async Task Main(string[] args)
        {
            string accountUri = "https://mysqlapicosmosdb2018.documents.azure.com:443/";
            string accountPrimaryKey = "oZlnIWhNDQwvX6K0HSE5PIKk3RcEH5MO6Ia3SjVoUT1gVFvJtikdR0rp7vQkChdWWJCxpyjfqqomumA3WR0xBA==";

            //Cosmos account
            CosmosClient cosmosClient = new CosmosClient(accountUri, accountPrimaryKey);

            //Create Database if not exist
            var cosmosDb = await cosmosClient.CreateDatabaseIfNotExistsAsync("customerdb");

            //Create container/table
            var customercontainer = await cosmosDb.Database.CreateContainerIfNotExistsAsync("customer", "/customername");

            //Create customer item in the container
            Customer dto = new Customer(1, "karunakar", "bangalore");
            dto.id = Guid.NewGuid().ToString();
            var response=  await customercontainer.Container.CreateItemAsync(dto, new PartitionKey(dto.customername));
            Console.WriteLine("Item created");
            Console.WriteLine(response.Resource.customerid);

        }
    }
}
