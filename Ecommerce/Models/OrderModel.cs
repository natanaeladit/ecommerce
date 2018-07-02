using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ecommerce.Models
{
    public class OrderModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public string Address { get; set; }
        public string Address2 { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }

        public string PaymentMethod { get; set; }
        public string CCName { get; set; }
        public string CCNumber { get; set; }
        public string CCExpiration { get; set; }
        public string CCCCV { get; set; }
    }
}