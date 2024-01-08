 
using System.Text.Json;

using static cww.Resources.common.Common;
using cww.Components.Pages.Coffee;
namespace cww.Components.Pages.Order
{
    public class OrderCoffeeEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid? OrderId { get; set; }

        public Guid CoffeeId { get; set; }


    }

    public class OrderCoffeeController
    {

        public static OrderCoffeeEntity CreateOrderCoffee(Guid coffeeId, Guid? orderId = null)
        {
            List<OrderCoffeeEntity> orderCoffee = LoadOrderCoffee();
            OrderCoffeeEntity newCoffeeOrder = new OrderCoffeeEntity { OrderId = orderId, CoffeeId = coffeeId };
            orderCoffee.Add(newCoffeeOrder);
            SaveorderCoffee(orderCoffee);
            return newCoffeeOrder;
        }

        public static void FinalizeCoffeeOrder(List<OrderCoffeeEntity> coffeeOrders, Guid orderId)
        {
            List<OrderCoffeeEntity> orderCoffee = LoadOrderCoffee();
            foreach (OrderCoffeeEntity orderCoffeeItem in orderCoffee)
            {
              
                OrderCoffeeEntity matchingCoffeeOrder = coffeeOrders.FirstOrDefault(co => co.Id == orderCoffeeItem.Id);

                if (matchingCoffeeOrder != null)
                {
                  
                    orderCoffeeItem.OrderId = orderId;
   
                }
            }
 
            SaveorderCoffee(orderCoffee);
          
        }


        public static List<Tuple<CoffeeEntity, int>> FindCoffeeRanking()
        {
            List < OrderCoffeeEntity > orderCoffee = LoadOrderCoffee();
            List<CoffeeEntity> coffeeList = CoffeeController.Loadcoffees();
            var coffeeOrderList = orderCoffee
                 .Where(order => order.CoffeeId != Guid.Empty)
                 .GroupBy(order => order.CoffeeId)
                 .OrderByDescending(group => group.Count())
                  .ToList();
            var rankingWithCoffeeInfo = coffeeOrderList
                .Select(group => Tuple.Create(
                    coffeeList.FirstOrDefault(coffee => coffee.Id == group.Key),
                    group.Count()))
                    .ToList();
            return rankingWithCoffeeInfo;


        }

        public static List<OrderCoffeeEntity> LoadOrderCoffee()
        {
            try
            {
            
                if (File.Exists(OrderCoffeeFilePath))
                {
                    var json = File.ReadAllText(OrderCoffeeFilePath);
                    return JsonSerializer.Deserialize<List<OrderCoffeeEntity>>(json) ?? new List<OrderCoffeeEntity>();
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error loading order Coffee data: {ex.Message}");
            }

            return new List<OrderCoffeeEntity>();
        }

        private static void SaveorderCoffee(List<OrderCoffeeEntity> orderCoffee)
        {
            var json = JsonSerializer.Serialize(orderCoffee, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(OrderCoffeeFilePath, json);
        }
    }
}
