using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WINTEX.Enums
{
    public static class CardinalDirections
    {
        [Display(Name = "East")]
        public static string East = "E";

        [Display(Name = "West")]
        public static string West = "W";

        [Display(Name = "North")]
        public static string North = "N";

        [Display(Name = "South")]
        public static string South = "S";

        [Display(Name = "NorthWest")]
        public static string NW = "NW";

        [Display(Name = "NorthEast")]
        public static string NE = "NE";
        
        [Display(Name = "SouthWest")]
        public static string SW = "SW";

        [Display(Name = "SouthEast")]
        public static string SE = "SE";

        public static IEnumerable<string> EastWest()
        {
            return new List<string>()
            {
                East, West
            };
        }

        public static IEnumerable<string> NorthSouth()
        {
            return new List<string>()
            {
                North, South
            };
        }

        public static IEnumerable<string> SubPlots()
        {
            return new List<string>()
            {
                NW, NE, SW, SE
            };
        }
    }
}
