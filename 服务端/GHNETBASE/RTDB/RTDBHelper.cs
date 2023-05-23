using GHCore.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GHNETBASE.RTDB
{
    public class RTDBHelper
    {
        #region Instance
        private static readonly RTDBHelper _instance = new RTDBHelper();
        private RTDBHelper()
        {
        }
        public static RTDBHelper SingletonInstance
        {
            get
            {
                return _instance;
            }
        }
        #endregion

        private bool checkLogin()
        {
            if(!string.IsNullOrEmpty(OAUTH.OAuthHelper.SingletonInstance.UUID))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 增加通道
        /// </summary>
        /// <param name="chl">通道名称</param>
        /// <returns></returns>
        public bool AddChannel(string chl)
        {
            if (!checkLogin())
                return false;

            //OAUTH.OAuthHelper.SingletonInstance.UUID; 
            //todo

            return true;
        }

        /// <summary>
        /// 增加控制器
        /// </summary>
        /// <param name="chl">通道名称</param>
        /// <param name="ctrl">控制器名称</param>
        /// <returns></returns>
        public bool AddController(string chl,string ctrl)
        {
            if (!checkLogin())
                return false;

            //OAUTH.OAuthHelper.SingletonInstance.UUID; 
            //todo

            return true;
        }

        /// <summary>
        /// 增加变量
        /// </summary>
        /// <param name="chl">通道名称</param>
        /// <param name="ctrl">控制器名称</param>
        /// <param name="var">变量名称</param>
        /// <returns></returns>
        public bool AddVariable(string chl, string ctrl, string var)
        {
            if (!checkLogin())
                return false;

            //OAUTH.OAuthHelper.SingletonInstance.UUID; 
            //todo

            return true;
        }

        /// <summary>
        /// 读取单个变量值
        /// </summary>
        /// <param name="chl">通道名称</param>
        /// <param name="ctrl">控制器名称</param>
        /// <param name="var">变量名称</param>
        /// <returns>String</returns>
        public string ReadVariableValue(string chl, string ctrl, string var)
        {
            if (!checkLogin())
                return "";

            //OAUTH.OAuthHelper.SingletonInstance.UUID; 
            //todo

            return "";
        }

        /// <summary>
        /// 读取单个变量值
        /// </summary>
        /// <param name="chl">通道名称</param>
        /// <param name="ctrl">控制器名称</param>
        /// <param name="var">变量名称</param>
        /// <returns>typeof T object</returns>
        public T ReadVariableValue<T>(string chl, string ctrl, string var)
        {
            if (!checkLogin())
                return default(T);

            try
            {
                string json = ReadVariableValue(chl, ctrl, var);
                return JSONFormatter.Deserialize<T>(json);
            }
            catch (Exception ex)
            {
            }

            return default(T);
        }

        /// <summary>
        /// 写入变量值
        /// </summary>
        /// <param name="chl">通道名称</param>
        /// <param name="ctrl">控制器名称</param>
        /// <param name="var">变量名称</param>
        /// <param name="value">写入的值</param>
        /// <returns></returns>
        public bool WriteVariableValue(string chl, string ctrl, string var, string value)
        {
            if (!checkLogin())
                return false;

            //OAUTH.OAuthHelper.SingletonInstance.UUID; 
            //todo

            return true;
        }
        /// <summary>
        /// 写入变量值
        /// </summary>
        /// <param name="chl">通道名称</param>
        /// <param name="ctrl">控制器名称</param>
        /// <param name="var">变量名称</param>
        /// <param name="value">写入对象</param>
        /// <returns></returns>
        public bool WriteVariableValue(string chl, string ctrl, string var, object value)
        {
            if (!checkLogin())
                return false;

            string jsonValue = JSONFormatter.Serialize(value);
            return WriteVariableValue(chl, ctrl, var, jsonValue);
        }

    }
}
