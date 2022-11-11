namespace Hafazah.Model.Entities.Program
{
    public class LevelHomework: BaseEntity
    {
        public int? RequiredMemorizedFrom { get; set; }
        public int? RequiredMemorizedTo { get; set; }
        public int? AbilityConnectFrom { get; set; }
        public int? AbilityConnectTo { get; set; }
        public int? ReviewFrom { get; set; }
        public int? ReviewTo { get; set; }
        public int OrderNumber { get; set; }

        #region RelationShips
        public int? LevelId { get; set; }
        public Level Level { get; set; }
        #endregion
    }
}
