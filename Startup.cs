using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MovieList.Startup))]
namespace MovieList
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
