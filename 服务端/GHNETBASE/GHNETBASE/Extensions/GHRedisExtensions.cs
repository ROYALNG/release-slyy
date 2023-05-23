using GHCore.Serialization;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GHDatabase.Redis.Extensions
{
    public static class GHRedisExtensions
    {
        public static T Get<T>(this IDatabase cache, string key)
        {
            return BinaryFormatter.Deserialize<T>(cache.StringGet(key));
        }

        public static object Get(this IDatabase cache, string key)
        {
            return BinaryFormatter.Deserialize<object>(cache.StringGet(key));
        }

        public static bool Set(this IDatabase cache, string key, object value)
        {
            return cache.StringSet(key, BinaryFormatter.Serialize(value));
        }
    }
}
