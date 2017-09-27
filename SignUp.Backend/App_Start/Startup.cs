using System;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(SignUp.Backend.App_Start.Startup))]
namespace SignUp.Backend.App_Start
{
    /// <summary>
    /// Startup.
    /// </summary>
    public partial class Startup
    {
        /// <summary>
        /// Configuration the specified app.
        /// </summary>
        /// <returns>The configuration.</returns>
        /// <param name="app">App.</param>
		public void Configuration(IAppBuilder app)
		{
			ConfigureMobileApp(app);
		}
    }
}
