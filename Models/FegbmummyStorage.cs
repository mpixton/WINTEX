using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WINTEX.Models
{
    [Table("FEGBMummyStorage")]
    [Index(nameof(MummyId), Name = "fki_FEGBMummyStorage_MummyID_Mummies")]
    [Index(nameof(ShelfId), Name = "fki_FEGBMummyStorage_StorageId_FEGBStorage")]
    public partial class FegbmummyStorage
    {
        [Key]
        [Display(Name = "Mummy Id")]
        public int MummyId { get; set; }

        [Key]
        [Display(Name = "Shelf Id")]
        public int ShelfId { get; set; }

        [ForeignKey(nameof(MummyId))]
        [InverseProperty("FegbmummyStorages")]
        public virtual Mummy Mummy { get; set; }

        [ForeignKey(nameof(ShelfId))]
        [InverseProperty(nameof(FegbstorageLocation.FegbmummyStorages))]
        [Display(Name = "Shelf")]
        public virtual FegbstorageLocation Shelf { get; set; }
    }
}
