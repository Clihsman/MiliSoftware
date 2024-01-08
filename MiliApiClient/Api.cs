using MiliSoftware.WebServices;
using OpenRestClient;
using System;
using System.Net.Http;

namespace MiliSoftware
{
    public abstract class Api : RestApp
    {
        private static HttpClient HttpClient { get; set; }

        protected Api(Type type) : base(type) { }

        protected override HttpClient CreateHttpClient()
        {
            AddHeader("Authorization", string.Format("Bearer {0}", AppStaticStore.GetToken()));
            return HttpClient = HttpClient ?? new HttpClient();
        }
    }
}
