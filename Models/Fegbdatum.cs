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
        public int MummyId { get; set; }

        public string YearOnSkull { get; set; }

        public string MonthOnSkull { get; set; }

        public string DayOnSkull { get; set; }

        public string FieldBook { get; set; }

        public string FieldBookPageNum { get; set; }

        public bool? PostcraniaAtMagazine { get; set; }

        [Column("BYUSample")]
        public bool? Byusample { get; set; }

        public bool? SkullAtMagazine { get; set; }

        public string Skull2018StudySex { get; set; }

        public string Skull2018StudyAge { get; set; }

        [ForeignKey(nameof(MummyId))]
        [InverseProperty("Fegbdatum")]
        public virtual Mummy Mummy { get; set; }
    }
}
