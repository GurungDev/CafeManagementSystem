using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static cww.Resources.common.Common;
namespace cww.Components.Pages.Topings
{

    public class ToppingEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }


    }

    public class ToppingController
    {

        public static void Createtopping(string Name, int Price)
        {
            List<ToppingEntity> toppings = Loadtoppings();

            toppings.Add(new ToppingEntity { Id = Guid.NewGuid(), Name = Name, Price = Price });
            Savetoppings(toppings);
        }

        public static int GettoppingNumber()
        {
            try
            {
                if (File.Exists(CoffeeTopingsFilePath))
                {
                    var json = File.ReadAllText(CoffeeTopingsFilePath);
                    return JsonSerializer.Deserialize<List<ToppingEntity>>(json)?.Count ?? 0;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading topping data: {ex.Message}");
            }
            return 0;
        }

        public static List<ToppingEntity> Loadtoppings()
        {
            try
            {
                if (File.Exists(CoffeeTopingsFilePath))
                {
                    var json = File.ReadAllText(CoffeeTopingsFilePath);
                    return JsonSerializer.Deserialize<List<ToppingEntity>>(json) ?? new List<ToppingEntity>();
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error loading topping data: {ex.Message}");
            }

            return new List<ToppingEntity>();
        }

        private static void Savetoppings(List<ToppingEntity> toppings)
        {
            var json = JsonSerializer.Serialize(toppings, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(CoffeeTopingsFilePath, json);
        }
    }
}
