using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using WINTEX.Models.ViewModels;

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
        [Display(Name = "Mummy Id")]
        public int MummyId { get; set; }
        
        [Display(Name = "Burial Number")]
        public string BurialNum { get; set; }

        [Display(Name = "Shaft Location")]
        public int? ShaftId { get; set; }

        [Display(Name = "Tomb Location")]
        public int? TombId { get; set; }

        [Display(Name = "Burial Depth")]
        [DisplayFormat(DataFormatString = "{0:N0}")]
        public decimal? BurialDepth { get; set; }

        [Display(Name = "West To Head")]
        public decimal? WestToHead { get; set; }
        
        [Display(Name = "West To Feet")]
        public decimal? WestToFeet { get; set; }

        [Display(Name = "South To Head")]
        public decimal? SouthToHead { get; set; }

        [Display(Name = "South To Feet")]
        public decimal? SouthToFeet { get; set; }

        public decimal? Length { get; set; }

        [Display(Name = "Burial Situation")]
        public string BurialSituation { get; set; }

        public string Goods { get; set; }

        [Display(Name = "Artifacts Description")]
        public string ArtifactsDescription { get; set; }

        [Display(Name = "Photo Taken?")]
        public bool? Photo { get; set; }

        [Display(Name = "Preservation Index")]
        public string PreservationIndex { get; set; }

        [Display(Name = "Cluser Id")]
        public string ClusterNum { get; set; }

        [Display(Name = "Hair Color Code")]
        public string HairColorCode { get; set; }

        [Display(Name = "Age Code Single")]
        public string AgeCodeSingle { get; set; }

        [Display(Name = "Burials Materials")]
        public string BurialMaterials { get; set; }

        [Display(Name = "Excavation Recorder")]
        public string ExcavationRecorder { get; set; }

        [Column(TypeName = "timestamp with time zone")]
        [Display(Name = "Date Excavated")]
        public DateTime? DateExcavated { get; set; }

        [Display(Name = "Year Excavated")]
        public string YearExcavated { get; set; }

        [Display(Name = "Month Excavated")]
        public string MonthExcavated { get; set; }

        [Display(Name = "Day Excavated")]
        public string DayExcavated { get; set; }

        [Display(Name = "Head Direction")]
        public string HeadDirection { get; set; }

        [Display(Name = "Artifact(s) Found?")]
        public bool? ArtifactFound { get; set; }

        [ForeignKey(nameof(ShaftId))]
        [InverseProperty(nameof(ShaftLocation.Mummies))]
        [Display(Name = "Shaft Location")]
        public virtual ShaftLocation Shaft { get; set; }

        [ForeignKey(nameof(TombId))]
        [InverseProperty(nameof(TombLocation.Mummies))]
        [Display(Name = "Tomb Location")]
        public virtual TombLocation Tomb { get; set; }

        [InverseProperty("Mummy")]
        [Display(Name = "FEGB Data")]
        public virtual Fegbdatum Fegbdatum { get; set; }

        [InverseProperty("Mummy")]
        [Display(Name = "DIS Data")]
        public virtual Gisdatum Gisdatum { get; set; }

        [InverseProperty("Mummy")]
        [Display(Name = "Osteological Data")]
        public virtual OsteologicalMummyDatum OsteologicalMummyDatum { get; set; }

        [InverseProperty("Mummy")]
        [Display(Name = "Post Exhumation Datum")]
        public virtual PostExhumationDatum PostExhumationDatum { get; set; }

        [InverseProperty(nameof(BiologicalSample.Mummy))]
        [Display(Name = "Biological Samples")]
        public virtual ICollection<BiologicalSample> BiologicalSamples { get; set; }

        [InverseProperty(nameof(CarbonDating.Mummy))]
        [Display(Name = "Carbon Dating Samples")]
        public virtual ICollection<CarbonDating> CarbonDatings { get; set; }

        [InverseProperty(nameof(FegbmummyStorage.Mummy))]
        [Display(Name = "FEGB Storage Location")]
        public virtual ICollection<FegbmummyStorage> FegbmummyStorages { get; set; }

        [InverseProperty(nameof(MummyNote.Mummy))]
        [Display(Name = "Mummy Notes")]
        public virtual ICollection<MummyNote> MummyNotes { get; set; }

        public static implicit operator Mummy(AddMummyViewModel m)
        {
            return new Mummy()
            {
                ShaftId = m.ShaftLocationId,
                TombId = m.TombLocationId,
                BurialNum = m.BurialNum,
                ArtifactFound = m.ArtifactFound,
                BurialDepth = m.BurialDepth,
                BurialMaterials = m.BurialMaterials,
                ClusterNum = m.ClusterNum,
                DateExcavated = m.DateTime,
                ExcavationRecorder = m.ExcavationRecorder,
                HairColorCode = m.HairColorCode,
                Photo = m.Photo,
                WestToFeet = m.WestToFeet,
                WestToHead = m.WestToHead,
                SouthToFeet = m.SouthToFeet,
                SouthToHead = m.SouthToHead,
                PreservationIndex = m.PreservationIndex,
                Length = m.Length
            };
        }
    }
}
