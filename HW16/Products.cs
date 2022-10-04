using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW16
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int Stockquantity { get; set; }
        public int Price { get; set; }

        //public Product(int id, string name, string description, int stockquantity, int price)
        //{
        //    Id = id;
        //    Name = name;
        //    Description = description;
        //    Stockquantity = stockquantity;
        //    Price = price;
        //}
    }
}
