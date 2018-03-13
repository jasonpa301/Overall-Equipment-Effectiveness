using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(oeeps.Startup))]
namespace oeeps
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
