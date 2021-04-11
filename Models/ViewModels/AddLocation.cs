using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WINTEX.Enums;

namespace WINTEX.Models.ViewModels
{
    public class AddLocation
    {
        [Required]
        public int? Ylower { get; set; }

        [Required]
        public int? Yupper { get; set; }

        [Required]
        public string North { get; set; }

        [Required]
        public int? Xlower { get; set; }

        [Required]
        public int? Xupper { get; set; }

        [Required]
        public string East { get; set; }

        [Required]
        public string Subplot { get; set; }
    }
}
