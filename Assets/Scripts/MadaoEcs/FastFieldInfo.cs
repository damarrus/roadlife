using System;
using System.Reflection;

namespace MadaoEcs {
    public class FastPropertyInfo {

        public Action<object> SetValue;
        public Func<object> GetValue;

        public FastPropertyInfo(PropertyInfo fieldInfo) {
            SetValue = (Action<object>)Delegate.CreateDelegate(typeof(Action<object>), null, fieldInfo.GetSetMethod());
            GetValue = (Func<object>)Delegate.CreateDelegate(typeof(Func<object>), null, fieldInfo.GetGetMethod());
        }

    }
}
