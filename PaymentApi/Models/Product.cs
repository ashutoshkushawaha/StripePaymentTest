using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PaymentApi.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string StripeProductId { get; set; }
        public string PlanId { get; set; }
    }
}