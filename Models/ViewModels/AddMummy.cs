using System;
using System.ComponentModel.DataAnnotations;
using WINTEX.Enums;

namespace WINTEX.Models.ViewModels
{
    public class AddMummy //View Model for mummy information/input of mummy burial excavation field data
    {
        public int ShaftLocationId { get; set; }

        public int TombLocationId { get; set; }

        [Required]
        public string BurialNum { get; set; }

        //measurements
        [Required]
        public decimal? BurialDepth { get; set; }
        [Required]
        public decimal? WestToHead { get; set; }
        [Required]
        public decimal? WestToFeet { get; set; }
        [Required]
        public decimal? SouthToHead { get; set; }
        [Required]
        public decimal? SouthToFeet { get; set; }
        [Required]
        public decimal? Length { get; set; }

        //details
        [Required]
        public YesNo ArtifactFound { get; set; } //Burial Goods, yes or no
        [Required]
        public bool Photo { get; set; }
        [Required]
        public string PreservationIndex { get; set; }
        [Required]
        public string ClusterNum { get; set; }
        [Required]
        public string HairColorCode { get; set; }
        [Required]
        public string AgeCodeSingle { get; set; }
        [Required]
        public YesNo BurialMaterials { get; set; }
        [Required]
        public string ExcavationRecorder { get; set; }

        //date/time
        [Required]
        public DateTime? DateTime { get; set; }
    }
}
