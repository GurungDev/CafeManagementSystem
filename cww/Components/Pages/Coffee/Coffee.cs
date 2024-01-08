 
using System.Text.Json;
 
using static cww.Resources.common.Common;

namespace cww.Components.Pages.Coffee
{
    public class CoffeeEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }


    }

    public class CoffeeController
    {

        public void Createcoffee(string Name, int Price)
        {
            List<CoffeeEntity> coffees = Loadcoffees();

            coffees.Add(new CoffeeEntity { Id = Guid.NewGuid(), Name = Name, Price = Price });
            Savecoffees(coffees);
        }

        public static int GetcoffeeNumber()
        {
            try
            {
                if (File.Exists(CoffeesFilePath))
                {
                    var json = File.ReadAllText(CoffeesFilePath);
                    return JsonSerializer.Deserialize<List<CoffeeEntity>>(json)?.Count ?? 0;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading coffee data: {ex.Message}");
            }
            return 0;
        }

        public static string UpdatePrice(Guid productId, int newPrice)
        {
            List<CoffeeEntity> coffees = Loadcoffees();
            CoffeeEntity productToUpdate = coffees.FirstOrDefault(p => p.Id == productId);

            if (productToUpdate != null)
            {
                productToUpdate.Price = newPrice;
                return "updated successfully";
            }
            else
            {
                return "product not found";
            }
        }


        public static List<CoffeeEntity> Loadcoffees()
        {
            try
            {
                if (File.Exists(CoffeesFilePath))
                {
                    var json = File.ReadAllText(CoffeesFilePath);
                    return JsonSerializer.Deserialize<List<CoffeeEntity>>(json) ?? new List<CoffeeEntity>();
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error loading coffee data: {ex.Message}");
            }

            return new List<CoffeeEntity>();
        }

        private static void Savecoffees(List<CoffeeEntity> coffees)
        {
            var json = JsonSerializer.Serialize(coffees, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(CoffeesFilePath, json);
        }
    }
}
