using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WINTEX.Models
{
    [Index(nameof(BioSampleId), Name = "fki_B")]
    public partial class BioSamplesNote
    {
        [Key]
        public int BioNoteId { get; set; }

        public int BioSampleId { get; set; }

        [StringLength(200)]
        public string NoteBody { get; set; }


        [ForeignKey(nameof(BioSampleId))]
        [InverseProperty(nameof(BiologicalSample.BioSamplesNotes))]
        public virtual BiologicalSample BioSample { get; set; }
    }
}
