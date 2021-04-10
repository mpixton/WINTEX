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
        public int MummyId { get; set; }
        public bool? HairTaken { get; set; }
        public bool? SoftTissueTaken { get; set; }
        public bool? BoneTaken { get; set; }
        public bool? ToothTaken { get; set; }
        public bool? TextileTaken { get; set; }
        public bool? BurialSampleTaken { get; set; }
        public string DescriptionOfTaken { get; set; }
        public int? SampleNum { get; set; }
        public string Sex { get; set; }
        public string SexBodyCol { get; set; }
        [Column("GEFunctionTotal")]
        public decimal? GefunctionTotal { get; set; }
        public string PreservationNotes { get; set; }
        public string AgeAtDeath { get; set; }
        public decimal? EstimateLivingStature { get; set; }
        public string BodyAnalysis { get; set; }
        public string SexBurialMethod { get; set; }
        public string FaceBundle { get; set; }

        [ForeignKey(nameof(MummyId))]
        [InverseProperty("PostExhumationDatum")]
        public virtual Mummy Mummy { get; set; }
    }
}
