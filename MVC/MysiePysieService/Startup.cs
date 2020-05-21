using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MysiePysieService.Startup))]
namespace MysiePysieService
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
