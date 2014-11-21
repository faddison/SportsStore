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
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using System.Web.Routing;

    using Moq;

    using Ninject;

    using SportsStore.Domain.Abstract;
    using SportsStore.Domain.Entities;
    using SportsStore.Domain.Concrete;

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
            /*
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products)
                .Returns(
                    new List<Product>
                        {
                            new Product() { Name = "Football", Price = 25 },
                            new Product() { Name = "Surf board", Price = 179 },
                            new Product() { Name = "Runner shoes", Price = 95 }
                        }.AsQueryable());

            ninjectKernel.Bind<IProductRepository>().ToConstant(mock.Object);
             */

            ninjectKernel.Bind<IProductRepository>().To<EFProductRepository>();
        }
    }
}