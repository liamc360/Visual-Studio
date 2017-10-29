using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Website.Startup))]
namespace Website
{
    //add new fields https://blog.falafel.com/customize-mvc-5-application-users-using-asp-net-identity-2-0/
    //search for "Age" in Website
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
