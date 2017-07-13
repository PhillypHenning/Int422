using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Assignment8._2.Startup))]
namespace Assignment8._2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
