using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace PlantShop.Infrastructure.Extensions
{
    public static class SessionExtensions1
    {   
        public static void SetObject(this ISession session, string key, object value)
        {
            //session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetObject<T>(this ISession session, string key)
        {
            //var value = session.GetString(key); 

            return /*value == null ? default(T) : */JsonConvert.DeserializeObject<T>("");
        }
    }
}
