using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GHDatabase.Redis.Models
{
    /// <summary>
    /// 队列模型
    /// </summary>
    public class AUTHModel
    {
        /// <summary>
        /// 用户队列
        /// </summary>
        public IEnumerable<QueueModel> queues { get; set; }
        /// <summary>
        /// 授权
        /// </summary>
        public IEnumerable<string> privs { get; set; }
    }

    /// <summary>
    /// 用户队列 属性
    /// </summary>
    public class QueueModel
    {
        /// <summary>
        /// 队列名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 取出消息隐藏时长(秒)
        /// 
        /// 队列的VisibilityTimeout 属性，消息从本队列中取出后会被从Active可取状态变成Inactive隐藏状态后，
        /// 本属性指定了消息隐藏持续时间，这个时间一到，消息会从隐藏恢复成Active可取状态；
        /// 单位为秒，有效值范围1-43200秒，也即1秒到12小时。
        /// </summary>
        public int VisibilityTimeout { set; get; }
        /// <summary>
        /// 消息的最大长度
        /// 
        /// 队列的MaxMessageSize属性， 限定允许发送到该队列的消息体的最大长度；
        /// 单位为byte， 有效值范围为1024-65536 也即1K到64K
        /// </summary>
        public int MaximumMessageSize { set; get; }
        /// <summary>
        /// 消息存活时间(秒)
        /// 
        /// 队列的MessageRetentionPeriod属性， 消息在本队列中最长的存活时间，从发送到该队列开始经过此参数指定的时间后，
        /// 不论消息是否被取出过都将被删除；
        /// 单位为秒，有效值范围60-1296000秒，也即1分钟到15天
        /// </summary>
        public int MessageRetentionPeriod { set; get; }
        /// <summary>
        /// 消息延时(秒)
        /// 
        /// 队列的MessageRetentionPeriod属性， 消息在本队列中最长的存活时间，从发送到该队列开始经过此参数指定的时间后，
        /// 不论消息是否被取出过都将被删除；
        /// 单位为秒，有效值范围60-1296000秒，也即1分钟到15天
        /// </summary>
        public int DelaySeconds { set; get; }
        /// <summary>
        /// 消息接收长轮询等待时间(秒)
        /// 
        /// 队列的PollingWaitSeconds属性， 设为0时关闭长轮询；当不为0时，长轮询模式开启，
        /// 此时一个消息消费请求只会在取到有效消息或长轮询超时时才返回响应；
        /// 单位秒，有效值范围为0-30秒。
        /// </summary>
        public int PollingWaitSeconds { set; get; }

        public DateTime CreateDate { set; get; }

        public DateTime UpdateDate { get; set; }
    }
}
