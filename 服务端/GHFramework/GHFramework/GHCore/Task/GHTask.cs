using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace GHCore.Task
{
    public class GHTask
    {
        static ConcurrentDictionary<Type, object> container = new ConcurrentDictionary<Type, object>();

        public static void Execute<T>(string methodName, string typeName, T content)
        {
            try
            {
                //string key = (methodName + typeName).ToUpper();
                Type type = Type.GetType(typeName);
                object active = null;
                if (container.ContainsKey(type))
                {
                    active = container[type];
                }
                else
                {
                    active = Activator.CreateInstance(type);
                    container[type] = active;
                }
                var methodInfo = type.GetMethod(methodName);
                var action = BuildAction<T>(methodInfo);
                action(active, content);

                #region
                //DynamicMethodExecutor methodExec = new DynamicMethodExecutor(methodInfo);
                //var obj = methodExec.Execute(active, content);

                //MethodInfo methodInfo = null;
                //if(container.ContainsKey(key))
                //    methodInfo = container[key];
                //if(methodInfo == null)
                //{
                //    var type = Type.GetType(typeName);
                //    if (type != null)
                //    {
                //        var active = Activator.CreateInstance(type);
                //        methodInfo = type.GetMethod(methodName, BindingFlags.Static| BindingFlags.Public);
                //        if(methodInfo != null)
                //            container[key] = methodInfo;
                //    }
                //}

                //    if(methodInfo != null)
                //    {
                //        List<object> pams = new List<object>();
                //        foreach(var pa in methodInfo.GetParameters())
                //        {
                //            var kv = content.FirstOrDefault(t=>t.Key.Equals(pa.Name, StringComparison.InvariantCultureIgnoreCase));
                //            if(string.IsNullOrWhiteSpace(kv.Value))
                //            {
                //                pams.Add(null);
                //            }
                //            else
                //            {
                //                if( pa.ParameterType.IsValueType)
                //                {

                //                    object o = Convert.ChangeType(kv.Value, pa.ParameterType);
                //                    pams.Add(o);
                //                }
                //                else
                //                    pams.Add(null);
                //            }
                //        }
                //        methodInfo.ReflectedType
                //        methodInfo.Invoke(active, pams.ToArray());
                //    }
                #endregion
            }
            catch (Exception ex)
            {
                GHLogger.Log(ex.Message, LogCategory.Exception);
#if DEBUG
                Console.WriteLine(ex.Message);
#endif
                throw;
            }
        }

        /// <summary>
        /// 动态构造委托
        /// var action = (Action<TInstance, T1, T2>)BuildDynamicDelegate(methodInfo);
        // var func = (Func<TInstance, T1, T2, TReturn>)BuildDynamicDelegate(methodInfo);
        /// </summary>
        /// <param name="methodInfo">方法元数据</param>
        /// <returns>委托</returns>
        public static Delegate BuildDynamicDelegate(MethodInfo methodInfo)
        {
            if (methodInfo == null)
                throw new ArgumentNullException("methodInfo");

            var paramExpressions = methodInfo.GetParameters().Select((p, i) =>
            {
                var name = "param" + (i + 1).ToString(CultureInfo.InvariantCulture);
                return System.Linq.Expressions.Expression.Parameter(p.ParameterType, name);
            }).ToList();

            MethodCallExpression callExpression;
            if (methodInfo.IsStatic)
            {
                //Call(params....)
                callExpression = System.Linq.Expressions.Expression.Call(methodInfo, paramExpressions);
            }
            else
            {
                var instanceExpression = System.Linq.Expressions.Expression.Parameter(typeof(object), "instance");
                //((T)instance)
                var castExpression = System.Linq.Expressions.Expression.Convert(instanceExpression, methodInfo.ReflectedType);
                //((T)instance).Call(params)
                callExpression = System.Linq.Expressions.Expression.Call(castExpression, methodInfo, paramExpressions);
                paramExpressions.Insert(0, instanceExpression);
            }

            var lambdaExpression = System.Linq.Expressions.Expression.Lambda(callExpression, paramExpressions);
            return lambdaExpression.Compile();
        }

        //使用
        public static Action<object> BuildAction(MethodInfo methodInfo)
        {
            return (Action<object>)BuildDynamicDelegate(methodInfo);
        }

        public static Action<object, T1> BuildAction<T1>(MethodInfo methodInfo)
        {
            return (Action<object, T1>)BuildDynamicDelegate(methodInfo);
        }


    }


    /// <summary>
    /// DynamicMethodExecutor executor = new DynamicMethodExecutor(methodInfo);
    /// executor.Execute(program, parameters);
    /// </summary>
    internal class DynamicMethodExecutor
    {
        private Func<object, object[], object> m_execute;

        public DynamicMethodExecutor(MethodInfo methodInfo)
        {
            this.m_execute = this.GetExecuteDelegate(methodInfo);
        }

        public object Execute(object instance, object[] parameters)
        {
            return this.m_execute(instance, parameters);
        }

        private Func<object, object[], object> GetExecuteDelegate(MethodInfo methodInfo)
        {
            // parameters to execute
            ParameterExpression instanceParameter =
                System.Linq.Expressions.Expression.Parameter(typeof(object), "instance");
            ParameterExpression parametersParameter =
                System.Linq.Expressions.Expression.Parameter(typeof(object[]), "parameters");

            // build parameter list
            List<System.Linq.Expressions.Expression> parameterExpressions = new List<System.Linq.Expressions.Expression>();
            ParameterInfo[] paramInfos = methodInfo.GetParameters();
            for (int i = 0; i < paramInfos.Length; i++)
            {
                // (Ti)parameters[i]
                BinaryExpression valueObj = System.Linq.Expressions.Expression.ArrayIndex(
                    parametersParameter, System.Linq.Expressions.Expression.Constant(i));
                UnaryExpression valueCast = System.Linq.Expressions.Expression.Convert(
                    valueObj, paramInfos[i].ParameterType);

                parameterExpressions.Add(valueCast);
            }

            // non-instance for static method, or ((TInstance)instance)
            System.Linq.Expressions.Expression instanceCast = methodInfo.IsStatic ? null :
                System.Linq.Expressions.Expression.Convert(instanceParameter, methodInfo.ReflectedType);

            // static invoke or ((TInstance)instance).Method
            MethodCallExpression methodCall = System.Linq.Expressions.Expression.Call(
                instanceCast, methodInfo, parameterExpressions);

            // ((TInstance)instance).Method((T0)parameters[0], (T1)parameters[1], ...)
            if (methodCall.Type == typeof(void))
            {
                Expression<Action<object, object[]>> lambda =
                    System.Linq.Expressions.Expression.Lambda<Action<object, object[]>>(
                        methodCall, instanceParameter, parametersParameter);

                Action<object, object[]> execute = lambda.Compile();
                return (instance, parameters) =>
                {
                    execute(instance, parameters);
                    return null;
                };
            }
            else
            {
                UnaryExpression castMethodCall = System.Linq.Expressions.Expression.Convert(
                    methodCall, typeof(object));
                Expression<Func<object, object[], object>> lambda =
                    System.Linq.Expressions.Expression.Lambda<Func<object, object[], object>>(
                        castMethodCall, instanceParameter, parametersParameter);

                return lambda.Compile();
            }
        }

    }

}
