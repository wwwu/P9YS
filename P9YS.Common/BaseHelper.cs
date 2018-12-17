using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace P9YS.Common
{
    public class BaseHelper
    {
        /// <summary>  
        /// 获取程序集中的实现类对应的多个接口
        /// </summary>  
        /// <param name="assemblyName">程序集</param>
        /// <param name="func">Type筛选</param>
        public static Dictionary<Type, Type[]> GetClassName(string assemblyName, Func<Type, bool> func = null)
        {
            Assembly assembly = Assembly.Load(assemblyName);

            var types = assembly.GetTypes().ToList();
            if (func != null)
                types = types.Where(func).ToList();

            var result = new Dictionary<Type, Type[]>();
            foreach (var item in types)
            {
                var interfaceType = item.GetInterfaces();
                result.Add(item, interfaceType);
            }
            return result;
        }
    }
}
