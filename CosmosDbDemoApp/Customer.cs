using System;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Newtonsoft.Json;

namespace CosmosDbDemoApp
{

    class customerDto
    {
        [JsonProperty(PropertyName = "id")]
        public string id { get; set; }
        public int customerid { get; set; }
        public string customername { get; set; }
        public string city { get; set; }

        public customerDto(int p_id, string p_name, string p_city)
        {
            customerid = p_id;
            customername = p_name;
            city = p_city;
        }
    }

   public class Customer
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
            customerDto dto = new customerDto(1, "karunakar", "bangalore");
            dto.id = Guid.NewGuid().ToString();
            var response=  await customercontainer.Container.CreateItemAsync(dto, new PartitionKey(dto.customername));
            Console.WriteLine("Item created");
            Console.WriteLine(response.Resource.customerid);

        }
    }
}
