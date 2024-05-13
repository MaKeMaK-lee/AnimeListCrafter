using AnimeListCrafter.Classes;
using AnimeListCrafter.Entity;
using System.Text;

namespace AnimeListCrafter
{
    public class Program
    {
        static void Main(string[] args)
        {
            CrafterWorkIsNotWolf();

            Console.WriteLine("Фсё.");
            Console.ReadKey();
        }

        static void CrafterWorkIsNotWolf()
        {
            Console.WriteLine("Скормите ники шики:");

            List<string> usernames = new List<string>();

            string? tmpUsername;
            do
            {
                if (usernames.Count > 0)
                {
                    Console.WriteLine("Добавлены ники:");
                    foreach (var n in usernames)
                        Console.WriteLine("\t" + n);
                    Console.WriteLine();
                }
                Console.WriteLine("Нажмите:\n" +
                    "ВВОД, чтобы продолжить\n" +
                    "M, чтобы добавить ник MaKeMaK\n" +
                    "R, чтобы добавить ник RuruDotaOneLove\n" +
                    "V, чтобы добавить ник RuffyZ\n" +
                    "D, чтобы добавить ник DemetriouS\n" +
                    "Любую другую клавишу, чтобы ввести ник вручную.");

                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.M:
                        tmpUsername = "MaKeMaK";
                        break;
                    case ConsoleKey.R:
                        tmpUsername = "RuruDotaOneLove";
                        break;
                    case ConsoleKey.V:
                        tmpUsername = "RuffyZ";
                        break;
                    case ConsoleKey.D:
                        tmpUsername = "DemetriouS";
                        break;
                    case ConsoleKey.Enter:
                        tmpUsername = null;
                        break;
                    default:
                        Console.WriteLine("Введите имя пользователя или оставьте пустым для возврата в предыдущее меню:");
                        tmpUsername = Console.ReadLine();
                        break;
                }
                if (!string.IsNullOrEmpty(tmpUsername))
                    usernames.Add(tmpUsername);

                Console.WriteLine();
            } while (tmpUsername != null);



            if (usernames.Count == 0)
            {
                Console.WriteLine("Рил?");
                return;
            }

            var animeListFiles = usernames.Select(u => Exporter.ExportAnimeListFromShikiToFileXml(u));
            var animeLists = animeListFiles.Select(u => Parser.ImportAnimeListFromXml(u));
            var animes = animeLists.SelectMany(list => list).DistinctBy(anime => anime.series_animedb_id).ToList();

            var animeListFilesJson = usernames.Select(u => Exporter.ExportAnimeListFromShikiToFileJson(u));
            var animeListsJson = animeListFilesJson.Select(u => Parser.ImportIdAndTitlesRuFromJson(u));
            var idTirleRuDictionary = animeListsJson.SelectMany(list => list)
                .DistinctBy(e => e.Key)
                .ToDictionary(e => e.Key, e => e.Value);


            DateTime startingTime = DateTime.Now;
            Console.WriteLine("Крафчу");

            foreach (var anime in animes)
            {
                var titleRu = idTirleRuDictionary.GetValueOrDefault(anime.series_animedb_id, "");
                if (string.IsNullOrEmpty(titleRu))
                    titleRu = anime.series_title;

                anime.TitleRu = titleRu;
            }

            var filteredAnimes = animes
                .Where(anime =>
                                  anime.shiki_status == "Plan to Watch" ? false : false ||
                                  anime.shiki_status == "Watching" ? true : false ||
                                  anime.shiki_status == "Rewatching" ? true : false ||
                                  anime.shiki_status == "Completed" ? true : false ||
                                  anime.shiki_status == "On-Hold" ? true : false ||
                                  anime.shiki_status == "Dropped" ? true : false)
                .Where(anime =>
                                  anime.series_type == "tv" ? true : false ||
                                  anime.series_type == "ova" ? false : false ||
                                  anime.series_type == "special" ? false : false ||
                                  anime.series_type == "movie" ? true : false ||
                                  anime.series_type == "ona" ? true : false ||
                                  anime.series_type == "music" ? false : false ||
                                  anime.series_type == "" ? false : false
                );

            var lines = filteredAnimes.Select(anime => anime.TitleRu).Order();

            Console.WriteLine($"Скрафчено. {(DateTime.Now - startingTime).TotalSeconds:f2} с");
            Console.WriteLine();

            DateTime startingTime2 = DateTime.Now;
            Console.WriteLine("Сру...");

            string dt = string.Format($"{DateTime.Now:u}").Replace(':', '.');
            var names = new StringBuilder().AppendJoin(" ", usernames.Distinct()).ToString();

            var resultFilename = $"Result {dt} {names}.txt";
            Writer.WriteAllLines(resultFilename, lines);

            Console.WriteLine("Насрано в " + resultFilename + $". {(DateTime.Now - startingTime2).TotalSeconds:f2} с");
            Console.WriteLine();

        }

    }
}
//TODO switch xml downloading to xmldoc.Load