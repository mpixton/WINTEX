using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WINTEX.Models
{
    public class HairColorCodes
    {
        [Key]
        [Display(Name = "Hair Color Code")]
        public string HairColorCode { get; set; }

        [Display(Name = "Description")]
        public string HairColorDescription { get; set; }
    }
}
