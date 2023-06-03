using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XRetailManagerUI.Pages;
using XRMWebUI.Library.Api;

namespace XRetailManagerUI
{
    public class MyModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Register your dependencies
            builder.RegisterType<ApiHelper>().As<IApiHelper>().SingleInstance();
            builder.RegisterType<Sales>();
            // Add more dependency registrations as needed
        }
    }
}