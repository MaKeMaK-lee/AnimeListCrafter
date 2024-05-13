using AnimeListCrafter.Entity;
using System.Text.Json;
using System.Xml;

namespace AnimeListCrafter.Classes
{
    public static class Parser
    {
        public static IDictionary<int, string> ImportIdAndTitlesRuFromJson(string fileName)
        {
            DateTime startingTime = DateTime.Now;
            Console.WriteLine("Читаю " + fileName+ "...");

            if (File.Exists(fileName))
            {
                string jsonString = File.ReadAllText(fileName);
                var animeJsonList = JsonSerializer.Deserialize<IEnumerable<AnimeJson>>(jsonString);

                if (animeJsonList != null)
                {
                    Console.WriteLine($"Прочитано. {(DateTime.Now - startingTime).TotalSeconds:f2} с");
                    Console.WriteLine();

                    return animeJsonList.Select(animeJson => (animeJson.target_id, animeJson.target_title_ru)).ToDictionary();
                }
            }


            return null;
        }


        public static IEnumerable<Anime> ImportAnimeListFromXml(string fileName)
        {
            DateTime startingTime = DateTime.Now;
            Console.WriteLine("Читаю " + fileName + "...");

            var list = new List<Anime>();
            XmlDocument XmlFileImportedAnimes = new XmlDocument();
            while (true)
            {
                try
                {
                    XmlFileImportedAnimes.Load(fileName);
                    XmlElement xRoot = XmlFileImportedAnimes.DocumentElement;
                    int ImportedAnimeCount = 0;
                    foreach (XmlNode xNodeAnime in xRoot)
                    {
                        if (xNodeAnime.Name == "anime")
                        {
                            Anime tmpAnime = new Anime();
                            foreach (XmlNode xNodePropertie in xNodeAnime)
                            {
                                if (xNodePropertie.Name == "series_title")
                                    tmpAnime.series_title = xNodePropertie.InnerText;
                                if (xNodePropertie.Name == "series_type")
                                    tmpAnime.series_type = xNodePropertie.InnerText;
                                if (xNodePropertie.Name == "series_episodes")
                                    tmpAnime.series_episodes = Convert.ToInt32(xNodePropertie.InnerText);
                                if (xNodePropertie.Name == "series_animedb_id")
                                    tmpAnime.series_animedb_id = Convert.ToInt32(xNodePropertie.InnerText);
                                if (xNodePropertie.Name == "my_watched_episodes")
                                    tmpAnime.my_watched_episodes = Convert.ToInt32(xNodePropertie.InnerText);
                                if (xNodePropertie.Name == "my_times_watched")
                                    tmpAnime.my_times_watched = Convert.ToInt32(xNodePropertie.InnerText);
                                if (xNodePropertie.Name == "my_score")
                                    tmpAnime.my_score = Convert.ToByte(xNodePropertie.InnerText);
                                if (xNodePropertie.Name == "my_status")
                                    tmpAnime.my_status = xNodePropertie.InnerText;
                                if (xNodePropertie.Name == "shiki_status")
                                    tmpAnime.shiki_status = xNodePropertie.InnerText;
                                if (xNodePropertie.Name == "my_comments")
                                    tmpAnime.my_comments = xNodePropertie.InnerText;
                            }
                            list.Add(tmpAnime);
                            ImportedAnimeCount++;
                        }

                    }

                }
                catch (System.IO.FileNotFoundException)
                {
                    Console.WriteLine("ERROR: Ошибка загрузки списка аниме из экспортированного файла: файл не был найден, лист не загружен. Попробуйте потыкать там на кнопки, ну, вы поняли...");
                }
                catch (System.IO.IOException)
                {
                    continue;
                }
                break;
            }
            Console.WriteLine($"Прочитано. {(DateTime.Now - startingTime).TotalSeconds:f2} с");
            Console.WriteLine();

            return list;
        }
    }
}
