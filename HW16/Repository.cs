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
        public IEnumerable<Order> QuaryFromHW15(int idc, int age)
        {

            //var query = @"SELECT o.customerid as custometId, c.firstname, c.lastname " +
            //    @", o.productid as productId, o.quantity, p.price " +
            //    "FROM Orders o LEFT JOIN Customers c ON c.id = o.customerid LEFT JOIN Products p ON p.id = o.productid " +
            //    "WHERE p.id = @id AND c.age > @age ;";

            //var query = @"SELECT o.id as Id, o.customerid as Idc, c.firstname, c.lastname, o.productid as Idb, o.quantity, p.price " +
            //    "FROM Orders o LEFT JOIN Customers c ON o.customerid = c.id   LEFT JOIN Products p ON p.id = o.productid " +
            //    "WHERE p.id = @id AND c.age > @age ;";

            var query = @"SELECT c.id Id, c.id customerid,  c.firstname, c.lastname, o.productid IdP, o.quantity, p.price " +
                "FROM Orders o INNER JOIN Customers c ON o.customerid = c.id  INNER JOIN Products p ON p.id = o.productid " +
                "WHERE p.id = @id AND c.age > @age ;";
            //var query = @"SELECT c.id , o.customerid as Id, c.firstname, c.lastname, o.productid as ProductId, o.quantity, p.price " +
            //    "FROM Customers c LEFT JOIN Orders o ON o.customerid = c.id JOIN Products p ON p.id = o.productid " +
            //    "WHERE p.id = @id AND c.age > @age ;";

            var connString = DBConnection.ConnectionString();
            using (var connection = new NpgsqlConnection(connString))
            {
                //var orderRes = connection.Query<Order, Customer, Product, Order>(query,
                //    (o, c, p) =>
                //    {
                //        o.Products = new List<Product>();
                //        o.Customers = new List<Customer>();


                //        if (c != null)
                //        {
                //            o.Customers.ToList().Add(c);
                //        }

                //        if (p != null)
                //        {
                //            o.Products.ToList().Add(p);
                //        }

                //        return o;
                //    }, new { @id = idc, @age = age },
                //    splitOn: "customerid,IdP");

                var lookup = new Dictionary<int, Order>();
                connection.Query<Order, Customer, Product, Order>(query, (o, c, p) => {
                        Order order;
                        if (!lookup.TryGetValue(o.Id, out order))
                        {
                            lookup.Add(o.Id, order = o);
                        }
                        if (order.Products == null)
                            order.Products = new List<Product>();

                        if (order.Customers == null)
                            order.Customers = new List<Customer>();

                        order.Products.Add(p);
                        order.Customers.Add(c);
                    return order;
                    }, new { @id = idc, @age = age }, splitOn: "customerid,IdP"
                 ).AsQueryable();
                var resultList = lookup.Values;

                connection.Close();
                return resultList;//orderRes;

                
            }
        }
    }
}

