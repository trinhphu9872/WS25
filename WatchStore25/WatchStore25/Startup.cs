using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WatchStore25.Startup))]
namespace WatchStore25
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
