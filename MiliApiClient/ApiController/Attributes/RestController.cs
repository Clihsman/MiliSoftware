using System;

namespace OpenRestClient.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class RestController : Attribute
    {
        public string Route { get; private set; }

        public RestController(string route)
        {
            Route = route;
        }
    }
}
