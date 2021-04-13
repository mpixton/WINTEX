using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WINTEX.Models
{
    [Table("CarbonDating")]
    [Index(nameof(MummyId), Name = "fki_CarbonDating_MummyId_Mummies")]
    [Index(nameof(ShaftLocationId), Name = "fki_CarbonDating_ShaftLocationIId_ShaftLocations")]
    public partial class CarbonDating
    {
        [Key]
        public int CarbonDatingId { get; set; }

        public int? Racknum { get; set; }

        public int? ShaftLocationId { get; set; }

        public int? BurialNum { get; set; }

        public int? MummyId { get; set; }

        public int? AreaHillBurialNum { get; set; }

        public int? TubeNum { get; set; }

        [StringLength(75)]
        public string Description { get; set; }

        [Column("Size_mm")]
        public int? SizeMm { get; set; }

        public int? Foci { get; set; }

        [Column("C14Sample2017")]
        public int? C14sample2017 { get; set; }

        [StringLength(80)]
        public string LocationDescription { get; set; }

        [StringLength(200)]
        public string Questions { get; set; }

        [Column("Conventional14CAgeBP")]
        public int? Conventional14CageBp { get; set; }

        [Column("14CCalendarDate")]
        public int? _14ccalendarDate { get; set; }

        public int? Calibrated95PerCalendarDateMax { get; set; }

        public int? Calibrated95PerCalendarDateMin { get; set; }

        [Column("Calibrated95PerCalendarDateSPAN")]
        public int? Calibrated95PerCalendarDateSpan { get; set; }

        [Column("Calibrated 95PerCalendarDateAvg")]
        public float? Calibrated95perCalendarDateAvg { get; set; }

        [StringLength(25)]
        public string Category { get; set; }
        
        [StringLength(50)]
        public string Notes { get; set; }

        [ForeignKey(nameof(MummyId))]
        [InverseProperty("CarbonDatings")]
        public virtual Mummy Mummy { get; set; }

        [ForeignKey(nameof(ShaftLocationId))]
        [InverseProperty("CarbonDatings")]
        public virtual ShaftLocation ShaftLocation { get; set; }
    }
}
