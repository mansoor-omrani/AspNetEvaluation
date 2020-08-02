using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AspNetEvaluation.Mvc.Startup))]
namespace AspNetEvaluation.Mvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
