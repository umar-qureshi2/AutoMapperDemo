﻿using AutoMapper;
using System;
using System.Collections.Generic;

namespace Demos
{
    public class Product
    {
        public int CategoryID { get; set; }
        public bool Discontinued { get; set; }
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string QuantityPerUnit { get; set; }
        public short? ReorderLevel { get; set; }

        // reference
        public Supplier Supplier { get; set; }

        public int SupplierID { get; set; }
        public decimal UnitPrice { get; set; }
        public short? UnitsInStock { get; set; }
        public short? UnitsOnOrder { get; set; }
    }

    public class Supplier
    {
        public string Address { get; set; }
        public string City { get; set; }
        public string CompanyName { get; set; }
        public string ContactName { get; set; }
        public string ContactTitle { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public List<Product> Products { get; set; }
        public int SupplierID { get; set; }
    }

    internal class ProductVM
    {
        public string DisplayName { get; set; }
        public bool Enabled { get; set; }
        public int Qty { get; set; }
        public string SupplierName { get; set; }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            InitAutoMapper();

            ProductVM productView = new ProductVM()
            {
                DisplayName = "Water Glass",
                Enabled = true,
                Qty = 1,
                SupplierName = "Ikea"
            };

            var product = Mapper.Map<Product>(productView);
            ObjectDumper.Write(product);

            var productView2 = Mapper.Map<ProductVM>(product);
            ObjectDumper.Write(productView2);

            Product emptyProduct = new Product()
            {
                ProductName = "No Supplier"
            };

            var productView3 = Mapper.Map<ProductVM>(emptyProduct);
            ObjectDumper.Write(productView3);

            Supplier Ikea = new Supplier()
            {
                CompanyName = "Ikea",
                Products = new List<Product>() { product, Mapper.Map<Product>(productView3) }
            };

            var IkeaDisplay = Mapper.Map<SupplierVM>(Ikea);
            ObjectDumper.Write(IkeaDisplay);
            
            Console.WriteLine($"Press any key to continue");
            Console.ReadKey();
        }

        private static void InitAutoMapper()
        {
            Mapper.Initialize(config =>
            {
                config.CreateMap<ProductVM, Product>()
                .ForMember(x => x.ProductName, opt => opt.MapFrom<string>(x => x.DisplayName))
                .ForMember<bool>(x => x.Discontinued, opt => opt.MapFrom<bool>(x => x.Enabled))
                .ForMember<short?>(x => x.UnitsInStock, opt => opt.MapFrom<int>(x => x.Qty))
                .ForMember<Supplier>(x => x.Supplier, opt => opt.MapFrom<Supplier>(x => new Supplier() { CompanyName = x.SupplierName }))
                .ReverseMap()
                .ForMember<string>(x => x.SupplierName, opt => opt.MapFrom<string>(x => x.Supplier.CompanyName))
                ;

                config.CreateMap<Supplier, SupplierVM>()
                .ForMember<string>(x => x.SupplierName, opt => opt.MapFrom<string>(x => x.CompanyName))
                .ForMember<int>(x => x.ProductsCount, opt => opt.MapFrom<int>(x => x.Products.Count))
                .ReverseMap()
                ;

            });
        }
    }

    internal class SupplierVM
    {
        public int ProductsCount { get; set; }
        public string SupplierName { get; set; }
    }
}