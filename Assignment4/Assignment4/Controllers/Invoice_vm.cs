using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Assignment_4.Controllers
{
    public class InvoiceBase
    {
        public InvoiceBase() { }

        [Key]
        public int InvoiceId { get; set; }

        public int CustomerId { get; set; }

        public DateTime InvoiceDate { get; set; }

        [StringLength(70)]
        public string BillingAddress { get; set; }

        [StringLength(40)]
        public string BillingCity { get; set; }

        [StringLength(40)]
        public string BillingState { get; set; }

        [StringLength(40)]
        public string BillingCountry { get; set; }

        [StringLength(10)]
        public string BillingPostalCode { get; set; }


        public decimal Total { get; set; }
    }

    public class InvoiceWithDetail : InvoiceBase
    {
        public InvoiceWithDetail() { }

        [Required]
        [StringLength(40)]
        public string CustomerFirstName { get; set; }

        [Required]
        [StringLength(40)]
        public string CustomerLastName { get; set; }

        [StringLength(70)]
        public string CustomerAddress { get; set; }

        [StringLength(40)]
        public string CustomerState { get; set; }


        [Required]
        [StringLength(40)]
        public string CustomerEmployeeFirstName { get; set; }

        [Required]
        [StringLength(40)]
        public string CustomerEmployeeLastName { get; set; }

        public ICollection<InvoiceLineWithDetail> InvoiceLines { get; set; }
    }
}