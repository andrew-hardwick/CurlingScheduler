using System.Collections.Generic;

namespace CurlingScheduler.Model
{
    internal class Game
    {
        public IEnumerable<Team> Teams { get; set; }

        public int Draw { get; set; }

        public int Sheet { get; set; }
    }
}