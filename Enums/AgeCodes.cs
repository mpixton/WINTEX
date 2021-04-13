using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WINTEX.Enums
{
    public class AgeCodes
    {
        [Display(Name = "Adult")]
        public static string Adult = "A";
        [Display(Name = "Child")]
        public static string Child = "C";
        [Display(Name = "Neonate/Infant")]
        public static string NeonateInfant = "N/I";

        public static IEnumerable<string> GetValues()
        {
            return new List<string>()
            {
                Adult, Child, NeonateInfant
            };
        }
    }
}
