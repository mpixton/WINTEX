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
        [Display(Name = "Biological Sample Note Id")]
        public int BioNoteId { get; set; }

        [Display(Name = "Biological Sample Id")]
        public int BioSampleId { get; set; }
        
        [StringLength(200)]
        [Display(Name = "Note")]
        public string NoteBody { get; set; }

        [ForeignKey(nameof(BioSampleId))]
        [InverseProperty(nameof(BiologicalSample.BioSamplesNotes))]
        [Display(Name = "Biological Sample")]
        public virtual BiologicalSample BioSample { get; set; }
    }
}
