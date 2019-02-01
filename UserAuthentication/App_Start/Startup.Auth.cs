using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.OAuth;
using Owin;
using UserAuthentication.Providers;
using UserAuthentication.Models;
using Microsoft.Owin.Security.Facebook;
using UserAuthentication.Facebook;

namespace UserAuthentication
{
    public partial class Startup
    {
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        public static string PublicClientId { get; private set; }

        public void ConfigureAuth(IAppBuilder app)
        {
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            OAuthAuthorizationServerOptions option = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/Token"),
                Provider = new ApplicationOAuthProvider(),
                AuthorizeEndpointPath = new PathString("/api/Account/ExternalLogin"),
                AccessTokenExpireTimeSpan = TimeSpan.FromSeconds(50),
                // In production mode set AllowInsecureHttp = false
                AllowInsecureHttp = true
            };

            app.UseOAuthAuthorizationServer(option);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }

        // For more information on configuring authentication, please visit https://go.microsoft.com/fwlink/?LinkId=301864
        //public void ConfigureAuth(IAppBuilder app)
        //{
        //    app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
        //    // Configure the db context and user manager to use a single instance per request
        //    app.CreatePerOwinContext(ApplicationDbContext.Create);
        //    app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);

        //    // Enable the application to use a cookie to store information for the signed in user
        //    // and to use a cookie to temporarily store information about a user logging in with a third party login provider
        //    app.UseCookieAuthentication(new CookieAuthenticationOptions());
        //    app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

        //    // Configure the application for OAuth based flow
        //    PublicClientId = "self";
        //    OAuthOptions = new OAuthAuthorizationServerOptions
        //    {
        //        TokenEndpointPath = new PathString("/Token"),
        //        Provider = new ApplicationOAuthProvider(PublicClientId),
        //        AuthorizeEndpointPath = new PathString("/api/Account/ExternalLogin"),
        //        AccessTokenExpireTimeSpan = TimeSpan.FromSeconds(50),
        //        // In production mode set AllowInsecureHttp = false
        //        AllowInsecureHttp = true
        //    };

        //    // Enable the application to use bearer tokens to authenticate users
        //    app.UseOAuthBearerTokens(OAuthOptions);

        //    // Uncomment the following lines to enable logging in with third party login providers
        //    //app.UseMicrosoftAccountAuthentication(
        //    //    clientId: "",
        //    //    clientSecret: "");

        //    //app.UseTwitterAuthentication(
        //    //    consumerKey: "",
        //    //    consumerSecret: "");

        //    //app.UseFacebookAuthentication(
        //    //    appId: "",
        //    //    appSecret: "");

        //    var facebookOptions = new FacebookAuthenticationOptions()
        //    {
        //        AppId = "516458582166123",
        //        AppSecret = "d5786a88a672d38b8d86b8b20f6b7d2c",
        //        BackchannelHttpHandler  =new FacebookBackChannelHandler(),
        //        UserInformationEndpoint="https://graph.facebook.com/v2.4/me?fields=id,email"
        //    };
        //    facebookOptions.Scope.Add("email");
        //    app.UseFacebookAuthentication(facebookOptions);
        //    app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
        //    {
        //        ClientId = "519292877264-ot29svrn2p7rdqjo082algcaa5vrl41g.apps.googleusercontent.com",
        //        ClientSecret = "ZTheiBAg1ZPnKtg6P4gzr2LI"
        //    });
        //}
    }
}
