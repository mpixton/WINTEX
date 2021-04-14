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
        [Display(Name = "Shelf Id")]
        public int ShelfId { get; set; }

        [Display(Name = "Rack")]
        public int Rack { get; set; }

        [Required]
        [StringLength(1)]
        [Display(Name = "Shelf")]
        public string Shelf { get; set; }
        
        [Required]
        [StringLength(2)]
        [Display(Name = "Sub Shelf")]
        public string SubShelf { get; set; }

        [InverseProperty(nameof(FegbmummyStorage.Shelf))]
        [Display(Name = "Mummies")]
        public virtual ICollection<FegbmummyStorage> FegbmummyStorages { get; set; }
    }
}
