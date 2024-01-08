using System;
using System.Runtime.Caching;

namespace MiliSoftware.WebServices
{
    public class AppStaticStore
    {
        internal static MemoryCache Memory { get; set; } = MemoryCache.Default;

        public static void SetToken(string token) {
            CacheItemPolicy policy = new CacheItemPolicy() {
                AbsoluteExpiration = DateTimeOffset.Now.AddHours(12),
                Priority = CacheItemPriority.NotRemovable,
            };
            Memory.Set("token", token, policy);
        }

        internal static string GetToken()
        {
            return Memory.Get("token") as string;
        }
    }
}
