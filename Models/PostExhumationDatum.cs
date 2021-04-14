using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WINTEX.Models
{
    public partial class PostExhumationDatum
    {
        [Key]
        [Display(Name = "Mummy Id")]
        public int MummyId { get; set; }

        [Display(Name = "Hair Sample?")]
        public bool? HairTaken { get; set; }

        [Display(Name = "Soft Tissue Sample?")]
        public bool? SoftTissueTaken { get; set; }

        [Display(Name = "Bone Sample?")]
        public bool? BoneTaken { get; set; }

        [Display(Name = "Tooth Sample?")]
        public bool? ToothTaken { get; set; }

        [Display(Name = "Textile Sample?")]
        public bool? TextileTaken { get; set; }

        [Display(Name = "Burial Sample?")]
        public bool? BurialSampleTaken { get; set; }

        [Display(Name = "Description of Sample")]
        public string DescriptionOfTaken { get; set; }

        [Display(Name = "Sample Number")]
        public int? SampleNum { get; set; }

        public string Sex { get; set; }

        [Display(Name = "Sex")]
        public string SexBodyCol { get; set; }

        [Column("GEFunctionTotal")]
        [Display(Name = "GE Function Total")]
        public decimal? GefunctionTotal { get; set; }

        [Display(Name = "Preservation Notes")]
        public string PreservationNotes { get; set; }

        [Display(Name = "Age at Death")]
        public string AgeAtDeath { get; set; }

        [Display(Name = "Living Stature Estimate")]
        public decimal? EstimateLivingStature { get; set; }

        [Display(Name = "Body Analysis")]
        public string BodyAnalysis { get; set; }

        [Display(Name = "Sex Burial Method")]
        public string SexBurialMethod { get; set; }

        [Display(Name = "Face Bundle")]
        public string FaceBundle { get; set; }

        [ForeignKey(nameof(MummyId))]
        [InverseProperty("PostExhumationDatum")]
        public virtual Mummy Mummy { get; set; }
    }
}
