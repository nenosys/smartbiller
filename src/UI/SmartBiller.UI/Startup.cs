using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SmartBiller.UI.Startup))]
namespace SmartBiller.UI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
