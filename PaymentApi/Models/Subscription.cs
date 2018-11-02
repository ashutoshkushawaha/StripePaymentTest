using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PaymentApi.Models
{
    public class Subscription
    {
        //public Guid SubscriptionId { get; set; }
        //public DateTime PurchaseDate { get; set; }
        //public DateTime Expirty { get; set; }
        //public int AmountInCents { get; set; }
        //public string UserId { get; set; }
        //public string PaymentSubscriptionId { get; set; }
        //public Guid ApiKey { get; set; }

        public Guid Id { get; set; }
        public string CustomerId { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? Expiry { get; set; }

        public string PlanId { get; set; }
        public string NickName { get; set; }
        public string Interval { get; set; }
        public int AmountInCents { get; set; }
        public string ProductId { get; set; }
        public string SubscriptionId { get; set; }
        public string UserId { get; set; }
    }
}