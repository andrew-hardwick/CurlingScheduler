using Newtonsoft.Json;
using System.Collections.Generic;

namespace CurlingScheduler.Model
{
    internal class Week
    {
        public IEnumerable<Draw> Draws { get; set; }

        [JsonIgnore]
        public IEnumerable<Game> UnbalancedGames { get; set; }
    }
}