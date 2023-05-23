using GHDatabase.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GHRedisTest
{
    class Program
    {
        
        static void Main(string[] args)
        {
            //mysql test
            //GHCore.Database.IGHDatabaseHelper dbhelper = GHCore.Database.GHDatabaseFactory.CreateDatabase();
            //GHCore.Database.GHDBProviderType providertype = GHCore.Database.GHDatabaseFactory.GetDbProviderType(dbhelper.Name);
            //System.Data.DataSet ds = dbhelper.ExecuteDataSet(dbhelper.ConnectionString, "select * from GHC_ACCOUNT where name = @name ", new object[] { "test1"});


            //return;
            //redis test
            string authkey= "280C3D25EBD94DC198E2A7FCFB326793";//Guid.NewGuid().ToString("N");
            string queueName = "testQueue001";
            string prive = "create";
            RedisHelper redisH = new RedisHelper("192.168.59.103", 32768, 32768);

            redisH.CreateAuthRecord(authkey, new GHDatabase.Redis.Models.QueueModel[] { new GHDatabase.Redis.Models.QueueModel(){ 
                Name = "testQueue002",
                MaximumMessageSize=1024,
                MessageRetentionPeriod=0,
                CreateDate = DateTime.Now.ToUniversalTime(),
                UpdateDate = DateTime.Now.ToUniversalTime()
            } }, new string[] { prive });
            //redisH.Authorize(queueName, authkey);

            return;

            dynamic val = redisH.QueueAdd(authkey, queueName, "testValue01");//TimeSpan.FromSeconds(30)
            val = redisH.QueueGet(authkey, queueName);
            /*
              {
                Policy = (policy.HasValue ? (int)policy : POLICY_BROADCAST),
                Queue = new
                {
                    key = (string)okey,
                    value = (string)vals[1],
                    count = c
                }
              };
             */
            val = redisH.QueueDel(authkey, queueName, val.Queue.key);

            val = redisH.QueueLen(authkey, queueName);

            val = redisH.QueueAll(authkey);

            val = redisH.QueueGetDel(authkey, queueName);

            //"broadcast": POLICY_BROADCAST,//广播
            //"roundrobin": POLICY_ROUNDROBIN,//循环
            val = redisH.QueuePolicySet(authkey, queueName, "broadcast");
            val = redisH.QueuePolicyGet(authkey, queueName);

            val = redisH.QueueTail(authkey, queueName);

            val = redisH.QueueCountElements(authkey, queueName);

            val = redisH.QueueLastItems(authkey, queueName);

            val = redisH.QueueChangestatus(authkey, queueName, 1);//Statuses: STOPQUEUE 0 / STARTQUEUE 1
            val = redisH.QueueStatus(authkey, queueName);

            val = redisH.QueuePurge(authkey, queueName);

            val = redisH.PubSub(authkey, queueName,"hello world");

            val = redisH.MultiQueueByStatus(authkey, new string[] { queueName });

            redisH.Dispose();

            Console.Read();

        }
    }
}
