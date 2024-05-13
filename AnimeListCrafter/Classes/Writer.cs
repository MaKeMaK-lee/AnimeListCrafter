using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
