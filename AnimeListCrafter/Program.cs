using AnimeListCrafter.Classes;
using AnimeListCrafter.Entity;
using System.Collections;
using System.Text;

namespace AnimeListCrafter
{
    public class Program
    {
        static void Main(string[] args)
        {
            CrafterWorkIsNotWolf();

            Console.WriteLine("Фсё.");
            Console.ReadKey(true);
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

            var filterSettings = new FilterSettings();
            Console.WriteLine("Нажмите F чтобы изменить настройки фильтрации, или любую клавишу для применения настроек по умолчанию");
            if (Console.ReadKey(true).Key == ConsoleKey.F)
            {
                Console.WriteLine("Укажите значения (N/Q/-/Z - нет, остальные - да)");

                var dicSetAction = (Dictionary<string, bool> dic) =>
                {
                    foreach (var keyValuePair in dic)
                    {
                        Console.Write(keyValuePair.Key != "" ? keyValuePair.Key : "unknown");
                        var key = Console.ReadKey(true).Key;
                        bool anwser = !new ConsoleKey[] { ConsoleKey.N, ConsoleKey.Q, ConsoleKey.Z, ConsoleKey.OemMinus }.Contains(key);

                        Console.WriteLine(anwser ? " +" : " -");
                        dic[keyValuePair.Key] = anwser;
                    }
                };

                Console.WriteLine();
                dicSetAction(filterSettings.shiki_status);
                Console.WriteLine();
                dicSetAction(filterSettings.series_type);
            }
            Console.WriteLine();


            var animeListFiles = usernames.Select(u => Exporter.ExportAnimeListFromShikiToFileXml(u));
            var animeLists = animeListFiles.Select(u => Parser.ImportAnimeListFromXml(u)
                .Where(anime => filterSettings.shiki_status.GetValueOrDefault(anime.shiki_status, false))
                .Where(anime => filterSettings.series_type.GetValueOrDefault(anime.series_type, false))
                );
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

            Console.WriteLine("В");

            var lines = animes.Select(anime => anime.TitleRu).Order();

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