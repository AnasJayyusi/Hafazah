using System;
using System.ComponentModel.DataAnnotations;

namespace Hafazah.Model.Entities.Program
{
    public class Level :IBaseEntity
    {
        public int LevelNumber { get; set; }
        public int TotalPageNumber { get; set; }
        public int PageFrom { get; set; }
        public int PageTo { get; set; }
        [MaxLength(50)]
        public string SurahFrom { get; set; }
        [MaxLength(50)]
        public string SurahTo { get; set; }
        public DateTime EstimatedTime { get; set; }
        public int MaxNumberOfExcuses { get; set; }

        #region RelationShips
        public Member Member { get; set; }
        #endregion
    }
}
