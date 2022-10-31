using System.Collections.Generic;

namespace Hafazah.Model.Entities.Program
{
    public class Phase : Common
    {
        public int? PhaseNumber { get; set; }

        #region RelationShips
     
        public List<PhaseHomework> Homework { get; set; }
        #endregion
    }
}
