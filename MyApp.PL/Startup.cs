using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyApp.PL.Startup))]
namespace MyApp.PL
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
