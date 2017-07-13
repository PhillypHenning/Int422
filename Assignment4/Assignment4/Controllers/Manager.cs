using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using AutoMapper;
using Assignment4.Models;

namespace Assignment_4.Controllers
{
    public class Manager
    {
        // Reference to the data context
        private DataContext ds = new DataContext();

        // AutoMapper instance
        public IMapper mapper;

        public Manager()
        {
            // If necessary, add more constructor code here...

            // Configure the AutoMapper components
            var config = new MapperConfiguration(cfg =>
            {
                // Define the mappings below, for example...
                // cfg.CreateMap<SourceType, DestinationType>();
                // cfg.CreateMap<Employee, EmployeeBase>();

                //InvoiceBase
                cfg.CreateMap<Invoice, InvoiceBase>();
                cfg.CreateMap<Invoice, InvoiceWithDetail>();

                //InvoiceWithDetails
                cfg.CreateMap<InvoiceLine, InvoiceLineBase>();
                cfg.CreateMap<InvoiceLine, InvoiceLineWithDetail>();
            });

            mapper = config.CreateMapper();

            // Turn off the Entity Framework (EF) proxy creation features
            // We do NOT want the EF to track changes - we'll do that ourselves
            ds.Configuration.ProxyCreationEnabled = false;

            // Also, turn off lazy loading...
            // We want to retain control over fetching related objects
            ds.Configuration.LazyLoadingEnabled = false;
        }

        // Add methods below
        // Controllers will call these methods
        // Ensure that the methods accept and deliver ONLY view model objects and collections
        // The collection return type is almost always IEnumerable<T>

        // Suggested naming convention: Entity + task/action
        // For example:
        // ProductGetAll()
        // ProductGetById()
        // ProductAdd()
        // ProductEdit()
        // ProductDelete()



        // Invoice manager methods
        public IEnumerable<InvoiceBase> InvoiceGetAll()
        {
            return mapper.Map<IEnumerable<Invoice>, IEnumerable<InvoiceBase>>(ds.Invoices);
        }

        public InvoiceBase InvoiceGetById(int? id)
        {
            var o = ds.Invoices.Find(id.GetValueOrDefault());

            return (o == null) ? null : mapper.Map<Invoice, InvoiceBase>(o);
        }

      
        // InvoiceWithDetail manager methods
        public InvoiceWithDetail InvoiceWithDetailGetById(int? id)
        {
            var o = ds.Invoices.Include("Customer.Employee").Include("Customer").
                Include("InvoiceLines.Track.Album.Artist").Include("InvoiceLines.Track.MediaType").SingleOrDefault(ce => ce.InvoiceId == id);

            return (o == null) ? null : mapper.Map<Invoice, InvoiceWithDetail>(o);
        } 
    }
}