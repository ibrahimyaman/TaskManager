using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using TaskManager.Core.CrossCuttingConcerns.Logging;
using TaskManager.Core.CrossCuttingConcerns.Logging.Log4Net;
using TaskManager.Core.Utilities.Interceptors;
using TaskManager.Core.Utilities.Messages;

namespace TaskManager.Core.Aspects.Autofac.Logging
{
    public class LogAspect : MethodInterception
    {
        private LoggerServiceBase _loggerServiceBase;

        public LogAspect(Type loggerServiceType)
        {
            if (loggerServiceType.BaseType != typeof(LoggerServiceBase))
            {
                throw new Exception(AspectMessages.WrongLoggerType);
            }
            _loggerServiceBase = (LoggerServiceBase)Activator.CreateInstance(loggerServiceType);
        }

        protected override void OnBefore(IInvocation invocation)
        {
            _loggerServiceBase.Info(GetLogDetail(invocation));
        }
        
        private LogDetail GetLogDetail(IInvocation invocation)
        {
            var logParameters = new List<LogParameter>();
            for (int i = 0; i < invocation.Arguments.Length; i++)
            {
                logParameters.Add(new LogParameter
                {
                    Name = invocation.GetConcreteMethod().GetParameters()[i].Name,
                    Value = invocation.Arguments[i],
                    Type = invocation.Arguments[i].GetType().Name,
                });
            }

            return new LogDetail
            {
                MethodName = invocation.Method.Name,
                LogParameters = logParameters
            };
        }
    }
}
