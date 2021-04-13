using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WINTEX.Enums
{
    public static class YesNo
    {
        [Display(Name = "Yes")]
        public static bool Yes = true;

        [Display(Name = "No")]
        public static bool No = false;

        public static IEnumerable<bool>GetValues()
        {
            return new List<bool>()
            {
                Yes, No
            };
        }
    }
}
