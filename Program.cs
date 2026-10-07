using System;
using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using HelloWorld.Models;
using HelloWorld.Data;
using Microsoft.Extensions.Configuration;
namespace HelloWorld

{
    
    class Program
    {
        static void Main(string[] args)
        {

            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            DataContextDapper dapper = new(config);

            
             
            Computer myComputer = new()
            {
                Motherboard = "ASUS ROG Strix Z790-E",
                CPUCores = 16,
                HasWifi = true,
                HasLTE = false,
                ReleaseDate = new DateTime(2023, 1, 1),
                Price = 2499.99m,
                VideoCard = "NVIDIA GeForce RTX 4090"
            };

            string sql = @"INSERT INTO ToturialAppSchema.Computer (
            Motherboard,
             CPUCores, 
             HasWifi, 
             HasLTE, 
             ReleaseDate, 
             Price, 
             VideoCard
             ) VALUES ('" + myComputer.Motherboard 
            + "', " + myComputer.CPUCores 
            + ", " + (myComputer.HasWifi ? 1 : 0) 
            + ", " + (myComputer.HasLTE ? 1 : 0)
            + ", '" + myComputer.ReleaseDate.ToString("yyyy-MM-dd")
            + "', " + myComputer.Price.ToString(System.Globalization.CultureInfo.InvariantCulture) 
            + ", '" + myComputer.VideoCard + "')";
            int result = dapper.ExecuteQueryRow(sql);

            // Console.WriteLine($"Inserted {result} row(s) into the database.");

            string sqlSelect = @"SELECT  
            Motherboard,
             CPUCores, 
             HasWifi, 
             HasLTE, 
             ReleaseDate, 
             Price, 
             VideoCard FROM ToturialAppSchema.Computer
             WHERE VideoCard LIKE '%NVIDIA%'AND CPUCores > 12 AND Price > 2500
             ";
            IEnumerable<Computer> computers = dapper.GetData<Computer>(sqlSelect);
            foreach(Computer singleComputer in computers)
            {
                Console.WriteLine($"Motherboard: {singleComputer.Motherboard}, CPU Cores: {singleComputer.CPUCores}, Has Wifi: {singleComputer.HasWifi}, Has LTE: {singleComputer.HasLTE}, Release Date: {singleComputer.ReleaseDate}, Price: {singleComputer.Price}, Video Card: {singleComputer.VideoCard}");
            }

            // Console.WriteLine(myComputer.Motherboard);
            // Console.WriteLine(myComputer.CPUCores);
            // Console.WriteLine(myComputer.HasWifi);
            // Console.WriteLine(myComputer.HasLTE);
            // Console.WriteLine(myComputer.ReleaseDate);
            // Console.WriteLine(myComputer.Price);
            // Console.WriteLine(myComputer.VideoCard);
            // Console.WriteLine("Hello World!");
            // string[] myGroceries = new string[3];
            //     // myGroceries[0] = "Apples";
            //     // myGroceries[1] = "Bananas";
            //     // myGroceries[2] = "Oranges"; // This line will throw an IndexOutOfRangeException

            // List<string> myGroceryList = new List<string>();

            // myGroceryList.Add("Grapes");

            // Console.WriteLine(myGroceryList[0]);

            // IEnumerable<string> myGroceryList2 = new List<string> { "Milk", "Eggs" }; 
      

        }

    //    static private int GetSum(int[] intsToCompress){
    //     int totalValue = 0;
    //     foreach (int intForCompression in intsToCompress)
    //     {
    //         totalValue += intForCompression;
    //     }

    //     return totalValue;

    //    } 
    }
}
