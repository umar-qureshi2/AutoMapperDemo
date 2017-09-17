using Demos;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosoftUnity
{
    class Program
    {
        static void Main(string[] args)
        {
            UnityContainer ioc = new UnityContainer();
            ioc.RegisterType<ISupplier, Supplier>();
            var supplier = ioc.Resolve<Supplier>();
            ObjectDumper.Write(supplier);

            var product = ioc.Resolve<Product>();
            ObjectDumper.Write(product);
            ObjectDumper.Write(supplier);
            Console.ReadKey();
        }
    }


    public interface ISupplier
    {
        string Address { get; set; }
        string City { get; set; }
        string CompanyName { get; set; }
        string ContactName { get; set; }
        string ContactTitle { get; set; }
        string Country { get; set; }
        string PostalCode { get; set; }
        int SupplierID { get; set; }
    }

    public class Supplier : ISupplier
    {
        public string Address { get; set; }
        public string City { get; set; }
        public string CompanyName { get; set; }
        public string ContactName { get; set; }
        public string ContactTitle { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public int SupplierID { get; set; }
    }

    public class Product
    {
        public Product(ISupplier supplier)
        {
            this.Supplier = supplier;
            this.SupplierID = supplier.SupplierID = 3;
        }
        public int CategoryID { get; set; }
        public bool Discontinued { get; set; }
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string QuantityPerUnit { get; set; }
        public short? ReorderLevel { get; set; }
        // reference
        public ISupplier Supplier { get; set; }
        public int SupplierID { get; set; }
        public decimal UnitPrice { get; set; }
        public short? UnitsInStock { get; set; }
        public short? UnitsOnOrder { get; set; }
    }

}
