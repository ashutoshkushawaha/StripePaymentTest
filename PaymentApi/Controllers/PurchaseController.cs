using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PaymentApi.Models;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;


namespace PaymentApi.Controllers
{
    public class PurchaseController : Controller
    {
        public PurchaseController()
        {

        }
        private ApplicationUserManager _userManager;
        ApplicationDbContext context = new ApplicationDbContext();
        public PurchaseController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        // GET: Purchase
        public async Task<ActionResult> Index()
        {
            var userId = User.Identity.GetUserId();
            ApplicationUser user = await UserManager.FindByIdAsync(userId);
           if(!string.IsNullOrEmpty(user.CustomerIdentifier))
            {
                StripeConfiguration.SetApiKey("sk_test_ILBG36E21hK6nO8C6fOpQvWs");
                var subscriptionSerive = new StripeSubscriptionService();
                StripeSubscriptionListOptions listOptions = new StripeSubscriptionListOptions();
                listOptions.CustomerId = user.CustomerIdentifier;
                IEnumerable<StripeSubscription> response = subscriptionSerive.List(listOptions);
                ViewBag.StripeKey = "pk_test_rAvojOoog9JrtM0vSmKm4r0D";
            }
            return View();
        }
        public async Task<ActionResult> Confirm(string stripeToken)
        {
            try
            {
                StripeConfiguration.SetApiKey("sk_test_ILBG36E21hK6nO8C6fOpQvWs");
                var userId = User.Identity.GetUserId();
                var planId = "plan_DtiCy8lnNADxbN";
                ApplicationUser user = await UserManager.FindByIdAsync(userId);
                if (string.IsNullOrEmpty(user.CustomerIdentifier))
                {
                    var customer = new StripeCustomerCreateOptions();
                    customer.Email = $"{user.Email}";
                    customer.Description = $"{user.Email}[{userId}]";
                    customer.PlanId = planId;

                    customer.SourceToken = stripeToken;
                    var customerService = new StripeCustomerService();
                    StripeCustomer stripeCustomer = customerService.Create(customer);
                    user.CustomerIdentifier = stripeCustomer.Id;
                    stripeCustomer.Subscriptions.Data.ForEach(
                        async s => await SaveSubscription(s, user));
                    await UserManager.UpdateAsync(user);


                }
                else
                {
                    var subscriptionService = new StripeSubscriptionService();
                    var stripeSubscription = subscriptionService.Create(user.CustomerIdentifier, planId);
                    await SaveSubscription(stripeSubscription, user);
                    await _userManager.UpdateAsync(user);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                throw ex;
            }
          
        }

        private async Task SaveSubscription(StripeSubscription subscriptionInfo, ApplicationUser user)
        {
            if(user.Subscriptions==null)
            {

                Subscription subscription = new Subscription();
                subscription.Id = Guid.NewGuid();
                subscription.CustomerId = subscriptionInfo.CustomerId;
                subscription.Start = subscriptionInfo.Start ?? DateTime.Now;
                subscription.PlanId = subscriptionInfo.StripePlan.Id;
                subscription.NickName = subscriptionInfo.StripePlan.Nickname;
                subscription.Interval = subscriptionInfo.StripePlan.Interval;
                subscription.ProductId = subscriptionInfo.StripePlan.ProductId;
                subscription.SubscriptionId = subscriptionInfo.Id;
                subscription.UserId = user.Id;
                subscription.Expiry = subscriptionInfo.EndedAt;
                context.Subscriptions.Add(subscription);
                context.SaveChanges();
            }
           

        }
    }
}