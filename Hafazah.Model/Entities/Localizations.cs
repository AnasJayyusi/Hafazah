
namespace Hafazah.Model.Entities
{
    public class Localization : IBaseEntity
    {
        public string ResourceId { get; set; }
        public string ResourceSet { get; set; }
        public string Locale { get; set; }
        public string Value { get; set; }
    }
}
