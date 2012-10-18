using ConsoleApplication1.Models;
using Microsoft.Data.Edm;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData.Builder;
using System.Web.Http.OData.Builder.Conventions;
using System.Web.Http.OData.Formatter;
using System.Web.Http.SelfHost;

namespace ConsoleApplication1
{
    class Service
    {
        static readonly Uri _baseAddress = new Uri(string.Format("http://{0}:9095/", Environment.MachineName));

        static void Main(string[] args)
        {
            HttpSelfHostServer server = null;

            try
            {
                //setup data
                //DbMigrator migrator = new DbMigrator(new Migrations.Configuration());
                //migrator.Update(null);
                //return;

                // Set up server configuration
                HttpSelfHostConfiguration configuration = new HttpSelfHostConfiguration(_baseAddress);
                configuration.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;

                //TraceConfig.Register(configuration);

                // Register an Action selector that can include template parameters in the name
                configuration.Services.Replace(typeof(IHttpActionSelector), new ODataActionSelector());

                // Generate a model
                IEdmModel model = GetEdmModel();
                //configuration.SetEdmModel(model);

                // Create the OData formatter and give it the model
                ODataMediaTypeFormatter odataFormatter = new ODataMediaTypeFormatter(model);
                configuration.SetODataFormatter(odataFormatter);

                // Metadata routes to support $metadata and code generation in the WCF Data Service client.
                configuration.Routes.MapHttpRoute(ODataRouteNames.Metadata, "$metadata", new { Controller = "ODataMetadata", Action = "GetMetadata" });
                configuration.Routes.MapHttpRoute(ODataRouteNames.ServiceDocument, "", new { Controller = "ODataMetadata", Action = "GetServiceDocument" });

                // Relationship routes (notice the parameters is {type}Id not id, this avoids colliding with GetById(id)).
                //configuration.Routes.MapHttpRoute(ODataRouteNames.InvokeBoundAction, "{controller}({boundId})/{odataAction}");
                configuration.Routes.MapHttpRoute(ODataRouteNames.PropertyNavigation, "{controller}({parentId})/{navigationProperty}");

                // Route for manipulating links.
                //configuration.Routes.MapHttpRoute(ODataRouteNames.Link, "{controller}({id})/$links/Products");
                configuration.Routes.MapHttpRoute(ODataRouteNames.Link, "{controller}({id})/$links/{navigationProperty}");

                // Routes for urls both producing and handling urls like ~/Product(1), ~/Products() and ~/Products
                configuration.Routes.MapHttpRoute(ODataRouteNames.GetById, "{controller}({id})");
                configuration.Routes.MapHttpRoute(ODataRouteNames.DefaultWithParentheses, "{controller}()");
                configuration.Routes.MapHttpRoute(ODataRouteNames.Default, "{controller}");

                // Create server
                server = new HttpSelfHostServer(configuration);

                // Start listening
                server.OpenAsync().Wait();
                Console.WriteLine("Listening on " + _baseAddress);
            }
            catch (Exception e)
            {
                Console.WriteLine("Could not start server: {0}", e.GetBaseException().Message);
            }
            finally
            {
                Console.WriteLine("Hit ENTER to exit...");
                Console.ReadLine();

                if (server != null)
                {
                    // Stop listening
                    server.CloseAsync().Wait();
                }
            }
        }

        static IEdmModel GetEdmModel()
        {
            // build the model by convention
            return GetImplicitEdmModel();
        }

