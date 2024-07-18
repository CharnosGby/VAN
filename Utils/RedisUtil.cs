using System;
using ServiceStack.Redis;

namespace Utils
{
    public class RedisUtil
    {
        private static readonly string Addr = "127.0.0.1";
        private static readonly int Port = 6379;
        private static readonly string Password = "123456";
        private static readonly long DbIndex = 0L;
        private static RedisClient redis;

        static RedisUtil()
        {
            redis = GetRedisClient(Addr, Port, Password, DbIndex);
        }

        private static RedisClient GetRedisClient(string addr, int port, string password, long dbIndex)
        {
            return new RedisClient(addr, port, password, dbIndex);
        }

        public static void DisposeRedisClient(RedisClient redisClient)
        {
            redisClient.Dispose();
        }

        public static void Set<T>(string key, T value, DateTime expireSeconds)
        {
            redis ??= GetRedisClient(Addr, Port, Password, DbIndex);
            redis.Set(key, value, expireSeconds);
        }

        public static T Get<T>(string key)
        {
            if (redis == null)
            {
                redis = GetRedisClient(Addr, Port, Password, DbIndex);
            }
            return redis.Get<T>(key);
        }
    }
}