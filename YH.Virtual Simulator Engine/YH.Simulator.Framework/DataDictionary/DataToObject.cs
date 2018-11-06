using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YH.Simulator.Framework
{
    public class DataToObject
    {
        public static T To<T>(int i)
        {
            Type type = typeof(T);
            dynamic data = Activator.CreateInstance(Type.GetType(type.FullName + "_"+ i.ToString("000")+","+ type.Assembly.FullName));
            T result = data;
            return result;
        }
    }
}
