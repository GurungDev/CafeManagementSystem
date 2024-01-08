using System.Text.Json;

using static cww.Resources.common.Common;
namespace cww.Components.Pages.Order
{
  
    public class OrderCoffeeAddInsEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid OrderedCoffeeId { get; set; }

        public Guid AddInsId { get; set; }


    }

    public class OrderCoffeeAddInsController
    {

        public static void CreateOrderCoffeeAddIns(Guid orderCoffeeAddInsId, Guid addInsId)
        {
            List<OrderCoffeeAddInsEntity> orderCoffeeAddIns = LoadOrderCoffeeAddIns();

            orderCoffeeAddIns.Add(new OrderCoffeeAddInsEntity { OrderedCoffeeId = orderCoffeeAddInsId, AddInsId = addInsId });
            SaveOrderCoffeeAddIns(orderCoffeeAddIns);
        }


        public static List<OrderCoffeeAddInsEntity> LoadOrderCoffeeAddIns()
        {
            try
            {
                if (File.Exists(OrderCoffeeAddInsFilePath))
                {
                    var json = File.ReadAllText(OrderCoffeeAddInsFilePath);
                    return JsonSerializer.Deserialize<List<OrderCoffeeAddInsEntity>>(json) ?? new List<OrderCoffeeAddInsEntity>();
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error loading order Coffee data: {ex.Message}");
            }

            return new List<OrderCoffeeAddInsEntity>();
        }

        private static void SaveOrderCoffeeAddIns(List<OrderCoffeeAddInsEntity> orderCoffeeAddIns)
        {
            var json = JsonSerializer.Serialize(orderCoffeeAddIns, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(OrderCoffeeAddInsFilePath, json);
        }
    }
}
