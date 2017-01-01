using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CP.Startup))]
namespace CP
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
