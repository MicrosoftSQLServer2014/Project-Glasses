using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using BLL.Interfaces;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;

namespace Server.Util
{
    public class ApplicationOAuthProvider: OAuthAuthorizationServerProvider
    {
        private IUserService UserService { get; set; }
        private string PublicClientId { get; }

        public ApplicationOAuthProvider(string publicClientId, IUserService userService)
        {
            if (userService == null)
            {
                throw  new ArgumentException("userService");
            }
            UserService = userService;
            if (publicClientId == null)
            {
                throw new ArgumentException("publicClientId");
            }
            PublicClientId = publicClientId;
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var userName = await UserService.GetUserNameAsync(context);
            if (userName == null)
            {
                context.SetError("invalid_grant", "Имя пользователя или пароль указаны неправильно.");
                return;
            }
            ClaimsIdentity oAuthIdentity =
                await UserService.GenerateUserIdentityAsync(context, OAuthDefaults.AuthenticationType);

            AuthenticationProperties properties = CreateProperties(userName);
            AuthenticationTicket ticket = new AuthenticationTicket(oAuthIdentity, properties);
            context.Validated(ticket);
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string,string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }
            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            // Учетные данные владельца ресурса не содержат идентификатор клиента.
            if (context.ClientId == null)
            {
                context.Validated();
            }

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            if (context.ClientId == PublicClientId)
            {
                Uri expectedRootUri = new Uri(context.Request.Uri, "/");

                if (expectedRootUri.AbsoluteUri == context.RedirectUri)
                {
                    context.Validated();
                }
            }

            return Task.FromResult<object>(null);
        }

        private static AuthenticationProperties CreateProperties(string userName)
        {
            IDictionary<string, string> data = new Dictionary<string, string>
            {
                {"userName", userName }
            };
            return  new AuthenticationProperties(data);
        }
    }
}