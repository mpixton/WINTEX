using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WINTEX.Models
{
    public partial class ShaftLocation
    {
        public ShaftLocation()
        {
            BiologicalSamples = new HashSet<BiologicalSample>();
            CarbonDatings = new HashSet<CarbonDating>();
            Mummies = new HashSet<Mummy>();
        }

        [Key]
        public int ShaftId { get; set; }

        [Column("YLower")]
        public int? Ylower { get; set; }

        [Column("YUpper")]
        public int? Yupper { get; set; }

        [MaxLength(1)]
        public char? North { get; set; }

        [Column("XLower")]
        public int? Xlower { get; set; }

        [Column("XUpper")]
        public int? Xupper { get; set; }

        [MaxLength(1)]
        public char? East { get; set; }

        [StringLength(8)]
        public string Subplot { get; set; }

        [StringLength(22)]
        public string Lookup { get; set; }

        [InverseProperty(nameof(BiologicalSample.Shaft))]
        public virtual ICollection<BiologicalSample> BiologicalSamples { get; set; }

        [InverseProperty(nameof(CarbonDating.ShaftLocation))]
        public virtual ICollection<CarbonDating> CarbonDatings { get; set; }

        [InverseProperty(nameof(Mummy.Shaft))]
        public virtual ICollection<Mummy> Mummies { get; set; }
    }
}
