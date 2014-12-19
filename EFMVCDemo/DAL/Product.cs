using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EFMVCDemo.DAL
{
    public class Product
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [StringLength(30)]
        public string Description { get; set; }
        [Required]
        public DateTime LogTime { get; set; }
    }
}