

namespace AnimeListCrafter.Entity
{
    public class AnimeJson
    {
        public required string target_title { get; set; }
        public required string target_title_ru { get; set; }
        public required int target_id { get; set; }
        public required string target_type { get; set; }
        public required int score { get; set; }
        public required string status { get; set; }
        public required int rewatches { get; set; }
        public required int episodes { get; set; }
        public required string text { get; set; }
    }
}
