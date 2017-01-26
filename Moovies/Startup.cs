using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Moovies.Startup))]
namespace Moovies
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
