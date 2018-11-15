using Newtonsoft.Json;

namespace WebApi.Models
{
    public partial class EnvironmentReleaseNote
    {
        public int EnvironmentId { get; set; }
        public int ReleaseNoteId { get; set; }

        [JsonIgnore]// -> to avoid runtime   Error Self referencing loop detected for type
        public Environment Environment { get; set; }
        [JsonIgnore]// -> to avoid runtime   Error Self referencing loop detected for type
        public ReleaseNote ReleaseNote { get; set; }
    }
}