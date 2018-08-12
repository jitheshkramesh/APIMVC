using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Threading;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Security.Principal;
using System.Text;

namespace AngApp
{
    public static class GlobalVariables
    {
        public static HttpClient webApiClient = new HttpClient();

       static GlobalVariables()
        {
            webApiClient.BaseAddress = new Uri("http://localhost:58953/api");
            webApiClient.DefaultRequestHeaders.Clear();
            webApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            webApiClient.DefaultRequestHeaders.Add("X-APIKEY","MyRandomApiKeyValue");
            string uname = "username1";
            string pswd = "passw@rd";
            string base64Code = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{uname};{pswd}"));
            webApiClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", base64Code);
            //webApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("", "");

        }
    }
}