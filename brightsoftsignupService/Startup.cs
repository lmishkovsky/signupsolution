using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(brightsoftsignupService.Startup))]

namespace brightsoftsignupService
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
        }
    }
}