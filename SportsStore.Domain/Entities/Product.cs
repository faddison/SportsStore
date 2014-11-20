// // --------------------------------------------------------------------------------------------------------------------
// // <copyright file="Product.cs" company="Homewood Human Solutions">
// // This file is subject to the terms and conditions defined in file 'LICENSE.txt', 
// // which is part of this source code package.
// // </copyright>
// // <author>Fraser Addison</author>
// // <created>20-11-2014</created>
// //
// // <summary>
// // The Product.cs
// // </summary>
// // --------------------------------------------------------------------------------------------------------------------
namespace SportsStore.Domain.Entities
{
    public class Product
    {
        public int ProductID { get; set; } 
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
    }
}