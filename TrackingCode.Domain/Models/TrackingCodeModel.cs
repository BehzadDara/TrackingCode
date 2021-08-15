using TrackingCode.Domain.Implementations;

namespace TrackingCode.Domain.Models
{
    public class TrackingCodeModel: TrackableEntity
    {
        public string ProjectName { get; set; }
        public string PreName { get; set; }
        public string TrackingCodeGenerated { get; set; }
    }
}