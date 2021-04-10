using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WINTEX.Models
{
    [Index(nameof(ShaftId), Name = "fki_Mummies_ShaftId_ShaftLocations")]
    [Index(nameof(TombId), Name = "fki_Mummies_TombId_TombLocations")]
    public partial class Mummy
    {
        public Mummy()
        {
            BiologicalSamples = new HashSet<BiologicalSample>();
            CarbonDatings = new HashSet<CarbonDating>();
            FegbmummyStorages = new HashSet<FegbmummyStorage>();
            MummyNotes = new HashSet<MummyNote>();
        }

        [Key]
        public int MummyId { get; set; }

        public string BurialNum { get; set; }

        public int? ShaftId { get; set; }

        public int? TombId { get; set; }

        public decimal? BurialDepth { get; set; }

        public decimal? WestToHead { get; set; }

        public decimal? WestToFeet { get; set; }

        public decimal? SouthToHead { get; set; }

        public decimal? SouthToFeet { get; set; }

        public decimal? Length { get; set; }

        public string BurialSituation { get; set; }

        public string Goods { get; set; }

        public string ArtifactsDescription { get; set; }

        public bool? Photo { get; set; }

        public string PreservationIndex { get; set; }

        public string ClusterNum { get; set; }

        public string HairColorCode { get; set; }

        public string AgeCodeSingle { get; set; }

        public string BurialMaterials { get; set; }

        public string ExcavationRecorder { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Date { get; set; }

        public string YearExcavated { get; set; }

        public string MonthExcavated { get; set; }

        public string DayExcavated { get; set; }

        public string HeadDirection { get; set; }

        public bool? ArtifactFound { get; set; }


        [ForeignKey(nameof(ShaftId))]
        [InverseProperty(nameof(ShaftLocation.Mummies))]
        public virtual ShaftLocation Shaft { get; set; }

        [ForeignKey(nameof(TombId))]
        [InverseProperty(nameof(TombLocation.Mummies))]
        public virtual TombLocation Tomb { get; set; }

        [InverseProperty("Mummy")]
        public virtual Fegbdatum Fegbdatum { get; set; }

        [InverseProperty("Mummy")]
        public virtual Gisdatum Gisdatum { get; set; }

        [InverseProperty("Mummy")]
        public virtual OsteologicalMummyDatum OsteologicalMummyDatum { get; set; }

        [InverseProperty("Mummy")]
        public virtual PostExhumationDatum PostExhumationDatum { get; set; }

        [InverseProperty(nameof(BiologicalSample.Mummy))]
        public virtual ICollection<BiologicalSample> BiologicalSamples { get; set; }

        [InverseProperty(nameof(CarbonDating.Mummy))]
        public virtual ICollection<CarbonDating> CarbonDatings { get; set; }

        [InverseProperty(nameof(FegbmummyStorage.Mummy))]
        public virtual ICollection<FegbmummyStorage> FegbmummyStorages { get; set; }

        [InverseProperty(nameof(MummyNote.Mummy))]
        public virtual ICollection<MummyNote> MummyNotes { get; set; }
    }
}
