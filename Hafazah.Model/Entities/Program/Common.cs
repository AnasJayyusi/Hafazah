using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Hafazah.Model.Entities.Program
{
    public class Common : BaseEntity
    {
        public int? TotalPageNumber { get; set; }
        public int PageFrom { get; set; }
        public int PageTo { get; set; }
        [MaxLength(50)]
        public string SurahFrom { get; set; }
        [MaxLength(50)]
        public string SurahTo { get; set; }
        public int MaxNumberOfExcuses { get; set; }
        public string Description { get; set; }
        public int HomeWorkTotalCount { get; set; }
        
        #region RelationShips
        public int PathId { get; set; }
        public Path Path { get; set; }
        #endregion
    }
}
