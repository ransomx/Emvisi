using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Emvisi.Startup))]
namespace Emvisi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
