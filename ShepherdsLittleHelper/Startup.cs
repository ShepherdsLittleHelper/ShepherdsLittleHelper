using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ShepherdsLittleHelper.Startup))]
namespace ShepherdsLittleHelper
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
