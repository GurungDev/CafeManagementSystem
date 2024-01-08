
using System.Text.Json;

using static cww.Resources.common.Common;

namespace cww.Components.Pages.Order
{
    public class OrderEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public int PhoneNumber { get; set; }

        public int TotalAmount { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Today;
        
    }
    public class OrderController
    {

        public static OrderEntity CreateOrder(int phoneNumber, int totalAmount)
        {
            List<OrderEntity> order = LoadOrder();

            OrderEntity newOrder = new OrderEntity { PhoneNumber = phoneNumber, TotalAmount = totalAmount };
            order.Add(newOrder);
            SaveOrder(order);
            return newOrder;
        }


        public static List<OrderEntity> LoadOrder()
        {
            try
            {
                if (File.Exists(OrderFilePath))
                {
                    var json = File.ReadAllText(OrderFilePath);
                    return JsonSerializer.Deserialize<List<OrderEntity>>(json) ?? new List<OrderEntity>();
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error loading order data: {ex.Message}");
            }

            return new List<OrderEntity>();
        }

        private static void SaveOrder(List<OrderEntity> order)
        {
            var json = JsonSerializer.Serialize(order, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(OrderFilePath, json);
        }
    }
}
