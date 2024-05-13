

namespace AnimeListCrafter.Classes
{
    internal class Writer
    {
        static internal void WriteAllLines(string fileName, IEnumerable<string> lines)
        {
            File.WriteAllLines(fileName, lines);
        }
    }
}
