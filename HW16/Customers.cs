using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW16
{
    public class Customer
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int Age { get; set; }


        //public IEnumerable<Order> Order { get; set; }
        //public IEnumerable<Product> Product { get; set; }

        //public Customer(int id, string firstName, string lastName, int age)
        //{
        //    Id = id;
        //    FirstName = firstName;
        //    LastName = lastName;
        //    Age = age;
        //}
    }
}
