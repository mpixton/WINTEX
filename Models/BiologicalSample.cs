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
        public int BioSampleId { get; set; }

        [StringLength(2)]
        public string RackNum { get; set; }

        public int? BagNum { get; set; }

        public int? ShaftId { get; set; }

        [StringLength(15)]
        public string BurialNum { get; set; }

        public int? MummyId { get; set; }

        public int? ClusterNum { get; set; }

        [StringLength(150)]
        public string Notes { get; set; }

        [StringLength(5)]
        public string Initials { get; set; }

        [StringLength(3)]
        public string SampledMonth { get; set; }

        public int? SampledDay { get; set; }

        public int? SampledYear { get; set; }

        public bool? PreviouslySampled { get; set; }

        [ForeignKey(nameof(MummyId))]
        [InverseProperty("BiologicalSamples")]
        public virtual Mummy Mummy { get; set; }

        [ForeignKey(nameof(ShaftId))]
        [InverseProperty(nameof(ShaftLocation.BiologicalSamples))]
        public virtual ShaftLocation Shaft { get; set; }

        [InverseProperty(nameof(BioSamplesNote.BioSample))]
        public virtual ICollection<BioSamplesNote> BioSamplesNotes { get; set; }
    }
}
