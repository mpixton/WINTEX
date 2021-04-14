using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WINTEX.Models
{
    [Index(nameof(ShaftId), Name = "fki_BiologicalSamples_LocationId_ShaftLocations")]
    [Index(nameof(MummyId), Name = "fki_BiologicalSamples_MummyId_Mummies")]
    public partial class BiologicalSample
    {
        public BiologicalSample()
        {
            BioSamplesNotes = new HashSet<BioSamplesNote>();
        }

        [Key]
        [Display(Name = "Biological Sample Id")]
        public int BioSampleId { get; set; }

        [StringLength(2)]
        [Display(Name = "Rack Number")]
        public string RackNum { get; set; }

        [Display(Name = "Bag Number")]
        public int? BagNum { get; set; }
        
        [Display(Name = "Shaft Id")]
        public int? ShaftId { get; set; }

        [StringLength(15)]
        [Display(Name = "Burial Number")]
        public string BurialNum { get; set; }

        [Display(Name = "Mummy Id")]
        public int? MummyId { get; set; }

        [Display(Name = "Cluster Number")]
        public int? ClusterNum { get; set; }

        [StringLength(150)]
        public string Notes { get; set; }

        [StringLength(5)]
        public string Initials { get; set; }

        [StringLength(3)]
        [Display(Name = "Sampled Month")]
        public string SampledMonth { get; set; }

        [Display(Name = "Sampled Day")]
        public int? SampledDay { get; set; }

        [Display(Name = "Sampled Year")]
        public int? SampledYear { get; set; }

        [Display(Name = "Previously Sampled")]
        public bool? PreviouslySampled { get; set; }

        [ForeignKey(nameof(MummyId))]
        [InverseProperty("BiologicalSamples")]
        public virtual Mummy Mummy { get; set; }

        [ForeignKey(nameof(ShaftId))]
        [InverseProperty(nameof(ShaftLocation.BiologicalSamples))]
        [Display(Name = "Shaft Location")]
        public virtual ShaftLocation Shaft { get; set; }

        [InverseProperty(nameof(BioSamplesNote.BioSample))]
        [Display(Name = "Notes")]
        public virtual ICollection<BioSamplesNote> BioSamplesNotes { get; set; }
    }
}
