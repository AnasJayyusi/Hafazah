﻿namespace Hafazah.Model.Entities.Program
{
    public class Level : Common
    {
        public int? LevelNumber { get; set; }

        #region RelationShips
        public int PathId { get; set; }
        public Path Path { get; set; }
        #endregion
    }
}
