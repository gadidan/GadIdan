using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GadIdan.Startup))]
namespace GadIdan
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
