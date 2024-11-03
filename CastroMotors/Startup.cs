using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CastroMotors.Startup))]
namespace CastroMotors
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
