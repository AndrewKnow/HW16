using System;

namespace HW16
{
    internal class Program
    {
        static void Main()
        {
            Repository repository = new Repository();

            var order = repository.GetOrder(1);
            var orderCount = repository.GetOrderCount();
            var сustomer = repository.GetCustomer("Имя 1", "Фамилия 1");
            var сustomerCount = repository.GetCustomerCount();
            var product = repository.GetProduct("Товар 1");
            var productCount = repository.GetProductCount();

            if (order is not null) Console.WriteLine($"Заказ: {order.Id}, {order.CustomerId}, {order.ProductId}, {order.Quantity}");
            Console.WriteLine($"Всего заказов: {orderCount}");
            if (сustomer is not null) Console.WriteLine($"Покупатель: {сustomer.Id}, {сustomer.FirstName}, {сustomer.LastName}, {сustomer.Age}");
            Console.WriteLine($"Всего покупателей: {сustomerCount}");
            if (product is not null) Console.WriteLine($"Товар: {product.Id}, {product.Name}, {product.Description}, {product.Stockquantity}, {product.Price}");
            Console.WriteLine($"Всего товаров: {productCount}");

            var result = repository.QuaryFromHW15(1, 30);

            foreach (var res in result)
            {
                Console.WriteLine(res.Id + " " + res.CustomerId);

                foreach (var cus in res.Customers)
                {
                    Console.WriteLine("\t Возраст: {0} \t  Имя: {1} \t  Имя: {2}", cus.Age, cus.FirstName, cus.LastName);
                }
            }



        }
    }
}
