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
        [Display(Name = "Mummy Id")]
        public int MummyId { get; set; }

        [Display(Name = "Femur Head")]
        public decimal? FemurHead { get; set; }

        [Display(Name = "Humerus Head")]
        public decimal? HumerusHead { get; set; }

        [Display(Name = "Osteophytosis Present?")]
        public string Osteophytosis { get; set; }

        [Display(Name = "Pubic Symphysis Present?")]
        public string PubicSymphysis { get; set; }

        [Display(Name = "Femur Length")]
        public decimal? FemurLength { get; set; }

        [Display(Name = "Humerus Length")]
        public decimal? HumerusLength { get; set; }

        [Display(Name = "Tibia Length")]
        public decimal? TibiaLength { get; set; }

        [Display(Name = "Robust")]
        public int? Robust { get; set; }

        [Display(Name = "Supraorbital Ridges")]
        public int? SupraorbitalRidges { get; set; }

        [Display(Name = "Orbit Edge")]
        public int? OrbitEdge { get; set; }

        [Display(Name = "Parietal Bossing")]
        public int? ParietalBossing { get; set; }

        [Display(Name = "Gonian")]
        public int? Gonian { get; set; }

        [Display(Name = "Nuchal Crest")]
        public int? NuchalCrest { get; set; }
        
        [Display(Name = "Zygomatic Crest")]
        public int? ZygomaticCrest { get; set; }

        [Display(Name = "Cranial Suture")]
        public string CranialSuture { get; set; }
        
        [Display(Name = "Maximum Cranial Length")]
        public decimal? MaximumCranialLength { get; set; }

        [Display(Name = "Maximum Cranial Breadth")]
        public decimal? MaximumCranialBreadth { get; set; }

        [Display(Name = "Basion-Bregma Height")]
        public decimal? BasionBregmaHeight { get; set; }

        [Display(Name = "Basion-Nasion")]
        public decimal? BasionNasion { get; set; }

        [Display(Name = "Basion Prostion Length")]
        public decimal? BasionProstionLength { get; set; }

        [Display(Name = "Bizygomatic Diameter")]
        public decimal? BizygomaticDiameter { get; set; }

        [Display(Name = "Nasion Prosthion")]
        public decimal? NasionProsthion { get; set; }

        [Display(Name = "Maximum Nasal Breadth")]
        public decimal? MaximumNasalBreadth { get; set; }

        [Display(Name = "Basilar Suture")]
        public string BasilarSuture { get; set; }

        [Display(Name = "Ventral Arc")]
        public int? VentralArc { get; set; }

        [Display(Name = "Sub-Pubic Angle")]
        public int? SubpubicAngle { get; set; }

        [Display(Name = "Sciatic Notch")]
        public int? SciaticNotch { get; set; }

        [Display(Name = "Pubic Bone")]
        public int? PubicBone { get; set; }

        [Display(Name = "Preaur Sulcus")]
        public int? PreaurSulcus { get; set; }

        [Column("MedialIPRamus")]
        [Display(Name = "Medial IP Ramus")]
        public int? MedialIpramus { get; set; }

        [Display(Name = "Dorsal Pitting")]
        public int? DorsalPitting { get; set; }

        [Display(Name = "Interorbital Breadth")]
        public decimal? InterorbitalBreadth { get; set; }

        [Display(Name = "Burial Hair Color")]
        public string BurialHairColor { get; set; }

        [Display(Name = "Tooth Attrition")]
        public string ToothAttrition { get; set; }

        [Display(Name = "Tooth Eruption")]
        public string ToothEruption { get; set; }

        [Display(Name = "Pathology Anomalies")]
        public string PathologyAnomalies { get; set; }

        [Display(Name = "Ephiphyseal Union")]
        public string EphiphysealUnion { get; set; }

        [Display(Name = "Skull Trauma")]
        public bool? SkullTrauma { get; set; }

        [Display(Name = "Post-Crania Trauma")]
        public bool? PostcraniaTrauma { get; set; }

        [Display(Name = "Cribra Orbitala")]
        public bool? CribraOrbitala { get; set; }

        [Display(Name = "Porotic Hyperostosis")]
        public bool? PoroticHyperostosis { get; set; }

        [Display(Name = "Metopic Suture")]
        public bool? MetopicSuture { get; set; }

        [Display(Name = "Button Osteoma")]
        public bool? ButtonOsteoma { get; set; }

        [Display(Name = "Temporal Mandibular Joint Osteoarthritis")]
        public bool? TemporalMandibularJointOsteoarthritis { get; set; }

        [Display(Name = "Linear Hypoplasia Enamel")]
        public bool? LinearHypoplasiaEnamel { get; set; }

        [Display(Name = "Porotic Hyperostosis Locations")]
        public string PoroticHyperostosisLocations { get; set; }

        [Display(Name = "Osteology Unknown Comment")]
        public string OsteologyUnknownComment { get; set; }

        [Display(Name = "To Be Confirmed")]
        public bool? ToBeConfirmed { get; set; }

        [ForeignKey(nameof(MummyId))]
        [InverseProperty("OsteologicalMummyDatum")]
        public virtual Mummy Mummy { get; set; }
    }
}
