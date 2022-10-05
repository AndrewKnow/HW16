using System;

namespace HW16
{
    internal class Program
    {
        static void Main()
        {
            Repository repository = new Repository();

            //Определиться с ORM: Dapper(Linq2Db).
            //Ответ: Dapper

            //Выбрать какую БД использовать(из задания "Sql запросы" или "Кластерный индекс")
            //Ответ: "Кластерный индекс" Shop

            //Используя ORM выполнить простые запросы к каждой таблице,
            //выполнить параметризованные запросы к каждой таблице(без JOIN) - 2 - 3 запроса на таблицу.
            var order = repository.GetOrder(1);
            var orderCount = repository.GetOrderCount();
            var сustomer = repository.GetCustomer("Имя 1", "Фамилия 1");
            var сustomerCount = repository.GetCustomerCount();
            var product = repository.GetProduct("Товар 1");
            var productCount = repository.GetProductCount();
            //1 запрос
            if (order is not null) Console.WriteLine($"Заказ: {order.Id}," +
                $" {order.CustomerId}, {order.ProductId}, {order.Quantity}");
            //2 запрос
            Console.WriteLine($"Всего заказов: {orderCount}");
            //3 запрос
            if (сustomer is not null) Console.WriteLine($"Покупатель: {сustomer.Id}, " +
                $"{сustomer.FirstName}, {сustomer.LastName}, {сustomer.Age}");
            //4 запрос
            Console.WriteLine($"Всего покупателей: {сustomerCount}");
            //5 запрос
            if (product is not null) Console.WriteLine($"Товар: {product.Id}, " +
                $"{product.Name}, {product.Description}, {product.Stockquantity}, {product.Price}");
            //6 запрос
            Console.WriteLine($"Всего товаров: {productCount}");

            //Выполнить все запросы, из выбранного ранее задания с передачей параметров.
            //Написать запрос, который возвращает список всех пользователей старше 30 лет,
            //у которых есть заказ на продукт с ID=1.

            var result = repository.QuaryFromHW15(1, 30);

            foreach (var res in result)
            {
                foreach (var prod in res.Products)
                {
                    foreach (var cus in res.Customers)
                    {
                        if (cus.Id == res.CustomerId)
                        Console.WriteLine($"CustomerID {res.CustomerId}, FirstName {cus.FirstName}, "
                                            + $"LastName  {cus.LastName}, ProductID {res.ProductId}, " 
                                            + $"ProductQuantity { res.Quantity}, ProductPrice {prod.Price}");
                    }
                }
            }
        }
    }
}
