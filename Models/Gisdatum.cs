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
        public int MummyId { get; set; }

        [Required]
        public string MaturityCode { get; set; }

        public string WrappingCode { get; set; }

        [Column("GISId")]
        public int? Gisid { get; set; }


        [ForeignKey(nameof(MummyId))]
        [InverseProperty("Gisdatum")]
        public virtual Mummy Mummy { get; set; }
    }
}
