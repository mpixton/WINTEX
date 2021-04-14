using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WINTEX.Models
{
    [Table("GISData")]
    public partial class Gisdatum
    {
        [Key]
        [Display(Name = "Mummy Id")]
        public int MummyId { get; set; }

        [Required]
        [Display(Name = "Maturity Code")]
        public string MaturityCode { get; set; }

        [Display(Name = "Wrapping Code")]
        public string WrappingCode { get; set; }

        [Column("GISId")]
        [Display(Name = "GIS Id")]
        public int? Gisid { get; set; }

        [ForeignKey(nameof(MummyId))]
        [InverseProperty("Gisdatum")]
        public virtual Mummy Mummy { get; set; }
    }
}
