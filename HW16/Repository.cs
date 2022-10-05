using Npgsql;
using Dapper;
using System.Text;

namespace HW16
{
    public class Repository
    {
        public Order GetOrder(int productId)
        {
            var query = @"select * from orders where id = @id;";
            var connString = DBConnection.ConnectionString();
            using var connection = new NpgsqlConnection(connString);
            var s = connection.QueryFirstOrDefault<Order>(query, new { productId });
            connection.Close();
            return s;
        }
        public int GetOrderCount()
        {
            var query = @"select count(*) from orders;";
            var connString = DBConnection.ConnectionString();
            using var connection = new NpgsqlConnection(connString);
            var s = connection.ExecuteScalar<int>(query);
            connection.Close();
            return s;
        }
        public Customer GetCustomer(string firstName, string lastName)
        {
            var query = @"select * from customers where firstname = @firstName and lastname = @lastName;";
            var connString = DBConnection.ConnectionString();
            using var connection = new NpgsqlConnection(connString);
            var s = connection.QueryFirstOrDefault<Customer>(query, new
            {
                @firstname = firstName,
                @lastname = lastName
            });
            connection.Close();
            return s;
        }
        public int GetCustomerCount()
        {
            var query = @"select count(*) from customers;";
            var connString = DBConnection.ConnectionString();
            using var connection = new NpgsqlConnection(connString);
            var s = connection.ExecuteScalar<int>(query);
            connection.Close();
            return s;
        }
        public Product GetProduct(string name)
        {
            var query = @"select * from products where name LIKE CONCAT('%',@name,'%');";
            var connString = DBConnection.ConnectionString();
            using var connection = new NpgsqlConnection(connString);
            var s = connection.QueryFirstOrDefault<Product>(query, new { name });
            connection.Close();
            return s;
        }
        public int GetProductCount()
        {
            var query = @"select count(*) from products;";
            var connString = DBConnection.ConnectionString();
            using var connection = new NpgsqlConnection(connString);
            var s = connection.ExecuteScalar<int>(query);
            connection.Close();
            return s;
        }
        public IEnumerable<Order> QuaryFromHW15(int id, int age)
        {

            var query = @"SELECT o.*, o.customerid as Id, c.firstname, c.lastname, o.productid as Id, o.quantity, p.price, p.Name " +
                "FROM Orders o JOIN Products p ON p.id = o.productid JOIN Customers c ON c.id = o.customerid " +
                "WHERE p.id = @id AND c.age > @age ;";

            var connString = DBConnection.ConnectionString();
            using (var connection = new NpgsqlConnection(connString))
            {
                var lookup = new Dictionary<int, Order>();
                var lookup2 = new Dictionary<int, Customer>();
                _ = connection.Query<Order, Customer, Product, Order >(query, (o, c, p) =>
                {
                    Order order;
                    if (!lookup.TryGetValue(o.Id, out order))
                    {
                        lookup.Add(o.Id, order = o);
                    }

                    Customer customer;
                    if (order.Customers == null)
                        order.Customers = new List<Customer>();
                    if (!lookup2.TryGetValue(c.Id, out customer))
                    {
                        lookup2.Add(c.Id, customer = c);
                        order.Customers.Add(customer);
                    }

                    Product product;

                    if (order.Products == null)
                        order.Products = new List<Product>();

                    order.Products.Add(p);

                    return order;
                }, new { @id = id, @age = age }, splitOn: "Id"
                 ).AsQueryable();
                var resultList = lookup.Values;

                connection.Close();
                return resultList;

                
            }
        }
    }
}

