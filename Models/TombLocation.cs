using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WINTEX.Models
{
    public partial class TombLocation
    {
        public TombLocation()
        {
            Mummies = new HashSet<Mummy>();
        }

        [Key]
        public int TombLocationId { get; set; }
        [StringLength(10)]
        public string LookupValue { get; set; }
        [StringLength(5)]
        public string AreaHillBurial { get; set; }
        [StringLength(5)]
        public string Tomb { get; set; }

        [InverseProperty(nameof(Mummy.Tomb))]
        public virtual ICollection<Mummy> Mummies { get; set; }
    }
}
