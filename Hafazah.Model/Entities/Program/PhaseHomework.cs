namespace Hafazah.Model.Entities.Program
{
    public class PhaseHomework: BaseEntity
    {
        public int? RequiredMemorizedFrom { get; set; }
        public int? RequiredMemorizedTo { get; set; }

        public int? AbilityConnectFrom { get; set; }
        public int? AbilityConnectTo { get; set; }

        public int? ReviewFrom { get; set; }

        public int? ReviewTo { get; set; }

        #region RelationShips
        public int? PhaseId { get; set; }
        public Phase Phase { get; set; }
        #endregion
    }
}
