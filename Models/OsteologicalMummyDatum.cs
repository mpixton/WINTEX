using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WINTEX.Models
{
    public partial class OsteologicalMummyDatum
    {
        [Key]
        public int MummyId { get; set; }

        public decimal? FemurHead { get; set; }

        public decimal? HumerusHead { get; set; }

        public string Osteophytosis { get; set; }

        public string PubicSymphysis { get; set; }

        public decimal? FemurLength { get; set; }

        public decimal? HumerusLength { get; set; }

        public decimal? TibiaLength { get; set; }

        public int? Robust { get; set; }

        public int? SupraorbitalRidges { get; set; }

        public int? OrbitEdge { get; set; }

        public int? ParietalBossing { get; set; }

        public int? Gonian { get; set; }

        public int? NuchalCrest { get; set; }

        public int? ZygomaticCrest { get; set; }

        public string CranialSuture { get; set; }

        public decimal? MaximumCranialLength { get; set; }

        public decimal? MaximumCranialBreadth { get; set; }

        public decimal? BasionBregmaHeight { get; set; }

        public decimal? BasionNasion { get; set; }

        public decimal? BasionProstionLength { get; set; }

        public decimal? BizygomaticDiameter { get; set; }

        public decimal? NasionProsthion { get; set; }

        public decimal? MaximumNasalBreadth { get; set; }

        public string BasilarSuture { get; set; }

        public int? VentralArc { get; set; }

        public int? SubpubicAngle { get; set; }

        public int? SciaticNotch { get; set; }

        public int? PubicBone { get; set; }

        public int? PreaurSulcus { get; set; }

        [Column("MedialIPRamus")]
        public int? MedialIpramus { get; set; }

        public int? DorsalPitting { get; set; }

        public decimal? InterorbitalBreadth { get; set; }

        public string BurialHairColor { get; set; }

        public string ToothAttrition { get; set; }

        public string ToothEruption { get; set; }

        public string PathologyAnomalies { get; set; }

        public string EphiphysealUnion { get; set; }

        public bool? SkullTrauma { get; set; }

        public bool? PostcraniaTrauma { get; set; }

        public bool? CribraOrbitala { get; set; }

        public bool? PoroticHyperostosis { get; set; }

        public bool? MetopicSuture { get; set; }

        public bool? ButtonOsteoma { get; set; }

        public bool? TemporalMandibularJointOsteoarthritis { get; set; }

        public bool? LinearHypoplasiaEnamel { get; set; }

        public string PoroticHyperostosisLocations { get; set; }

        public string OsteologyUnknownComment { get; set; }

        public bool? ToBeConfirmed { get; set; }

        [ForeignKey(nameof(MummyId))]
        [InverseProperty("OsteologicalMummyDatum")]
        public virtual Mummy Mummy { get; set; }
    }
}
