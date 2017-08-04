using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Assignment_9.Startup))]
namespace Assignment_9
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
