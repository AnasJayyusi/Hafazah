namespace Hafazah.Model.Entities.Program
{
    public class Phase : Common
    {
        public int? PhaseNumber { get; set; }

        #region RelationShips
        public int PathId { get; set; }
        public Path Path { get; set; }
        #endregion
    }
}
