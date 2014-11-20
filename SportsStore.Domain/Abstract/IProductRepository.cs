// // --------------------------------------------------------------------------------------------------------------------
// // <copyright file="IProductsRepository.cs" company="Homewood Human Solutions">
// // This file is subject to the terms and conditions defined in file 'LICENSE.txt', 
// // which is part of this source code package.
// // </copyright>
// // <author>Fraser Addison</author>
// // <created>20-11-2014</created>
// //
// // <summary>
// // The IProductsRepository.cs
// // </summary>
// // --------------------------------------------------------------------------------------------------------------------
namespace SportsStore.Domain.Abstract
{
    using System.Linq;

    using SportsStore.Domain.Entities;

    public interface IProductRepository
    {
        IQueryable<Product> Products { get; } 
    }
}