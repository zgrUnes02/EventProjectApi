using System.ComponentModel;

namespace EventProjectApi.Helpers.Queries
{
    public class AllEventQuery
    {
        public string? eventDate { get; set; }

        [DefaultValue("asc")]
        public string? sortingDate { get; set; } = "asc";

        [DefaultValue(1)]
        public int page { get; set; } = 1;

        [DefaultValue(10)]
        public int pageSize { get; set; } = 10;
    }
}
