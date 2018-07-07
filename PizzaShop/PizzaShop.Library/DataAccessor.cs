using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PizzaShop.Library
{
    public static class DataAccessor
    {
        public static DataHandler DH { get; set; }
        //public static readonly string serializationFilepath = @"C:\Revature\kirk-project1\PizzaShop\data.xml";
        public static readonly string serializationFilepath = @"E:\Revature\kirk-project1\PizzaShop\data.xml";

        public static void Setup(bool importFromXML)
        {
            if (importFromXML)
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
                    Console.WriteLine($"Serialization file '{serializationFilepath}' not found.");
                }
                if (result != null)
                {
                    DH = result;
                }
                else
                {
                    throw new Exception("Error deserializing object.  Result == null");
                }
            }
            else
            {
                DH = new DataHandler();
                DH.ToString();  // just to check DH initialization right away
                //Adding in generic data for testing
                DH.Locations[0].AddBulkStock(new List<IIngredient>
                {   new Crust("classic crust", 20),
                    new Sauce("classic sauce", 20),
                    new Topping("cheese", 100),
                    new Topping("sausage", 50)
                });
            }
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
    }
}
