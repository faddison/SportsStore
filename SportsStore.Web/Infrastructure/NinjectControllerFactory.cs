// // --------------------------------------------------------------------------------------------------------------------
// // <copyright file="NinjectControllerFactory.cs" company="Homewood Human Solutions">
// // This file is subject to the terms and conditions defined in file 'LICENSE.txt', 
// // which is part of this source code package.
// // </copyright>
// // <author>Fraser Addison</author>
// // <created>20-11-2014</created>
// //
// // <summary>
// // The NinjectControllerFactory.cs
// // </summary>
// // --------------------------------------------------------------------------------------------------------------------
namespace SportsStore.Web.Infrastructure
{
    using System;
    using System.Web.Mvc;
    using System.Web.Routing;

    using Ninject;

    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel ninjectKernel;

        public NinjectControllerFactory()
        {
            ninjectKernel = new StandardKernel();
            AddBindings();
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return controllerType == null ? null : (IController)ninjectKernel.Get(controllerType);
        }

        private void AddBindings()
        {
            // add bindings here
        }
    }
}