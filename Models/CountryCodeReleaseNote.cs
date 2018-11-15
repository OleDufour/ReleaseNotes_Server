using Newtonsoft.Json;

namespace WebApi.Models
{
    public partial class CountryCodeReleaseNote
    {
        public int CountryCodeId { get; set; }
        public int ReleaseNoteId { get; set; }

        [JsonIgnore] // -> to avoid runtime   Error Self referencing loop detected for type
        public CountryCode CountryCode { get; set; }
        [JsonIgnore]// -> to avoid runtime   Error Self referencing loop detected for type
        public ReleaseNote ReleaseNote { get; set; }
    }
}
