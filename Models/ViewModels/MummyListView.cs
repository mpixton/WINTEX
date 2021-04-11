using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WINTEX.Models.ViewModels
{
    public class MummyListView
    {
        // TODO - Add List View Action
        // Required attributes - Burial Location, Length, Depth, Burial Num, Sex, Age, Date Excavated, Artifacts, Head Direction, 
        // Hair, Preservation Index
        public string BurialLocation { get; set; }
        public decimal? Length { get; set; }
        public decimal? BurialDepth { get; set; }
        public string BurialNum { get; set; }
        public string Sex { get; set; }
        public string Age { get; set; }
        public string DateExcavated { get; set; }
        public bool? Artifacts { get; set; }
        public string HeadDirection { get; set; }
        public string Hair { get; set; }
        public string PreservationIndex { get; set; }

        public static implicit operator MummyListView(Mummy m)
        {
            var dateExcavated = m.DateExcavated.ToString();
            if (m.DateExcavated is null)
            {
                dateExcavated = $"{m.DayExcavated} {m.MonthExcavated} {m.YearExcavated}";
            }
            return new MummyListView()
            {
                Age = m.AgeCodeSingle,
                Artifacts = m.ArtifactFound,
                BurialDepth = m.BurialDepth,
                BurialLocation = m.Shaft.ToString(),
                BurialNum = m.BurialNum,
                Hair = m.HairColorCode,
                HeadDirection = m.HeadDirection,
                Length = m.Length,
                PreservationIndex = m.PreservationIndex,
                Sex = m.PostExhumationDatum.Sex,
                DateExcavated = dateExcavated
            };
        }
    }
}
