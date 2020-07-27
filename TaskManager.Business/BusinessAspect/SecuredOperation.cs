using Castle.DynamicProxy;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using TaskManager.Business.Constants;
using TaskManager.Core.Exceptions;
using TaskManager.Core.Extensions;
using TaskManager.Core.Utilities.Interceptors;
using TaskManager.Core.Utilities.IoC;

namespace TaskManager.Business.BusinessAspect
{
    public class SecuredOperation : MethodInterception
    {
        private string[] _roles;
        private string _argumentNameOfIdentifier = "UserId";
        private IHttpContextAccessor _httpContextAccessor;
        public SecuredOperation()
        {
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }
        public SecuredOperation(string roles) : this()
        {
            if (roles.IsNullOrWhiteSpace())
                throw new CustomException("Yetki tanıma sorunu ile karşılaşıldı");

            _roles = roles.Split(',');
        }
        protected override void OnBefore(IInvocation invocation)
        {
            if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                var roleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles();
                if (_roles != null && _roles.Any())
                {

                    foreach (var item in _roles)
                    {
                        if (roleClaims.Contains(item))
                        {
                            return;
                        }
                    }
                }
                else
                {
                    var nameOfIdendifierProperty = invocation.TargetType.GetProperty(_argumentNameOfIdentifier);

                    if (nameOfIdendifierProperty != null)
                    {
                        var value = Convert.ChangeType(_httpContextAccessor.HttpContext.User.NameIdentifier(), nameOfIdendifierProperty.PropertyType);
                        nameOfIdendifierProperty.SetValue(invocation.InvocationTarget, value);
                        return;
                    }
                }
            }
            
            throw new CustomException(Messages.AuthorizationDenied);
        }
    }
}
