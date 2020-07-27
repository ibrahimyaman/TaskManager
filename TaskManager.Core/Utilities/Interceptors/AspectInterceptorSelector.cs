using Castle.DynamicProxy;
using System;
using System.Linq;
using System.Reflection;

namespace TaskManager.Core.Utilities.Interceptors
{
    public class AspectInterceptorSelector : IInterceptorSelector
    {
        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            var classAttributes = type.GetCustomAttributes<MethodInterceptionBaseAttribute>(true).ToList();
            var methodName = type.GetMethod(method.Name);
            if (methodName == null)
                return new MethodInterceptionBaseAttribute[0];

            var methodAttributes = methodName.GetCustomAttributes<MethodInterceptionBaseAttribute>(true);
            classAttributes.AddRange(methodAttributes);
            return classAttributes.OrderBy(o => o.Priority).ToArray();
        }
    }
}
