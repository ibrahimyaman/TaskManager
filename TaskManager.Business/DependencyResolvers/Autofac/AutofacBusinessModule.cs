using Autofac;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using TaskManager.Business.Abstract;
using TaskManager.Business.Concrete;
using TaskManager.Core.Utilities.Interceptors;
using TaskManager.Core.Utilities.Security.Jwt;
using TaskManager.DataAccess.Abstract;
using TaskManager.DataAccess.Concrete.EntityFramework;

namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AuthManager>().As<IAuthService>();
            builder.RegisterType<UserManager>().As<IUserService>();
            builder.RegisterType<DailyPlanManager>().As<IDailyPlanService>();
            builder.RegisterType<WeeklyPlanManager>().As<IWeeklyPlanService>();
            builder.RegisterType<MonthlyPlanManager>().As<IMonthlyPlanService>();
            builder.RegisterType<ImportanceTypeManager>().As<IImportanceTypeService>();


            builder.RegisterType<EfDailyPlanDal>().As<IDailyPlanDal>();
            builder.RegisterType<EfDailyPlanDetailDal>().As<IDailyPlanDetailDal>();
            builder.RegisterType<EfWeeklyPlanDal>().As<IWeeklyPlanDal>();
            builder.RegisterType<EfWeeklyPlanDetailDal>().As<IWeeklyPlanDetailDal>();
            builder.RegisterType<EfMonthlyPlanDal>().As<IMonthlyPlanDal>();
            builder.RegisterType<EfMonthlyPlanDetailDal>().As<IMonthlyPlanDetailDal>();
            builder.RegisterType<EfUserDal>().As<IUserDal>();
            builder.RegisterType<EfRefreshTokenDal>().As<IRefreshTokenDal>();
            builder.RegisterType<EfOperationClaimDal>().As<IOperationClaimDal>();
            builder.RegisterType<EfImportanceTypeDal>().As<IImportanceTypeDal>();


            builder.RegisterType<JwtHelper>().As<ITokenHelper>();

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();

        }
    }
}
