using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IHSDC.WebApp.Startup))]
namespace IHSDC.WebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
