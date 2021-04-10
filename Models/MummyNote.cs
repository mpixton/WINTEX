using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WINTEX.Models
{
    [Index(nameof(MummyId), Name = "fki_MummyNotes_MummyId_Mummies")]
    public partial class MummyNote
    {
        [Key]
        public long NoteId { get; set; }
        public int MummyId { get; set; }
        [StringLength(15)]
        public string NoteType { get; set; }
        [StringLength(500)]
        public string NoteBody { get; set; }

        [ForeignKey(nameof(MummyId))]
        [InverseProperty("MummyNotes")]
        public virtual Mummy Mummy { get; set; }
    }
}
