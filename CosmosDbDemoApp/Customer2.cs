using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CosmosDbDemoApp
{
    class Customer2
    {
        static string database = "customerdb";
        static string containername = "customer";
        static string endpoint = "https://mysqlapicosmosdb2018.documents.azure.com:443/";
        static string accountkeys = "oZlnIWhNDQwvX6K0HSE5PIKk3RcEH5MO6Ia3SjVoUT1gVFvJtikdR0rp7vQkChdWWJCxpyjfqqomumA3WR0xBA==";

       public static async Task Main(string[] args)
        {
            //await CreateNewItem();
            //await ReadItem();
            //ReplaceItem().Wait();
            DeleteItem().Wait();
            Console.ReadLine();
        }

        private static async Task CreateNewItem()
        {
            using (CosmosClient cosmos_client = new CosmosClient(endpoint, accountkeys))
            {

                Database db_conn = cosmos_client.GetDatabase(database);

                Container container_conn = db_conn.GetContainer(containername);

                Customer obj = new Customer(1, "SatyaNarayana", "HYD");
                obj.id = Guid.NewGuid().ToString(); //16 bit - 


                ItemResponse<Customer> response = await container_conn.CreateItemAsync(obj);
                Console.WriteLine("Request charge is {0}", response.RequestCharge);
                Console.WriteLine("Customer added");
            }
        }

        private static async Task ReadItem()
        {
            using (CosmosClient cosmos_client = new CosmosClient(endpoint, accountkeys))
            {

                Database db_conn = cosmos_client.GetDatabase(database);

                Container container_conn = db_conn.GetContainer(containername);

                string cosmos_sql = "select c.customerid,c.customername,c.city from c";
                QueryDefinition query = new QueryDefinition(cosmos_sql);

                FeedIterator<Customer> iterator_obj = container_conn.GetItemQueryIterator<Customer>(cosmos_sql);


                while (iterator_obj.HasMoreResults)
                {
                    FeedResponse<Customer> customer_obj = await iterator_obj.ReadNextAsync();
                    foreach (Customer obj in customer_obj)
                    {
                        Console.WriteLine("Customer id is {0}", obj.customerid);
                        Console.WriteLine("Customer name is {0}", obj.customername);
                        Console.WriteLine("Customer city is {0}", obj.city);
                    }
                }

            }
        }

        private static async Task ReplaceItem()
        {
            using (CosmosClient cosmos_client = new CosmosClient(endpoint, accountkeys))
            {

                Database db_conn = cosmos_client.GetDatabase(database);

                Container container_conn = db_conn.GetContainer(containername);

                PartitionKey pk = new PartitionKey("HYD");
                string id = "4121b7ea-e629-42b4-bfbc-2cc05c829669";

                ItemResponse<Customer> response = await container_conn.ReadItemAsync<Customer>(id, pk);
                Customer customer_obj = response.Resource;

                customer_obj.customername = "James";

                response = await container_conn.ReplaceItemAsync<Customer>(customer_obj, id, pk);
                Console.WriteLine("Item updated");

            }
        }

        private static async Task DeleteItem()
        {
            using (CosmosClient cosmos_client = new CosmosClient(endpoint, accountkeys))
            {

                Database db_conn = cosmos_client.GetDatabase(database);

                Container container_conn = db_conn.GetContainer(containername);

                PartitionKey pk = new PartitionKey("SatyaNarayana"); //We Assumed partition key as customer name, so provide customer name here
                string id = "7fc5b8a6-4a3c-48ef-aaf2-f4dc2c78a656"; //Id of the item

                ItemResponse<Customer> response = await container_conn.DeleteItemAsync<Customer>(id, pk);

                Console.WriteLine("Item deleted");
            }

        }
    }

}

