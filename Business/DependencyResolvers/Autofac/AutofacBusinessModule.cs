using System;
using System.Collections.Generic;
using System.Linq;
//using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Core;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Concrete;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using DataAccess.Concrete;
using Microsoft.AspNetCore.Http;

namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<StudentDal>().As<IStudentDal>();
            builder.RegisterType<StudentManager>().As<IStudentService>();

            builder.RegisterType<AuthManager>().As<IAuthService>();
            builder.RegisterType<JwtHelper>().As<ITokenHelper>();

            builder.RegisterType<CourseManager>().As<ICourseService>();
            builder.RegisterType<CourseDal>().As<ICourseDal>();

            builder.RegisterType<StudentCourseManager>().As<IStudentCourseService>();
            builder.RegisterType<StudentCourseDal>().As<IStudentCourseDal>();

            builder.RegisterType<GenderManager>().As<IGenderService>();
            builder.RegisterType<GenderDal>().As<IGenderDal>();

            builder.RegisterType<MaritalStatusManager>().As<IMaritalStatusService>();
            builder.RegisterType<MaritalStatusDal>().As<IMaritalStatusDal>();

            //builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>();
            //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();
        }
    }
}
