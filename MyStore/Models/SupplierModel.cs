using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyStore.Models
{
    public class SupplierModel
    {
        public int Supplierid { get; set; }
        [Required]
        public string Companyname { get; set; }
        public string Contactname { get; set; }
        [Required]
        public string Contacttitle { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        [Required]
        public string Phone { get; set; }
    }
}
