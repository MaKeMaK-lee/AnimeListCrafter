
using System.Net;

namespace AnimeListCrafter.Classes
{
    public static class Exporter
    {
        /// <returns>exported filename</returns>
        public static string ExportAnimeListFromShikiToFileXml(string _Username)
        {
            string newFilename = _Username + "_s_latest_export_animelist.xml";

            DateTime startingTime = DateTime.Now;
            Console.WriteLine("Качаю XML список " + _Username);

            try
            {
                WebClient client = new();
                client.DownloadFile(new Uri("https://shikimori.one/" + _Username + "/list_export/animes.xml"), newFilename);
            }
            catch
            {
                return "";
            }

            Console.WriteLine($"Скачано. {(DateTime.Now - startingTime).TotalSeconds:f2} с");
            Console.WriteLine();

            return newFilename;
        }

        /// <returns>exported filename</returns>
        public static string ExportAnimeListFromShikiToFileJson(string _Username)
        {
            string newFilename = _Username + "_s_latest_export_animelist.json";

            DateTime startingTime = DateTime.Now;
            Console.WriteLine("Качаю JSON список " + _Username);

            try
            {
                WebClient client = new();
                client.DownloadFile(new Uri("https://shikimori.one/" + _Username + "/list_export/animes.json"), newFilename);
            }
            catch
            {
                return "";
            }
            Console.WriteLine($"Скачано. {(DateTime.Now - startingTime).TotalSeconds:f2} с");
            Console.WriteLine();

            return newFilename;
        }

    }
}
