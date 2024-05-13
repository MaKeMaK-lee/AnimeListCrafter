
namespace AnimeListCrafter.Entity
{
    public class Anime
    {
        public string series_title = "No Title";
        public string series_type = "No Type";
        public int series_episodes = int.MaxValue;
        public int series_animedb_id = int.MaxValue;
        public int my_watched_episodes = int.MaxValue;
        public int my_times_watched = int.MaxValue;
        public byte my_score = byte.MaxValue;
        public string my_status = "No status";
        public string shiki_status = "No sstatus";
        public string my_comments = "No comment";
        public bool IsEnabledInSort = true;

        public string TitleRu { get; set; } = "";

        public Anime(string Title, string Type, int Episodes, int Id, int MyEpisodes, int MyTimesRewatched, byte MyScore, string MyStatus, string ShikiStatus, string Comment)
        {
            series_title = Title;
            series_type = Type;
            series_episodes = Episodes;
            series_animedb_id = Id;
            my_watched_episodes = MyEpisodes;
            my_times_watched = MyTimesRewatched;
            my_score = MyScore;
            my_status = MyStatus;
            shiki_status = ShikiStatus;
            my_comments = Comment;
        }

        public Anime(Anime a)
        {
            series_title = a.series_title;
            series_type = a.series_type;
            series_episodes = a.series_episodes;
            series_animedb_id = a.series_animedb_id;
            my_watched_episodes = a.my_watched_episodes;
            my_times_watched = a.my_times_watched;
            my_score = a.my_score;
            my_status = a.my_status;
            shiki_status = a.shiki_status;
            my_comments = a.my_comments;
        }

        public Anime()
        {

        }
    }

}
