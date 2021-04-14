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
        [Display(Name = "Carbon Dating Id")]
        public int CarbonDatingId { get; set; }

        [Display(Name = "Rack Number")]
        public int? Racknum { get; set; }

        [Display(Name = "Shaft Id")]
        public int? ShaftLocationId { get; set; }

        [Display(Name = "Burial Number")]
        public int? BurialNum { get; set; }

        [Display(Name = "Mummy Id")]
        public int? MummyId { get; set; }

        [Display(Name = "Area Hill Burial Number")]
        public int? AreaHillBurialNum { get; set; }

        [Display(Name = "Tube Number")]
        public int? TubeNum { get; set; }

        [StringLength(75)]
        public string Description { get; set; }

        [Column("Size_mm")]
        [Display(Name = "Size (mm)")]
        public int? SizeMm { get; set; }

        [Display(Name = "")]
        public int? Foci { get; set; }

        [Column("C14Sample2017")]
        [Display(Name = "2017 C14 Sample")]
        public int? C14sample2017 { get; set; }

        [StringLength(80)]
        [Display(Name = "Location Description")]
        public string LocationDescription { get; set; }

        [StringLength(200)]
        public string Questions { get; set; }

        [Column("Conventional14CAgeBP")]
        [Display(Name = "C14 Conventional Age BP")]
        public int? Conventional14CageBp { get; set; }

        [Column("14CCalendarDate")]
        [Display(Name = "C14 Calendar Date")]
        public int? _14ccalendarDate { get; set; }

        [Display(Name = "Calibrated 95% Calendar Date Max")]
        public int? Calibrated95PerCalendarDateMax { get; set; }

        [Display(Name = "Calibrated 95% Calendar Date Min")]
        public int? Calibrated95PerCalendarDateMin { get; set; }

        [Column("Calibrated95PerCalendarDateSPAN")]
        [Display(Name = "Calibrated 95% Calendar Date Span")]
        public int? Calibrated95PerCalendarDateSpan { get; set; }

        [Column("Calibrated 95PerCalendarDateAvg")]
        [Display(Name = "Calibrated 95% Calendar Date Avg")]
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
        [Display(Name = "Shaft Location")]
        public virtual ShaftLocation ShaftLocation { get; set; }
    }
}
