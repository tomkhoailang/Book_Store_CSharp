using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(testIdentity2.Startup))]
namespace testIdentity2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
