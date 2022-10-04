using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW16
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        //public List<Order> Orders { get; set; }
        public IList<Product> Products { get; set; }
        public IList<Customer> Customers { get; set; }


        //public Order(int id, int customerId, int productId, int quantity)
        //{
        //    Id = id;
        //    CustomerId = customerId;
        //    ProductId = productId;
        //    Quantity = quantity;
        //}
    }
}
