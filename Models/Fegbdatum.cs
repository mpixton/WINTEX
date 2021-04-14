using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WINTEX.Models
{
    [Table("FEGBData")]
    public partial class Fegbdatum
    {
        [Key]
        [Display(Name = "Mummy Id")]
        public int MummyId { get; set; }

        [Display(Name = "Year On Skull")]
        public string YearOnSkull { get; set; }

        [Display(Name = "Month On Skull")]
        public string MonthOnSkull { get; set; }

        [Display(Name = "Day On Skull")]
        public string DayOnSkull { get; set; }

        [Display(Name = "Field Book")]
        public string FieldBook { get; set; }

        [Display(Name = "Field Book Pages")]
        public string FieldBookPageNum { get; set; }

        [Display(Name = "Postcrania At Magazine")]
        public bool? PostcraniaAtMagazine { get; set; }

        [Column("BYUSample")]
        [Display(Name = "BYU Sample")]
        public bool? Byusample { get; set; }

        [Display(Name = "Skull At Magazine")]
        public bool? SkullAtMagazine { get; set; }

        [Display(Name = "2018 Skull Study Sex")]
        public string Skull2018StudySex { get; set; }

        [Display(Name = "2018 Skull Study Age")]
        public string Skull2018StudyAge { get; set; }

        [ForeignKey(nameof(MummyId))]
        [InverseProperty("Fegbdatum")]
        public virtual Mummy Mummy { get; set; }
    }
}