        static IEdmModel GetImplicitEdmModel()
        {
            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();

            // Views from Database.
            var es1 = builder.EntitySet<Alphabetical_list_of_product>("AlphabeticalListOfProducts");
            var es2 = builder.EntitySet<Category_Sales_for_1997>("CategorySalesFor1997");
            
            es1.EntityType.HasKey(p => p.ProductID);
            es2.EntityType.HasKey(c => c.CategoryName);

            var employees = builder.EntitySet<Employee>("Employees");
            var categories = builder.EntitySet<Category>("Categories");
            var customerDemographics = builder.EntitySet<CustomerDemographic>("CustomerDemographics");
            var customers = builder.EntitySet<Customer>("Customers");
            var orders = builder.EntitySet<Order>("Orders");
            var orderDetails = builder.EntitySet<Order_Detail>("OrderDetails");
            var products = builder.EntitySet<Product>("Products");
            var regions = builder.EntitySet<Region>("Regions");
            var shippers = builder.EntitySet<Shipper>("Shippers");
            var suppliers = builder.EntitySet<Supplier>("Suppliers");
            var territories = builder.EntitySet<Territory>("Territories");
            var invoices = builder.EntitySet<Invoice>("Invoices");
            var photos = builder.EntitySet<Photo>("Photos");

            // hide photo details of the Employee
            employees.EntityType.Ignore(emp => emp.Photo);
            employees.EntityType.Ignore(emp => emp.PhotoPath);
            
            // create keys for the auto-generated classes for which
            // they do not conform to conventions
            customerDemographics.EntityType.HasKey(cd => cd.CustomerTypeID);
            orderDetails.EntityType.HasKey(od => new { od.ProductID, od.OrderID });
            invoices.EntityType.HasKey(iv => new { iv.ProductID, iv.OrderID });
            photos.EntityType.HasKey(ph => ph.EmployeeID);
            
            //setup relationships
            orderDetails.HasRequiredBinding(od => od.Order, orders);
            orderDetails.HasRequiredBinding(od => od.Product, products);
            territories.HasRequiredBinding(tr => tr.Region, regions);

            // Employee and Photos form a 1-1 relationship. Even though at the database level
            // Photos is present in the Employee table, here we are representing it as a separate entity set which is fine
            photos.HasRequiredBinding(ph => ph.Employee, employees);
            employees.HasRequiredBinding(emp => emp.PhotoInfo, photos);

            //public DbSet<Category> Categories { get; set; }
            //public DbSet<CustomerDemographic> CustomerDemographics { get; set; }
            //public DbSet<Customer> Customers { get; set; }
            //public DbSet<Employee> Employees { get; set; }
            //public DbSet<Order_Detail> Order_Details { get; set; }
            //public DbSet<Order> Orders { get; set; }
            //public DbSet<Product> Products { get; set; }
            //public DbSet<Region> Regions { get; set; }
            //public DbSet<Shipper> Shippers { get; set; }
            //public DbSet<Supplier> Suppliers { get; set; }
            //public DbSet<Territory> Territories { get; set; }
            //public DbSet<Alphabetical_list_of_product> Alphabetical_list_of_products { get; set; }
            //public DbSet<Category_Sales_for_1997> Category_Sales_for_1997 { get; set; }
            //public DbSet<Current_Product_List> Current_Product_Lists { get; set; }
            //public DbSet<Customer_and_Suppliers_by_City> Customer_and_Suppliers_by_Cities { get; set; }
            //public DbSet<Invoice> Invoices { get; set; }
            //public DbSet<Order_Details_Extended> Order_Details_Extendeds { get; set; }
            //public DbSet<Order_Subtotal> Order_Subtotals { get; set; }
            //public DbSet<Orders_Qry> Orders_Qries { get; set; }
            //public DbSet<Product_Sales_for_1997> Product_Sales_for_1997 { get; set; }
            //public DbSet<Products_Above_Average_Price> Products_Above_Average_Prices { get; set; }
            //public DbSet<Products_by_Category> Products_by_Categories { get; set; }
            //public DbSet<Sales_by_Category> Sales_by_Categories { get; set; }
            //public DbSet<Sales_Totals_by_Amount> Sales_Totals_by_Amounts { get; set; }
            //public DbSet<Summary_of_Sales_by_Quarter> Summary_of_Sales_by_Quarters { get; set; }
            //public DbSet<Summary_of_Sales_by_Year> Summary_of_Sales_by_Years { get

            return builder.GetEdmModel();
        }

        static IEdmModel BuildModel()
        {
            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();

            return builder.GetEdmModel();
        }
    }
}
