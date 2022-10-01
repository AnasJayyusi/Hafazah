using Hafazah.Model.Entities.DropDownListOptions;
using System.Collections.Generic;

namespace Hafazah.Model.Dtos
{
    public class DropDownListOptions
    {
        public List<Country> Countries { get; set; }
        public List<EducationLevel> EducationLevels { get; set; }
        public List<LevelName> LevelNames { get; set; }
        public List<QuranMemorized> QuranMemorized { get; set; }
    }


}
