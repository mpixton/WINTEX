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
        public string HairColorCode { get; set; }

        public string HairColorDescription { get; set; }
    }
}
