using Hafazah.Model.Enums;
using System.Collections.Generic;

namespace Hafazah.Model.Entities.Program
{
    public class Path : BaseEntity
    {
        public string Name { get; set; }
        public string Duration { get; set; } // Ex: 6 month , 1 year , 2 years
        public int TotalNumber { get; set; } // Ex:  6 Stages , 30 Level
        public decimal RequiredWorkingDays { get; set; }
        public decimal RequiredPagesToSubmit { get; set; }
        public string Description { get; set; }

        #region RelationShips
        public ProgramType ProgramType { get; set; }
        public List<Phase> Phases { get; set; }
        public List<Level> Levels { get; set; }
        #endregion

        public bool IsOpenToRegistration { get; set; }
    }
}
