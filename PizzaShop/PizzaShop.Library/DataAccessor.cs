using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PizzaShop.Data;
using PizzaShop.Library.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PizzaShop.Library
{
    public static class DataAccessor
    {
        public static DataHandler DH { get; set; }
        //public static RepoHandler RH { get; set; }


        public static readonly string serializationFilepath = @"C:\Revature\kirk-project1\PizzaShop\data.xml";
        //public static readonly string serializationFilepath = @"E:\Revature\kirk-project1\PizzaShop\data.xml";

        public static void Setup(bool importFromXML, bool useSQL)
        {
            if (useSQL)
            {
                //INSERTING SQL STUFF HERE

                // get the configuration from file
                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

                IConfigurationRoot configuration = builder.Build();

                //Use to confirm .json is being read in properly
                //Console.WriteLine(configuration.GetConnectionString("Project1DB"));

                var optionsBuilder = new DbContextOptionsBuilder<Project1DBContext>();
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("Project1DB"));
                var options = optionsBuilder.Options;

                DH = new DataHandler(new Project1DBContext(optionsBuilder.Options));

                //END SQL STUFF
                //Should now be able to access database records by using RH instead of DH
            }
            else if (importFromXML)
            {
                //Run deserialization code
                Task<DataHandler> desListTask = DeserializeFromFileAsync(serializationFilepath);
                DataHandler result = null;
                try
                {
                    result = desListTask.Result; // synchronously sits around until the result is ready
                }
                catch (AggregateException ex)
                {
                    Console.WriteLine($"Serialization file '{serializationFilepath}' not found.  Full error: {ex}");
                }
                if (result != null)
                {
                    DH = result;
                }
                else
                {
                    //Throw new Exception("Error deserializing object.  Result == null");
                    InitializeDummyData();
                }
            }
            

            if (! (importFromXML || useSQL))
            {
                InitializeDummyData();
            }
        }


        private static void InitializeDummyData()
        {
            DH = new DataHandler();
            DH.ToString();  // just to check DH initialization right away
                            //Adding in generic data for testing
                            //Default fake data used for testing before actual data is added from 
            DH.Locations.Add(new Location("Placeholder"));
            DH.Locations.Add(new Location("Alternate Placeholder"));
            DH.Users.Add(new User("test", "a", "b", "a@a.com", "1234567980", "Placeholder"));
            DH.ingDir.AddIngredient(new Ingredient("classic crust", 1, "crust"));
            DH.ingDir.AddIngredient(new Ingredient("thin crust", 1, "crust"));
            DH.ingDir.AddIngredient(new Ingredient("classic sauce", 1, "sauce"));
            DH.ingDir.AddIngredient(new Ingredient("garlic white sauce", 1, "sauce"));
            DH.ingDir.AddIngredient(new Ingredient("cheese", 1, "topping"));
            DH.ingDir.AddIngredient(new Ingredient("pepperoni", 1, "topping"));
            DH.ingDir.AddIngredient(new Ingredient("sausage", 1, "topping"));
            DH.SPM.AddNewSize("small", 5, .5m, 1);
            DH.SPM.AddNewSize("medium", 7.5m, .75m, 2);
            DH.SPM.AddNewSize("large", 10m, 1m, 3);
            DH.SPM.AddNewSize("party-sized", 40m, 4m, 4);
            DH.Locations[0].AddBulkStock(new List<Ingredient>
                {   new Ingredient("classic crust", 20, "crust"),
                    new Ingredient("classic sauce", 20, "sauce"),
                    new Ingredient("cheese", 100, "topping"),
                    new Ingredient("sausage", 50, "topping")
                });
        }
        private async static Task<DataHandler> DeserializeFromFileAsync(string fileName)
        {
            var serializer = new XmlSerializer(typeof(DataHandler));

            using (var memoryStream = new MemoryStream())
            {
                using (var fileStream = new FileStream(fileName, FileMode.Open))
                {
                    await fileStream.CopyToAsync(memoryStream);
                }
                memoryStream.Position = 0; // reset "cursor" of stream to beginning
                return (DataHandler)serializer.Deserialize(memoryStream);
            }
        }

        public static void SerializeToFile()
        {
            var serializer = new XmlSerializer(typeof(DataHandler));
            FileStream fileStream = null;

            try
            {
                fileStream = new FileStream(serializationFilepath, FileMode.Create);
                serializer.Serialize(fileStream, DH);
            }
            catch (PathTooLongException ex)
            {
                Console.WriteLine($"Path {serializationFilepath} is too long. {ex.Message}");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Other file io error exception: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Other general exception: {ex.Message}");
                throw; // re-throws the same exception
            }
            finally
            {
                if (fileStream != null)
                {
                    fileStream.Dispose();
                }
            }
        }

        //overload for user OrderHistory and location OrderHistory
        public static List<Order> CreateSortedOrderList(List<int> OrderIds, int orderingType)
        {
            //ordering Types:
            // 1= newst first
            // 2= oldest first
            // 3= cheapest first
            // 4= priciest first

            List<Order> sortedOrders = new List<Order>();

            foreach (var o in OrderIds)
                sortedOrders.Add(GetOrderByID(o));

            switch (orderingType)
            {
                case 1:
                    sortedOrders = (sortedOrders.OrderByDescending(a=> a.Timestamp)).ToList();
                    break;
                case 2:
                    sortedOrders = (sortedOrders.OrderBy(a => a.Timestamp)).ToList();
                    break;
                case 3:
                    sortedOrders = (sortedOrders.OrderBy(a => a.Price)).ToList();
                    break;
                case 4:
                    sortedOrders = (sortedOrders.OrderByDescending(a => a.Price)).ToList();
                    break;
            }

            return sortedOrders;
        }

        //overload for all orders
        public static List<Order> CreateSortedOrderList(int orderingType)
        {
            //ordering Types:
            // 1= newst first
            // 2= oldest first
            // 3= cheapest first
            // 4= priciest first

            List<Order> sortedOrders = new List<Order>();
            foreach (var o in DH.Orders)
                sortedOrders.Add(o);

            switch (orderingType)
            {
                case 1:
                    sortedOrders = (sortedOrders.OrderByDescending(a => a.Timestamp)).ToList();
                    break;
                case 2:
                    sortedOrders = (sortedOrders.OrderBy(a => a.Timestamp)).ToList();
                    break;
                case 3:
                    sortedOrders = (sortedOrders.OrderBy(a => a.Price)).ToList();
                    break;
                case 4:
                    sortedOrders = (sortedOrders.OrderByDescending(a => a.Price)).ToList();
                    break;
            }
            return sortedOrders;
        }


        //LINQ based methods used to aid in replacing dictionares with lists
        //get element with specified key
        public static User GetUserByUsername(string username)
        {
            return DH.Users.First(u => u.Username.Equals(username));
        }

        public static Location GetLocationByName(string name)
        {
            return DH.Locations.First(l => l.Name.Equals(name));
        }

        public static Order GetOrderByID(int id)
        {
            return DH.Orders.First(o => o.Id ==id);
        }

        //check if list contains specified key
        public static bool UsersContainsUsername(string username)
        {
            return DH.Users.Any(t => t.Username.Equals(username));
        }

        public static bool LocationsContainsName(string name)
        {
            return DH.Locations.Any(t => t.Name.Equals(name));
        }

        public static bool OrdersContainsID(int id)
        {
            return DH.Orders.Any(t => t.Id==id);
        }

    }
}
