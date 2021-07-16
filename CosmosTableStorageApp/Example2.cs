using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CosmosTableStorageApp
{
    class Example2
    {
        //Aim : 
        //1. just by changing connection string we can insert the data to azure storage table with 0 code changes
        //2. Tp prove cosmos table apis are common for both azure cosmos table  and azure storage table

        //Step 1: Create Azure storage account in the azure portal 
        //Step 2:  Create a table called Customer

        static string connection_string = "DefaultEndpointsProtocol=https;AccountName=storagekpmgbooks;AccountKey=OVxH5ou/Ikys5/2+gITSssH4OG+n0Rdfa6EiV3FKT216b4Y0it0pb++cOunpubkrJa30J45CGWeVuAfu15msgA==;EndpointSuffix=core.windows.net";
        public static async Task Main(string[] args)
        {
            //NewItem().Wait();
            //ReadItem().Wait();
            //UpdateItem().Wait();
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
            //Partition key and row key are mandatory while updateing the object
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

            //Delete operation expects to have complete object reference to delete
            TableOperation p_delete = TableOperation.Delete(return_obj);

            response = await p_table.ExecuteAsync(p_delete);
            Console.WriteLine("Entity deleted");

        }


    }
}
