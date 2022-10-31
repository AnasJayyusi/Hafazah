using System.Collections.Generic;

namespace Hafazah.Model.Entities.Program
{
    public class Level : Common
    {
        public int? LevelNumber { get; set; }

        #region RelationShips
        public List<LevelHomework> Homework { get; set; }
        #endregion
    }
}
