using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WINTEX.Models.ViewModels
{
    public class MummyViewModel //View Model for mummy information/input of mummy burial excavation field data
    {

        //Location
        [Required]
        [Column("YLower")]
        public int? Ylower { get; set; }
        [Required]
        [Column("YUpper")]
        public int? Yupper { get; set; }
        [Required]
        [MaxLength(1)]
        public char? North { get; set; } //N
        [Required]
        [Column("XLower")]
        public int? Xlower { get; set; }
        [Required]
        [Column("XUpper")]
        public int? Xupper { get; set; }
        [Required]
        [MaxLength(1)]
        public char? East { get; set; } //E
        [Required]
        [StringLength(8)]
        public string Subplot { get; set; } //SE, NE, SW, or NW. (one of the four)
        
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
        public bool? ArtifactFound { get; set; } //Burial Goods, yes or no
        [Required]
        public bool? Photo { get; set; }
        [Required]
        public string PreservationIndex { get; set; }
        [Required]
        public string ClusterNum { get; set; }
        [Required]
        public string HairColorCode { get; set; }
        [Required]
        public string AgeCodeSingle { get; set; }
        [Required]
        public string BurialMaterials { get; set; }
        [Required]
        public string ExcavationRecorder { get; set; }

        //date/time
        [Required]
        [Column(TypeName = "date")] //---------FIXME-------
        public DateTime? DateTime { get; set; }


        //Notes
        [Required]
        [StringLength(15)]
        public string NoteType { get; set; }
        [Required]
        [StringLength(500)]
        public string NoteBody { get; set; }

        
        
    }
}
