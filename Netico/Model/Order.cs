using System;
using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class Order
    {
        public int ID { get; set; }

        public Guid CustomerID { get; set; }

        public int ProductID { get; set; }

        public string Amount { get; set; }

        public DateTime OrderDate { get; set; }

        public string ProductName { get; set; }

        public string CategoryName { get; set; }

        public string CustomerName { get; set; }
    }
}
