using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PaymentApi.Startup))]
namespace PaymentApi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            //startup is changed 
        }
    }
}
