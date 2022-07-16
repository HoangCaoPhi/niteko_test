using System;

namespace Model
{
    public class Customer
    {
        public Guid CustomerID { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}
