using MiliSoftware.Modelos;
using Newtonsoft.Json;
using OpenRestClient.Attributes;
using OpenRestClient.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OpenRestClient
{
    public abstract class RestApp
    {
        private string Host { get; set; }
        private string RequestString { get; set; }
        public HttpStatusCode StatusCode { get; private set; }
        
        private Dictionary<string, Dictionary<int, MethodArgs>> Methods { get; set; }
        private Dictionary<string, string> Headers { get; set; }

        protected RestApp(string host, Type type) {
            Host = host;
            Headers = new Dictionary<string, string>();
            Methods = new Dictionary<string, Dictionary<int, MethodArgs>>();
            AddUrls(this, GetRoute(type), type);
        }

        protected RestApp(Type type) {
            Host = GetHost(type);
            Headers = new Dictionary<string, string>();
            Methods = new Dictionary<string, Dictionary<int, MethodArgs>>();
            AddUrls(this, GetRoute(type), type);
        }

        protected async Task<string> CallString(string method, params object[] args)
        {
            (string methodType, string url) = GetUrl(method, args);           
            return await ReadAsStringAsync(methodType, url, args);
        }

        protected async Task<T[]> CallArray<T>(string method, params object[] args)
        {
            (string methodType, string url) = GetUrl(method, args);
            if (methodType != "get" && methodType != "post") throw new NotSupportedException(string.Format("{0}: El metodo {1} no soporta esta implementación", method, methodType));
            string json = await ReadAsStringAsync(methodType, url, args);
            if (json != null && (StatusCode == (HttpStatusCode)200 || StatusCode == (HttpStatusCode)201)) return JsonConvert.DeserializeObject<T[]>(json);
            return null;
        }

        protected async Task<T> Call<T>(string method, params object[] args)
        {
            (string methodType, string url) = GetUrl(method, args);
            string json = await ReadAsStringAsync(methodType, url, args);

            if (json != null)
            {
                if (StatusCode == (HttpStatusCode)200 || StatusCode == (HttpStatusCode)201 || StatusCode == HttpStatusCode.BadRequest)
                    return JsonConvert.DeserializeObject<T>(json);
            }

            return default;
        }

        protected async Task<RestResponse<T, R>> Call<T, R>(string method,params object[] args) {
            (string methodType, string url) = GetUrl(method, args);
            string json = await ReadAsStringAsync(methodType, url, args);
            if (json != null)
            {
                Console.WriteLine(json);
                Console.WriteLine(StatusCode);
                RestResponse<T, R> response = new RestResponse<T, R>();
                if (StatusCode == (HttpStatusCode)200 || StatusCode == (HttpStatusCode)201)
                {
  
                    response.Value0 = JsonConvert.DeserializeObject<T>(json);
                    response.Value1 = default;
                    return response;
                }

                response.Value0 = default;
                response.Value1 = JsonConvert.DeserializeObject<R>(json);
                return response;
            }

            return default;
        }

        protected async Task<RestResponse<T[], R>> CallArray<T, R>(string method,params object[] args)
        {
            (string methodType, string url) = GetUrl(method, args);
            string json = await ReadAsStringAsync(methodType, url, args);
            if (json != null)
            {
                RestResponse<T[], R> response = new RestResponse<T[], R>();
                if (StatusCode == (HttpStatusCode)200 || StatusCode == (HttpStatusCode)201)
                {
                    response.Value0 = JsonConvert.DeserializeObject<T[]>(json);
                    response.Value1 = default;
                    return response;
                }

                Console.WriteLine(json);

                response.Value0 = default;
                response.Value1 = JsonConvert.DeserializeObject<R>(json);
                return response;
            }

            return default;
        }

        protected async Task<object> CallDynamic(string method, params object[] args)
        {
            (string methodType, string url) = GetUrl(method, args);
            string json = await ReadAsStringAsync(methodType, url, args);
            if(json != null) return JsonConvert.DeserializeObject(json);
            return null;
        }

        private async Task<string> ReadAsStringAsync(string methodType, string url, object[] args) {
            try
            {
                HttpClient client = CreateHttpClient();
                foreach (var header in Headers)
                {
                    client.DefaultRequestHeaders.Remove(header.Key);
                    client.DefaultRequestHeaders.Add(header.Key, header.Value);
                }

                HttpResponseMessage response = null;

                if (methodType == "get")
                {
                    response = await client.GetAsync(url);
                }
                if (methodType == "post")
                {
                    string json = JsonConvert.SerializeObject(args[0]);
                    var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                    response = await client.PostAsync(url, httpContent);
                }

                if (methodType == "put")
                {
                    string json = JsonConvert.SerializeObject(args[0]);
                    var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                    response = await client.PutAsync(url, httpContent);
                }

                if (methodType == "delete")
                {
                    response = await client.DeleteAsync(url);
                }

                if (response != null)
                {
                    RequestString = await response.Content.ReadAsStringAsync();
                    StatusCode = response.StatusCode;

                    if (StatusCode == HttpStatusCode.InternalServerError)
                    {
                        File.WriteAllText("error.html", RequestString);
                        Process.Start("error.html");
                    }

                    if (StatusCode == HttpStatusCode.BadRequest || StatusCode == HttpStatusCode.NotFound || StatusCode == HttpStatusCode.InternalServerError || StatusCode == HttpStatusCode.Conflict)
                        return null;

                    return RequestString;
                }
            }
             catch (HttpRequestException ex)
            {
                if (ex.InnerException.InnerException is SocketException)
                {
                    RequestString = null;
                    StatusCode = HttpStatusCode.ServiceUnavailable;
                }
            }
            return null;
        }

        public void AddHeader(string name, string value) { 
            if(Headers.ContainsKey(name))
                Headers[name] = value;
            else
              Headers.Add(name, value);
        }

        private (string, string) GetUrl(string method, params object[] args) {
            method += args.Length;   
            MethodArgs methodArgs = Methods[method][args.Length];
            args = GetArgs(methodArgs,  args);
            string url = string.Format("{0}/{1}", Host, methodArgs.Url);
            return (GetMethodName(methodArgs.MethodType), string.Format(url, args));
        }

        private static object[] GetArgs(MethodArgs method, object[] args) {
            for (int i = 0; i < method.InFields.Count; i++) {
          
                if (method.InFields[i] is InJoin) {
                    if (!args[i].GetType().IsArray) throw new ArgumentException();

                    InJoin inJoin = method.InFields[i] as InJoin;
                    Array elements = args[i] as Array;
                    List<object> values = new List<object>();
                    for (int index = 0; index < elements?.Length; index++)
                        values.Add(elements?.GetValue(index));
                    args[i] = string.Join(inJoin?.Separator, values);
                }
            }

            return args;
        }

        private static string GetHost(Type type) {
           return Assembly.GetAssembly(type)?.GetCustomAttribute<RestHost>()?.Host;
        }

        private static string GetMethodName(MethodType methodType)
        {
            string methodName = methodType == MethodType.GET ? "get" : "";
            methodName = methodType == MethodType.POST ? "post" : methodName;
            methodName = methodType == MethodType.PUT ? "put" : methodName;
            methodName = methodType == MethodType.DELETE ? "delete" : methodName;
            methodName = methodType == MethodType.OPTIONS ? "options" : methodName;
            return methodName;
        }

        private static void AddUrls(RestApp app, string route, Type type) {
            foreach (var method in type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly))
            {
                string methodName = method.Name;
                MethodArgs url = new MethodArgs();
                ParameterInfo[] parameters = method.GetParameters();
                Dictionary<int, MethodArgs> urls = new Dictionary<int, MethodArgs>();
                url.InFields = new List<InField>();
                RestMethod restMethod = method.GetCustomAttribute<RestMethod>();
                url.Url = !string.IsNullOrEmpty(restMethod?.Route) ? string.Format("{0}/{1}", route, restMethod.Route) : route;
               
                int index = restMethod?.Method == MethodType.POST || restMethod?.Method == MethodType.PUT ? 1 : 0;

                if (restMethod != null)
                {
                    url.MethodType = restMethod.Method;
                }

                for (int i = index; i < parameters?.Length; i++)
                {
                    url.Url = string.Format("{0}/{1}", url.Url, "{" + i + "}");
                    InField inField = parameters[i].GetCustomAttribute<InField>();
                    if (inField != null) url.InFields.Add(inField);
                }

                app?.Methods.Add(methodName + parameters?.Length, urls);
                urls?.Add(parameters.Length, url);
            }
        }

        private static string GetRoute(Type type) { 
            return type.GetCustomAttribute<RestController>()?.Route;
        }

        protected virtual HttpClient CreateHttpClient() {        
            return new HttpClient();
        }

        public string GetRequestString() {
            return RequestString;
        }

        private struct MethodArgs
        {
            public string Url { get; set; }
            public MethodType MethodType { get; set; }

            public List<InField> InFields { get; set; }
        }
    }
}
