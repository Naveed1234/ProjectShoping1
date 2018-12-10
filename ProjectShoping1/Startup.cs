using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProjectShoping1.Startup))]
namespace ProjectShoping1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
