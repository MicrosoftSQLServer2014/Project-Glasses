using System;
using BLL.Services;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using Server;
using Server.Util;

[assembly: OwinStartup(typeof(Startup))]

namespace Server
{
    public class Startup
    {
        private string PublicClientId { get; set; }
        private OAuthAuthorizationServerOptions OAuthOptions { get; set; }
        public ServiceCreator ServiceCreator { get; set; }

        public void Configuration(IAppBuilder app)
        {
            ServiceCreator = new ServiceCreator();
            // Дополнительные сведения о настройке приложения см. по адресу: http://go.microsoft.com/fwlink/?LinkID=316888
            PublicClientId = "self";
            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/Account/Token"),
                Provider = new ApplicationOAuthProvider(PublicClientId, ServiceCreator.CreateUserService("DefaultConnection")),
                AuthorizeEndpointPath = new PathString("/api/Account/ExternalLogin"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(2),
                AllowInsecureHttp = true
            };
            app.UseOAuthBearerTokens(OAuthOptions);
        }
    }
}
