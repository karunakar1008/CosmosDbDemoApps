using Microsoft.Azure.Cosmos.Table;
using System;
using System.Threading.Tasks;

namespace CosmosTableStorageApp
{
    class Example1
    {
        //Aim: Azure cosmos database with table API

        //Step 1: Create cosmos DB with table end point in the azure portal 
        //Step 2:  create a table called Customer

        static string connection_string = "DefaultEndpointsProtocol=https;AccountName=storagecosmostableapidemo;AccountKey=i6OeO32cXWiaJsoSiLC0DpWhJ8LPjxYzz5pftatsdm4W2UuBqvs36NNKAekWiWNihVaruHfEOhihzdkwzbEdKw==;TableEndpoint=https://storagecosmostableapidemo.table.cosmos.azure.com:443/;";

       public static async Task Main(string[] args)
        {
            //NewItem().Wait();
            //ReadItem().Wait();
            ////UpdateItem().Wait();
           await DeleteItem();
            Console.WriteLine("Hello World!");
        }

        /// <summary>
        /// Insert new item to the existing table in the cosmos datbase.
        /// </summary>
        /// <returns></returns>
        static async Task NewItem()
        {
            CloudStorageAccount p_account = CloudStorageAccount.Parse(connection_string);

            CloudTableClient p_tableclient = p_account.CreateCloudTableClient();

            //get the reference of Customer table that was created in the portal
            CloudTable p_table = p_tableclient.GetTableReference("Customer");

            Customer obj = new Customer("2", "James", "New York1234");

            //Insert table record
            TableOperation p_operation = TableOperation.InsertOrReplace(obj);

            TableResult response = await p_table.ExecuteAsync(p_operation);

            Console.WriteLine("Entity added");
        }


        static async Task ReadItem()
        {
            CloudStorageAccount p_account = CloudStorageAccount.Parse(connection_string);

            CloudTableClient p_tableclient = p_account.CreateCloudTableClient();

            CloudTable p_table = p_tableclient.GetTableReference("Customer");

            string partition_key = "2";
            string rowkey = "James";

            TableOperation p_operation = TableOperation.Retrieve<Customer>(partition_key, rowkey);
            TableResult response = await p_table.ExecuteAsync(p_operation);

            Customer return_obj = (Customer)response.Result;

            Console.WriteLine("Customer ID is {0}", return_obj.PartitionKey);
            Console.WriteLine("Customer Name is {0}", return_obj.RowKey);
            Console.WriteLine("Customer City is {0}", return_obj.city);


        }


        static async Task UpdateItem()
        {
            CloudStorageAccount p_account = CloudStorageAccount.Parse(connection_string);

            CloudTableClient p_tableclient = p_account.CreateCloudTableClient();

            CloudTable p_table = p_tableclient.GetTableReference("Customer");

            string partition_key = "2";
            string rowkey = "James";

            Customer updated_obj = new Customer(partition_key, rowkey, "Chicago");

            TableOperation p_operation = TableOperation.InsertOrReplace(updated_obj);
            TableResult response = await p_table.ExecuteAsync(p_operation);
            Console.WriteLine("Entity updated");

        }

        static async Task DeleteItem()
        {
            CloudStorageAccount p_account = CloudStorageAccount.Parse(connection_string);

            CloudTableClient p_tableclient = p_account.CreateCloudTableClient();

            CloudTable p_table = p_tableclient.GetTableReference("Customer");

            string partition_key = "2";
            string rowkey = "James";

            TableOperation p_operation = TableOperation.Retrieve<Customer>(partition_key, rowkey);
            TableResult response = await p_table.ExecuteAsync(p_operation);

            Customer return_obj = (Customer)response.Result;

            //Delete operation expects to have cmoplete object refeence
            TableOperation p_delete = TableOperation.Delete(return_obj);

            response = await p_table.ExecuteAsync(p_delete);
            Console.WriteLine("Entity deleted");

        }

    }
}
