using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WINTEX.Models
{
    [Table("FEGBStorageLocations")]
    public partial class FegbstorageLocation
    {
        public FegbstorageLocation()
        {
            FegbmummyStorages = new HashSet<FegbmummyStorage>();
        }

        [Key]
        public int ShelfId { get; set; }
        public int Rack { get; set; }
        [Required]
        [StringLength(1)]
        public string Shelf { get; set; }
        [Required]
        [StringLength(2)]
        public string SubShelf { get; set; }

        [InverseProperty(nameof(FegbmummyStorage.Shelf))]
        public virtual ICollection<FegbmummyStorage> FegbmummyStorages { get; set; }
    }
}
